# POS System Installer Build Guide

## Prerequisites

Before compiling the installer, you need to gather the following prerequisites:

### 1. Download SQL Server 2022 Express
- Download from: https://www.microsoft.com/en-us/sql-server/sql-server-downloads
- Select "Express" edition
- Download the web installer: `SQLServer2022-SSEI-Expr.exe`
- Place it in: `Installer\Prerequisites\`

### 2. Download .NET 8.0 Desktop Runtime
- Download from: https://dotnet.microsoft.com/en-us/download/dotnet/8.0
- Select "Windows Desktop Runtime" for x64
- Download: `windowsdesktop-runtime-8.0.x-win-x64.exe`
- Place it in: `Installer\Prerequisites\`

### 3. Application Icon (Optional)
- Create or obtain a `.ico` file for the application
- Name it `POSIcon.ico`
- Place it in: `Installer\`

## Folder Structure

Your installer folder should look like this:

```
Installer/
├── POSSetup.iss              # Inno Setup script
├── License.txt               # License agreement
├── ReadMe.txt                # Installation notes
├── POSIcon.ico               # Application icon (create this)
├── Config/
│   └── App.config.template   # Connection string template
├── Database/
│   ├── CreateDatabase.sql    # Database schema script
│   ├── StoredProcedures.sql  # Stored procedures script
│   └── RunDatabaseSetup.bat  # Database setup batch file
├── Prerequisites/
│   ├── SQLServer2022-SSEI-Expr.exe     # SQL Server installer
│   └── windowsdesktop-runtime-8.0.x-win-x64.exe  # .NET runtime
└── Output/                   # Compiled installer output folder
```

## Building the Installer

### Step 1: Install Inno Setup
1. Download Inno Setup from: https://jrsoftware.org/isdl.php
2. Install with default options

### Step 2: Build Your Application
```powershell
cd POS
dotnet publish -c Release -r win-x64 --self-contained false
```

Or for a self-contained deployment (no .NET runtime required):
```powershell
dotnet publish -c Release -r win-x64 --self-contained true
```

### Step 3: Update Paths in POSSetup.iss
1. Open `POSSetup.iss` in Inno Setup Compiler
2. Verify the paths in the `[Files]` section match your build output:
   ```ini
   Source: "..\POS\bin\Release\net8.0-windows\win-x64\publish\*"; DestDir: "{app}"; ...
   ```

### Step 4: Compile the Installer
1. Open `POSSetup.iss` in Inno Setup Compiler
2. Press `Ctrl+F9` or click `Build > Compile`
3. The installer will be created in `Installer\Output\`

## Configuration Options

### Change SQL Server Password
In `POSSetup.iss`, modify:
```ini
#define SQLPassword "POSSystem@2026!"
```

### Change Database Name
In `POSSetup.iss`, modify:
```ini
#define SQLDatabaseName "POS-db"
```

### Change Instance Name
In `POSSetup.iss`, modify:
```ini
#define SQLInstanceName "SQLEXPRESS"
```

## Testing the Installer

### Test on a Clean VM
1. Create a Windows 10/11 virtual machine
2. Run the installer
3. Verify SQL Server Express installs (if needed)
4. Verify database is created
5. Launch POS System and login with admin/password

### Test with Existing SQL Server
1. On a machine with SQL Server already installed
2. Run the installer
3. Enter your existing server details when prompted
4. Verify the database is created on the existing server

## Troubleshooting

### SQL Server Installation Fails
- Ensure Windows Updates are current
- Run as Administrator
- Check Windows Event Viewer for errors
- Manually run: `SQLServer2022-SSEI-Expr.exe /QS /ACTION=Install /FEATURES=SQLEngine /INSTANCENAME=SQLEXPRESS`

### Database Creation Fails
- Check if sqlcmd is available in PATH
- Manually test: `sqlcmd -S localhost\SQLEXPRESS -U sa -P YourPassword -Q "SELECT 1"`
- Review the batch file output in installer log

### Connection String Not Updated
- Check the App.config.template has the `{{CONNECTION_STRING}}` placeholder
- Verify the config file path in UpdateConnectionString procedure

## Silent Installation

For automated deployment, use:
```powershell
POSSystem_Setup_1.0.0.exe /VERYSILENT /SUPPRESSMSGBOXES /LOG="install.log"
```

## Uninstallation

The installer creates a standard Windows uninstaller. The uninstaller will:
1. Back up the database (optional)
2. Remove application files
3. Remove Start Menu and Desktop shortcuts
4. Note: SQL Server Express will NOT be uninstalled

## Security Notes

⚠️ **Important Security Considerations:**

1. Change the default SA password in production
2. Change the default admin credentials after first login
3. Consider using Windows Authentication instead of SQL Authentication
4. Review and harden SQL Server security settings

## Support

For issues with the installer:
1. Check the installation log in `%TEMP%\Setup Log*.txt`
2. Review SQL Server error logs
3. Contact support@serendib.lk
