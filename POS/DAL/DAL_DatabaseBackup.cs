using System;
using System.Data.SqlClient;
using System.IO;

namespace POS.DAL
{
    internal class DAL_DatabaseBackup
    {
        private readonly string _connectionString;
        private readonly string _databaseName;

        public DAL_DatabaseBackup()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["POSConnectionString"].ConnectionString;
            _databaseName = GetDatabaseNameFromConnectionString(_connectionString);
        }

        /// <summary>
        /// Extracts the database name from the connection string
        /// </summary>
        private string GetDatabaseNameFromConnectionString(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            return builder.InitialCatalog;
        }

        /// <summary>
        /// Builds a connection string for the master database using the same server credentials
        /// </summary>
        private string GetMasterConnectionString()
        {
            var builder = new SqlConnectionStringBuilder(_connectionString)
            {
                InitialCatalog = "master"
            };
            return builder.ConnectionString;
        }

        /// <summary>
        /// Backs up the database to the specified file path
        /// </summary>
        /// <param name="backupFilePath">Full path where the backup file will be saved (e.g., C:\Backups\POS_20240101.bak)</param>
        /// <returns>True if backup succeeds, false otherwise</returns>
        public bool BackupDatabase(string backupFilePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(backupFilePath))
                {
                    throw new ArgumentException("Backup file path cannot be empty", nameof(backupFilePath));
                }

                // Ensure the directory exists
                string directory = Path.GetDirectoryName(backupFilePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string backupQuery = $@"
                    BACKUP DATABASE [{_databaseName}]
                    TO DISK = @backupFilePath
                    WITH FORMAT,
                         MEDIANAME = '{_databaseName}_Backup',
                         NAME = 'Full Backup of {_databaseName}';";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(backupQuery, connection))
                    {
                        command.CommandTimeout = 300; // 5 minutes timeout for large databases
                        command.Parameters.AddWithValue("@backupFilePath", backupFilePath);
                        command.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error backing up database '{_databaseName}': {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Restores the database from the specified backup file
        /// </summary>
        /// <param name="backupFilePath">Full path to the backup file (e.g., C:\Backups\POS_20240101.bak)</param>
        /// <returns>True if restore succeeds, false otherwise</returns>
        public bool RestoreDatabase(string backupFilePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(backupFilePath))
                {
                    throw new ArgumentException("Backup file path cannot be empty", nameof(backupFilePath));
                }

                if (!File.Exists(backupFilePath))
                {
                    throw new FileNotFoundException($"Backup file not found: {backupFilePath}");
                }

                string masterConnectionString = GetMasterConnectionString();

                // First, set the database to SINGLE_USER mode to close all existing connections
                string setSingleUserQuery = $@"
                    IF EXISTS (SELECT name FROM sys.databases WHERE name = N'{_databaseName}')
                    BEGIN
                        ALTER DATABASE [{_databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    END";

                using (SqlConnection masterConnection = new SqlConnection(masterConnectionString))
                {
                    masterConnection.Open();

                    // Set to SINGLE_USER mode
                    using (SqlCommand command = new SqlCommand(setSingleUserQuery, masterConnection))
                    {
                        command.CommandTimeout = 60;
                        command.ExecuteNonQuery();
                    }

                    // Perform the restore
                    string restoreQuery = $@"
                        RESTORE DATABASE [{_databaseName}]
                        FROM DISK = @backupFilePath
                        WITH REPLACE;";

                    using (SqlCommand command = new SqlCommand(restoreQuery, masterConnection))
                    {
                        command.CommandTimeout = 300; // 5 minutes timeout for large databases
                        command.Parameters.AddWithValue("@backupFilePath", backupFilePath);
                        command.ExecuteNonQuery();
                    }

                    // Set back to MULTI_USER mode
                    string setMultiUserQuery = $@"
                        ALTER DATABASE [{_databaseName}] SET MULTI_USER;";

                    using (SqlCommand command = new SqlCommand(setMultiUserQuery, masterConnection))
                    {
                        command.CommandTimeout = 60;
                        command.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // Try to set the database back to MULTI_USER mode if an error occurs
                try
                {
                    string masterConnectionString = GetMasterConnectionString();
                    using (SqlConnection masterConnection = new SqlConnection(masterConnectionString))
                    {
                        masterConnection.Open();
                        string setMultiUserQuery = $@"
                            IF EXISTS (SELECT name FROM sys.databases WHERE name = N'{_databaseName}')
                            BEGIN
                                ALTER DATABASE [{_databaseName}] SET MULTI_USER;
                            END";
                        using (SqlCommand command = new SqlCommand(setMultiUserQuery, masterConnection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch
                {
                    // Ignore errors when trying to restore MULTI_USER mode
                }

                throw new Exception($"Error restoring database '{_databaseName}' from '{backupFilePath}': {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Gets the default backup file path with timestamp
        /// </summary>
        /// <param name="backupDirectory">Directory where backup should be saved</param>
        /// <returns>Full path for the backup file</returns>
        public string GetDefaultBackupFilePath(string backupDirectory)
        {
            if (string.IsNullOrWhiteSpace(backupDirectory))
            {
                backupDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "POSBackups");
            }

            if (!Directory.Exists(backupDirectory))
            {
                Directory.CreateDirectory(backupDirectory);
            }

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string fileName = $"{_databaseName}_Backup_{timestamp}.bak";
            return Path.Combine(backupDirectory, fileName);
        }
    }
}
