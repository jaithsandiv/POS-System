using DevExpress.XtraPrinting;
using System;
using System.Drawing;
using System.IO;

namespace POS.PAL.Helpers
{
    /// <summary>
    /// Helper class for configuring consistent report headers and footers across all exports and print operations.
    /// Provides business branding with logo, business name, report title, date/time, and standardized footer.
    /// </summary>
    public static class ReportHelper
    {
        /// <summary>
        /// Standard footer text for all system-generated reports
        /// </summary>
        public const string SystemGeneratedFooter = "This is a system generated Report.";

        /// <summary>
        /// Gets the business name from the application dataset
        /// </summary>
        /// <returns>Business name or default text if not available</returns>
        public static string GetBusinessName()
        {
            try
            {
                if (Main.DataSetApp?.Business != null && Main.DataSetApp.Business.Rows.Count > 0)
                {
                    var businessRow = Main.DataSetApp.Business[0];
                    if (!businessRow.Isbusiness_nameNull() && !string.IsNullOrWhiteSpace(businessRow.business_name))
                    {
                        return businessRow.business_name;
                    }
                }
            }
            catch
            {
                // Silently handle any errors accessing business data
            }
            return "POS System";
        }

        /// <summary>
        /// Gets the business logo as an Image from the application dataset
        /// </summary>
        /// <returns>Business logo image or null if not available</returns>
        public static Image GetBusinessLogo()
        {
            try
            {
                if (Main.DataSetApp?.Business != null && Main.DataSetApp.Business.Rows.Count > 0)
                {
                    var businessRow = Main.DataSetApp.Business[0];
                    if (!businessRow.IslogoNull() && businessRow.logo != null && businessRow.logo.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(businessRow.logo))
                        {
                            return Image.FromStream(ms);
                        }
                    }
                }
            }
            catch
            {
                // Silently handle any errors accessing logo data
            }
            return null;
        }

        /// <summary>
        /// Configures the header and footer for a PrintableComponentLink with business branding
        /// </summary>
        /// <param name="printLink">The PrintableComponentLink to configure</param>
        /// <param name="reportName">The name of the report to display in the header</param>
        public static void ConfigureReportHeaderFooter(PrintableComponentLink printLink, string reportName)
        {
            if (printLink == null) return;

            string businessName = GetBusinessName();
            string dateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            // Configure header
            PageHeaderFooter headerFooter = printLink.PageHeaderFooter as PageHeaderFooter;
            if (headerFooter != null)
            {
                // Clear existing header content
                headerFooter.Header.Content.Clear();
                
                // Header layout: [Business Name] [Report Name] [Date/Time]
                // Left: Business Name, Center: Report Name, Right: Date/Time
                headerFooter.Header.Content.AddRange(new string[] {
                    businessName,
                    reportName,
                    $"Generated: {dateTime}"
                });
                headerFooter.Header.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                headerFooter.Header.LineAlignment = BrickAlignment.Center;

                // Clear existing footer content
                headerFooter.Footer.Content.Clear();
                
                // Footer layout: [System Generated Text] [Page Numbers] [Empty]
                // Left: System generated text, Center: Page numbers, Right: Empty
                headerFooter.Footer.Content.AddRange(new string[] {
                    SystemGeneratedFooter,
                    "[Page # of Pages #]",
                    ""
                });
                headerFooter.Footer.Font = new Font("Segoe UI", 9);
                headerFooter.Footer.LineAlignment = BrickAlignment.Center;
            }
        }

        /// <summary>
        /// Configures the header and footer for a PrintableComponentLink with custom header text
        /// </summary>
        /// <param name="printLink">The PrintableComponentLink to configure</param>
        /// <param name="reportName">The name of the report to display in the header</param>
        /// <param name="additionalHeaderText">Additional text to include in the header (e.g., filter criteria)</param>
        public static void ConfigureReportHeaderFooter(PrintableComponentLink printLink, string reportName, string additionalHeaderText)
        {
            if (printLink == null) return;

            string businessName = GetBusinessName();
            string dateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            // Configure header
            PageHeaderFooter headerFooter = printLink.PageHeaderFooter as PageHeaderFooter;
            if (headerFooter != null)
            {
                // Clear existing header content
                headerFooter.Header.Content.Clear();
                
                // Include additional text if provided
                string headerCenter = string.IsNullOrWhiteSpace(additionalHeaderText) 
                    ? reportName 
                    : $"{reportName}\n{additionalHeaderText}";
                
                headerFooter.Header.Content.AddRange(new string[] {
                    businessName,
                    headerCenter,
                    $"Generated: {dateTime}"
                });
                headerFooter.Header.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                headerFooter.Header.LineAlignment = BrickAlignment.Center;

                // Clear existing footer content
                headerFooter.Footer.Content.Clear();
                
                headerFooter.Footer.Content.AddRange(new string[] {
                    SystemGeneratedFooter,
                    "[Page # of Pages #]",
                    ""
                });
                headerFooter.Footer.Font = new Font("Segoe UI", 9);
                headerFooter.Footer.LineAlignment = BrickAlignment.Center;
            }
        }

        /// <summary>
        /// Creates a standardized PrintableComponentLink with common settings for printing
        /// </summary>
        /// <param name="component">The component to print (typically a grid control)</param>
        /// <param name="reportName">The name of the report</param>
        /// <param name="landscape">Whether to use landscape orientation</param>
        /// <returns>Configured PrintableComponentLink ready for printing</returns>
        public static PrintableComponentLink CreatePrintLink(
            DevExpress.XtraPrinting.IPrintable component, 
            string reportName, 
            bool landscape = false)
        {
            var printingSystem = new PrintingSystem();
            var printLink = new PrintableComponentLink(printingSystem);
            
            printLink.Component = component;
            
            // Configure print settings
            printLink.Landscape = landscape;
            printLink.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
            
            // Set margins
            printLink.Margins.Left = 50;
            printLink.Margins.Right = 50;
            printLink.Margins.Top = 50;
            printLink.Margins.Bottom = 50;
            
            // Configure header and footer
            ConfigureReportHeaderFooter(printLink, reportName);
            
            // Create document
            printLink.CreateDocument();
            
            // Auto-fit to page width
            printLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            
            return printLink;
        }

        /// <summary>
        /// Creates a standardized PrintableComponentLink with reduced margins for reports with many columns
        /// </summary>
        /// <param name="component">The component to print (typically a grid control)</param>
        /// <param name="reportName">The name of the report</param>
        /// <param name="landscape">Whether to use landscape orientation</param>
        /// <returns>Configured PrintableComponentLink ready for printing</returns>
        public static PrintableComponentLink CreateCompactPrintLink(
            DevExpress.XtraPrinting.IPrintable component, 
            string reportName, 
            bool landscape = false)
        {
            var printingSystem = new PrintingSystem();
            var printLink = new PrintableComponentLink(printingSystem);
            
            printLink.Component = component;
            
            // Configure print settings
            printLink.Landscape = landscape;
            printLink.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
            
            // Set reduced margins for more space
            printLink.Margins.Left = 20;
            printLink.Margins.Right = 20;
            printLink.Margins.Top = 50;
            printLink.Margins.Bottom = 50;
            
            // Configure header and footer
            ConfigureReportHeaderFooter(printLink, reportName);
            
            // Create document
            printLink.CreateDocument();
            
            // Auto-fit to page width
            printLink.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            
            return printLink;
        }

        /// <summary>
        /// Gets formatted header text for exports (CSV, Excel, etc.) that don't support PrintableComponentLink
        /// </summary>
        /// <param name="reportName">The name of the report</param>
        /// <returns>Formatted header string</returns>
        public static string GetExportHeaderText(string reportName)
        {
            string businessName = GetBusinessName();
            string dateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            
            return $"{businessName} - {reportName} - Generated: {dateTime}";
        }
    }
}
