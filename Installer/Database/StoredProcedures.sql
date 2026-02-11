-- =============================================================
-- POS v3 Stored Procedures
-- =============================================================

-- =============================================
-- 1. Dashboard KPI Cards
-- =============================================
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_GetDashboardKpis')
    DROP PROCEDURE usp_GetDashboardKpis;
GO

CREATE PROCEDURE usp_GetDashboardKpis
    @FromDate DATETIME2,
    @ToDate DATETIME2
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @TotalSales DECIMAL(14,2);
    DECLARE @TotalTransactions INT;
    DECLARE @TotalSalesReturn DECIMAL(14,2);
    DECLARE @InvoiceDue DECIMAL(14,2);
    DECLARE @TotalCashSales DECIMAL(14,2);
    DECLARE @LowStockCount INT;

    -- 1. Total Sales & Transactions (Active Sales)
    SELECT 
        @TotalSales = ISNULL(SUM(grand_total), 0),
        @TotalTransactions = COUNT(sale_id)
    FROM Sale
    WHERE sale_type = 'SALE' 
      AND status = 'A' 
      AND created_date BETWEEN @FromDate AND @ToDate;

    -- 2. Total Sales Return
    SELECT 
        @TotalSalesReturn = ISNULL(SUM(total_amount), 0)
    FROM SaleReturn
    WHERE status = 'A' 
      AND created_date BETWEEN @FromDate AND @ToDate;

    -- 3. Invoice Due
    SELECT 
        @InvoiceDue = ISNULL(SUM(grand_total - total_paid), 0)
    FROM Sale
    WHERE sale_type = 'SALE' 
      AND status = 'A' 
      AND payment_status IN ('PENDING', 'PARTIAL', 'CREDIT')
      AND created_date BETWEEN @FromDate AND @ToDate;

    -- 4. Total Cash Sales
    SELECT 
        @TotalCashSales = ISNULL(SUM(amount), 0)
    FROM Payment
    WHERE payment_method = 'CASH'
      AND status = 'A'
      AND created_date BETWEEN @FromDate AND @ToDate;

    -- 5. Low Stock Items
    SELECT 
        @LowStockCount = COUNT(product_id)
    FROM Product
    WHERE status = 'A' 
      AND stock_quantity <= 10;

    -- Return Single Row Result
    SELECT 
        @TotalSales AS TotalSales,
        @TotalTransactions AS TotalTransactions,
        (@TotalSales - @TotalSalesReturn) AS NetSales,
        @InvoiceDue AS InvoiceDue,
        @TotalSalesReturn AS TotalSalesReturn,
        @TotalCashSales AS TotalCashSales,
        CASE WHEN @TotalTransactions > 0 THEN @TotalSales / @TotalTransactions ELSE 0 END AS AvgSaleValue,
        @LowStockCount AS LowStockCount;
END;
GO

PRINT 'Stored procedure usp_GetDashboardKpis created.';
GO

-- =============================================
-- 2. Sales Trend Chart
-- =============================================
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_GetDashboardSalesTrend')
    DROP PROCEDURE usp_GetDashboardSalesTrend;
GO

CREATE PROCEDURE usp_GetDashboardSalesTrend
    @FromDate DATETIME2,
    @ToDate DATETIME2
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        CAST(created_date AS DATE) AS Argument,
        SUM(grand_total) AS Value
    FROM Sale
    WHERE sale_type = 'SALE' 
      AND status = 'A' 
      AND created_date BETWEEN @FromDate AND @ToDate
    GROUP BY CAST(created_date AS DATE)
    ORDER BY Argument;
END;
GO

PRINT 'Stored procedure usp_GetDashboardSalesTrend created.';
GO

-- =============================================
-- 3. Top Products Chart
-- =============================================
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_GetDashboardTopProducts')
    DROP PROCEDURE usp_GetDashboardTopProducts;
GO

CREATE PROCEDURE usp_GetDashboardTopProducts
    @FromDate DATETIME2,
    @ToDate DATETIME2
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT TOP 5
        p.product_name AS Argument,
        SUM(si.quantity) AS Value
    FROM SaleItem si
    JOIN Sale s ON si.sale_id = s.sale_id
    JOIN Product p ON si.product_id = p.product_id
    WHERE s.sale_type = 'SALE' 
      AND s.status = 'A' 
      AND s.created_date BETWEEN @FromDate AND @ToDate
    GROUP BY p.product_name
    ORDER BY Value DESC;
END;
GO

PRINT 'Stored procedure usp_GetDashboardTopProducts created.';
GO

-- =============================================
-- 4. Get Customer Transactions
-- =============================================
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_GetCustomerTransactions')
    DROP PROCEDURE usp_GetCustomerTransactions;
GO

CREATE PROCEDURE usp_GetCustomerTransactions
    @CustomerId INT,
    @StartDate DATETIME2,
    @EndDate DATETIME2,
    @StoreId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        T.TransactionDate,
        T.InvoiceNumber,
        T.TransactionType,
        T.Description,
        T.Debit,
        T.Credit,
        T.Status
    FROM (
        -- Sales (Invoices)
        SELECT 
            created_date AS TransactionDate,
            invoice_number AS InvoiceNumber,
            'INVOICE' AS TransactionType,
            'Sale #' + CAST(sale_id AS NVARCHAR(20)) AS Description,
            grand_total AS Debit,
            0 AS Credit,
            payment_status AS Status
        FROM Sale
        WHERE customer_id = @CustomerId
          AND sale_type = 'SALE'
          AND status = 'A'
          AND created_date BETWEEN @StartDate AND @EndDate
          AND (@StoreId IS NULL OR store_id = @StoreId)

        UNION ALL

        -- Payments
        SELECT 
            p.created_date AS TransactionDate,
            s.invoice_number AS InvoiceNumber,
            'PAYMENT' AS TransactionType,
            p.payment_method AS Description,
            0 AS Debit,
            p.amount AS Credit,
            'COMPLETED' AS Status
        FROM Payment p
        JOIN Sale s ON p.sale_id = s.sale_id
        WHERE s.customer_id = @CustomerId
          AND p.status = 'A'
          AND p.created_date BETWEEN @StartDate AND @EndDate
          AND (@StoreId IS NULL OR s.store_id = @StoreId)

        UNION ALL

        -- Returns
        SELECT 
            sr.created_date AS TransactionDate,
            s.invoice_number AS InvoiceNumber,
            'RETURN' AS TransactionType,
            ISNULL(sr.reason, 'Return') AS Description,
            0 AS Debit,
            sr.total_amount AS Credit,
            'COMPLETED' AS Status
        FROM SaleReturn sr
        JOIN Sale s ON sr.sale_id = s.sale_id
        WHERE s.customer_id = @CustomerId
          AND sr.status = 'A'
          AND sr.created_date BETWEEN @StartDate AND @EndDate
          AND (@StoreId IS NULL OR s.store_id = @StoreId)
    ) AS T
    ORDER BY T.TransactionDate DESC;
END;
GO

PRINT 'Stored procedure usp_GetCustomerTransactions created.';
GO

-- =============================================
-- 5. Get Customer Account Summary
-- =============================================
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_GetCustomerAccountSummary')
    DROP PROCEDURE usp_GetCustomerAccountSummary;
GO

CREATE PROCEDURE usp_GetCustomerAccountSummary
    @CustomerId INT,
    @StartDate DATETIME2,
    @EndDate DATETIME2,
    @StoreId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @OpeningBalance DECIMAL(14,2) = 0;
    DECLARE @TotalInvoice DECIMAL(14,2) = 0;
    DECLARE @TotalPaid DECIMAL(14,2) = 0;
    DECLARE @TotalReturn DECIMAL(14,2) = 0;
    DECLARE @CurrentBalance DECIMAL(14,2) = 0;
    DECLARE @CreditLimit DECIMAL(14,2) = 0;

    -- Get Current Balance & Credit Limit
    SELECT 
        @CurrentBalance = ISNULL(credit_balance, 0),
        @CreditLimit = ISNULL(credit_limit, 0)
    FROM Customer
    WHERE customer_id = @CustomerId;

    -- Total Invoiced
    SELECT @TotalInvoice = ISNULL(SUM(grand_total), 0)
    FROM Sale
    WHERE customer_id = @CustomerId
      AND sale_type = 'SALE'
      AND status = 'A'
      AND created_date BETWEEN @StartDate AND @EndDate
      AND (@StoreId IS NULL OR store_id = @StoreId);

    -- Total Paid
    SELECT @TotalPaid = ISNULL(SUM(p.amount), 0)
    FROM Payment p
    JOIN Sale s ON p.sale_id = s.sale_id
    WHERE s.customer_id = @CustomerId
      AND p.status = 'A'
      AND p.created_date BETWEEN @StartDate AND @EndDate
      AND (@StoreId IS NULL OR s.store_id = @StoreId);

    -- Total Returned
    SELECT @TotalReturn = ISNULL(SUM(sr.total_amount), 0)
    FROM SaleReturn sr
    JOIN Sale s ON sr.sale_id = s.sale_id
    WHERE s.customer_id = @CustomerId
      AND sr.status = 'A'
      AND sr.created_date BETWEEN @StartDate AND @EndDate
      AND (@StoreId IS NULL OR s.store_id = @StoreId);

    -- Back-calculate Opening Balance
    SET @OpeningBalance = @CurrentBalance - (@TotalInvoice - @TotalPaid - @TotalReturn);

    SELECT 
        @OpeningBalance AS OpeningBalance,
        @TotalInvoice AS TotalInvoice,
        @TotalPaid AS TotalPaid,
        @TotalReturn AS TotalReturn,
        @CurrentBalance AS CurrentBalance,
        @CreditLimit AS CreditLimit;
END;
GO

PRINT 'Stored procedure usp_GetCustomerAccountSummary created.';
GO

-- =============================================
-- 6. Get Supplier Transactions (Placeholder)
-- =============================================
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_GetSupplierTransactions')
    DROP PROCEDURE usp_GetSupplierTransactions;
GO

CREATE PROCEDURE usp_GetSupplierTransactions
    @SupplierId INT,
    @StartDate DATETIME2,
    @EndDate DATETIME2,
    @StoreId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        CAST(NULL AS DATETIME2) AS TransactionDate,
        CAST(NULL AS NVARCHAR(50)) AS InvoiceNumber,
        CAST(NULL AS NVARCHAR(20)) AS TransactionType,
        CAST(NULL AS NVARCHAR(100)) AS Description,
        CAST(0 AS DECIMAL(14,2)) AS Debit,
        CAST(0 AS DECIMAL(14,2)) AS Credit,
        CAST(NULL AS NVARCHAR(20)) AS Status
    WHERE 1 = 0;
END;
GO

PRINT 'Stored procedure usp_GetSupplierTransactions created.';
GO

PRINT '============================================';
PRINT 'All stored procedures created successfully!';
PRINT '============================================';
