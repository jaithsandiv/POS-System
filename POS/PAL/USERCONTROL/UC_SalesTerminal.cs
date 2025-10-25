using DevExpress.XtraEditors;
using POS.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Printing;
using System.Windows.Forms;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_SalesTerminal : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_SalesTerminal _bllSalesTerminal = new BLL_SalesTerminal();
        private DevExpress.XtraEditors.ComboBoxEdit cmbCustomers;

        public UC_SalesTerminal()
        {
            InitializeComponent();

            LoadCategories();
            LoadBrands();
            LoadProducts();

            LoadCustomers();
            LoadTableNos();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Main.Instance.SwitchToControl(new UC_Dashboard());
        }

        private void FilterProducts(string brandId, string categoryId)
        {
            DataTable products = _bllSalesTerminal.GetProducts();
            DataView filteredView = new DataView(products);

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

                // Fetch product details from the dataset
                DataTable products = _bllSalesTerminal.GetProducts();
                DataRow[] productRows = products.Select($"product_name = '{selectedProduct}'");

                if (productRows.Length > 0)
                {
                    DataRow product = productRows[0];

                    // Add product to the transaction summary grid
                    DataTable salesItems = gvTransactionSum.GridControl.DataSource as DataTable ?? new DataTable();

                    if (salesItems.Columns.Count == 0)
                    {
                        salesItems.Columns.Add("product_id", typeof(string));
                        salesItems.Columns.Add("product_name", typeof(string));
                        salesItems.Columns.Add("unit_price", typeof(decimal));
                        salesItems.Columns.Add("quantity", typeof(int));
                        salesItems.Columns.Add("discount_type", typeof(string));
                        salesItems.Columns.Add("discount_value", typeof(decimal));
                        salesItems.Columns.Add("subtotal", typeof(decimal));
                    }

                    DataRow newRow = salesItems.NewRow();
                    newRow["product_id"] = product["product_id"];
                    newRow["product_name"] = product["product_name"];
                    newRow["unit_price"] = Convert.ToDecimal(product["selling_price"]);
                    newRow["quantity"] = 1;

                    // Load discount details from the product
                    string promotionType = product["promotion_type"]?.ToString() ?? "PERCENTAGE";
                    decimal discountValue = Convert.ToDecimal(product["discount_value"] ?? 0);

                    newRow["discount_type"] = promotionType;
                    newRow["discount_value"] = discountValue;

                    // Calculate subtotal
                    decimal price = Convert.ToDecimal(product["selling_price"]);
                    decimal discountAmount = promotionType == "PERCENTAGE" ? (price * discountValue / 100m) : discountValue;
                    newRow["subtotal"] = price - discountAmount;

                    salesItems.Rows.Add(newRow);

                    gvTransactionSum.GridControl.DataSource = salesItems;
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
            DataTable categories = _bllSalesTerminal.GetCategories();

            List<(string Id, string Name)> categoryDetails = new List<(string, string)> { ("All Categories", "All Categories") };
            foreach (DataRow row in categories.Rows)
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
            DataTable brands = _bllSalesTerminal.GetBrands();

            List<(string Id, string Name)> brandDetails = new List<(string, string)> { ("All Brands", "All Brands") };
            foreach (DataRow row in brands.Rows)
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
            DataTable products = _bllSalesTerminal.GetProducts();

            List<(string Name, byte[] Image, string Stock)> productDetails = new List<(string, byte[], string)>();
            foreach (DataRow row in products.Rows)
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
                MessageBox.Show("KOT is enabled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("KOT is disabled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
