# Prerequisites Folder

Place the following files in this folder before compiling the installer:

## Required Files

### 1. SQL Server 2022 Express
- **Filename:** `SQLServer2022-SSEI-Expr.exe`
- **Download:** https://www.microsoft.com/en-us/sql-server/sql-server-downloads
- **Size:** ~6 MB (web installer that downloads full setup)

### 2. .NET 8.0 Desktop Runtime (if not using self-contained deployment)
- **Filename:** `windowsdesktop-runtime-8.0.x-win-x64.exe`
- **Download:** https://dotnet.microsoft.com/en-us/download/dotnet/8.0
- **Size:** ~60 MB

## Optional (for Self-Contained Deployment)

If you publish with `--self-contained true`, the .NET runtime installer is not required.

## Notes

- The installer script checks if these components are already installed
- If already installed, the prerequisite installers will be skipped
- The SQL Server Express installer will only download required components
