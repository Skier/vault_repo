
using System;
using SmartSchedule.Domain;

namespace SmartSchedule.Win32.ActionReport
{
    partial class ActionReportView
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
            SmartSchedule.Domain.TimeInterval timeInterval1 = new SmartSchedule.Domain.TimeInterval();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_gridReport = new DevExpress.XtraGrid.GridControl();
            this.m_gridReportView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.m_colUser = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_colAction = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_colTechnician = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_colTickets = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_colDashboardDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_colActionDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.m_btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_dtpDashboardDate = new DevExpress.XtraEditors.DateEdit();
            this.m_cmbTechnician = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.m_dtpActionRange = new SmartSchedule.Win32.Controls.DateRange();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.m_cmbAction = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.m_txtTicket = new DevExpress.XtraEditors.TextEdit();
            this.m_cmbUser = new DevExpress.XtraEditors.ImageComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridReportView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpDashboardDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpDashboardDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTechnician.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbAction.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtTicket.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbUser.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.CausesValidation = false;
            this.panelControl1.Controls.Add(this.m_gridReport);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1284, 668);
            this.panelControl1.TabIndex = 0;
            // 
            // m_gridReport
            // 
            this.m_gridReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridReport.Location = new System.Drawing.Point(0, 58);
            this.m_gridReport.MainView = this.m_gridReportView;
            this.m_gridReport.Name = "m_gridReport";
            this.m_gridReport.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoEdit1});
            this.m_gridReport.Size = new System.Drawing.Size(1284, 610);
            this.m_gridReport.TabIndex = 0;
            this.m_gridReport.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridReportView});
            // 
            // m_gridReportView
            // 
            this.m_gridReportView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.m_colUser,
            this.m_colAction,
            this.m_colTechnician,
            this.m_colTickets,
            this.m_colDashboardDate,
            this.m_colActionDate,
            this.gridColumn7});
            this.m_gridReportView.GridControl = this.m_gridReport;
            this.m_gridReportView.Name = "m_gridReportView";
            this.m_gridReportView.OptionsBehavior.Editable = false;
            this.m_gridReportView.OptionsCustomization.AllowFilter = false;
            this.m_gridReportView.OptionsCustomization.AllowGroup = false;
            this.m_gridReportView.OptionsDetail.EnableMasterViewMode = false;
            this.m_gridReportView.OptionsDetail.ShowDetailTabs = false;
            this.m_gridReportView.OptionsMenu.EnableColumnMenu = false;
            this.m_gridReportView.OptionsSelection.MultiSelect = true;
            this.m_gridReportView.OptionsView.RowAutoHeight = true;
            this.m_gridReportView.OptionsView.ShowDetailButtons = false;
            this.m_gridReportView.OptionsView.ShowGroupPanel = false;
            // 
            // m_colUser
            // 
            this.m_colUser.Caption = "User";
            this.m_colUser.FieldName = "UserName";
            this.m_colUser.MaxWidth = 151;
            this.m_colUser.Name = "m_colUser";
            this.m_colUser.Visible = true;
            this.m_colUser.VisibleIndex = 0;
            this.m_colUser.Width = 151;
            // 
            // m_colAction
            // 
            this.m_colAction.Caption = "Action";
            this.m_colAction.FieldName = "ActionTypeText";
            this.m_colAction.MaxWidth = 150;
            this.m_colAction.Name = "m_colAction";
            this.m_colAction.Visible = true;
            this.m_colAction.VisibleIndex = 1;
            this.m_colAction.Width = 150;
            // 
            // m_colTechnician
            // 
            this.m_colTechnician.Caption = "Technician";
            this.m_colTechnician.FieldName = "TechnicianName";
            this.m_colTechnician.MaxWidth = 185;
            this.m_colTechnician.Name = "m_colTechnician";
            this.m_colTechnician.Visible = true;
            this.m_colTechnician.VisibleIndex = 2;
            this.m_colTechnician.Width = 185;
            // 
            // m_colTickets
            // 
            this.m_colTickets.Caption = "Tickets";
            this.m_colTickets.FieldName = "TicketNumber";
            this.m_colTickets.MaxWidth = 200;
            this.m_colTickets.Name = "m_colTickets";
            this.m_colTickets.Visible = true;
            this.m_colTickets.VisibleIndex = 3;
            this.m_colTickets.Width = 102;
            // 
            // m_colDashboardDate
            // 
            this.m_colDashboardDate.Caption = "Dashboard Date";
            this.m_colDashboardDate.DisplayFormat.FormatString = "d";
            this.m_colDashboardDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.m_colDashboardDate.FieldName = "DashboardDate";
            this.m_colDashboardDate.MaxWidth = 98;
            this.m_colDashboardDate.Name = "m_colDashboardDate";
            this.m_colDashboardDate.Visible = true;
            this.m_colDashboardDate.VisibleIndex = 4;
            this.m_colDashboardDate.Width = 98;
            // 
            // m_colActionDate
            // 
            this.m_colActionDate.Caption = "Action Date";
            this.m_colActionDate.DisplayFormat.FormatString = "g";
            this.m_colActionDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.m_colActionDate.FieldName = "ActionDate";
            this.m_colActionDate.MaxWidth = 120;
            this.m_colActionDate.Name = "m_colActionDate";
            this.m_colActionDate.Visible = true;
            this.m_colActionDate.VisibleIndex = 5;
            this.m_colActionDate.Width = 120;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Change Description";
            this.gridColumn7.ColumnEdit = this.repositoryItemMemoEdit1;
            this.gridColumn7.FieldName = "Text";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            this.gridColumn7.Width = 476;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.CausesValidation = false;
            this.panelControl2.Controls.Add(this.m_btnClose);
            this.panelControl2.Controls.Add(this.m_btnRefresh);
            this.panelControl2.Controls.Add(this.labelControl6);
            this.panelControl2.Controls.Add(this.labelControl5);
            this.panelControl2.Controls.Add(this.labelControl4);
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.m_dtpDashboardDate);
            this.panelControl2.Controls.Add(this.m_cmbTechnician);
            this.panelControl2.Controls.Add(this.m_dtpActionRange);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Controls.Add(this.m_cmbAction);
            this.panelControl2.Controls.Add(this.m_txtTicket);
            this.panelControl2.Controls.Add(this.m_cmbUser);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1284, 58);
            this.panelControl2.TabIndex = 1;
            // 
            // m_btnClose
            // 
            this.m_btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnClose.Location = new System.Drawing.Point(1206, 31);
            this.m_btnClose.Name = "m_btnClose";
            this.m_btnClose.Size = new System.Drawing.Size(75, 23);
            this.m_btnClose.TabIndex = 7;
            this.m_btnClose.Text = "Close";
            // 
            // m_btnRefresh
            // 
            this.m_btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnRefresh.Location = new System.Drawing.Point(1206, 3);
            this.m_btnRefresh.Name = "m_btnRefresh";
            this.m_btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.m_btnRefresh.TabIndex = 6;
            this.m_btnRefresh.Text = "Refresh";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(599, 34);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(78, 13);
            this.labelControl6.TabIndex = 27;
            this.labelControl6.Text = "Dashboard Date";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(599, 8);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(28, 13);
            this.labelControl5.TabIndex = 26;
            this.labelControl5.Text = "Ticket";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(243, 34);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(90, 13);
            this.labelControl4.TabIndex = 25;
            this.labelControl4.Text = "Action Date Range";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 34);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(30, 13);
            this.labelControl3.TabIndex = 24;
            this.labelControl3.Text = "Action";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(22, 13);
            this.labelControl1.TabIndex = 23;
            this.labelControl1.Text = "User";
            // 
            // m_dtpDashboardDate
            // 
            this.m_dtpDashboardDate.EditValue = null;
            this.m_dtpDashboardDate.Location = new System.Drawing.Point(683, 31);
            this.m_dtpDashboardDate.Name = "m_dtpDashboardDate";
            this.m_dtpDashboardDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_dtpDashboardDate.Properties.ValidateOnEnterKey = true;
            this.m_dtpDashboardDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_dtpDashboardDate.Size = new System.Drawing.Size(97, 20);
            this.m_dtpDashboardDate.TabIndex = 5;
            // 
            // m_cmbTechnician
            // 
            this.m_cmbTechnician.Location = new System.Drawing.Point(339, 5);
            this.m_cmbTechnician.Name = "m_cmbTechnician";
            this.m_cmbTechnician.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbTechnician.Properties.Sorted = true;
            this.m_cmbTechnician.Size = new System.Drawing.Size(222, 20);
            this.m_cmbTechnician.TabIndex = 2;
            // 
            // m_dtpActionRange
            // 
            timeInterval1.End = new System.DateTime(9999, 12, 31, 23, 59, 59, 999);
            timeInterval1.Start = new System.DateTime(((long)(0)));
            this.m_dtpActionRange.EditValue = timeInterval1;
            this.m_dtpActionRange.Location = new System.Drawing.Point(339, 31);
            this.m_dtpActionRange.Name = "m_dtpActionRange";
            this.m_dtpActionRange.Size = new System.Drawing.Size(222, 20);
            this.m_dtpActionRange.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(243, 8);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 13);
            this.labelControl2.TabIndex = 19;
            this.labelControl2.Text = "Technician";
            // 
            // m_cmbAction
            // 
            this.m_cmbAction.Location = new System.Drawing.Point(50, 31);
            this.m_cmbAction.Name = "m_cmbAction";
            this.m_cmbAction.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbAction.Properties.Sorted = true;
            this.m_cmbAction.Size = new System.Drawing.Size(162, 20);
            this.m_cmbAction.TabIndex = 1;
            // 
            // m_txtTicket
            // 
            this.m_txtTicket.Location = new System.Drawing.Point(683, 5);
            this.m_txtTicket.Name = "m_txtTicket";
            this.m_txtTicket.Properties.Mask.EditMask = "\\d{6,6}";
            this.m_txtTicket.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.m_txtTicket.Properties.Mask.ShowPlaceHolders = false;
            this.m_txtTicket.Size = new System.Drawing.Size(97, 20);
            this.m_txtTicket.TabIndex = 4;
            // 
            // m_cmbUser
            // 
            this.m_cmbUser.Location = new System.Drawing.Point(50, 5);
            this.m_cmbUser.Name = "m_cmbUser";
            this.m_cmbUser.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbUser.Properties.Sorted = true;
            this.m_cmbUser.Size = new System.Drawing.Size(162, 20);
            this.m_cmbUser.TabIndex = 0;
            // 
            // ActionReportView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 668);
            this.Controls.Add(this.panelControl1);
            this.Name = "ActionReportView";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "User Action Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Controls.SetChildIndex(this.panelControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridReportView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpDashboardDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpDashboardDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTechnician.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbAction.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtTicket.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbUser.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraGrid.GridControl m_gridReport;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridReportView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        internal DevExpress.XtraEditors.ImageComboBoxEdit m_cmbUser;
        internal DevExpress.XtraEditors.TextEdit m_txtTicket;
        internal DevExpress.XtraEditors.ImageComboBoxEdit m_cmbAction;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        internal DevExpress.XtraEditors.ImageComboBoxEdit m_cmbTechnician;
        internal DevExpress.XtraEditors.SimpleButton m_btnRefresh;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        internal DevExpress.XtraEditors.DateEdit m_dtpDashboardDate;
        internal Controls.DateRange m_dtpActionRange;
        internal DevExpress.XtraEditors.SimpleButton m_btnClose;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colUser;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colAction;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colTechnician;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colTickets;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colDashboardDate;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colActionDate;

    }
}