GitHub Copilot: # High-Level Sales Terminal Process Flow Explanation

Let me explain the complete workflow of the sales terminal system, covering all sale types and their interactions.

---

## 🎯 **Core Sales Terminal Workflow**

### **1. Initial State (btnCancel_Click)**

```
┌─────────────────────────────────────────┐
│  SALES TERMINAL INITIALIZATION          │
├─────────────────────────────────────────┤
│ • Load Products/Categories/Brands       │
│ • Create NEW saleTable row              │
│ • Create EMPTY salesItemsTable          │
│ • Set customer_id = 1 (Walk-In)         │
│ • Set biller_id = logged-in user        │
│ • Set store_id = current store          │
│ • Reset all UI fields to defaults       │
│ • total_amount = 0.00                   │
│ • grand_total = 0.00                    │
│ • discount_value = 0.00                 │
└─────────────────────────────────────────┘
```

**Key Points:**

- Everything starts fresh
- No database interaction yet (all in-memory DataTables)
- Walk-In Customer is default
- Cart is empty

---

## 🛒 **2. Building the Cart**

### **Product Selection Flow:**

```
User Action                    System Response
─────────────────────────────────────────────────
Click Product Button     →    AddProductToSalesItems()
  or Scan Barcode             ├─ Check if product already in cart
                              │  ├─ YES: Increment quantity
                              │  └─ NO: Add new row to salesItemsTable
                              │
                              ├─ Calculate item subtotal with discount
                              │  └─ unit_price × quantity - discount
                              │
                              ├─ Update saleTable:
                              │  ├─ total_amount (sum of all subtotals)
                              │  └─ total_items (sum of all quantities)
                              │
                              ├─ Call CalculateAndUpdateGrandTotal()
                              │  ├─ Get total_amount
                              │  ├─ Apply sale-level discount
                              │  └─ Update grand_total
                              │
                              └─ Refresh grid display
```

**Key Calculations:**

- **Item Subtotal** = (unit_price - item_discount) × quantity
- **Total Amount** = Sum of all item subtotals
- **Grand Total** = total_amount - sale_discount

---

## 💰 **3. Discount System**

### **Two-Level Discount Architecture:**

```
┌──────────────────────────────────────────────────┐
│           ITEM-LEVEL DISCOUNT                    │
├──────────────────────────────────────────────────┤
│ Location: salesItemsTable (per row)              │
│ Fields: discount_type, discount_value            │
│ Types: PERCENTAGE or FIXED_AMOUNT                │
│ Source: Product Promotion or Manual Entry        │
│ Control: Requires Staff PIN to edit              │
│ Effect: Applied before sale-level discount       │
└──────────────────────────────────────────────────┘

┌──────────────────────────────────────────────────┐
│           SALE-LEVEL DISCOUNT                    │
├──────────────────────────────────────────────────┤
│ Location: saleTable (single row)                 │
│ Fields: discount_type, discount_value            │
│ Types: PERCENTAGE or FIXED_AMOUNT                │
│ Source: Customer Group or Manual Entry           │
│ Control: Requires Staff PIN to edit              │
│ Effect: Applied to total_amount                  │
└──────────────────────────────────────────────────┘

Calculation Order:
1. Calculate each item's subtotal (with item discount)
2. Sum all subtotals → total_amount
3. Apply sale-level discount to total_amount
4. Result → grand_total
```

**Example:**

```
Item 1: Rs. 100 × 2 = Rs. 200 - 10% item discount = Rs. 180
Item 2: Rs. 50 × 3 = Rs. 150 - Rs. 5 fixed = Rs. 145
─────────────────────────────────────────────────────────
Total Amount: Rs. 325
Sale Discount (5%): -Rs. 16.25
═════════════════════════════════════════════════════════
Grand Total: Rs. 308.75
```

---

## 📝 **4. DRAFT (btnDraft_Click)**

### **Purpose:**

Save incomplete/pending order for later completion

### **Process Flow:**

```
┌─────────────────────────────────────────┐
│         DRAFT CREATION FLOW              │
└─────────────────────────────────────────┘
        │
        ├─ Validation
        │  └─ Check: Cart not empty
        │
        ├─ Collect Data
        │  ├─ customer_id (can be Walk-In)
        │  ├─ All cart items (salesItemsTable)
        │  ├─ Discounts (item + sale level)
        │  ├─ Order type (DINE_IN/TAKE_AWAY)
        │  └─ Table number (if dine-in)
        │
        ├─ Call BLL.SaveSale()
        │  ├─ sale_type = "DRAFT"
        │  ├─ payment_status = "PENDING"
        │  ├─ sale_status = "COMPLETED"
        │  ├─ invoice_number = NULL
        │  ├─ quotation_number = NULL
        │  ├─ total_paid = 0
        │  └─ change_due = 0
        │
        ├─ Database: INSERT INTO Sale + SaleItem
        │  └─ Returns: sale_id
        │
        ├─ Update saleTable (in-memory)
        │  └─ Set sale_id from database
        │
        ├─ Show Success Message
        │  └─ Display detailed draft info
        │
        └─ Reset UI (btnCancel_Click)
```

**Key Characteristics:**

- ✅ No payment required
- ✅ Can be retrieved later
- ✅ No invoice/quotation number
- ✅ Useful for: Kitchen orders, incomplete orders, reservations
- ❌ Not a financial transaction (no accounting impact)

**Database State After Draft:**

```
Sale Table:
  sale_id: 123
  sale_type: "DRAFT"
  payment_status: "PENDING"
  sale_status: "COMPLETED"
  grand_total: 308.75
  total_paid: 0.00
  invoice_number: NULL
  quotation_number: NULL

SaleItem Table:
  sale_item_id: 456, sale_id: 123, product_id: 10, quantity: 2, subtotal: 180
  sale_item_id: 457, sale_id: 123, product_id: 15, quantity: 3, subtotal: 145

Payment Table:
  (no records)
```

---

## 📄 **5. QUOTATION (btnQuotation_Click)**

### **Purpose:**

Generate a formal price quote for customer (no sale yet)

### **Process Flow:**

```
┌─────────────────────────────────────────┐
│       QUOTATION CREATION FLOW            │
└─────────────────────────────────────────┘
        │
        ├─ Validation
        │  └─ Check: Cart not empty
        │
        ├─ Generate Quotation Number
        │  └─ Call: GetNextQuotationNumber()
        │     └─ Returns: "QT-2025-000001"
        │
        ├─ Collect Data (same as draft)
        │  ├─ customer_id
        │  ├─ All cart items
        │  └─ Discounts
        │
        ├─ Call BLL.SaveSale()
        │  ├─ sale_type = "QUOTATION"
        │  ├─ quotation_number = "QT-2025-000001"
        │  ├─ payment_status = "PENDING"
        │  ├─ sale_status = "COMPLETED"
        │  └─ invoice_number = NULL
        │
        ├─ Database: INSERT INTO Sale + SaleItem
        │
        ├─ Generate Quotation Report
        │  ├─ Create: Quotation.cs report
        │  ├─ Bind: salesItemsTable to report
        │  ├─ Set Parameters:
        │  │  ├─ p_quotation_no
        │  │  ├─ p_customer_name
        │  │  ├─ p_total, p_discount, p_grand_total
        │  │  └─ p_date, p_email, p_contact, p_address
        │  └─ Show: Print Preview
        │
        ├─ Show Success Message
        │
        └─ Reset UI
```

**Key Characteristics:**

- ✅ Has quotation number (QT-2025-XXXXXX)
- ✅ Generates printable quotation document
- ✅ Valid for specific time period
- ✅ Can be converted to sale later
- ❌ No payment involved
- ❌ Not a sale (no inventory impact)

**Database State After Quotation:**

```
Sale Table:
  sale_id: 124
  sale_type: "QUOTATION"
  quotation_number: "QT-2025-000001"
  payment_status: "PENDING"
  sale_status: "COMPLETED"
  grand_total: 308.75
  invoice_number: NULL

SaleItem Table:
  (same structure as draft)

Payment Table:
  (no records)
```

---

## 💳 **6. FULL SALE (btnPMComplete_Click)**

### **Purpose:**

Complete sale with full or partial payment

### **Process Flow:**

```
┌─────────────────────────────────────────────────┐
│           FULL SALE CREATION FLOW                │
└─────────────────────────────────────────────────┘
        │
        ├─ Validation
        │  ├─ Check: Cart not empty
        │  └─ Check: Payment amount matches grand total
        │
        ├─ User Opens Payment Panel (cmbPM)
        │  └─ pnlPM becomes visible
        │
        ├─ User Adds Payment Entries
        │  ├─ Payment #1: CASH - Rs. 100
        │  ├─ Payment #2: CARD - Rs. 200
        │  └─ Payment #3: CREDIT - Rs. 8.75
        │
        ├─ Validate Payments
        │  ├─ Calculate: totalPaid (exclude CREDIT)
        │  │  └─ Rs. 100 + Rs. 200 = Rs. 300
        │  ├─ Calculate: due
        │  │  └─ Rs. 308.75 - Rs. 300 = Rs. 8.75
        │  └─ Check: Sum of ALL payments = grand_total
        │     └─ Rs. 100 + Rs. 200 + Rs. 8.75 = Rs. 308.75 ✓
        │
        ├─ Generate Invoice Number
        │  └─ "INV-2025-000001"
        │
        ├─ Save Sale to Database
        │  ├─ Call: BLL.SaveSale()
        │  │  ├─ sale_type = "SALE"
        │  │  ├─ invoice_number = "INV-2025-000001"
        │  │  ├─ payment_status = "PARTIAL" (due > 0)
        │  │  │              or "PAID" (due = 0)
        │  │  ├─ sale_status = "COMPLETED"
        │  │  ├─ total_paid = Rs. 300
        │  │  └─ change_due = Rs. 0
        │  │
        │  └─ Returns: sale_id
        │
        ├─ Save Payments to Database
        │  └─ Call: BLL.SavePayments(saleId, paymentsTable)
        │     ├─ INSERT Payment: CASH, Rs. 100
        │     ├─ INSERT Payment: CARD, Rs. 200
        │     └─ INSERT Payment: CREDIT, Rs. 8.75
        │
        ├─ Generate Invoice Report
        │  ├─ Main Report: Invoice.cs
        │  │  ├─ Bind: salesItemsTable
        │  │  └─ Set: invoice parameters
        │  │
        │  └─ Subreport: SR_Payment.cs
        │     ├─ Bind: paymentsTable
        │     ├─ Set: p_total_paid = Rs. 300
        │     ├─ Set: p_due = Rs. 8.75
        │     └─ Display:
        │        ├─ CASH      100.00   2025-11-16 10:30:00
        │        ├─ CARD      200.00   2025-11-16 10:30:00
        │        ├─ CREDIT      8.75   2025-11-16 10:30:00
        │        ├─ Total Paid: 300.00
        │        └─ Due: 8.75
        │
        ├─ Show: Print Preview
        │
        ├─ Show Success Message
        │
        └─ Reset UI
```

**Key Characteristics:**

- ✅ Has invoice number (INV-2025-XXXXXX)
- ✅ Multiple payment methods allowed
- ✅ Can be partial payment (with CREDIT)
- ✅ Generates invoice with payment details
- ✅ Financial transaction (affects accounting)
- ✅ Inventory impact (reduces stock)

**Payment Validation Rules:**

```
Rule 1: Sum of ALL payments = Grand Total
  CASH + CARD + BANK_TRANSFER + CREDIT = grand_total

Rule 2: Total Paid = Non-CREDIT payments
  totalPaid = CASH + CARD + BANK_TRANSFER

Rule 3: Due = Grand Total - Total Paid
  due = grand_total - totalPaid

Rule 4: Payment Status
  if (due == 0) → "PAID"
  if (due > 0 && due < grand_total) → "PARTIAL"
  if (due == grand_total) → "CREDIT"
```

**Database State After Full Sale:**

```
Sale Table:
  sale_id: 125
  sale_type: "SALE"
  invoice_number: "INV-2025-000001"
  payment_status: "PARTIAL"
  sale_status: "COMPLETED"
  grand_total: 308.75
  total_paid: 300.00
  change_due: 0.00

SaleItem Table:
  (same structure)

Payment Table:
  payment_id: 1, sale_id: 125, payment_method: "CASH", amount: 100.00
  payment_id: 2, sale_id: 125, payment_method: "CARD", amount: 200.00
  payment_id: 3, sale_id: 125, payment_method: "CREDIT", amount: 8.75
```

---

## 🏦 **7. CREDIT SALE (btnCreditSale_Click)**

### **Purpose:**

Allow trusted customer to purchase on credit (pay later)

### **Process Flow:**

```
┌─────────────────────────────────────────────────┐
│         CREDIT SALE CREATION FLOW                │
└─────────────────────────────────────────────────┘
        │
        ├─ Validation Layer 1: Cart
        │  └─ Check: Cart not empty
        │
        ├─ Validation Layer 2: Customer
        │  ├─ Check: customer_id ≠ 1 (not Walk-In)
        │  └─ Error: "Credit not allowed for Walk-In"
        │
        ├─ Validation Layer 3: Credit Limit
        │  ├─ Get: customer.credit_limit
        │  ├─ Check: credit_limit > 0
        │  └─ Error: "No credit limit set"
        │
        ├─ Validation Layer 4: Amount Check
        │  ├─ Check: grand_total ≤ credit_limit
        │  └─ Error: "Exceeds credit limit"
        │
        ├─ Generate Invoice Number
        │  └─ "INV-2025-000002"
        │
        ├─ Save Sale to Database
        │  ├─ Call: BLL.SaveSale()
        │  │  ├─ sale_type = "CREDIT_SALE"
        │  │  ├─ invoice_number = "INV-2025-000002"
        │  │  ├─ payment_status = "CREDIT"
        │  │  ├─ sale_status = "COMPLETED"
        │  │  ├─ total_paid = 0.00
        │  │  └─ change_due = 0.00
        │  │
        │  └─ Returns: sale_id
        │
        ├─ Create CREDIT Payment Record
        │  └─ Auto-create creditPaymentTable:
        │     └─ payment_method: "CREDIT"
        │        amount: grand_total (full amount)
        │
        ├─ Save Payment to Database
        │  └─ Call: BLL.SavePayments()
        │     └─ INSERT: CREDIT payment record
        │
        ├─ Generate Invoice Report
        │  ├─ Main Report: Invoice.cs
        │  │
        │  └─ Subreport: SR_Payment.cs
        │     ├─ Bind: creditPaymentTable
        │     ├─ Set: p_total_paid = 0.00
        │     ├─ Set: p_due = grand_total
        │     └─ Display:
        │        ├─ CREDIT    308.75   2025-11-16 10:30:00
        │        └─ Due: 308.75
        │        (Total Paid: hidden because 0.00)
        │
        ├─ Show: Print Preview
        │
        ├─ Show Success Message
        │  └─ Include: Remaining credit limit
        │
        └─ Reset UI
```

**Key Characteristics:**

- ✅ Requires registered customer with credit_limit
- ✅ Validates credit limit before saving
- ✅ Creates CREDIT payment record automatically
- ✅ Shows full amount as "Due"
- ✅ Generates invoice (proof of credit sale)
- ✅ Financial transaction (receivable created)
- ✅ Inventory impact (reduces stock)
- ⚠️ Creates accounts receivable entry

**Validation Example:**

```
Customer: John Doe
Credit Limit: Rs. 5,000.00
Current Outstanding: Rs. 1,500.00 (from previous sales)
─────────────────────────────────────────────────────────
Available Credit: Rs. 3,500.00

Attempt: Credit sale for Rs. 4,000.00
Result: ❌ REJECTED (exceeds available credit)

Attempt: Credit sale for Rs. 3,000.00
Result: ✅ APPROVED
New Outstanding: Rs. 4,500.00
Remaining Credit: Rs. 500.00
```

**Database State After Credit Sale:**

```
Sale Table:
  sale_id: 126
  sale_type: "CREDIT_SALE"
  invoice_number: "INV-2025-000002"
  payment_status: "CREDIT"
  sale_status: "COMPLETED"
  grand_total: 308.75
  total_paid: 0.00
  customer_id: 5 (John Doe)

SaleItem Table:
  (same structure)

Payment Table:
  payment_id: 4, sale_id: 126, payment_method: "CREDIT", amount: 308.75
```

---

## 📊 **8. MIXED PAYMENT SCENARIOS**

### **Scenario A: Cash + Card (Full Payment)**

```
Grand Total: Rs. 500.00

Payments:
  CASH:  Rs. 200.00
  CARD:  Rs. 300.00
─────────────────────────
Total Paid: Rs. 500.00
Due: Rs. 0.00

Payment Status: "PAID"
Invoice Shows: Total Paid: 500.00
```

### **Scenario B: Cash + Card + Credit (Partial Payment)**

```
Grand Total: Rs. 500.00

Payments:
  CASH:    Rs. 150.00
  CARD:    Rs. 200.00
  CREDIT:  Rs. 150.00
───────────────────────────
Total Paid: Rs. 350.00 (excludes CREDIT)
Due: Rs. 150.00

Payment Status: "PARTIAL"
Invoice Shows:
  CASH      150.00   2025-11-16 10:30:00
  CARD      200.00   2025-11-16 10:30:00
  CREDIT    150.00   2025-11-16 10:30:00
  Total Paid: 350.00
  Due: 150.00
```

### **Scenario C: Cash Overpayment**

```
Grand Total: Rs. 500.00

Payments:
  CASH:  Rs. 600.00
─────────────────────────
Total Paid: Rs. 600.00
Change Due: Rs. 100.00
Due: Rs. 0.00

Payment Status: "PAID"
Invoice Shows:
  Total Paid: 600.00
  Change Due: 100.00
```

---

## 🔄 **9. Complete Transaction Lifecycle**

```
┌─────────────┐
│   START     │ User opens Sales Terminal
└──────┬──────┘
       │
       ▼
┌──────────────────────────────────────┐
│  BUILD CART                          │
│  • Add products                      │
│  • Apply item discounts (with PIN)   │
│  • Select customer (optional)        │
│  • Apply sale discount (with PIN)    │
└──────┬───────────────────────────────┘
       │
       ├───► DRAFT ────────► Save for later ────► Reset
       │
       ├───► QUOTATION ───► Print quote ──────► Reset
       │
       ├───► CREDIT SALE ─► Validate credit ──► Save + Print ──► Reset
       │
       └───► SELECT PAYMENT METHOD (cmbPM)
             │
             ▼
       ┌─────────────────────────────────┐
       │  PAYMENT PANEL (pnlPM)          │
       │  • Add CASH payment             │
       │  • Add CARD payment             │
       │  • Add BANK_TRANSFER payment    │
       │  • Add CREDIT payment           │
       └──────┬──────────────────────────┘
              │
              ▼
       ┌─────────────────────────────────┐
       │  VALIDATE PAYMENTS              │
       │  • Check total matches          │
       │  • Calculate due                │
       │  • Calculate change             │
       └──────┬──────────────────────────┘
              │
              ▼
       ┌─────────────────────────────────┐
       │  COMPLETE SALE (btnPMComplete)  │
       │  • Save Sale record             │
       │  • Save Payment records         │
       │  • Generate invoice             │
       │  • Print invoice                │
       └──────┬──────────────────────────┘
              │
              ▼
       ┌──────────────┐
       │    RESET     │ Ready for next customer
       └──────────────┘
```

---

## 📌 **Summary of Sale Types**

| Sale Type          | Invoice# | Quotation# | Payment Required | Report                | Stock Impact | Accounting Impact       |
| ------------------ | -------- | ---------- | ---------------- | --------------------- | ------------ | ----------------------- |
| **DRAFT**          | ❌       | ❌         | ❌               | ❌                    | ❌           | ❌                      |
| **QUOTATION**      | ❌       | ✅         | ❌               | ✅ Quotation          | ❌           | ❌                      |
| **CREDIT_SALE**    | ✅       | ❌         | ❌ (full credit) | ✅ Invoice            | ✅           | ✅ Receivable           |
| **SALE (Full)**    | ✅       | ❌         | ✅ (due = 0)     | ✅ Invoice + Payments | ✅           | ✅ Revenue              |
| **SALE (Partial)** | ✅       | ❌         | ✅ (due > 0)     | ✅ Invoice + Payments | ✅           | ✅ Revenue + Receivable |

---

This comprehensive flow ensures proper handling of all transaction types while maintaining data integrity and providing complete audit trails. Each sale type serves a specific business purpose and follows strict validation rules to prevent errors.

---

## 🖨️ **10. Printing System**

### **Configuration Options:**

The system supports flexible printing configurations based on business needs.

**1. Print Formats:**

- **80mm Thermal Receipt:** Standard POS receipt format
- **A4 Invoice:** Full-page invoice for corporate/large orders
- **Both:** Prints both formats simultaneously

**2. System Settings:**

- **ENABLE_THERMAL_PRINT:** (True/False) - Enable 80mm printing
- **ENABLE_A4_PRINT:** (True/False) - Enable A4 printing
- **AUTO_PRINT_ON_COMPLETION:** (True/False) - Automatically print without preview dialog

### **Printing Logic Flow:**

```
On Sale Completion:
  │
  ├─ Check AUTO_PRINT_ON_COMPLETION
  │  ├─ TRUE: Proceed to print directly
  │  └─ FALSE: Show Print Preview dialog
  │
  ├─ Check Print Format Settings
  │  ├─ If ENABLE_THERMAL_PRINT = TRUE
  │  │  └─ Generate & Print ThermalInvoice.cs
  │  │
  │  └─ If ENABLE_A4_PRINT = TRUE
  │     └─ Generate & Print Invoice.cs
```
