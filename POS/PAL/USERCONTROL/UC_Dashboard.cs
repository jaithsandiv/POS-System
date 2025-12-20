using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using POS.BLL;
using System;
using System.Data;
using System.Windows.Forms;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_Dashboard : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_Dashboard _bll = new BLL_Dashboard();

        public UC_Dashboard()
        {
            InitializeComponent();
            
            // Configure DateFilterComboBox appearance to fix dropdown visibility issue
            // Set the main appearance
            DateFilterComboBox.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            DateFilterComboBox.Properties.Appearance.BackColor = System.Drawing.Color.White;
            
            // Set the dropdown appearance
            DateFilterComboBox.Properties.AppearanceDropDown.ForeColor = System.Drawing.Color.Black;
            DateFilterComboBox.Properties.AppearanceDropDown.BackColor = System.Drawing.Color.White;
            
            // Set disabled appearance (optional, for consistency)
            DateFilterComboBox.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Gray;
            DateFilterComboBox.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.WhiteSmoke;
            
            // Set focused appearance
            DateFilterComboBox.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Black;
            DateFilterComboBox.Properties.AppearanceFocused.BackColor = System.Drawing.Color.White;
            
            // Set readonly appearance
            DateFilterComboBox.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Black;
            DateFilterComboBox.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White;
            
            DateFilterComboBox.SelectedIndexChanged += DateFilterComboBox_SelectedIndexChanged;
            this.Load += UC_Dashboard_Load;
        }

        private void UC_Dashboard_Load(object sender, EventArgs e)
        {
            // Default to "This Month"
            DateFilterComboBox.SelectedIndex = 3; 
            LoadDashboardData();
        }

        private void DateFilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            try
            {
                GetDateRange(DateFilterComboBox.Text, out DateTime fromDate, out DateTime toDate);

                // 1. Load KPI Cards
                DataTable dtKpi = _bll.GetKpis(fromDate, toDate);
                if (dtKpi.Rows.Count > 0)
                {
                    DataRow row = dtKpi.Rows[0];
                    
                    // Map DB columns to UI Labels (Note: Variable names are legacy, Text is updated)
                    lblTotalSales.Text = $"Rs. {Convert.ToDecimal(row["TotalSales"]):N2}";
                    
                    // lblTotalPurchase -> Total Transactions
                    lblTotalPurchase.Text = row["TotalTransactions"].ToString();
                    
                    // lblNet -> Net Sales
                    lblNet.Text = $"Rs. {Convert.ToDecimal(row["NetSales"]):N2}";
                    
                    // lblPurchaseDue -> Low Stock Items
                    lblPurchaseDue.Text = row["LowStockCount"].ToString();
                    
                    lblInvoiceDue.Text = $"Rs. {Convert.ToDecimal(row["InvoiceDue"]):N2}";
                    
                    // lblTotalPurchaseReturn -> Avg Sale Value
                    lblTotalPurchaseReturn.Text = $"Rs. {Convert.ToDecimal(row["AvgSaleValue"]):N2}";
                    
                    lblTotalSalesReturn.Text = $"Rs. {Convert.ToDecimal(row["TotalSalesReturn"]):N2}";
                    
                    // lblExpense -> Total Cash Sales
                    lblExpense.Text = $"Rs. {Convert.ToDecimal(row["TotalCashSales"]):N2}";
                }

                // 2. Load Sales Trend Chart
                DataTable dtTrend = _bll.GetSalesTrend(fromDate, toDate);
                BindChart(chartControl1, dtTrend, "Argument", "Value", ViewType.Bar);

                // 3. Load Top Products Chart
                DataTable dtTop = _bll.GetTopProducts(fromDate, toDate);
                BindChart(chartControl2, dtTop, "Argument", "Value", ViewType.Doughnut);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error loading dashboard: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindChart(ChartControl chart, DataTable dt, string argumentField, string valueField, ViewType viewType)
        {
            chart.Series.Clear();
            Series series = new Series("Data", viewType);
            series.DataSource = dt;
            series.ArgumentDataMember = argumentField;
            series.ValueDataMembers.AddRange(new string[] { valueField });
            
            // Optional: Formatting
            if (viewType == ViewType.Doughnut)
            {
                series.Label.TextPattern = "{A}: {V}";
            }
            
            chart.Series.Add(series);
        }

        private void GetDateRange(string filter, out DateTime fromDate, out DateTime toDate)
        {
            DateTime now = DateTime.Now;
            toDate = now; // Default end is now

            switch (filter)
            {
                case "Yesterday":
                    fromDate = now.Date.AddDays(-1);
                    toDate = now.Date.AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);
                    break;
                case "Last 7 Days":
                    fromDate = now.Date.AddDays(-6);
                    break;
                case "Last 30 Days":
                    fromDate = now.Date.AddDays(-29);
                    break;
                case "This Month":
                    fromDate = new DateTime(now.Year, now.Month, 1);
                    break;
                case "Last Month":
                    fromDate = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
                    toDate = new DateTime(now.Year, now.Month, 1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);
                    break;
                case "This Year":
                    fromDate = new DateTime(now.Year, 1, 1);
                    break;
                case "Last Year":
                    fromDate = new DateTime(now.Year - 1, 1, 1);
                    toDate = new DateTime(now.Year, 1, 1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);
                    break;
                default: // Default to This Month
                    fromDate = new DateTime(now.Year, now.Month, 1);
                    break;
            }
        }
    }
}
