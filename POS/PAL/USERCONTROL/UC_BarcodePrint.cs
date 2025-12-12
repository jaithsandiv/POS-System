using DevExpress.XtraEditors;
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

namespace POS.PAL.USERCONTROL
{
    public partial class UC_BarcodePrint : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Products _bllProducts = new BLL_Products();

        public UC_BarcodePrint()
        {
            InitializeComponent();
            LoadProducts();
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
                DataTable dt = _bllProducts.SearchBarcodePrints(keyword);
                gridBarcodePrints.DataSource = dt;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error searching data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                DataTable dt = _bllProducts.GetProducts();
                lueProduct.Properties.DataSource = dt;
                lueProduct.Properties.DisplayMember = "product_name";
                lueProduct.Properties.ValueMember = "product_id";
                lueProduct.Properties.Columns.Clear();
                lueProduct.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("product_code", "Code"));
                lueProduct.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("product_name", "Name"));
                lueProduct.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("selling_price", "Price"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ConfigureGrid()
        {
            gridView1.Columns.Clear();

            var colProduct = gridView1.Columns.AddVisible("product_name", "Product");
            var colCode = gridView1.Columns.AddVisible("product_code", "Code");
            var colQty = gridView1.Columns.AddVisible("quantity_printed", "Qty");
            var colName = gridView1.Columns.AddVisible("include_name", "Inc. Name");
            var colPrice = gridView1.Columns.AddVisible("include_price", "Inc. Price");
            var colExpiry = gridView1.Columns.AddVisible("include_expiry", "Inc. Expiry");
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = _bllProducts.GetBarcodePrints();
                gridBarcodePrints.DataSource = dt;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (lueProduct.EditValue == null)
            {
                XtraMessageBox.Show("Please select a product.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int productId = Convert.ToInt32(lueProduct.EditValue);
                int qty = Convert.ToInt32(numQuantity.Value);
                bool incName = chkName.Checked;
                bool incPrice = chkPrice.Checked;
                bool incExpiry = chkExpiry.Checked;
                bool incMan = chkManufacture.Checked;
                bool incPromo = chkPromo.Checked;
                int userId = 1;

                _bllProducts.InsertBarcodePrint(productId, qty, incName, incPrice, incExpiry, incMan, incPromo, userId);
                LoadData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure you want to clear the list?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _bllProducts.ClearBarcodePrints();
                LoadData();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            // Placeholder for printing logic
            // In a real app, this would use XtraReport or PrintDocument to generate labels based on the list
            XtraMessageBox.Show("Labels sent to printer.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
