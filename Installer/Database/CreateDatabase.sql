-- =============================================================
-- POS v3 Database Creation Script
-- Run this script against the POS-db database
-- =============================================================

-- Ensure we're using the correct database
-- USE [POS-db]  -- Commented out as database is specified via sqlcmd -d parameter
-- GO

-- =============================================================
-- Check and create tables only if they don't exist
-- =============================================================

-- ---------------------------
-- 1. BUSINESS
-- ---------------------------
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Business')
BEGIN
    CREATE TABLE Business (
        business_id      INT IDENTITY(1,1) PRIMARY KEY,
        business_name    NVARCHAR(200) NOT NULL,
        logo             VARBINARY(MAX),
        status           CHAR(1) DEFAULT 'A',
        trial_start_date DATETIME2 NULL,
        trial_end_date   DATETIME2 NULL,
        is_licensed      BIT DEFAULT 0,
        created_by       INT NULL,
        created_date     DATETIME2 NULL DEFAULT GETDATE(),
        updated_by       INT NULL,
        updated_date     DATETIME2 NULL DEFAULT GETDATE()
    );
    PRINT 'Table Business created.';
END
GO

-- ---------------------------
-- 2. STORE
-- ---------------------------
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Store')
BEGIN
    CREATE TABLE Store (
        store_id       INT IDENTITY(1,1) PRIMARY KEY,
        store_name     NVARCHAR(100) NOT NULL,
        phone          NVARCHAR(20),
        email          NVARCHAR(100),
        address        NVARCHAR(500),
        city           NVARCHAR(50),
        state          NVARCHAR(50),
        country        NVARCHAR(50),
        postal_code    NVARCHAR(20),
        status         CHAR(1) DEFAULT 'A',
        created_by     INT NULL,
        created_date   DATETIME2 NULL DEFAULT GETDATE(),
        updated_by     INT NULL,
        updated_date   DATETIME2 NULL DEFAULT GETDATE()
    );
    PRINT 'Table Store created.';
    
    -- Insert default store
    INSERT INTO Store (store_name, address, city, country, status) VALUES 
    ('Main Store', '123 Main St', 'Colombo', 'Sri Lanka', 'A');
    PRINT 'Default store inserted.';
END
GO

-- ---------------------------
-- 3. ROLE
-- ---------------------------
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Role')
BEGIN
    CREATE TABLE Role (
        role_id        INT IDENTITY(1,1) PRIMARY KEY,
        role_name      NVARCHAR(100) NOT NULL,
        description    NVARCHAR(255) NULL,
        status         CHAR(1) DEFAULT 'A',
        created_by     INT NULL,
        created_date   DATETIME2 NULL DEFAULT GETDATE(),
        updated_by     INT NULL,
        updated_date   DATETIME2 NULL DEFAULT GETDATE()
    );
    PRINT 'Table Role created.';
    
    -- Insert default roles
    INSERT INTO Role (role_name, description, status, created_by) VALUES
    ('Admin', 'System administrator with full access', 'A', NULL),
    ('Cashier', 'Standard cashier for sales transactions', 'A', NULL);
    PRINT 'Default roles inserted.';
END
GO

-- ---------------------------
-- 4. ROLE PERMISSION
-- ---------------------------
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'RolePermission')
BEGIN
    CREATE TABLE RolePermission (
        role_id        INT NOT NULL,
        permission_code NVARCHAR(100) NOT NULL,
        status         CHAR(1) DEFAULT 'A',
        created_by     INT NULL,
        created_date   DATETIME2 NULL DEFAULT GETDATE(),
        updated_by     INT NULL,
        updated_date   DATETIME2 NULL DEFAULT GETDATE(),
        PRIMARY KEY (role_id, permission_code),
        FOREIGN KEY (role_id) REFERENCES Role(role_id)
    );
    PRINT 'Table RolePermission created.';
END
GO

-- ---------------------------
-- 5. USER
-- ---------------------------
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'User')
BEGIN
    CREATE TABLE [User] (
        user_id        INT IDENTITY(1,1) PRIMARY KEY,
        store_id       INT NOT NULL,
        role_id        INT NULL,
        full_name      NVARCHAR(100) NOT NULL,
        username       NVARCHAR(100) UNIQUE NOT NULL,
        email          NVARCHAR(100) UNIQUE NOT NULL,
        phone          NVARCHAR(20),
        password_hash  NVARCHAR(255) NOT NULL,
        pin_code       NVARCHAR(8),
        is_super_admin BIT DEFAULT 0,
        status         CHAR(1) DEFAULT 'A',
        created_by     INT NULL,
        created_date   DATETIME2 NULL DEFAULT GETDATE(),
        updated_by     INT NULL,
        updated_date   DATETIME2 NULL DEFAULT GETDATE(),
        FOREIGN KEY (store_id) REFERENCES Store(store_id),
        FOREIGN KEY (role_id) REFERENCES Role(role_id)
    );
    PRINT 'Table User created.';
    
    -- Insert default admin user (password: password)
    INSERT INTO [User] (store_id, role_id, full_name, username, email, phone, password_hash, pin_code, is_super_admin, status, created_by)
    VALUES (1, 1, 'ADMINISTRATOR', 'admin', 'admin@serendib.lk', '012-345-6789', 
            '$2a$11$rcV8axnF02T84Wy9wa7DG.o4.N1ayQll9RgqTr6Yt8lfjZwYoCBGO', '62940731', 1, 'A', NULL);
    PRINT 'Default admin user inserted.';
END
GO

-- ---------------------------
-- 6. SUPPLIER (must be before Brand)
-- ---------------------------
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Supplier')
BEGIN
    CREATE TABLE Supplier (
        supplier_id    INT IDENTITY(1,1) PRIMARY KEY,
        supplier_name  NVARCHAR(100) NOT NULL,
        company_name   NVARCHAR(100) NULL,
        email          NVARCHAR(100) NULL,
        phone          NVARCHAR(20) NULL,
        address        NVARCHAR(500) NULL,
        status         CHAR(1) DEFAULT 'A',
        created_by     INT NULL,
        created_date   DATETIME2 NULL DEFAULT GETDATE(),
        updated_by     INT NULL,
        updated_date   DATETIME2 NULL DEFAULT GETDATE()
    );
    PRINT 'Table Supplier created.';
END
GO

-- ---------------------------
-- 7. CATEGORY
-- ---------------------------
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Category')
BEGIN
    CREATE TABLE Category (
        category_id    INT IDENTITY(1,1) PRIMARY KEY,
        category_name  NVARCHAR(100) NOT NULL,
        status         CHAR(1) DEFAULT 'A',
        created_by     INT NULL,
        created_date   DATETIME2 NULL DEFAULT GETDATE(),
        updated_by     INT NULL,
        updated_date   DATETIME2 NULL DEFAULT GETDATE()
    );
    PRINT 'Table Category created.';
END
GO

-- ---------------------------
-- 8. BRAND
-- ---------------------------
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Brand')
BEGIN
    CREATE TABLE Brand (
        brand_id       INT IDENTITY(1,1) PRIMARY KEY,
        brand_name     NVARCHAR(100) NOT NULL,
        supplier_id    INT NULL,
        status         CHAR(1) DEFAULT 'A',
        created_by     INT NULL,
        created_date   DATETIME2 NULL DEFAULT GETDATE(),
        updated_by     INT NULL,
        updated_date   DATETIME2 NULL DEFAULT GETDATE(),
        FOREIGN KEY (supplier_id) REFERENCES Supplier(supplier_id)
    );
    PRINT 'Table Brand created.';
END
GO

-- ---------------------------
-- 9. UNIT
-- ---------------------------
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Unit')
BEGIN
    CREATE TABLE Unit (
        unit_id        INT IDENTITY(1,1) PRIMARY KEY,
        code           NVARCHAR(10) NOT NULL,
        name           NVARCHAR(50) NOT NULL,
        status         CHAR(1) DEFAULT 'A',
        created_by     INT NULL,
        created_date   DATETIME2 NULL DEFAULT GETDATE(),
        updated_by     INT NULL,
        updated_date   DATETIME2 NULL DEFAULT GETDATE()
    );
    PRINT 'Table Unit created.';
    
    -- Insert default units
    INSERT INTO Unit (code, name, status) VALUES ('PCS', 'Pieces', 'A');
    INSERT INTO Unit (code, name, status) VALUES ('KG', 'Kilograms', 'A');
    INSERT INTO Unit (code, name, status) VALUES ('L', 'Liters', 'A');
    INSERT INTO Unit (code, name, status) VALUES ('M', 'Meters', 'A');
    INSERT INTO Unit (code, name, status) VALUES ('BOX', 'Box', 'A');
    PRINT 'Default units inserted.';
END
GO

-- ---------------------------
-- 10. PRODUCT
-- ---------------------------
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Product')
BEGIN
    CREATE TABLE Product (
        product_id     INT IDENTITY(1,1) PRIMARY KEY,
        product_name   NVARCHAR(200) NOT NULL,
        product_code   NVARCHAR(50) NOT NULL UNIQUE,
        barcode        NVARCHAR(100),
        product_type   NVARCHAR(20) DEFAULT 'Standard',
        category_id    INT NULL,
        brand_id       INT NULL,
        unit_id        INT NOT NULL,
        purchase_cost  DECIMAL(14,2) NULL,
        selling_price  DECIMAL(14,2) NOT NULL,
        stock_quantity DECIMAL(14,3) DEFAULT 0,
        expiry_date    DATE NULL,
        manufacture_date DATE NULL,
        description    NVARCHAR(MAX) NULL,
        image          VARBINARY(MAX),
        status         CHAR(1) DEFAULT 'A',
        created_by     INT NULL,
        created_date   DATETIME2 NULL DEFAULT GETDATE(),
        updated_by     INT NULL,
        updated_date   DATETIME2 NULL DEFAULT GETDATE(),
        FOREIGN KEY (category_id) REFERENCES Category(category_id),
        FOREIGN KEY (brand_id) REFERENCES Brand(brand_id),
        FOREIGN KEY (unit_id) REFERENCES Unit(unit_id)
    );
    PRINT 'Table Product created.';
END
GO

-- ---------------------------
-- 11. PROMOTION
-- ---------------------------
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Promotion')
BEGIN
    CREATE TABLE Promotion (
        promotion_id     INT IDENTITY(1,1) PRIMARY KEY,
        promotion_name   NVARCHAR(100) NOT NULL,
        description      NVARCHAR(255) NULL,
        start_date       DATETIME2 NOT NULL,
        end_date         DATETIME2 NOT NULL,
        status           CHAR(1) DEFAULT 'A',
        created_by       INT NULL,
        created_date     DATETIME2 DEFAULT GETDATE(),
        updated_by       INT NULL,
        updated_date     DATETIME2 DEFAULT GETDATE()
    );
    PRINT 'Table Promotion created.';
END
GO

-- ---------------------------
-- 12. PRODUCT PROMOTION
-- ---------------------------
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ProductPromotion')
BEGIN
    CREATE TABLE ProductPromotion (
        promotion_product_id INT IDENTITY(1,1) PRIMARY KEY,
        promotion_id         INT NOT NULL,
        product_id           INT NOT NULL,
        promotion_type       NVARCHAR(20) NOT NULL,
        discount_value       DECIMAL(10,2) NOT NULL,
        status               CHAR(1) DEFAULT 'A',
        created_by           INT NULL,
        created_date         DATETIME2 DEFAULT GETDATE(),
        updated_by           INT NULL,
        updated_date         DATETIME2 DEFAULT GETDATE(),
        FOREIGN KEY (promotion_id) REFERENCES Promotion(promotion_id) ON DELETE CASCADE,
        FOREIGN KEY (product_id) REFERENCES Product(product_id),
        CONSTRAINT UQ_PromotionProduct UNIQUE (promotion_id, product_id),
        CONSTRAINT CHK_PromotionType CHECK (promotion_type IN ('PERCENTAGE', 'FIXED_AMOUNT'))
    );
    PRINT 'Table ProductPromotion created.';
END
GO

-- ---------------------------
-- 13. CUSTOMER GROUP
-- ---------------------------
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CustomerGroup')
BEGIN
    CREATE TABLE CustomerGroup (
        group_id       INT IDENTITY(1,1) PRIMARY KEY,
        group_name     NVARCHAR(100) NOT NULL,
        discount_percent DECIMAL(5,2) DEFAULT 0,
        status         CHAR(1) DEFAULT 'A',
        created_by     INT NULL,
        created_date   DATETIME2 NULL DEFAULT GETDATE(),
        updated_by     INT NULL,
        updated_date   DATETIME2 NULL DEFAULT GETDATE()
    );
    PRINT 'Table CustomerGroup created.';
END
GO

-- ---------------------------
-- 14. CUSTOMER
-- ---------------------------
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Customer')
BEGIN
    CREATE TABLE Customer (
        customer_id    INT IDENTITY(1,1) PRIMARY KEY,
        group_id       INT NULL,
        full_name      NVARCHAR(100) NOT NULL,
        company_name   NVARCHAR(100) NULL,
        email          NVARCHAR(100) NULL,
        phone          NVARCHAR(20) NULL,
        address        NVARCHAR(500) NULL,
        city           NVARCHAR(50) NULL,
        state          NVARCHAR(50) NULL,
        country        NVARCHAR(50) NULL,
        postal_code    NVARCHAR(20) NULL,
        credit_limit   DECIMAL(14,2) DEFAULT 0,
        credit_balance DECIMAL(14,2) DEFAULT 0,
        status         CHAR(1) DEFAULT 'A',
        created_by     INT NULL,
        created_date   DATETIME2 NULL DEFAULT GETDATE(),
        updated_by     INT NULL,
        updated_date   DATETIME2 NULL DEFAULT GETDATE(),
        FOREIGN KEY (group_id) REFERENCES CustomerGroup(group_id)
    );
    PRINT 'Table Customer created.';
    
    -- Insert default walk-in customer
    INSERT INTO Customer (full_name, status, created_by) VALUES ('Walk-In Customer', 'A', NULL);
    PRINT 'Default customer inserted.';
END
GO

-- ---------------------------
-- 15. SALE
-- ---------------------------
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Sale')
BEGIN
    CREATE TABLE Sale (
        sale_id        INT IDENTITY(1,1) PRIMARY KEY,
        store_id       INT NOT NULL,
        sale_type      NVARCHAR(20) NOT NULL,
        invoice_number NVARCHAR(50) NULL,
        quotation_number NVARCHAR(50) NULL,
        customer_id    INT NULL,
        biller_id      INT NOT NULL,
        total_items    INT NOT NULL,
        total_amount   DECIMAL(14,2) NOT NULL,
        discount_type  NVARCHAR(20) NOT NULL,
        discount_value DECIMAL(10,2) NOT NULL,
        grand_total    DECIMAL(14,2) NOT NULL,
        total_paid     DECIMAL(14,2) NOT NULL DEFAULT 0,
        change_due     DECIMAL(14,2) NOT NULL DEFAULT 0,
        payment_status NVARCHAR(20) DEFAULT 'PENDING',
        sale_status    NVARCHAR(20) DEFAULT 'COMPLETED',
        order_type     NVARCHAR(20) NULL,
        table_number   NVARCHAR(20) NULL,
        notes          NVARCHAR(MAX) NULL,
        status         CHAR(1) DEFAULT 'A',
        created_by     INT NULL,
        created_date   DATETIME2 NULL DEFAULT GETDATE(),
        updated_by     INT NULL,
        updated_date   DATETIME2 NULL DEFAULT GETDATE(),
        FOREIGN KEY (store_id) REFERENCES Store(store_id),
        FOREIGN KEY (customer_id) REFERENCES Customer(customer_id),
        FOREIGN KEY (biller_id) REFERENCES [User](user_id)
    );
    PRINT 'Table Sale created.';
END
GO

-- ---------------------------
-- 16. SALE ITEM
-- ---------------------------
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SaleItem')
BEGIN
    CREATE TABLE SaleItem (
        sale_item_id   INT IDENTITY(1,1) PRIMARY KEY,
        sale_id        INT NOT NULL,
        product_id     INT NOT NULL,
        product_name   NVARCHAR(200) NOT NULL,
        product_code   NVARCHAR(50) NULL,
        unit           NVARCHAR(50) NULL,
        unit_price     DECIMAL(14,2) NOT NULL,
        quantity       DECIMAL(10,3) NOT NULL,
        discount_type  NVARCHAR(20) NOT NULL,
        discount_value DECIMAL(10,2) NOT NULL,
        subtotal       DECIMAL(14,2) NOT NULL,
        status         CHAR(1) DEFAULT 'A',
        created_by     INT NULL,
        created_date   DATETIME2 NULL DEFAULT GETDATE(),
        updated_by     INT NULL,
        updated_date   DATETIME2 NULL DEFAULT GETDATE(),
        FOREIGN KEY (sale_id) REFERENCES Sale(sale_id) ON DELETE CASCADE,
        FOREIGN KEY (product_id) REFERENCES Product(product_id)
    );
    PRINT 'Table SaleItem created.';
END
GO

-- ---------------------------
-- 17. PAYMENT
-- ---------------------------
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Payment')
BEGIN
    CREATE TABLE Payment (
        payment_id             INT IDENTITY(1,1) PRIMARY KEY,
        sale_id                INT NOT NULL,
        payment_method         NVARCHAR(50) NOT NULL,
        amount                 DECIMAL(14,2) NOT NULL,
        card_last_four_digits  NVARCHAR(4) NULL,
        card_holder_name       NVARCHAR(100) NULL,
        card_transaction_number NVARCHAR(100) NULL,
        card_type              NVARCHAR(50) NULL,
        bank_reference_number  NVARCHAR(100) NULL,
        status                 CHAR(1) DEFAULT 'A',
        created_by             INT NULL,
        created_date           DATETIME2 NULL DEFAULT GETDATE(),
        updated_by             INT NULL,
        updated_date           DATETIME2 NULL DEFAULT GETDATE(),
        FOREIGN KEY (sale_id) REFERENCES Sale(sale_id) ON DELETE CASCADE,
        CONSTRAINT CHK_PaymentMethod CHECK (payment_method IN ('CASH', 'CARD', 'BANK_TRANSFER', 'CREDIT'))
    );
    PRINT 'Table Payment created.';
END
GO

-- ---------------------------
-- 18. SEQUENCES
-- ---------------------------
IF NOT EXISTS (SELECT * FROM sys.sequences WHERE name = 'InvoiceNumberSequence')
BEGIN
    CREATE SEQUENCE InvoiceNumberSequence AS INT START WITH 1 INCREMENT BY 1;
    PRINT 'Sequence InvoiceNumberSequence created.';
END
GO

IF NOT EXISTS (SELECT * FROM sys.sequences WHERE name = 'QuotationNumberSequence')
BEGIN
    CREATE SEQUENCE QuotationNumberSequence AS INT START WITH 1 INCREMENT BY 1;
    PRINT 'Sequence QuotationNumberSequence created.';
END
GO

-- ---------------------------
-- 19. SALE RETURN
-- ---------------------------
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SaleReturn')
BEGIN
    CREATE TABLE SaleReturn (
        return_id      INT IDENTITY(1,1) PRIMARY KEY,
        sale_id        INT NOT NULL,
        total_amount   DECIMAL(14,2) NOT NULL,
        reason         NVARCHAR(200) NULL,
        processed_by   INT NULL,
        status         CHAR(1) DEFAULT 'A',
        created_by     INT NULL,
        created_date   DATETIME2 NULL DEFAULT GETDATE(),
        updated_by     INT NULL,
        updated_date   DATETIME2 NULL DEFAULT GETDATE(),
        FOREIGN KEY (sale_id) REFERENCES Sale(sale_id),
        FOREIGN KEY (processed_by) REFERENCES [User](user_id)
    );
    PRINT 'Table SaleReturn created.';
END
GO

-- ---------------------------
-- 20. RETURN ITEM
-- ---------------------------
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ReturnItem')
BEGIN
    CREATE TABLE ReturnItem (
        return_item_id INT IDENTITY(1,1) PRIMARY KEY,
        return_id      INT NOT NULL,
        sale_item_id   INT NOT NULL,
        product_id     INT NOT NULL,
        quantity       DECIMAL(10,3) NOT NULL,
        refund_amount  DECIMAL(14,2) NOT NULL,
        status         CHAR(1) DEFAULT 'A',
        created_by     INT NULL,
        created_date   DATETIME2 NULL DEFAULT GETDATE(),
        updated_by     INT NULL,
        updated_date   DATETIME2 NULL DEFAULT GETDATE(),
        FOREIGN KEY (return_id) REFERENCES SaleReturn(return_id) ON DELETE CASCADE,
        FOREIGN KEY (sale_item_id) REFERENCES SaleItem(sale_item_id)
    );
    PRINT 'Table ReturnItem created.';
END
GO

-- ---------------------------
-- 21. TABLE (KOT)
-- ---------------------------
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Table')
BEGIN
    CREATE TABLE [Table] (
        table_id       INT IDENTITY(1,1) PRIMARY KEY,
        table_number   NVARCHAR(20) NOT NULL,
        capacity       INT NULL,
        status         CHAR(1) DEFAULT 'A',
        created_by     INT NULL,
        created_date   DATETIME2 NULL DEFAULT GETDATE(),
        updated_by     INT NULL,
        updated_date   DATETIME2 NULL DEFAULT GETDATE()
    );
    PRINT 'Table [Table] created.';
END
GO

-- ---------------------------
-- 22. SYSTEM SETTING
-- ---------------------------
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SystemSetting')
BEGIN
    CREATE TABLE SystemSetting (
        setting_key    NVARCHAR(100) PRIMARY KEY,
        setting_value  NVARCHAR(MAX) NULL,
        status         CHAR(1) DEFAULT 'A',
        created_by     INT NULL,
        created_date   DATETIME2 NULL DEFAULT GETDATE(),
        updated_by     INT NULL,
        updated_date   DATETIME2 NULL DEFAULT GETDATE()
    );
    PRINT 'Table SystemSetting created.';
    
    -- Insert default system settings
    INSERT INTO SystemSetting (setting_key, setting_value, status) VALUES
    ('ENABLE_THERMAL_PRINT', 'True', 'A'),
    ('ENABLE_A4_PRINT', 'False', 'A'),
    ('auto_print', 'True', 'A'),
    ('thermal_printer_name', 'Microsoft Print to PDF', 'A'),
    ('kot_enabled', 'False', 'A'),
    ('kot_printer_name', '', 'A'),
    ('invoice_footer', 'Thank You For Your Business!', 'A'),
    ('stock_check_enabled', 'True', 'A'),
    ('store_website', '', 'A');
    PRINT 'Default system settings inserted.';
END
GO

-- ---------------------------
-- 23. BARCODE PRINT
-- ---------------------------
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'BarcodePrint')
BEGIN
    CREATE TABLE BarcodePrint (
        print_id       INT IDENTITY(1,1) PRIMARY KEY,
        product_id     INT NOT NULL,
        quantity_printed INT NOT NULL,
        include_name   BIT DEFAULT 1,
        include_price  BIT DEFAULT 1,
        include_expiry BIT DEFAULT 0,
        include_manufacture BIT DEFAULT 0,
        include_promo_price BIT DEFAULT 0,
        printed_by     INT NULL,
        status         CHAR(1) DEFAULT 'A',
        created_by     INT NULL,
        created_date   DATETIME2 NULL DEFAULT GETDATE(),
        updated_by     INT NULL,
        updated_date   DATETIME2 NULL DEFAULT GETDATE(),
        FOREIGN KEY (product_id) REFERENCES Product(product_id),
        FOREIGN KEY (printed_by) REFERENCES [User](user_id)
    );
    PRINT 'Table BarcodePrint created.';
END
GO

-- ---------------------------
-- 24. SYSTEM LOG
-- ---------------------------
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SystemLog')
BEGIN
    CREATE TABLE SystemLog (
        log_id         BIGINT IDENTITY(1,1) PRIMARY KEY,
        log_type       NVARCHAR(20) NOT NULL,
        source         NVARCHAR(100) NOT NULL,
        reference_id   INT NULL,
        message        NVARCHAR(MAX) NOT NULL,
        stack_trace    NVARCHAR(MAX) NULL,
        user_id        INT NULL,
        created_date   DATETIME2 DEFAULT GETDATE(),
        FOREIGN KEY (user_id) REFERENCES [User](user_id)
    );
    PRINT 'Table SystemLog created.';
END
GO

PRINT '============================================';
PRINT 'Database schema creation completed!';
PRINT '============================================';
