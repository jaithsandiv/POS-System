using DevExpress.DataAccess.Native.Excel;
using DevExpress.XtraEditors;
using POS.BLL;
using POS.DAL;
using POS.DAL.DataSource;
using POS.PAL.REPORT;
using POS.PAL.REPORT.SUBREPORT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Printing;
using System.Windows.Forms;
using DataView = System.Data.DataView;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_SalesTerminal : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_SalesTerminal _bllSalesTerminal = new BLL_SalesTerminal();
        private readonly BLL_Discount _bllDiscount = new BLL_Discount();

        private DataTable productsTable;
        private DataTable categoriesTable;
        private DataTable brandsTable;
        private DataTable saleTable;
        private DataTable salesItemsTable;
        private DataTable customersTable;
        private DataTable tableNosTable;
        private DataTable staffPinTable;
        private DataTable paymentsTable;

        private int paymentEntryCounter = 0;

        // Labels for Payment Summary
        private LabelControl lblTotalPaid;
        private LabelControl lblBalance;

        public UC_SalesTerminal()
        {
            InitializeComponent();
            InitializePaymentSummaryControls();
            gvTransactionSum.CustomColumnDisplayText += gvTransactionSum_CustomColumnDisplayText;
            btnCancel_Click(null, null);
        }

        private void InitializePaymentSummaryControls()
        {
            // Total Paid Label
            lblTotalPaid = new LabelControl
            {
                Text = "Total Paid: Rs. 0.00",
                Appearance = { Font = new Font("Segoe UI", 12F, FontStyle.Bold), ForeColor = Color.FromArgb(4, 181, 152) },
                AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                Width = 300,
                Height = 30,
                Location = new Point(10, 520) // Adjust position as needed within pnlPM or relative to btnAddPayment
            };

            // Balance Label
            lblBalance = new LabelControl
            {
                Text = "Balance: Rs. 0.00",
                Appearance = { Font = new Font("Segoe UI", 12F, FontStyle.Bold), ForeColor = Color.Red },
                AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                Width = 300,
                Height = 30,
                Location = new Point(10, 550) // Below Total Paid
            };

            // Add to pnlPM
            // Note: Since pnlPM uses manual layout with ScrollableControl and bottom buttons,
            // we should place these above the 'Add Payment' button or in a visible area.
            // Let's place them just above the 'Add Payment' button.
            // But 'Add Payment' is at 593. So 520 and 550 are fine.
            pnlPM.Controls.Add(lblTotalPaid);
            pnlPM.Controls.Add(lblBalance);
        }

        private void ResetUIElements()
        {
            // Reset text fields
            txtTotal.Text = "0.00";
            txtGrandTotal.Text = "0.00";
            txtDiscount.EditValue = 0m;
            txtStaffPin.Text = string.Empty;
            txtCustomer.Text = "Walk-In Customer";
            txtBarcode.Text = string.Empty;

            // Reset text field properties
            txtStaffPin.ReadOnly = true;
            txtDiscount.Properties.ReadOnly = true;
            txtTotal.Properties.ReadOnly = true;
            txtGrandTotal.Properties.ReadOnly = true;

            // Reset discount button caption
            if (txtDiscount.Properties.Buttons.Count > 0)
                txtDiscount.Properties.Buttons[0].Caption = "%";

            // Reset combo boxes
            cmbPM.Properties.Items.Clear();
            cmbPM.Properties.Items.AddRange(new string[] { "CASH", "CARD", "BANK_TRANSFER", "CREDIT" });
            cmbPM.SelectedIndex = -1;
            cmbTableNo.SelectedIndex = -1;

            // Reset discount editing
            SetDiscountFieldsEditable(false);

            // Reset visible panels
            pnlCustomers.Visible = false;
            pnlPM.Visible = false;

            // Reset KOT UI
            CheckKotEnabled();
            pnlKOTTableNo.Visible = true;

            // Reset dine/takeaway buttons appearance
            btnDineIn.Appearance.BackColor = Color.FromArgb(4, 181, 152);
            btnDineIn.Appearance.ForeColor = Color.White;
            btnTakeAway.Appearance.BackColor = Color.White;
            btnTakeAway.Appearance.ForeColor = Color.Black;

            // Reset button highlights
            ResetBrandButtonColors();
            ResetCategoryButtonColors();
        }

        private void FilterProducts(string brandId, string categoryId)
        {
            DataView filteredView = new DataView(productsTable);

            string filter = string.Empty;

            if (brandId != "All Brands")
            {
                filter += $"brand_id = '{brandId}'";
            }

            if (categoryId != "All Categories")
            {
                if (!string.IsNullOrEmpty(filter))
                {
                    filter += " AND ";
                }
                filter += $"category_id = '{categoryId}'";
            }

            filteredView.RowFilter = filter;

            List<(string Name, byte[] Image, string Stock)> productDetails = new List<(string, byte[], string)>();
            foreach (DataRowView row in filteredView)
            {
                string name = row["product_name"]?.ToString();
                byte[] image = row["image"] as byte[];
                string stock = row["stock_quantity"]?.ToString();

                productDetails.Add((name, image, stock));
            }

            AddProductButtonsToScrollableControl(productDetails);
        }

        private string _selectedBrandId = "All Brands";
        private string _selectedCategoryId = "All Categories";

        private void BrandButton_Click(object sender, EventArgs e)
        {
            if (sender is DevExpress.XtraEditors.SimpleButton button)
            {
                _selectedBrandId = button.Tag.ToString();
                FilterProducts(_selectedBrandId, _selectedCategoryId);
            }
        }

        private void CatButton_Click(object sender, EventArgs e)
        {
            if (sender is DevExpress.XtraEditors.SimpleButton button)
            {
                _selectedCategoryId = button.Tag.ToString();
                FilterProducts(_selectedBrandId, _selectedCategoryId);
            }
        }

        private void CalculateAndUpdateGrandTotal()
        {
            if (saleTable.Rows.Count == 0)
                return;

            DataRow saleRow = saleTable.Rows[0];

            // Get total amount (sum of all item subtotals)
            decimal totalAmount = 0;
            if (decimal.TryParse(saleRow["total_amount"]?.ToString(), out decimal parsedTotal))
            {
                totalAmount = parsedTotal;
            }

            // Get sale-level discount details
            string discountType = saleRow["discount_type"]?.ToString() ?? "PERCENTAGE";
            decimal discountValue = 0;
            if (decimal.TryParse(saleRow["discount_value"]?.ToString(), out decimal parsedDiscount))
            {
                discountValue = parsedDiscount;
            }

            // Calculate discount amount
            decimal discountAmount = 0;
            if (discountType == "PERCENTAGE")
            {
                discountAmount = totalAmount * discountValue / 100m;
            }
            else if (discountType == "FIXED_AMOUNT")
            {
                discountAmount = discountValue;
            }

            // Calculate grand total
            decimal grandTotal = Math.Max(0, totalAmount - discountAmount);
            saleRow["grand_total"] = grandTotal.ToString("F2");
            txtGrandTotal.Text = grandTotal.ToString("F2");
        }

        private void AddProductToSalesItems(DataRow product)
        {
            if (product == null)
                return;

            // Check if the product already exists in the salesItemsTable
            DataRow existingRow = null;
            foreach (DataRow row in salesItemsTable.Rows)
            {
                if (row["product_id"].ToString() == product["product_id"].ToString())
                {
                    existingRow = row;
                    break;
                }
            }

            if (existingRow != null)
            {
                // Increase the quantity of the existing product
                int currentQuantity = Convert.ToInt32(existingRow["quantity"]);
                existingRow["quantity"] = currentQuantity + 1;

                // Recalculate subtotal
                decimal price = Convert.ToDecimal(existingRow["unit_price"]);
                string promotionType = existingRow["discount_type"].ToString();
                decimal discountValue = Convert.ToDecimal(existingRow["discount_value"]);
                decimal discountAmount = promotionType == "PERCENTAGE" ? (price * discountValue / 100m) : discountValue;
                existingRow["subtotal"] = (price - discountAmount) * Convert.ToInt32(existingRow["quantity"]);
            }
            else
            {
                // Add new product to the salesItemsTable
                DataRow newRow = salesItemsTable.NewRow();
                newRow["sale_item_id"] = DBNull.Value;
                newRow["sale_id"] = DBNull.Value;
                newRow["product_id"] = product["product_id"];
                newRow["product_name"] = product["product_name"];
                newRow["product_code"] = product["product_code"];
                newRow["unit_price"] = Convert.ToDecimal(product["selling_price"]);
                newRow["quantity"] = 1;

                // Load discount details from the product (fetch active promotion)
                string promotionType = "PERCENTAGE";
                decimal discountValue = 0;

                // Check for active promotions for this product
                // Note: In a real scenario, we might want to cache this or fetch in bulk, but for now we fetch per add
                // We need to iterate through active promotions and find if this product is in one
                try
                {
                    DataTable activePromos = _bllDiscount.GetDiscounts(); // Gets active promotions
                    bool promoFound = false;
                    foreach(DataRow promo in activePromos.Rows)
                    {
                        int promoId = Convert.ToInt32(promo["discount_id"]);
                        // Check if current date is within range
                        DateTime start = Convert.ToDateTime(promo["start_date"]);
                        DateTime end = Convert.ToDateTime(promo["end_date"]);
                        if (DateTime.Now >= start && DateTime.Now <= end)
                        {
                            DataTable promoProducts = _bllDiscount.GetProductsByPromotionID(promoId);
                            foreach(DataRow pp in promoProducts.Rows)
                            {
                                if (pp["product_id"].ToString() == product["product_id"].ToString())
                                {
                                    promotionType = pp["promotion_type"].ToString();
                                    discountValue = Convert.ToDecimal(pp["discount_value"]);
                                    promoFound = true;
                                    break;
                                }
                            }
                        }
                        if (promoFound) break;
                    }
                }
                catch { /* Ignore errors in promo fetching for stability */ }

                newRow["discount_type"] = promotionType;
                newRow["discount_value"] = discountValue;

                // Calculate subtotal
                decimal price = Convert.ToDecimal(product["selling_price"]);
                decimal discountAmount = promotionType == "PERCENTAGE" ? (price * discountValue / 100m) : discountValue;
                newRow["subtotal"] = price - discountAmount;

                // Set all default values for SaleItem fields
                newRow["status"] = "A";
                newRow["created_by"] = DBNull.Value;
                newRow["created_date"] = DBNull.Value;
                newRow["updated_by"] = DBNull.Value;
                newRow["updated_date"] = DBNull.Value;
                newRow["remove_action"] = "X"; // Set button text
                newRow["unit"] = product["unit_name"];

                salesItemsTable.Rows.Add(newRow);
            }

            RecalculateTotals();
        }

        private void RecalculateTotals()
        {
            // Update total amount and total items
            decimal totalAmount = 0;
            int totalItems = 0;

            foreach (DataRow row in salesItemsTable.Rows)
            {
                if (row.RowState == DataRowState.Deleted) continue;

                totalAmount += Convert.ToDecimal(row["subtotal"]);
                totalItems += Convert.ToInt32(row["quantity"]);
            }

            // Update the saleTable with total amount and total items
            if (saleTable.Rows.Count > 0)
            {
                DataRow saleRow = saleTable.Rows[0];
                saleRow["total_amount"] = totalAmount.ToString("F2");
                saleRow["total_items"] = totalItems.ToString();
            }

            txtTotal.Text = totalAmount.ToString("F2");

            // Calculate and update grand total with sale-level discount
            CalculateAndUpdateGrandTotal();

            gvTransactionSum.GridControl.DataSource = salesItemsTable;
        }

        private void ProductButton_Click(object sender, EventArgs e)
        {
            if (sender is DevExpress.XtraEditors.SimpleButton button)
            {
                string selectedProduct = button.Tag.ToString();

                // Fetch product details from the global products table
                DataRow[] productRows = productsTable.Select($"product_name = '{selectedProduct}'");

                if (productRows.Length > 0)
                {
                    AddProductToSalesItems(productRows[0]);
                }
            }
        }

        private void gvTransactionSum_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var view = gvTransactionSum;
            int rowHandle = e.RowHandle;

            if (rowHandle < 0) return;

            if (e.Column.Name == "colRemove") // Remove column
            {
                view.DeleteRow(rowHandle);
                RecalculateTotals();
            }
            else if (e.Column.FieldName == "discount_type") // Discount Type column
            {
                string currentType = view.GetRowCellValue(rowHandle, "discount_type")?.ToString() ?? "PERCENTAGE";
                string newType = currentType == "PERCENTAGE" ? "FIXED_AMOUNT" : "PERCENTAGE";

                view.SetRowCellValue(rowHandle, "discount_type", newType);

                // Recalculate subtotal for this row
                decimal price = Convert.ToDecimal(view.GetRowCellValue(rowHandle, "unit_price"));
                decimal qty = Convert.ToDecimal(view.GetRowCellValue(rowHandle, "quantity"));
                decimal discountValue = Convert.ToDecimal(view.GetRowCellValue(rowHandle, "discount_value"));

                decimal discountAmount = newType == "PERCENTAGE" ? (price * qty * discountValue / 100m) : discountValue;
                view.SetRowCellValue(rowHandle, "subtotal", price * qty - discountAmount);

                RecalculateTotals();
            }
        }

        private void gvTransactionSum_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "quantity" ||
                e.Column.FieldName == "discount_value")
            {
                decimal price = Convert.ToDecimal(gvTransactionSum.GetRowCellValue(e.RowHandle, "unit_price"));
                decimal qty = Convert.ToDecimal(gvTransactionSum.GetRowCellValue(e.RowHandle, "quantity"));
                decimal discountValue = Convert.ToDecimal(gvTransactionSum.GetRowCellValue(e.RowHandle, "discount_value"));
                string type = gvTransactionSum.GetRowCellValue(e.RowHandle, "discount_type")?.ToString() ?? "PERCENTAGE";

                decimal discountAmount = type == "PERCENTAGE" ? (price * qty * discountValue / 100m) : discountValue;
                gvTransactionSum.SetRowCellValue(e.RowHandle, "subtotal", price * qty - discountAmount);

                RecalculateTotals();
            }
        }

        private void gvTransactionSum_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "discount_type")
            {
                string value = e.Value?.ToString();
                if (value == "PERCENTAGE")
                    e.DisplayText = "%";
                else if (value == "FIXED_AMOUNT")
                    e.DisplayText = "Rs.";
            }
        }

        private void LoadCategories()
        {
            List<(string Id, string Name)> categoryDetails = new List<(string, string)> { ("All Categories", "All Categories") };
            foreach (DataRow row in categoriesTable.Rows)
            {
                if (row["category_id"] != DBNull.Value && row["category_name"] != DBNull.Value)
                {
                    categoryDetails.Add((row["category_id"].ToString(), row["category_name"].ToString()));
                }
            }

            AddCatButtonsToScrollableControl(categoryDetails);
        }

        private void AddCatButtonsToScrollableControl(List<(string Id, string Name)> categoryDetails, int buttonWidth = 150, int buttonHeight = 41, int spacing = 10)
        {
            xtraScrollableControl1.Controls.Clear();

            int currentX = 0;

            foreach (var category in categoryDetails)
            {
                DevExpress.XtraEditors.SimpleButton categoryButton = new DevExpress.XtraEditors.SimpleButton
                {
                    Text = category.Name,
                    Name = $"btnCat{category.Id}",
                    Width = buttonWidth,
                    Height = buttonHeight,
                    Tag = category.Id,
                    Appearance = { BackColor = Color.LightGray } // Default color
                };

                categoryButton.Location = new Point(currentX, 0);

                categoryButton.Click += (s, e) =>
                {
                    ResetCategoryButtonColors();
                    categoryButton.Appearance.BackColor = Color.LightBlue; // Highlight selected button
                    CatButton_Click(s, e);
                };

                xtraScrollableControl1.Controls.Add(categoryButton);

                currentX += buttonWidth + spacing;
            }
        }

        private void ResetCategoryButtonColors()
        {
            foreach (Control control in xtraScrollableControl1.Controls)
            {
                if (control is DevExpress.XtraEditors.SimpleButton button)
                {
                    button.Appearance.BackColor = Color.LightGray; // Reset to default color
                }
            }
        }

        private void LoadBrands()
        {
            List<(string Id, string Name)> brandDetails = new List<(string, string)> { ("All Brands", "All Brands") };
            foreach (DataRow row in brandsTable.Rows)
            {
                if (row["brand_id"] != DBNull.Value && row["brand_name"] != DBNull.Value)
                {
                    brandDetails.Add((row["brand_id"].ToString(), row["brand_name"].ToString()));
                }
            }

            AddBrandButtonsToScrollableControl(brandDetails);
        }

        private void AddBrandButtonsToScrollableControl(List<(string Id, string Name)> brandDetails, int buttonWidth = 150, int buttonHeight = 41, int spacing = 10)
        {
            xtraScrollableControl2.Controls.Clear();

            int currentX = 0;

            foreach (var brand in brandDetails)
            {
                DevExpress.XtraEditors.SimpleButton brandButton = new DevExpress.XtraEditors.SimpleButton
                {
                    Text = brand.Name,
                    Name = $"btnBrand{brand.Id}",
                    Width = buttonWidth,
                    Height = buttonHeight,
                    Tag = brand.Id,
                    Appearance = { BackColor = Color.LightGray } // Default color
                };

                brandButton.Location = new Point(currentX, 0);

                brandButton.Click += (s, e) =>
                {
                    ResetBrandButtonColors();
                    brandButton.Appearance.BackColor = Color.LightBlue; // Highlight selected button
                    BrandButton_Click(s, e);
                };

                xtraScrollableControl2.Controls.Add(brandButton);

                currentX += buttonWidth + spacing;
            }
        }

        private void ResetBrandButtonColors()
        {
            foreach (Control control in xtraScrollableControl2.Controls)
            {
                if (control is DevExpress.XtraEditors.SimpleButton button)
                {
                    button.Appearance.BackColor = Color.LightGray;
                }
            }
        }

        private void LoadProducts()
        {
            List<(string Name, byte[] Image, string Stock)> productDetails = new List<(string, byte[], string)>();
            foreach (DataRow row in productsTable.Rows)
            {
                string name = row["product_name"]?.ToString();
                byte[] image = row["image"] as byte[];
                string stock = row["stock_quantity"]?.ToString();

                productDetails.Add((name, image, stock));
            }

            AddProductButtonsToScrollableControl(productDetails);
        }

        private void AddProductButtonsToScrollableControl(List<(string Name, byte[] Image, string Stock)> productDetails, int buttonWidth = 170, int buttonHeight = 150, int spacing = 10, int maxButtonsPerRow = 4)
        {
            xtraScrollableControl3.Controls.Clear();

            int currentX = 0;
            int currentY = 0;

            for (int i = 0; i < productDetails.Count; i++)
            {
                var product = productDetails[i];

                DevExpress.XtraEditors.SimpleButton productButton = new DevExpress.XtraEditors.SimpleButton
                {
                    Text = $"{product.Name}\nStock: {product.Stock}",
                    Name = $"btnProduct{i}",
                    Width = buttonWidth,
                    Height = buttonHeight,
                    Tag = product.Name
                };

                if (product.Image != null)
                {
                    using (var ms = new System.IO.MemoryStream(product.Image))
                    {
                        productButton.ImageOptions.Image = Image.FromStream(ms);
                        productButton.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
                    }
                }

                productButton.Location = new Point(currentX, currentY);

                productButton.Click += ProductButton_Click;

                xtraScrollableControl3.Controls.Add(productButton);

                if ((i + 1) % maxButtonsPerRow == 0)
                {
                    currentX = 0;
                    currentY += buttonHeight + spacing;
                }
                else
                {
                    currentX += buttonWidth + spacing;
                }
            }
        }

        private void CheckKotEnabled()
        {
            bool isKotEnabled = _bllSalesTerminal.IsKotEnabled();
            if (isKotEnabled)
            {
                pnlKOT.Visible = true;
            }
            else
            {
                pnlKOT.Visible = false;
            }
        }

        private void LoadTableNos()
        {
            DataTable TableNos = _bllSalesTerminal.GetTables();

            cmbTableNo.Properties.Items.Clear();

            foreach (DataRow row in TableNos.Rows)
            {
                cmbTableNo.Properties.Items.Add(row["table_number"]);
            }
        }

        private void SaleTable_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (e.Row.Table.Columns.Contains("total_amount"))
            {
                decimal totalAmount = Convert.ToDecimal(e.Row["total_amount"]);
                decimal grandTotal = 0;

                // Get grand_total if available
                if (e.Row.Table.Columns.Contains("grand_total") &&
                    decimal.TryParse(e.Row["grand_total"]?.ToString(), out decimal parsedGrandTotal))
                {
                    grandTotal = parsedGrandTotal;
                }
                else
                {
                    grandTotal = totalAmount; // Fallback to total_amount if grand_total not set
                }

                // Display total and grand total
                txtTotal.Text = totalAmount.ToString("F2");
            }
        }

        private void btnTakeAway_Click(object sender, EventArgs e)
        {
            pnlKOTTableNo.Visible = false;
            btnTakeAway.Appearance.BackColor = Color.FromArgb(4, 181, 152);
            btnTakeAway.Appearance.ForeColor = Color.White;
            btnDineIn.Appearance.BackColor = Color.White;
            btnDineIn.Appearance.ForeColor = Color.Black;
        }

        private void btnDineIn_Click(object sender, EventArgs e)
        {
            pnlKOTTableNo.Visible = true;
            btnDineIn.Appearance.BackColor = Color.FromArgb(4, 181, 152);
            btnDineIn.Appearance.ForeColor = Color.White;
            btnTakeAway.Appearance.BackColor = Color.White;
            btnTakeAway.Appearance.ForeColor = Color.Black;
        }

        private void SetDiscountFieldsEditable(bool isEditable)
        {
            // Product discount in grid (discount_value column)
            // Note: Now we allow editing only by click for discount type, but value editing might still need pin
            // Assuming "Normal Text Edit Repos" means standard editing, we might need to enable editing for value.
            // For now, let's keep logic as is but on the new column structure if applicable.
            // Since we removed button edit, it's a standard text edit now.
            gvTransactionSum.Columns["discount_value"].OptionsColumn.AllowEdit = isEditable;
            gvTransactionSum.Columns["discount_value"].OptionsColumn.AllowFocus = isEditable;

            // Sale discount field (txtDiscount)
            if (txtDiscount != null)
            {
                txtDiscount.Properties.ReadOnly = !isEditable;
            }
        }

        private void txtStaffPin_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStaffPin.Text))
            {
                SetDiscountFieldsEditable(false);
                return;
            }

            string enteredPin = txtStaffPin.Text.Trim();

            // Search for the PIN in the staffPinTable
            DataRow[] matchingRows = staffPinTable.Select($"pin_code = '{enteredPin}'");

            if (matchingRows.Length > 0)
            {
                // PIN found - Enable discount editing
                SetDiscountFieldsEditable(true);
            }
            else
            {
                SetDiscountFieldsEditable(false);
            }
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Only process on Enter key press
            if (e.KeyChar != (char)Keys.Return)
                return;

            e.Handled = true; // Prevent the beep sound

            if (string.IsNullOrWhiteSpace(txtBarcode.Text))
                return;

            string scannedBarcode = txtBarcode.Text.Trim();

            // Search for the product by barcode in the global products table
            DataRow[] productRows = productsTable.Select($"barcode = '{scannedBarcode}'");

            if (productRows.Length > 0)
            {
                // Product found - Add to sales items
                AddProductToSalesItems(productRows[0]);

                // Clear the barcode field for the next scan
                txtBarcode.Text = string.Empty;
            }
            else
            {
                // Product not found - Show warning
                MessageBox.Show($"Product with barcode '{scannedBarcode}' not found.", "Invalid Barcode", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Clear the barcode field
                txtBarcode.Text = string.Empty;
            }
        }

        private void txtDiscount_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (saleTable.Rows.Count == 0)
                return;

            DataRow saleRow = saleTable.Rows[0];

            // Get current discount type
            string currentType = saleRow["discount_type"]?.ToString() ?? "PERCENTAGE";
            string newType = currentType == "PERCENTAGE" ? "FIXED_AMOUNT" : "PERCENTAGE";

            // Update discount type in saleTable
            saleRow["discount_type"] = newType;

            // Update button caption on txtDiscount control
            var txtDiscountControl = (DevExpress.XtraEditors.ButtonEdit)sender;
            if (txtDiscountControl?.Properties.Buttons.Count > 0)
            {
                txtDiscountControl.Properties.Buttons[0].Caption = newType == "PERCENTAGE" ? "%" : "Rs.";
            }

            // Recalculate grand total with new discount type
            CalculateAndUpdateGrandTotal();
        }

        private void txtDiscount_EditValueChanged(object sender, EventArgs e)
        {
            if (saleTable.Rows.Count == 0)
                return;

            DataRow saleRow = saleTable.Rows[0];

            // Get the discount value from the control
            var txtDiscountControl = (DevExpress.XtraEditors.ButtonEdit)sender;
            if (txtDiscountControl?.EditValue != null && decimal.TryParse(txtDiscountControl.EditValue.ToString(), out decimal discountValue))
            {
                // Update discount value in saleTable
                saleRow["discount_value"] = discountValue.ToString("F2");

                // Recalculate grand total
                CalculateAndUpdateGrandTotal();
            }
        }

        private void txtStaffPin_DoubleClick(object sender, EventArgs e)
        {
            if (txtStaffPin.ReadOnly)
            {
                txtStaffPin.ReadOnly = false;
                txtStaffPin.Text = string.Empty;
            }
            else
            {
                txtStaffPin.ReadOnly = true;
                txtStaffPin.Text = string.Empty;
                SetDiscountFieldsEditable(false);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            pnlCustomers.Visible = !pnlCustomers.Visible;
        }

        private void cmbCustomer_DoubleClick(object sender, EventArgs e)
        {
            pnlCustomers.Visible = true;
            gcCustomers.DataSource = customersTable;
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.BackColor = Color.IndianRed;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.BackColor = Color.DimGray;
        }

        private void gvCustomers_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                string customerId = gvCustomers.GetFocusedRowCellValue("customer_id").ToString();

                // Find the customer row in customersTable
                DataRow[] customerRows = customersTable.Select($"customer_id = '{customerId}'");

                if (customerRows.Length > 0)
                {
                    DataRow selectedCustomer = customerRows[0];

                    // Update the saleTable with customer information
                    if (saleTable.Rows.Count > 0)
                    {
                        DataRow saleRow = saleTable.Rows[0];
                        saleRow["customer_id"] = selectedCustomer["customer_id"];

                        // Set discount type to PERCENTAGE
                        saleRow["discount_type"] = "PERCENTAGE";

                        // Set discount value from customer group discount
                        decimal customerDiscount = 0;
                        if (selectedCustomer["discount_percent"] != DBNull.Value &&
                            !string.IsNullOrWhiteSpace(selectedCustomer["discount_percent"]?.ToString()))
                        {
                            if (decimal.TryParse(selectedCustomer["discount_percent"].ToString(), out decimal parsedDiscount))
                            {
                                customerDiscount = parsedDiscount;
                            }
                        }

                        saleRow["discount_value"] = customerDiscount.ToString("F2");
                    }

                    // Update txtDiscount control with customer discount
                    if (txtDiscount != null)
                    {
                        decimal customerDiscount = 0;
                        if (selectedCustomer["discount_percent"] != DBNull.Value &&
                            !string.IsNullOrWhiteSpace(selectedCustomer["discount_percent"]?.ToString()))
                        {
                            if (decimal.TryParse(selectedCustomer["discount_percent"].ToString(), out decimal parsedDiscount))
                            {
                                customerDiscount = parsedDiscount;
                            }
                        }

                        txtDiscount.EditValue = customerDiscount;

                        // Update button caption to show PERCENTAGE
                        if (txtDiscount.Properties.Buttons.Count > 0)
                        {
                            txtDiscount.Properties.Buttons[0].Caption = "%";
                        }
                    }

                    // Recalculate grand total with new customer discount
                    CalculateAndUpdateGrandTotal();

                    pnlCustomers.Visible = false;
                    txtCustomer.Text = $"{selectedCustomer["full_name"]}";
                }
            }
        }

        private void cmbPM_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedPaymentMethod = cmbPM.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(selectedPaymentMethod))
                return;

            // Show payment panel and initialize it
            if (!pnlPM.Visible)
            {
                InitializePaymentPanel();
                pnlPM.Visible = true;
            }

            // Add the first payment entry with the selected method
            AddPaymentEntry(selectedPaymentMethod);
        }

        private void InitializePaymentPanel()
        {
            // Clear the scrollable control
            xtraScrollableControl4.Controls.Clear();

            // Initialize payments DataTable
            // If paymentsTable is already populated (e.g. from LoadSaleIntoTerminal), use it.
            // Otherwise, initialize new.
            if (paymentsTable == null)
            {
                DAL_DS_SalesTerminal ds = new DAL_DS_SalesTerminal();
                paymentsTable = ds.Payment;
            }

            // Only clear if we are starting fresh (sale_id is null/0)
            int currentSaleId = 0;
            if (saleTable != null && saleTable.Rows.Count > 0 && saleTable.Rows[0]["sale_id"] != DBNull.Value)
            {
                int.TryParse(saleTable.Rows[0]["sale_id"].ToString(), out currentSaleId);
            }

            if (currentSaleId == 0)
            {
                paymentsTable.Clear();
                paymentEntryCounter = 0;
            }
            else
            {
                // If loading existing, we need to populate the UI with existing payments
                // For now, let's just ensure we don't wipe the data table.
                // Re-populating the UI (adding panels) for existing payments would be nice but complex
                // as we need to map rows back to UI controls.
                // For the requirement "complete partial payments", the user just needs to see balance and add NEW payment.
                // So we can list existing payments in a read-only way or just show totals.
                // Let's rely on `lblTotalPaid` which calculates from `paymentsTable`.
                // We will NOT add panels for existing payments to avoid complexity of editing them.
                // User adds NEW payments.
            }

            // Reset labels
            UpdatePaymentSummaryUI();
        }

        private void btnAddPayment_Click(object sender, EventArgs e)
        {
            // Add a new payment entry with no method pre-selected
            AddPaymentEntry(null);
        }

        private void AddPaymentEntry(string preselectedMethod = null)
        {
            paymentEntryCounter++;
            int entryId = paymentEntryCounter;

            // Determine panel height based on payment method
            int panelHeight = 180; // Default for CASH/CREDIT (smaller)
            if (preselectedMethod != null)
            {
                if (preselectedMethod.ToUpper() == "CARD")
                    panelHeight = 380; // Increased for CARD (more fields + better spacing)
                else if (preselectedMethod.ToUpper() == "BANK_TRANSFER")
                    panelHeight = 260; // Medium for BANK_TRANSFER
            }

            // Create a panel for this payment entry with DevExpress styling
            PanelControl paymentEntryPanel = new PanelControl
            {
                Name = $"pnlPaymentEntry{entryId}",
                Width = xtraScrollableControl4.Width - 30,
                Height = panelHeight,
                BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple,
                Dock = DockStyle.Top // Use docking for automatic positioning
            };
            paymentEntryPanel.Appearance.BorderColor = Color.LightGray;
            paymentEntryPanel.Appearance.Options.UseBorderColor = true;
            // Add margin/padding between panels
            paymentEntryPanel.Padding = new System.Windows.Forms.Padding(5, 5, 5, 15); // Top, Left, Right, Bottom margins

            // Payment method label (matching your existing label style)
            LabelControl lblMethod = new LabelControl
            {
                Text = $"Payment #{entryId}",
                Location = new Point(10, 10),
                AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                Width = 160
            };
            lblMethod.Appearance.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblMethod.Appearance.Options.UseFont = true;

            // Payment method combo box (matching cmbPM style)
            ComboBoxEdit cmbMethod = new ComboBoxEdit
            {
                Name = $"cmbMethod{entryId}",
                Location = new Point(10, 35),
                Tag = entryId
            };
            cmbMethod.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            cmbMethod.Properties.Appearance.Options.UseFont = true;
            cmbMethod.Properties.Items.AddRange(new string[] { "CASH", "CARD", "BANK_TRANSFER", "CREDIT" });
            cmbMethod.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cmbMethod.Properties.Padding = new System.Windows.Forms.Padding(10);
            cmbMethod.Size = new Size(paymentEntryPanel.Width - 130, 44);

            if (!string.IsNullOrEmpty(preselectedMethod))
            {
                cmbMethod.SelectedItem = preselectedMethod;
            }

            // Fields panel (will hold method-specific fields) - INCREASED HEIGHT
            PanelControl pnlFields = new PanelControl
            {
                Name = $"pnlFields{entryId}",
                Location = new Point(10, 90),
                Width = paymentEntryPanel.Width - 20,
                Height = panelHeight - 140, // Dynamic height based on panel
                BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder,
                Tag = entryId
            };

            // Remove button (matching btnCancel style) - DYNAMIC POSITION
            SimpleButton btnRemove = new SimpleButton
            {
                Text = "× Remove",
                Name = $"btnRemove{entryId}",
                Width = 100,
                Height = 30,
                Location = new Point(paymentEntryPanel.Width - 115, panelHeight - 45),
                Tag = entryId
            };
            btnRemove.Appearance.BackColor = Color.IndianRed;
            btnRemove.Appearance.ForeColor = Color.White;
            btnRemove.Appearance.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnRemove.Appearance.Options.UseBackColor = true;
            btnRemove.Appearance.Options.UseForeColor = true;
            btnRemove.Appearance.Options.UseFont = true;
            btnRemove.Click += (s, ev) => RemovePaymentEntry(entryId);

            // Wire up the method selection change event
            cmbMethod.SelectedIndexChanged += (s, ev) =>
            {
                string selectedMethod = cmbMethod.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedMethod))
                {
                    // Update panel height based on selected method
                    int newHeight = 180;
                    if (selectedMethod.ToUpper() == "CARD")
                        newHeight = 380;
                    else if (selectedMethod.ToUpper() == "BANK_TRANSFER")
                        newHeight = 260;
                    else if (selectedMethod.ToUpper() == "CREDIT")
                        newHeight = 200;

                    paymentEntryPanel.Height = newHeight;
                    pnlFields.Height = newHeight - 140;
                    btnRemove.Location = new Point(paymentEntryPanel.Width - 115, newHeight - 45);

                    PopulatePaymentFields(pnlFields, selectedMethod, entryId);
                }
            };

            // Add controls to payment entry panel
            paymentEntryPanel.Controls.Add(lblMethod);
            paymentEntryPanel.Controls.Add(cmbMethod);
            paymentEntryPanel.Controls.Add(pnlFields);
            paymentEntryPanel.Controls.Add(btnRemove);

            // Add payment entry panel to scrollable control
            // Since we're using Dock.Top, add to the END to maintain visual order (top to bottom)
            xtraScrollableControl4.Controls.Add(paymentEntryPanel);
            xtraScrollableControl4.Controls.SetChildIndex(paymentEntryPanel, 0); // Move to top

            // If method is preselected, populate fields immediately
            if (!string.IsNullOrEmpty(preselectedMethod))
            {
                PopulatePaymentFields(pnlFields, preselectedMethod, entryId);
            }

            // Create a payment row in paymentsTable
            DataRow paymentRow = paymentsTable.NewRow();
            paymentRow["payment_id"] = DBNull.Value;
            paymentRow["sale_id"] = DBNull.Value;
            paymentRow["payment_method"] = preselectedMethod ?? string.Empty;
            paymentRow["amount"] = "0.00";
            paymentRow["status"] = "A";
            paymentRow["created_by"] = DBNull.Value;
            paymentRow["created_date"] = DBNull.Value;
            paymentRow["updated_by"] = DBNull.Value;
            paymentRow["updated_date"] = DBNull.Value;
            // Card fields (SECURE: Only last 4 digits)
            paymentRow["card_last_four_digits"] = DBNull.Value;
            paymentRow["card_holder_name"] = DBNull.Value;
            paymentRow["card_transaction_number"] = DBNull.Value;
            paymentRow["card_type"] = DBNull.Value;
            // Bank transfer field
            paymentRow["bank_reference_number"] = DBNull.Value;

            paymentsTable.Rows.Add(paymentRow);
            paymentRow["payment_id"] = entryId; // Use counter as temporary ID
        }

        private void RemovePaymentEntry(int entryId)
        {
            // Find and remove the panel
            Control panelToRemove = xtraScrollableControl4.Controls[$"pnlPaymentEntry{entryId}"];
            if (panelToRemove != null)
            {
                xtraScrollableControl4.Controls.Remove(panelToRemove);
                panelToRemove.Dispose();
            }

            // Remove from payments DataTable
            DataRow[] rowsToRemove = paymentsTable.Select($"payment_id = {entryId}");
            foreach (DataRow row in rowsToRemove)
            {
                paymentsTable.Rows.Remove(row);
            }

            UpdatePaymentSummaryUI();
        }

        private void PopulatePaymentFields(PanelControl fieldsPanel, string paymentMethod, int entryId)
        {
            // Clear existing controls
            fieldsPanel.Controls.Clear();

            switch (paymentMethod.ToUpper())
            {
                case "CASH":
                    AddCashFieldsToPanel(fieldsPanel, entryId);
                    break;
                case "CARD":
                    AddCardFieldsToPanel(fieldsPanel, entryId);
                    break;
                case "BANK_TRANSFER":
                    AddBankTransferFieldsToPanel(fieldsPanel, entryId);
                    break;
                case "CREDIT":
                    AddCreditFieldsToPanel(fieldsPanel, entryId);
                    break;
            }
        }

        private void AddCashFieldsToPanel(PanelControl panel, int entryId)
        {
            // Amount Label (matching your label style)
            LabelControl lblAmount = new LabelControl
            {
                Text = "Amount (Rs.):",
                Location = new Point(10, 15),
                AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                Width = 100
            };
            lblAmount.Appearance.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            lblAmount.Appearance.Options.UseFont = true;

            // Amount TextEdit (matching txtTotal, txtGrandTotal style)
            TextEdit txtAmount = new TextEdit
            {
                Name = $"txtCashAmount{entryId}",
                Location = new Point(120, 2),
                Width = 200,
                Tag = entryId
            };
            txtAmount.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtAmount.Properties.Appearance.Options.UseFont = true;
            txtAmount.Properties.Appearance.Options.UseTextOptions = true;
            txtAmount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtAmount.Properties.Padding = new System.Windows.Forms.Padding(10);
            txtAmount.Size = new Size(200, 44);
            txtAmount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtAmount.Properties.Mask.EditMask = "n2";
            txtAmount.EditValueChanged += (s, e) =>
            {
                UpdatePaymentAmount(entryId, txtAmount.Text);
            };

            panel.Controls.Add(lblAmount);
            panel.Controls.Add(txtAmount);
        }

        private void AddCardFieldsToPanel(PanelControl panel, int entryId)
        {
            int yPos = 5;
            int fieldHeight = 44;
            int verticalSpacing = 55; // Space between rows

            // Row 1: Amount and Card Last 4
            LabelControl lblAmount = new LabelControl
            {
                Text = "Amount (Rs.):",
                Location = new Point(10, yPos + 13),
                AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                Width = 100
            };
            lblAmount.Appearance.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            lblAmount.Appearance.Options.UseFont = true;

            TextEdit txtAmount = new TextEdit
            {
                Name = $"txtCardAmount{entryId}",
                Location = new Point(115, yPos),
                Tag = entryId
            };
            txtAmount.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtAmount.Properties.Appearance.Options.UseFont = true;
            txtAmount.Properties.Appearance.Options.UseTextOptions = true;
            txtAmount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtAmount.Properties.Padding = new System.Windows.Forms.Padding(10);
            txtAmount.Size = new Size(150, fieldHeight);
            txtAmount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtAmount.Properties.Mask.EditMask = "n2";
            txtAmount.EditValueChanged += (s, e) => UpdatePaymentAmount(entryId, txtAmount.Text);

            LabelControl lblCardLast4 = new LabelControl
            {
                Text = "Last 4 Digits:",
                Location = new Point(280, yPos + 13),
                AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                Width = 90
            };
            lblCardLast4.Appearance.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            lblCardLast4.Appearance.Options.UseFont = true;

            TextEdit txtCardLast4 = new TextEdit
            {
                Name = $"txtCardLast4{entryId}",
                Location = new Point(375, yPos),
                Tag = entryId
            };
            txtCardLast4.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtCardLast4.Properties.Appearance.Options.UseFont = true;
            txtCardLast4.Properties.Padding = new System.Windows.Forms.Padding(10);
            txtCardLast4.Size = new Size(150, fieldHeight);
            txtCardLast4.Properties.MaxLength = 4;
            txtCardLast4.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtCardLast4.Properties.Mask.EditMask = "d4";
            txtCardLast4.EditValueChanged += (s, e) => UpdateCardField(entryId, "card_last_four_digits", txtCardLast4.Text);

            yPos += verticalSpacing;

            // Row 2: Card Holder
            LabelControl lblCardHolder = new LabelControl
            {
                Text = "Card Holder:",
                Location = new Point(10, yPos + 13),
                AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                Width = 100
            };
            lblCardHolder.Appearance.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            lblCardHolder.Appearance.Options.UseFont = true;

            TextEdit txtCardHolder = new TextEdit
            {
                Name = $"txtCardHolder{entryId}",
                Location = new Point(115, yPos),
                Tag = entryId
            };
            txtCardHolder.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtCardHolder.Properties.Appearance.Options.UseFont = true;
            txtCardHolder.Properties.Padding = new System.Windows.Forms.Padding(10);
            txtCardHolder.Size = new Size(410, fieldHeight);
            txtCardHolder.EditValueChanged += (s, e) => UpdateCardField(entryId, "card_holder_name", txtCardHolder.Text);

            yPos += verticalSpacing;

            // Row 3: Card Type
            LabelControl lblCardType = new LabelControl
            {
                Text = "Card Type:",
                Location = new Point(10, yPos + 13),
                AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                Width = 100
            };
            lblCardType.Appearance.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            lblCardType.Appearance.Options.UseFont = true;

            ComboBoxEdit cmbCardType = new ComboBoxEdit
            {
                Name = $"cmbCardType{entryId}",
                Location = new Point(115, yPos),
                Tag = entryId
            };
            cmbCardType.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            cmbCardType.Properties.Appearance.Options.UseFont = true;
            cmbCardType.Properties.Items.AddRange(new string[] { "Visa", "MasterCard", "Amex", "Discover", "Other" });
            cmbCardType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cmbCardType.Properties.Padding = new System.Windows.Forms.Padding(10);
            cmbCardType.Size = new Size(200, fieldHeight);
            cmbCardType.SelectedIndexChanged += (s, e) =>
            {
                if (cmbCardType.SelectedItem != null)
                {
                    UpdateCardField(entryId, "card_type", cmbCardType.SelectedItem.ToString());
                }
            };

            yPos += verticalSpacing;

            // Row 4: Transaction Number
            LabelControl lblTransNo = new LabelControl
            {
                Text = "Transaction #:",
                Location = new Point(10, yPos + 13),
                AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                Width = 100
            };
            lblTransNo.Appearance.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            lblTransNo.Appearance.Options.UseFont = true;

            TextEdit txtTransNo = new TextEdit
            {
                Name = $"txtTransNo{entryId}",
                Location = new Point(115, yPos),
                Tag = entryId
            };
            txtTransNo.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtTransNo.Properties.Appearance.Options.UseFont = true;
            txtTransNo.Properties.Padding = new System.Windows.Forms.Padding(10);
            txtTransNo.Size = new Size(300, fieldHeight);
            txtTransNo.EditValueChanged += (s, e) => UpdateCardField(entryId, "card_transaction_number", txtTransNo.Text);

            panel.Controls.Add(lblAmount);
            panel.Controls.Add(txtAmount);
            panel.Controls.Add(lblCardLast4);
            panel.Controls.Add(txtCardLast4);
            panel.Controls.Add(lblCardHolder);
            panel.Controls.Add(txtCardHolder);
            panel.Controls.Add(lblCardType);
            panel.Controls.Add(cmbCardType);
            panel.Controls.Add(lblTransNo);
            panel.Controls.Add(txtTransNo);
        }

        private void AddBankTransferFieldsToPanel(PanelControl panel, int entryId)
        {
            int yPos = 5;
            int fieldHeight = 44;
            int verticalSpacing = 55;

            // Row 1: Amount
            LabelControl lblAmount = new LabelControl
            {
                Text = "Amount (Rs.):",
                Location = new Point(10, yPos + 13),
                AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                Width = 100
            };
            lblAmount.Appearance.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            lblAmount.Appearance.Options.UseFont = true;

            TextEdit txtAmount = new TextEdit
            {
                Name = $"txtBankAmount{entryId}",
                Location = new Point(115, yPos),
                Tag = entryId
            };
            txtAmount.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtAmount.Properties.Appearance.Options.UseFont = true;
            txtAmount.Properties.Appearance.Options.UseTextOptions = true;
            txtAmount.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            txtAmount.Properties.Padding = new System.Windows.Forms.Padding(10);
            txtAmount.Size = new Size(150, fieldHeight);
            txtAmount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtAmount.Properties.Mask.EditMask = "n2";
            txtAmount.EditValueChanged += (s, e) => UpdatePaymentAmount(entryId, txtAmount.Text);

            yPos += verticalSpacing;

            // Row 2: Bank Reference
            LabelControl lblBankRef = new LabelControl
            {
                Text = "Bank Reference:",
                Location = new Point(10, yPos + 13),
                AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None,
                Width = 100
            };
            lblBankRef.Appearance.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            lblBankRef.Appearance.Options.UseFont = true;

            TextEdit txtBankRef = new TextEdit
            {
                Name = $"txtBankRef{entryId}",
                Location = new Point(115, yPos),
                Tag = entryId
            };
            txtBankRef.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtBankRef.Properties.Appearance.Options.UseFont = true;
            txtBankRef.Properties.Padding = new System.Windows.Forms.Padding(10);
            txtBankRef.Size = new Size(410, fieldHeight);
            txtBankRef.EditValueChanged += (s, e) => UpdateBankField(entryId, "bank_reference_number", txtBankRef.Text);

            panel.Controls.Add(lblAmount);
            panel.Controls.Add(txtAmount);
            panel.Controls.Add(lblBankRef);
            panel.Controls.Add(txtBankRef);
        }

        private void AddCreditFieldsToPanel(PanelControl panel, int entryId)
        {
            // Info Label (matching your label style)
            LabelControl lblInfo = new LabelControl
            {
                Text = "Credit payment will be added to customer's account.\nAmount will be set to Grand Total automatically.",
                Location = new Point(10, 15),
                AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical,
                Width = panel.Width - 20
            };
            lblInfo.Appearance.ForeColor = Color.DarkGreen;
            lblInfo.Appearance.Font = new Font("Segoe UI", 9.75F);
            lblInfo.Appearance.Options.UseForeColor = true;
            lblInfo.Appearance.Options.UseFont = true;

            panel.Controls.Add(lblInfo);

            // Auto-set amount to grand total for credit
            if (saleTable.Rows.Count > 0)
            {
                decimal grandTotal = decimal.Parse(saleTable.Rows[0]["grand_total"]?.ToString() ?? "0");
                UpdatePaymentAmount(entryId, grandTotal.ToString("F2"));
            }
        }

        private void UpdatePaymentAmount(int entryId, string amount)
        {
            DataRow[] rows = paymentsTable.Select($"payment_id = {entryId}");
            if (rows.Length > 0)
            {
                decimal amountValue = 0;
                decimal.TryParse(amount, out amountValue);
                rows[0]["amount"] = amountValue.ToString("F2");

                // Update method in payment row based on the combo box
                Control methodCombo = FindControlInScrollable($"cmbMethod{entryId}");
                if (methodCombo is ComboBoxEdit cmb && cmb.SelectedItem != null)
                {
                    rows[0]["payment_method"] = cmb.SelectedItem.ToString();
                }
            }

            UpdatePaymentSummaryUI();
        }

        private void UpdateCardField(int entryId, string fieldName, string value)
        {
            DataRow[] rows = paymentsTable.Select($"payment_id = {entryId}");
            if (rows.Length > 0)
            {
                rows[0][fieldName] = string.IsNullOrWhiteSpace(value) ? (object)DBNull.Value : value;
            }
        }

        private void UpdateBankField(int entryId, string fieldName, string value)
        {
            DataRow[] rows = paymentsTable.Select($"payment_id = {entryId}");
            if (rows.Length > 0)
            {
                rows[0][fieldName] = string.IsNullOrWhiteSpace(value) ? (object)DBNull.Value : value;
            }
        }

        private Control FindControlInScrollable(string controlName)
        {
            foreach (Control ctrl in xtraScrollableControl4.Controls)
            {
                if (ctrl.Name == controlName)
                    return ctrl;

                // Search within panels
                if (ctrl is PanelControl panel)
                {
                    foreach (Control innerCtrl in panel.Controls)
                    {
                        if (innerCtrl.Name == controlName)
                            return innerCtrl;
                    }
                }
            }
            return null;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DAL_DS_SalesTerminal ds = new DAL_DS_SalesTerminal();

            // Initialize global DataTables
            productsTable = _bllSalesTerminal.GetProducts();
            categoriesTable = _bllSalesTerminal.GetCategories();
            brandsTable = _bllSalesTerminal.GetBrands();
            staffPinTable = _bllSalesTerminal.GetStaffPin();
            customersTable = _bllSalesTerminal.GetCustomers();
            saleTable = ds.Sale;
            saleTable.Clear();

            // Clear payments table reference when cancelling
            if (paymentsTable != null) paymentsTable.Clear();

            DataRow newSaleRow = saleTable.NewRow();
            newSaleRow["sale_id"] = DBNull.Value;
            newSaleRow["store_id"] = Main.DataSetApp.Store[0].store_id;
            newSaleRow["sale_type"] = DBNull.Value;
            newSaleRow["invoice_number"] = DBNull.Value;
            newSaleRow["quotation_number"] = DBNull.Value;
            newSaleRow["customer_id"] = 1;  // Default to Walk-In Customer (customer_id = 1)
            newSaleRow["biller_id"] = Main.DataSetApp.User[0].user_id;
            newSaleRow["total_items"] = "0";
            newSaleRow["total_amount"] = "0.00";
            newSaleRow["discount_type"] = "PERCENTAGE";
            newSaleRow["discount_value"] = "0.00";
            newSaleRow["grand_total"] = "0.00";
            newSaleRow["total_paid"] = "0.00";  // NEW: Total amount paid
            newSaleRow["change_due"] = "0.00";  // NEW: Change to be given back
            newSaleRow["payment_status"] = DBNull.Value;
            newSaleRow["sale_status"] = DBNull.Value;
            newSaleRow["order_type"] = DBNull.Value;
            newSaleRow["table_number"] = DBNull.Value;
            newSaleRow["notes"] = DBNull.Value;
            newSaleRow["status"] = "A";
            newSaleRow["created_by"] = DBNull.Value;
            newSaleRow["created_date"] = DBNull.Value;
            newSaleRow["updated_by"] = DBNull.Value;
            newSaleRow["updated_date"] = DBNull.Value;
            saleTable.Rows.Add(newSaleRow);

            salesItemsTable = ds.SaleItem;
            salesItemsTable.Clear();
            if (!salesItemsTable.Columns.Contains("remove_action"))
            {
                salesItemsTable.Columns.Add("remove_action", typeof(string));
            }

            tableNosTable = ds.Table;
            tableNosTable.Clear();

            ResetUIElements();

            // Reload UI lists
            LoadCategories();
            LoadBrands();
            LoadProducts();
            LoadTableNos();

            // Bind sales items to grid
            gvTransactionSum.GridControl.DataSource = salesItemsTable;

            // Recalculate totals
            CalculateAndUpdateGrandTotal();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            pnlPM.Visible = !pnlPM.Visible;
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.BackColor = Color.IndianRed;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.BackColor = Color.DimGray;
        }

        private void btnDraft_Click(object sender, EventArgs e)
        {
            if (salesItemsTable.Rows.Count == 0)
            {
                MessageBox.Show("Cart is empty. Cannot save draft.", "Empty Cart",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int storeId = int.Parse(saleTable.Rows[0]["store_id"].ToString());
            int billerId = int.Parse(saleTable.Rows[0]["biller_id"].ToString());
            int? customerId = null;

            if (saleTable.Rows[0]["customer_id"] != DBNull.Value)
            {
                customerId = int.Parse(saleTable.Rows[0]["customer_id"].ToString());
            }

            string discountType = saleTable.Rows[0]["discount_type"]?.ToString() ?? "PERCENTAGE";
            decimal discountValue = decimal.Parse(saleTable.Rows[0]["discount_value"]?.ToString() ?? "0");
            decimal totalAmount = decimal.Parse(saleTable.Rows[0]["total_amount"]?.ToString() ?? "0");
            int totalItems = int.Parse(saleTable.Rows[0]["total_items"]?.ToString() ?? "0");
            decimal grandTotal = decimal.Parse(saleTable.Rows[0]["grand_total"]?.ToString() ?? "0");
            decimal totalPaid = decimal.Parse(saleTable.Rows[0]["total_paid"]?.ToString() ?? "0");
            decimal changeDue = decimal.Parse(saleTable.Rows[0]["change_due"]?.ToString() ?? "0");

            // Check if we are updating an existing sale
            int currentSaleId = 0;
            if (saleTable.Rows[0]["sale_id"] != DBNull.Value)
            {
                int.TryParse(saleTable.Rows[0]["sale_id"].ToString(), out currentSaleId);
            }

            string orderType = null;
            string tableNumber = null;

            if (pnlKOT.Visible)
            {
                if (btnDineIn.Appearance.BackColor == Color.FromArgb(4, 181, 152))
                {
                    orderType = "DINE_IN";
                    tableNumber = cmbTableNo.SelectedItem?.ToString();
                }
                else if (btnTakeAway.Appearance.BackColor == Color.FromArgb(4, 181, 152))
                {
                    orderType = "TAKE_AWAY";
                }
            }

            string notes = $"Draft saved on {DateTime.Now:yyyy-MM-dd HH:mm:ss}";

            // Save to database using unified SaveSale method
            // The database will auto-generate the sale_id
            int saleId = _bllSalesTerminal.SaveSale(storeId, billerId, customerId, "DRAFT",
                discountType, discountValue, totalAmount, totalItems, grandTotal, notes,
                salesItemsTable, totalPaid, changeDue, null, null, orderType, tableNumber, currentSaleId);

            // Update saleTable with the returned sale_id from database
            DataRow saleRow = saleTable.Rows[0];
            saleRow["sale_id"] = saleId;
            saleRow["sale_type"] = "DRAFT";
            saleRow["customer_id"] = customerId ?? (object)DBNull.Value;
            saleRow["payment_status"] = "PENDING";
            saleRow["sale_status"] = "COMPLETED";
            saleRow["order_type"] = orderType ?? (object)DBNull.Value;
            saleRow["table_number"] = tableNumber ?? (object)DBNull.Value;
            saleRow["notes"] = notes;
            saleRow["created_by"] = billerId;
            saleRow["created_date"] = DateTime.Now;
            saleRow["total_paid"] = totalPaid.ToString("F2");
            saleRow["change_due"] = changeDue.ToString("F2");

            // Generate simple success message
            MessageBox.Show($"Draft saved successfully!\nDraft ID: {saleId}", "Draft Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnCancel_Click(null, null);
        }

        private void btnQuotation_Click(object sender, EventArgs e)
        {
            if (salesItemsTable.Rows.Count == 0)
            {
                MessageBox.Show("Cart is empty. Cannot save quotation.", "Empty Cart",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int storeId = int.Parse(saleTable.Rows[0]["store_id"].ToString());
            int billerId = int.Parse(saleTable.Rows[0]["biller_id"].ToString());
            int? customerId = null;

            if (saleTable.Rows[0]["customer_id"] != DBNull.Value)
            {
                customerId = int.Parse(saleTable.Rows[0]["customer_id"].ToString());
            }

            string discountType = saleTable.Rows[0]["discount_type"]?.ToString() ?? "PERCENTAGE";
            decimal discountValue = decimal.Parse(saleTable.Rows[0]["discount_value"]?.ToString() ?? "0");
            decimal totalAmount = decimal.Parse(saleTable.Rows[0]["total_amount"]?.ToString() ?? "0");
            int totalItems = int.Parse(saleTable.Rows[0]["total_items"]?.ToString() ?? "0");
            decimal grandTotal = decimal.Parse(saleTable.Rows[0]["grand_total"]?.ToString() ?? "0");
            decimal totalPaid = decimal.Parse(saleTable.Rows[0]["total_paid"]?.ToString() ?? "0");
            decimal changeDue = decimal.Parse(saleTable.Rows[0]["change_due"]?.ToString() ?? "0");

            // Check if we are updating an existing sale
            int currentSaleId = 0;
            if (saleTable.Rows[0]["sale_id"] != DBNull.Value)
            {
                int.TryParse(saleTable.Rows[0]["sale_id"].ToString(), out currentSaleId);
            }

            // Get quotation number from database sequence
            // Note: If updating, we keep the same quotation number ideally, but GetNextQuotationNumber generates a new one.
            // For now, if it's an update, let's keep the existing one if possible, or generate new one.
            // The current UI flow doesn't store quotation number in `saleTable` easily accessible for reuse without reading it.
            // Let's assume we generate a new number for simplicity or check if `quotation_number` column exists.
            string quotationNumber;
            if (currentSaleId > 0 && saleTable.Columns.Contains("quotation_number") && saleTable.Rows[0]["quotation_number"] != DBNull.Value)
            {
                quotationNumber = saleTable.Rows[0]["quotation_number"].ToString();
            }
            else
            {
                quotationNumber = _bllSalesTerminal.GetNextQuotationNumber();
            }

            string notes = $"Quotation created on {DateTime.Now:yyyy-MM-dd HH:mm:ss}";

            // Save to database using unified SaveSale method
            // The database will auto-generate the sale_id
            int saleId = _bllSalesTerminal.SaveSale(storeId, billerId, customerId, "QUOTATION",
                discountType, discountValue, totalAmount, totalItems, grandTotal, notes,
                salesItemsTable, totalPaid, changeDue, null, quotationNumber, null, null, currentSaleId);

            // Update saleTable with the returned sale_id from database
            DataRow saleRow = saleTable.Rows[0];
            saleRow["sale_id"] = saleId;
            saleRow["sale_type"] = "QUOTATION";
            saleRow["quotation_number"] = quotationNumber;
            saleRow["customer_id"] = customerId ?? (object)DBNull.Value;
            saleRow["payment_status"] = "PENDING";
            saleRow["sale_status"] = "COMPLETED";
            saleRow["notes"] = notes;
            saleRow["created_by"] = billerId;
            saleRow["created_date"] = DateTime.Now;
            saleRow["total_paid"] = totalPaid.ToString("F2");
            saleRow["change_due"] = changeDue.ToString("F2");

            // Get store details (email, phone, address) from Main.DataSetApp
            string storeEmail = "";
            string storePhone = "";
            string storeAddress = "";

            if (Main.DataSetApp.Store.Rows.Count > 0)
            {
                var storeRow = Main.DataSetApp.Store[0];
                storeEmail = storeRow.IsemailNull() ? "" : storeRow.email;
                storePhone = storeRow.IsphoneNull() ? "" : storeRow.phone;
                storeAddress = storeRow.IsaddressNull() ? "" : storeRow.address;
            }

            // Get customer details
            string customerName = "Walk-In Customer";
            string customerPhone = "";

            if (customerId.HasValue && customerId.Value > 1) // Skip Walk-In Customer (id = 1)
            {
                DataRow[] customerRows = customersTable.Select($"customer_id = '{customerId}'");
                if (customerRows.Length > 0)
                {
                    DataRow customerRow = customerRows[0];
                    customerName = customerRow["full_name"]?.ToString() ?? "Walk-In Customer";
                    customerPhone = customerRow["phone"]?.ToString() ?? "";
                }
            }

            // Calculate discount amount
            decimal discountAmount = 0;
            if (discountType == "PERCENTAGE")
            {
                discountAmount = totalAmount * discountValue / 100m;
            }
            else if (discountType == "FIXED_AMOUNT")
            {
                discountAmount = discountValue;
            }

            // Create and configure the report
            POS.PAL.REPORT.Quotation quotationReport = new POS.PAL.REPORT.Quotation();

            // Set datasource for detail items (SaleItem)
            quotationReport.DataSource = salesItemsTable;

            // Set all parameters
            quotationReport.Parameters["p_date"].Value = DateTime.Now.ToString("yyyy-MM-dd");
            quotationReport.Parameters["p_email"].Value = storeEmail;
            quotationReport.Parameters["p_contact"].Value = storePhone;
            quotationReport.Parameters["p_address"].Value = storeAddress;
            quotationReport.Parameters["p_quotation_no"].Value = quotationNumber;
            quotationReport.Parameters["p_customer_name"].Value = customerName;
            quotationReport.Parameters["p_customer_phone"].Value = customerPhone;
            quotationReport.Parameters["p_total"].Value = totalAmount.ToString("F2");
            quotationReport.Parameters["p_discount"].Value = discountAmount.ToString("F2");
            quotationReport.Parameters["p_grand_total"].Value = grandTotal.ToString("F2");

            // Show the report preview
            DevExpress.XtraReports.UI.ReportPrintTool printTool = new DevExpress.XtraReports.UI.ReportPrintTool(quotationReport);
            printTool.ShowPreview();

            // Show success message
            MessageBox.Show($"Quotation #{quotationNumber} created successfully!", "Quotation Created",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnCancel_Click(null, null);
        }

        private void btnCreditSale_Click(object sender, EventArgs e)
        {
            if (salesItemsTable.Rows.Count == 0)
            {
                MessageBox.Show("Cart is empty. Cannot save credit sale.", "Empty Cart",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int storeId = int.Parse(saleTable.Rows[0]["store_id"].ToString());
            int billerId = int.Parse(saleTable.Rows[0]["biller_id"].ToString());
            int customerId = 0;

            // Get customer ID from saleTable
            if (saleTable.Rows[0]["customer_id"] != DBNull.Value)
            {
                customerId = int.Parse(saleTable.Rows[0]["customer_id"].ToString());
            }

            // Validation: Credit sale not allowed for Walk-In Customer (customer_id = 1)
            if (customerId == 1 || customerId == 0)
            {
                MessageBox.Show("Credit sale is not allowed for Walk-In Customer. Please select a registered customer.",
                    "Invalid Customer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Find customer details in customersTable
            DataRow[] customerRows = customersTable.Select($"customer_id = '{customerId}'");
            if (customerRows.Length == 0)
            {
                MessageBox.Show("Customer information not found.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataRow selectedCustomer = customerRows[0];

            // Check if customer has credit limit
            decimal creditLimit = 0;
            decimal creditBalance = 0;

            if (selectedCustomer.Table.Columns.Contains("credit_limit") &&
                selectedCustomer["credit_limit"] != DBNull.Value &&
                !string.IsNullOrWhiteSpace(selectedCustomer["credit_limit"]?.ToString()))
            {
                if (decimal.TryParse(selectedCustomer["credit_limit"].ToString(), out decimal parsedCreditLimit))
                {
                    creditLimit = parsedCreditLimit;
                }
            }

            if (selectedCustomer.Table.Columns.Contains("credit_balance") &&
                selectedCustomer["credit_balance"] != DBNull.Value &&
                !string.IsNullOrWhiteSpace(selectedCustomer["credit_balance"]?.ToString()))
            {
                if (decimal.TryParse(selectedCustomer["credit_balance"].ToString(), out decimal parsedCreditBalance))
                {
                    creditBalance = parsedCreditBalance;
                }
            }

            // Validation: Credit limit must be set
            if (creditLimit <= 0)
            {
                MessageBox.Show($"Customer '{selectedCustomer["full_name"]}' does not have a credit limit set. Please update customer credit limit.",
                    "No Credit Limit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get grand total
            decimal grandTotal = 0;
            if (decimal.TryParse(saleTable.Rows[0]["grand_total"]?.ToString(), out decimal parsedGrandTotal))
            {
                grandTotal = parsedGrandTotal;
            }

            // Validation: Grand total must not exceed available credit
            decimal availableCredit = creditLimit - creditBalance;
            if (grandTotal > availableCredit)
            {
                MessageBox.Show(
                    $"Grand Total ({grandTotal:F2}) exceeds available credit.\n\n" +
                    $"Credit Limit: {creditLimit:F2}\n" +
                    $"Current Balance: {creditBalance:F2}\n" +
                    $"Available Credit: {availableCredit:F2}",
                    "Credit Limit Exceeded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get sale data
            string discountType = saleTable.Rows[0]["discount_type"]?.ToString() ?? "PERCENTAGE";
            decimal discountValue = decimal.Parse(saleTable.Rows[0]["discount_value"]?.ToString() ?? "0");
            decimal totalAmount = decimal.Parse(saleTable.Rows[0]["total_amount"]?.ToString() ?? "0");
            int totalItems = int.Parse(saleTable.Rows[0]["total_items"]?.ToString() ?? "0");

            // Check if we are updating an existing sale
            int currentSaleId = 0;
            if (saleTable.Rows[0]["sale_id"] != DBNull.Value)
            {
                int.TryParse(saleTable.Rows[0]["sale_id"].ToString(), out currentSaleId);
            }

            // Get invoice number from database sequence
            string invoiceNumber = _bllSalesTerminal.GetNextInvoiceNumber();
            string notes = $"Credit sale created on {DateTime.Now:yyyy-MM-dd HH:mm:ss}\nCredit Limit: {creditLimit:F2}";

            // Save sale to database using unified SaveSale method
            int saleId = _bllSalesTerminal.SaveSale(storeId, billerId, customerId, "CREDIT_SALE",
                discountType, discountValue, totalAmount, totalItems, grandTotal, notes,
                salesItemsTable, 0m, 0m, invoiceNumber, null, null, null, currentSaleId);

                // Create CREDIT payment record
                DataTable creditPaymentTable = new DataTable();
                creditPaymentTable.Columns.Add("payment_method", typeof(string));
                creditPaymentTable.Columns.Add("amount", typeof(decimal));
                creditPaymentTable.Columns.Add("payment_date", typeof(DateTime));

                DataRow creditPayment = creditPaymentTable.NewRow();
                creditPayment["payment_method"] = "CREDIT";
                creditPayment["amount"] = grandTotal;
                creditPayment["payment_date"] = DateTime.Now;
                creditPaymentTable.Rows.Add(creditPayment);

                // Save CREDIT payment to database
                _bllSalesTerminal.SavePayments(saleId, creditPaymentTable, billerId);

                // Update saleTable with the returned sale_id from database
                DataRow saleRow = saleTable.Rows[0];
                saleRow["sale_id"] = saleId;
                saleRow["sale_type"] = "CREDIT_SALE";
                saleRow["invoice_number"] = invoiceNumber;
                saleRow["customer_id"] = customerId;
                saleRow["payment_status"] = "CREDIT";
                saleRow["sale_status"] = "COMPLETED";
                saleRow["notes"] = notes;
                saleRow["created_by"] = billerId;
                saleRow["created_date"] = DateTime.Now;
                saleRow["total_paid"] = "0.00";
                saleRow["change_due"] = "0.00";

                // Generate Invoice Report
                REPORT.Invoice invoiceReport = new REPORT.Invoice();

                // Bind main report data (sale items)
                invoiceReport.DataSource = salesItemsTable;

                // Set main invoice parameters
                invoiceReport.Parameters["p_invoice_no"].Value = invoiceNumber;
                invoiceReport.Parameters["p_date"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                invoiceReport.Parameters["p_total"].Value = totalAmount.ToString("F2");
                invoiceReport.Parameters["p_discount"].Value = discountValue.ToString("F2");
                invoiceReport.Parameters["p_grand_total"].Value = grandTotal.ToString("F2");

                // Get customer details
                string customerName = selectedCustomer["full_name"]?.ToString() ?? "Customer";
                string customerEmail = selectedCustomer["email"]?.ToString() ?? "";
                string customerContact = selectedCustomer["phone"]?.ToString() ?? "";
                string customerAddress = selectedCustomer["address"]?.ToString() ?? "";

                invoiceReport.Parameters["p_customer_name"].Value = customerName;
                invoiceReport.Parameters["p_email"].Value = customerEmail;
                invoiceReport.Parameters["p_contact"].Value = customerContact;
                invoiceReport.Parameters["p_address"].Value = customerAddress;

                // Configure subreport for payments
                if (invoiceReport.PaymentSubreport?.ReportSource is REPORT.SUBREPORT.SR_Payment subreport)
                {
                    subreport.DataSource = creditPaymentTable;
                    subreport.Parameters["p_total_paid"].Value = "0.00";
                    subreport.Parameters["p_due"].Value = grandTotal.ToString("F2");
                }

                // Print invoice based on system settings
                bool enableThermal = Main.GetSetting("ENABLE_THERMAL_PRINT", "False").Equals("True", StringComparison.OrdinalIgnoreCase);
                bool enableA4 = Main.GetSetting("ENABLE_A4_PRINT", "True").Equals("True", StringComparison.OrdinalIgnoreCase);
                bool autoPrint = true; // Enforce auto print by default

                // Print thermal invoice if enabled
                if (enableThermal)
                {
                    PrintThermalInvoice(invoiceNumber, grandTotal, customerName, salesItemsTable, creditPaymentTable, autoPrint);
                }
                // Print A4 invoice if enabled (Mutually exclusive)
                else if (enableA4)
                {
                    DevExpress.XtraReports.UI.ReportPrintTool printTool = new DevExpress.XtraReports.UI.ReportPrintTool(invoiceReport);
                    printTool.Print();
                }

                // Success message
                MessageBox.Show(
                    $"Credit Sale Created Successfully!\n\n" +
                    $"Invoice Number: {invoiceNumber}\n" +
                    $"Customer: {customerName}\n" +
                    $"Grand Total: {grandTotal:F2}\n" +
                    $"Credit Limit: {creditLimit:F2}\n" +
                    $"Remaining Credit: {Math.Max(0, availableCredit - grandTotal):F2}\n" +
                    $"Due: {grandTotal:F2}",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset UI
                btnCancel_Click(null, null);
        }



        /// <summary>
        /// Calculates total paid and due amounts from payments table
        /// </summary>
        private (decimal totalPaid, decimal due) CalculatePaymentTotals()
        {
            if (paymentsTable == null || paymentsTable.Rows.Count == 0)
                return (0m, 0m);

            decimal totalPaid = 0m;
            decimal grandTotal = 0m;

            // Calculate total paid (excluding CREDIT payments)
            foreach (DataRow payment in paymentsTable.Rows)
            {
                string paymentMethod = payment["payment_method"]?.ToString();
                if (string.IsNullOrEmpty(paymentMethod))
                    continue;

                if (decimal.TryParse(payment["amount"]?.ToString(), out decimal amount))
                {
                    // CREDIT is not counted as "paid" - it's still owed
                    if (paymentMethod != "CREDIT")
                    {
                        totalPaid += amount;
                    }
                }
            }

            // Get grand total from sale table
            if (saleTable.Rows.Count > 0)
            {
                if (decimal.TryParse(saleTable.Rows[0]["grand_total"]?.ToString(), out decimal parsedGrandTotal))
                {
                    grandTotal = parsedGrandTotal;
                }
            }

            decimal due = Math.Max(0, grandTotal - totalPaid);

            return (totalPaid, due);
        }

        private void UpdatePaymentSummaryUI()
        {
            var (totalPaid, due) = CalculatePaymentTotals();
            if (lblTotalPaid != null) lblTotalPaid.Text = $"Total Paid: Rs. {totalPaid:N2}";
            if (lblBalance != null) lblBalance.Text = $"Balance: Rs. {due:N2}";
        }

        /// <summary>
        /// Print thermal invoice (80mm receipt)
        /// </summary>
        private void PrintThermalInvoice(string invoiceNumber, decimal grandTotal, string customerName, DataTable itemsTable, DataTable paymentsTable, bool autoPrint)
        {
            // Create thermal invoice report
            ThermalInvoice thermalInvoice = new ThermalInvoice();

                // Set main invoice parameters
                thermalInvoice.Parameters["p_invoice_no"].Value = invoiceNumber;
                thermalInvoice.Parameters["p_date"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                thermalInvoice.Parameters["p_customer_name"].Value = customerName;
                thermalInvoice.Parameters["p_grand_total"].Value = grandTotal.ToString("F2");

                // Get store details from Main.DataSetApp
                if (Main.DataSetApp.Store.Rows.Count > 0)
                {
                    var storeRow = Main.DataSetApp.Store[0];
                    thermalInvoice.Parameters["p_contact"].Value = storeRow.IsphoneNull() ? "" : storeRow.phone;
                    thermalInvoice.Parameters["p_email"].Value = storeRow.IsemailNull() ? "" : storeRow.email;
                    thermalInvoice.Parameters["p_address"].Value = storeRow.IsaddressNull() ? "" : storeRow.address;
                }

                // Set data source for items
                thermalInvoice.DataSource = itemsTable;

                // Calculate totals from itemsTable
                decimal subtotal = 0m;
                foreach (DataRow item in itemsTable.Rows)
                {
                    if (decimal.TryParse(item["subtotal"]?.ToString(), out decimal itemTotal))
                    {
                        subtotal += itemTotal;
                    }
                }

                // Get discount from sale table
                decimal discountAmount = 0m;
                if (saleTable.Rows.Count > 0)
                {
                    DataRow saleRow = saleTable.Rows[0];
                    if (decimal.TryParse(saleRow["discount_value"]?.ToString(), out decimal discountValue))
                    {
                        string discountType = saleRow["discount_type"]?.ToString();
                        if (discountType == "PERCENTAGE")
                        {
                            discountAmount = subtotal * (discountValue / 100m);
                        }
                        else
                        {
                            discountAmount = discountValue;
                        }
                    }
                }

                thermalInvoice.Parameters["p_total"].Value = subtotal.ToString("F2");
                thermalInvoice.Parameters["p_discount"].Value = "-" + discountAmount.ToString("F2");

                // Bind payment subreport (thermal version)
                if (thermalInvoice.PaymentSubreport != null)
                {
                    SR_ThermalPayment paymentReport = new SR_ThermalPayment();
                    paymentReport.DataSource = paymentsTable;

                    // Calculate payment totals
                    var (totalPaid, due) = CalculatePaymentTotals();
                    decimal change = Math.Max(0, totalPaid - grandTotal);
                    
                    paymentReport.Parameters["p_total_paid"].Value = totalPaid.ToString("F2");
                    paymentReport.Parameters["p_due"].Value = due.ToString("F2");
                    paymentReport.Parameters["p_change"].Value = change.ToString("F2");

                    thermalInvoice.PaymentSubreport.ReportSource = paymentReport;
                }

                // Get thermal printer name from system settings
                string printerName = Main.GetSetting("thermal_printer_name", null);

                // Configure printer if specified
                if (!string.IsNullOrEmpty(printerName))
                {
                    thermalInvoice.PrinterName = printerName;
                }

            // Print based on autoPrint setting
            if (autoPrint)
                thermalInvoice.Print();
            else
                new DevExpress.XtraReports.UI.ReportPrintTool(thermalInvoice).ShowPreview();
        }

        /// <summary>
        /// Validates that payments cover the grand total
        /// </summary>
        private bool ValidatePayments(out string errorMessage)
        {
            errorMessage = string.Empty;

            if (paymentsTable == null || paymentsTable.Rows.Count == 0)
            {
                errorMessage = "No payment methods selected. Please add at least one payment method.";
                return false;
            }

            decimal grandTotal = 0m;
            if (saleTable.Rows.Count > 0)
            {
                decimal.TryParse(saleTable.Rows[0]["grand_total"]?.ToString(), out grandTotal);
            }

            if (grandTotal <= 0)
            {
                errorMessage = "Grand total must be greater than zero.";
                return false;
            }

            // Calculate total payment amount
            decimal totalPaymentAmount = 0m;
            foreach (DataRow payment in paymentsTable.Rows)
            {
                string paymentMethod = payment["payment_method"]?.ToString();
                if (string.IsNullOrEmpty(paymentMethod))
                    continue;

                if (decimal.TryParse(payment["amount"]?.ToString(), out decimal amount))
                {
                    totalPaymentAmount += amount;
                }
            }

            if (totalPaymentAmount < grandTotal)
            {
                decimal remainder = grandTotal - totalPaymentAmount;

                // Check if customer is selected (not Walk-In)
                int? customerId = null;
                if (saleTable.Rows.Count > 0 && saleTable.Rows[0]["customer_id"] != DBNull.Value)
                {
                    customerId = int.Parse(saleTable.Rows[0]["customer_id"].ToString());
                }

                if (customerId == null || customerId == 1)
                {
                    errorMessage = "Walk-In customers cannot have credit/partial payment. Please select a registered customer.";
                    return false;
                }

                // Check credit limit
                if (customersTable != null)
                {
                    DataRow[] customerRows = customersTable.Select($"customer_id = '{customerId}'");
                    if (customerRows.Length > 0)
                    {
                        DataRow customer = customerRows[0];
                        decimal creditLimit = 0m;
                        decimal creditBalance = 0m;

                        decimal.TryParse(customer["credit_limit"]?.ToString(), out creditLimit);
                        decimal.TryParse(customer["credit_balance"]?.ToString(), out creditBalance);

                        decimal availableCredit = creditLimit - creditBalance;

                        if (remainder > availableCredit)
                        {
                            errorMessage = $"Credit limit exceeded.\n\nRequired: {remainder:F2}\nAvailable Credit: {availableCredit:F2}";
                            return false;
                        }
                    }
                }
            }

            // Allow overpayment (change due)
            if (totalPaymentAmount > grandTotal)
            {
                // This is valid, change will be calculated
            }

            return true;
        }

        private void btnPMComplete_Click(object sender, EventArgs e)
        {
            // Validation: Check if cart has items
            if (salesItemsTable.Rows.Count == 0)
            {
                MessageBox.Show("Cart is empty. Cannot create sale.", "Empty Cart",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validation: Check payments
            if (!ValidatePayments(out string errorMessage))
            {
                // If validation fails (e.g. no payments), show error
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    MessageBox.Show(errorMessage, "Payment Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Get sale data
            int storeId = int.Parse(saleTable.Rows[0]["store_id"].ToString());
            int billerId = int.Parse(saleTable.Rows[0]["biller_id"].ToString());
            int? customerId = null;

            if (saleTable.Rows[0]["customer_id"] != DBNull.Value)
            {
                customerId = int.Parse(saleTable.Rows[0]["customer_id"].ToString());
            }

            string discountType = saleTable.Rows[0]["discount_type"]?.ToString() ?? "PERCENTAGE";
            decimal discountValue = decimal.Parse(saleTable.Rows[0]["discount_value"]?.ToString() ?? "0");
            decimal totalAmount = decimal.Parse(saleTable.Rows[0]["total_amount"]?.ToString() ?? "0");
            int totalItems = int.Parse(saleTable.Rows[0]["total_items"]?.ToString() ?? "0");
            decimal grandTotal = decimal.Parse(saleTable.Rows[0]["grand_total"]?.ToString() ?? "0");

            // Calculate payment totals
            var (totalPaid, due) = CalculatePaymentTotals();

            // If there is a due amount, add it as a CREDIT payment
            if (due > 0)
            {
                DataRow creditPayment = paymentsTable.NewRow();
                creditPayment["payment_method"] = "CREDIT";
                creditPayment["amount"] = due;
                creditPayment["payment_date"] = DateTime.Now;
                paymentsTable.Rows.Add(creditPayment);
            }

            decimal changeDue = Math.Max(0, totalPaid - grandTotal);

            // Get order details (if KOT enabled)
            string orderType = null;
            string tableNumber = null;

            if (pnlKOT.Visible)
            {
                if (btnDineIn.Appearance.BackColor == Color.FromArgb(4, 181, 152))
                {
                    orderType = "DINE_IN";
                    tableNumber = cmbTableNo.SelectedItem?.ToString();

                    if (string.IsNullOrEmpty(tableNumber))
                    {
                        MessageBox.Show("Please select a table number for Dine-In order.", "Table Required",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (btnTakeAway.Appearance.BackColor == Color.FromArgb(4, 181, 152))
                {
                    orderType = "TAKE_AWAY";
                }
                else
                {
                    MessageBox.Show("Please select an order type (Dine-In or Take-Away).", "Order Type Required",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Check if we are updating an existing sale
            int currentSaleId = 0;
            if (saleTable.Rows[0]["sale_id"] != DBNull.Value)
            {
                int.TryParse(saleTable.Rows[0]["sale_id"].ToString(), out currentSaleId);
            }

            // Get invoice number from database sequence
            // Note: If updating from DRAFT/QUOTATION to SALE, we need a NEW invoice number.
            // If updating an existing SALE (adding payments), we might want to keep it?
            // Usually invoice number is generated when converting to SALE.
            // If currentSaleType is already SALE, we keep it. If not, we generate new.
            string invoiceNumber;
            string currentSaleType = saleTable.Rows[0]["sale_type"]?.ToString();

            if (currentSaleId > 0 && currentSaleType == "SALE" && saleTable.Columns.Contains("invoice_number") && saleTable.Rows[0]["invoice_number"] != DBNull.Value)
            {
                invoiceNumber = saleTable.Rows[0]["invoice_number"].ToString();
            }
            else
            {
                invoiceNumber = _bllSalesTerminal.GetNextInvoiceNumber();
            }

            string notes = $"Invoice created on {DateTime.Now:yyyy-MM-dd HH:mm:ss}";

            // Save sale to database
            int saleId = _bllSalesTerminal.SaveSale(storeId, billerId, customerId, "SALE",
                discountType, discountValue, totalAmount, totalItems, grandTotal, notes,
                salesItemsTable, totalPaid, changeDue, invoiceNumber, null, orderType, tableNumber, currentSaleId);

                // Save payments to database
                _bllSalesTerminal.SavePayments(saleId, paymentsTable, billerId);

                // Update saleTable with the returned sale_id from database
                DataRow saleRow = saleTable.Rows[0];
                saleRow["sale_id"] = saleId;
                saleRow["sale_type"] = "SALE";
                saleRow["invoice_number"] = invoiceNumber;
                saleRow["customer_id"] = customerId ?? (object)DBNull.Value;

                // Determine payment status
                string paymentStatus;
                if (due == 0)
                    paymentStatus = "PAID";
                else if (due > 0 && due < grandTotal)
                    paymentStatus = "PARTIAL";
                else
                    paymentStatus = "CREDIT";

                saleRow["payment_status"] = paymentStatus;
                saleRow["sale_status"] = "COMPLETED";
                saleRow["order_type"] = orderType ?? (object)DBNull.Value;
                saleRow["table_number"] = tableNumber ?? (object)DBNull.Value;
                saleRow["notes"] = notes;
                saleRow["created_by"] = billerId;
                saleRow["created_date"] = DateTime.Now;
                saleRow["total_paid"] = totalPaid.ToString("F2");
                saleRow["change_due"] = changeDue.ToString("F2");

                // Generate Invoice Report
                REPORT.Invoice invoiceReport = new REPORT.Invoice();

                // Bind main report data (sale items)
                invoiceReport.DataSource = salesItemsTable;

                // Set main invoice parameters
                invoiceReport.Parameters["p_invoice_no"].Value = invoiceNumber;
                invoiceReport.Parameters["p_date"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                invoiceReport.Parameters["p_total"].Value = totalAmount.ToString("F2");
                invoiceReport.Parameters["p_discount"].Value = discountValue.ToString("F2");
                invoiceReport.Parameters["p_grand_total"].Value = grandTotal.ToString("F2");

                // Get customer details
                string customerName = "Walk-In Customer";
                string customerEmail = "";
                string customerContact = "";
                string customerAddress = "";

                if (customerId.HasValue && customerId.Value != 1)
                {
                    DataRow[] customerRows = customersTable.Select($"customer_id = '{customerId.Value}'");
                    if (customerRows.Length > 0)
                    {
                        DataRow customer = customerRows[0];
                        customerName = customer["full_name"]?.ToString() ?? "Walk-In Customer";
                        customerEmail = customer["email"]?.ToString() ?? "";
                        customerContact = customer["phone"]?.ToString() ?? "";
                        customerAddress = customer["address"]?.ToString() ?? "";
                    }
                }

                invoiceReport.Parameters["p_customer_name"].Value = customerName;
                invoiceReport.Parameters["p_email"].Value = customerEmail;
                invoiceReport.Parameters["p_contact"].Value = customerContact;
                invoiceReport.Parameters["p_address"].Value = customerAddress;

                // Configure subreport for payments
                if (invoiceReport.PaymentSubreport?.ReportSource is REPORT.SUBREPORT.SR_Payment subreport)
                {
                    subreport.DataSource = paymentsTable;
                    subreport.Parameters["p_total_paid"].Value = totalPaid.ToString("F2");
                    subreport.Parameters["p_due"].Value = due.ToString("F2");
                }

                // Print invoice based on system settings
                bool enableThermal = Main.GetSetting("ENABLE_THERMAL_PRINT", "False").Equals("True", StringComparison.OrdinalIgnoreCase);
                bool enableA4 = Main.GetSetting("ENABLE_A4_PRINT", "True").Equals("True", StringComparison.OrdinalIgnoreCase);
                bool autoPrint = true; // Enforce auto print by default

                // Print thermal invoice if enabled
                if (enableThermal)
                {
                    PrintThermalInvoice(invoiceNumber, grandTotal, customerName, salesItemsTable, paymentsTable, autoPrint);
                }
                // Print A4 invoice if enabled (Mutually exclusive)
                else if (enableA4)
                {
                    DevExpress.XtraReports.UI.ReportPrintTool printTool = new DevExpress.XtraReports.UI.ReportPrintTool(invoiceReport);
                    printTool.Print();
                }

                // Success message
                MessageBox.Show(
                    $"Invoice Created Successfully!\n\n" +
                    $"Invoice Number: {invoiceNumber}\n" +
                    $"Customer: {customerName}\n" +
                    $"Grand Total: {grandTotal:F2}\n" +
                    $"Total Paid: {totalPaid:F2}\n" +
                    $"Due: {due:F2}\n" +
                    $"Payment Status: {paymentStatus}",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset UI
                btnCancel_Click(null, null);
        }


    }
}
        private void btnLoadSaved_Click(object sender, EventArgs e)
        {
            // Initialize panel visibility and grids
            pnlLoadSaved.Visible = true;
            pnlLoadSaved.BringToFront();

            // Load data for each tab
            LoadDraftsList();
            LoadQuotationsList();
            LoadSalesList();
        }

        private void btnCloseLoadSaved_Click(object sender, EventArgs e)
        {
            pnlLoadSaved.Visible = false;
        }

        private void LoadDraftsList()
        {
            try
            {
                DataTable drafts = _bllSalesTerminal.GetSales("DRAFT");
                gcDrafts.DataSource = drafts;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading drafts: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadQuotationsList()
        {
            try
            {
                DataTable quotations = _bllSalesTerminal.GetSales("QUOTATION");
                gcQuotations.DataSource = quotations;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading quotations: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSalesList()
        {
            try
            {
                // We want to load recent sales. The GetSales method in BLL takes a type.
                // Assuming "SALE" or "CREDIT_SALE" are the types.
                // The GetSales method in DAL filters by type.
                // Let's load standard SALE for now.
                DataTable sales = _bllSalesTerminal.GetSales("SALE");
                // Optionally merge CREDIT_SALE if needed, or if GetSales("SALE") covers both if logic allows.
                // Based on DAL: WHERE sale_type = @saleType.
                // Let's stick to 'SALE' as primary.
                gcSales.DataSource = sales;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sales: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSelectedSale(object sender, EventArgs e)
        {
            // Determine which grid triggered the event
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null || view.FocusedRowHandle < 0) return;

            string saleIdStr = view.GetFocusedRowCellValue("sale_id")?.ToString();
            if (int.TryParse(saleIdStr, out int saleId))
            {
                LoadSaleIntoTerminal(saleId);
                pnlLoadSaved.Visible = false;
            }
        }

        private void LoadSaleIntoTerminal(int saleId)
        {
            try
            {
                // 1. Reset current terminal
                // Use btnCancel logic but don't call it directly to avoid UI flicker if possible,
                // but for safety let's call it to clear state.
                btnCancel_Click(null, null);

                // 2. Fetch Data
                DataTable fetchedSale = _bllSalesTerminal.GetSale(saleId);
                DataTable fetchedItems = _bllSalesTerminal.GetSaleItems(saleId);
                DataTable fetchedPayments = _bllSalesTerminal.GetSalePayments(saleId);

                if (fetchedSale.Rows.Count == 0)
                {
                    MessageBox.Show("Sale record not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataRow saleRow = fetchedSale.Rows[0];

                // 3. Populate saleTable (The main header data source)
                DataRow currentSaleRow = saleTable.Rows[0];
                currentSaleRow["sale_id"] = saleRow["sale_id"];
                currentSaleRow["store_id"] = saleRow["store_id"];
                currentSaleRow["sale_type"] = saleRow["sale_type"];
                currentSaleRow["invoice_number"] = saleRow["invoice_number"];
                currentSaleRow["quotation_number"] = saleRow["quotation_number"];
                currentSaleRow["customer_id"] = saleRow["customer_id"];
                currentSaleRow["biller_id"] = saleRow["biller_id"];
                currentSaleRow["total_items"] = saleRow["total_items"];
                currentSaleRow["total_amount"] = saleRow["total_amount"];
                currentSaleRow["discount_type"] = saleRow["discount_type"];
                currentSaleRow["discount_value"] = saleRow["discount_value"];
                currentSaleRow["grand_total"] = saleRow["grand_total"];
                currentSaleRow["total_paid"] = saleRow["total_paid"];
                currentSaleRow["change_due"] = saleRow["change_due"];
                currentSaleRow["payment_status"] = saleRow["payment_status"];
                currentSaleRow["sale_status"] = saleRow["sale_status"];
                currentSaleRow["order_type"] = saleRow["order_type"];
                currentSaleRow["table_number"] = saleRow["table_number"];
                currentSaleRow["notes"] = saleRow["notes"];
                // Keep created_by/date as is or update? Usually we keep original for history.

                // 4. Populate Items
                salesItemsTable.Clear();
                foreach (DataRow item in fetchedItems.Rows)
                {
                    DataRow newItem = salesItemsTable.NewRow();
                    newItem["sale_item_id"] = item["sale_item_id"];
                    newItem["sale_id"] = item["sale_id"];
                    newItem["product_id"] = item["product_id"];
                    newItem["product_name"] = item["product_name"];
                    newItem["product_code"] = item["product_code"];
                    newItem["unit"] = item["unit"];
                    newItem["unit_price"] = item["unit_price"];
                    newItem["quantity"] = item["quantity"];
                    newItem["discount_type"] = item["discount_type"];
                    newItem["discount_value"] = item["discount_value"];
                    newItem["subtotal"] = item["subtotal"];
                    newItem["status"] = "A";
                    newItem["remove_action"] = "X";
                    salesItemsTable.Rows.Add(newItem);
                }

                // 5. Populate Payments
                // We need to re-initialize the payment table similar to InitializePaymentPanel
                // But we don't want to clear and show the panel yet, just load the data.
                // Actually, `paymentsTable` is local to the instance but initialized in `InitializePaymentPanel`
                // which is called when `pnlPM` is shown.
                // We should pre-load it.
                // Create a temporary reference or just wait until user opens payment panel?
                // Better to load it now so if they click "Add Payment", it shows history.
                // However, `paymentsTable` is currently tied to `DAL_DS_SalesTerminal` instance in `InitializePaymentPanel`.
                // Let's refactor `InitializePaymentPanel` slightly or just manually populate a table if `pnlPM` is not visible.
                // BUT `paymentsTable` variable is used in `btnPMComplete_Click`.
                // So we must populate `paymentsTable` variable.
                DAL_DS_SalesTerminal ds = new DAL_DS_SalesTerminal();
                paymentsTable = ds.Payment;
                paymentsTable.Clear();
                foreach (DataRow pay in fetchedPayments.Rows)
                {
                    paymentsTable.ImportRow(pay);
                }

                // If payment panel is visible (unlikely), refresh it.
                // If not, it will be refreshed when opened because `InitializePaymentPanel` clears it.
                // WAIT! `InitializePaymentPanel` CLEARS `paymentsTable`. This is bad if we want to retain loaded payments.
                // We need to change `InitializePaymentPanel` to NOT clear if we just loaded a sale.
                // OR we just populate it inside `InitializePaymentPanel`.
                // Strategy: We will modify `InitializePaymentPanel` to check if `paymentsTable` already has data from a loaded sale.
                // For now, let's just populate it here.
                // AND we need to prevent `InitializePaymentPanel` from clearing it if we are in "Edit Mode".
                // How do we know? `saleTable.Rows[0]["sale_id"]` > 0.

                // 6. Update UI Elements
                txtTotal.Text = currentSaleRow["total_amount"].ToString();
                txtGrandTotal.Text = currentSaleRow["grand_total"].ToString();

                // Customer
                if (currentSaleRow["customer_id"] != DBNull.Value)
                {
                    int custId = Convert.ToInt32(currentSaleRow["customer_id"]);
                    DataRow[] custRows = customersTable.Select($"customer_id = '{custId}'");
                    if (custRows.Length > 0)
                    {
                        txtCustomer.Text = custRows[0]["full_name"].ToString();
                    }
                }

                // Discount
                txtDiscount.EditValue = currentSaleRow["discount_value"];
                string discType = currentSaleRow["discount_type"]?.ToString();
                if (txtDiscount.Properties.Buttons.Count > 0)
                {
                    txtDiscount.Properties.Buttons[0].Caption = (discType == "PERCENTAGE") ? "%" : "Rs.";
                }

                // KOT
                if (pnlKOT.Visible)
                {
                    string orderType = currentSaleRow["order_type"]?.ToString();
                    if (orderType == "DINE_IN")
                    {
                        btnDineIn.Appearance.BackColor = Color.FromArgb(4, 181, 152);
                        btnDineIn.Appearance.ForeColor = Color.White;
                        btnTakeAway.Appearance.BackColor = Color.White;
                        btnTakeAway.Appearance.ForeColor = Color.Black;
                        pnlKOTTableNo.Visible = true;
                        cmbTableNo.SelectedItem = currentSaleRow["table_number"]?.ToString();
                    }
                    else if (orderType == "TAKE_AWAY")
                    {
                        btnTakeAway.Appearance.BackColor = Color.FromArgb(4, 181, 152);
                        btnTakeAway.Appearance.ForeColor = Color.White;
                        btnDineIn.Appearance.BackColor = Color.White;
                        btnDineIn.Appearance.ForeColor = Color.Black;
                        pnlKOTTableNo.Visible = false;
                    }
                }

                // Grid
                gvTransactionSum.GridControl.DataSource = salesItemsTable;
                gvTransactionSum.RefreshData();

                MessageBox.Show("Sale loaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sale: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
