
namespace SmartSchedule.Win32.SnapshotChange
{
    partial class SnapshotChangeView
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
            this.m_gridSnapshotChanges = new DevExpress.XtraGrid.GridControl();
            this.m_gridSnapshotChangesView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnPrint = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridSnapshotChanges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridSnapshotChangesView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.CausesValidation = false;
            this.panelControl1.Controls.Add(this.m_btnPrint);
            this.panelControl1.Controls.Add(this.m_gridSnapshotChanges);
            this.panelControl1.Controls.Add(this.m_btnClose);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(491, 548);
            this.panelControl1.TabIndex = 0;
            // 
            // m_gridSnapshotChanges
            // 
            this.m_gridSnapshotChanges.Location = new System.Drawing.Point(0, 0);
            this.m_gridSnapshotChanges.MainView = this.m_gridSnapshotChangesView;
            this.m_gridSnapshotChanges.Name = "m_gridSnapshotChanges";
            this.m_gridSnapshotChanges.Size = new System.Drawing.Size(491, 516);
            this.m_gridSnapshotChanges.TabIndex = 12;
            this.m_gridSnapshotChanges.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridSnapshotChangesView});
            // 
            // m_gridSnapshotChangesView
            // 
            this.m_gridSnapshotChangesView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn17,
            this.gridColumn1});
            this.m_gridSnapshotChangesView.GridControl = this.m_gridSnapshotChanges;
            this.m_gridSnapshotChangesView.Name = "m_gridSnapshotChangesView";
            this.m_gridSnapshotChangesView.OptionsBehavior.Editable = false;
            this.m_gridSnapshotChangesView.OptionsCustomization.AllowFilter = false;
            this.m_gridSnapshotChangesView.OptionsCustomization.AllowGroup = false;
            this.m_gridSnapshotChangesView.OptionsDetail.EnableMasterViewMode = false;
            this.m_gridSnapshotChangesView.OptionsDetail.ShowDetailTabs = false;
            this.m_gridSnapshotChangesView.OptionsMenu.EnableColumnMenu = false;
            this.m_gridSnapshotChangesView.OptionsSelection.MultiSelect = true;
            this.m_gridSnapshotChangesView.OptionsView.ShowDetailButtons = false;
            this.m_gridSnapshotChangesView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "Ticket #";
            this.gridColumn17.FieldName = "TicketNumber";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 0;
            this.gridColumn17.Width = 68;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Description";
            this.gridColumn1.FieldName = "ChangeDescription";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 542;
            // 
            // m_btnClose
            // 
            this.m_btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnClose.Location = new System.Drawing.Point(413, 522);
            this.m_btnClose.Name = "m_btnClose";
            this.m_btnClose.Size = new System.Drawing.Size(75, 23);
            this.m_btnClose.TabIndex = 11;
            this.m_btnClose.Text = "Close";
            // 
            // m_btnPrint
            // 
            this.m_btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnPrint.Location = new System.Drawing.Point(332, 522);
            this.m_btnPrint.Name = "m_btnPrint";
            this.m_btnPrint.Size = new System.Drawing.Size(75, 23);
            this.m_btnPrint.TabIndex = 13;
            this.m_btnPrint.Text = "Print";
            // 
            // SnapshotChangeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnClose;
            this.ClientSize = new System.Drawing.Size(491, 548);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SnapshotChangeView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Snapshot Report";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridSnapshotChanges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridSnapshotChangesView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnClose;
        internal DevExpress.XtraGrid.GridControl m_gridSnapshotChanges;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridSnapshotChangesView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        internal DevExpress.XtraEditors.SimpleButton m_btnPrint;

    }
}