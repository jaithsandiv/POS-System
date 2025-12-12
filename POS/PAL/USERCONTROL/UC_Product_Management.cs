using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using POS.BLL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_Product_Management : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Products _bllProducts = new BLL_Products();
        private RepositoryItemButtonEdit repositoryItemButtonEdit_Edit;
        private RepositoryItemButtonEdit repositoryItemButtonEdit_Delete;

        public UC_Product_Management()
        {
            InitializeComponent();
            InitializeRepositoryItems();
            ConfigureGrid();
            LoadData();

            // Wire up search events
            if (btnSearch != null)
                btnSearch.Click += btnSearch_Click;
            if (txtSearch != null)
                txtSearch.KeyDown += txtSearch_KeyDown;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformSearch();
            }
        }

        private void PerformSearch()
        {
            try
            {
                string keyword = txtSearch.Text.Trim();
                DataTable dt = _bllProducts.SearchProducts(keyword);
                gridProducts.DataSource = dt;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error searching data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeRepositoryItems()
        {
            repositoryItemButtonEdit_Edit = new RepositoryItemButtonEdit();
            repositoryItemButtonEdit_Delete = new RepositoryItemButtonEdit();

            repositoryItemButtonEdit_Edit.TextEditStyle = TextEditStyles.HideTextEditor;
            repositoryItemButtonEdit_Edit.Buttons.Clear();
            repositoryItemButtonEdit_Edit.Buttons.Add(new EditorButton(ButtonPredefines.Glyph) { Caption = "Edit" });
            repositoryItemButtonEdit_Edit.ButtonClick += RepositoryItemButtonEdit_Edit_ButtonClick;

            repositoryItemButtonEdit_Delete.TextEditStyle = TextEditStyles.HideTextEditor;
            repositoryItemButtonEdit_Delete.Buttons.Clear();
            repositoryItemButtonEdit_Delete.Buttons.Add(new EditorButton(ButtonPredefines.Glyph) { Caption = "Delete" });
            repositoryItemButtonEdit_Delete.ButtonClick += RepositoryItemButtonEdit_Delete_ButtonClick;

            gridProducts.RepositoryItems.Add(repositoryItemButtonEdit_Edit);
            gridProducts.RepositoryItems.Add(repositoryItemButtonEdit_Delete);
        }

        private void ConfigureGrid()
        {
            gridView1.Columns.Clear();

            var colId = gridView1.Columns.AddVisible("product_id", "ID");
            colId.OptionsColumn.AllowEdit = false;
            colId.Width = 40;

            var colName = gridView1.Columns.AddVisible("product_name", "Name");
            colName.OptionsColumn.AllowEdit = false;
            colName.Width = 150;

            var colCode = gridView1.Columns.AddVisible("product_code", "Code");
            colCode.OptionsColumn.AllowEdit = false;
            colCode.Width = 80;

            var colCategory = gridView1.Columns.AddVisible("category_name", "Category");
            colCategory.OptionsColumn.AllowEdit = false;
            colCategory.Width = 100;

            var colBrand = gridView1.Columns.AddVisible("brand_name", "Brand");
            colBrand.OptionsColumn.AllowEdit = false;
            colBrand.Width = 100;

            var colUnit = gridView1.Columns.AddVisible("unit_code", "Unit");
            colUnit.OptionsColumn.AllowEdit = false;
            colUnit.Width = 50;

            var colPrice = gridView1.Columns.AddVisible("selling_price", "Price");
            colPrice.OptionsColumn.AllowEdit = false;
            colPrice.Width = 80;
            colPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPrice.DisplayFormat.FormatString = "n2";

            var colQty = gridView1.Columns.AddVisible("stock_quantity", "Qty");
            colQty.OptionsColumn.AllowEdit = false;
            colQty.Width = 60;
            colQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colQty.DisplayFormat.FormatString = "n3";

            var colEdit = gridView1.Columns.AddVisible("Edit", "");
            colEdit.ColumnEdit = repositoryItemButtonEdit_Edit;
            colEdit.Width = 50;
            colEdit.OptionsColumn.FixedWidth = true;

            var colDelete = gridView1.Columns.AddVisible("Delete", "");
            colDelete.ColumnEdit = repositoryItemButtonEdit_Delete;
            colDelete.Width = 60;
            colDelete.OptionsColumn.FixedWidth = true;
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = _bllProducts.GetProducts();
                gridProducts.DataSource = dt;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_Product_Registration());
        }

        private void RepositoryItemButtonEdit_Edit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
             DataRow row = gridView1.GetFocusedDataRow();
             if (row != null)
             {
                 int id = Convert.ToInt32(row["product_id"]);
                 Main.Instance.LoadUserControl(new UC_Product_Registration(id));
             }
        }

        private void RepositoryItemButtonEdit_Delete_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            DataRow row = gridView1.GetFocusedDataRow();
            if (row != null)
            {
                if (XtraMessageBox.Show("Are you sure you want to delete this product?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(row["product_id"]);
                    int userId = 1; // Default
                    if (_bllProducts.DeleteProduct(id, userId))
                    {
                        LoadData();
                    }
                }
            }
        }
    }
}
