using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Data;

namespace POS.PAL.REPORT
{
    public partial class ThermalInvoice : DevExpress.XtraReports.UI.XtraReport
    {
        public ThermalInvoice()
        {
            InitializeComponent();
            LoadBusinessLogo();
            LoadBusinessName();
            
            // Subscribe to BeforePrint event to calculate total discount
            this.BeforePrint += ThermalInvoice_BeforePrint;
        }

        /// <summary>
        /// Public property to access the payment subreport
        /// </summary>
        public XRSubreport PaymentSubreport => xrSubreport1;

        /// <summary>
        /// Calculate and display total discount including product-level and sale-level discounts
        /// </summary>
        private void ThermalInvoice_BeforePrint(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                // Clear any existing expression bindings on xrLabel18 so we can set text directly
                xrLabel18.ExpressionBindings.Clear();
                
                decimal totalDiscount = CalculateTotalDiscount();
                
                // Update xrLabel18 to show total discount with proper formatting
                xrLabel18.Text = $"-{totalDiscount:F2}";
            }
            catch (Exception)
            {
                // In case of error, fall back to parameter value
                xrLabel18.ExpressionBindings.Clear();
                xrLabel18.Text = this.Parameters["p_discount"]?.Value?.ToString() ?? "0.00";
            }
        }

        /// <summary>
        /// Calculate total discount from both product-level and sale-level discounts
        /// </summary>
        private decimal CalculateTotalDiscount()
        {
            decimal productLevelDiscount = 0m;
            decimal saleLevelDiscount = 0m;

            // Calculate product-level discounts (difference between original price and discounted subtotal)
            if (this.DataSource != null)
            {
                DataTable itemsTable = null;

                if (this.DataSource is DataTable dt)
                {
                    itemsTable = dt;
                }
                else if (this.DataSource is DataView dv)
                {
                    itemsTable = dv.Table;
                }

                if (itemsTable != null)
                {
                    foreach (DataRow item in itemsTable.Rows)
                    {
                        if (item.RowState == DataRowState.Deleted)
                            continue;

                        decimal unitPrice = 0m;
                        decimal quantity = 0m;
                        decimal subtotal = 0m;

                        if (decimal.TryParse(item["unit_price"]?.ToString(), out unitPrice) &&
                            decimal.TryParse(item["quantity"]?.ToString(), out quantity) &&
                            decimal.TryParse(item["subtotal"]?.ToString(), out subtotal))
                        {
                            // Product discount = (unit_price * quantity) - subtotal
                            decimal itemDiscount = (unitPrice * quantity) - subtotal;
                            productLevelDiscount += itemDiscount;
                        }
                    }
                }
            }

            // Get sale-level discount from parameter
            if (this.Parameters["p_discount"]?.Value != null)
            {
                string discountStr = this.Parameters["p_discount"].Value.ToString().Replace("-", "").Trim();
                if (decimal.TryParse(discountStr, out decimal discountValue))
                {
                    saleLevelDiscount = discountValue;
                }
            }

            // Total discount = product-level + sale-level discounts
            return productLevelDiscount + saleLevelDiscount;
        }

        /// <summary>
        /// Load and display the business logo from the database
        /// </summary>
        private void LoadBusinessLogo()
        {
            try
            {
                if (Main.DataSetApp?.Business != null && Main.DataSetApp.Business.Rows.Count > 0)
                {
                    var businessRow = Main.DataSetApp.Business[0];

                    // Load logo image
                    if (!businessRow.IslogoNull() && businessRow.logo != null && businessRow.logo.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(businessRow.logo))
                        {
                            xrPictureBoxLogo.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        // Set to null if no logo exists
                        xrPictureBoxLogo.Image = null;
                    }
                }
            }
            catch (Exception)
            {
                // Silently handle any errors - report will show without logo
                xrPictureBoxLogo.Image = null;
            }
        }

        /// <summary>
        /// Load and display the business name from the database
        /// </summary>
        private void LoadBusinessName()
        {
            try
            {
                if (Main.DataSetApp?.Business != null && Main.DataSetApp.Business.Rows.Count > 0)
                {
                    var businessRow = Main.DataSetApp.Business[0];

                    // Load business name
                    if (!businessRow.Isbusiness_nameNull() && !string.IsNullOrWhiteSpace(businessRow.business_name))
                    {
                        xrLabelBusinessName.Text = businessRow.business_name;
                    }
                    else
                    {
                        // Set default text if no business name exists
                        xrLabelBusinessName.Text = "Business Name";
                    }
                }
                else
                {
                    // Set default text if no business data exists
                    xrLabelBusinessName.Text = "Business Name";
                }
            }
            catch (Exception)
            {
                // Silently handle any errors - report will show default text
                xrLabelBusinessName.Text = "Business Name";
            }
        }
    }
}
