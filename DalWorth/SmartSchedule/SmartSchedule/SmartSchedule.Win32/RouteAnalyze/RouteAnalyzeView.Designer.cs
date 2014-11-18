
namespace SmartSchedule.Win32.RouteAnalyze
{
    partial class RouteAnalyzeView
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
            this.m_lblCurrentRpm = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.m_gridRouteChanges = new DevExpress.XtraGrid.GridControl();
            this.m_gridRouteChangesView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridRouteChanges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridRouteChangesView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.CausesValidation = false;
            this.panelControl1.Controls.Add(this.m_lblCurrentRpm);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.m_gridRouteChanges);
            this.panelControl1.Controls.Add(this.m_btnClose);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(308, 382);
            this.panelControl1.TabIndex = 0;
            // 
            // m_lblCurrentRpm
            // 
            this.m_lblCurrentRpm.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblCurrentRpm.Appearance.Options.UseFont = true;
            this.m_lblCurrentRpm.Location = new System.Drawing.Point(84, 7);
            this.m_lblCurrentRpm.Name = "m_lblCurrentRpm";
            this.m_lblCurrentRpm.Size = new System.Drawing.Size(31, 13);
            this.m_lblCurrentRpm.TabIndex = 14;
            this.m_lblCurrentRpm.Text = "50.00";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(7, 7);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(65, 13);
            this.labelControl2.TabIndex = 13;
            this.labelControl2.Text = "Current Drive";
            // 
            // m_gridRouteChanges
            // 
            this.m_gridRouteChanges.Location = new System.Drawing.Point(0, 27);
            this.m_gridRouteChanges.MainView = this.m_gridRouteChangesView;
            this.m_gridRouteChanges.Name = "m_gridRouteChanges";
            this.m_gridRouteChanges.Size = new System.Drawing.Size(306, 325);
            this.m_gridRouteChanges.TabIndex = 12;
            this.m_gridRouteChanges.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridRouteChangesView});
            // 
            // m_gridRouteChangesView
            // 
            this.m_gridRouteChangesView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn17,
            this.gridColumn1});
            this.m_gridRouteChangesView.GridControl = this.m_gridRouteChanges;
            this.m_gridRouteChangesView.Name = "m_gridRouteChangesView";
            this.m_gridRouteChangesView.OptionsBehavior.Editable = false;
            this.m_gridRouteChangesView.OptionsCustomization.AllowFilter = false;
            this.m_gridRouteChangesView.OptionsCustomization.AllowGroup = false;
            this.m_gridRouteChangesView.OptionsDetail.EnableMasterViewMode = false;
            this.m_gridRouteChangesView.OptionsDetail.ShowDetailTabs = false;
            this.m_gridRouteChangesView.OptionsMenu.EnableColumnMenu = false;
            this.m_gridRouteChangesView.OptionsSelection.MultiSelect = true;
            this.m_gridRouteChangesView.OptionsView.ShowDetailButtons = false;
            this.m_gridRouteChangesView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "Removed Tickets #";
            this.gridColumn17.FieldName = "VisitsToRemoveText";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 0;
            this.gridColumn17.Width = 202;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "New Drive (mi)";
            this.gridColumn1.DisplayFormat.FormatString = "0";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn1.FieldName = "NewDrive";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 83;
            // 
            // m_btnClose
            // 
            this.m_btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnClose.Location = new System.Drawing.Point(230, 356);
            this.m_btnClose.Name = "m_btnClose";
            this.m_btnClose.Size = new System.Drawing.Size(75, 23);
            this.m_btnClose.TabIndex = 11;
            this.m_btnClose.Text = "Close";
            // 
            // RouteAnalyzeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnClose;
            this.ClientSize = new System.Drawing.Size(308, 382);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RouteAnalyzeView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Route Analyze";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridRouteChanges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridRouteChangesView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnClose;
        internal DevExpress.XtraGrid.GridControl m_gridRouteChanges;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridRouteChangesView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        internal DevExpress.XtraEditors.LabelControl m_lblCurrentRpm;
        private DevExpress.XtraEditors.LabelControl labelControl2;

    }
}