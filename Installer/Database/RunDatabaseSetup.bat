@echo off
REM ============================================
REM POS Database Setup Script
REM ============================================
REM Parameters:
REM   %1 = Server Name (e.g., localhost\SQLEXPRESS)
REM   %2 = Database Name (e.g., POS-db)
REM   %3 = Auth Type (WINDOWS or SQL)
REM   %4 = Username (if SQL)
REM   %5 = Password (if SQL)
REM   %6 = Application Install Path
REM ============================================

setlocal enabledelayedexpansion

REM Read parameters
set SERVERNAME=%~1
set DBNAME=%~2
set AUTHTYPE=%~3
set DBUSER=%~4
set DBPASS=%~5
set APPPATH=%~6

REM Display everything to console first
echo.
echo ============================================
echo POS Database Setup Starting...
echo Parameters received:
echo   Server: [%SERVERNAME%]
echo   Database: [%DBNAME%]
echo   Auth Type: [%AUTHTYPE%]
echo   User: [%DBUSER%]
echo   Password: [%DBPASS%]
echo   App Path: [%APPPATH%]
echo ============================================
echo.

REM Validate parameters
if "!SERVERNAME!"=="" (
    echo ERROR: Server name is empty!
    pause
    exit /b 1
)

if "!DBNAME!"=="" (
    echo ERROR: Database name is empty!
    pause
    exit /b 1
)

if "!APPPATH!"=="" (
    echo ERROR: App path is empty!
    pause
    exit /b 1
)

REM Trim variables of any extra quotes
set AUTHTYPE=!AUTHTYPE:"=!

REM Create log directory if it doesn't exist
if not exist "!APPPATH!\Database" (
    echo Creating Database folder...
    mkdir "!APPPATH!\Database"
    if !errorlevel! neq 0 (
        echo ERROR: Failed to create Database folder at: !APPPATH!\Database
        pause
        exit /b 1
    )
)

REM Log file for debugging
set LOGFILE=!APPPATH!\Database\setup.log

REM Create initial log file
echo. > "!LOGFILE!"
echo ============================================ >> "!LOGFILE!"
echo POS Database Setup Log >> "!LOGFILE!"
echo Started: %date% %time% >> "!LOGFILE!"
echo ============================================ >> "!LOGFILE!"
echo. >> "!LOGFILE!"
echo Server: !SERVERNAME! >> "!LOGFILE!"
echo Database: !DBNAME! >> "!LOGFILE!"
echo Auth Type: !AUTHTYPE! >> "!LOGFILE!"
echo App Path: !APPPATH! >> "!LOGFILE!"
echo Log File: !LOGFILE! >> "!LOGFILE!"
echo. >> "!LOGFILE!"

if !errorlevel! neq 0 (
    echo ERROR: Failed to create log file at: !LOGFILE!
    echo Check folder permissions.
    pause
    exit /b 1
)

echo Log file created at: !LOGFILE!
echo.

REM Determine Auth Switches
echo Configuring authentication...
if /I "!AUTHTYPE!"=="WINDOWS" (
    echo Using Windows Authentication...
    set "AUTH_SWITCHES=-E"
    echo Using Windows Authentication >> "!LOGFILE!"    
    REM Fix for when installer passes dummy "-" or empty user
    if "!DBUSER!"=="-" set DBUSER=%USERNAME%
    if "!DBUSER!"=="" set DBUSER=%USERNAME%) else (
    set AUTH_SWITCHES=-U !DBUSER! -P !DBPASS!
    echo Using SQL Authentication with user: !DBUSER!
    echo Using SQL Authentication with user: !DBUSER! >> "!LOGFILE!"
)

REM Wait for SQL Server to be ready (after fresh install)
REM This is critical when SQL Server was just installed in the same setup session
echo.
echo Waiting for SQL Server to become available...
echo Waiting for SQL Server to be ready... >> "!LOGFILE!"

setlocal enabledelayedexpansion
set WAIT_COUNTER=0
set MAX_WAITS=60
set WAIT_INTERVAL=5

:WAIT_FOR_SQLSERVER
set /a WAIT_TIME=!WAIT_COUNTER! * !WAIT_INTERVAL!
echo [!WAIT_TIME!s] Checking SQL Server availability...
echo [!WAIT_TIME!s] Checking SQL Server on !SERVERNAME!... >> "!LOGFILE!"

REM Attempt connection with brief timeout
!SQLCMD! -S "!SERVERNAME!" !AUTH_SWITCHES! -Q "SELECT @@version" > nul 2>&1
if !errorlevel! equ 0 (
    echo SQL Server is ready!
    echo SQL Server is ready at !SERVERNAME! >> "!LOGFILE!"
    goto SQLSERVER_READY
)

REM If not ready, wait and retry
set /a WAIT_COUNTER=!WAIT_COUNTER! + 1
if !WAIT_COUNTER! lss !MAX_WAITS! (
    timeout /t !WAIT_INTERVAL! /nobreak > nul
    goto WAIT_FOR_SQLSERVER
) else (
    echo ERROR: SQL Server did not become available after !WAIT_TIME! seconds >> "!LOGFILE!"
    goto SQLSERVER_TIMEOUT
)

:SQLSERVER_TIMEOUT
echo.
echo ============================================
echo ERROR: SQL Server Connection Failed
echo ============================================
echo SQL Server at !SERVERNAME! did not respond after !WAIT_TIME! seconds
echo This may indicate:
echo   1. SQL Server installation is still in progress
echo   2. SQL Server failed to install or start
echo   3. Incorrect server name or credentials
echo   4. Firewall blocking connection
echo.
echo Check log file for details: !LOGFILE!
echo ============================================
echo.
pause
exit /b 1

:SQLSERVER_READY
echo.

REM Check if sqlcmd is available
echo Locating sqlcmd.exe...
echo Locating sqlcmd.exe... >> "!LOGFILE!"
where sqlcmd > nul 2>&1
if !errorlevel! neq 0 (
    echo sqlcmd not in PATH. Searching common SQL Server locations... >> "!LOGFILE!"
    set SQLCMD=
    
    REM SQL Server 2022
    if exist "C:\Program Files\Microsoft SQL Server\160\Tools\Binn\sqlcmd.exe" (
        set SQLCMD="C:\Program Files\Microsoft SQL Server\160\Tools\Binn\sqlcmd.exe"
        echo Found SQL Server 2022 >> "!LOGFILE!"
    )
    
    REM SQL Server 2019
    if not defined SQLCMD (
        if exist "C:\Program Files\Microsoft SQL Server\150\Tools\Binn\sqlcmd.exe" (
            set SQLCMD="C:\Program Files\Microsoft SQL Server\150\Tools\Binn\sqlcmd.exe"
            echo Found SQL Server 2019 >> "!LOGFILE!"
        )
    )
    
    REM SQL Server 2017
    if not defined SQLCMD (
        if exist "C:\Program Files\Microsoft SQL Server\140\Tools\Binn\sqlcmd.exe" (
            set SQLCMD="C:\Program Files\Microsoft SQL Server\140\Tools\Binn\sqlcmd.exe"
            echo Found SQL Server 2017 >> "!LOGFILE!"
        )
    )
    
    REM SQL Server 2016
    if not defined SQLCMD (
        if exist "C:\Program Files\Microsoft SQL Server\130\Tools\Binn\sqlcmd.exe" (
            set SQLCMD="C:\Program Files\Microsoft SQL Server\130\Tools\Binn\sqlcmd.exe"
            echo Found SQL Server 2016 >> "!LOGFILE!"
        )
    )
    
    REM Client SDK
    if not defined SQLCMD (
        if exist "C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\170\Tools\Binn\sqlcmd.exe" (
            set SQLCMD="C:\Program Files\Microsoft SQL Server\Client SDK\ODBC\170\Tools\Binn\sqlcmd.exe"
            echo Found SQL Server Client SDK >> "!LOGFILE!"
        )
    )
    
    if not defined SQLCMD (
        echo ERROR: Cannot find sqlcmd.exe >> "!LOGFILE!"
        echo ERROR: Cannot find sqlcmd.exe
        pause
        exit /b 1
    )
    echo sqlcmd.exe located at: !SQLCMD! >> "!LOGFILE!"
) else (
    set SQLCMD=sqlcmd
    echo sqlcmd found in PATH >> "!LOGFILE!"
)

REM Test SQL Server connection
echo Testing connection to: !SERVERNAME!
echo Command: !SQLCMD! -S "!SERVERNAME!" !AUTH_SWITCHES! -Q "SELECT 1"
echo Testing connection... >> "!LOGFILE!"
!SQLCMD! -S "!SERVERNAME!" !AUTH_SWITCHES! -Q "SELECT 1" -b >> "!LOGFILE!" 2>&1
if !errorlevel! neq 0 (
    echo.
    echo ============================================
    echo ERROR: Cannot connect to SQL Server
    echo Server: !SERVERNAME!
    echo Auth: !AUTHTYPE!
    echo Check log for details: !LOGFILE!
    echo ============================================
    echo.
    echo ERROR: Cannot connect to SQL Server >> "!LOGFILE!"
    pause
    exit /b 1
)

echo SQL Server connection successful >> "!LOGFILE!"
echo Connection successful!
echo.

REM Check if database exists
echo Checking if database '!DBNAME!' exists...
echo Checking if database exists... >> "!LOGFILE!"
!SQLCMD! -S "!SERVERNAME!" !AUTH_SWITCHES! -Q "IF DB_ID('!DBNAME!') IS NOT NULL SELECT 1 ELSE SELECT 0" -h -1 -W > "%temp%\dbcheck.txt" 2>&1
set /p DBEXISTS=<"%temp%\dbcheck.txt"

echo Database check result: !DBEXISTS! >> "!LOGFILE!"

if "!DBEXISTS!"=="1" (
    echo Database !DBNAME! already exists >> "!LOGFILE!"
    echo.
    echo Database '!DBNAME!' already exists.
    echo Creating backup...
    echo Creating backup before update... >> "!LOGFILE!"
    !SQLCMD! -S "!SERVERNAME!" !AUTH_SWITCHES! -Q "BACKUP DATABASE [!DBNAME!] TO DISK = '!APPPATH!\Database\!DBNAME!_backup.bak' WITH FORMAT" -b >> "!LOGFILE!" 2>&1
    echo Backup completed >> "!LOGFILE!"
) else (
    echo Creating new database... >> "!LOGFILE!"
    echo.
    echo Database '!DBNAME!' does not exist in system catalog.
    echo Checking for and removing orphaned data files to prevent conflicts...
    echo Checking for orphaned files... >> "!LOGFILE!"

    REM Create cleanup script for orphaned files
    (
        echo SET NOCOUNT ON;
        echo EXEC sp_configure 'show advanced options', 1; RECONFIGURE WITH OVERRIDE;
        echo EXEC sp_configure 'xp_cmdshell', 1; RECONFIGURE WITH OVERRIDE;
        echo GO
        echo.
        echo DECLARE @DataPath NVARCHAR(4000^);
        echo DECLARE @LogPath NVARCHAR(4000^);
        echo.
        echo -- Get default data/log paths
        echo SET @DataPath = CAST(SERVERPROPERTY('InstanceDefaultDataPath'^) AS NVARCHAR(4000^)^);
        echo SET @LogPath = CAST(SERVERPROPERTY('InstanceDefaultLogPath'^) AS NVARCHAR(4000^)^);
        echo.
        echo -- Fallbacks if null
        echo IF @DataPath IS NULL SET @DataPath = CAST(SERVERPROPERTY('InstanceDefaultLogPath'^) AS NVARCHAR(4000^)^);
        echo IF @DataPath IS NULL
        echo BEGIN
        echo     -- Last resort: try to derive from master db location
        echo     SELECT TOP 1 @DataPath = LEFT(physical_name, LEN(physical_name^) - CHARINDEX('\', REVERSE(physical_name^)^) + 1^)
        echo     FROM sys.master_files WHERE database_id = 1;
        echo END
        echo.
        echo IF @LogPath IS NULL SET @LogPath = @DataPath;
        echo.
        echo DECLARE @DbName NVARCHAR(255^) = '!DBNAME!';
        echo DECLARE @Cmd NVARCHAR(4000^);
        echo.
        echo -- Delete .mdf
        echo SET @Cmd = 'del /F /Q "' + @DataPath + @DbName + '.mdf"';
        echo PRINT 'Executing: ' + @Cmd;
        echo EXEC xp_cmdshell @Cmd;
        echo.
        echo -- Delete _log.ldf
        echo SET @Cmd = 'del /F /Q "' + @LogPath + @DbName + '_log.ldf"';
        echo PRINT 'Executing: ' + @Cmd;
        echo EXEC xp_cmdshell @Cmd;
        echo GO
        echo.
        echo EXEC sp_configure 'xp_cmdshell', 0; RECONFIGURE WITH OVERRIDE;
        echo EXEC sp_configure 'show advanced options', 0; RECONFIGURE WITH OVERRIDE;
        echo GO
    ) > "%temp%\cleanup_orphans.sql"

    REM Execute cleanup
    !SQLCMD! -S "!SERVERNAME!" !AUTH_SWITCHES! -i "%temp%\cleanup_orphans.sql" -b >> "!LOGFILE!" 2>&1
    
    echo Creating new database '!DBNAME!'...
    !SQLCMD! -S "!SERVERNAME!" !AUTH_SWITCHES! -Q "CREATE DATABASE [!DBNAME!]" -b >> "!LOGFILE!" 2>&1
    if !errorlevel! neq 0 (
        echo ERROR: Failed to create database >> "!LOGFILE!"
        echo.
        echo ERROR: Failed to create database
        echo Check log: !LOGFILE!
        pause
        exit /b 1
    )
    echo Database created successfully >> "!LOGFILE!"
    echo Database '!DBNAME!' created successfully!
    echo.
)

REM Run database schema script
echo Initializing database schema... >> "!LOGFILE!"
echo.
echo Initializing database schema...
if exist "!APPPATH!\Database\CreateDatabase.sql" (
    echo Running CreateDatabase.sql... >> "!LOGFILE!"
    !SQLCMD! -S "!SERVERNAME!" !AUTH_SWITCHES! -d !DBNAME! -i "!APPPATH!\Database\CreateDatabase.sql" -b >> "!LOGFILE!" 2>&1
    if !errorlevel! neq 0 (
        echo WARNING: Some errors occurred during schema creation >> "!LOGFILE!"
        echo WARNING: Some errors occurred during schema creation (tables may already exist)
    ) else (
        echo Schema created successfully >> "!LOGFILE!"
        echo Schema created successfully.
    )
) else (
    echo WARNING: CreateDatabase.sql not found at !APPPATH!\Database\ >> "!LOGFILE!"
    echo WARNING: CreateDatabase.sql not found at !APPPATH!\Database\
)

REM Run stored procedures script
echo Creating stored procedures... >> "!LOGFILE!"
echo Creating stored procedures...
if exist "!APPPATH!\Database\StoredProcedures.sql" (
    echo Running StoredProcedures.sql... >> "!LOGFILE!"
    !SQLCMD! -S "!SERVERNAME!" !AUTH_SWITCHES! -d !DBNAME! -i "!APPPATH!\Database\StoredProcedures.sql" -b >> "!LOGFILE!" 2>&1
    if !errorlevel! neq 0 (
        echo WARNING: Some errors occurred during stored procedure creation >> "!LOGFILE!"
        echo WARNING: Some errors occurred during stored procedure creation
    ) else (
        echo Stored procedures created successfully >> "!LOGFILE!"
        echo Stored procedures created successfully.
    )
) else (
    echo WARNING: StoredProcedures.sql not found >> "!LOGFILE!"
    echo WARNING: StoredProcedures.sql not found at !APPPATH!\Database\
)

REM Grant Windows user access to database for future Windows Authentication
if "!AUTHTYPE!"=="WINDOWS" (
    echo Setting up Windows user permissions... >> "!LOGFILE!"
    echo Setting up Windows user permissions...
    set CURRENT_USER=!DBUSER!
    echo Creating login for Windows user: !CURRENT_USER! >> "!LOGFILE!"
    
    REM Create SQL login for Windows user (ignore if already exists)
    REM Using a temporary file to avoid quote parsing issues
    (
        echo IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name = '[!CURRENT_USER!]')
        echo BEGIN
        echo   CREATE LOGIN [!CURRENT_USER!] FROM WINDOWS;
        echo END
    ) > "%temp%\create_login.sql"
    
    !SQLCMD! -S "!SERVERNAME!" !AUTH_SWITCHES! -i "%temp%\create_login.sql" >> "!LOGFILE!" 2>&1
    
    REM Add Windows user as database user with db_owner role
    (
        echo IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = '[!CURRENT_USER!]')
        echo BEGIN
        echo   CREATE USER [!CURRENT_USER!] FOR LOGIN [!CURRENT_USER!];
        echo END
    ) > "%temp%\create_user.sql"
    
    !SQLCMD! -S "!SERVERNAME!" !AUTH_SWITCHES! -d !DBNAME! -i "%temp%\create_user.sql" >> "!LOGFILE!" 2>&1
    
    REM Grant db_owner role (this may fail if already a member, which is OK)
    !SQLCMD! -S "!SERVERNAME!" !AUTH_SWITCHES! -d !DBNAME! -Q "EXEC sp_addrolemember 'db_owner', '[!CURRENT_USER!]'" >> "!LOGFILE!" 2>&1
    
    if !errorlevel! equ 0 (
        echo Windows user permissions configured successfully >> "!LOGFILE!"
        echo Windows user permissions configured successfully.
    ) else (
        echo NOTE: Permission setup completed (may have warnings, which are usually OK) >> "!LOGFILE!"
        echo NOTE: Permission setup completed (may have warnings, which are usually OK)
    )
)

REM Verify installation
echo Verifying database installation... >> "!LOGFILE!"
echo Verifying database installation...
!SQLCMD! -S "!SERVERNAME!" !AUTH_SWITCHES! -d !DBNAME! -Q "SELECT COUNT(*) AS TableCount FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'" -h -1 -W > "%temp%\tablecount.txt" 2>&1
set /p TABLECOUNT=<"%temp%\tablecount.txt"

echo ============================================ >> "!LOGFILE!"
echo Database Setup Complete! >> "!LOGFILE!"
echo Tables created: !TABLECOUNT! >> "!LOGFILE!"
echo Log file location: !LOGFILE! >> "!LOGFILE!"
echo ============================================ >> "!LOGFILE!"

echo ============================================
echo Database Setup Complete!
echo Tables created: !TABLECOUNT!
echo Log file saved to: !LOGFILE!
echo ============================================
echo.
echo Press any key to continue...
pause >nul

REM Cleanup temp files
del "%temp%\dbcheck.txt" > nul 2>&1
del "%temp%\tablecount.txt" > nul 2>&1

exit /b 0
