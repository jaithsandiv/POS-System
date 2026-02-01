using System;
using System.IO;
using POS.Properties;

namespace POS.BLL
{
    /// <summary>
    /// Service for managing automatic daily database backups
    /// </summary>
    internal class BLL_AutoBackup
    {
        private const string BACKUP_DIRECTORY = @"C:\POS_Backups";
        private readonly DAL.DAL_DatabaseBackup _dalBackup;

        public BLL_AutoBackup()
        {
            _dalBackup = new DAL.DAL_DatabaseBackup();
        }

        /// <summary>
        /// Checks if a backup is needed today and performs it if necessary
        /// </summary>
        /// <returns>True if backup was performed, false if not needed or failed</returns>
        public bool PerformDailyBackupIfNeeded()
        {
            try
            {
                // Check if backup has already been done today
                if (IsBackupDoneToday())
                {
                    return false; // Backup already done today
                }

                // Ensure backup directory exists
                if (!Directory.Exists(BACKUP_DIRECTORY))
                {
                    Directory.CreateDirectory(BACKUP_DIRECTORY);
                }

                // Generate timestamped filename
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string backupFileName = $"POS_AutoBackup_{timestamp}.bak";
                string backupFilePath = Path.Combine(BACKUP_DIRECTORY, backupFileName);

                // Perform backup
                bool success = _dalBackup.BackupDatabase(backupFilePath);

                if (success)
                {
                    // Update last backup date
                    UpdateLastBackupDate();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                // Log error but don't throw - we don't want to prevent app startup
                System.Diagnostics.Debug.WriteLine($"Auto-backup failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Checks if a backup has been performed today
        /// </summary>
        private bool IsBackupDoneToday()
        {
            try
            {
                string lastBackupDateStr = Settings.Default.LastBackupDate;

                if (string.IsNullOrWhiteSpace(lastBackupDateStr))
                {
                    return false; // Never backed up
                }

                if (DateTime.TryParse(lastBackupDateStr, out DateTime lastBackupDate))
                {
                    // Check if last backup was today
                    return lastBackupDate.Date == DateTime.Now.Date;
                }

                return false;
            }
            catch
            {
                return false; // Assume not backed up if there's an error
            }
        }

        /// <summary>
        /// Updates the last backup date setting to today
        /// </summary>
        private void UpdateLastBackupDate()
        {
            try
            {
                Settings.Default.LastBackupDate = DateTime.Now.ToString("yyyy-MM-dd");
                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to update LastBackupDate: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the last backup date
        /// </summary>
        public DateTime? GetLastBackupDate()
        {
            try
            {
                string lastBackupDateStr = Settings.Default.LastBackupDate;

                if (string.IsNullOrWhiteSpace(lastBackupDateStr))
                {
                    return null;
                }

                if (DateTime.TryParse(lastBackupDateStr, out DateTime lastBackupDate))
                {
                    return lastBackupDate;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the backup directory path
        /// </summary>
        public string GetBackupDirectory()
        {
            return BACKUP_DIRECTORY;
        }

        /// <summary>
        /// Cleans up old backup files (keeps only the last N backups)
        /// </summary>
        /// <param name="keepCount">Number of recent backups to keep</param>
        public void CleanupOldBackups(int keepCount = 30)
        {
            try
            {
                if (!Directory.Exists(BACKUP_DIRECTORY))
                {
                    return;
                }

                var backupFiles = Directory.GetFiles(BACKUP_DIRECTORY, "POS_AutoBackup_*.bak");

                if (backupFiles.Length <= keepCount)
                {
                    return; // Nothing to clean up
                }

                // Sort by creation time (oldest first)
                Array.Sort(backupFiles, (a, b) => 
                    File.GetCreationTime(a).CompareTo(File.GetCreationTime(b)));

                // Delete oldest files
                int filesToDelete = backupFiles.Length - keepCount;
                for (int i = 0; i < filesToDelete; i++)
                {
                    try
                    {
                        File.Delete(backupFiles[i]);
                    }
                    catch
                    {
                        // Continue even if one file fails to delete
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to cleanup old backups: {ex.Message}");
            }
        }
    }
}
