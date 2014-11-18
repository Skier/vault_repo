using Dalworth.Server.MainForm.Components;

namespace Dalworth.Server.MainForm.StartDay
{
    partial class StartDayView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartDayView));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.m_lblShortcutR = new DevExpress.XtraEditors.LabelControl();
            this.m_gridRugs = new DevExpress.XtraGrid.GridControl();
            this.m_gridViewRugs = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.m_lblShortcutE = new DevExpress.XtraEditors.LabelControl();
            this.m_gridEquipmentSummary = new DevExpress.XtraGrid.GridControl();
            this.m_gridViewEquipmentSummary = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_colDueQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl8 = new DevExpress.XtraEditors.GroupControl();
            this.m_txtVanEquipment = new Dalworth.Server.MainForm.Components.EquipmentQuantityTextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblTechnicianName = new DevExpress.XtraEditors.LabelControl();
            this.m_lblWorkDate = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblStartDayTime = new DevExpress.XtraEditors.LabelControl();
            this.m_lblVan = new DevExpress.XtraEditors.LabelControl();
            this.m_timeStartDay = new Dalworth.Server.MainForm.Components.TimeEditEx();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).BeginInit();
            this.groupControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridRugs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewRugs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridEquipmentSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewEquipmentSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl8)).BeginInit();
            this.groupControl8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtVanEquipment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_timeStartDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.groupControl5);
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Controls.Add(this.groupControl8);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Controls.Add(this.m_btnOk);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(536, 398);
            this.panelControl1.TabIndex = 0;
            // 
            // groupControl5
            // 
            this.groupControl5.Controls.Add(this.m_lblShortcutR);
            this.groupControl5.Controls.Add(this.m_gridRugs);
            this.groupControl5.Location = new System.Drawing.Point(3, 144);
            this.groupControl5.Name = "groupControl5";
            this.groupControl5.Size = new System.Drawing.Size(528, 224);
            this.groupControl5.TabIndex = 2;
            this.groupControl5.Text = "&Rugs";
            // 
            // m_lblShortcutR
            // 
            this.m_lblShortcutR.AllowDrop = true;
            this.m_lblShortcutR.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblShortcutR.Location = new System.Drawing.Point(33, 72);
            this.m_lblShortcutR.Name = "m_lblShortcutR";
            this.m_lblShortcutR.Size = new System.Drawing.Size(0, 0);
            this.m_lblShortcutR.TabIndex = 0;
            this.m_lblShortcutR.Text = "&R shortcut";
            // 
            // m_gridRugs
            // 
            this.m_gridRugs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridRugs.EmbeddedNavigator.Name = "";
            this.m_gridRugs.Location = new System.Drawing.Point(2, 20);
            this.m_gridRugs.MainView = this.m_gridViewRugs;
            this.m_gridRugs.Name = "m_gridRugs";
            this.m_gridRugs.Size = new System.Drawing.Size(524, 202);
            this.m_gridRugs.TabIndex = 1;
            this.m_gridRugs.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridViewRugs});
            // 
            // m_gridViewRugs
            // 
            this.m_gridViewRugs.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16});
            this.m_gridViewRugs.GridControl = this.m_gridRugs;
            this.m_gridViewRugs.Name = "m_gridViewRugs";
            this.m_gridViewRugs.OptionsCustomization.AllowFilter = false;
            this.m_gridViewRugs.OptionsCustomization.AllowGroup = false;
            this.m_gridViewRugs.OptionsCustomization.AllowSort = false;
            this.m_gridViewRugs.OptionsMenu.EnableColumnMenu = false;
            this.m_gridViewRugs.OptionsNavigation.UseTabKey = false;
            this.m_gridViewRugs.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.m_gridViewRugs.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.m_gridViewRugs.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "Task";
            this.gridColumn14.FieldName = "TaskNumber";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 0;
            this.gridColumn14.Width = 41;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "Customer";
            this.gridColumn15.FieldName = "CustomerName";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowEdit = false;
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 1;
            this.gridColumn15.Width = 149;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "Items";
            this.gridColumn16.FieldName = "ItemsCount";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.OptionsColumn.AllowEdit = false;
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 2;
            this.gridColumn16.Width = 47;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.m_lblShortcutE);
            this.groupControl1.Controls.Add(this.m_gridEquipmentSummary);
            this.groupControl1.Location = new System.Drawing.Point(271, 3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(260, 135);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "&Equipment Summary";
            // 
            // m_lblShortcutE
            // 
            this.m_lblShortcutE.AllowDrop = true;
            this.m_lblShortcutE.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblShortcutE.Location = new System.Drawing.Point(31, 86);
            this.m_lblShortcutE.Name = "m_lblShortcutE";
            this.m_lblShortcutE.Size = new System.Drawing.Size(0, 0);
            this.m_lblShortcutE.TabIndex = 0;
            this.m_lblShortcutE.Text = "&E shortcut";
            // 
            // m_gridEquipmentSummary
            // 
            this.m_gridEquipmentSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridEquipmentSummary.EmbeddedNavigator.Name = "";
            this.m_gridEquipmentSummary.Location = new System.Drawing.Point(2, 20);
            this.m_gridEquipmentSummary.MainView = this.m_gridViewEquipmentSummary;
            this.m_gridEquipmentSummary.Name = "m_gridEquipmentSummary";
            this.m_gridEquipmentSummary.Size = new System.Drawing.Size(256, 113);
            this.m_gridEquipmentSummary.TabIndex = 1;
            this.m_gridEquipmentSummary.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridViewEquipmentSummary});
            // 
            // m_gridViewEquipmentSummary
            // 
            this.m_gridViewEquipmentSummary.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn11,
            this.m_colDueQuantity});
            this.m_gridViewEquipmentSummary.GridControl = this.m_gridEquipmentSummary;
            this.m_gridViewEquipmentSummary.Name = "m_gridViewEquipmentSummary";
            this.m_gridViewEquipmentSummary.OptionsCustomization.AllowFilter = false;
            this.m_gridViewEquipmentSummary.OptionsCustomization.AllowGroup = false;
            this.m_gridViewEquipmentSummary.OptionsCustomization.AllowSort = false;
            this.m_gridViewEquipmentSummary.OptionsMenu.EnableColumnMenu = false;
            this.m_gridViewEquipmentSummary.OptionsNavigation.UseTabKey = false;
            this.m_gridViewEquipmentSummary.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.m_gridViewEquipmentSummary.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.m_gridViewEquipmentSummary.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Equipment Type";
            this.gridColumn1.FieldName = "EquipmentTypeName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 158;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Van";
            this.gridColumn2.FieldName = "VanQuantity";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 79;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Sugg";
            this.gridColumn11.FieldName = "SuggestedQuantity";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 2;
            // 
            // m_colDueQuantity
            // 
            this.m_colDueQuantity.Caption = "Due";
            this.m_colDueQuantity.FieldName = "DueQuantity";
            this.m_colDueQuantity.Name = "m_colDueQuantity";
            this.m_colDueQuantity.OptionsColumn.AllowEdit = false;
            this.m_colDueQuantity.Visible = true;
            this.m_colDueQuantity.VisibleIndex = 3;
            // 
            // groupControl8
            // 
            this.groupControl8.Controls.Add(this.m_txtVanEquipment);
            this.groupControl8.Controls.Add(this.labelControl2);
            this.groupControl8.Controls.Add(this.labelControl8);
            this.groupControl8.Controls.Add(this.m_lblTechnicianName);
            this.groupControl8.Controls.Add(this.m_lblWorkDate);
            this.groupControl8.Controls.Add(this.labelControl10);
            this.groupControl8.Controls.Add(this.m_lblStartDayTime);
            this.groupControl8.Controls.Add(this.m_lblVan);
            this.groupControl8.Controls.Add(this.m_timeStartDay);
            this.groupControl8.Controls.Add(this.labelControl1);
            this.groupControl8.Location = new System.Drawing.Point(3, 3);
            this.groupControl8.Name = "groupControl8";
            this.groupControl8.Size = new System.Drawing.Size(262, 135);
            this.groupControl8.TabIndex = 0;
            this.groupControl8.Text = "Work";
            // 
            // m_txtVanEquipment
            // 
            this.m_txtVanEquipment.EditValue = "0/0/0";
            this.m_txtVanEquipment.Location = new System.Drawing.Point(169, 109);
            this.m_txtVanEquipment.Name = "m_txtVanEquipment";
            this.m_txtVanEquipment.Properties.Mask.EditMask = "\\d{1,2}/\\d{1,2}/\\d{1,2}";
            this.m_txtVanEquipment.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.m_txtVanEquipment.Properties.NullText = "0/0/0";
            this.m_txtVanEquipment.Quantities = ((System.Collections.Generic.Dictionary<int, int>)(resources.GetObject("m_txtVanEquipment.Quantities")));
            this.m_txtVanEquipment.Size = new System.Drawing.Size(88, 20);
            this.m_txtVanEquipment.TabIndex = 9;
            // 
            // labelControl2
            // 
            this.labelControl2.AllowDrop = true;
            this.labelControl2.Location = new System.Drawing.Point(6, 112);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(140, 13);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "Van Equipment (Fan/Deh/Air)";
            // 
            // labelControl8
            // 
            this.labelControl8.AllowDrop = true;
            this.labelControl8.Location = new System.Drawing.Point(5, 62);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(23, 13);
            this.labelControl8.TabIndex = 4;
            this.labelControl8.Text = "Date";
            // 
            // m_lblTechnicianName
            // 
            this.m_lblTechnicianName.AllowDrop = true;
            this.m_lblTechnicianName.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblTechnicianName.Appearance.Options.UseFont = true;
            this.m_lblTechnicianName.Appearance.Options.UseTextOptions = true;
            this.m_lblTechnicianName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.m_lblTechnicianName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblTechnicianName.Location = new System.Drawing.Point(77, 23);
            this.m_lblTechnicianName.Name = "m_lblTechnicianName";
            this.m_lblTechnicianName.Size = new System.Drawing.Size(180, 13);
            this.m_lblTechnicianName.TabIndex = 1;
            this.m_lblTechnicianName.Text = "Shane Hobbs";
            // 
            // m_lblWorkDate
            // 
            this.m_lblWorkDate.AllowDrop = true;
            this.m_lblWorkDate.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblWorkDate.Appearance.Options.UseFont = true;
            this.m_lblWorkDate.Appearance.Options.UseTextOptions = true;
            this.m_lblWorkDate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.m_lblWorkDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblWorkDate.Location = new System.Drawing.Point(77, 64);
            this.m_lblWorkDate.Name = "m_lblWorkDate";
            this.m_lblWorkDate.Size = new System.Drawing.Size(180, 13);
            this.m_lblWorkDate.TabIndex = 5;
            this.m_lblWorkDate.Text = "55/55/5555";
            // 
            // labelControl10
            // 
            this.labelControl10.AllowDrop = true;
            this.labelControl10.Location = new System.Drawing.Point(5, 42);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(18, 13);
            this.labelControl10.TabIndex = 2;
            this.labelControl10.Text = "Van";
            // 
            // m_lblStartDayTime
            // 
            this.m_lblStartDayTime.AllowDrop = true;
            this.m_lblStartDayTime.Location = new System.Drawing.Point(5, 86);
            this.m_lblStartDayTime.Name = "m_lblStartDayTime";
            this.m_lblStartDayTime.Size = new System.Drawing.Size(22, 13);
            this.m_lblStartDayTime.TabIndex = 6;
            this.m_lblStartDayTime.Text = "&Time";
            // 
            // m_lblVan
            // 
            this.m_lblVan.AllowDrop = true;
            this.m_lblVan.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblVan.Appearance.Options.UseFont = true;
            this.m_lblVan.Appearance.Options.UseTextOptions = true;
            this.m_lblVan.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.m_lblVan.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblVan.Location = new System.Drawing.Point(77, 42);
            this.m_lblVan.Name = "m_lblVan";
            this.m_lblVan.Size = new System.Drawing.Size(180, 13);
            this.m_lblVan.TabIndex = 3;
            this.m_lblVan.Text = "234234";
            // 
            // m_timeStartDay
            // 
            this.m_timeStartDay.EditValue = new System.DateTime(2008, 2, 19, 0, 0, 0, 0);
            this.m_timeStartDay.Location = new System.Drawing.Point(169, 83);
            this.m_timeStartDay.Name = "m_timeStartDay";
            this.m_timeStartDay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_timeStartDay.Properties.Mask.EditMask = "t";
            this.m_timeStartDay.Size = new System.Drawing.Size(88, 20);
            this.m_timeStartDay.TabIndex = 7;
            // 
            // labelControl1
            // 
            this.labelControl1.AllowDrop = true;
            this.labelControl1.Location = new System.Drawing.Point(5, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(50, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Technician";
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(458, 372);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 4;
            this.m_btnCancel.Text = "&Cancel";
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.Location = new System.Drawing.Point(377, 372);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 3;
            this.m_btnOk.Text = "&OK";
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // StartDayView
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(536, 398);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartDayView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "StartDayView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
            this.groupControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridRugs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewRugs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridEquipmentSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewEquipmentSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl8)).EndInit();
            this.groupControl8.ResumeLayout(false);
            this.groupControl8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtVanEquipment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_timeStartDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnCancel;
        internal DevExpress.XtraEditors.SimpleButton m_btnOk;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        internal DevExpress.XtraGrid.GridControl m_gridEquipmentSummary;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridViewEquipmentSummary;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colDueQuantity;
        private DevExpress.XtraEditors.GroupControl groupControl8;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        internal DevExpress.XtraEditors.LabelControl m_lblTechnicianName;
        internal DevExpress.XtraEditors.LabelControl m_lblWorkDate;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        internal DevExpress.XtraEditors.LabelControl m_lblStartDayTime;
        internal DevExpress.XtraEditors.LabelControl m_lblVan;
        internal TimeEditEx m_timeStartDay;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl5;
        internal DevExpress.XtraGrid.GridControl m_gridRugs;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridViewRugs;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        internal DevExpress.XtraEditors.LabelControl m_lblShortcutR;
        internal DevExpress.XtraEditors.LabelControl m_lblShortcutE;
        internal EquipmentQuantityTextEdit m_txtVanEquipment;
        internal DevExpress.XtraEditors.LabelControl labelControl2;
    }
}