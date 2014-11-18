using DevExpress.XtraEditors;

namespace Dalworth.Server.MainForm.CreateWork
{
    partial class CreateWorkView
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_cmbVans = new DevExpress.XtraEditors.LookUpEdit();
            this.m_lblTechnician = new DevExpress.XtraEditors.LabelControl();
            this.m_lblWorkDate = new DevExpress.XtraEditors.LabelControl();
            this.label4 = new DevExpress.XtraEditors.LabelControl();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.m_btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.m_gridEquipment = new DevExpress.XtraGrid.GridControl();
            this.m_gridViewEquipmet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_lblShortcut = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbVans.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridEquipment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewEquipmet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_cmbVans
            // 
            this.m_cmbVans.Location = new System.Drawing.Point(78, 89);
            this.m_cmbVans.Name = "m_cmbVans";
            this.m_cmbVans.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbVans.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LicensePlateNumber", "Name2", 5)});
            this.m_cmbVans.Properties.DisplayMember = "LicensePlateNumber";
            this.m_cmbVans.Properties.NullText = "[No Vans abailable]";
            this.m_cmbVans.Properties.PopupSizeable = false;
            this.m_cmbVans.Properties.PopupWidth = 100;
            this.m_cmbVans.Properties.ShowFooter = false;
            this.m_cmbVans.Properties.ShowHeader = false;
            this.m_cmbVans.Properties.ShowLines = false;
            this.m_cmbVans.Size = new System.Drawing.Size(157, 20);
            this.m_cmbVans.TabIndex = 5;
            // 
            // m_lblTechnician
            // 
            this.m_lblTechnician.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblTechnician.Appearance.Options.UseFont = true;
            this.m_lblTechnician.Location = new System.Drawing.Point(78, 58);
            this.m_lblTechnician.Name = "m_lblTechnician";
            this.m_lblTechnician.Size = new System.Drawing.Size(80, 13);
            this.m_lblTechnician.TabIndex = 3;
            this.m_lblTechnician.Text = "Shane, Hobbs";
            // 
            // m_lblWorkDate
            // 
            this.m_lblWorkDate.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblWorkDate.Appearance.Options.UseFont = true;
            this.m_lblWorkDate.Location = new System.Drawing.Point(78, 27);
            this.m_lblWorkDate.Name = "m_lblWorkDate";
            this.m_lblWorkDate.Size = new System.Drawing.Size(75, 13);
            this.m_lblWorkDate.TabIndex = 1;
            this.m_lblWorkDate.Text = "21 Feb, 2007";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Technician";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "&Van";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Work Date";
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.Location = new System.Drawing.Point(380, 130);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 2;
            this.m_btnOk.Text = "&OK";
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(461, 130);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 3;
            this.m_btnCancel.Text = "&Cancel";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.m_cmbVans);
            this.groupControl1.Controls.Add(this.m_lblWorkDate);
            this.groupControl1.Controls.Add(this.m_lblTechnician);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Location = new System.Drawing.Point(5, 5);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(244, 120);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Work Properties";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.m_lblShortcut);
            this.groupControl2.Controls.Add(this.m_gridEquipment);
            this.groupControl2.Location = new System.Drawing.Point(255, 5);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(281, 120);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Equipment";
            // 
            // m_gridEquipment
            // 
            this.m_gridEquipment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridEquipment.EmbeddedNavigator.Name = "";
            this.m_gridEquipment.Location = new System.Drawing.Point(2, 20);
            this.m_gridEquipment.MainView = this.m_gridViewEquipmet;
            this.m_gridEquipment.Name = "m_gridEquipment";
            this.m_gridEquipment.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.m_gridEquipment.Size = new System.Drawing.Size(277, 98);
            this.m_gridEquipment.TabIndex = 1;
            this.m_gridEquipment.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridViewEquipmet});
            // 
            // m_gridViewEquipmet
            // 
            this.m_gridViewEquipmet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.m_gridViewEquipmet.GridControl = this.m_gridEquipment;
            this.m_gridViewEquipmet.Name = "m_gridViewEquipmet";
            this.m_gridViewEquipmet.OptionsCustomization.AllowFilter = false;
            this.m_gridViewEquipmet.OptionsCustomization.AllowGroup = false;
            this.m_gridViewEquipmet.OptionsCustomization.AllowSort = false;
            this.m_gridViewEquipmet.OptionsMenu.EnableColumnMenu = false;
            this.m_gridViewEquipmet.OptionsNavigation.UseTabKey = false;
            this.m_gridViewEquipmet.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.m_gridViewEquipmet.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.m_gridViewEquipmet.OptionsView.ShowGroupPanel = false;
            this.m_gridViewEquipmet.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn1, DevExpress.Data.ColumnSortOrder.Descending)});
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Equipment Type";
            this.gridColumn1.FieldName = "EquipmentTypeText";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 134;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Qty";
            this.gridColumn2.ColumnEdit = this.repositoryItemTextEdit1;
            this.gridColumn2.FieldName = "Quantity";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 67;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Mask.EditMask = "\\d+";
            this.repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Controls.Add(this.groupControl2);
            this.panelControl1.Controls.Add(this.m_btnOk);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(542, 158);
            this.panelControl1.TabIndex = 0;
            // 
            // m_lblShortcut
            // 
            this.m_lblShortcut.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblShortcut.Location = new System.Drawing.Point(25, 46);
            this.m_lblShortcut.Name = "m_lblShortcut";
            this.m_lblShortcut.Size = new System.Drawing.Size(0, 0);
            this.m_lblShortcut.TabIndex = 0;
            this.m_lblShortcut.Text = "&B shortcut";
            // 
            // CreateWorkView
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(542, 158);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateWorkView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CreateWorkView";
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbVans.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridEquipment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewEquipmet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private LabelControl label2;        
        internal SimpleButton m_btnOk;
        internal SimpleButton m_btnCancel;
        private LabelControl label4;
        private LabelControl label1;
        internal LabelControl m_lblTechnician;
        internal LabelControl m_lblWorkDate;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        internal LookUpEdit m_cmbVans;
        private GroupControl groupControl2;
        private PanelControl panelControl1;
        internal DevExpress.XtraGrid.GridControl m_gridEquipment;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridViewEquipmet;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private LabelControl m_lblShortcut;
    }
}