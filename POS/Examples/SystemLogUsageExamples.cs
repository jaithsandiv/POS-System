/*
 * SystemLog Usage Examples
 * 
 * This file demonstrates how to use the SystemLog components
 * to log various events in the POS application.
 */

using POS.BLL;
using System;
using System.Data;

namespace POS.Examples
{
    public class SystemLogUsageExamples
    {
        private readonly BLL_SystemLog _logManager;
        private readonly int _currentUserId;

        public SystemLogUsageExamples(int userId)
        {
            _logManager = new BLL_SystemLog();
            _currentUserId = userId;
        }

        #region Logging Examples

        /// <summary>
        /// Example: Log user login activity
        /// </summary>
        public void LogUserLogin(string username)
        {
            _logManager.LogAudit(
                source: "LOGIN",
                message: $"User '{username}' logged in successfully",
                referenceId: null,
                userId: _currentUserId
            );
        }

        /// <summary>
        /// Example: Log sale completion
        /// </summary>
        public void LogSaleCompleted(int saleId, decimal grandTotal)
        {
            _logManager.LogInfo(
                source: "SALE",
                message: $"Sale completed - Invoice ID: {saleId}, Total: LKR {grandTotal:N2}",
                referenceId: saleId,
                userId: _currentUserId
            );
        }

        /// <summary>
        /// Example: Log low stock warning
        /// </summary>
        public void LogLowStockWarning(int productId, string productName, decimal currentStock)
        {
            _logManager.LogWarning(
                source: "STOCK",
                message: $"Low stock alert - Product: {productName} (ID: {productId}), Current Stock: {currentStock}",
                referenceId: productId,
                userId: null
            );
        }

        /// <summary>
        /// Example: Log database error
        /// </summary>
        public void LogDatabaseError(Exception ex, string operation)
        {
            _logManager.LogError(
                source: "DATABASE",
                ex: ex,
                referenceId: null,
                userId: _currentUserId
            );
        }

        /// <summary>
        /// Example: Log payment processing
        /// </summary>
        public void LogPaymentProcessed(int saleId, string paymentMethod, decimal amount)
        {
            _logManager.LogInfo(
                source: "PAYMENT",
                message: $"Payment processed - Sale ID: {saleId}, Method: {paymentMethod}, Amount: LKR {amount:N2}",
                referenceId: saleId,
                userId: _currentUserId
            );
        }

        /// <summary>
        /// Example: Log inventory adjustment
        /// </summary>
        public void LogInventoryAdjustment(int productId, string productName, decimal oldQty, decimal newQty, string reason)
        {
            _logManager.LogAudit(
                source: "INVENTORY",
                message: $"Stock adjusted - Product: {productName}, Old Qty: {oldQty}, New Qty: {newQty}, Reason: {reason}",
                referenceId: productId,
                userId: _currentUserId
            );
        }

        /// <summary>
        /// Example: Log discount approval
        /// </summary>
        public void LogDiscountApproval(int saleId, decimal discountAmount, int approvedBy)
        {
            _logManager.LogAudit(
                source: "DISCOUNT",
                message: $"Discount approved - Sale ID: {saleId}, Amount: LKR {discountAmount:N2}, Approved By User ID: {approvedBy}",
                referenceId: saleId,
                userId: _currentUserId
            );
        }

        #endregion

        #region Retrieval Examples

        /// <summary>
        /// Example: Get all logs
        /// </summary>
        public DataTable GetAllLogs()
        {
            return _logManager.GetSystemLogs();
        }

        /// <summary>
        /// Example: Get logs by date range (last 7 days)
        /// </summary>
        public DataTable GetRecentLogs()
        {
            DateTime fromDate = DateTime.Now.AddDays(-7);
            DateTime toDate = DateTime.Now;
            
            return _logManager.GetSystemLogsByDateRange(fromDate, toDate);
        }

        /// <summary>
        /// Example: Get today's logs
        /// </summary>
        public DataTable GetTodaysLogs()
        {
            DateTime fromDate = DateTime.Today;
            DateTime toDate = DateTime.Today.AddDays(1).AddSeconds(-1);
            
            return _logManager.GetSystemLogsByDateRange(fromDate, toDate);
        }

        /// <summary>
        /// Example: Get error logs only
        /// </summary>
        public DataTable GetErrorLogs()
        {
            return _logManager.GetSystemLogsByType("ERROR");
        }

        /// <summary>
        /// Example: Get audit trail logs
        /// </summary>
        public DataTable GetAuditLogs()
        {
            return _logManager.GetSystemLogsByType("AUDIT");
        }

        /// <summary>
        /// Example: Get all sale-related logs
        /// </summary>
        public DataTable GetSaleLogs()
        {
            return _logManager.GetSystemLogsBySource("SALE");
        }

        /// <summary>
        /// Example: Search logs by keyword
        /// </summary>
        public DataTable SearchLogs(string keyword)
        {
            return _logManager.SearchSystemLogs(keyword);
        }

        /// <summary>
        /// Example: Get specific log by ID
        /// </summary>
        public DataTable GetLogById(long logId)
        {
            return _logManager.GetSystemLogById(logId);
        }

        #endregion

        #region Maintenance Examples

        /// <summary>
        /// Example: Delete logs older than 90 days (for maintenance)
        /// </summary>
        public bool CleanupOldLogs()
        {
            try
            {
                bool success = _logManager.DeleteOldLogs(90);
                
                if (success)
                {
                    _logManager.LogInfo(
                        source: "MAINTENANCE",
                        message: "Old system logs cleaned up successfully (logs older than 90 days deleted)",
                        referenceId: null,
                        userId: _currentUserId
                    );
                }
                
                return success;
            }
            catch (Exception ex)
            {
                _logManager.LogError(
                    source: "MAINTENANCE",
                    message: "Error cleaning up old logs",
                    stackTrace: ex.StackTrace,
                    referenceId: null,
                    userId: _currentUserId
                );
                return false;
            }
        }

        #endregion

        #region Integration Examples

        /// <summary>
        /// Example: Integrate logging in a try-catch block
        /// </summary>
        public bool ProcessSaleWithLogging(int saleId)
        {
            try
            {
                _logManager.LogInfo(
                    source: "SALE",
                    message: $"Processing sale ID: {saleId}",
                    referenceId: saleId,
                    userId: _currentUserId
                );

                // ... your sale processing code here ...
                
                _logManager.LogInfo(
                    source: "SALE",
                    message: $"Sale ID: {saleId} processed successfully",
                    referenceId: saleId,
                    userId: _currentUserId
                );
                
                return true;
            }
            catch (Exception ex)
            {
                _logManager.LogError(
                    source: "SALE",
                    ex: ex,
                    referenceId: saleId,
                    userId: _currentUserId
                );
                
                return false;
            }
        }

        #endregion
    }
}
