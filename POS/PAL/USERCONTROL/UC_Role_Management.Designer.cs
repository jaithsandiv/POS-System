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
            txtSearch = new DevExpress.XtraEditors.TextEdit();
            btnSearch = new DevExpress.XtraEditors.SimpleButton();
            btnAddRoles = new DevExpress.XtraEditors.SimpleButton();
            btnExportPDF = new DevExpress.XtraEditors.SimpleButton();
            btnColumnVisibilty = new DevExpress.XtraEditors.SimpleButton();
            btnPrint = new DevExpress.XtraEditors.SimpleButton();
            btnExportExcel = new DevExpress.XtraEditors.SimpleButton();
            btnExportCSV = new DevExpress.XtraEditors.SimpleButton();
            panelControl3 = new DevExpress.XtraEditors.PanelControl();
            simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl4).BeginInit();
            panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridRoles).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtSearch.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelControl3).BeginInit();
            panelControl3.SuspendLayout();
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
            panelControl1.Controls.Add(panelControl3);
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
            labelControl8.Location = new System.Drawing.Point(13, 126);
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
            panelControl4.Controls.Add(gridRoles);
            panelControl4.Controls.Add(txtSearch);
            panelControl4.Controls.Add(btnSearch);
            panelControl4.Controls.Add(btnAddRoles);
            panelControl4.Controls.Add(btnExportPDF);
            panelControl4.Controls.Add(btnColumnVisibilty);
            panelControl4.Controls.Add(btnPrint);
            panelControl4.Controls.Add(btnExportExcel);
            panelControl4.Controls.Add(btnExportCSV);
            panelControl4.Location = new System.Drawing.Point(14, 172);
            panelControl4.Name = "panelControl4";
            panelControl4.Size = new System.Drawing.Size(1894, 818);
            panelControl4.TabIndex = 128;
            // 
            // gridRoles
            // 
            gridRoles.Location = new System.Drawing.Point(28, 79);
            gridRoles.MainView = gridView1;
            gridRoles.Name = "gridRoles";
            gridRoles.Size = new System.Drawing.Size(1840, 721);
            gridRoles.TabIndex = 113;
            gridRoles.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.GridControl = gridRoles;
            gridView1.Name = "gridView1";
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
            // 
            // btnExportPDF
            // 
            btnExportPDF.Location = new System.Drawing.Point(1033, 37);
            btnExportPDF.Name = "btnExportPDF";
            btnExportPDF.Size = new System.Drawing.Size(75, 23);
            btnExportPDF.TabIndex = 16;
            btnExportPDF.Text = "Export PDF";
            // 
            // btnColumnVisibilty
            // 
            btnColumnVisibilty.Location = new System.Drawing.Point(928, 37);
            btnColumnVisibilty.Name = "btnColumnVisibilty";
            btnColumnVisibilty.Size = new System.Drawing.Size(99, 23);
            btnColumnVisibilty.TabIndex = 15;
            btnColumnVisibilty.Text = "Column Visibilty";
            // 
            // btnPrint
            // 
            btnPrint.Location = new System.Drawing.Point(872, 37);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new System.Drawing.Size(50, 23);
            btnPrint.TabIndex = 14;
            btnPrint.Text = "Print";
            // 
            // btnExportExcel
            // 
            btnExportExcel.Location = new System.Drawing.Point(791, 37);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new System.Drawing.Size(75, 23);
            btnExportExcel.TabIndex = 13;
            btnExportExcel.Text = "Export Excel";
            // 
            // btnExportCSV
            // 
            btnExportCSV.Location = new System.Drawing.Point(710, 37);
            btnExportCSV.Name = "btnExportCSV";
            btnExportCSV.Size = new System.Drawing.Size(75, 23);
            btnExportCSV.TabIndex = 12;
            btnExportCSV.Text = "Export CSV";
            // 
            // panelControl3
            // 
            panelControl3.Appearance.BackColor = System.Drawing.Color.White;
            panelControl3.Appearance.Options.UseBackColor = true;
            panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panelControl3.Controls.Add(simpleButton1);
            panelControl3.Location = new System.Drawing.Point(14, 59);
            panelControl3.Name = "panelControl3";
            panelControl3.Size = new System.Drawing.Size(1894, 61);
            panelControl3.TabIndex = 125;
            // 
            // simpleButton1
            // 
            simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            simpleButton1.Appearance.Options.UseFont = true;
            simpleButton1.ImageOptions.Image = (System.Drawing.Image)resources.GetObject("simpleButton1.ImageOptions.Image");
            simpleButton1.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            simpleButton1.Location = new System.Drawing.Point(10, 5);
            simpleButton1.Name = "simpleButton1";
            simpleButton1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            simpleButton1.Size = new System.Drawing.Size(231, 51);
            simpleButton1.TabIndex = 0;
            simpleButton1.Text = "Filter";
            // 
            // separatorControl1
            // 
            separatorControl1.BackColor = System.Drawing.Color.Transparent;
            separatorControl1.Location = new System.Drawing.Point(107, 135);
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
            ((System.ComponentModel.ISupportInitialize)panelControl3).EndInit();
            panelControl3.ResumeLayout(false);
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
        private DevExpress.XtraEditors.TextEdit txtSearch;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnAddRoles;
        private DevExpress.XtraEditors.SimpleButton btnExportPDF;
        private DevExpress.XtraEditors.SimpleButton btnColumnVisibilty;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnExportExcel;
        private DevExpress.XtraEditors.SimpleButton btnExportCSV;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
    }
}
