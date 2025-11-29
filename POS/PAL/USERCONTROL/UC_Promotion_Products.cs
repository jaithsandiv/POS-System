using POS.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_Promotion_Products : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly int _promotionId;
        private readonly string _promotionName;
        private readonly BLL_Discount _bllDiscount = new BLL_Discount();
        private readonly BLL_Products _bllProducts = new BLL_Products();

        public UC_Promotion_Products(int promotionId, string promotionName)
        {
            InitializeComponent();
            _promotionId = promotionId;
            _promotionName = promotionName;
            labelControlTitle.Text = $"Manage Products: {_promotionName}";

            InitializeControls();
            LoadAvailableProducts();
            LoadPromotionProducts();
        }

        private void InitializeControls()
        {
            // Setup Discount Type Combo
            cmbDiscountType.Properties.Items.AddRange(new string[] { "Percentage", "Fixed Amount" });
            cmbDiscountType.SelectedIndex = 0;

            // Setup Grid
            ConfigureGrid();
        }

        private void ConfigureGrid()
        {
            gridViewProducts.Columns.Clear();

            var colId = gridViewProducts.Columns.AddVisible("promotion_product_id", "ID");
            colId.FieldName = "promotion_product_id";
            colId.Visible = false;

            var colCode = gridViewProducts.Columns.AddVisible("product_code", "Code");
            colCode.FieldName = "product_code";
            colCode.Width = 150;

            var colName = gridViewProducts.Columns.AddVisible("product_name", "Product Name");
            colName.FieldName = "product_name";
            colName.Width = 400;

            var colType = gridViewProducts.Columns.AddVisible("promotion_type", "Type");
            colType.FieldName = "promotion_type";
            colType.Width = 150;

            var colValue = gridViewProducts.Columns.AddVisible("discount_value", "Value");
            colValue.FieldName = "discount_value";
            colValue.Width = 150;
            colValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colValue.DisplayFormat.FormatString = "n2";

            // Style
            gridViewProducts.Appearance.HeaderPanel.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            gridViewProducts.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridViewProducts.Appearance.Row.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular);
            gridViewProducts.ColumnPanelRowHeight = 44;
            gridViewProducts.RowHeight = 44;
            gridViewProducts.OptionsSelection.MultiSelect = true;
            gridViewProducts.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
        }

        private void LoadAvailableProducts()
        {
            try
            {
                DataTable products = _bllProducts.GetProducts();

                lookupProduct.Properties.DataSource = products;
                lookupProduct.Properties.DisplayMember = "product_name";
                lookupProduct.Properties.ValueMember = "product_id";

                // Customize columns in dropdown
                lookupProduct.Properties.Columns.Clear();
                lookupProduct.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("product_code", "Code", 40));
                lookupProduct.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("product_name", "Product Name", 100));
                lookupProduct.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("selling_price", "Price", 40, DevExpress.Utils.FormatType.Numeric, "n2", true, DevExpress.Utils.HorzAlignment.Far));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPromotionProducts()
        {
            try
            {
                DataTable dt = _bllDiscount.GetProductsByPromotionID(_promotionId);
                gridControlProducts.DataSource = dt;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error loading promotion products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (lookupProduct.EditValue == null)
            {
                XtraMessageBox.Show("Please select a product.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDiscountValue.Text) || !decimal.TryParse(txtDiscountValue.Text, out decimal value))
            {
                XtraMessageBox.Show("Please enter a valid discount value.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate Percentage
            string type = cmbDiscountType.Text == "Percentage" ? "PERCENTAGE" : "FIXED_AMOUNT";
            if (type == "PERCENTAGE" && (value < 0 || value > 100))
            {
                XtraMessageBox.Show("Percentage must be between 0 and 100.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int productId = Convert.ToInt32(lookupProduct.EditValue);

            bool success = _bllDiscount.AddProductToPromotion(_promotionId, productId, type, value);

            if (success)
            {
                LoadPromotionProducts();
                lookupProduct.EditValue = null;
                txtDiscountValue.Text = "";
                XtraMessageBox.Show("Product added to promotion.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show("Failed to add product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gridViewProducts.GetSelectedRows();
            if (selectedRows.Length == 0)
            {
                XtraMessageBox.Show("Please select products to remove.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (XtraMessageBox.Show($"Are you sure you want to remove {selectedRows.Length} product(s) from this promotion?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (int rowHandle in selectedRows)
                {
                    int id = Convert.ToInt32(gridViewProducts.GetRowCellValue(rowHandle, "promotion_product_id"));
                    _bllDiscount.RemoveProductFromPromotion(id);
                }
                LoadPromotionProducts();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_Discount_Management());
        }
    }
}
