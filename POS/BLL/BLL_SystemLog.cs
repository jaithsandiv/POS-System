using POS.DAL;
using System;
using System.Data;

namespace POS.BLL
{
    internal class BLL_SystemLog
    {
        private readonly DAL_SystemLog _dalSystemLog = new DAL_SystemLog();

        /// <summary>
        /// Gets all system logs
        /// </summary>
        public DataTable GetSystemLogs()
        {
            return _dalSystemLog.GetSystemLogs();
        }

        /// <summary>
        /// Gets system logs filtered by date range
        /// </summary>
        public DataTable GetSystemLogsByDateRange(DateTime fromDate, DateTime toDate)
        {
            if (fromDate > toDate)
                throw new ArgumentException("From date cannot be greater than to date.");

            return _dalSystemLog.GetSystemLogsByDateRange(fromDate, toDate);
        }

        /// <summary>
        /// Gets a specific log entry by ID
        /// </summary>
        public DataTable GetSystemLogById(long logId)
        {
            if (logId <= 0)
                throw new ArgumentException("Log ID must be greater than zero.");

            return _dalSystemLog.GetSystemLogById(logId);
        }

        /// <summary>
        /// Logs an informational message
        /// </summary>
        public long LogInfo(string source, string message, int? referenceId = null, int? userId = null)
        {
            ValidateLogParameters(source, message);
            return _dalSystemLog.InsertSystemLog("INFO", source, referenceId, message, null, userId);
        }

        /// <summary>
        /// Logs a warning message
        /// </summary>
        public long LogWarning(string source, string message, int? referenceId = null, int? userId = null)
        {
            ValidateLogParameters(source, message);
            return _dalSystemLog.InsertSystemLog("WARNING", source, referenceId, message, null, userId);
        }

        /// <summary>
        /// Logs an error with optional stack trace
        /// </summary>
        public long LogError(string source, string message, string stackTrace = null, int? referenceId = null, int? userId = null)
        {
            ValidateLogParameters(source, message);
            return _dalSystemLog.InsertSystemLog("ERROR", source, referenceId, message, stackTrace, userId);
        }

        /// <summary>
        /// Logs an error from an exception
        /// </summary>
        public long LogError(string source, Exception ex, int? referenceId = null, int? userId = null)
        {
            if (ex == null)
                throw new ArgumentNullException(nameof(ex));

            ValidateLogParameters(source, ex.Message);
            return _dalSystemLog.InsertSystemLog("ERROR", source, referenceId, ex.Message, ex.StackTrace, userId);
        }

        /// <summary>
        /// Logs an audit trail entry
        /// </summary>
        public long LogAudit(string source, string message, int? referenceId = null, int? userId = null)
        {
            ValidateLogParameters(source, message);
            return _dalSystemLog.InsertSystemLog("AUDIT", source, referenceId, message, null, userId);
        }

        /// <summary>
        /// Inserts a custom log entry
        /// </summary>
        public long InsertSystemLog(string logType, string source, int? referenceId, string message, string stackTrace, int? userId)
        {
            ValidateLogType(logType);
            ValidateLogParameters(source, message);

            return _dalSystemLog.InsertSystemLog(logType, source, referenceId, message, stackTrace, userId);
        }

        /// <summary>
        /// Searches system logs by keyword
        /// </summary>
        public DataTable SearchSystemLogs(string keyword)
        {
            return _dalSystemLog.SearchSystemLogs(keyword);
        }

        /// <summary>
        /// Filters logs by type
        /// </summary>
        public DataTable GetSystemLogsByType(string logType)
        {
            ValidateLogType(logType);
            return _dalSystemLog.GetSystemLogsByType(logType);
        }

        /// <summary>
        /// Filters logs by source
        /// </summary>
        public DataTable GetSystemLogsBySource(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                throw new ArgumentException("Source cannot be empty.");

            return _dalSystemLog.GetSystemLogsBySource(source);
        }

        /// <summary>
        /// Deletes logs older than specified days
        /// </summary>
        public bool DeleteOldLogs(int daysOld)
        {
            if (daysOld < 1)
                throw new ArgumentException("Days old must be at least 1.");

            return _dalSystemLog.DeleteOldLogs(daysOld);
        }

        #region Validation Helpers

        private void ValidateLogType(string logType)
        {
            if (string.IsNullOrWhiteSpace(logType))
                throw new ArgumentException("Log type cannot be empty.");

            string[] validTypes = { "INFO", "WARNING", "ERROR", "AUDIT" };
            if (Array.IndexOf(validTypes, logType.ToUpper()) == -1)
                throw new ArgumentException($"Invalid log type. Must be one of: {string.Join(", ", validTypes)}");
        }

        private void ValidateLogParameters(string source, string message)
        {
            if (string.IsNullOrWhiteSpace(source))
                throw new ArgumentException("Source cannot be empty.");

            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Message cannot be empty.");
        }

        #endregion
    }
}
