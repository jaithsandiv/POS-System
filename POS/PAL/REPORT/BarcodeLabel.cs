using DevExpress.XtraReports.UI;
using System;
using System.ComponentModel;
using System.Drawing;

namespace POS.PAL.REPORT
{
    /// <summary>
    /// Barcode label report for product barcode printing
    /// Label size: 40mm x 20mm
    /// </summary>
    public partial class BarcodeLabel : DevExpress.XtraReports.UI.XtraReport
    {
        public BarcodeLabel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Configures label display based on include flags
        /// </summary>
        /// <param name="includeName">Whether to show product name</param>
        /// <param name="includePrice">Whether to show price</param>
        /// <param name="includeExpiry">Whether to show expiry date</param>
        /// <param name="includeManufacture">Whether to show manufacture date</param>
        /// <param name="includePromo">Whether to show promotional price</param>
        public void ConfigureLabel(bool includeName, bool includePrice, bool includeExpiry, 
                                   bool includeManufacture, bool includePromo)
        {
            lblProductName.Visible = includeName;
            lblPrice.Visible = includePrice;
            lblExpiry.Visible = includeExpiry;
            lblManufacture.Visible = includeManufacture;
            lblPromoPrice.Visible = includePromo;
        }
    }
}
