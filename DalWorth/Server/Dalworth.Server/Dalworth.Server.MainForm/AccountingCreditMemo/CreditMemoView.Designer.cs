namespace Dalworth.Server.MainForm.AccountingCreditMemo
{
    partial class CreditMemoView
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
            this.m_panelInvoiceDetail = new DevExpress.XtraEditors.PanelControl();
            this.m_btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.m_lblQbTemplate = new DevExpress.XtraEditors.LabelControl();
            this.m_lblSalesTaxName = new DevExpress.XtraEditors.LabelControl();
            this.m_lblQbAccount = new DevExpress.XtraEditors.LabelControl();
            this.m_lblRep = new DevExpress.XtraEditors.LabelControl();
            this.m_lblQbClass = new DevExpress.XtraEditors.LabelControl();
            this.labelControl26 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblClaimNumber = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblTerms = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblDate = new DevExpress.XtraEditors.LabelControl();
            this.m_lblClosedAmount = new DevExpress.XtraEditors.LabelControl();
            this.m_lblCreditNumber = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.m_ctlShipAddress = new Dalworth.Server.MainForm.Components.AddressViewEdit();
            this.m_lblProjectType = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtMemo = new DevExpress.XtraEditors.MemoEdit();
            this.m_lblProjectId = new DevExpress.XtraEditors.LabelControl();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblSubTotal = new DevExpress.XtraEditors.LabelControl();
            this.m_lblCustomer = new DevExpress.XtraEditors.LabelControl();
            this.m_lblTotalAmount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblSalesTaxTotal = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.m_gridMemoLines = new DevExpress.XtraGrid.GridControl();
            this.m_gridMemoLinesView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Item = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Quantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Description = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Rate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Amount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Tax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblRemainingCredit = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_panelInvoiceDetail)).BeginInit();
            this.m_panelInvoiceDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtMemo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridMemoLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridMemoLinesView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_panelInvoiceDetail);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(764, 453);
            this.panelControl1.TabIndex = 0;
            // 
            // m_panelInvoiceDetail
            // 
            this.m_panelInvoiceDetail.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.m_panelInvoiceDetail.Controls.Add(this.labelControl5);
            this.m_panelInvoiceDetail.Controls.Add(this.m_lblRemainingCredit);
            this.m_panelInvoiceDetail.Controls.Add(this.m_btnClose);
            this.m_panelInvoiceDetail.Controls.Add(this.m_lblQbTemplate);
            this.m_panelInvoiceDetail.Controls.Add(this.m_lblSalesTaxName);
            this.m_panelInvoiceDetail.Controls.Add(this.m_lblQbAccount);
            this.m_panelInvoiceDetail.Controls.Add(this.m_lblRep);
            this.m_panelInvoiceDetail.Controls.Add(this.m_lblQbClass);
            this.m_panelInvoiceDetail.Controls.Add(this.labelControl26);
            this.m_panelInvoiceDetail.Controls.Add(this.m_lblClaimNumber);
            this.m_panelInvoiceDetail.Controls.Add(this.labelControl9);
            this.m_panelInvoiceDetail.Controls.Add(this.m_lblTerms);
            this.m_panelInvoiceDetail.Controls.Add(this.labelControl8);
            this.m_panelInvoiceDetail.Controls.Add(this.m_lblDate);
            this.m_panelInvoiceDetail.Controls.Add(this.m_lblClosedAmount);
            this.m_panelInvoiceDetail.Controls.Add(this.m_lblCreditNumber);
            this.m_panelInvoiceDetail.Controls.Add(this.labelControl6);
            this.m_panelInvoiceDetail.Controls.Add(this.m_ctlShipAddress);
            this.m_panelInvoiceDetail.Controls.Add(this.m_lblProjectType);
            this.m_panelInvoiceDetail.Controls.Add(this.labelControl4);
            this.m_panelInvoiceDetail.Controls.Add(this.m_txtMemo);
            this.m_panelInvoiceDetail.Controls.Add(this.m_lblProjectId);
            this.m_panelInvoiceDetail.Controls.Add(this.labelControl18);
            this.m_panelInvoiceDetail.Controls.Add(this.labelControl1);
            this.m_panelInvoiceDetail.Controls.Add(this.m_lblSubTotal);
            this.m_panelInvoiceDetail.Controls.Add(this.m_lblCustomer);
            this.m_panelInvoiceDetail.Controls.Add(this.m_lblTotalAmount);
            this.m_panelInvoiceDetail.Controls.Add(this.labelControl3);
            this.m_panelInvoiceDetail.Controls.Add(this.labelControl7);
            this.m_panelInvoiceDetail.Controls.Add(this.m_lblSalesTaxTotal);
            this.m_panelInvoiceDetail.Controls.Add(this.labelControl2);
            this.m_panelInvoiceDetail.Controls.Add(this.m_gridMemoLines);
            this.m_panelInvoiceDetail.Controls.Add(this.labelControl17);
            this.m_panelInvoiceDetail.Controls.Add(this.labelControl16);
            this.m_panelInvoiceDetail.Controls.Add(this.labelControl15);
            this.m_panelInvoiceDetail.Controls.Add(this.labelControl13);
            this.m_panelInvoiceDetail.Controls.Add(this.labelControl12);
            this.m_panelInvoiceDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_panelInvoiceDetail.Location = new System.Drawing.Point(0, 0);
            this.m_panelInvoiceDetail.Name = "m_panelInvoiceDetail";
            this.m_panelInvoiceDetail.Size = new System.Drawing.Size(764, 453);
            this.m_panelInvoiceDetail.TabIndex = 2;
            // 
            // m_btnClose
            // 
            this.m_btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnClose.Location = new System.Drawing.Point(677, 427);
            this.m_btnClose.Name = "m_btnClose";
            this.m_btnClose.Size = new System.Drawing.Size(83, 23);
            this.m_btnClose.TabIndex = 7;
            this.m_btnClose.Text = "&Close";
            // 
            // m_lblQbTemplate
            // 
            this.m_lblQbTemplate.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblQbTemplate.Appearance.Options.UseFont = true;
            this.m_lblQbTemplate.Location = new System.Drawing.Point(315, 44);
            this.m_lblQbTemplate.Name = "m_lblQbTemplate";
            this.m_lblQbTemplate.Size = new System.Drawing.Size(87, 13);
            this.m_lblQbTemplate.TabIndex = 27;
            this.m_lblQbTemplate.Text = "Service Invoice";
            // 
            // m_lblInvoiceTax
            // 
            this.m_lblSalesTaxName.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblSalesTaxName.Appearance.Options.UseFont = true;
            this.m_lblSalesTaxName.Location = new System.Drawing.Point(620, 323);
            this.m_lblSalesTaxName.Name = "m_lblSalesTaxName";
            this.m_lblSalesTaxName.Size = new System.Drawing.Size(33, 13);
            this.m_lblSalesTaxName.TabIndex = 47;
            this.m_lblSalesTaxName.Text = "8.25%";
            // 
            // m_lblQbAccount
            // 
            this.m_lblQbAccount.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblQbAccount.Appearance.Options.UseFont = true;
            this.m_lblQbAccount.Location = new System.Drawing.Point(315, 25);
            this.m_lblQbAccount.Name = "m_lblQbAccount";
            this.m_lblQbAccount.Size = new System.Drawing.Size(135, 13);
            this.m_lblQbAccount.TabIndex = 26;
            this.m_lblQbAccount.Text = "AR: Service Receivables";
            // 
            // m_lblRep
            // 
            this.m_lblRep.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblRep.Appearance.Options.UseFont = true;
            this.m_lblRep.Location = new System.Drawing.Point(98, 63);
            this.m_lblRep.Name = "m_lblRep";
            this.m_lblRep.Size = new System.Drawing.Size(65, 13);
            this.m_lblRep.TabIndex = 46;
            this.m_lblRep.Text = "Archer, Ken";
            // 
            // m_lblQbClass
            // 
            this.m_lblQbClass.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblQbClass.Appearance.Options.UseFont = true;
            this.m_lblQbClass.Location = new System.Drawing.Point(315, 6);
            this.m_lblQbClass.Name = "m_lblQbClass";
            this.m_lblQbClass.Size = new System.Drawing.Size(47, 13);
            this.m_lblQbClass.TabIndex = 25;
            this.m_lblQbClass.Text = "30 - Rug";
            // 
            // labelControl26
            // 
            this.labelControl26.Location = new System.Drawing.Point(250, 44);
            this.labelControl26.Name = "labelControl26";
            this.labelControl26.Size = new System.Drawing.Size(44, 13);
            this.labelControl26.TabIndex = 24;
            this.labelControl26.Text = "Template";
            // 
            // m_lblClaimNumber
            // 
            this.m_lblClaimNumber.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblClaimNumber.Appearance.Options.UseFont = true;
            this.m_lblClaimNumber.Location = new System.Drawing.Point(667, 44);
            this.m_lblClaimNumber.Name = "m_lblClaimNumber";
            this.m_lblClaimNumber.Size = new System.Drawing.Size(7, 13);
            this.m_lblClaimNumber.TabIndex = 45;
            this.m_lblClaimNumber.Text = "1";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(250, 25);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(39, 13);
            this.labelControl9.TabIndex = 21;
            this.labelControl9.Text = "Account";
            // 
            // m_lblTerms
            // 
            this.m_lblTerms.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblTerms.Appearance.Options.UseFont = true;
            this.m_lblTerms.Location = new System.Drawing.Point(315, 63);
            this.m_lblTerms.Name = "m_lblTerms";
            this.m_lblTerms.Size = new System.Drawing.Size(76, 13);
            this.m_lblTerms.TabIndex = 44;
            this.m_lblTerms.Text = "1% 10 Net 30";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(250, 6);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(25, 13);
            this.labelControl8.TabIndex = 19;
            this.labelControl8.Text = "Class";
            // 
            // m_lblInvoiceDate
            // 
            this.m_lblDate.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblDate.Appearance.Options.UseFont = true;
            this.m_lblDate.Location = new System.Drawing.Point(98, 44);
            this.m_lblDate.Name = "m_lblDate";
            this.m_lblDate.Size = new System.Drawing.Size(54, 13);
            this.m_lblDate.TabIndex = 43;
            this.m_lblDate.Text = "9/8/2010";
            // 
            // m_lblClosedAmount
            // 
            this.m_lblClosedAmount.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblClosedAmount.Appearance.Options.UseFont = true;
            this.m_lblClosedAmount.Location = new System.Drawing.Point(667, 63);
            this.m_lblClosedAmount.Name = "m_lblClosedAmount";
            this.m_lblClosedAmount.Size = new System.Drawing.Size(45, 13);
            this.m_lblClosedAmount.TabIndex = 18;
            this.m_lblClosedAmount.Text = "$200.12";
            // 
            // m_lblCreditNumber
            // 
            this.m_lblCreditNumber.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblCreditNumber.Appearance.Options.UseFont = true;
            this.m_lblCreditNumber.Location = new System.Drawing.Point(98, 6);
            this.m_lblCreditNumber.Name = "m_lblCreditNumber";
            this.m_lblCreditNumber.Size = new System.Drawing.Size(35, 13);
            this.m_lblCreditNumber.TabIndex = 42;
            this.m_lblCreditNumber.Text = "34534";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(576, 63);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(72, 13);
            this.labelControl6.TabIndex = 17;
            this.labelControl6.Text = "Closed Amount";
            // 
            // m_ctlShipAddress
            // 
            this.m_ctlShipAddress.BaseAddress = null;
            this.m_ctlShipAddress.BaseAddressName = null;
            this.m_ctlShipAddress.Caption = null;
            this.m_ctlShipAddress.CurrentAddress = null;
            this.m_ctlShipAddress.EditButtonText = "Edi&t";
            this.m_ctlShipAddress.IsBaseAddressActive = false;
            this.m_ctlShipAddress.Location = new System.Drawing.Point(5, 300);
            this.m_ctlShipAddress.Margin = new System.Windows.Forms.Padding(1, 1, 1, 0);
            this.m_ctlShipAddress.Name = "m_ctlShipAddress";
            this.m_ctlShipAddress.Size = new System.Drawing.Size(265, 119);
            this.m_ctlShipAddress.TabIndex = 41;
            // 
            // m_lblProjectType
            // 
            this.m_lblProjectType.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblProjectType.Appearance.Options.UseFont = true;
            this.m_lblProjectType.Location = new System.Drawing.Point(667, 25);
            this.m_lblProjectType.Name = "m_lblProjectType";
            this.m_lblProjectType.Size = new System.Drawing.Size(73, 13);
            this.m_lblProjectType.TabIndex = 16;
            this.m_lblProjectType.Text = "Rug Cleaning";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(576, 25);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(61, 13);
            this.labelControl4.TabIndex = 15;
            this.labelControl4.Text = "Project Type";
            // 
            // m_txtInvoiceMemo
            // 
            this.m_txtMemo.Location = new System.Drawing.Point(274, 301);
            this.m_txtMemo.Name = "m_txtMemo";
            this.m_txtMemo.Properties.ReadOnly = true;
            this.m_txtMemo.Size = new System.Drawing.Size(314, 118);
            this.m_txtMemo.TabIndex = 8;
            // 
            // m_lblProjectId
            // 
            this.m_lblProjectId.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblProjectId.Appearance.Options.UseFont = true;
            this.m_lblProjectId.Location = new System.Drawing.Point(667, 6);
            this.m_lblProjectId.Name = "m_lblProjectId";
            this.m_lblProjectId.Size = new System.Drawing.Size(42, 13);
            this.m_lblProjectId.TabIndex = 14;
            this.m_lblProjectId.Text = "123123";
            // 
            // labelControl18
            // 
            this.labelControl18.Location = new System.Drawing.Point(596, 304);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(45, 13);
            this.labelControl18.TabIndex = 36;
            this.labelControl18.Text = "Sub Total";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(576, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 13);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "Project ID";
            // 
            // m_lblInvoiceSubTotal
            // 
            this.m_lblSubTotal.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblSubTotal.Appearance.Options.UseFont = true;
            this.m_lblSubTotal.Appearance.Options.UseTextOptions = true;
            this.m_lblSubTotal.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.m_lblSubTotal.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblSubTotal.Location = new System.Drawing.Point(652, 304);
            this.m_lblSubTotal.Name = "m_lblSubTotal";
            this.m_lblSubTotal.Size = new System.Drawing.Size(104, 13);
            this.m_lblSubTotal.TabIndex = 35;
            this.m_lblSubTotal.Text = "$35.00";
            // 
            // m_lblCustomer
            // 
            this.m_lblCustomer.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblCustomer.Appearance.Options.UseFont = true;
            this.m_lblCustomer.Location = new System.Drawing.Point(98, 25);
            this.m_lblCustomer.Name = "m_lblCustomer";
            this.m_lblCustomer.Size = new System.Drawing.Size(79, 13);
            this.m_lblCustomer.TabIndex = 12;
            this.m_lblCustomer.Text = "Johnson, Jack";
            // 
            // m_lblInvoiceTotal
            // 
            this.m_lblTotalAmount.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblTotalAmount.Appearance.Options.UseFont = true;
            this.m_lblTotalAmount.Appearance.Options.UseTextOptions = true;
            this.m_lblTotalAmount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.m_lblTotalAmount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblTotalAmount.Location = new System.Drawing.Point(652, 358);
            this.m_lblTotalAmount.Name = "m_lblTotalAmount";
            this.m_lblTotalAmount.Size = new System.Drawing.Size(104, 16);
            this.m_lblTotalAmount.TabIndex = 34;
            this.m_lblTotalAmount.Text = "$135.00";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(7, 25);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(46, 13);
            this.labelControl3.TabIndex = 11;
            this.labelControl3.Text = "Customer";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(596, 361);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(24, 13);
            this.labelControl7.TabIndex = 33;
            this.labelControl7.Text = "Total";
            // 
            // m_lblInvoiceTaxAmount
            // 
            this.m_lblSalesTaxTotal.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblSalesTaxTotal.Appearance.Options.UseFont = true;
            this.m_lblSalesTaxTotal.Appearance.Options.UseTextOptions = true;
            this.m_lblSalesTaxTotal.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.m_lblSalesTaxTotal.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblSalesTaxTotal.Location = new System.Drawing.Point(665, 323);
            this.m_lblSalesTaxTotal.Name = "m_lblSalesTaxTotal";
            this.m_lblSalesTaxTotal.Size = new System.Drawing.Size(91, 13);
            this.m_lblSalesTaxTotal.TabIndex = 32;
            this.m_lblSalesTaxTotal.Text = "$35.00";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(596, 323);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(18, 13);
            this.labelControl2.TabIndex = 30;
            this.labelControl2.Text = "Tax";
            // 
            // m_gridMemoLines
            // 
            this.m_gridMemoLines.EmbeddedNavigator.Name = "";
            this.m_gridMemoLines.Location = new System.Drawing.Point(5, 82);
            this.m_gridMemoLines.MainView = this.m_gridMemoLinesView;
            this.m_gridMemoLines.Name = "m_gridMemoLines";
            this.m_gridMemoLines.Size = new System.Drawing.Size(753, 214);
            this.m_gridMemoLines.TabIndex = 27;
            this.m_gridMemoLines.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridMemoLinesView});
            // 
            // m_gridMemoLinesView
            // 
            this.m_gridMemoLinesView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Item,
            this.Quantity,
            this.Description,
            this.Rate,
            this.Amount,
            this.Tax});
            this.m_gridMemoLinesView.GridControl = this.m_gridMemoLines;
            this.m_gridMemoLinesView.Name = "m_gridMemoLinesView";
            this.m_gridMemoLinesView.OptionsBehavior.Editable = false;
            this.m_gridMemoLinesView.OptionsCustomization.AllowFilter = false;
            this.m_gridMemoLinesView.OptionsCustomization.AllowGroup = false;
            this.m_gridMemoLinesView.OptionsCustomization.AllowSort = false;
            this.m_gridMemoLinesView.OptionsMenu.EnableColumnMenu = false;
            this.m_gridMemoLinesView.OptionsView.ShowDetailButtons = false;
            this.m_gridMemoLinesView.OptionsView.ShowGroupPanel = false;
            // 
            // Item
            // 
            this.Item.Caption = "Item";
            this.Item.FieldName = "QbItemName";
            this.Item.Name = "Item";
            this.Item.Visible = true;
            this.Item.VisibleIndex = 0;
            this.Item.Width = 269;
            // 
            // Quantity
            // 
            this.Quantity.Caption = "Quantity";
            this.Quantity.FieldName = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.Visible = true;
            this.Quantity.VisibleIndex = 1;
            this.Quantity.Width = 57;
            // 
            // Description
            // 
            this.Description.Caption = "Description";
            this.Description.FieldName = "Description";
            this.Description.Name = "Description";
            this.Description.Visible = true;
            this.Description.VisibleIndex = 2;
            this.Description.Width = 204;
            // 
            // Rate
            // 
            this.Rate.Caption = "Rate";
            this.Rate.DisplayFormat.FormatString = "C";
            this.Rate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.Rate.FieldName = "Rate";
            this.Rate.Name = "Rate";
            this.Rate.Visible = true;
            this.Rate.VisibleIndex = 3;
            this.Rate.Width = 61;
            // 
            // Amount
            // 
            this.Amount.Caption = "Amount";
            this.Amount.DisplayFormat.FormatString = "C";
            this.Amount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.Amount.FieldName = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.Visible = true;
            this.Amount.VisibleIndex = 4;
            this.Amount.Width = 104;
            // 
            // Tax
            // 
            this.Tax.Caption = "Tax";
            this.Tax.FieldName = "QbSalesTaxCodeName";
            this.Tax.Name = "Tax";
            this.Tax.Visible = true;
            this.Tax.VisibleIndex = 5;
            this.Tax.Width = 37;
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(576, 44);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(65, 13);
            this.labelControl17.TabIndex = 25;
            this.labelControl17.Text = "Claim Number";
            // 
            // labelControl16
            // 
            this.labelControl16.Location = new System.Drawing.Point(7, 63);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(19, 13);
            this.labelControl16.TabIndex = 23;
            this.labelControl16.Text = "Rep";
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(250, 63);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(29, 13);
            this.labelControl15.TabIndex = 22;
            this.labelControl15.Text = "Terms";
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(7, 44);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(23, 13);
            this.labelControl13.TabIndex = 17;
            this.labelControl13.Text = "Date";
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(7, 6);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(49, 13);
            this.labelControl12.TabIndex = 16;
            this.labelControl12.Text = "Credit No.";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(596, 393);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(49, 26);
            this.labelControl5.TabIndex = 49;
            this.labelControl5.Text = "Remaining\r\nCredit";
            // 
            // m_lblRemainingCredit
            // 
            this.m_lblRemainingCredit.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblRemainingCredit.Appearance.Options.UseFont = true;
            this.m_lblRemainingCredit.Appearance.Options.UseTextOptions = true;
            this.m_lblRemainingCredit.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.m_lblRemainingCredit.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblRemainingCredit.Location = new System.Drawing.Point(652, 406);
            this.m_lblRemainingCredit.Name = "m_lblRemainingCredit";
            this.m_lblRemainingCredit.Size = new System.Drawing.Size(105, 13);
            this.m_lblRemainingCredit.TabIndex = 48;
            this.m_lblRemainingCredit.Text = "$35.00";
            // 
            // CreditMemoView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 453);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CreditMemoView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Credit Memo";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_panelInvoiceDetail)).EndInit();
            this.m_panelInvoiceDetail.ResumeLayout(false);
            this.m_panelInvoiceDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtMemo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridMemoLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridMemoLinesView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        internal DevExpress.XtraEditors.SimpleButton m_btnClose;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraGrid.Columns.GridColumn Item;
        private DevExpress.XtraGrid.Columns.GridColumn Quantity;
        private DevExpress.XtraGrid.Columns.GridColumn Description;
        private DevExpress.XtraGrid.Columns.GridColumn Rate;
        private DevExpress.XtraGrid.Columns.GridColumn Tax;
        internal DevExpress.XtraEditors.LabelControl m_lblCustomer;
        internal DevExpress.XtraEditors.LabelControl m_lblProjectId;
        internal DevExpress.XtraEditors.LabelControl m_lblProjectType;
        internal DevExpress.XtraEditors.LabelControl m_lblClosedAmount;
        internal DevExpress.XtraGrid.GridControl m_gridMemoLines;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridMemoLinesView;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        internal DevExpress.XtraEditors.LabelControl m_lblTotalAmount;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        internal DevExpress.XtraEditors.LabelControl m_lblSubTotal;
        public DevExpress.XtraGrid.Columns.GridColumn Amount;
        internal DevExpress.XtraEditors.LabelControl m_lblSalesTaxTotal;
        private DevExpress.XtraEditors.LabelControl labelControl26;
        internal DevExpress.XtraEditors.PanelControl m_panelInvoiceDetail;
        internal DevExpress.XtraEditors.LabelControl m_lblQbTemplate;
        internal DevExpress.XtraEditors.LabelControl m_lblQbAccount;
        internal DevExpress.XtraEditors.LabelControl m_lblQbClass;
        internal Dalworth.Server.MainForm.Components.AddressViewEdit m_ctlShipAddress;
        internal DevExpress.XtraEditors.LabelControl m_lblRep;
        internal DevExpress.XtraEditors.LabelControl m_lblClaimNumber;
        internal DevExpress.XtraEditors.LabelControl m_lblTerms;
        internal DevExpress.XtraEditors.LabelControl m_lblDate;
        internal DevExpress.XtraEditors.LabelControl m_lblCreditNumber;
        internal DevExpress.XtraEditors.LabelControl m_lblSalesTaxName;
        internal DevExpress.XtraEditors.MemoEdit m_txtMemo;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        internal DevExpress.XtraEditors.LabelControl m_lblRemainingCredit;
    }
}