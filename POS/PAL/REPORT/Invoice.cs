using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace POS.PAL.REPORT
{
    public partial class Invoice : DevExpress.XtraReports.UI.XtraReport
    {
        public Invoice()
        {
            InitializeComponent();
            LoadBusinessLogo();
            LoadBusinessName();
        }

        /// <summary>
        /// Public property to access the payment subreport
        /// </summary>
        public XRSubreport PaymentSubreport => xrSubreport1;

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
                            xrPictureBoxBusinessLogo.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        // Set to null if no logo exists
                        xrPictureBoxBusinessLogo.Image = null;
                    }
                }
            }
            catch (Exception)
            {
                // Silently handle any errors - report will show without logo
                xrPictureBoxBusinessLogo.Image = null;
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
