using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using POS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace POS
{
    internal static class Program
    {
        private static readonly BLL_SystemLog _logManager = new BLL_SystemLog();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Set up global exception handlers
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            try
            {
                Application.Run(new POS.Main());
            }
            catch (Exception ex)
            {
                // Log any unhandled exception during application startup
                LogFatalError(ex);
                MessageBox.Show(
                    $"A fatal error occurred:\n\n{ex.Message}\n\nThe application will now close.",
                    "Fatal Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Handles exceptions from UI thread
        /// </summary>
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            try
            {
                _logManager.LogError(
                    source: "SYSTEM",
                    ex: e.Exception,
                    referenceId: null,
                    userId: GetCurrentUserId()
                );

                MessageBox.Show(
                    $"An error occurred:\n\n{e.Exception.Message}\n\nThe error has been logged.",
                    "Application Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch
            {
                // If logging fails, show a basic error message
                MessageBox.Show(
                    $"A critical error occurred:\n\n{e.Exception.Message}",
                    "Critical Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Handles unhandled exceptions from non-UI threads
        /// </summary>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                if (e.ExceptionObject is Exception ex)
                {
                    _logManager.LogError(
                        source: "SYSTEM",
                        ex: ex,
                        referenceId: null,
                        userId: GetCurrentUserId()
                    );

                    MessageBox.Show(
                        $"A fatal error occurred:\n\n{ex.Message}\n\nThe application will now close.",
                        "Fatal Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch
            {
                // Last resort error message
                MessageBox.Show(
                    "A critical error occurred and the application must close.",
                    "Critical Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Logs fatal errors that occur during application startup
        /// </summary>
        private static void LogFatalError(Exception ex)
        {
            try
            {
                _logManager.LogError(
                    source: "SYSTEM",
                    ex: ex,
                    referenceId: null,
                    userId: GetCurrentUserId()
                );
            }
            catch
            {
                // If logging fails, silently continue
            }
        }

        /// <summary>
        /// Gets the current user ID if available
        /// </summary>
        private static int? GetCurrentUserId()
        {
            try
            {
                if (POS.Main.DataSetApp?.User != null && POS.Main.DataSetApp.User.Rows.Count > 0)
                {
                    var userRow = POS.Main.DataSetApp.User[0];
                    if (!userRow.Isuser_idNull())
                    {
                        if (int.TryParse(userRow.user_id, out int userId))
                        {
                            return userId;
                        }
                    }
                }
            }
            catch
            {
                // If we can't get the user ID, return null
            }
            return null;
        }
    }
}
