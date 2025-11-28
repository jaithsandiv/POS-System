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
    public partial class UC_Brand_Management : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Products _bllProducts = new BLL_Products();
        private RepositoryItemButtonEdit repositoryItemButtonEdit_Edit;
        private RepositoryItemButtonEdit repositoryItemButtonEdit_Delete;

        public UC_Brand_Management()
        {
            InitializeComponent();
            InitializeRepositoryItems();
            ConfigureGrid();
            LoadData();
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

            gridBrands.RepositoryItems.Add(repositoryItemButtonEdit_Edit);
            gridBrands.RepositoryItems.Add(repositoryItemButtonEdit_Delete);
        }

        private void ConfigureGrid()
        {
            gridView1.Columns.Clear();

            var colId = gridView1.Columns.AddVisible("brand_id", "ID");
            colId.OptionsColumn.AllowEdit = false;
            colId.Width = 50;

            var colName = gridView1.Columns.AddVisible("brand_name", "Name");
            colName.OptionsColumn.AllowEdit = false;

            var colSupplier = gridView1.Columns.AddVisible("supplier_name", "Supplier");
            colSupplier.OptionsColumn.AllowEdit = false;

            var colStatus = gridView1.Columns.AddVisible("status", "Status");
            colStatus.OptionsColumn.AllowEdit = false;
            colStatus.Width = 50;

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
                DataTable dt = _bllProducts.GetBrands();
                gridBrands.DataSource = dt;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddBrand_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_Brand_Registration());
        }

        private void RepositoryItemButtonEdit_Edit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
             DataRow row = gridView1.GetFocusedDataRow();
             if (row != null)
             {
                 int id = Convert.ToInt32(row["brand_id"]);
                 Main.Instance.LoadUserControl(new UC_Brand_Registration(id));
             }
        }

        private void RepositoryItemButtonEdit_Delete_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            DataRow row = gridView1.GetFocusedDataRow();
            if (row != null)
            {
                if (XtraMessageBox.Show("Are you sure you want to delete this brand?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(row["brand_id"]);
                    int userId = 1; // Default
                    if (_bllProducts.DeleteBrand(id, userId))
                    {
                        LoadData();
                    }
                }
            }
        }
    }
}
