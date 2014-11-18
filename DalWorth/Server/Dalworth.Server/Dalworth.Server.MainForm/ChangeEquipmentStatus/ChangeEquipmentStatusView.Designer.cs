namespace Dalworth.Server.MainForm.ChangeEquipmentStatus
{
    partial class ChangeEquipmentStatusView
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
            this.components = new System.ComponentModel.Container();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.m_cmbStatus = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.m_lblShortcut = new DevExpress.XtraEditors.LabelControl();
            this.m_gridEquipment = new DevExpress.XtraGrid.GridControl();
            this.m_gridEquipmentView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_gridCmbStatus = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.m_btnReset = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridEquipment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridEquipmentView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridCmbStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.groupControl2);
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Controls.Add(this.m_btnOk);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(596, 311);
            this.panelControl1.TabIndex = 0;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Controls.Add(this.m_txtNotes);
            this.groupControl2.Controls.Add(this.m_cmbStatus);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Location = new System.Drawing.Point(349, 5);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(242, 271);
            this.groupControl2.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 49);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(28, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "&Notes";
            // 
            // m_txtNotes
            // 
            this.m_txtNotes.Location = new System.Drawing.Point(66, 49);
            this.m_txtNotes.Name = "m_txtNotes";
            this.m_txtNotes.Properties.MaxLength = 200;
            this.m_txtNotes.Size = new System.Drawing.Size(170, 217);
            this.m_txtNotes.TabIndex = 7;
            // 
            // m_cmbStatus
            // 
            this.m_cmbStatus.Location = new System.Drawing.Point(66, 23);
            this.m_cmbStatus.Name = "m_cmbStatus";
            this.m_cmbStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbStatus.Properties.NullText = "[Multiple Set]";
            this.m_cmbStatus.Size = new System.Drawing.Size(170, 20);
            this.m_cmbStatus.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(5, 26);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(31, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "&Status";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.m_lblShortcut);
            this.groupControl1.Controls.Add(this.m_gridEquipment);
            this.groupControl1.Controls.Add(this.panelControl3);
            this.groupControl1.Location = new System.Drawing.Point(5, 5);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(338, 271);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Equipment";
            // 
            // m_lblShortcut
            // 
            this.m_lblShortcut.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblShortcut.Location = new System.Drawing.Point(57, 49);
            this.m_lblShortcut.Name = "m_lblShortcut";
            this.m_lblShortcut.Size = new System.Drawing.Size(0, 0);
            this.m_lblShortcut.TabIndex = 0;
            this.m_lblShortcut.Text = "&B Shortcut";
            // 
            // m_gridEquipment
            // 
            this.m_gridEquipment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridEquipment.EmbeddedNavigator.Name = "";
            this.m_gridEquipment.Location = new System.Drawing.Point(2, 20);
            this.m_gridEquipment.MainView = this.m_gridEquipmentView;
            this.m_gridEquipment.Name = "m_gridEquipment";
            this.m_gridEquipment.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.m_gridCmbStatus});
            this.m_gridEquipment.ShowOnlyPredefinedDetails = true;
            this.m_gridEquipment.Size = new System.Drawing.Size(334, 214);
            this.m_gridEquipment.TabIndex = 1;
            this.m_gridEquipment.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridEquipmentView});
            // 
            // m_gridEquipmentView
            // 
            this.m_gridEquipmentView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn11,
            this.gridColumn13,
            this.gridColumn14});
            this.m_gridEquipmentView.GridControl = this.m_gridEquipment;
            this.m_gridEquipmentView.Name = "m_gridEquipmentView";
            this.m_gridEquipmentView.OptionsCustomization.AllowFilter = false;
            this.m_gridEquipmentView.OptionsCustomization.AllowGroup = false;
            this.m_gridEquipmentView.OptionsNavigation.UseTabKey = false;
            this.m_gridEquipmentView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Serial Number";
            this.gridColumn11.FieldName = "SerialNumber";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 0;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Type";
            this.gridColumn13.FieldName = "TypeText";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 1;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Status";
            this.gridColumn14.ColumnEdit = this.m_gridCmbStatus;
            this.gridColumn14.FieldName = "StatusId";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 2;
            // 
            // m_gridCmbStatus
            // 
            this.m_gridCmbStatus.AutoHeight = false;
            this.m_gridCmbStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_gridCmbStatus.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("In Service", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Retired", 2, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Broken", 3, -1)});
            this.m_gridCmbStatus.Name = "m_gridCmbStatus";
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.m_btnReset);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(2, 234);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(334, 35);
            this.panelControl3.TabIndex = 2;
            // 
            // m_btnReset
            // 
            this.m_btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnReset.Location = new System.Drawing.Point(256, 6);
            this.m_btnReset.Name = "m_btnReset";
            this.m_btnReset.Size = new System.Drawing.Size(75, 23);
            this.m_btnReset.TabIndex = 3;
            this.m_btnReset.Text = "&Reset";
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(516, 282);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 9;
            this.m_btnCancel.Text = "&Cancel";
            // 
            // m_btnOk
            // 
            this.m_btnOk.Location = new System.Drawing.Point(435, 282);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 8;
            this.m_btnOk.Text = "&OK";
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // ChangeEquipmentStatusView
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(596, 311);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangeEquipmentStatusView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CustomerEditView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridEquipment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridEquipmentView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridCmbStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnCancel;
        internal DevExpress.XtraEditors.SimpleButton m_btnOk;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        internal DevExpress.XtraEditors.ImageComboBoxEdit m_cmbStatus;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        internal DevExpress.XtraGrid.GridControl m_gridEquipment;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridEquipmentView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        internal DevExpress.XtraEditors.SimpleButton m_btnReset;
        internal DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox m_gridCmbStatus;
        internal DevExpress.XtraEditors.MemoEdit m_txtNotes;
        private DevExpress.XtraEditors.LabelControl m_lblShortcut;
    }
}