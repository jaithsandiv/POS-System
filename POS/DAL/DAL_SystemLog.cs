using System;
using System.Data;
using System.Data.SqlClient;

namespace POS.DAL
{
    internal class DAL_SystemLog
    {
        public DAL_SystemLog()
        {
        }

        /// <summary>
        /// Gets all system logs with user information
        /// </summary>
        public DataTable GetSystemLogs()
        {
            string query = @"
                SELECT 
                    sl.log_id,
                    sl.log_type,
                    sl.source,
                    sl.reference_id,
                    sl.message,
                    sl.stack_trace,
                    sl.user_id,
                    u.username,
                    CONVERT(varchar, sl.created_date, 120) AS created_date
                FROM SystemLog sl
                LEFT JOIN [User] u ON sl.user_id = u.user_id
                ORDER BY sl.created_date DESC";

            return Connection.ExecuteQuery(query);
        }

        /// <summary>
        /// Gets system logs filtered by date range
        /// </summary>
        public DataTable GetSystemLogsByDateRange(DateTime fromDate, DateTime toDate)
        {
            string query = @"
                SELECT 
                    sl.log_id,
                    sl.log_type,
                    sl.source,
                    sl.reference_id,
                    sl.message,
                    sl.stack_trace,
                    sl.user_id,
                    u.username,
                    CONVERT(varchar, sl.created_date, 120) AS created_date
                FROM SystemLog sl
                LEFT JOIN [User] u ON sl.user_id = u.user_id
                WHERE sl.created_date BETWEEN @from_date AND @to_date
                ORDER BY sl.created_date DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@from_date", fromDate),
                new SqlParameter("@to_date", toDate)
            };

            return Connection.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Gets a specific log entry by ID
        /// </summary>
        public DataTable GetSystemLogById(long logId)
        {
            string query = @"
                SELECT 
                    sl.log_id,
                    sl.log_type,
                    sl.source,
                    sl.reference_id,
                    sl.message,
                    sl.stack_trace,
                    sl.user_id,
                    u.username,
                    CONVERT(varchar, sl.created_date, 120) AS created_date
                FROM SystemLog sl
                LEFT JOIN [User] u ON sl.user_id = u.user_id
                WHERE sl.log_id = @log_id";

            SqlParameter[] parameters = { new SqlParameter("@log_id", logId) };
            return Connection.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Inserts a new system log entry
        /// </summary>
        public long InsertSystemLog(string logType, string source, int? referenceId, string message, string stackTrace, int? userId)
        {
            try
            {
                string query = @"
                    INSERT INTO SystemLog (log_type, source, reference_id, message, stack_trace, user_id, created_date)
                    VALUES (@log_type, @source, @reference_id, @message, @stack_trace, @user_id, GETDATE());
                    SELECT CAST(SCOPE_IDENTITY() AS BIGINT);";

                SqlParameter[] parameters = {
                    new SqlParameter("@log_type", logType),
                    new SqlParameter("@source", source),
                    new SqlParameter("@reference_id", referenceId.HasValue ? (object)referenceId.Value : DBNull.Value),
                    new SqlParameter("@message", message),
                    new SqlParameter("@stack_trace", stackTrace ?? (object)DBNull.Value),
                    new SqlParameter("@user_id", userId.HasValue ? (object)userId.Value : DBNull.Value)
                };

                object result = Connection.ExecuteScalar(query, parameters);
                return result != null ? Convert.ToInt64(result) : 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inserting system log: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Searches system logs by keyword
        /// </summary>
        public DataTable SearchSystemLogs(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return GetSystemLogs();
            }

            string query = @"
                SELECT 
                    sl.log_id,
                    sl.log_type,
                    sl.source,
                    sl.reference_id,
                    sl.message,
                    sl.stack_trace,
                    sl.user_id,
                    u.username,
                    CONVERT(varchar, sl.created_date, 120) AS created_date
                FROM SystemLog sl
                LEFT JOIN [User] u ON sl.user_id = u.user_id
                WHERE 
                    sl.log_type LIKE @keyword 
                    OR sl.source LIKE @keyword
                    OR sl.message LIKE @keyword
                    OR u.username LIKE @keyword
                    OR CAST(sl.reference_id AS NVARCHAR) LIKE @keyword
                ORDER BY sl.created_date DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@keyword", "%" + keyword + "%")
            };

            return Connection.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Filters logs by type
        /// </summary>
        public DataTable GetSystemLogsByType(string logType)
        {
            string query = @"
                SELECT 
                    sl.log_id,
                    sl.log_type,
                    sl.source,
                    sl.reference_id,
                    sl.message,
                    sl.stack_trace,
                    sl.user_id,
                    u.username,
                    CONVERT(varchar, sl.created_date, 120) AS created_date
                FROM SystemLog sl
                LEFT JOIN [User] u ON sl.user_id = u.user_id
                WHERE sl.log_type = @log_type
                ORDER BY sl.created_date DESC";

            SqlParameter[] parameters = { new SqlParameter("@log_type", logType) };
            return Connection.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Filters logs by source
        /// </summary>
        public DataTable GetSystemLogsBySource(string source)
        {
            string query = @"
                SELECT 
                    sl.log_id,
                    sl.log_type,
                    sl.source,
                    sl.reference_id,
                    sl.message,
                    sl.stack_trace,
                    sl.user_id,
                    u.username,
                    CONVERT(varchar, sl.created_date, 120) AS created_date
                FROM SystemLog sl
                LEFT JOIN [User] u ON sl.user_id = u.user_id
                WHERE sl.source = @source
                ORDER BY sl.created_date DESC";

            SqlParameter[] parameters = { new SqlParameter("@source", source) };
            return Connection.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Deletes logs older than specified days (for maintenance)
        /// </summary>
        public bool DeleteOldLogs(int daysOld)
        {
            try
            {
                string query = @"
                    DELETE FROM SystemLog
                    WHERE created_date < DATEADD(DAY, -@days_old, GETDATE())";

                SqlParameter[] parameters = {
                    new SqlParameter("@days_old", daysOld)
                };

                int rowsAffected = Connection.ExecuteNonQuery(query, parameters);
                return rowsAffected >= 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting old logs: {ex.Message}", ex);
            }
        }
    }
}
