namespace POS.PAL.USERCONTROL
{
    partial class UC_Table_Management
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
            this.pnlInput = new DevExpress.XtraEditors.PanelControl();
            this.btnReset = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.nudCapacity = new DevExpress.XtraEditors.SpinEdit();
            this.lblCapacity = new DevExpress.XtraEditors.LabelControl();
            this.txtTableNumber = new DevExpress.XtraEditors.TextEdit();
            this.lblTableNumber = new DevExpress.XtraEditors.LabelControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.gridControlTables = new DevExpress.XtraGrid.GridControl();
            this.gridViewTables = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.pnlInput)).BeginInit();
            this.pnlInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCapacity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlTables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTables)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlInput
            // 
            this.pnlInput.Controls.Add(this.btnReset);
            this.pnlInput.Controls.Add(this.btnDelete);
            this.pnlInput.Controls.Add(this.btnUpdate);
            this.pnlInput.Controls.Add(this.btnAdd);
            this.pnlInput.Controls.Add(this.nudCapacity);
            this.pnlInput.Controls.Add(this.lblCapacity);
            this.pnlInput.Controls.Add(this.txtTableNumber);
            this.pnlInput.Controls.Add(this.lblTableNumber);
            this.pnlInput.Controls.Add(this.lblTitle);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInput.Location = new System.Drawing.Point(0, 0);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Size = new System.Drawing.Size(1000, 150);
            this.pnlInput.TabIndex = 0;
            // 
            // btnReset
            // 
            this.btnReset.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnReset.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Appearance.Options.UseBackColor = true;
            this.btnReset.Appearance.Options.UseFont = true;
            this.btnReset.Location = new System.Drawing.Point(380, 100);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(80, 30);
            this.btnReset.TabIndex = 8;
            this.btnReset.Text = "Reset";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnDelete.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Appearance.Options.UseBackColor = true;
            this.btnDelete.Appearance.Options.UseFont = true;
            this.btnDelete.Location = new System.Drawing.Point(290, 100);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 30);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnUpdate.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Appearance.Options.UseBackColor = true;
            this.btnUpdate.Appearance.Options.UseFont = true;
            this.btnUpdate.Location = new System.Drawing.Point(200, 100);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(80, 30);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(181)))), ((int)(((byte)(152)))));
            this.btnAdd.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Appearance.Options.UseBackColor = true;
            this.btnAdd.Appearance.Options.UseFont = true;
            this.btnAdd.Location = new System.Drawing.Point(110, 100);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(80, 30);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // nudCapacity
            // 
            this.nudCapacity.EditValue = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudCapacity.Location = new System.Drawing.Point(450, 60);
            this.nudCapacity.Name = "nudCapacity";
            this.nudCapacity.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.nudCapacity.Properties.Appearance.Options.UseFont = true;
            this.nudCapacity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.nudCapacity.Size = new System.Drawing.Size(120, 24);
            this.nudCapacity.TabIndex = 4;
            // 
            // lblCapacity
            // 
            this.lblCapacity.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblCapacity.Appearance.Options.UseFont = true;
            this.lblCapacity.Location = new System.Drawing.Point(380, 63);
            this.lblCapacity.Name = "lblCapacity";
            this.lblCapacity.Size = new System.Drawing.Size(54, 17);
            this.lblCapacity.TabIndex = 3;
            this.lblCapacity.Text = "Capacity:";
            // 
            // txtTableNumber
            // 
            this.txtTableNumber.Location = new System.Drawing.Point(110, 60);
            this.txtTableNumber.Name = "txtTableNumber";
            this.txtTableNumber.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtTableNumber.Properties.Appearance.Options.UseFont = true;
            this.txtTableNumber.Size = new System.Drawing.Size(200, 24);
            this.txtTableNumber.TabIndex = 2;
            // 
            // lblTableNumber
            // 
            this.lblTableNumber.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblTableNumber.Appearance.Options.UseFont = true;
            this.lblTableNumber.Location = new System.Drawing.Point(20, 63);
            this.lblTableNumber.Name = "lblTableNumber";
            this.lblTableNumber.Size = new System.Drawing.Size(88, 17);
            this.lblTableNumber.TabIndex = 1;
            this.lblTableNumber.Text = "Table Number:";
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(173, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Table Management";
            // 
            // gridControlTables
            // 
            this.gridControlTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlTables.Location = new System.Drawing.Point(0, 150);
            this.gridControlTables.MainView = this.gridViewTables;
            this.gridControlTables.Name = "gridControlTables";
            this.gridControlTables.Size = new System.Drawing.Size(1000, 550);
            this.gridControlTables.TabIndex = 1;
            this.gridControlTables.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewTables});
            // 
            // gridViewTables
            // 
            this.gridViewTables.GridControl = this.gridControlTables;
            this.gridViewTables.Name = "gridViewTables";
            this.gridViewTables.OptionsBehavior.Editable = false;
            this.gridViewTables.OptionsView.ShowGroupPanel = false;
            this.gridViewTables.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewTables_RowClick);
            // 
            // UC_Table_Management
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlTables);
            this.Controls.Add(this.pnlInput);
            this.Name = "UC_Table_Management";
            this.Size = new System.Drawing.Size(1000, 700);
            ((System.ComponentModel.ISupportInitialize)(this.pnlInput)).EndInit();
            this.pnlInput.ResumeLayout(false);
            this.pnlInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCapacity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTableNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlTables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTables)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlInput;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblTableNumber;
        private DevExpress.XtraEditors.TextEdit txtTableNumber;
        private DevExpress.XtraEditors.LabelControl lblCapacity;
        private DevExpress.XtraEditors.SpinEdit nudCapacity;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnUpdate;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnReset;
        private DevExpress.XtraGrid.GridControl gridControlTables;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTables;
    }
}
