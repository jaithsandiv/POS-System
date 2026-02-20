# Stock Management & Automatic Inventory Deduction

## Overview

The POS system now includes **automatic stock deduction** when sales are finalized. This ensures real-time inventory accuracy and prevents overselling.

---

## ?? **How Stock Deduction Works**

### **1. Stock Validation (Before Sale)**

When products are added to the cart, the system:
- ? Checks available stock in real-time
- ? Prevents adding more quantity than available
- ? Shows visual indicators (OUT OF STOCK, Low Stock)
- ? Validates stock on quantity changes in the grid

**See:** `UC_SalesTerminal.cs` ? `AddProductToSalesItems()` and `gvTransactionSum_CellValueChanged()`

---

### **2. Stock Deduction (On Sale Completion)**

When a sale is finalized, the system **automatically deducts stock** from inventory.

#### **Trigger Points:**

| Sale Type        | Stock Deducted? | When?                          |
|------------------|-----------------|--------------------------------|
| **SALE**         | ? **YES**      | When payment is completed      |
| **CREDIT_SALE**  | ? **YES**      | When sale is saved             |
| **DRAFT**        | ? **NO**       | Stock not deducted (pending)   |
| **QUOTATION**    | ? **NO**       | Stock not deducted (quote only)|

---

### **3. Implementation Details**

#### **Database Layer (DAL_SalesTerminal.cs)**

```csharp
// STOCK DEDUCTION: Only deduct stock for actual SALE transactions
// Don't deduct for DRAFT or QUOTATION
if (saleType == "SALE" || saleType == "CREDIT_SALE")
{
    decimal quantity = decimal.Parse(item["quantity"]?.ToString() ?? "0");
    int productId = Convert.ToInt32(item["product_id"]);

    string updateStockQuery = @"
        UPDATE Product
        SET stock_quantity = stock_quantity - @quantity,
            updated_by = @updated_by,
            updated_date = GETDATE()
        WHERE product_id = @product_id";

    // Execute stock update
    Connection.ExecuteNonQuery(updateStockQuery, stockParams);
}
```

**Location:** `POS\DAL\DAL_SalesTerminal.cs` ? `SaveSale()` method

---

#### **Business Logic Layer (BLL_SalesTerminal.cs)**

```csharp
// Log stock deduction for SALE types (not DRAFT or QUOTATION)
if (saleType == "SALE" || saleType == "CREDIT_SALE")
{
    foreach (DataRow item in saleItems.Rows)
    {
        int productId = Convert.ToInt32(item["product_id"]);
        string productName = item["product_name"]?.ToString() ?? "Unknown Product";
        decimal quantity = Convert.ToDecimal(item["quantity"]);

        _logManager.LogInfo(
            source: "STOCK",
            message: $"Stock deducted - Product: {productName} (ID: {productId}), Quantity: {quantity}, Reason: {saleType} (Sale ID: {resultSaleId})",
            referenceId: productId,
            userId: billerId
        );
    }
}
```

**Location:** `POS\BLL\BLL_SalesTerminal.cs` ? `SaveSale()` method

---

## ?? **Stock Restoration (Returns)**

When a sale is returned, the stock is **automatically restored** to inventory.

### **Return Process:**

1. User processes a return via `UC_Sales_Returns.cs`
2. System calls `DAL_SalesReturn.SaveSaleReturn()`
3. For each returned item:
   - Stock quantity is **added back** to Product table
   - System log entry is created

#### **Database Implementation (DAL_SalesReturn.cs)**

```csharp
// Update product stock quantity (add back returned quantity)
string updateStockQuery = @"
    UPDATE Product
    SET stock_quantity = stock_quantity + @quantity,
        updated_by = @updated_by,
        updated_date = GETDATE()
    WHERE product_id = @product_id";

Connection.ExecuteNonQuery(updateStockQuery, stockParams);
```

**Location:** `POS\DAL\DAL_SalesReturn.cs` ? `SaveSaleReturn()` method

---

#### **Business Logic Logging (BLL_SalesReturn.cs)**

```csharp
// Log stock restoration for each returned item
foreach (DataRow item in returnItems.Rows)
{
    int productId = Convert.ToInt32(item["product_id"]);
    decimal quantity = Convert.ToDecimal(item["quantity"]);
    
    _logManager.LogInfo(
        source: "STOCK",
        message: $"Stock restored - {productInfo}, Quantity: {quantity}, Reason: RETURN (Return ID: {returnId})",
        referenceId: productId,
        userId: processedBy
    );
}
```

**Location:** `POS\BLL\BLL_SalesReturn.cs` ? `SaveSaleReturn()` method

---

## ?? **Stock Tracking & Audit Trail**

### **SystemLog Integration**

Every stock change is logged in the `SystemLog` table for complete audit trail:

| Event Type          | Log Source | Log Type | Message Format                                                |
|---------------------|------------|----------|---------------------------------------------------------------|
| **Stock Deducted**  | STOCK      | INFO     | Stock deducted - Product: {Name}, Quantity: {Qty}, Reason... |
| **Stock Restored**  | STOCK      | INFO     | Stock restored - Product: {Name}, Quantity: {Qty}, Reason... |
| **Low Stock Alert** | STOCK      | WARNING  | Low stock alert - Product: {Name}, Current Stock: {Qty}      |

### **Query Stock History**

```sql
-- Get all stock changes for a specific product
SELECT 
    log_id,
    created_date,
    message,
    user_id
FROM SystemLog
WHERE log_type IN ('INFO', 'WARNING')
  AND source = 'STOCK'
  AND reference_id = @product_id  -- Product ID
ORDER BY created_date DESC;
```

---

## ?? **Stock Validation Flow**

```
???????????????????????????????????????????????????
?         STOCK VALIDATION & DEDUCTION FLOW        ?
???????????????????????????????????????????????????

1. USER ADDS PRODUCT TO CART
   ?
   ??? Check Current Stock in Products Table
   ?   ??? Get stock_quantity
   ?
   ??? Check Existing Cart Quantity
   ?   ??? Sum quantities already in salesItemsTable
   ?
   ??? Validate: (Existing + New) <= Available Stock
   ?   ??? ? PASS ? Add to cart
   ?   ??? ? FAIL ? Show error, prevent addition
   ?
   ??? Display Stock Status on Product Button
       ??? OUT OF STOCK (qty = 0) ? Gray, disabled
       ??? LOW STOCK (qty ? 10) ? Orange text
       ??? NORMAL STOCK (qty > 10) ? Standard

2. USER EDITS QUANTITY IN GRID
   ?
   ??? Re-validate Stock
   ?   ??? Check if new quantity ? available stock
   ?
   ??? If EXCEEDS stock:
   ?   ??? Show warning message
   ?   ??? Auto-adjust to available stock
   ?
   ??? If VALID:
       ??? Update subtotal and continue

3. USER COMPLETES PAYMENT (SALE TYPE)
   ?
   ??? Save Sale Record ? Sale Table
   ?
   ??? Save Sale Items ? SaleItem Table
   ?
   ??? FOR EACH Sale Item:
   ?   ?
   ?   ??? IF sale_type IN ('SALE', 'CREDIT_SALE'):
   ?   ?   ?
   ?   ?   ??? UPDATE Product
   ?   ?   ?   SET stock_quantity = stock_quantity - item.quantity
   ?   ?   ?
   ?   ?   ??? INSERT SystemLog
   ?   ?       ??? "Stock deducted - Product: {Name}, Qty: {Qty}"
   ?   ?
   ?   ??? IF sale_type IN ('DRAFT', 'QUOTATION'):
   ?       ??? Skip stock deduction (not finalized)
   ?
   ??? Generate Invoice & Reset Cart

4. USER PROCESSES RETURN
   ?
   ??? Save Return Record ? SaleReturn Table
   ?
   ??? Save Return Items ? ReturnItem Table
   ?
   ??? FOR EACH Return Item:
   ?   ?
   ?   ??? UPDATE Product
   ?   ?   SET stock_quantity = stock_quantity + item.quantity
   ?   ?
   ?   ??? INSERT SystemLog
   ?       ??? "Stock restored - Product: {Name}, Qty: {Qty}"
   ?
   ??? Process Refund
```

---

## ?? **Example Scenario**

### **Scenario: Complete Sale with Stock Deduction**

**Initial State:**
- Product: "Coca Cola 500ml"
- Product ID: 42
- Stock Quantity: 10 units

**User Actions:**

1. **Add to Cart:** 5 units
   - ? Validation: 5 ? 10 (PASS)
   - Cart: 5 units
   - Available Stock: Still 10 (not deducted yet)

2. **Try to Add More:** 6 more units
   - ? Validation: (5 + 6) = 11 > 10 (FAIL)
   - Error: "Insufficient stock. Available: 10, Already in cart: 5"

3. **Complete Sale:** SALE type with payment
   - Sale saved with 5 units
   - **Stock Deducted:** 10 - 5 = **5 units remaining**
   - SystemLog entry created

**Final State:**
- Product: "Coca Cola 500ml"
- Product ID: 42
- Stock Quantity: **5 units** ?
- SystemLog: "Stock deducted - Product: Coca Cola 500ml (ID: 42), Quantity: 5, Reason: SALE (Sale ID: 125)"

---

### **Scenario: Return with Stock Restoration**

**Initial State:**
- Product: "Coca Cola 500ml"
- Product ID: 42
- Stock Quantity: 5 units

**User Actions:**

1. **Process Return:** Return 2 units from Sale ID 125
   - Return saved
   - **Stock Restored:** 5 + 2 = **7 units**
   - SystemLog entry created

**Final State:**
- Product: "Coca Cola 500ml"
- Product ID: 42
- Stock Quantity: **7 units** ?
- SystemLog: "Stock restored - Coca Cola 500ml (ID: 42), Quantity: 2, Reason: RETURN (Return ID: 8)"

---

## ?? **Key Benefits**

1. **? Real-Time Inventory Accuracy**
   - Stock levels are always up-to-date
   - Prevents overselling

2. **? Complete Audit Trail**
   - Every stock change is logged
   - Full traceability for compliance

3. **? Automatic & Transparent**
   - No manual stock adjustments needed for sales
   - Reduces human error

4. **? Smart Validation**
   - Prevents adding products with insufficient stock
   - Visual indicators for stock status

5. **? Seamless Returns**
   - Stock automatically restored on returns
   - Maintains inventory integrity

---

## ?? **Configuration**

### **Stock Validation Toggle**

The system includes a setting to enable/disable stock validation:

```sql
-- Enable/Disable stock validation
UPDATE SystemSetting 
SET setting_value = 'True'  -- or 'False' to disable
WHERE setting_key = 'stock_check_enabled';
```

**Default:** Enabled (`True`)

---

## ?? **Important Notes**

### **1. Stock Deduction Timing**

- **SALE & CREDIT_SALE:** Stock is deducted **immediately** when the sale is saved
- **DRAFT:** Stock is **NOT deducted** (sale is pending)
- **QUOTATION:** Stock is **NOT deducted** (it's just a quote)

### **2. Partial Sales & Restocking**

If a sale is edited or cancelled:
- Currently, the system does **NOT** automatically restore stock
- Use the **Returns** module to properly restore inventory
- Future enhancement: Add sale cancellation with auto-restock

### **3. Concurrent Sales**

The current implementation uses standard SQL transactions:
- Multiple cashiers can process sales simultaneously
- Stock updates are handled by the database (row-level locking)
- Race conditions are minimal due to validation at cart level

### **4. Negative Stock Prevention**

The system prevents negative stock through:
1. **UI Validation:** Before adding to cart
2. **Grid Validation:** When editing quantities
3. **Database Constraint:** (Recommended) Add CHECK constraint:

```sql
ALTER TABLE Product 
ADD CONSTRAINT CHK_StockNonNegative 
CHECK (stock_quantity >= 0);
```

---

## ?? **Future Enhancements**

1. **Stock Reservation:**
   - Reserve stock when DRAFT is created
   - Release reservation after timeout or conversion

2. **Low Stock Notifications:**
   - Real-time alerts when stock falls below threshold
   - Email/SMS notifications to manager

3. **Batch Stock Adjustments:**
   - Manual stock adjustments (damage, theft, etc.)
   - Audit trail for manual changes

4. **Multi-Location Stock:**
   - Track stock per store/warehouse
   - Transfer stock between locations

5. **Stock Forecasting:**
   - Predict stock needs based on sales trends
   - Automatic reorder suggestions

---

## ?? **Related Documentation**

- **Stock Validation:** See `UC_SalesTerminal.cs` documentation
- **System Logging:** See `SYSTEMLOG_README.md`
- **Sales Flow:** See `SALE_TERMINAL.md`
- **Database Schema:** See `POS-TableStructures.txt`

---

## ??? **Troubleshooting**

### **Issue: Stock not deducting after sale**

**Check:**
1. Verify `sale_type` is "SALE" or "CREDIT_SALE" (not "DRAFT" or "QUOTATION")
2. Check SystemLog table for errors
3. Verify database permissions for UPDATE on Product table

**Query to verify:**
```sql
SELECT * FROM SystemLog 
WHERE source = 'STOCK' 
  AND created_date > DATEADD(hour, -1, GETDATE())
ORDER BY created_date DESC;
```

---

### **Issue: Negative stock quantities**

**Fix:**
1. Add database constraint (see section 4 above)
2. Review SystemLog for unusual stock changes
3. Run stock reconciliation report

**Reconciliation Query:**
```sql
SELECT 
    p.product_id,
    p.product_name,
    p.stock_quantity AS CurrentStock,
    ISNULL(SUM(si.quantity), 0) AS TotalSold,
    ISNULL(SUM(ri.quantity), 0) AS TotalReturned
FROM Product p
LEFT JOIN SaleItem si ON p.product_id = si.product_id
LEFT JOIN Sale s ON si.sale_id = s.sale_id AND s.sale_type = 'SALE' AND s.status = 'A'
LEFT JOIN ReturnItem ri ON p.product_id = ri.product_id AND ri.status = 'A'
WHERE p.status = 'A'
GROUP BY p.product_id, p.product_name, p.stock_quantity
HAVING p.stock_quantity < 0;  -- Find negative stock
```

---

**Version:** 1.0  
**Last Updated:** 2025-11-16  
**Status:** ? Implemented & Tested
