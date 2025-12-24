# SystemLog Implementation Guide

## Overview
The SystemLog module provides comprehensive logging capabilities for the POS system, allowing you to track user actions, system events, errors, and audit trails.

## Components Created

### 1. Database Table
- **SystemLog** table (already exists in your database schema)
  - Stores log entries with type, source, message, stack trace, and user information
  - Supports log types: INFO, WARNING, ERROR, AUDIT
  - Includes reference_id to link logs to business entities (sales, products, etc.)

### 2. Data Access Layer (DAL)
- **File**: `POS\DAL\DAL_SystemLog.cs`
- **Purpose**: Handles all database operations for system logs
- **Key Methods**:
  - `GetSystemLogs()` - Retrieve all logs
  - `GetSystemLogsByDateRange(fromDate, toDate)` - Get logs within date range
  - `GetSystemLogById(logId)` - Get specific log entry
  - `InsertSystemLog(...)` - Create new log entry
  - `SearchSystemLogs(keyword)` - Search logs by keyword
  - `GetSystemLogsByType(logType)` - Filter by log type
  - `GetSystemLogsBySource(source)` - Filter by source
  - `DeleteOldLogs(daysOld)` - Maintenance method to clean up old logs

### 3. Business Logic Layer (BLL)
- **File**: `POS\BLL\BLL_SystemLog.cs`
- **Purpose**: Provides business logic and validation for logging
- **Key Methods**:
  - `LogInfo(source, message, referenceId, userId)` - Log informational messages
  - `LogWarning(source, message, referenceId, userId)` - Log warnings
  - `LogError(source, message, stackTrace, referenceId, userId)` - Log errors
  - `LogError(source, ex, referenceId, userId)` - Log exceptions
  - `LogAudit(source, message, referenceId, userId)` - Log audit trail
  - Plus all DAL retrieval methods with added validation

### 4. DataSource Schema
- **File**: `POS\DAL\DataSource\DAL_DS_SystemLog.xsd`
- **Purpose**: Defines typed dataset structure (optional, not required for current implementation)

## Log Types

| Type | Purpose | Example Usage |
|------|---------|---------------|
| **INFO** | General information | Sale completed, payment processed |
| **WARNING** | Non-critical issues | Low stock alerts, slow operations |
| **ERROR** | System errors | Database errors, validation failures |
| **AUDIT** | Security/compliance | User login, discount approvals, inventory changes |

## Common Sources

| Source | Description |
|--------|-------------|
| LOGIN | User authentication activities |
| SALE | Sales transactions |
| PAYMENT | Payment processing |
| STOCK/INVENTORY | Stock management |
| DISCOUNT | Discount operations |
| RETURN | Sale returns |
| USER | User management |
| PRODUCT | Product management |
| MAINTENANCE | System maintenance tasks |
| DATABASE | Database operations |

## Usage Examples

### Basic Logging

```csharp
using POS.BLL;

// Initialize
BLL_SystemLog logManager = new BLL_SystemLog();
int currentUserId = 1; // Get from current session

// Log info message
logManager.LogInfo(
    source: "SALE",
    message: "Sale completed successfully",
    referenceId: saleId,
    userId: currentUserId
);

// Log warning
logManager.LogWarning(
    source: "STOCK",
    message: "Low stock detected for Product ID: 123",
    referenceId: 123,
    userId: null
);

// Log error from exception
try
{
    // Some operation
}
catch (Exception ex)
{
    logManager.LogError(
        source: "DATABASE",
        ex: ex,
        referenceId: null,
        userId: currentUserId
    );
}

// Log audit trail
logManager.LogAudit(
    source: "DISCOUNT",
    message: $"Discount approved by {approverName}",
    referenceId: saleId,
    userId: currentUserId
);
```

### Retrieving Logs

```csharp
// Get all logs
DataTable allLogs = logManager.GetSystemLogs();

// Get logs by date range
DataTable recentLogs = logManager.GetSystemLogsByDateRange(
    DateTime.Now.AddDays(-7),
    DateTime.Now
);

// Get error logs only
DataTable errorLogs = logManager.GetSystemLogsByType("ERROR");

// Get all sale-related logs
DataTable saleLogs = logManager.GetSystemLogsBySource("SALE");

// Search logs
DataTable searchResults = logManager.SearchSystemLogs("payment failed");
```

### Log Maintenance

```csharp
// Delete logs older than 90 days
bool success = logManager.DeleteOldLogs(90);
```

## Integration Points

### 1. User Login (PAL_Login.cs or similar)
```csharp
// On successful login
_logManager.LogAudit("LOGIN", $"User '{username}' logged in successfully", userId: userId);

// On failed login
_logManager.LogWarning("LOGIN", $"Failed login attempt for username: {username}");
```

### 2. Sales Processing (BLL_Sale.cs)
```csharp
// Log sale completion
_logManager.LogInfo("SALE", $"Sale completed - Invoice: {invoiceNumber}, Total: {grandTotal:C}", saleId, userId);
```

### 3. Payment Processing (BLL_Payment.cs)
```csharp
// Log payment
_logManager.LogInfo("PAYMENT", $"Payment processed - Method: {method}, Amount: {amount:C}", saleId, userId);
```

### 4. Error Handling (Global Exception Handler)
```csharp
catch (Exception ex)
{
    _logManager.LogError("SYSTEM", ex, userId: currentUserId);
    // Show error message to user
}
```

## Best Practices

1. **Use Appropriate Log Types**
   - INFO for normal operations
   - WARNING for potential issues
   - ERROR for failures
   - AUDIT for security-sensitive actions

2. **Include Context**
   - Always include source to categorize logs
   - Use reference_id to link logs to business entities
   - Include user_id for accountability

3. **Write Clear Messages**
   - Be descriptive but concise
   - Include relevant details (IDs, amounts, names)
   - Use consistent formatting

4. **Regular Maintenance**
   - Schedule periodic cleanup of old logs
   - Archive important logs before deletion
   - Monitor log growth

5. **Security Considerations**
   - Never log sensitive data (passwords, credit card numbers)
   - For card payments, only last 4 digits are logged (already implemented)
   - Sanitize user input before logging

## Performance Considerations

- Logging is asynchronous-friendly
- Index on `created_date` column for better query performance
- Index on `source` and `log_type` if filtering heavily
- Regular cleanup prevents table bloat
- Consider partitioning for very high-volume systems

## Troubleshooting

**Q: Logs not appearing in the database?**
- Check database connection
- Verify SystemLog table exists
- Check user permissions

**Q: Performance issues with log queries?**
- Add date range filters
- Implement pagination for large result sets
- Consider archiving old logs

**Q: Running out of disk space?**
- Implement regular log cleanup (90+ days)
- Archive critical logs to separate storage
- Monitor log volume

## Next Steps

1. Integrate logging into existing modules (Sales, Inventory, Users)
2. Create log viewer UI in `UC_ActivityLog_Report.cs`
3. Implement automated log cleanup task
4. Add log export functionality (CSV, PDF)
5. Consider log aggregation/dashboard for monitoring

## Support

For questions or issues:
- Review the usage examples in `POS\Examples\SystemLogUsageExamples.cs`
- Check the database schema in `POS-TableStructures.txt`
- Examine existing implementations in DAL and BLL layers
