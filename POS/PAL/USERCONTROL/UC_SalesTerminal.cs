using DevExpress.DataAccess.Native.Excel;
using DevExpress.XtraEditors;
using POS.BLL;
using POS.DAL.DataSource;
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

        private DataTable productsTable;
        private DataTable categoriesTable;
        private DataTable brandsTable;
        private DataTable saleTable;
        private DataTable salesItemsTable;
        private DataTable customersTable;
        private DataTable tableNosTable;

        public UC_SalesTerminal()
        {
            InitializeComponent();

            DAL_DS_SalesTerminal ds = new DAL_DS_SalesTerminal();

            // Initialize global DataTables
            productsTable = _bllSalesTerminal.GetProducts();
            categoriesTable = _bllSalesTerminal.GetCategories();
            brandsTable = _bllSalesTerminal.GetBrands();
            saleTable = ds.Sale;
            saleTable.Clear();
            saleTable.Rows.Add(saleTable.NewRow());
            salesItemsTable = ds.SaleItem;
            salesItemsTable.Clear();
            customersTable = ds.Customer;
            customersTable.Clear();
            tableNosTable = ds.Table;
            tableNosTable.Clear();

            saleTable.RowChanged += SaleTable_RowChanged;

            LoadCategories();
            LoadBrands();
            LoadProducts();

            LoadCustomers();
            CheckKotEnabled();
            LoadTableNos();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Main.Instance.SwitchToControl(new UC_Dashboard());
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

        private void ProductButton_Click(object sender, EventArgs e)
        {
            if (sender is DevExpress.XtraEditors.SimpleButton button)
            {
                string selectedProduct = button.Tag.ToString();

                // Fetch product details from the global products table
                DataRow[] productRows = productsTable.Select($"product_name = '{selectedProduct}'");

                if (productRows.Length > 0)
                {
                    DataRow product = productRows[0];

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
                        newRow["product_id"] = product["product_id"];
                        newRow["product_name"] = product["product_name"];
                        newRow["unit_price"] = Convert.ToDecimal(product["selling_price"]);
                        newRow["quantity"] = 1;

                        // Load discount details from the product
                        string promotionType = product["promotion_type"]?.ToString() ?? "PERCENTAGE";
                        decimal discountValue = 0;
                        if (product["discount_value"] != DBNull.Value && !string.IsNullOrWhiteSpace(product["discount_value"]?.ToString()))
                        {
                            discountValue = Convert.ToDecimal(product["discount_value"]);
                        }

                        newRow["discount_type"] = promotionType;
                        newRow["discount_value"] = discountValue;

                        // Calculate subtotal
                        decimal price = Convert.ToDecimal(product["selling_price"]);
                        decimal discountAmount = promotionType == "PERCENTAGE" ? (price * discountValue / 100m) : discountValue;
                        newRow["subtotal"] = price - discountAmount;

                        salesItemsTable.Rows.Add(newRow);
                    }

                    // Update total amount and total items
                    decimal totalAmount = 0;
                    int totalItems = 0;

                    foreach (DataRow row in salesItemsTable.Rows)
                    {
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

                    gvTransactionSum.GridControl.DataSource = salesItemsTable;
                }
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var view = gvTransactionSum;
            int row = view.FocusedRowHandle;

            string currentType = view.GetRowCellValue(row, "discount_type")?.ToString() ?? "PERCENTAGE";
            string newType = currentType == "PERCENTAGE" ? "FIXED_AMOUNT" : "PERCENTAGE";

            view.SetRowCellValue(row, "discount_type", newType);

            // Access the RepositoryItemButtonEdit directly from the column's repository item
            var editor = view.Columns["discount_value"].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit;
            if (editor != null)
            {
                editor.Buttons[0].Caption = newType == "PERCENTAGE" ? "%" : "$";
            }

            // Recalculate subtotal
            decimal price = Convert.ToDecimal(view.GetRowCellValue(row, "unit_price"));
            decimal qty = Convert.ToDecimal(view.GetRowCellValue(row, "quantity"));
            decimal discountValue = Convert.ToDecimal(view.GetRowCellValue(row, "discount_value"));

            decimal discountAmount = newType == "PERCENTAGE" ? (price * qty * discountValue / 100m) : discountValue;
            view.SetRowCellValue(row, "subtotal", price * qty - discountAmount);

            view.RefreshRow(row);
        }

        private void gvTransactionSum_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "quantity" ||
                e.Column.FieldName == "discount_value" ||
                e.Column.FieldName == "discount_type")
            {
                decimal price = Convert.ToDecimal(gvTransactionSum.GetRowCellValue(e.RowHandle, "unit_price"));
                decimal qty = Convert.ToDecimal(gvTransactionSum.GetRowCellValue(e.RowHandle, "quantity"));
                decimal discountValue = Convert.ToDecimal(gvTransactionSum.GetRowCellValue(e.RowHandle, "discount_value"));
                string type = gvTransactionSum.GetRowCellValue(e.RowHandle, "discount_type")?.ToString() ?? "PERCENTAGE";

                decimal discountAmount = type == "PERCENTAGE" ? (price * qty * discountValue / 100m) : discountValue;
                gvTransactionSum.SetRowCellValue(e.RowHandle, "subtotal", price * qty - discountAmount);

                // Update total amount and total items in the saleTable
                decimal totalAmount = 0;
                int totalItems = 0;

                foreach (DataRow row in salesItemsTable.Rows)
                {
                    totalAmount += Convert.ToDecimal(row["subtotal"]);
                    totalItems += Convert.ToInt32(row["quantity"]);
                }

                if (saleTable.Rows.Count > 0)
                {
                    DataRow saleRow = saleTable.Rows[0];
                    saleRow["total_amount"] = totalAmount.ToString("F2");
                    saleRow["total_items"] = totalItems.ToString();
                }
            }
        }

        private void gvTransactionSum_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "discount_value")
            {
                string type = gvTransactionSum.GetRowCellValue(e.RowHandle, "discount_type")?.ToString();
                var editor = repositoryItemButtonEdit1;

                editor.Buttons[0].Caption = type == "FIXED_AMOUNT" ? "$" : "%";
                e.RepositoryItem = editor;
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

        private void LoadCustomers()
        {
            DataTable customers = _bllSalesTerminal.GetCustomers();

            cmbCustomer.Properties.Items.Clear();

            foreach (DataRow row in customers.Rows)
            {
                string customerDisplay = $"{row["full_name"]} ({row["phone"]})";
                cmbCustomer.Properties.Items.Add(customerDisplay);
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

        private void repositoryItemButtonEdit2_Click(object sender, EventArgs e)
        {
            var view = gvTransactionSum;
            int rowHandle = view.FocusedRowHandle;

            if (rowHandle >= 0)
            {
                // Get the data source
                DataTable salesItems = view.GridControl.DataSource as DataTable;

                if (salesItems != null)
                {
                    // Remove the row from the data source
                    salesItems.Rows.RemoveAt(rowHandle);

                    // Refresh the grid view
                    view.RefreshData();
                }
            }
        }

        private void SaleTable_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (e.Row.Table.Columns.Contains("total_amount"))
            {
                decimal totalAmount = Convert.ToDecimal(e.Row["total_amount"]);
                lblTotal.Text = $"{totalAmount:C}";
            }
        }
    }
}
