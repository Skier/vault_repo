
namespace SmartSchedule.Win32.HistoricalOrders
{
    partial class HistoricalOrdersView
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.m_gridOrders = new DevExpress.XtraGrid.GridControl();
            this.m_gridViewOrders = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_dtpDateSchedule = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.m_btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpDateSchedule.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpDateSchedule.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_btnAdd);
            this.panelControl1.Controls.Add(this.m_gridOrders);
            this.panelControl1.Controls.Add(this.m_dtpDateSchedule);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.m_btnClose);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(934, 649);
            this.panelControl1.TabIndex = 0;
            // 
            // m_btnAdd
            // 
            this.m_btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnAdd.Location = new System.Drawing.Point(775, 623);
            this.m_btnAdd.Name = "m_btnAdd";
            this.m_btnAdd.Size = new System.Drawing.Size(75, 23);
            this.m_btnAdd.TabIndex = 2;
            this.m_btnAdd.Text = "Add";
            // 
            // m_gridOrders
            // 
            this.m_gridOrders.Location = new System.Drawing.Point(3, 35);
            this.m_gridOrders.MainView = this.m_gridViewOrders;
            this.m_gridOrders.Name = "m_gridOrders";
            this.m_gridOrders.Size = new System.Drawing.Size(928, 581);
            this.m_gridOrders.TabIndex = 1;
            this.m_gridOrders.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridViewOrders});
            // 
            // m_gridViewOrders
            // 
            this.m_gridViewOrders.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6});
            this.m_gridViewOrders.GridControl = this.m_gridOrders;
            this.m_gridViewOrders.Name = "m_gridViewOrders";
            this.m_gridViewOrders.OptionsBehavior.Editable = false;
            this.m_gridViewOrders.OptionsCustomization.AllowGroup = false;
            this.m_gridViewOrders.OptionsMenu.EnableColumnMenu = false;
            this.m_gridViewOrders.OptionsSelection.MultiSelect = true;
            this.m_gridViewOrders.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Call";
            this.gridColumn7.DisplayFormat.FormatString = "g";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn7.FieldName = "DateTimeCall";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn7.OptionsFilter.AllowFilter = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            this.gridColumn7.Width = 119;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Date Schedule";
            this.gridColumn1.DisplayFormat.FormatString = "d";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn1.FieldName = "DateSchedule";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn1.OptionsFilter.AllowFilter = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 86;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Customer";
            this.gridColumn2.FieldName = "CustomerName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn2.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn2.OptionsFilter.AllowFilter = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 139;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Address";
            this.gridColumn3.FieldName = "Address";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn3.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn3.OptionsFilter.AllowFilter = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            this.gridColumn3.Width = 244;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Zip";
            this.gridColumn4.FieldName = "Zip";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            this.gridColumn4.Width = 60;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Price";
            this.gridColumn5.DisplayFormat.FormatString = "C";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "Cost";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn5.OptionsFilter.AllowFilter = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 70;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Exclusive Company";
            this.gridColumn6.FieldName = "ExclusiveCompanyName";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            this.gridColumn6.Width = 189;
            // 
            // m_dtpDateSchedule
            // 
            this.m_dtpDateSchedule.EditValue = null;
            this.m_dtpDateSchedule.Location = new System.Drawing.Point(89, 9);
            this.m_dtpDateSchedule.Name = "m_dtpDateSchedule";
            this.m_dtpDateSchedule.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_dtpDateSchedule.Properties.MaxValue = new System.DateTime(2009, 8, 31, 0, 0, 0, 0);
            this.m_dtpDateSchedule.Properties.MinValue = new System.DateTime(2009, 8, 1, 0, 0, 0, 0);
            this.m_dtpDateSchedule.Properties.NullText = "All";
            this.m_dtpDateSchedule.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_dtpDateSchedule.Size = new System.Drawing.Size(155, 20);
            this.m_dtpDateSchedule.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(69, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Date Schedule";
            // 
            // m_btnClose
            // 
            this.m_btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnClose.Location = new System.Drawing.Point(856, 623);
            this.m_btnClose.Name = "m_btnClose";
            this.m_btnClose.Size = new System.Drawing.Size(75, 23);
            this.m_btnClose.TabIndex = 3;
            this.m_btnClose.Text = "Close";
            // 
            // HistoricalOrdersView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnClose;
            this.ClientSize = new System.Drawing.Size(934, 649);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "HistoricalOrdersView";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HistoricalOrdersView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpDateSchedule.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpDateSchedule.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnClose;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        internal DevExpress.XtraGrid.GridControl m_gridOrders;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridViewOrders;
        internal DevExpress.XtraEditors.DateEdit m_dtpDateSchedule;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        internal DevExpress.XtraEditors.SimpleButton m_btnAdd;

    }
}