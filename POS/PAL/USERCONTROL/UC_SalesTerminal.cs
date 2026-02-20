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

        public UC_SalesTerminal()
        {
            InitializeComponent();
            InitializePaymentSummaryControls();
            gvTransactionSum.CustomColumnDisplayText += gvTransactionSum_CustomColumnDisplayText;
            btnCancel_Click(null, null);
        }

        private void InitializePaymentSummaryControls()
        {
            // Reset labels
            lblPaymentTotalValue.Text = "0.00";
            lblPaymentPaidValue.Text = "0.00";
            lblPaymentBalanceValue.Text = "0.00";
            lblPaymentBalanceValue.Appearance.ForeColor = Color.IndianRed;
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

            List<(string Name, byte[] Image, string Stock, string Price)> productDetails = new List<(string, byte[], string, string)>();
            foreach (DataRowView row in filteredView)
            {
                string name = row["product_name"]?.ToString();
                byte[] image = row["image"] as byte[];
                string stock = row["stock_quantity"]?.ToString();
                string price = row["selling_price"]?.ToString();

                productDetails.Add((name, image, stock, price));
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

            // Get available stock for the product
            decimal availableStock = 0;
            if (product["stock_quantity"] != DBNull.Value)
            {
                availableStock = Convert.ToDecimal(product["stock_quantity"]);
            }

            // Check if the product already exists in the salesItemsTable
            DataRow existingRow = null;
            decimal currentCartQuantity = 0;
            
            foreach (DataRow row in salesItemsTable.Rows)
            {
                if (row["product_id"].ToString() == product["product_id"].ToString())
                {
                    existingRow = row;
                    currentCartQuantity = Convert.ToDecimal(existingRow["quantity"]);
                    break;
                }
            }

            // Validate stock availability
            decimal newQuantity = currentCartQuantity + 1;
            
            if (newQuantity > availableStock)
            {
                string productName = product["product_name"]?.ToString() ?? "Unknown Product";
                MessageBox.Show(
                    $"Insufficient stock for '{productName}'.\n\n" +
                    $"Available Stock: {availableStock:F2}\n" +
                    $"Already in Cart: {currentCartQuantity:F2}\n" +
                    $"Cannot add more items.",
                    "Stock Unavailable",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (existingRow != null)
            {
                // Increase the quantity of the existing product
                existingRow["quantity"] = newQuantity;

                // Recalculate subtotal
                decimal price = Convert.ToDecimal(existingRow["unit_price"]);
                string promotionType = existingRow["discount_type"].ToString();
                decimal discountValue = Convert.ToDecimal(existingRow["discount_value"]);
                decimal discountAmount = promotionType == "PERCENTAGE" ? (price * discountValue / 100m) : discountValue;
                existingRow["subtotal"] = (price - discountAmount) * newQuantity;
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
                totalItems += (int)Convert.ToDecimal(row["quantity"]);
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
                // Get the current row data
                DataRow currentRow = gvTransactionSum.GetDataRow(e.RowHandle);
                if (currentRow == null) return;

                // Validate stock if quantity changed
                if (e.Column.FieldName == "quantity")
                {
                    string productIdStr = currentRow["product_id"]?.ToString();
                    if (!string.IsNullOrEmpty(productIdStr))
                    {
                        // Get available stock from products table
                        DataRow[] productRows = productsTable.Select($"product_id = '{productIdStr}'");
                        if (productRows.Length > 0)
                        {
                            decimal availableStock = Convert.ToDecimal(productRows[0]["stock_quantity"]);
                            decimal requestedQty = Convert.ToDecimal(gvTransactionSum.GetRowCellValue(e.RowHandle, "quantity"));

                            if (requestedQty > availableStock)
                            {
                                string productName = currentRow["product_name"]?.ToString() ?? "Unknown Product";
                                MessageBox.Show(
                                    $"Insufficient stock for '{productName}'.\n\n" +
                                    $"Available Stock: {availableStock:F2}\n" +
                                    $"Requested Quantity: {requestedQty:F2}\n" +
                                    $"Quantity has been adjusted to available stock.",
                                    "Stock Unavailable",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning
                                );

                                // Set quantity to available stock
                                gvTransactionSum.SetRowCellValue(e.RowHandle, "quantity", availableStock);
                                return;
                            }
                        }
                    }
                }

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
            List<(string Name, byte[] Image, string Stock, string Price)> productDetails = new List<(string, byte[], string, string)>();
            foreach (DataRow row in productsTable.Rows)
            {
                string name = row["product_name"]?.ToString();
                byte[] image = row["image"] as byte[];
                string stock = row["stock_quantity"]?.ToString();
                string price = row["selling_price"]?.ToString();

                productDetails.Add((name, image, stock, price));
            }

            AddProductButtonsToScrollableControl(productDetails);
        }

        private void AddProductButtonsToScrollableControl(List<(string Name, byte[] Image, string Stock, string Price)> productDetails, int buttonWidth = 170, int buttonHeight = 150, int spacing = 10, int maxButtonsPerRow = 4)
        {
            xtraScrollableControl3.Controls.Clear();

            int currentX = 0;
            int currentY = 0;

            for (int i = 0; i < productDetails.Count; i++)
            {
                var product = productDetails[i];
                
                // Parse price and stock for display
                decimal price = 0;
                if (!string.IsNullOrEmpty(product.Price))
                    decimal.TryParse(product.Price, out price);

                decimal stock = 0;
                if (!string.IsNullOrEmpty(product.Stock))
                    decimal.TryParse(product.Stock, out stock);

                DevExpress.XtraEditors.SimpleButton productButton = new DevExpress.XtraEditors.SimpleButton
                {
                    Name = $"btnProduct{i}",
                    Width = buttonWidth,
                    Height = buttonHeight,
                    Tag = product.Name
                };

                // Disable button if out of stock
                if (stock <= 0)
                {
                    productButton.Enabled = false;
                    productButton.Appearance.BackColor = Color.LightGray;
                    productButton.Appearance.ForeColor = Color.DarkGray;
                }
                else if (stock <= 10)
                {
                    // Highlight low stock items
                    productButton.Appearance.ForeColor = Color.DarkOrange;
                }

                if (product.Image != null && product.Image.Length > 0)
                {
                    // Product has image - display image above text
                    try
                    {
                        using (var ms = new System.IO.MemoryStream(product.Image))
                        {
                            Image originalImage = Image.FromStream(ms);
                            
                            // Calculate scaled image size (leave space for text at bottom)
                            int imageAreaHeight = buttonHeight - 50; // Reserve 50px for text
                            int imageWidth = buttonWidth - 20; // 10px padding on each side
                            int imageHeight = imageAreaHeight;
                            
                            // Maintain aspect ratio
                            float aspectRatio = (float)originalImage.Width / originalImage.Height;
                            if (imageWidth / aspectRatio < imageHeight)
                            {
                                imageHeight = (int)(imageWidth / aspectRatio);
                            }
                            else
                            {
                                imageWidth = (int)(imageHeight * aspectRatio);
                            }
                            
                            // Create scaled image
                            productButton.ImageOptions.Image = originalImage.GetThumbnailImage(imageWidth, imageHeight, null, IntPtr.Zero);
                            productButton.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
                        }
                        
                        // Set text below image with stock information
                        if (stock <= 0)
                        {
                            productButton.Text = $"{product.Name}\nRs. {price:F2}\nOUT OF STOCK";
                        }
                        else if (stock <= 10)
                        {
                            productButton.Text = $"{product.Name}\nRs. {price:F2}\nStock: {stock:F2} (Low)";
                            productButton.Appearance.ForeColor = Color.DarkOrange;
                        }
                        else
                        {
                            productButton.Text = $"{product.Name}\nRs. {price:F2}\nStock: {stock:F2}";
                        }
                    }
                    catch
                    {
                        // If image loading fails, show text only
                        if (stock <= 0)
                        {
                            productButton.Text = $"{product.Name}\nRs. {price:F2}\nOUT OF STOCK";
                        }
                        else if (stock <= 10)
                        {
                            productButton.Text = $"{product.Name}\nRs. {price:F2}\nStock: {stock:F2} (Low)";
                            productButton.Appearance.ForeColor = Color.DarkOrange;
                        }
                        else
                        {
                            productButton.Text = $"{product.Name}\nRs. {price:F2}\nStock: {stock:F2}";
                        }
                    }
                }
                else
                {
                    // No image - display text only with stock details
                    if (stock <= 0)
                    {
                        productButton.Text = $"{product.Name}\nRs. {price:F2}\nOUT OF STOCK";
                    }
                    else if (stock <= 10)
                    {
                        productButton.Text = $"{product.Name}\nRs. {price:F2}\nStock: {stock:F2} (Low)";
                        productButton.Appearance.ForeColor = Color.DarkOrange;
                    }
                    else
                    {
                        productButton.Text = $"{product.Name}\nRs. {price:F2}\nStock: {stock:F2}";
                    }
                }

                productButton.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                productButton.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
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
            DataTable TableNos = _bllSalesTerminal.GetAvailableTables();

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
            var (totalPaid, due) = CalculatePaymentTotals();
            AddPaymentEntry(selectedPaymentMethod, due);

            // Reset selection so it can be selected again if needed
            cmbPM.EditValue = null;
        }

        private void InitializePaymentPanel()
        {
            // Clear the scrollable control
            pnlPayment.Controls.Clear();

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
            }

            // Reset labels
            UpdatePaymentSummaryUI();
        }

        private void btnAddPayment_Click(object sender, EventArgs e)
        {
            // Add a new payment entry with no method pre-selected
            var (totalPaid, due) = CalculatePaymentTotals();
            AddPaymentEntry(null, due > 0 ? due : (decimal?)null);
        }

        private void AddPaymentEntry(string preselectedMethod = null, decimal? prefilledAmount = null)
        {
            paymentEntryCounter++;
            int entryId = paymentEntryCounter;

            // Determine panel height based on payment method
            int panelHeight = 200;//180; // Default for CASH/CREDIT (smaller)
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
                Width = pnlPayment.Width - 30,
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
                Tag = entryId,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
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
                    int newHeight = 200;
                    if (selectedMethod.ToUpper() == "CARD")
                        newHeight = 380;
                    else if (selectedMethod.ToUpper() == "BANK_TRANSFER")
                        newHeight = 260;
                    else if (selectedMethod.ToUpper() == "CREDIT")
                        newHeight = 200;

                    paymentEntryPanel.Height = newHeight;
                    pnlFields.Height = newHeight - 140;
                    btnRemove.Location = new Point(paymentEntryPanel.Width - 115, newHeight - 45);

                    // When changing method within an existing entry, we might want to preserve the amount if possible
                    // But for now, let's just repopulate. If we want to preserve amount, we'd need to read it from current fields.
                    // Let's try to read the current amount from the data table
                    decimal? currentAmount = null;
                    DataRow[] rows = paymentsTable.Select($"payment_id = {entryId}");
                    if (rows.Length > 0 && decimal.TryParse(rows[0]["amount"]?.ToString(), out decimal amt))
                    {
                        currentAmount = amt;
                    }

                    PopulatePaymentFields(pnlFields, selectedMethod, entryId, currentAmount);
                }
            };

            // Add controls to payment entry panel
            paymentEntryPanel.Controls.Add(lblMethod);
            paymentEntryPanel.Controls.Add(cmbMethod);
            paymentEntryPanel.Controls.Add(pnlFields);
            paymentEntryPanel.Controls.Add(btnRemove);

            // Add payment entry panel to scrollable control
            // Since we're using Dock.Top, add to the END to maintain visual order (top to bottom)
            pnlPayment.Controls.Add(paymentEntryPanel);
            paymentEntryPanel.SendToBack(); // Move to bottom of z-order so it appears below existing panels

            // If method is preselected, populate fields immediately
            if (!string.IsNullOrEmpty(preselectedMethod))
            {
                PopulatePaymentFields(pnlFields, preselectedMethod, entryId, prefilledAmount);
            }

            // Create a payment row in paymentsTable
            DataRow paymentRow = paymentsTable.NewRow();
            paymentRow["payment_id"] = DBNull.Value;
            paymentRow["sale_id"] = DBNull.Value;
            paymentRow["payment_method"] = preselectedMethod ?? string.Empty;
            paymentRow["amount"] = prefilledAmount.HasValue ? prefilledAmount.Value.ToString("F2") : "0.00";
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

            UpdatePaymentSummaryUI();
        }

        private void RemovePaymentEntry(int entryId)
        {
            // Find and remove the panel
            Control panelToRemove = pnlPayment.Controls[$"pnlPaymentEntry{entryId}"];
            if (panelToRemove != null)
            {
                pnlPayment.Controls.Remove(panelToRemove);
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

        private void PopulatePaymentFields(PanelControl fieldsPanel, string paymentMethod, int entryId, decimal? amount = null)
        {
            // Clear existing controls
            fieldsPanel.Controls.Clear();

            switch (paymentMethod.ToUpper())
            {
                case "CASH":
                    AddCashFieldsToPanel(fieldsPanel, entryId, amount);
                    break;
                case "CARD":
                    AddCardFieldsToPanel(fieldsPanel, entryId, amount);
                    break;
                case "BANK_TRANSFER":
                    AddBankTransferFieldsToPanel(fieldsPanel, entryId, amount);
                    break;
                case "CREDIT":
                    AddCreditFieldsToPanel(fieldsPanel, entryId);
                    break;
            }
        }

        private void AddCashFieldsToPanel(PanelControl panel, int entryId, decimal? amount = null)
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
            if (amount.HasValue)
            {
                txtAmount.Text = amount.Value.ToString("F2");
            }
            txtAmount.EditValueChanged += (s, e) =>
            {
                UpdatePaymentAmount(entryId, txtAmount.Text);
            };

            panel.Controls.Add(lblAmount);
            panel.Controls.Add(txtAmount);
        }

        private void AddCardFieldsToPanel(PanelControl panel, int entryId, decimal? amount = null)
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
            if (amount.HasValue)
            {
                txtAmount.Text = amount.Value.ToString("F2");
            }
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

        private void AddBankTransferFieldsToPanel(PanelControl panel, int entryId, decimal? amount = null)
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
            if (amount.HasValue)
            {
                txtAmount.Text = amount.Value.ToString("F2");
            }
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
            foreach (Control ctrl in pnlPayment.Controls)
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

            // Print KOT
            string customerName = txtCustomer.Text;
            PrintKOT($"DRAFT-{saleId}", orderType, tableNumber, customerName, salesItemsTable, true);

            btnCancel_Click(null, null);
        }

        private void btnPrintKOT_Click(object sender, EventArgs e)
        {
            if (salesItemsTable.Rows.Count == 0)
            {
                MessageBox.Show("Cart is empty.", "Empty Cart", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
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

            string customerName = txtCustomer.Text;
            string invoiceNo = "KOT-PREVIEW";

            // If it's a loaded sale/draft, use its number
            if (saleTable.Rows.Count > 0 && saleTable.Rows[0]["sale_id"] != DBNull.Value)
            {
                invoiceNo = saleTable.Rows[0]["invoice_number"]?.ToString();
                if (string.IsNullOrEmpty(invoiceNo))
                    invoiceNo = $"DRAFT-{saleTable.Rows[0]["sale_id"]}";
            }

            PrintKOT(invoiceNo, orderType, tableNumber, customerName, salesItemsTable, true);
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
            try
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

                // Show success message
                string customerName = selectedCustomer["full_name"]?.ToString() ?? "Customer";
                MessageBox.Show(
                    $"Credit Sale Created Successfully!\n\n" +
                    $"Invoice Number: {invoiceNumber}\n" +
                    $"Customer: {customerName}\n" +
                    $"Grand Total: {grandTotal:F2}\n" +
                    $"Credit Applied: {grandTotal:F2}\n" +
                    $"Payment Status: CREDIT",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset UI
                btnCancel_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating credit sale: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Calculates payment totals excluding CREDIT as paid
        /// </summary>
        private (decimal totalPaid, decimal due) CalculatePaymentTotals()
        {
            decimal grandTotal = 0;
            if (saleTable.Rows.Count > 0)
            {
                decimal.TryParse(saleTable.Rows[0]["grand_total"]?.ToString(), out grandTotal);
            }

            decimal totalPaid = 0;
            if (paymentsTable != null)
            {
                foreach (DataRow payment in paymentsTable.Rows)
                {
                    if (payment.RowState == DataRowState.Deleted)
                        continue;

                    string method = payment["payment_method"]?.ToString();
                    decimal amount = 0;
                    decimal.TryParse(payment["amount"]?.ToString(), out amount);

                    // Exclude CREDIT from total paid calculation
                    if (method != "CREDIT")
                    {
                        totalPaid += amount;
                    }
                }
            }

            decimal due = Math.Max(0, grandTotal - totalPaid);
            return (totalPaid, due);
        }

        /// <summary>
        /// Updates the payment summary UI labels
        /// </summary>
        private void UpdatePaymentSummaryUI()
        {
            if (lblPaymentTotalValue == null || lblPaymentPaidValue == null || lblPaymentBalanceValue == null)
                return;

            decimal grandTotal = 0;
            if (saleTable.Rows.Count > 0)
            {
                decimal.TryParse(saleTable.Rows[0]["grand_total"]?.ToString(), out grandTotal);
            }

            var (totalPaid, due) = CalculatePaymentTotals();

            lblPaymentTotalValue.Text = grandTotal.ToString("F2");
            lblPaymentPaidValue.Text = totalPaid.ToString("F2");
            lblPaymentBalanceValue.Text = due.ToString("F2");

            // Change balance label color based on status
            if (due > 0)
            {
                lblPaymentBalanceValue.Appearance.ForeColor = Color.IndianRed;
            }
            else
            {
                lblPaymentBalanceValue.Appearance.ForeColor = Color.Green;
            }
        }

        /// <summary>
        /// Prints Kitchen Order Ticket (KOT)
        /// </summary>
        private void PrintKOT(string invoiceNumber, string orderType, string tableNumber, string customerName, DataTable items, bool autoPrint)
        {
            // KOT printing logic - placeholder for now
            // You would implement the actual KOT report here
            System.Diagnostics.Debug.WriteLine($"Printing KOT: {invoiceNumber}, Type: {orderType}, Table: {tableNumber}");
        }

        /// <summary>
        /// Prints thermal invoice
        /// </summary>
        private void PrintThermalInvoice(string invoiceNumber, decimal grandTotal, string customerName, 
            DataTable items, DataTable payments, bool autoPrint)
        {
            try
            {
                ThermalInvoice thermalInvoice = new ThermalInvoice();

                // Set footer text from settings
                string footerText = Main.GetSetting("invoice_footer", "Thank You!");
                thermalInvoice.Parameters["p_footer"].Value = footerText;

                // Set main invoice parameters
                thermalInvoice.Parameters["p_invoice_no"].Value = invoiceNumber;
                thermalInvoice.Parameters["p_date"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                thermalInvoice.Parameters["p_customer_name"].Value = customerName;
                thermalInvoice.Parameters["p_grand_total"].Value = grandTotal.ToString("F2");

                // Get store details
                if (Main.DataSetApp.Store.Rows.Count > 0)
                {
                    var storeRow = Main.DataSetApp.Store[0];
                    thermalInvoice.Parameters["p_contact"].Value = storeRow.IsphoneNull() ? "" : storeRow.phone;
                    thermalInvoice.Parameters["p_email"].Value = storeRow.IsemailNull() ? "" : storeRow.email;
                    thermalInvoice.Parameters["p_address"].Value = storeRow.IsaddressNull() ? "" : storeRow.address;
                }

                // Set data source
                thermalInvoice.DataSource = items;

                // Calculate totals
                decimal subtotal = 0m;
                foreach (DataRow item in items.Rows)
                {
                    if (decimal.TryParse(item["subtotal"]?.ToString(), out decimal itemTotal))
                    {
                        subtotal += itemTotal;
                    }
                }

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

                // Get thermal printer name
                string printerName = Main.GetSetting("thermal_printer_name", null);
                if (!string.IsNullOrEmpty(printerName))
                {
                    thermalInvoice.PrinterName = printerName;
                }

                // Print
                if (autoPrint)
                {
                    thermalInvoice.Print();
                }
                else
                {
                    DevExpress.XtraReports.UI.ReportPrintTool printTool = new DevExpress.XtraReports.UI.ReportPrintTool(thermalInvoice);
                    printTool.ShowPreview();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing thermal invoice: {ex.Message}", "Print Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Complete payment and create sale
        /// </summary>
        private void btnPMComplete_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate cart
                if (salesItemsTable.Rows.Count == 0)
                {
                    MessageBox.Show("Cart is empty. Cannot complete sale.", "Empty Cart",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate payments
                var (totalPaid, due) = CalculatePaymentTotals();
                decimal grandTotal = decimal.Parse(saleTable.Rows[0]["grand_total"]?.ToString() ?? "0");

                // Calculate total of all payments (including CREDIT)
                decimal allPaymentsTotal = 0;
                foreach (DataRow payment in paymentsTable.Rows)
                {
                    if (payment.RowState != DataRowState.Deleted)
                    {
                        allPaymentsTotal += decimal.Parse(payment["amount"]?.ToString() ?? "0");
                    }
                }

                // Check if sum of all payments equals grand total
                if (Math.Abs(allPaymentsTotal - grandTotal) > 0.01m)
                {
                    MessageBox.Show(
                        $"Payment validation failed:\n\n" +
                        $"Grand Total: Rs. {grandTotal:F2}\n" +
                        $"Total Payments: Rs. {allPaymentsTotal:F2}\n\n" +
                        $"The sum of all payments must equal the grand total.",
                        "Payment Mismatch",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
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

                // Calculate change
                decimal changeDue = Math.Max(0, totalPaid - grandTotal);

                // Get order type and table
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

                // Generate invoice number
                string invoiceNumber = _bllSalesTerminal.GetNextInvoiceNumber();
                string notes = $"Sale completed on {DateTime.Now:yyyy-MM-dd HH:mm:ss}";

                // Save sale
                int saleId = _bllSalesTerminal.SaveSale(storeId, billerId, customerId, "SALE",
                    discountType, discountValue, totalAmount, totalItems, grandTotal, notes,
                    salesItemsTable, totalPaid, changeDue, invoiceNumber, null, orderType, tableNumber);

                // Save payments
                _bllSalesTerminal.SavePayments(saleId, paymentsTable, billerId);

                // Determine payment status
                string paymentStatus = "PAID";
                if (due > 0)
                {
                    paymentStatus = "PARTIAL";
                }

                // Update saleTable
                DataRow saleRow = saleTable.Rows[0];
                saleRow["sale_id"] = saleId;
                saleRow["sale_type"] = "SALE";
                saleRow["invoice_number"] = invoiceNumber;
                saleRow["payment_status"] = paymentStatus;
                saleRow["sale_status"] = "COMPLETED";
                saleRow["total_paid"] = totalPaid.ToString("F2");
                saleRow["change_due"] = changeDue.ToString("F2");

                // Generate and print invoice
                string customerName = txtCustomer.Text;

                // Check print settings
                bool enableThermal = Main.GetSetting("ENABLE_THERMAL_PRINT", "False").Equals("True", StringComparison.OrdinalIgnoreCase);
                bool enableA4 = Main.GetSetting("ENABLE_A4_PRINT", "True").Equals("True", StringComparison.OrdinalIgnoreCase);
                bool autoPrint = true;

                if (enableThermal)
                {
                    PrintThermalInvoice(invoiceNumber, grandTotal, customerName, salesItemsTable, paymentsTable, autoPrint);
                }
                else if (enableA4)
                {
                    // Print A4 invoice
                    REPORT.Invoice invoiceReport = new REPORT.Invoice();
                    invoiceReport.DataSource = salesItemsTable;
                    
                    string footerTextA4 = Main.GetSetting("invoice_footer", "Thank You For Your Business!");
                    invoiceReport.Parameters["p_footer"].Value = footerTextA4;
                    invoiceReport.Parameters["p_invoice_no"].Value = invoiceNumber;
                    invoiceReport.Parameters["p_date"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    invoiceReport.Parameters["p_total"].Value = totalAmount.ToString("F2");
                    invoiceReport.Parameters["p_discount"].Value = discountValue.ToString("F2");
                    invoiceReport.Parameters["p_grand_total"].Value = grandTotal.ToString("F2");
                    invoiceReport.Parameters["p_customer_name"].Value = customerName;

                    DevExpress.XtraReports.UI.ReportPrintTool printTool = new DevExpress.XtraReports.UI.ReportPrintTool(invoiceReport);
                    printTool.Print();
                }

                // Print KOT if enabled
                if (!string.IsNullOrEmpty(orderType))
                {
                    PrintKOT(invoiceNumber, orderType, tableNumber, customerName, salesItemsTable, autoPrint);
                }

                // Show success message
                MessageBox.Show(
                    $"Invoice Created Successfully!\n\n" +
                    $"Invoice Number: {invoiceNumber}\n" +
                    $"Customer: {customerName}\n" +
                    $"Grand Total: {grandTotal:F2}\n" +
                    $"Total Paid: {totalPaid:F2}\n" +
                    $"Change Due: {changeDue:F2}\n" +
                    $"Due: {due:F2}\n" +
                    $"Payment Status: {paymentStatus}",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset UI
                btnCancel_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error completing sale: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Opens panel to load saved drafts/quotations/sales
        /// </summary>
        private void btnLoadSaved_Click(object sender, EventArgs e)
        {
            // This button would open a panel showing drafts/quotations/sales
            // For now just show a message
            MessageBox.Show("Load saved sales feature - panel should be visible", "Feature", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Closes the load saved panel
        /// </summary>
        private void btnCloseLoadSaved_Click(object sender, EventArgs e)
        {
            // This would hide the load saved panel
            // For now just show a message
            MessageBox.Show("Close load saved panel", "Feature", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Loads a selected sale (draft/quotation/sale) into the terminal
        /// </summary>
        private void LoadSelectedSale(object sender, EventArgs e)
        {
            // This would load the selected sale into the terminal for editing/completing
            // For now just show a message
            MessageBox.Show("Load selected sale into terminal", "Feature", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
// Sales Discounts by user already implemented in UC_SalesTerminal.cs