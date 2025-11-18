using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace POS.PAL.REPORT
{
    public partial class ThermalInvoice : DevExpress.XtraReports.UI.XtraReport
    {
        public ThermalInvoice()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Public property to access the payment subreport
        /// </summary>
        public XRSubreport PaymentSubreport => xrSubreport1;
    }
}
