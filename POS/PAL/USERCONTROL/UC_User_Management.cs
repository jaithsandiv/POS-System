using DevExpress.XtraEditors;
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
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;

namespace POS.PAL.USERCONTROL
{
    public partial class UC_User_Management : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly BLL_User _bllUser = new BLL_User();
        private DataTable usersTable;

        public UC_User_Management()
        {
            InitializeComponent();
            ConfigureGrid();
            LoadUsers();
            
            // Wire up the Add button click event
            if (btnAddUsers != null)
            {
                btnAddUsers.Click += btnAddUsers_Click;
            }
        }

        /// <summary>
        /// Configures the grid columns to match database schema
        /// </summary>
        private void ConfigureGrid()
        {
            // Clear existing columns
            gridView1.Columns.Clear();

            // Add columns matching the database schema
            var colUserId = gridView1.Columns.AddVisible("user_id", "User ID");
            colUserId.FieldName = "user_id";
            colUserId.Caption = "User ID";
            colUserId.Width = 80;
            colUserId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colUserId.OptionsColumn.AllowEdit = false;
            colUserId.OptionsColumn.AllowFocus = false;
            colUserId.OptionsColumn.FixedWidth = true;

            var colFullName = gridView1.Columns.AddVisible("full_name", "Full Name");
            colFullName.FieldName = "full_name";
            colFullName.Caption = "Full Name";
            colFullName.Width = 200;
            colFullName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colFullName.OptionsColumn.AllowEdit = false;
            colFullName.OptionsColumn.AllowFocus = false;
            colFullName.OptionsColumn.FixedWidth = true;

            var colUsername = gridView1.Columns.AddVisible("username", "Username");
            colUsername.FieldName = "username";
            colUsername.Caption = "Username";
            colUsername.Width = 150;
            colUsername.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colUsername.OptionsColumn.AllowEdit = false;
            colUsername.OptionsColumn.AllowFocus = false;
            colUsername.OptionsColumn.FixedWidth = true;

            var colEmail = gridView1.Columns.AddVisible("email", "Email");
            colEmail.FieldName = "email";
            colEmail.Caption = "Email";
            colEmail.Width = 200;
            colEmail.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colEmail.OptionsColumn.AllowEdit = false;
            colEmail.OptionsColumn.AllowFocus = false;
            colEmail.OptionsColumn.FixedWidth = true;

            var colPhone = gridView1.Columns.AddVisible("phone", "Phone");
            colPhone.FieldName = "phone";
            colPhone.Caption = "Phone";
            colPhone.Width = 150;
            colPhone.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colPhone.OptionsColumn.AllowEdit = false;
            colPhone.OptionsColumn.AllowFocus = false;
            colPhone.OptionsColumn.FixedWidth = true;

            var colStoreName = gridView1.Columns.AddVisible("store_name", "Store");
            colStoreName.FieldName = "store_name";
            colStoreName.Caption = "Store";
            colStoreName.Width = 150;
            colStoreName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colStoreName.OptionsColumn.AllowEdit = false;
            colStoreName.OptionsColumn.AllowFocus = false;
            colStoreName.OptionsColumn.FixedWidth = true;

            var colRoleName = gridView1.Columns.AddVisible("role_name", "Role");
            colRoleName.FieldName = "role_name";
            colRoleName.Caption = "Role";
            colRoleName.Width = 120;
            colRoleName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colRoleName.OptionsColumn.AllowEdit = false;
            colRoleName.OptionsColumn.AllowFocus = false;
            colRoleName.OptionsColumn.FixedWidth = true;

            var colStatus = gridView1.Columns.AddVisible("status", "Status");
            colStatus.FieldName = "status";
            colStatus.Caption = "Status";
            colStatus.Width = 80;
            colStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colStatus.OptionsColumn.AllowEdit = false;
            colStatus.OptionsColumn.AllowFocus = false;
            colStatus.OptionsColumn.FixedWidth = true;

            // Add Edit button column (using repository item from Designer)
            var colEdit = gridView1.Columns.AddVisible("Edit", "Edit");
            colEdit.Caption = "Edit";
            colEdit.Width = 80;
            colEdit.ColumnEdit = repositoryItemButtonEdit_Edit;
            colEdit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colEdit.OptionsColumn.AllowEdit = true;
            colEdit.OptionsColumn.AllowFocus = true;
            colEdit.OptionsColumn.FixedWidth = true;
            colEdit.OptionsColumn.ShowCaption = true;

            // Add Delete button column (using repository item from Designer)
            var colDelete = gridView1.Columns.AddVisible("Delete", "Delete");
            colDelete.Caption = "Delete";
            colDelete.Width = 80;
            colDelete.ColumnEdit = repositoryItemButtonEdit_Delete;
            colDelete.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colDelete.OptionsColumn.AllowEdit = true;
            colDelete.OptionsColumn.AllowFocus = true;
            colDelete.OptionsColumn.FixedWidth = true;
            colDelete.OptionsColumn.ShowCaption = true;

            // Configure button editors (now uses Designer-created repository items)
            ConfigureButtonEditors();

            // Note: Grid appearance styling is now set in Designer.cs for design-time visibility
            // Runtime behavior remains identical due to ConfigureGrid being called in constructor

            // Set default sort order by user_id ascending
            colUserId.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;

            // Enable double-click to edit
            gridView1.DoubleClick += GridView1_DoubleClick;

            // Add context menu for Edit and Delete
            CreateContextMenu();
        }

        /// <summary>
        /// Configures the button editors for Edit and Delete columns
        /// </summary>
        private void ConfigureButtonEditors()
        {
            // Configure Edit button (using Designer-created repository item)
            repositoryItemButtonEdit_Edit.Buttons.Clear();
            var editButton = new EditorButton(ButtonPredefines.Glyph);
            editButton.Caption = "Edit";
            editButton.Kind = ButtonPredefines.Glyph;
            repositoryItemButtonEdit_Edit.Buttons.Add(editButton);
            repositoryItemButtonEdit_Edit.TextEditStyle = TextEditStyles.HideTextEditor;
            repositoryItemButtonEdit_Edit.ButtonClick += RepositoryItemButtonEdit_Edit_ButtonClick;

            // Configure Delete button (using Designer-created repository item)
            repositoryItemButtonEdit_Delete.Buttons.Clear();
            var deleteButton = new EditorButton(ButtonPredefines.Glyph);
            deleteButton.Caption = "Delete";
            deleteButton.Kind = ButtonPredefines.Glyph;
            repositoryItemButtonEdit_Delete.Buttons.Add(deleteButton);
            repositoryItemButtonEdit_Delete.TextEditStyle = TextEditStyles.HideTextEditor;
            repositoryItemButtonEdit_Delete.ButtonClick += RepositoryItemButtonEdit_Delete_ButtonClick;
        }

        /// <summary>
        /// Handle Edit button click in grid
        /// </summary>
        private void RepositoryItemButtonEdit_Edit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            EditSelectedUser();
        }

        /// <summary>
        /// Handle Delete button click in grid
        /// </summary>
        private void RepositoryItemButtonEdit_Delete_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            DeleteSelectedUser();
        }

        /// <summary>
        /// Creates context menu for grid operations
        /// </summary>
        private void CreateContextMenu()
        {
            var contextMenu = new ContextMenuStrip();

            var editMenuItem = new ToolStripMenuItem("Edit");
            editMenuItem.Click += EditMenuItem_Click;
            contextMenu.Items.Add(editMenuItem);

            var deleteMenuItem = new ToolStripMenuItem("Delete");
            deleteMenuItem.Click += DeleteMenuItem_Click;
            contextMenu.Items.Add(deleteMenuItem);

            var refreshMenuItem = new ToolStripMenuItem("Refresh");
            refreshMenuItem.Click += (s, e) => LoadUsers();
            contextMenu.Items.Add(refreshMenuItem);

            gridUsers.ContextMenuStrip = contextMenu;
        }

        /// <summary>
        /// Loads all users from database
        /// </summary>
        public void LoadUsers()
        {
            try
            {
                usersTable = _bllUser.GetUsers();
                gridUsers.DataSource = usersTable;
                gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error loading users: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Handle double-click to edit
        /// </summary>
        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            EditSelectedUser();
        }

        /// <summary>
        /// Handle Edit menu item click
        /// </summary>
        private void EditMenuItem_Click(object sender, EventArgs e)
        {
            EditSelectedUser();
        }

        /// <summary>
        /// Handle Delete menu item click
        /// </summary>
        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedUser();
        }

        /// <summary>
        /// Edits the selected user
        /// </summary>
        private void EditSelectedUser()
        {
            try
            {
                if (gridView1.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("Please select a user to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow selectedRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (selectedRow == null)
                    return;

                int userId = Convert.ToInt32(selectedRow["user_id"]);

                // TODO: Navigate to user edit form when implemented
                XtraMessageBox.Show(
                    $"User edit functionality for User ID {userId} will be implemented in the user registration form.",
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Uncomment when user registration form is ready:
                // var registrationForm = new UC_User_Registration(userId);
                // Main.Instance.LoadUserControl(registrationForm);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error editing user: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Deletes the selected user
        /// </summary>
        private void DeleteSelectedUser()
        {
            try
            {
                if (gridView1.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("Please select a user to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow selectedRow = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (selectedRow == null)
                    return;

                int userId = Convert.ToInt32(selectedRow["user_id"]);
                string fullName = selectedRow["full_name"]?.ToString();

                // Confirm deletion
                var result = XtraMessageBox.Show(
                    $"Are you sure you want to delete the user '{fullName}'?\n\nThis will mark the user as inactive.",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result != DialogResult.Yes)
                    return;

                // Get current user ID
                int currentUserId = 1; // Default
                if (Main.DataSetApp?.User != null && Main.DataSetApp.User.Rows.Count > 0)
                {
                    currentUserId = Convert.ToInt32(Main.DataSetApp.User[0]["user_id"]);
                }

                // Delete the user (soft delete)
                bool success = _bllUser.DeleteUser(userId, currentUserId);

                if (success)
                {
                    XtraMessageBox.Show(
                        $"User '{fullName}' deleted successfully.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Reload the grid
                    LoadUsers();
                }
                else
                {
                    XtraMessageBox.Show(
                        "Failed to delete user.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    $"Error deleting user: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        /// <summary>
        /// Handle Add Users button click
        /// </summary>
        private void btnAddUsers_Click(object sender, EventArgs e)
        {
            // TODO: Navigate to user registration form when ready
            XtraMessageBox.Show(
                "User registration functionality will be implemented in the user registration form.",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            
            // Uncomment when user registration form is ready:
            // Main.Instance.LoadUserControl(new UC_User_Registration());
        }
    }
}
