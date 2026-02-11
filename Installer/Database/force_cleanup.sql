-- Enable xp_cmdshell to allow file system operations
EXEC sp_configure 'show advanced options', 1;
RECONFIGURE;
EXEC sp_configure 'xp_cmdshell', 1;
RECONFIGURE;
GO

-- Force delete the orphaned files
EXEC xp_cmdshell 'del /F /Q "C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\POS-db.mdf"';
EXEC xp_cmdshell 'del /F /Q "C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\POS-db_log.ldf"';
GO

-- Disable xp_cmdshell for security
EXEC sp_configure 'xp_cmdshell', 0;
RECONFIGURE;
EXEC sp_configure 'show advanced options', 0;
RECONFIGURE;
GO