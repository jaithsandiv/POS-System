using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using DevExpress.XtraPrinting;
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
    public partial class UC_TrendingProducts_Report : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_SalesTerminal _bllSalesTerminal = new BLL_SalesTerminal();
        private DataTable trendingProductsTable;

        public UC_TrendingProducts_Report()
        {
            InitializeComponent();
            LoadTrendingProducts();
            
            // Apply permission-based visibility for print button
            ApplyExportButtonVisibility();
        }

        /// <summary>
        /// Apply permission-based visibility to export buttons
        /// </summary>
        private void ApplyExportButtonVisibility()
        {
            bool canExport = PermissionManager.HasPermission(PermissionManager.Permissions.VIEW_EXPORT_BUTTONS);
            if (btnPrint != null) btnPrint.Visible = canExport;
        }

        private void UC_TrendingProducts_Report_Load(object sender, EventArgs e)
        {
            // ...existing code...
        }

        /// <summary>
        /// Loads trending products data and binds to chart
        /// </summary>
        private void LoadTrendingProducts()
        {
            try
            {
                // Get trending products data
                trendingProductsTable = _bllSalesTerminal.GetTrendingProducts();

                if (trendingProductsTable == null || trendingProductsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No sales data available to display trending products.",
                        "No Data",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                // Bind data to chart
                BindChartData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading trending products: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Binds the trending products data to the bar chart
        /// </summary>
        private void BindChartData()
        {
            try
            {
                // Clear existing series
                chartTrendingProducts.Series.Clear();

                // Create new series for bar chart
                Series series = new Series("Trending Products", ViewType.Bar);
                
                // Set data source
                series.DataSource = trendingProductsTable;
                series.ArgumentDataMember = "ProductName";  // X-Axis (Product Name)
                series.ValueDataMembers.AddRange(new string[] { "TotalQuantitySold" });  // Y-Axis (Quantity)

                // Configure series label to show values on bars
                if (series.Label is SideBySideBarSeriesLabel barLabel)
                {
                    barLabel.Visible = true;
                    barLabel.TextPattern = "{V}";  // Show quantity value
                    barLabel.Position = BarSeriesLabelPosition.Top;
                }

                // Configure series view (bar appearance)
                if (series.View is SideBySideBarSeriesView barView)
                {
                    barView.Color = System.Drawing.Color.FromArgb(79, 129, 189);  // Professional blue color
                }

                // Add series to chart
                chartTrendingProducts.Series.Add(series);

                // Configure diagram (axes)
                if (chartTrendingProducts.Diagram is XYDiagram diagram)
                {
                    // X-Axis configuration
                    diagram.AxisX.Title.Text = "Product Name";
                    diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                    diagram.AxisX.Title.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                    diagram.AxisX.Label.Angle = -45;  // Rotate labels for better readability
                    diagram.AxisX.Label.Font = new Font("Segoe UI", 9F);
                    diagram.AxisX.Label.ResolveOverlappingOptions.AllowRotate = true;
                    diagram.AxisX.Label.ResolveOverlappingOptions.AllowStagger = false;

                    // Y-Axis configuration
                    diagram.AxisY.Title.Text = "Number of Items Sold";
                    diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                    diagram.AxisY.Title.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                    diagram.AxisY.Label.Font = new Font("Segoe UI", 9F);
                    diagram.AxisY.NumericScaleOptions.AutoGrid = true;
                    
                    // Enable gridlines for better readability
                    diagram.AxisY.GridLines.Visible = true;
                    diagram.AxisX.GridLines.Visible = false;
                }

                // Refresh chart
                chartTrendingProducts.RefreshData();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error binding chart data: {ex.Message}",
                    "Chart Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Handles print button click - prints the trending products chart
        /// </summary>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (trendingProductsTable == null || trendingProductsTable.Rows.Count == 0)
                {
                    XtraMessageBox.Show(
                        "No data available to print.",
                        "Print",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                // Create a printable component link for the chart
                PrintableComponentLink printLink = new PrintableComponentLink(new PrintingSystem());
                
                // Set the chart as the component to print
                printLink.Component = chartTrendingProducts;
                
                // Configure print settings for portrait orientation for better chart visibility
                printLink.Landscape = false;
                printLink.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
                
                // Set margins (reduced to maximize chart space)
                printLink.Margins.Left = 30;
                printLink.Margins.Right = 30;
                printLink.Margins.Top = 80;    // Space for header
                printLink.Margins.Bottom = 50; // Space for footer
                
                // Configure scaling to fit the chart to the page
                printLink.PrintingSystemBase.Document.AutoFitToPagesWidth = 1;  // Fit to page width
                
                // Create the document
                printLink.CreateDocument();
                
                // Enable image scaling for better quality
                printLink.PrintingSystemBase.ExportOptions.PrintPreview.DefaultFileName = $"TrendingProducts_{DateTime.Now:yyyyMMdd_HHmmss}";
                
                // Add header
                PageHeaderFooter header = printLink.PageHeaderFooter as PageHeaderFooter;
                if (header != null)
                {
                    header.Header.Content.Clear();
                    header.Header.Content.AddRange(new string[] {
                        "Trending Products Report",
                        "",
                        $"Generated: {DateTime.Now:dd/MM/yyyy HH:mm}"
                    });
                    header.Header.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    header.Header.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Center;
                }
                
                // Add footer with page numbers
                if (header != null)
                {
                    header.Footer.Content.Clear();
                    header.Footer.Content.AddRange(new string[] {
                        "",
                        "[Page # of Pages #]",
                        ""
                    });
                    header.Footer.Font = new Font("Segoe UI", 9);
                    header.Footer.LineAlignment = DevExpress.XtraPrinting.BrickAlignment.Center;
                }

                // Show print preview dialog with scaling options
                printLink.ShowPreviewDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error printing report: {ex.Message}",
                    "Print Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
