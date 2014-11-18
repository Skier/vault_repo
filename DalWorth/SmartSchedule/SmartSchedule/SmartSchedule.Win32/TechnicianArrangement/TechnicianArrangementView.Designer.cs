
using SmartSchedule.Win32.Controls;
using SmartSchedule.Windows;

namespace SmartSchedule.Win32.TechnicianArrangement
{
    partial class TechnicianArrangementView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TechnicianArrangementView));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_btnMoveToInvisibleAll = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnMoveToVisibleAll = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnMoveToInvisible = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnMoveToVisible = new DevExpress.XtraEditors.SimpleButton();
            this.m_gridTechnicianUnordered = new SmartSchedule.Win32.Controls.Grid();
            this.m_gridTechnicianUnorderedView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.m_imagesYesNo = new System.Windows.Forms.ImageList(this.components);
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_gridTechnicianOrdered = new SmartSchedule.Win32.Controls.Grid();
            this.m_gridTechnicianOrderedView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.m_colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.m_colTechnicianName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.m_btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.m_imagesUpDown = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTechnicianUnordered)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTechnicianUnorderedView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTechnicianOrdered)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTechnicianOrderedView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_btnMoveToInvisibleAll);
            this.panelControl1.Controls.Add(this.m_btnMoveToVisibleAll);
            this.panelControl1.Controls.Add(this.m_btnMoveToInvisible);
            this.panelControl1.Controls.Add(this.m_btnMoveToVisible);
            this.panelControl1.Controls.Add(this.m_gridTechnicianUnordered);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.m_gridTechnicianOrdered);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.m_btnOk);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(563, 563);
            this.panelControl1.TabIndex = 0;
            // 
            // m_btnMoveToInvisibleAll
            // 
            this.m_btnMoveToInvisibleAll.Location = new System.Drawing.Point(264, 315);
            this.m_btnMoveToInvisibleAll.Name = "m_btnMoveToInvisibleAll";
            this.m_btnMoveToInvisibleAll.Size = new System.Drawing.Size(33, 23);
            this.m_btnMoveToInvisibleAll.TabIndex = 16;
            this.m_btnMoveToInvisibleAll.TabStop = false;
            this.m_btnMoveToInvisibleAll.Text = "<<";
            // 
            // m_btnMoveToVisibleAll
            // 
            this.m_btnMoveToVisibleAll.Location = new System.Drawing.Point(264, 286);
            this.m_btnMoveToVisibleAll.Name = "m_btnMoveToVisibleAll";
            this.m_btnMoveToVisibleAll.Size = new System.Drawing.Size(33, 23);
            this.m_btnMoveToVisibleAll.TabIndex = 15;
            this.m_btnMoveToVisibleAll.TabStop = false;
            this.m_btnMoveToVisibleAll.Text = ">>";
            // 
            // m_btnMoveToInvisible
            // 
            this.m_btnMoveToInvisible.Location = new System.Drawing.Point(264, 246);
            this.m_btnMoveToInvisible.Name = "m_btnMoveToInvisible";
            this.m_btnMoveToInvisible.Size = new System.Drawing.Size(33, 23);
            this.m_btnMoveToInvisible.TabIndex = 14;
            this.m_btnMoveToInvisible.TabStop = false;
            this.m_btnMoveToInvisible.Text = "<";
            // 
            // m_btnMoveToVisible
            // 
            this.m_btnMoveToVisible.Location = new System.Drawing.Point(264, 217);
            this.m_btnMoveToVisible.Name = "m_btnMoveToVisible";
            this.m_btnMoveToVisible.Size = new System.Drawing.Size(33, 23);
            this.m_btnMoveToVisible.TabIndex = 13;
            this.m_btnMoveToVisible.TabStop = false;
            this.m_btnMoveToVisible.Text = ">";
            // 
            // m_gridTechnicianUnordered
            // 
            this.m_gridTechnicianUnordered.AllowDrop = true;
            this.m_gridTechnicianUnordered.IsReadOnly = false;
            this.m_gridTechnicianUnordered.Location = new System.Drawing.Point(3, 22);
            this.m_gridTechnicianUnordered.MainView = this.m_gridTechnicianUnorderedView;
            this.m_gridTechnicianUnordered.Name = "m_gridTechnicianUnordered";
            this.m_gridTechnicianUnordered.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox2});
            this.m_gridTechnicianUnordered.Size = new System.Drawing.Size(255, 511);
            this.m_gridTechnicianUnordered.TabIndex = 0;
            this.m_gridTechnicianUnordered.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridTechnicianUnorderedView});
            // 
            // m_gridTechnicianUnorderedView
            // 
            this.m_gridTechnicianUnorderedView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2});
            this.m_gridTechnicianUnorderedView.GridControl = this.m_gridTechnicianUnordered;
            this.m_gridTechnicianUnorderedView.Name = "m_gridTechnicianUnorderedView";
            this.m_gridTechnicianUnorderedView.OptionsBehavior.AllowIncrementalSearch = true;
            this.m_gridTechnicianUnorderedView.OptionsBehavior.Editable = false;
            this.m_gridTechnicianUnorderedView.OptionsBehavior.FocusLeaveOnTab = true;
            this.m_gridTechnicianUnorderedView.OptionsCustomization.AllowFilter = false;
            this.m_gridTechnicianUnorderedView.OptionsCustomization.AllowGroup = false;
            this.m_gridTechnicianUnorderedView.OptionsDetail.EnableMasterViewMode = false;
            this.m_gridTechnicianUnorderedView.OptionsDetail.ShowDetailTabs = false;
            this.m_gridTechnicianUnorderedView.OptionsMenu.EnableColumnMenu = false;
            this.m_gridTechnicianUnorderedView.OptionsNavigation.UseTabKey = false;
            this.m_gridTechnicianUnorderedView.OptionsSelection.MultiSelect = true;
            this.m_gridTechnicianUnorderedView.OptionsView.ShowDetailButtons = false;
            this.m_gridTechnicianUnorderedView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Name";
            this.gridColumn2.FieldName = "Name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 283;
            // 
            // repositoryItemImageComboBox2
            // 
            this.repositoryItemImageComboBox2.AutoHeight = false;
            this.repositoryItemImageComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox2.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 0, 0)});
            this.repositoryItemImageComboBox2.Name = "repositoryItemImageComboBox2";
            this.repositoryItemImageComboBox2.SmallImages = this.m_imagesYesNo;
            // 
            // m_imagesYesNo
            // 
            this.m_imagesYesNo.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imagesYesNo.ImageStream")));
            this.m_imagesYesNo.TransparentColor = System.Drawing.Color.Magenta;
            this.m_imagesYesNo.Images.SetKeyName(0, "no.bmp");
            this.m_imagesYesNo.Images.SetKeyName(1, "yes.bmp");
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(6, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(109, 13);
            this.labelControl1.TabIndex = 11;
            this.labelControl1.Text = "Unordered Technicians";
            // 
            // m_gridTechnicianOrdered
            // 
            this.m_gridTechnicianOrdered.AllowDrop = true;
            this.m_gridTechnicianOrdered.IsReadOnly = false;
            this.m_gridTechnicianOrdered.Location = new System.Drawing.Point(303, 22);
            this.m_gridTechnicianOrdered.MainView = this.m_gridTechnicianOrderedView;
            this.m_gridTechnicianOrdered.Name = "m_gridTechnicianOrdered";
            this.m_gridTechnicianOrdered.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1});
            this.m_gridTechnicianOrdered.Size = new System.Drawing.Size(255, 511);
            this.m_gridTechnicianOrdered.TabIndex = 1;
            this.m_gridTechnicianOrdered.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridTechnicianOrderedView});
            // 
            // m_gridTechnicianOrderedView
            // 
            this.m_gridTechnicianOrderedView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.m_colStatus,
            this.m_colTechnicianName});
            this.m_gridTechnicianOrderedView.GridControl = this.m_gridTechnicianOrdered;
            this.m_gridTechnicianOrderedView.Name = "m_gridTechnicianOrderedView";
            this.m_gridTechnicianOrderedView.OptionsBehavior.AllowIncrementalSearch = true;
            this.m_gridTechnicianOrderedView.OptionsBehavior.Editable = false;
            this.m_gridTechnicianOrderedView.OptionsBehavior.FocusLeaveOnTab = true;
            this.m_gridTechnicianOrderedView.OptionsCustomization.AllowFilter = false;
            this.m_gridTechnicianOrderedView.OptionsCustomization.AllowGroup = false;
            this.m_gridTechnicianOrderedView.OptionsDetail.EnableMasterViewMode = false;
            this.m_gridTechnicianOrderedView.OptionsDetail.ShowDetailTabs = false;
            this.m_gridTechnicianOrderedView.OptionsMenu.EnableColumnMenu = false;
            this.m_gridTechnicianOrderedView.OptionsNavigation.UseTabKey = false;
            this.m_gridTechnicianOrderedView.OptionsSelection.MultiSelect = true;
            this.m_gridTechnicianOrderedView.OptionsView.ShowDetailButtons = false;
            this.m_gridTechnicianOrderedView.OptionsView.ShowGroupPanel = false;
            // 
            // m_colStatus
            // 
            this.m_colStatus.Caption = " ";
            this.m_colStatus.ColumnEdit = this.repositoryItemImageComboBox1;
            this.m_colStatus.FieldName = "ImageIndex";
            this.m_colStatus.Name = "m_colStatus";
            this.m_colStatus.Visible = true;
            this.m_colStatus.VisibleIndex = 0;
            this.m_colStatus.Width = 21;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 0, 0)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            this.repositoryItemImageComboBox1.SmallImages = this.m_imagesYesNo;
            // 
            // m_colTechnicianName
            // 
            this.m_colTechnicianName.Caption = "Name";
            this.m_colTechnicianName.FieldName = "Name";
            this.m_colTechnicianName.Name = "m_colTechnicianName";
            this.m_colTechnicianName.Visible = true;
            this.m_colTechnicianName.VisibleIndex = 1;
            this.m_colTechnicianName.Width = 283;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(303, 3);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(98, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Ordered Technicians";
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.Location = new System.Drawing.Point(404, 537);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 2;
            this.m_btnOk.Text = "OK";
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(485, 537);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 3;
            this.m_btnCancel.Text = "Cancel";
            // 
            // m_imagesUpDown
            // 
            this.m_imagesUpDown.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imagesUpDown.ImageStream")));
            this.m_imagesUpDown.TransparentColor = System.Drawing.Color.Magenta;
            this.m_imagesUpDown.Images.SetKeyName(0, "down.bmp");
            this.m_imagesUpDown.Images.SetKeyName(1, "up.bmp");
            // 
            // TechnicianArrangementView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(563, 563);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TechnicianArrangementView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TechnicianArrangementView";
            this.Controls.SetChildIndex(this.panelControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTechnicianUnordered)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTechnicianUnorderedView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTechnicianOrdered)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridTechnicianOrderedView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnCancel;
        internal DevExpress.XtraEditors.SimpleButton m_btnOk;
        private System.Windows.Forms.ImageList m_imagesYesNo;
        private System.Windows.Forms.ImageList m_imagesUpDown;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        internal Grid m_gridTechnicianOrdered;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridTechnicianOrderedView;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colStatus;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colTechnicianName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        internal Grid m_gridTechnicianUnordered;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridTechnicianUnorderedView;
        internal DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox2;
        internal DevExpress.XtraEditors.SimpleButton m_btnMoveToInvisibleAll;
        internal DevExpress.XtraEditors.SimpleButton m_btnMoveToVisibleAll;
        internal DevExpress.XtraEditors.SimpleButton m_btnMoveToInvisible;
        internal DevExpress.XtraEditors.SimpleButton m_btnMoveToVisible;

    }
}