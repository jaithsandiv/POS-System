; =============================================================
; POS System Installer Script for Inno Setup
; Version: 1.0
; Description: Installs POS System with SQL Server Express 2022
; =============================================================

#define MyAppName "POS System"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "Serendib IT Solutions"
#define MyAppURL "https://serendibitsolutions.com"
#define MyAppExeName "POS.exe"
#define MyAppCopyright "Copyright © 2026 Serendib IT Solutions"

; SQL Server Configuration
#define SQLInstanceName "SQLEXPRESS"
#define SQLDatabaseName "POS-db"
#define SQLPassword "POSSystem@2026!"

[Setup]
AppId={{A1B2C3D4-E5F6-7890-ABCD-EF1234567890}
SetupLogging=yes
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
AppCopyright={#MyAppCopyright}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
LicenseFile=License.txt
InfoBeforeFile=ReadMe.txt
OutputDir=Output
OutputBaseFilename=POSSystem_Setup_{#MyAppVersion}
Compression=lzma2/ultra64
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=admin
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
MinVersion=10.0.17763
DisableProgramGroupPage=yes
UninstallDisplayIcon={app}\{#MyAppExeName}
SetupIconFile=POSIcon.ico

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 6.1; Check: not IsAdminInstallMode

[Files]
; Application Files (from Release build)
Source: "..\POS\bin\Release\net8.0-windows\win-x64\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

; SQL Server Express Installer
Source: "Prerequisites\SQLServer2022-SSEI-Expr.exe"; DestDir: "{tmp}"; Flags: deleteafterinstall; Check: not IsSQLServerInstalled

; .NET 8 Desktop Runtime (if not using self-contained deployment)
Source: "Prerequisites\windowsdesktop-runtime-8.0.x-win-x64.exe"; DestDir: "{tmp}"; Flags: deleteafterinstall; Check: not IsDotNet8Installed

; Database Scripts (extract SetupHelper.bat to app for use during installation)
Source: "Database\RunDatabaseSetup.bat"; DestDir: "{app}\Database"; Flags: ignoreversion
Source: "Database\CreateDatabase.sql"; DestDir: "{app}\Database"; Flags: ignoreversion
Source: "Database\StoredProcedures.sql"; DestDir: "{app}\Database"; Flags: ignoreversion

; Config template
Source: "Config\App.config.template"; DestDir: "{app}"; DestName: "POS.dll.config"; Flags: ignoreversion

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
; Install .NET 8 Desktop Runtime (if needed)
Filename: "{tmp}\windowsdesktop-runtime-8.0.x-win-x64.exe"; Parameters: "/install /quiet /norestart"; StatusMsg: "Installing .NET 8 Desktop Runtime..."; Flags: waituntilterminated; Check: not IsDotNet8Installed

; Install SQL Server Express (if needed)
Filename: "{tmp}\SQLServer2022-SSEI-Expr.exe"; Parameters: "/QS /ACTION=Install /FEATURES=SQLEngine /INSTANCENAME={#SQLInstanceName} /SECURITYMODE=SQL /SAPWD={#SQLPassword} /TCPENABLED=1 /IACCEPTSQLSERVERLICENSETERMS"; StatusMsg: "Installing SQL Server Express 2022... (This may take several minutes)"; Flags: waituntilterminated; Check: not IsSQLServerInstalled

; Setup database after SQL Server is installed (or if it already exists)
Filename: "{cmd}"; Parameters: "/c """"{app}\Database\RunDatabaseSetup.bat"" localhost\{#SQLInstanceName} {#SQLDatabaseName} WINDOWS - - ""{app}"""""; StatusMsg: "Setting up database..."; Flags: waituntilterminated

; Launch application option
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[UninstallRun]
; Optional: Add database backup before uninstall
Filename: "{cmd}"; Parameters: "/c sqlcmd -S localhost\{#SQLInstanceName} -U sa -P {#SQLPassword} -Q ""BACKUP DATABASE [{#SQLDatabaseName}] TO DISK = '{userappdata}\{#MyAppName}\backup\{#SQLDatabaseName}_uninstall.bak'"""; Flags: runhidden; RunOnceId: "BackupDB"

[Code]
var
  SQLServerPage: TInputQueryWizardPage;
  UseExistingSQLServer: Boolean;
  ExistingServerName: String;
  ExistingServerAuth: String;
  ExistingServerUser: String;
  ExistingServerPassword: String;
  InstallerLogFile: String;

// ============================================
// Custom Logging for Installer
// ============================================
procedure LogToFile(Message: String);
begin
  if InstallerLogFile = '' then
    InstallerLogFile := ExpandConstant('{userdesktop}\POSSetup_{#MyAppVersion}.log');
  
  // Append message to log file
  SaveStringToFile(InstallerLogFile, Message + #13#10, True);
end;

// ============================================
// Get DB Connection Parameters
// ============================================
function GetDBServerName(Param: String): String;
begin
  if UseExistingSQLServer then
    Result := ExistingServerName
  else
    Result := 'localhost\{#SQLInstanceName}';
end;

function GetDBAuthMode(Param: String): String;
begin
  if UseExistingSQLServer and (ExistingServerAuth = 'Windows') then
    Result := 'WINDOWS'
  else
    Result := 'SQL';
end;

function GetDBUser(Param: String): String;
begin
  if UseExistingSQLServer and (ExistingServerAuth = 'SQL') then
    Result := ExistingServerUser
  else
    Result := 'sa';
end;

function GetDBPassword(Param: String): String;
begin
  if UseExistingSQLServer and (ExistingServerAuth = 'SQL') then
    Result := ExistingServerPassword
  else
    Result := '{#SQLPassword}';
end;

// ============================================
// Helper: Boolean to String
// ============================================
function BoolToStr(Value: Boolean): String;
begin
  if Value then
    Result := 'True'
  else
    Result := 'False';
end;

// ============================================
// Check if SQL Server Instance Exists
// ============================================
function IsSQLServerInstalled: Boolean;
var
  ResultCode: Integer;
begin
  Result := False;
  
  // Check registry for SQL Server instances
  if RegKeyExists(HKEY_LOCAL_MACHINE, 'SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL') then
  begin
    // Check specifically for SQLEXPRESS instance
    if RegValueExists(HKEY_LOCAL_MACHINE, 'SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL', '{#SQLInstanceName}') then
    begin
      Log('SQL Server {#SQLInstanceName} instance found');
      LogToFile('SQL Server {#SQLInstanceName} instance found in registry');
      Result := True;
    end;
  end;
  
  // Also check if we can connect to any existing SQL Server
  if not Result then
  begin
    if Exec('sqlcmd', '-S localhost\{#SQLInstanceName} -E -Q "SELECT 1"', '', SW_HIDE, ewWaitUntilTerminated, ResultCode) then
    begin
      if ResultCode = 0 then
      begin
        Log('SQL Server connection successful via Windows Auth');
        LogToFile('SQL Server connection successful');
        Result := True;
      end;
    end;
  end;
  
  if not Result then
  begin
    LogToFile('SQL Server NOT found - will be installed');
  end;
end;

// ============================================
// Check if .NET 8 Desktop Runtime is Installed
// ============================================
function IsDotNet8Installed: Boolean;
var
  ResultCode: Integer;
begin
  Result := False;
  
  // Check using dotnet --list-runtimes
  if Exec('cmd', '/c dotnet --list-runtimes | findstr "Microsoft.WindowsDesktop.App 8."', '', SW_HIDE, ewWaitUntilTerminated, ResultCode) then
  begin
    if ResultCode = 0 then
    begin
      Log('.NET 8 Desktop Runtime found');
      LogToFile('.NET 8 Desktop Runtime is installed');
      Result := True;
    end;
  end;
  
  // Alternative: Check registry
  if not Result then
  begin
    if RegKeyExists(HKEY_LOCAL_MACHINE, 'SOFTWARE\dotnet\Setup\InstalledVersions\x64\sharedfx\Microsoft.WindowsDesktop.App') then
    begin
      Log('.NET 8 Desktop Runtime registry key found');
      LogToFile('.NET 8 Desktop Runtime found via registry');
      Result := True;
    end;
  end;
  
  if not Result then
  begin
    LogToFile('.NET 8 Desktop Runtime NOT found - will be installed');
  end;
end;

// ============================================
// Check if database already exists
// ============================================
function DatabaseExists(ServerName, DBName, Password: String): Boolean;
var
  ResultCode: Integer;
  CmdLine: String;
begin
  Result := False;
  CmdLine := Format('-S %s -U sa -P %s -Q "SELECT name FROM sys.databases WHERE name = ''%s''"', [ServerName, Password, DBName]);
  
  if Exec('sqlcmd', CmdLine, '', SW_HIDE, ewWaitUntilTerminated, ResultCode) then
  begin
    Result := (ResultCode = 0);
  end;
end;

// ============================================
// Test SQL Server Connection
// ============================================
function TestSQLConnection(ServerName, Username, Password: String; UseWindowsAuth: Boolean): Boolean;
var
  ResultCode: Integer;
  CmdLine: String;
begin
  Result := False;
  
  if UseWindowsAuth then
    CmdLine := Format('-S %s -E -Q "SELECT 1"', [ServerName])
  else
    CmdLine := Format('-S %s -U %s -P %s -Q "SELECT 1"', [ServerName, Username, Password]);
  
  if Exec('sqlcmd', CmdLine, '', SW_HIDE, ewWaitUntilTerminated, ResultCode) then
  begin
    Result := (ResultCode = 0);
  end;
end;

// ============================================
// Verify Database Setup Batch File Exists
// ============================================
procedure CreateDatabaseSetupBatch(AppPath: String);
var
  BatchFile: String;
begin
  BatchFile := AppPath + '\Database\RunDatabaseSetup.bat';
  
  LogToFile('Verifying batch file exists at: ' + BatchFile);
  
  if FileExists(BatchFile) then
  begin
    LogToFile('Batch file found successfully');
  end
  else
  begin
    LogToFile('ERROR: Batch file was not extracted to ' + BatchFile);
    LogToFile('This file should have been included in the installer.');
  end;
end;

// ============================================
// Run Database Setup with Logging
// ============================================
procedure RunDatabaseSetup;
var
  ServerName: String;
  DBName: String;
  AppPath: String;
  BatchFile: String;
  CmdLine: String;
  ResultCode: Integer;
begin
  AppPath := ExpandConstant('{app}');
  ServerName := GetDBServerName('');
  DBName := ExpandConstant('{#SQLDatabaseName}');
  BatchFile := AppPath + '\Database\RunDatabaseSetup.bat';
  
  LogToFile('==========================================');
  LogToFile('Running Database Setup Batch Script');
  LogToFile('Batch File: ' + BatchFile);
  LogToFile('Server Name: ' + ServerName);
  LogToFile('Database Name: ' + DBName);
  LogToFile('Auth Type: WINDOWS');
  LogToFile('App Path: ' + AppPath);
  LogToFile('==========================================');
  
  // Ensure batch file exists (copy from source if needed)
  CreateDatabaseSetupBatch(AppPath);
  
  // Check if batch file exists
  if not FileExists(BatchFile) then
  begin
    LogToFile('ERROR: Batch file not found at ' + BatchFile);
    MsgBox('Database setup batch file not found at:' + #13 + BatchFile, mbError, MB_OK);
    Exit;
  end;
  
  LogToFile('Batch file found - executing...');
  
  // Execute the batch file
  // Parameters: ServerName, DBName, AuthType, Username, Password, AppPath
  CmdLine := '"' + BatchFile + '" ' + ServerName + ' ' + DBName + ' WINDOWS - - "' + AppPath + '"';
  
  if Exec(ExpandConstant('{cmd}'), '/c ' + CmdLine, '', SW_SHOW, ewWaitUntilTerminated, ResultCode) then
  begin
    LogToFile('Batch script execution completed with code: ' + IntToStr(ResultCode));
    if ResultCode = 0 then
      LogToFile('Database setup completed successfully')
    else
      LogToFile('WARNING: Batch script returned error code ' + IntToStr(ResultCode));
  end
  else
  begin
    LogToFile('ERROR: Failed to execute batch script');
  end;
  
  LogToFile('==========================================');
end;

// ============================================
// Create Database Folder Before Setup
// ============================================
procedure CreateDatabaseFolder;
var
  DatabaseFolder: String;
begin
  DatabaseFolder := ExpandConstant('{app}\Database');
  LogToFile('Creating Database folder: ' + DatabaseFolder);
  if not DirExists(DatabaseFolder) then
  begin
    CreateDir(DatabaseFolder);
    Log('Created database folder: ' + DatabaseFolder);
    LogToFile('Database folder created');
  end
  else
  begin
    LogToFile('Database folder already exists');
  end;
  
  LogToFile('Database folder setup complete');
end;

// ============================================
// Update Connection String in Config File
// ============================================
procedure UpdateConnectionString;
var
  ConfigFile: String;
  ConnectionString: String;
  ServerName: String;
  ResultCode: Integer;
  BatchFile: String;
  CmdLine: String;
begin
  LogToFile('Updating application connection string...');
  ConfigFile := ExpandConstant('{app}\POS.dll.config');
  BatchFile := ExpandConstant('{tmp}\UpdateConfig.bat');
  
  // Determine server name
  if UseExistingSQLServer and (ExistingServerName <> '') then
    ServerName := ExistingServerName
  else
    ServerName := 'localhost\{#SQLInstanceName}';
  
  LogToFile('Server: ' + ServerName);
  
  // Build connection string (Check for TrustServerCertificate which is needed for SQL 2022 compatibility)
  ConnectionString := Format('Server=%s;Database={#SQLDatabaseName};Trusted_Connection=True;TrustServerCertificate=True;', [ServerName]);
  
  Log('Connection string to apply: ' + ConnectionString);
  Log('Config file location: ' + ConfigFile);
  LogToFile('Config file: ' + ConfigFile);
  LogToFile('Connection string configured');
  
  // Create a batch file to update the config using PowerShell
  CmdLine := Format('powershell -NoProfile -ExecutionPolicy Bypass -Command "' +
    '[xml]$config = Get-Content ''%s''; ' +
    '$config.configuration.connectionStrings.add.connectionString = ''%s''; ' +
    '$config.Save(''%s''); ' +
    'Write-Host ''Config updated successfully''"', [ConfigFile, ConnectionString, ConfigFile]);
  
  // Execute the PowerShell command
  if Exec(ExpandConstant('{cmd}'), '/c ' + CmdLine, '', SW_HIDE, ewWaitUntilTerminated, ResultCode) then
  begin
    if ResultCode = 0 then
    begin
      Log('Connection string updated successfully');
      LogToFile('Connection string updated successfully');
    end
    else
    begin
      Log('Failed to update connection string, ResultCode: ' + IntToStr(ResultCode));
      LogToFile('Failed to update connection string, ResultCode: ' + IntToStr(ResultCode));
    end;
  end
  else
  begin
    Log('Failed to execute config update command');
    LogToFile('ERROR: Failed to execute config update command');
  end;
end;

// ============================================
// Custom Wizard Pages
// ============================================
procedure InitializeWizard;
begin
  LogToFile('========== POS System Installer Started ==========');
  LogToFile('Version: {#MyAppVersion}');
  LogToFile('Windows User: ' + GetEnv('USERNAME'));
  LogToFile('Computer Name: ' + GetEnv('COMPUTERNAME'));
  
  // Create SQL Server configuration page
  SQLServerPage := CreateInputQueryPage(wpSelectDir,
    'SQL Server Configuration',
    'Configure the database server for POS System.',
    'If you have an existing SQL Server, enter the server name below. Otherwise, leave blank to install SQL Server Express 2022 automatically.');
  
  SQLServerPage.Add('Server Name (e.g., localhost\SQLEXPRESS or SERVER\INSTANCE):', False);
  SQLServerPage.Add('Use Windows Authentication? (Yes/No):', False);
  SQLServerPage.Add('SQL Username (if using SQL Auth):', False);
  SQLServerPage.Add('SQL Password (if using SQL Auth):', True);
  
  // Set default values
  SQLServerPage.Values[0] := '';
  SQLServerPage.Values[1] := 'Yes';
  SQLServerPage.Values[2] := 'sa';
  SQLServerPage.Values[3] := '';
  
  UseExistingSQLServer := False;
end;

// ============================================
// Log Installation Steps
// ============================================
procedure CurStepChanged(CurStep: TSetupStep);
begin
  case CurStep of
    ssInstall:
      begin
        LogToFile('Installation step started');
        LogToFile('SQL Server: ' + SQLServerPage.Values[0]);
        LogToFile('Database Name: ' + SQLServerPage.Values[2]);
      end;
    ssPostInstall:
      begin
        LogToFile('Post-installation step started');
        LogToFile('Creating database folder and setting up database...');
        CreateDatabaseFolder;
        UpdateConnectionString;
      end;
    ssDone:
      begin
        LogToFile('Installation completed successfully');
        LogToFile('========== POS System Installer Finished ==========');
      end;
  end;
end;

// ============================================
// Validate SQL Server Page Input
// ============================================
function NextButtonClick(CurPageID: Integer): Boolean;
var
  ServerName: String;
  UseWindowsAuth: Boolean;
begin
  Result := True;
  
  if CurPageID = SQLServerPage.ID then
  begin
    ServerName := SQLServerPage.Values[0];
    UseWindowsAuth := (Uppercase(SQLServerPage.Values[1]) = 'YES');
    
    if ServerName <> '' then
    begin
      // User wants to use existing server
      ExistingServerName := ServerName;
      
      if UseWindowsAuth then
        ExistingServerAuth := 'Windows'
      else
      begin
        ExistingServerAuth := 'SQL';
        ExistingServerUser := SQLServerPage.Values[2];
        ExistingServerPassword := SQLServerPage.Values[3];
        
        if ExistingServerUser = '' then
        begin
          MsgBox('Please enter a SQL username or use Windows Authentication.', mbError, MB_OK);
          Result := False;
          Exit;
        end;
      end;
      
      // Test connection
      if not TestSQLConnection(ServerName, ExistingServerUser, ExistingServerPassword, UseWindowsAuth) then
      begin
        if MsgBox('Could not connect to the specified SQL Server. Do you want to continue anyway?', mbConfirmation, MB_YESNO) = IDNO then
        begin
          Result := False;
          Exit;
        end;
      end
      else
      begin
        UseExistingSQLServer := True;
        Log('Successfully connected to existing SQL Server: ' + ServerName);
      end;
    end
    else
    begin
      // Will install SQL Server Express
      UseExistingSQLServer := False;
      Log('No existing server specified - will install SQL Server Express');
    end;
  end;
end;

// ============================================
// Determine if SQL Server needs installation
// ============================================
function ShouldInstallSQLServer: Boolean;
begin
  Result := (not UseExistingSQLServer) and (not IsSQLServerInstalled);
end;

// ============================================
// Initialization
// ============================================
function InitializeSetup: Boolean;
begin
  Result := True;
  UseExistingSQLServer := False;
  ExistingServerName := '';
  ExistingServerAuth := '';
  ExistingServerUser := '';
  ExistingServerPassword := '';
  
  Log('POS System Installer Starting...');
  Log('SQL Server Check: ' + BoolToStr(IsSQLServerInstalled));
  Log('.NET 8 Check: ' + BoolToStr(IsDotNet8Installed));
end;

// ============================================
// Cleanup on Cancel
// ============================================
procedure DeinitializeSetup;
begin
  Log('Setup completed or cancelled');
end;
