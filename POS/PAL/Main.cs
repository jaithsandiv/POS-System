using DevExpress.XtraEditors;
using POS.BLL;
using POS.DAL.DataSource;
using POS.PAL.USERCONTROL;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace POS
{
    public partial class Main : DevExpress.XtraEditors.XtraForm
    {
        public static Main Instance { get; private set; }

        private readonly BLL_Initialize _bllInitialize = new BLL_Initialize();

        public static DAL_DS_Initialize DataSetApp { get; private set; }

        // Sidebar state fields
        private bool isCollapsed = false;
        private const int collapsedWidth = 0;
        private const int expandedWidth = 250;
        private Timer animationTimer;

        public Main()
        {
            InitializeComponent();

            DataSetApp = new DAL_DS_Initialize();
            LoadBusinessData();
            UpdateBusinessName();

            Instance = this;

            if (DataSetApp.Business.Rows.Count > 0)
            {
                var firstBusinessRow = DataSetApp.Business[0];
                if (!firstBusinessRow.Isbusiness_idNull() && !string.IsNullOrWhiteSpace(firstBusinessRow.business_id))
                {
                    UC_Login login = new UC_Login();
                    LoadUserControl(login, hideNavigation: true);
                }
                else
                {
                    UC_Business_Registration businessRegistration = new UC_Business_Registration();
                    LoadUserControl(businessRegistration, hideNavigation: true);
                }
            }
            else
            {
                UC_Business_Registration businessRegistration = new UC_Business_Registration();
                LoadUserControl(businessRegistration, hideNavigation: true);
            }
        }

        private void LoadBusinessData()
        {
            try
            {
                DataSetApp.Business.Clear();
                DataSetApp.Business.Merge(_bllInitialize.GetBusiness());

                if (DataSetApp.Business.Rows.Count == 0)
                    DataSetApp.Business.AddBusinessRow(DataSetApp.Business.NewBusinessRow());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading business data: {ex.Message}", "Initialization Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateBusinessName()
        {
            try
            {
                if (DataSetApp.Business.Rows.Count > 0)
                {
                    var businessRow = DataSetApp.Business[0];
                    if (!businessRow.Isbusiness_nameNull() && !string.IsNullOrWhiteSpace(businessRow.business_name))
                    {
                        lblBusinessName.Text = businessRow.business_name;
                    }
                    else
                    {
                        lblBusinessName.Text = "Business Name";
                    }
                }
                else
                {
                    lblBusinessName.Text = "Business Name";
                }
            }
            catch (Exception ex)
            {
                lblBusinessName.Text = "Business Name";
            }
        }

        /// <summary>
        /// Loads a UserControl into the content panel while keeping topbar and sidebar persistent
        /// </summary>
        /// <param name="control">The UserControl to load</param>
        /// <param name="hideNavigation">Set to true to hide topbar and sidebar (for login/registration screens)</param>
        public void LoadUserControl(UserControl control, bool hideNavigation = false)
        {
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(control);
            control.Dock = DockStyle.Fill;

            // Show/hide navigation based on the parameter
            panelTopBar.Visible = !hideNavigation;
            panelSideBar.Visible = !hideNavigation;
        }

        /// <summary>
        /// Legacy method for backward compatibility - redirects to LoadUserControl
        /// </summary>
        [Obsolete("Use LoadUserControl instead")]
        public void SwitchToControl(UserControl control)
        {
            LoadUserControl(control);
        }

        private void btnToggleMenu_Click(object sender, EventArgs e)
        {
            if (isCollapsed)
                AnimateSidebar(true);
            else
                AnimateSidebar(false);
        }

        private void AnimateSidebar(bool expand)
        {
            // Stop any existing animation
            if (animationTimer != null)
            {
                animationTimer.Stop();
                animationTimer.Dispose();
                animationTimer = null;
            }

            animationTimer = new Timer { Interval = 10 };
            animationTimer.Tick += (s, e) =>
            {
                if (expand)
                {
                    panelSideBar.Width += 10;
                    if (panelSideBar.Width >= expandedWidth)
                    {
                        panelSideBar.Width = expandedWidth; // Ensure exact width
                        animationTimer.Stop();
                        animationTimer.Dispose();
                        animationTimer = null;
                        isCollapsed = false;

                        // Restore button text after expansion
                        foreach (Control ctrl in panelSideBar.Controls)
                        {
                            if (ctrl is SimpleButton btn)
                            {
                                btn.Text = btn.Tag?.ToString();
                            }
                        }
                    }
                }
                else
                {
                    panelSideBar.Width -= 10;
                    if (panelSideBar.Width <= collapsedWidth)
                    {
                        panelSideBar.Width = collapsedWidth; // Ensure exact width
                        animationTimer.Stop();
                        animationTimer.Dispose();
                        animationTimer = null;
                        isCollapsed = true;

                        // Hide button text after collapse
                        foreach (Control ctrl in panelSideBar.Controls)
                        {
                            if (ctrl is SimpleButton btn)
                            {
                                btn.Text = "";
                                btn.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
                            }
                        }
                    }
                }
            };
            animationTimer.Start();
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_SalesTerminal());
        }
        // Sidebar button click handlers
        private void btnHome_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_Dashboard());
        }

        private void btnUserManagement_Click(object sender, EventArgs e)
        {
            panelUserManagementSubmenu.Visible = !panelUserManagementSubmenu.Visible;
        }

        private void btnContacts_Click(object sender, EventArgs e)
        {
            panelContactsSubmenu.Visible = !panelContactsSubmenu.Visible;
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_Customer_Management());
        }

        private void btnCustomerGroups_Click(object sender, EventArgs e)
        {
            Main.Instance.LoadUserControl(new UC_CustomerGroup_Management());
        }
    }
}
