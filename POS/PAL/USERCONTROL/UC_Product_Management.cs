using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using OfficeOpenXml;
using POS.BLL;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_Product_Management : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Products _bllProducts = new BLL_Products();
        private RepositoryItemButtonEdit repositoryItemButtonEdit_Edit;
        private RepositoryItemButtonEdit repositoryItemButtonEdit_Delete;

        // Static constructor to set EPPlus license once (EPPlus 8+ requires this)
        static UC_Product_Management()
        {
            ExcelPackage.License.SetNonCommercialPersonal("POS-System");
        }

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

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Select Product Import File";
                    openFileDialog.Filter = "Excel Files|*.xlsx;*.xls|CSV Files|*.csv|All Files|*.*";
                    openFileDialog.FilterIndex = 1;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;
                        string extension = Path.GetExtension(filePath).ToLower();

                        DataTable importData = null;

                        if (extension == ".csv")
                        {
                            importData = ReadCsvFile(filePath);
                        }
                        else if (extension == ".xlsx" || extension == ".xls")
                        {
                            importData = ReadExcelFile(filePath);
                        }
                        else
                        {
                            XtraMessageBox.Show("Unsupported file format. Please select an Excel (.xlsx, .xls) or CSV (.csv) file.",
                                "Invalid File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (importData == null || importData.Rows.Count == 0)
                        {
                            XtraMessageBox.Show("The selected file is empty or could not be read.",
                                "No Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Validate columns
                        var (isValid, errorMessage, missingColumns) = _bllProducts.ValidateImportColumns(importData);
                        if (!isValid)
                        {
                            string columnInfo = GetRequiredColumnsInfo();
                            XtraMessageBox.Show($"{errorMessage}\n\n{columnInfo}",
                                "Column Validation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Confirm import
                        var confirmResult = XtraMessageBox.Show(
                            $"Found {importData.Rows.Count} product(s) to import.\n\nDo you want to proceed with the import?",
                            "Confirm Import", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (confirmResult != DialogResult.Yes)
                            return;

                        // Perform import
                        int userId = 1; // Default user
                        var (successCount, failedCount, errors) = _bllProducts.ImportProducts(importData, userId);

                        // Show results
                        StringBuilder resultMessage = new StringBuilder();
                        resultMessage.AppendLine($"Import completed!");
                        resultMessage.AppendLine($"Successfully imported: {successCount}");
                        resultMessage.AppendLine($"Failed: {failedCount}");

                        if (errors.Count > 0 && errors.Count <= 10)
                        {
                            resultMessage.AppendLine("\nErrors:");
                            foreach (var error in errors)
                            {
                                resultMessage.AppendLine($"• {error}");
                            }
                        }
                        else if (errors.Count > 10)
                        {
                            resultMessage.AppendLine($"\nFirst 10 errors (total {errors.Count}):");
                            for (int i = 0; i < 10; i++)
                            {
                                resultMessage.AppendLine($"• {errors[i]}");
                            }
                        }

                        MessageBoxIcon icon = failedCount == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning;
                        XtraMessageBox.Show(resultMessage.ToString(), "Import Results", MessageBoxButtons.OK, icon);

                        // Refresh grid
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error during import: {ex.Message}", "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable ReadExcelFile(string filePath)
        {
            DataTable dt = new DataTable();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                if (worksheet.Dimension == null)
                    return dt;

                // Read headers (first row)
                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    string columnName = worksheet.Cells[1, col].Text?.Trim().ToLower() ?? $"Column{col}";
                    if (!dt.Columns.Contains(columnName))
                        dt.Columns.Add(columnName);
                }

                // Read data rows
                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    DataRow dataRow = dt.NewRow();
                    bool hasData = false;

                    for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                    {
                        var cellValue = worksheet.Cells[row, col].Value;
                        if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()))
                        {
                            hasData = true;
                        }
                        dataRow[col - 1] = cellValue ?? DBNull.Value;
                    }

                    if (hasData)
                        dt.Rows.Add(dataRow);
                }
            }

            return dt;
        }

        private DataTable ReadCsvFile(string filePath)
        {
            DataTable dt = new DataTable();

            using (var reader = new StreamReader(filePath))
            {
                // Read header line
                string headerLine = reader.ReadLine();
                if (string.IsNullOrEmpty(headerLine))
                    return dt;

                string[] headers = ParseCsvLine(headerLine);
                foreach (var header in headers)
                {
                    string columnName = header.Trim().ToLower();
                    if (!dt.Columns.Contains(columnName))
                        dt.Columns.Add(columnName);
                }

                // Read data lines
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] values = ParseCsvLine(line);
                    DataRow dataRow = dt.NewRow();

                    for (int i = 0; i < Math.Min(values.Length, dt.Columns.Count); i++)
                    {
                        string value = values[i]?.Trim();
                        dataRow[i] = string.IsNullOrEmpty(value) ? DBNull.Value : value;
                    }

                    dt.Rows.Add(dataRow);
                }
            }

            return dt;
        }

        private string[] ParseCsvLine(string line)
        {
            var result = new System.Collections.Generic.List<string>();
            bool inQuotes = false;
            StringBuilder field = new StringBuilder();

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        field.Append('"');
                        i++;
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == ',' && !inQuotes)
                {
                    result.Add(field.ToString());
                    field.Clear();
                }
                else
                {
                    field.Append(c);
                }
            }

            result.Add(field.ToString());
            return result.ToArray();
        }

        private string GetRequiredColumnsInfo()
        {
            return @"Required columns:
• product_name (text) - Product name
• product_code (text) - Unique product code
• unit_id (number) - Unit ID from Unit table
• selling_price (number) - Selling price

Optional columns:
• barcode (text) - Product barcode
• product_type (text) - e.g., 'Standard', 'Service'
• category_id (number) - Category ID from Category table
• brand_id (number) - Brand ID from Brand table
• purchase_cost (number) - Purchase cost
• stock_quantity (number) - Initial stock quantity
• expiry_date (date) - Expiry date
• manufacture_date (date) - Manufacture date
• description (text) - Product description";
        }
    }
}
