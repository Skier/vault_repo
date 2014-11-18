namespace Dalworth.Server.MainForm.VisitSplit
{
    partial class VisitSplitView
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
            Dalworth.Server.Domain.Visit visit1 = new Dalworth.Server.Domain.Visit();
            Dalworth.Server.Domain.Visit visit2 = new Dalworth.Server.Domain.Visit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_lblShortcut2 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblShortcut = new DevExpress.XtraEditors.LabelControl();
            this.m_txtTaskDetails = new DevExpress.XtraEditors.MemoEdit();
            this.m_btnMoveTasksToVisit2 = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnMoveTasksToVisit1 = new DevExpress.XtraEditors.SimpleButton();
            this.m_gridTasksVisit2 = new DevExpress.XtraGrid.GridControl();
            this.m_gridTasksVisit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_gridTasksVisit1 = new DevExpress.XtraGrid.GridControl();
            this.m_gridTasksVisit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_pnlVisit2 = new DevExpress.XtraEditors.GroupControl();
            this.m_ctlHeaderVisit2 = new Dalworth.Server.MainForm.Components.VisitHeaderEdit();
            this.m_pnlVisit1 = new DevExpress.XtraEditors.GroupControl();
            this.m_ctlHeaderVisit1 = new Dalworth.Server.MainForm.Components.VisitHeaderEdit();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtTaskDetails.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTasksVisit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTasksVisit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTasksVisit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTasksVisit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlVisit2)).BeginInit();
            this.m_pnlVisit2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlVisit1)).BeginInit();
            this.m_pnlVisit1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.m_lblShortcut2);
            this.panelControl1.Controls.Add(this.m_lblShortcut);
            this.panelControl1.Controls.Add(this.m_txtTaskDetails);
            this.panelControl1.Controls.Add(this.m_btnMoveTasksToVisit2);
            this.panelControl1.Controls.Add(this.m_btnMoveTasksToVisit1);
            this.panelControl1.Controls.Add(this.m_gridTasksVisit2);
            this.panelControl1.Controls.Add(this.m_gridTasksVisit1);
            this.panelControl1.Controls.Add(this.m_pnlVisit2);
            this.panelControl1.Controls.Add(this.m_pnlVisit1);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Controls.Add(this.m_btnOk);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(609, 578);
            this.panelControl1.TabIndex = 0;
            // 
            // m_lblShortcut2
            // 
            this.m_lblShortcut2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblShortcut2.Location = new System.Drawing.Point(361, 268);
            this.m_lblShortcut2.Name = "m_lblShortcut2";
            this.m_lblShortcut2.Size = new System.Drawing.Size(0, 0);
            this.m_lblShortcut2.TabIndex = 4;
            this.m_lblShortcut2.Text = "&B shortcut";
            // 
            // m_lblShortcut
            // 
            this.m_lblShortcut.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblShortcut.Location = new System.Drawing.Point(35, 268);
            this.m_lblShortcut.Name = "m_lblShortcut";
            this.m_lblShortcut.Size = new System.Drawing.Size(0, 0);
            this.m_lblShortcut.TabIndex = 2;
            this.m_lblShortcut.Text = "&B shortcut";
            // 
            // m_txtTaskDetails
            // 
            this.m_txtTaskDetails.Location = new System.Drawing.Point(5, 449);
            this.m_txtTaskDetails.Name = "m_txtTaskDetails";
            this.m_txtTaskDetails.Properties.ReadOnly = true;
            this.m_txtTaskDetails.Size = new System.Drawing.Size(599, 96);
            this.m_txtTaskDetails.TabIndex = 6;
            // 
            // m_btnMoveTasksToVisit2
            // 
            this.m_btnMoveTasksToVisit2.Location = new System.Drawing.Point(286, 319);
            this.m_btnMoveTasksToVisit2.Name = "m_btnMoveTasksToVisit2";
            this.m_btnMoveTasksToVisit2.Size = new System.Drawing.Size(38, 23);
            this.m_btnMoveTasksToVisit2.TabIndex = 20;
            this.m_btnMoveTasksToVisit2.TabStop = false;
            this.m_btnMoveTasksToVisit2.Text = ">";
            // 
            // m_btnMoveTasksToVisit1
            // 
            this.m_btnMoveTasksToVisit1.Location = new System.Drawing.Point(286, 348);
            this.m_btnMoveTasksToVisit1.Name = "m_btnMoveTasksToVisit1";
            this.m_btnMoveTasksToVisit1.Size = new System.Drawing.Size(38, 23);
            this.m_btnMoveTasksToVisit1.TabIndex = 21;
            this.m_btnMoveTasksToVisit1.TabStop = false;
            this.m_btnMoveTasksToVisit1.Text = "<";
            // 
            // m_gridTasksVisit2
            // 
            this.m_gridTasksVisit2.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.m_gridTasksVisit2.EmbeddedNavigator.Buttons.First.Visible = false;
            this.m_gridTasksVisit2.EmbeddedNavigator.Buttons.Last.Visible = false;
            this.m_gridTasksVisit2.EmbeddedNavigator.Buttons.Next.Visible = false;
            this.m_gridTasksVisit2.EmbeddedNavigator.Buttons.NextPage.Visible = false;
            this.m_gridTasksVisit2.EmbeddedNavigator.Buttons.Prev.Visible = false;
            this.m_gridTasksVisit2.EmbeddedNavigator.Buttons.PrevPage.Visible = false;
            this.m_gridTasksVisit2.EmbeddedNavigator.Name = "";
            this.m_gridTasksVisit2.Location = new System.Drawing.Point(330, 248);
            this.m_gridTasksVisit2.MainView = this.m_gridTasksVisit2View;
            this.m_gridTasksVisit2.Name = "m_gridTasksVisit2";
            this.m_gridTasksVisit2.Size = new System.Drawing.Size(274, 195);
            this.m_gridTasksVisit2.TabIndex = 5;
            this.m_gridTasksVisit2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridTasksVisit2View});
            // 
            // m_gridTasksVisit2View
            // 
            this.m_gridTasksVisit2View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn3});
            this.m_gridTasksVisit2View.GridControl = this.m_gridTasksVisit2;
            this.m_gridTasksVisit2View.Name = "m_gridTasksVisit2View";
            this.m_gridTasksVisit2View.OptionsBehavior.Editable = false;
            this.m_gridTasksVisit2View.OptionsCustomization.AllowFilter = false;
            this.m_gridTasksVisit2View.OptionsCustomization.AllowGroup = false;
            this.m_gridTasksVisit2View.OptionsMenu.EnableColumnMenu = false;
            this.m_gridTasksVisit2View.OptionsMenu.EnableFooterMenu = false;
            this.m_gridTasksVisit2View.OptionsMenu.EnableGroupPanelMenu = false;
            this.m_gridTasksVisit2View.OptionsNavigation.UseTabKey = false;
            this.m_gridTasksVisit2View.OptionsSelection.MultiSelect = true;
            this.m_gridTasksVisit2View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Number";
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Task";
            this.gridColumn3.FieldName = "TaskTypeText";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // m_gridTasksVisit1
            // 
            this.m_gridTasksVisit1.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.m_gridTasksVisit1.EmbeddedNavigator.Buttons.First.Visible = false;
            this.m_gridTasksVisit1.EmbeddedNavigator.Buttons.Last.Visible = false;
            this.m_gridTasksVisit1.EmbeddedNavigator.Buttons.Next.Visible = false;
            this.m_gridTasksVisit1.EmbeddedNavigator.Buttons.NextPage.Visible = false;
            this.m_gridTasksVisit1.EmbeddedNavigator.Buttons.Prev.Visible = false;
            this.m_gridTasksVisit1.EmbeddedNavigator.Buttons.PrevPage.Visible = false;
            this.m_gridTasksVisit1.EmbeddedNavigator.Name = "";
            this.m_gridTasksVisit1.Location = new System.Drawing.Point(5, 248);
            this.m_gridTasksVisit1.MainView = this.m_gridTasksVisit1View;
            this.m_gridTasksVisit1.Name = "m_gridTasksVisit1";
            this.m_gridTasksVisit1.Size = new System.Drawing.Size(275, 195);
            this.m_gridTasksVisit1.TabIndex = 3;
            this.m_gridTasksVisit1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridTasksVisit1View});
            // 
            // m_gridTasksVisit1View
            // 
            this.m_gridTasksVisit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn4});
            this.m_gridTasksVisit1View.GridControl = this.m_gridTasksVisit1;
            this.m_gridTasksVisit1View.Name = "m_gridTasksVisit1View";
            this.m_gridTasksVisit1View.OptionsBehavior.Editable = false;
            this.m_gridTasksVisit1View.OptionsCustomization.AllowFilter = false;
            this.m_gridTasksVisit1View.OptionsCustomization.AllowGroup = false;
            this.m_gridTasksVisit1View.OptionsMenu.EnableColumnMenu = false;
            this.m_gridTasksVisit1View.OptionsMenu.EnableFooterMenu = false;
            this.m_gridTasksVisit1View.OptionsMenu.EnableGroupPanelMenu = false;
            this.m_gridTasksVisit1View.OptionsNavigation.UseTabKey = false;
            this.m_gridTasksVisit1View.OptionsSelection.MultiSelect = true;
            this.m_gridTasksVisit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Number";
            this.gridColumn2.FieldName = "ID";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 66;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Task";
            this.gridColumn4.FieldName = "TaskTypeText";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            this.gridColumn4.Width = 181;
            // 
            // m_pnlVisit2
            // 
            this.m_pnlVisit2.Controls.Add(this.m_ctlHeaderVisit2);
            this.m_pnlVisit2.Location = new System.Drawing.Point(308, 5);
            this.m_pnlVisit2.Name = "m_pnlVisit2";
            this.m_pnlVisit2.Size = new System.Drawing.Size(297, 237);
            this.m_pnlVisit2.TabIndex = 1;
            this.m_pnlVisit2.Text = "Splitted Visit";
            // 
            // m_ctlHeaderVisit2
            // 
            this.m_ctlHeaderVisit2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_ctlHeaderVisit2.IsReadOnly = false;
            this.m_ctlHeaderVisit2.Location = new System.Drawing.Point(2, 20);
            this.m_ctlHeaderVisit2.Name = "m_ctlHeaderVisit2";
            this.m_ctlHeaderVisit2.Size = new System.Drawing.Size(293, 215);
            this.m_ctlHeaderVisit2.TabIndex = 0;
            visit1.ClosedDollarAmount = new decimal(new int[] {
            0,
            0,
            0,
            0});
            visit1.ConfirmDateTime = null;
            visit1.ConfirmedFrameBegin = null;
            visit1.ConfirmedFrameEnd = null;
            visit1.ConfirmLeftMessage = false;
            visit1.CreateDate = new System.DateTime(((long)(0)));
            visit1.CustomerId = null;
            visit1.DurationMin = null;
            visit1.ID = 0;
            visit1.IsCallOnYourWay = false;
            visit1.Notes = "";
            visit1.PreferedTimeFrom = null;
            visit1.PreferedTimeTo = null;
            visit1.ServiceAddressId = null;
            visit1.ServiceDate = new System.DateTime(2008, 10, 24, 12, 46, 24, 812);
            visit1.SyncToolPrintDate = null;
            visit1.VisitStatus = Dalworth.Server.Domain.VisitStatusEnum.Pending;
            visit1.VisitStatusId = 1;
            this.m_ctlHeaderVisit2.Visit = visit1;
            // 
            // m_pnlVisit1
            // 
            this.m_pnlVisit1.Controls.Add(this.m_ctlHeaderVisit1);
            this.m_pnlVisit1.Location = new System.Drawing.Point(5, 5);
            this.m_pnlVisit1.Name = "m_pnlVisit1";
            this.m_pnlVisit1.Size = new System.Drawing.Size(297, 237);
            this.m_pnlVisit1.TabIndex = 0;
            this.m_pnlVisit1.Text = "Original Visit";
            // 
            // m_ctlHeaderVisit1
            // 
            this.m_ctlHeaderVisit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_ctlHeaderVisit1.IsReadOnly = false;
            this.m_ctlHeaderVisit1.Location = new System.Drawing.Point(2, 20);
            this.m_ctlHeaderVisit1.Name = "m_ctlHeaderVisit1";
            this.m_ctlHeaderVisit1.Size = new System.Drawing.Size(293, 215);
            this.m_ctlHeaderVisit1.TabIndex = 0;
            visit2.ClosedDollarAmount = new decimal(new int[] {
            0,
            0,
            0,
            0});
            visit2.ConfirmDateTime = null;
            visit2.ConfirmedFrameBegin = null;
            visit2.ConfirmedFrameEnd = null;
            visit2.ConfirmLeftMessage = false;
            visit2.CreateDate = new System.DateTime(((long)(0)));
            visit2.CustomerId = null;
            visit2.DurationMin = null;
            visit2.ID = 0;
            visit2.IsCallOnYourWay = false;
            visit2.Notes = "";
            visit2.PreferedTimeFrom = null;
            visit2.PreferedTimeTo = null;
            visit2.ServiceAddressId = null;
            visit2.ServiceDate = new System.DateTime(2008, 10, 24, 12, 46, 24, 859);
            visit2.SyncToolPrintDate = null;
            visit2.VisitStatus = Dalworth.Server.Domain.VisitStatusEnum.Pending;
            visit2.VisitStatusId = 1;
            this.m_ctlHeaderVisit1.Visit = visit2;
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(529, 550);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 8;
            this.m_btnCancel.Text = "&Cancel";
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.Location = new System.Drawing.Point(448, 550);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 7;
            this.m_btnOk.Text = "&OK";
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // VisitSplitView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(609, 578);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisitSplitView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "VisitSplitView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_txtTaskDetails.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTasksVisit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTasksVisit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTasksVisit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTasksVisit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlVisit2)).EndInit();
            this.m_pnlVisit2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_pnlVisit1)).EndInit();
            this.m_pnlVisit1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnCancel;
        internal DevExpress.XtraEditors.SimpleButton m_btnOk;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        internal Dalworth.Server.MainForm.Components.VisitHeaderEdit m_ctlHeaderVisit2;
        internal Dalworth.Server.MainForm.Components.VisitHeaderEdit m_ctlHeaderVisit1;
        internal DevExpress.XtraEditors.MemoEdit m_txtTaskDetails;
        internal DevExpress.XtraEditors.SimpleButton m_btnMoveTasksToVisit2;
        internal DevExpress.XtraEditors.SimpleButton m_btnMoveTasksToVisit1;
        internal DevExpress.XtraGrid.GridControl m_gridTasksVisit2;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridTasksVisit2View;
        internal DevExpress.XtraGrid.GridControl m_gridTasksVisit1;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridTasksVisit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        internal DevExpress.XtraEditors.GroupControl m_pnlVisit1;
        internal DevExpress.XtraEditors.GroupControl m_pnlVisit2;
        private DevExpress.XtraEditors.LabelControl m_lblShortcut2;
        private DevExpress.XtraEditors.LabelControl m_lblShortcut;
    }
}