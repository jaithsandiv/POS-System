namespace POS.PAL.USERCONTROL
{
    partial class UC_Role_Management
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_Role_Management));
            panelControl1 = new DevExpress.XtraEditors.PanelControl();
            labelControl7 = new DevExpress.XtraEditors.LabelControl();
            labelControl14 = new DevExpress.XtraEditors.LabelControl();
            labelControl8 = new DevExpress.XtraEditors.LabelControl();
            panelControl4 = new DevExpress.XtraEditors.PanelControl();
            gridRoles = new DevExpress.XtraGrid.GridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            colRoleId = new DevExpress.XtraGrid.Columns.GridColumn();
            colRoleName = new DevExpress.XtraGrid.Columns.GridColumn();
            colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            colEdit = new DevExpress.XtraGrid.Columns.GridColumn();
            colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            txtSearch = new DevExpress.XtraEditors.TextEdit();
            btnSearch = new DevExpress.XtraEditors.SimpleButton();
            btnAddRoles = new DevExpress.XtraEditors.SimpleButton();
            btnExportPDF = new DevExpress.XtraEditors.SimpleButton();
            btnPrint = new DevExpress.XtraEditors.SimpleButton();
            btnExportExcel = new DevExpress.XtraEditors.SimpleButton();
            btnExportCSV = new DevExpress.XtraEditors.SimpleButton();
            separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl4).BeginInit();
            panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridRoles).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtSearch.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)separatorControl1).BeginInit();
            SuspendLayout();
            // 
            // panelControl1
            // 
            panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            panelControl1.Appearance.Options.UseBackColor = true;
            panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panelControl1.Controls.Add(labelControl7);
            panelControl1.Controls.Add(labelControl14);
            panelControl1.Controls.Add(labelControl8);
            panelControl1.Controls.Add(panelControl4);
            panelControl1.Controls.Add(separatorControl1);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Location = new System.Drawing.Point(0, 0);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(1920, 1001);
            panelControl1.TabIndex = 0;
            // 
            // labelControl7
            // 
            labelControl7.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            labelControl7.Appearance.Options.UseFont = true;
            labelControl7.Location = new System.Drawing.Point(72, 18);
            labelControl7.Name = "labelControl7";
            labelControl7.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            labelControl7.Size = new System.Drawing.Size(123, 40);
            labelControl7.TabIndex = 127;
            labelControl7.Text = "Manage your roles";
            // 
            // labelControl14
            // 
            labelControl14.Appearance.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            labelControl14.Appearance.Options.UseFont = true;
            labelControl14.Location = new System.Drawing.Point(14, 10);
            labelControl14.Name = "labelControl14";
            labelControl14.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            labelControl14.Size = new System.Drawing.Size(53, 50);
            labelControl14.TabIndex = 126;
            labelControl14.Text = "Roles";
            // 
            // labelControl8
            // 
            labelControl8.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            labelControl8.Appearance.Options.UseFont = true;
            labelControl8.Location = new System.Drawing.Point(13, 57);
            labelControl8.Name = "labelControl8";
            labelControl8.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            labelControl8.Size = new System.Drawing.Size(87, 40);
            labelControl8.TabIndex = 129;
            labelControl8.Text = "All your roles";
            // 
            // panelControl4
            // 
            panelControl4.Appearance.BackColor = System.Drawing.Color.White;
            panelControl4.Appearance.BorderColor = System.Drawing.Color.Black;
            panelControl4.Appearance.Options.UseBackColor = true;
            panelControl4.Appearance.Options.UseBorderColor = true;
            panelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panelControl4.Controls.Add(btnExportPDF);
            panelControl4.Controls.Add(btnPrint);
            panelControl4.Controls.Add(btnExportExcel);
            panelControl4.Controls.Add(btnExportCSV);
            panelControl4.Controls.Add(gridRoles);
            panelControl4.Controls.Add(txtSearch);
            panelControl4.Controls.Add(btnSearch);
            panelControl4.Controls.Add(btnAddRoles);
            panelControl4.Location = new System.Drawing.Point(14, 103);
            panelControl4.Name = "panelControl4";
            panelControl4.Size = new System.Drawing.Size(1894, 877);
            panelControl4.TabIndex = 128;
            // 
            // gridRoles
            // 
            gridRoles.Location = new System.Drawing.Point(28, 79);
            gridRoles.MainView = gridView1;
            gridRoles.Name = "gridRoles";
            gridRoles.Size = new System.Drawing.Size(1840, 795);
            gridRoles.TabIndex = 113;
            gridRoles.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            gridView1.Appearance.Row.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            gridView1.Appearance.Row.Options.UseFont = true;
            gridView1.ColumnPanelRowHeight = 44;
            gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { colRoleId, colRoleName, colDescription, colStatus, colEdit, colDelete });
            gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridView1.GridControl = gridRoles;
            gridView1.Name = "gridView1";
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsCustomization.AllowFilter = false;
            gridView1.OptionsCustomization.AllowGroup = false;
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ShowIndicator = false;
            gridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            gridView1.RowHeight = 44;
            // 
            // colRoleId
            // 
            colRoleId.AppearanceCell.Options.UseTextOptions = true;
            colRoleId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colRoleId.Caption = "Role ID";
            colRoleId.FieldName = "role_id";
            colRoleId.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            colRoleId.Name = "colRoleId";
            colRoleId.OptionsColumn.AllowEdit = false;
            colRoleId.OptionsColumn.AllowFocus = false;
            colRoleId.OptionsColumn.FixedWidth = true;
            colRoleId.Visible = true;
            colRoleId.VisibleIndex = 0;
            colRoleId.Width = 100;
            // 
            // colRoleName
            // 
            colRoleName.AppearanceCell.Options.UseTextOptions = true;
            colRoleName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colRoleName.Caption = "Role Name";
            colRoleName.FieldName = "role_name";
            colRoleName.Name = "colRoleName";
            colRoleName.OptionsColumn.AllowEdit = false;
            colRoleName.OptionsColumn.AllowFocus = false;
            colRoleName.Visible = true;
            colRoleName.VisibleIndex = 1;
            colRoleName.Width = 250;
            // 
            // colDescription
            // 
            colDescription.AppearanceCell.Options.UseTextOptions = true;
            colDescription.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colDescription.Caption = "Description";
            colDescription.FieldName = "description";
            colDescription.Name = "colDescription";
            colDescription.OptionsColumn.AllowEdit = false;
            colDescription.OptionsColumn.AllowFocus = false;
            colDescription.Visible = true;
            colDescription.VisibleIndex = 2;
            colDescription.Width = 400;
            // 
            // colStatus
            // 
            colStatus.AppearanceCell.Options.UseTextOptions = true;
            colStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colStatus.Caption = "Status";
            colStatus.FieldName = "status";
            colStatus.Name = "colStatus";
            colStatus.OptionsColumn.AllowEdit = false;
            colStatus.OptionsColumn.AllowFocus = false;
            colStatus.OptionsColumn.FixedWidth = true;
            colStatus.Visible = true;
            colStatus.VisibleIndex = 3;
            colStatus.Width = 100;
            // 
            // colEdit
            // 
            colEdit.AppearanceCell.Options.UseTextOptions = true;
            colEdit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colEdit.Caption = "Edit";
            colEdit.Name = "colEdit";
            colEdit.OptionsColumn.AllowEdit = true;
            colEdit.OptionsColumn.AllowFocus = true;
            colEdit.OptionsColumn.FixedWidth = true;
            colEdit.OptionsColumn.ShowCaption = true;
            colEdit.Visible = true;
            colEdit.VisibleIndex = 4;
            colEdit.Width = 80;
            // 
            // colDelete
            // 
            colDelete.AppearanceCell.Options.UseTextOptions = true;
            colDelete.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            colDelete.Caption = "Delete";
            colDelete.Name = "colDelete";
            colDelete.OptionsColumn.AllowEdit = true;
            colDelete.OptionsColumn.AllowFocus = true;
            colDelete.OptionsColumn.FixedWidth = true;
            colDelete.OptionsColumn.ShowCaption = true;
            colDelete.Visible = true;
            colDelete.VisibleIndex = 5;
            colDelete.Width = 80;
            // 
            // txtSearch
            // 
            txtSearch.Location = new System.Drawing.Point(28, 18);
            txtSearch.Name = "txtSearch";
            txtSearch.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            txtSearch.Properties.Appearance.Options.UseFont = true;
            txtSearch.Properties.Padding = new System.Windows.Forms.Padding(10);
            txtSearch.Size = new System.Drawing.Size(343, 44);
            txtSearch.TabIndex = 112;
            // 
            // btnSearch
            // 
            btnSearch.Appearance.BackColor = System.Drawing.Color.FromArgb(4, 181, 152);
            btnSearch.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnSearch.Appearance.Options.UseBackColor = true;
            btnSearch.Appearance.Options.UseFont = true;
            btnSearch.AppearanceHovered.Options.UseBackColor = true;
            btnSearch.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("btnSearch.ImageOptions.Image");
            btnSearch.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnSearch.Location = new System.Drawing.Point(371, 18);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new System.Drawing.Size(60, 44);
            btnSearch.TabIndex = 21;
            // 
            // btnAddRoles
            // 
            btnAddRoles.Appearance.BackColor = System.Drawing.Color.FromArgb(4, 181, 152);
            btnAddRoles.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            btnAddRoles.Appearance.Options.UseBackColor = true;
            btnAddRoles.Appearance.Options.UseFont = true;
            btnAddRoles.AppearanceHovered.Options.UseBackColor = true;
            btnAddRoles.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnAddRoles.Location = new System.Drawing.Point(1779, 31);
            btnAddRoles.Name = "btnAddRoles";
            btnAddRoles.Size = new System.Drawing.Size(89, 29);
            btnAddRoles.TabIndex = 10;
            btnAddRoles.Text = "Add";
            btnAddRoles.Click += btnAddRoles_Click;
            // 
            // btnExportPDF
            // 
            btnExportPDF.Appearance.BackColor = System.Drawing.Color.White;
            btnExportPDF.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            btnExportPDF.Appearance.ForeColor = System.Drawing.Color.Black;
            btnExportPDF.Appearance.Options.UseBackColor = true;
            btnExportPDF.Appearance.Options.UseFont = true;
            btnExportPDF.Appearance.Options.UseForeColor = true;
            btnExportPDF.Location = new System.Drawing.Point(1673, 31);
            btnExportPDF.Name = "btnExportPDF";
            btnExportPDF.Size = new System.Drawing.Size(100, 29);
            btnExportPDF.TabIndex = 120;
            btnExportPDF.Text = "Export PDF";
            // 
            // btnPrint
            // 
            btnPrint.Appearance.BackColor = System.Drawing.Color.White;
            btnPrint.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            btnPrint.Appearance.ForeColor = System.Drawing.Color.Black;
            btnPrint.Appearance.Options.UseBackColor = true;
            btnPrint.Appearance.Options.UseFont = true;
            btnPrint.Appearance.Options.UseForeColor = true;
            btnPrint.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            btnPrint.Location = new System.Drawing.Point(1355, 31);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new System.Drawing.Size(100, 29);
            btnPrint.TabIndex = 119;
            btnPrint.Text = "Print";
            // 
            // btnExportExcel
            // 
            btnExportExcel.Appearance.BackColor = System.Drawing.Color.White;
            btnExportExcel.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            btnExportExcel.Appearance.ForeColor = System.Drawing.Color.Black;
            btnExportExcel.Appearance.Options.UseBackColor = true;
            btnExportExcel.Appearance.Options.UseFont = true;
            btnExportExcel.Appearance.Options.UseForeColor = true;
            btnExportExcel.Location = new System.Drawing.Point(1567, 31);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new System.Drawing.Size(100, 29);
            btnExportExcel.TabIndex = 118;
            btnExportExcel.Text = "Export Excel";
            // 
            // btnExportCSV
            // 
            btnExportCSV.Appearance.BackColor = System.Drawing.Color.White;
            btnExportCSV.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            btnExportCSV.Appearance.ForeColor = System.Drawing.Color.Black;
            btnExportCSV.Appearance.Options.UseBackColor = true;
            btnExportCSV.Appearance.Options.UseFont = true;
            btnExportCSV.Appearance.Options.UseForeColor = true;
            btnExportCSV.Location = new System.Drawing.Point(1461, 31);
            btnExportCSV.Name = "btnExportCSV";
            btnExportCSV.Size = new System.Drawing.Size(100, 29);
            btnExportCSV.TabIndex = 117;
            btnExportCSV.Text = "Export CSV";
            // 
            // separatorControl1
            // 
            separatorControl1.BackColor = System.Drawing.Color.Transparent;
            separatorControl1.Location = new System.Drawing.Point(107, 66);
            separatorControl1.Name = "separatorControl1";
            separatorControl1.Size = new System.Drawing.Size(1801, 23);
            separatorControl1.TabIndex = 130;
            // 
            // UC_Role_Management
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panelControl1);
            Name = "UC_Role_Management";
            Size = new System.Drawing.Size(1920, 1001);
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl4).EndInit();
            panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridRoles).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtSearch.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)separatorControl1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraGrid.GridControl gridRoles;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colRoleId;
        private DevExpress.XtraGrid.Columns.GridColumn colRoleName;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colEdit;
        private DevExpress.XtraGrid.Columns.GridColumn colDelete;
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnAddRoles;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.SimpleButton btnExportPDF;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnExportExcel;
        private DevExpress.XtraEditors.SimpleButton btnExportCSV;
    }
}
