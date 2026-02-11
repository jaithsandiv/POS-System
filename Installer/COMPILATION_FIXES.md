# POS System Installer - Compilation Fixes

## Issues Found and Fixed

### 1. **LoadStringFromFile Type Mismatch Error**
   **Problem:** Inno Setup Pascal doesn't have `LoadStringFromFile` and `SaveStringToFile` functions
   
   **Solution:** Replaced with PowerShell XML manipulation approach:
   - Uses PowerShell to read and parse the XML config file
   - Updates the connectionString element dynamically
   - Saves the updated configuration back to the file
   
   **Code Location:** `UpdateConnectionString()` procedure

### 2. **Incorrect File Path for Application Files**
   **Problem:** Script referenced `Debug` build instead of `Release`
   
   **Solution:** Updated [Files] section to use Release build path:
   ```ini
   Source: "..\POS\bin\Release\net8.0-windows\*"; DestDir: "{app}"
   ```

### 3. **Missing Setup Icon File**
   **Problem:** SetupIconFile directive referenced `POSIcon.ico` which may not exist
   
   **Solution:** Removed the SetupIconFile directive to allow compilation without the icon
   - Users can add the icon later by recreating it
   - The application will still function without a custom installer icon

## How the Updated Process Works

### Connection String Update Flow

1. **Database setup batch file runs** - `RunDatabaseSetup.bat` creates/initializes the database
2. **AfterInstall callback triggers** - Calls `UpdateConnectionString()` procedure
3. **PowerShell updates config** - Modifies `POS.dll.config` with correct connection string
4. **Application launches** - With properly configured database connection

### Connection String Variants

The script creates different connection strings based on user choices:

**For new SQL Server Express installation (default):**
```
Server=localhost\SQLEXPRESS;Database=POS-db;User Id=sa;Password=POSSystem@2026!;
```

**For existing SQL Server (Windows Auth):**
```
Server=[ServerName];Database=POS-db;Trusted_Connection=True;
```

**For existing SQL Server (SQL Auth):**
```
Server=[ServerName];Database=POS-db;User Id=[Username];Password=[Password];
```

## Prerequisites Still Required

Before compiling, ensure these files exist:

1. **SQL Server Installer** (6 MB)
   - `Installer/Prerequisites/SQLServer2022-SSEI-Expr.exe`
   - Download from: https://www.microsoft.com/sql-server

2. **.NET Runtime Installer** (60 MB)  
   - `Installer/Prerequisites/windowsdesktop-runtime-8.0.x-win-x64.exe`
   - Download from: https://dotnet.microsoft.com

3. **Application Files**
   - Run: `dotnet publish -c Release -r win-x64` in the POS project

4. **Icon** (Optional)
   - Create `Installer/POSIcon.ico` for custom installer icon

## Compilation Steps

1. Install Inno Setup from jrsoftware.org
2. Open `POSSetup.iss` in Inno Setup Compiler
3. Press Ctrl+F9 to compile
4. Output will be in `Installer/Output/`

## Testing the Installer

Run the compiled installer with:
```powershell
.\POSSystem_Setup_1.0.0.exe
```

The installer will:
1. Check for existing SQL Server (skip if found)
2. Check for .NET 8 Runtime (skip if found)
3. Prompt for SQL Server configuration (new or existing)
4. Install prerequisites if needed
5. Create and initialize the database
6. Update the configuration file
7. Create Start Menu and Desktop shortcuts
8. Optionally launch the application

## Troubleshooting

If compilation still fails:
1. Check that prerequisites exist in the correct folders
2. Verify file paths are correct for your system
3. Ensure you're using Inno Setup 6 or later
4. Check Inno Setup compiler log for specific errors
