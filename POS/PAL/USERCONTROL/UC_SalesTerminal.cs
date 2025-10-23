using DevExpress.XtraEditors;
using POS.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_SalesTerminal : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_SalesTerminal _bllSalesTerminal = new BLL_SalesTerminal();

        public UC_SalesTerminal()
        {
            InitializeComponent();

            List<string> brands = new List<string>
            {
                "Brand A", "Brand B", "Brand C", "Brand D",
                "Brand E", "Brand F", "Brand G", "Brand H"
            };

            List<string> cats = new List<string>
            {
                "Cat A", "Cat B", "Cat C", "Cat D",
                "Cat E", "Cat F", "Cat G", "Cat H"
            };

            List<string> products = new List<string>
            {
                "Product A", "Product B", "Product C", "Product D",
                "Product E", "Product F", "Product G", "Product H",
                "Product AA", "Product AB", "Product AC", "Product AD",
                "Product AE", "Product AF", "Product AG", "Product AH",
                "Product BA", "Product BB", "Product BC", "Product BD",
                "Product BE", "Product BF", "Product BG", "Product BH"
            };

            AddBrandButtonsToScrollableControl(brands);
            AddCatButtonsToScrollableControl(cats);
            AddProductButtonsToScrollableControl(products);

            LoadCategories();
            LoadBrands();
            LoadProducts();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Main.Instance.SwitchToControl(new UC_Dashboard());
        }

        private void AddBrandButtonsToScrollableControl(List<string> brandNames, int buttonWidth = 150, int buttonHeight = 41, int spacing = 10)
        {
            xtraScrollableControl2.Controls.Clear();

            int currentX = 0;

            foreach (var brand in brandNames)
            {
                DevExpress.XtraEditors.SimpleButton brandButton = new DevExpress.XtraEditors.SimpleButton
                {
                    Text = brand,
                    Name = $"btn{brand.Replace(" ", "_")}",
                    Width = buttonWidth,
                    Height = buttonHeight,
                    Tag = brand
                };

                brandButton.Location = new Point(currentX, 0);

                brandButton.Click += BrandButton_Click;

                xtraScrollableControl2.Controls.Add(brandButton);

                currentX += buttonWidth + spacing;
            }
        }

        private void BrandButton_Click(object sender, EventArgs e)
        {
            if (sender is DevExpress.XtraEditors.SimpleButton button)
            {
                string selectedBrand = button.Tag.ToString();
                MessageBox.Show($"Selected Brand: {selectedBrand}", "Brand Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // TODO: Add filtering logic based on the selected brand
            }
        }

        private void AddCatButtonsToScrollableControl(List<string> catNames, int buttonWidth = 150, int buttonHeight = 41, int spacing = 10)
        {
            xtraScrollableControl1.Controls.Clear();

            int currentX = 0;

            foreach (var cat in catNames)
            {
                DevExpress.XtraEditors.SimpleButton brandButton = new DevExpress.XtraEditors.SimpleButton
                {
                    Text = cat,
                    Name = $"btn{cat.Replace(" ", "_")}",
                    Width = buttonWidth,
                    Height = buttonHeight,
                    Tag = cat
                };

                brandButton.Location = new Point(currentX, 0);

                brandButton.Click += CatButton_Click;

                xtraScrollableControl1.Controls.Add(brandButton);

                currentX += buttonWidth + spacing;
            }
        }

        private void CatButton_Click(object sender, EventArgs e)
        {
            if (sender is DevExpress.XtraEditors.SimpleButton button)
            {
                string selectedCat = button.Tag.ToString();
                MessageBox.Show($"Selected Category: {selectedCat}", "Cat Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // TODO: Add filtering logic based on the selected brand
            }
        }

        private void AddProductButtonsToScrollableControl(List<string> productNames, int buttonWidth = 170, int buttonHeight = 150, int spacing = 10, int maxButtonsPerRow = 4)
        {
            xtraScrollableControl3.Controls.Clear();

            int currentX = 0;
            int currentY = 0;

            for (int i = 0; i < productNames.Count; i++)
            {
                DevExpress.XtraEditors.SimpleButton productButton = new DevExpress.XtraEditors.SimpleButton
                {
                    Text = productNames[i],
                    Name = $"btnProduct_{i}",
                    Width = buttonWidth,
                    Height = buttonHeight,
                    Tag = productNames[i]
                };

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

        private void ProductButton_Click(object sender, EventArgs e)
        {
            if (sender is DevExpress.XtraEditors.SimpleButton button)
            {
                string selectedProduct = button.Tag.ToString();
                MessageBox.Show($"Selected Product: {selectedProduct}", "Product Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // TODO: Add logic for product selection
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var view = gvTransactionSum;
            int row = view.FocusedRowHandle;

            string currentType = view.GetRowCellValue(row, "discount_type")?.ToString() ?? "PERCENTAGE";
            string newType = currentType == "PERCENTAGE" ? "FIXED_AMOUNT" : "PERCENTAGE";

            view.SetRowCellValue(row, "discount_type", newType);

            var editor = sender as DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit;
            editor.Buttons[0].Caption = newType == "PERCENTAGE" ? "%" : "$";

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
            // Populate categories into UI (e.g., buttons or dropdowns)
        }

        private void LoadBrands()
        {
            DataTable brands = _bllSalesTerminal.GetBrands();
            // Populate brands into UI (e.g., buttons or dropdowns)
        }

        private void LoadProducts()
        {
            DataTable products = _bllSalesTerminal.GetProducts();
            // Populate products into UI (e.g., buttons or grid)
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
    }
}
