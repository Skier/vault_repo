namespace Dalworth.Server.MainForm.MapscoLookup
{
    partial class MapscoLookupView
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
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtStreet = new DevExpress.XtraEditors.TextEdit();
            this.m_gridMapsco = new DevExpress.XtraGrid.GridControl();
            this.m_gridViewMapsco = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.m_colFirstName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_colLastName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_colHomePhone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_colBusPhone = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_colBlock = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.m_btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.m_timerFilter = new System.Windows.Forms.Timer(this.components);
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtStreet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridMapsco)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewMapsco)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.m_txtStreet);
            this.panelControl1.Controls.Add(this.m_gridMapsco);
            this.panelControl1.Controls.Add(this.panelControl3);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(619, 568);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.Location = new System.Drawing.Point(33, 76);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(0, 0);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "&B Shortcut";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(30, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "&Street";
            // 
            // m_txtStreet
            // 
            this.m_txtStreet.Location = new System.Drawing.Point(44, 9);
            this.m_txtStreet.Name = "m_txtStreet";
            this.m_txtStreet.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_txtStreet.Size = new System.Drawing.Size(240, 20);
            this.m_txtStreet.TabIndex = 1;
            // 
            // m_gridMapsco
            // 
            this.m_gridMapsco.EmbeddedNavigator.Name = "";
            this.m_gridMapsco.Location = new System.Drawing.Point(0, 37);
            this.m_gridMapsco.MainView = this.m_gridViewMapsco;
            this.m_gridMapsco.Name = "m_gridMapsco";
            this.m_gridMapsco.Size = new System.Drawing.Size(619, 500);
            this.m_gridMapsco.TabIndex = 4;
            this.m_gridMapsco.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridViewMapsco});
            // 
            // m_gridViewMapsco
            // 
            this.m_gridViewMapsco.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.m_colFirstName,
            this.m_colLastName,
            this.m_colHomePhone,
            this.m_colBusPhone,
            this.m_colBlock,
            this.gridColumn5});
            this.m_gridViewMapsco.GridControl = this.m_gridMapsco;
            this.m_gridViewMapsco.GroupFormat = "{0}[#image]{1} {2}";
            this.m_gridViewMapsco.Name = "m_gridViewMapsco";
            this.m_gridViewMapsco.OptionsBehavior.AutoExpandAllGroups = true;
            this.m_gridViewMapsco.OptionsCustomization.AllowFilter = false;
            this.m_gridViewMapsco.OptionsCustomization.AllowGroup = false;
            this.m_gridViewMapsco.OptionsMenu.EnableColumnMenu = false;
            this.m_gridViewMapsco.OptionsNavigation.UseTabKey = false;
            this.m_gridViewMapsco.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.m_gridViewMapsco.OptionsView.RowAutoHeight = true;
            this.m_gridViewMapsco.OptionsView.ShowGroupPanel = false;
            // 
            // m_colFirstName
            // 
            this.m_colFirstName.Caption = "Street";
            this.m_colFirstName.FieldName = "StreetFull";
            this.m_colFirstName.Name = "m_colFirstName";
            this.m_colFirstName.OptionsColumn.AllowEdit = false;
            this.m_colFirstName.Visible = true;
            this.m_colFirstName.VisibleIndex = 0;
            this.m_colFirstName.Width = 182;
            // 
            // m_colLastName
            // 
            this.m_colLastName.Caption = "City";
            this.m_colLastName.FieldName = "City";
            this.m_colLastName.Name = "m_colLastName";
            this.m_colLastName.OptionsColumn.AllowEdit = false;
            this.m_colLastName.Visible = true;
            this.m_colLastName.VisibleIndex = 1;
            this.m_colLastName.Width = 124;
            // 
            // m_colHomePhone
            // 
            this.m_colHomePhone.Caption = "Zip";
            this.m_colHomePhone.FieldName = "Zip";
            this.m_colHomePhone.Name = "m_colHomePhone";
            this.m_colHomePhone.OptionsColumn.AllowEdit = false;
            this.m_colHomePhone.Visible = true;
            this.m_colHomePhone.VisibleIndex = 2;
            this.m_colHomePhone.Width = 68;
            // 
            // m_colBusPhone
            // 
            this.m_colBusPhone.Caption = "Block Start";
            this.m_colBusPhone.FieldName = "BlockBegin";
            this.m_colBusPhone.Name = "m_colBusPhone";
            this.m_colBusPhone.OptionsColumn.AllowEdit = false;
            this.m_colBusPhone.Visible = true;
            this.m_colBusPhone.VisibleIndex = 3;
            this.m_colBusPhone.Width = 66;
            // 
            // m_colBlock
            // 
            this.m_colBlock.Caption = "Block End";
            this.m_colBlock.FieldName = "BlockEnd";
            this.m_colBlock.Name = "m_colBlock";
            this.m_colBlock.OptionsColumn.AllowEdit = false;
            this.m_colBlock.Visible = true;
            this.m_colBlock.VisibleIndex = 4;
            this.m_colBlock.Width = 58;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Mapsco";
            this.gridColumn5.FieldName = "MapscoFull";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 100;
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.m_btnClose);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(0, 537);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(619, 31);
            this.panelControl3.TabIndex = 15;
            // 
            // m_btnClose
            // 
            this.m_btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnClose.Location = new System.Drawing.Point(541, 5);
            this.m_btnClose.Name = "m_btnClose";
            this.m_btnClose.Size = new System.Drawing.Size(75, 23);
            this.m_btnClose.TabIndex = 0;
            this.m_btnClose.Text = "&Close";
            // 
            // m_timerFilter
            // 
            this.m_timerFilter.Interval = 500;
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // MapscoLookupView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnClose;
            this.ClientSize = new System.Drawing.Size(619, 568);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MapscoLookupView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CustomerLookupView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtStreet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridMapsco)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewMapsco)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnClose;
        internal DevExpress.XtraGrid.GridControl m_gridMapsco;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridViewMapsco;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colHomePhone;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colBusPhone;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colFirstName;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colLastName;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colBlock;
        internal System.Windows.Forms.Timer m_timerFilter;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        internal DevExpress.XtraEditors.TextEdit m_txtStreet;
    }
}