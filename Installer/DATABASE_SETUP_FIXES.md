# Database Setup Troubleshooting - Fixes Applied

## Issues Fixed

### 1. **Hidden Output During Database Setup**
   **Problem:** The `runhidden` flag prevented error messages from showing, making troubleshooting impossible
   
   **Solution:** Removed `runhidden` flag so you can see:
   - SQL Server connection attempts
   - Database creation status
   - SQL script execution results
   - Any errors that occur

### 2. **Insufficient Wait Time After SQL Server Installation**
   **Problem:** SQL Server 2022 Express needs 30+ seconds to fully initialize; the script only waited 10 seconds
   
   **Solution:** Increased timeout from 10 seconds to 30 seconds
   - SQL Server services need time to start
   - Registry needs time to be updated
   - Default databases need to be created

### 3. **Better sqlcmd.exe Detection**
   **Problem:** Script only checked a few paths, missing SQL Server 2022 location (160 folder)
   
   **Solution:** Now checks all common SQL Server versions in order:
   - SQL Server 2022 (160)
   - SQL Server 2019 (150)
   - SQL Server 2017 (140)
   - SQL Server 2016 (130)
   - Client SDK paths

### 4. **No Error Logging**
   **Problem:** When database setup failed, there was no way to debug the issue
   
   **Solution:** Created a comprehensive log file at:
   ```
   {InstallFolder}\Database\setup.log
   ```
   The log includes:
   - SQL Server location found
   - Connection attempts and results
   - Database creation status
   - Schema script execution details
   - Stored procedure creation results
   - Final table count verification

### 5. **Missing Database Folder**
   **Problem:** The Database folder might not exist before the batch file tries to create the log
   
   **Solution:** Added `CreateDatabaseFolder()` procedure that runs before database setup

## How to Use the Updated Installer

### Step 1: Recompile the Installer
Open `POSSetup.iss` and press Ctrl+F9 to compile with the updated script.

### Step 2: Run the Installer
When you run the installer:
1. You'll see a command window showing database setup progress
2. It will display connection attempts, creation status, and results
3. Any errors will be visible immediately

### Step 3: Check the Log File
If something goes wrong, the detailed log is saved at:
```
C:\Program Files\POS System\Database\setup.log
```

Example log output:
```
============================================
POS Database Setup Started
Date: 2/7/2026 14:30:45
Instance: SQLEXPRESS
Database: POS-db
App Path: C:\Program Files\POS System
============================================
Waiting for SQL Server...
Ready to proceed with database setup
Locating sqlcmd.exe...
Found SQL Server 2022
sqlcmd found in PATH
Testing SQL Server connection...
Attempting connection to localhost\SQLEXPRESS...
SQL Server connection successful
Checking if database exists...
Database check result: 0
Creating new database...
Running CreateDatabase.sql...
Database created successfully
Running StoredProcedures.sql...
Stored procedures created successfully.
Verifying database installation...
============================================
Database Setup Complete!
Tables created: 24
Log file location: C:\Program Files\POS System\Database\setup.log
============================================
```

## When Using Existing SQL Server

If you provide an existing server during installation:
1. The installer tests the connection immediately
2. If it fails, you can choose to continue or retry
3. The batch script will use your server credentials
4. The log file will show all connection attempts

## Common Issues and Solutions

### Issue: "Cannot find sqlcmd.exe"
**Solution:** The batch file now searches multiple SQL Server installation paths. If it still fails, verify SQL Server 2022 Express is actually installed.

### Issue: "Cannot connect to SQL Server"
**Cause:** Server might still be initializing
**Solution:** Manually run the batch file 30 seconds later:
```cmd
cd "C:\Program Files\POS System\Database"
RunDatabaseSetup.bat SQLEXPRESS POS-db POSSystem@2026! "C:\Program Files\POS System"
```

### Issue: Database exists but tables aren't created
**Check the log file:** `C:\Program Files\POS System\Database\setup.log`
Look for SQL script errors and verify the `.sql` files exist in the Database folder.

### Issue: Can't see database in SSMS
1. Verify the database name: should be `POS-db`
2. Verify the instance: should be `SQLEXPRESS` (or your provided server)
3. Check the log file for the actual results
4. Try refreshing SSMS or restarting it

## Next Steps After Installation

1. **Review the setup.log** to verify all tables were created
2. **Open SSMS** and verify `POS-db` exists with all tables
3. **Check the configuration** in `C:\Program Files\POS System\POS.dll.config`
4. **Run the application** and verify it connects to the database

## For Support

If the database still doesn't create:
1. Save the `setup.log` file
2. Check Windows Event Viewer for SQL Server errors
3. Verify SQL Server service is running:
   ```cmd
   sc query MSSQL$SQLEXPRESS
   ```
4. Contact support with the log file contents
