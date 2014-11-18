
namespace SmartSchedule.Win32.VisitAdd
{
    partial class VisitAddView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisitAddView));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_lblTicketNumber = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.m_gridRecommedations = new DevExpress.XtraGrid.GridControl();
            this.m_gridRecommedationsView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.m_images = new System.Windows.Forms.ImageList(this.components);
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridRecommedations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridRecommedationsView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_lblTicketNumber);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.m_gridRecommedations);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(454, 356);
            this.panelControl1.TabIndex = 0;
            // 
            // m_lblTicketNumber
            // 
            this.m_lblTicketNumber.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblTicketNumber.Appearance.Options.UseFont = true;
            this.m_lblTicketNumber.Location = new System.Drawing.Point(48, 5);
            this.m_lblTicketNumber.Name = "m_lblTicketNumber";
            this.m_lblTicketNumber.Size = new System.Drawing.Size(42, 13);
            this.m_lblTicketNumber.TabIndex = 42;
            this.m_lblTicketNumber.Text = "863488";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(7, 5);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(28, 13);
            this.labelControl5.TabIndex = 41;
            this.labelControl5.Text = "Ticket";
            // 
            // m_gridRecommedations
            // 
            this.m_gridRecommedations.Location = new System.Drawing.Point(5, 24);
            this.m_gridRecommedations.MainView = this.m_gridRecommedationsView;
            this.m_gridRecommedations.Name = "m_gridRecommedations";
            this.m_gridRecommedations.Size = new System.Drawing.Size(445, 301);
            this.m_gridRecommedations.TabIndex = 10;
            this.m_gridRecommedations.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridRecommedationsView});
            // 
            // m_gridRecommedationsView
            // 
            this.m_gridRecommedationsView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn6,
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.m_gridRecommedationsView.GridControl = this.m_gridRecommedations;
            this.m_gridRecommedationsView.Name = "m_gridRecommedationsView";
            this.m_gridRecommedationsView.OptionsBehavior.Editable = false;
            this.m_gridRecommedationsView.OptionsCustomization.AllowFilter = false;
            this.m_gridRecommedationsView.OptionsCustomization.AllowGroup = false;
            this.m_gridRecommedationsView.OptionsDetail.EnableMasterViewMode = false;
            this.m_gridRecommedationsView.OptionsDetail.ShowDetailTabs = false;
            this.m_gridRecommedationsView.OptionsMenu.EnableColumnMenu = false;
            this.m_gridRecommedationsView.OptionsView.ShowDetailButtons = false;
            this.m_gridRecommedationsView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Date";
            this.gridColumn6.FieldName = "DateSchedule";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 0;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Time Frame";
            this.gridColumn1.FieldName = "TimeFrame";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 67;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Cost";
            this.gridColumn3.DisplayFormat.FormatString = "0.00";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn3.FieldName = "FrameCostText";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 58;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Secondary Area";
            this.gridColumn4.FieldName = "IsSecondaryAreaText";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 91;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Bucket";
            this.gridColumn5.FieldName = "IsBucketText";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 55;
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(376, 330);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 9;
            this.m_btnCancel.Text = "Cancel";
            // 
            // m_images
            // 
            this.m_images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_images.ImageStream")));
            this.m_images.TransparentColor = System.Drawing.Color.Magenta;
            this.m_images.Images.SetKeyName(0, "no.bmp");
            this.m_images.Images.SetKeyName(1, "yes.bmp");
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // VisitAddView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(454, 356);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisitAddView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "VisitAddView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridRecommedations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridRecommedationsView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        internal DevExpress.XtraEditors.SimpleButton m_btnCancel;
        private System.Windows.Forms.ImageList m_images;
        internal DevExpress.XtraGrid.GridControl m_gridRecommedations;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridRecommedationsView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        internal DevExpress.XtraEditors.LabelControl m_lblTicketNumber;
        internal DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;

    }
}