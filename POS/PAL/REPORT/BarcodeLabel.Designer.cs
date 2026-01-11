namespace POS.PAL.REPORT
{
    partial class BarcodeLabel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // Initialize bands
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();

            // Initialize controls
            this.lblBusinessName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrBarCode = new DevExpress.XtraReports.UI.XRBarCode();
            this.lblProductName = new DevExpress.XtraReports.UI.XRLabel();
            this.lblPrice = new DevExpress.XtraReports.UI.XRLabel();
            this.lblPromoPrice = new DevExpress.XtraReports.UI.XRLabel();
            this.lblExpiry = new DevExpress.XtraReports.UI.XRLabel();
            this.lblManufacture = new DevExpress.XtraReports.UI.XRLabel();

            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();

            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";

            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";

            // 
            // Detail
            // Label size: 45mm x 35mm = ~170 x 132 pixels at 96 DPI
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
                this.lblBusinessName,
                this.xrBarCode,
                this.lblProductName,
                this.lblPrice,
                this.lblPromoPrice,
                this.lblExpiry,
                this.lblManufacture
            });
            this.Detail.HeightF = 132F;
            this.Detail.Name = "Detail";

            // 
            // lblBusinessName - Business name at top, centered
            // Position: X=2, Y=2, Width=166, Height=14
            // 
            this.lblBusinessName.LocationF = new DevExpress.Utils.PointFloat(2F, 2F);
            this.lblBusinessName.SizeF = new System.Drawing.SizeF(166F, 14F);
            this.lblBusinessName.Name = "lblBusinessName";
            this.lblBusinessName.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.lblBusinessName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lblBusinessName.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[business_name]")
            });
            this.lblBusinessName.WordWrap = false;
            this.lblBusinessName.AutoWidth = false;
            this.lblBusinessName.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);

            // 
            // xrBarCode - Full width barcode below business name
            // Position: X=2, Y=18, Width=166, Height=50
            // 
            this.xrBarCode.LocationF = new DevExpress.Utils.PointFloat(2F, 18F);
            this.xrBarCode.SizeF = new System.Drawing.SizeF(166F, 50F);
            this.xrBarCode.Name = "xrBarCode";
            this.xrBarCode.Symbology = new DevExpress.XtraPrinting.BarCode.Code128Generator();
            this.xrBarCode.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[barcode]")
            });
            this.xrBarCode.ShowText = true;
            this.xrBarCode.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular);
            this.xrBarCode.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
            this.xrBarCode.Module = 1.5F;
            this.xrBarCode.AutoModule = true;
            this.xrBarCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);

            // 
            // lblProductName - Below barcode, centered
            // Position: X=2, Y=70, Width=166, Height=16
            // 
            this.lblProductName.LocationF = new DevExpress.Utils.PointFloat(2F, 70F);
            this.lblProductName.SizeF = new System.Drawing.SizeF(166F, 16F);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.lblProductName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.lblProductName.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[product_name]")
            });
            this.lblProductName.WordWrap = false;
            this.lblProductName.AutoWidth = false;
            this.lblProductName.CanShrink = true;
            this.lblProductName.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);

            // 
            // lblPrice - Below product name, left side
            // Position: X=2, Y=88, Width=82, Height=16
            // 
            this.lblPrice.LocationF = new DevExpress.Utils.PointFloat(2F, 88F);
            this.lblPrice.SizeF = new System.Drawing.SizeF(82F, 16F);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.lblPrice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblPrice.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "'Rs.' + FormatString('{0:N2}', [selling_price])")
            });
            this.lblPrice.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);

            // 
            // lblPromoPrice - Right side of price row
            // Position: X=86, Y=88, Width=82, Height=16
            // 
            this.lblPromoPrice.LocationF = new DevExpress.Utils.PointFloat(86F, 88F);
            this.lblPromoPrice.SizeF = new System.Drawing.SizeF(82F, 16F);
            this.lblPromoPrice.Name = "lblPromoPrice";
            this.lblPromoPrice.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Bold);
            this.lblPromoPrice.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lblPromoPrice.ForeColor = System.Drawing.Color.Red;
            this.lblPromoPrice.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", 
                    "Iif([promo_discount_value] > 0, " +
                    "Iif([promo_discount_type] = 'PERCENTAGE', " +
                    "'Promo: -' + FormatString('{0:N0}', [promo_discount_value]) + '%', " +
                    "'Promo: -Rs.' + FormatString('{0:N2}', [promo_discount_value])), '')")
            });
            this.lblPromoPrice.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 2, 0, 0, 100F);

            // 
            // lblExpiry - Bottom row left
            // Position: X=2, Y=106, Width=82, Height=14
            // 
            this.lblExpiry.LocationF = new DevExpress.Utils.PointFloat(2F, 106F);
            this.lblExpiry.SizeF = new System.Drawing.SizeF(82F, 14F);
            this.lblExpiry.Name = "lblExpiry";
            this.lblExpiry.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Regular);
            this.lblExpiry.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.lblExpiry.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", 
                    "Iif(IsNull([expiry_date]), '', 'Exp: ' + FormatString('{0:dd/MM/yyyy}', [expiry_date]))")
            });
            this.lblExpiry.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100F);

            // 
            // lblManufacture - Bottom row right
            // Position: X=86, Y=106, Width=82, Height=14
            // 
            this.lblManufacture.LocationF = new DevExpress.Utils.PointFloat(86F, 106F);
            this.lblManufacture.SizeF = new System.Drawing.SizeF(82F, 14F);
            this.lblManufacture.Name = "lblManufacture";
            this.lblManufacture.Font = new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Regular);
            this.lblManufacture.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.lblManufacture.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
                new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", 
                    "Iif(IsNull([manufacture_date]), '', 'Mfg: ' + FormatString('{0:dd/MM/yyyy}', [manufacture_date]))")
            });
            this.lblManufacture.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 2, 0, 0, 100F);

            // 
            // BarcodeLabel
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
                this.TopMargin,
                this.Detail,
                this.BottomMargin
            });

            // Paper size: 45mm x 35mm (in pixels at 96 DPI: ~170 x 132)
            this.PageWidth = 170;  // ~45mm
            this.PageHeight = 132; // ~35mm
            this.Margins = new DevExpress.Drawing.DXMargins(0, 0, 0, 0);
            this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.Custom;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.Pixels;
            this.RollPaper = true;
            this.Version = "24.2";

            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
        }

        #endregion

        internal DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        internal DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        internal DevExpress.XtraReports.UI.DetailBand Detail;
        internal DevExpress.XtraReports.UI.XRLabel lblBusinessName;
        internal DevExpress.XtraReports.UI.XRBarCode xrBarCode;
        internal DevExpress.XtraReports.UI.XRLabel lblProductName;
        internal DevExpress.XtraReports.UI.XRLabel lblPrice;
        internal DevExpress.XtraReports.UI.XRLabel lblPromoPrice;
        internal DevExpress.XtraReports.UI.XRLabel lblExpiry;
        internal DevExpress.XtraReports.UI.XRLabel lblManufacture;
    }
}
