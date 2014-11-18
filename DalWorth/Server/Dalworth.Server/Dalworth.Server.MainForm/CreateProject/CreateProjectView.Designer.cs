using Dalworth.Server.MainForm.Components;
using DevExpress.XtraEditors;

namespace Dalworth.Server.MainForm.CreateProject
{
    partial class CreateProjectView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateProjectView));
            this.m_btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.m_txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.m_btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl7 = new DevExpress.XtraEditors.GroupControl();
            this.m_lblOutstandingAmount = new DevExpress.XtraEditors.LabelControl();
            this.m_lblCollectedAmount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl24 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblBilledAmount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl25 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblCreditAmount2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl20 = new DevExpress.XtraEditors.LabelControl();
            this.m_tabProjetDetail = new Dalworth.Server.MainForm.Components.DalworthTabControl();
            this.m_tabpgGeneral = new DevExpress.XtraTab.XtraTabPage();
            this.m_ctlProjectEdit = new Dalworth.Server.MainForm.Components.ProjectEdit();
            this.m_ctlAddressLookup = new Dalworth.Server.MainForm.Components.AddressViewEdit();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.m_dtpCompleteDate = new Dalworth.Server.MainForm.Components.DateEdit();
            this.labelControl22 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtJobNumber = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.m_dtpScopeDate = new Dalworth.Server.MainForm.Components.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.m_dtpDeclineDate = new Dalworth.Server.MainForm.Components.DateEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtJobCost = new DevExpress.XtraEditors.TextEdit();
            this.m_dtpSignUpDate = new Dalworth.Server.MainForm.Components.DateEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.m_dtpLeadDate = new Dalworth.Server.MainForm.Components.DateEdit();
            this.m_txtEstimatedAmount = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.m_cmbProjectType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.m_chkSelfGeneratedLead = new DevExpress.XtraEditors.CheckEdit();
            this.m_cmbProjectManager = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.m_lblNumber = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblProgress = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblStatus = new DevExpress.XtraEditors.LabelControl();
            this.m_tabpgTransaction = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.m_lblBControlBillPays = new DevExpress.XtraEditors.LabelControl();
            this.m_gridBillPayTransactions = new DevExpress.XtraGrid.GridControl();
            this.m_gridViewBillPayTransactions = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.m_colIssueDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_colBillPayAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_colBalance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.m_imagesVoid = new System.Windows.Forms.ImageList(this.components);
            this.m_cmbBillPayType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.m_lblLastBillingDate = new DevExpress.XtraEditors.LabelControl();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblLastPaymentDate = new DevExpress.XtraEditors.LabelControl();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.m_ctlCustomerLookup = new Dalworth.Server.MainForm.Components.CustomerViewEditLookup();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.m_txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl7)).BeginInit();
            this.groupControl7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_tabProjetDetail)).BeginInit();
            this.m_tabProjetDetail.SuspendLayout();
            this.m_tabpgGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpCompleteDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpCompleteDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtJobNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpScopeDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpScopeDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpDeclineDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpDeclineDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtJobCost.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpSignUpDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpSignUpDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpLeadDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpLeadDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtEstimatedAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbProjectType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkSelfGeneratedLead.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbProjectManager.Properties)).BeginInit();
            this.m_tabpgTransaction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).BeginInit();
            this.groupControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridBillPayTransactions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewBillPayTransactions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbBillPayType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.Location = new System.Drawing.Point(663, 754);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 3;
            this.m_btnOk.Text = "&Save";
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(744, 754);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 4;
            this.m_btnCancel.Text = "&Cancel";
            // 
            // m_txtNotes
            // 
            this.m_txtNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtNotes.Location = new System.Drawing.Point(2, 20);
            this.m_txtNotes.Name = "m_txtNotes";
            this.m_txtNotes.Properties.MaxLength = 2000;
            this.m_txtNotes.Size = new System.Drawing.Size(805, 106);
            this.m_txtNotes.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Controls.Add(this.m_btnPrint);
            this.panelControl1.Controls.Add(this.groupControl7);
            this.panelControl1.Controls.Add(this.m_tabProjetDetail);
            this.panelControl1.Controls.Add(this.m_ctlCustomerLookup);
            this.panelControl1.Controls.Add(this.m_btnOk);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(822, 780);
            this.panelControl1.TabIndex = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.m_txtNotes);
            this.groupControl1.Location = new System.Drawing.Point(0, 582);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(809, 128);
            this.groupControl1.TabIndex = 5;
            this.groupControl1.Text = "Notes";
            // 
            // m_btnPrint
            // 
            this.m_btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnPrint.Location = new System.Drawing.Point(7, 754);
            this.m_btnPrint.Name = "m_btnPrint";
            this.m_btnPrint.Size = new System.Drawing.Size(94, 23);
            this.m_btnPrint.TabIndex = 5;
            this.m_btnPrint.Text = "&Print and Save";
            // 
            // groupControl7
            // 
            this.groupControl7.Controls.Add(this.m_lblOutstandingAmount);
            this.groupControl7.Controls.Add(this.m_lblCollectedAmount);
            this.groupControl7.Controls.Add(this.labelControl24);
            this.groupControl7.Controls.Add(this.labelControl19);
            this.groupControl7.Controls.Add(this.m_lblBilledAmount);
            this.groupControl7.Controls.Add(this.labelControl25);
            this.groupControl7.Controls.Add(this.m_lblCreditAmount2);
            this.groupControl7.Controls.Add(this.labelControl20);
            this.groupControl7.Location = new System.Drawing.Point(284, 4);
            this.groupControl7.Name = "groupControl7";
            this.groupControl7.Size = new System.Drawing.Size(534, 105);
            this.groupControl7.TabIndex = 1;
            this.groupControl7.Text = "Project Amounts";
            // 
            // m_lblOutstandingAmount
            // 
            this.m_lblOutstandingAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblOutstandingAmount.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.m_lblOutstandingAmount.Appearance.Options.UseFont = true;
            this.m_lblOutstandingAmount.Appearance.Options.UseTextOptions = true;
            this.m_lblOutstandingAmount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.m_lblOutstandingAmount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblOutstandingAmount.Location = new System.Drawing.Point(391, 79);
            this.m_lblOutstandingAmount.Name = "m_lblOutstandingAmount";
            this.m_lblOutstandingAmount.Size = new System.Drawing.Size(115, 16);
            this.m_lblOutstandingAmount.TabIndex = 11;
            this.m_lblOutstandingAmount.Text = "($10 000.00)";
            // 
            // m_lblCollectedAmount
            // 
            this.m_lblCollectedAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblCollectedAmount.Appearance.Options.UseTextOptions = true;
            this.m_lblCollectedAmount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.m_lblCollectedAmount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblCollectedAmount.Location = new System.Drawing.Point(391, 60);
            this.m_lblCollectedAmount.Name = "m_lblCollectedAmount";
            this.m_lblCollectedAmount.Size = new System.Drawing.Size(115, 13);
            this.m_lblCollectedAmount.TabIndex = 7;
            this.m_lblCollectedAmount.Text = "($10 000.00)";
            // 
            // labelControl24
            // 
            this.labelControl24.Location = new System.Drawing.Point(344, 41);
            this.labelControl24.Name = "labelControl24";
            this.labelControl24.Size = new System.Drawing.Size(29, 13);
            this.labelControl24.TabIndex = 8;
            this.labelControl24.Text = "Credit";
            // 
            // labelControl19
            // 
            this.labelControl19.Location = new System.Drawing.Point(344, 23);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(24, 13);
            this.labelControl19.TabIndex = 2;
            this.labelControl19.Text = "Billed";
            // 
            // m_lblBilledAmount
            // 
            this.m_lblBilledAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblBilledAmount.Appearance.Options.UseTextOptions = true;
            this.m_lblBilledAmount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.m_lblBilledAmount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblBilledAmount.Location = new System.Drawing.Point(400, 23);
            this.m_lblBilledAmount.Name = "m_lblBilledAmount";
            this.m_lblBilledAmount.Size = new System.Drawing.Size(106, 13);
            this.m_lblBilledAmount.TabIndex = 3;
            this.m_lblBilledAmount.Text = "($10 000.00)";
            // 
            // labelControl25
            // 
            this.labelControl25.Location = new System.Drawing.Point(344, 60);
            this.labelControl25.Name = "labelControl25";
            this.labelControl25.Size = new System.Drawing.Size(44, 13);
            this.labelControl25.TabIndex = 10;
            this.labelControl25.Text = "Collected";
            // 
            // m_lblCreditAmount2
            // 
            this.m_lblCreditAmount2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblCreditAmount2.Appearance.Options.UseTextOptions = true;
            this.m_lblCreditAmount2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.m_lblCreditAmount2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblCreditAmount2.Location = new System.Drawing.Point(391, 41);
            this.m_lblCreditAmount2.Name = "m_lblCreditAmount2";
            this.m_lblCreditAmount2.Size = new System.Drawing.Size(115, 13);
            this.m_lblCreditAmount2.TabIndex = 9;
            this.m_lblCreditAmount2.Text = "($10 000.00)";
            // 
            // labelControl20
            // 
            this.labelControl20.Location = new System.Drawing.Point(344, 81);
            this.labelControl20.Name = "labelControl20";
            this.labelControl20.Size = new System.Drawing.Size(33, 13);
            this.labelControl20.TabIndex = 6;
            this.labelControl20.Text = "Unpaid";
            // 
            // m_tabProjetDetail
            // 
            this.m_tabProjetDetail.Location = new System.Drawing.Point(3, 115);
            this.m_tabProjetDetail.Name = "m_tabProjetDetail";
            this.m_tabProjetDetail.SelectedTabPage = this.m_tabpgGeneral;
            this.m_tabProjetDetail.Size = new System.Drawing.Size(818, 601);
            this.m_tabProjetDetail.TabIndex = 2;
            this.m_tabProjetDetail.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.m_tabpgGeneral,
            this.m_tabpgTransaction});
            this.m_tabProjetDetail.Text = "xtraTabControl1";
            // 
            // m_tabpgGeneral
            // 
            this.m_tabpgGeneral.Controls.Add(this.m_ctlProjectEdit);
            this.m_tabpgGeneral.Controls.Add(this.m_ctlAddressLookup);
            this.m_tabpgGeneral.Controls.Add(this.groupControl3);
            this.m_tabpgGeneral.Controls.Add(this.groupControl2);
            this.m_tabpgGeneral.Name = "m_tabpgGeneral";
            this.m_tabpgGeneral.Size = new System.Drawing.Size(809, 570);
            this.m_tabpgGeneral.Text = "General";
            // 
            // m_ctlProjectEdit
            // 
            this.m_ctlProjectEdit.AreaId = null;
            this.m_ctlProjectEdit.IsEditable = false;
            this.m_ctlProjectEdit.IsInsuranceVisible = false;
            this.m_ctlProjectEdit.IsQbSalesRepRequired = false;
            this.m_ctlProjectEdit.IsQbSalesRepVisible = true;
            this.m_ctlProjectEdit.Location = new System.Drawing.Point(278, 3);
            this.m_ctlProjectEdit.Name = "m_ctlProjectEdit";
            this.m_ctlProjectEdit.Project = null;
            this.m_ctlProjectEdit.ProjectInsurance = null;
            this.m_ctlProjectEdit.Size = new System.Drawing.Size(531, 352);
            this.m_ctlProjectEdit.TabIndex = 2;
            // 
            // m_ctlAddressLookup
            // 
            this.m_ctlAddressLookup.BaseAddress = null;
            this.m_ctlAddressLookup.BaseAddressName = null;
            this.m_ctlAddressLookup.Caption = null;
            this.m_ctlAddressLookup.CurrentAddress = null;
            this.m_ctlAddressLookup.EditButtonText = "Edi&t";
            this.m_ctlAddressLookup.IsBaseAddressActive = false;
            this.m_ctlAddressLookup.Location = new System.Drawing.Point(0, 3);
            this.m_ctlAddressLookup.Name = "m_ctlAddressLookup";
            this.m_ctlAddressLookup.Size = new System.Drawing.Size(275, 99);
            this.m_ctlAddressLookup.TabIndex = 0;
            this.m_ctlAddressLookup.TabStop = false;
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.m_dtpCompleteDate);
            this.groupControl3.Controls.Add(this.labelControl22);
            this.groupControl3.Controls.Add(this.m_txtJobNumber);
            this.groupControl3.Controls.Add(this.label2);
            this.groupControl3.Controls.Add(this.m_dtpScopeDate);
            this.groupControl3.Controls.Add(this.labelControl3);
            this.groupControl3.Controls.Add(this.labelControl13);
            this.groupControl3.Controls.Add(this.m_dtpDeclineDate);
            this.groupControl3.Controls.Add(this.labelControl12);
            this.groupControl3.Controls.Add(this.m_txtJobCost);
            this.groupControl3.Controls.Add(this.m_dtpSignUpDate);
            this.groupControl3.Controls.Add(this.labelControl11);
            this.groupControl3.Controls.Add(this.labelControl10);
            this.groupControl3.Controls.Add(this.m_dtpLeadDate);
            this.groupControl3.Controls.Add(this.m_txtEstimatedAmount);
            this.groupControl3.Controls.Add(this.labelControl8);
            this.groupControl3.Location = new System.Drawing.Point(0, 361);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(809, 75);
            this.groupControl3.TabIndex = 4;
            this.groupControl3.Text = "Details";
            // 
            // m_dtpCompleteDate
            // 
            this.m_dtpCompleteDate.EditValue = null;
            this.m_dtpCompleteDate.Location = new System.Drawing.Point(496, 48);
            this.m_dtpCompleteDate.Name = "m_dtpCompleteDate";
            this.m_dtpCompleteDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.m_dtpCompleteDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_dtpCompleteDate.Properties.NullText = "Undefined";
            this.m_dtpCompleteDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_dtpCompleteDate.Size = new System.Drawing.Size(97, 20);
            this.m_dtpCompleteDate.TabIndex = 11;
            // 
            // labelControl22
            // 
            this.labelControl22.Location = new System.Drawing.Point(419, 51);
            this.labelControl22.Name = "labelControl22";
            this.labelControl22.Size = new System.Drawing.Size(71, 13);
            this.labelControl22.TabIndex = 10;
            this.labelControl22.Text = "Complete Date";
            // 
            // m_txtJobNumber
            // 
            this.m_txtJobNumber.Location = new System.Drawing.Point(496, 23);
            this.m_txtJobNumber.Name = "m_txtJobNumber";
            this.m_txtJobNumber.Size = new System.Drawing.Size(97, 20);
            this.m_txtJobNumber.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(416, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "&Job No.";
            // 
            // m_dtpScopeDate
            // 
            this.m_dtpScopeDate.EditValue = null;
            this.m_dtpScopeDate.Location = new System.Drawing.Point(64, 48);
            this.m_dtpScopeDate.Name = "m_dtpScopeDate";
            this.m_dtpScopeDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.m_dtpScopeDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_dtpScopeDate.Properties.NullText = "Undefined";
            this.m_dtpScopeDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_dtpScopeDate.Size = new System.Drawing.Size(119, 20);
            this.m_dtpScopeDate.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(622, 51);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(42, 13);
            this.labelControl3.TabIndex = 14;
            this.labelControl3.Text = "Jo&b Cost";
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(622, 26);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(87, 13);
            this.labelControl13.TabIndex = 12;
            this.labelControl13.Text = "Estimated A&mount";
            // 
            // m_dtpDeclineDate
            // 
            this.m_dtpDeclineDate.EditValue = null;
            this.m_dtpDeclineDate.Location = new System.Drawing.Point(276, 48);
            this.m_dtpDeclineDate.Name = "m_dtpDeclineDate";
            this.m_dtpDeclineDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.m_dtpDeclineDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_dtpDeclineDate.Properties.NullText = "Undefined";
            this.m_dtpDeclineDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_dtpDeclineDate.Size = new System.Drawing.Size(119, 20);
            this.m_dtpDeclineDate.TabIndex = 7;
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(204, 51);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(66, 13);
            this.labelControl12.TabIndex = 6;
            this.labelControl12.Text = "Decli&ned Date";
            // 
            // m_txtJobCost
            // 
            this.m_txtJobCost.Location = new System.Drawing.Point(715, 48);
            this.m_txtJobCost.Name = "m_txtJobCost";
            this.m_txtJobCost.Properties.DisplayFormat.FormatString = "C";
            this.m_txtJobCost.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_txtJobCost.Properties.Mask.EditMask = "$###,###,###,##0.00;($###,###,###,##0.00)";
            this.m_txtJobCost.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.m_txtJobCost.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_txtJobCost.Size = new System.Drawing.Size(91, 20);
            this.m_txtJobCost.TabIndex = 15;
            // 
            // m_dtpSignUpDate
            // 
            this.m_dtpSignUpDate.EditValue = null;
            this.m_dtpSignUpDate.Location = new System.Drawing.Point(276, 23);
            this.m_dtpSignUpDate.Name = "m_dtpSignUpDate";
            this.m_dtpSignUpDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.m_dtpSignUpDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_dtpSignUpDate.Properties.NullText = "Undefined";
            this.m_dtpSignUpDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_dtpSignUpDate.Size = new System.Drawing.Size(119, 20);
            this.m_dtpSignUpDate.TabIndex = 5;
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(204, 26);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(62, 13);
            this.labelControl11.TabIndex = 4;
            this.labelControl11.Text = "Sig&n Up Date";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(5, 51);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(53, 13);
            this.labelControl10.TabIndex = 2;
            this.labelControl10.Text = "Appt. Da&te";
            // 
            // m_dtpLeadDate
            // 
            this.m_dtpLeadDate.EditValue = null;
            this.m_dtpLeadDate.Location = new System.Drawing.Point(64, 23);
            this.m_dtpLeadDate.Name = "m_dtpLeadDate";
            this.m_dtpLeadDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.m_dtpLeadDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_dtpLeadDate.Properties.NullText = "Undefined";
            this.m_dtpLeadDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_dtpLeadDate.Size = new System.Drawing.Size(119, 20);
            this.m_dtpLeadDate.TabIndex = 1;
            // 
            // m_txtEstimatedAmount
            // 
            this.m_txtEstimatedAmount.Location = new System.Drawing.Point(715, 23);
            this.m_txtEstimatedAmount.Name = "m_txtEstimatedAmount";
            this.m_txtEstimatedAmount.Properties.DisplayFormat.FormatString = "C";
            this.m_txtEstimatedAmount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_txtEstimatedAmount.Properties.Mask.EditMask = "$###,###,###,##0.00;($###,###,###,##0.00)";
            this.m_txtEstimatedAmount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.m_txtEstimatedAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_txtEstimatedAmount.Size = new System.Drawing.Size(91, 20);
            this.m_txtEstimatedAmount.TabIndex = 13;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(5, 26);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(49, 13);
            this.labelControl8.TabIndex = 0;
            this.labelControl8.Text = "Lead Da&te";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.m_cmbProjectType);
            this.groupControl2.Controls.Add(this.label3);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Controls.Add(this.m_chkSelfGeneratedLead);
            this.groupControl2.Controls.Add(this.m_cmbProjectManager);
            this.groupControl2.Controls.Add(this.m_lblNumber);
            this.groupControl2.Controls.Add(this.labelControl7);
            this.groupControl2.Controls.Add(this.m_lblProgress);
            this.groupControl2.Controls.Add(this.labelControl6);
            this.groupControl2.Controls.Add(this.labelControl4);
            this.groupControl2.Controls.Add(this.m_lblStatus);
            this.groupControl2.Location = new System.Drawing.Point(0, 108);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(275, 247);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Summary";
            // 
            // m_cmbProjectType
            // 
            this.m_cmbProjectType.Location = new System.Drawing.Point(96, 42);
            this.m_cmbProjectType.Name = "m_cmbProjectType";
            this.m_cmbProjectType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbProjectType.Size = new System.Drawing.Size(170, 20);
            this.m_cmbProjectType.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Type";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(8, 71);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(79, 13);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "P&roject Manager";
            // 
            // m_chkSelfGeneratedLead
            // 
            this.m_chkSelfGeneratedLead.Location = new System.Drawing.Point(94, 174);
            this.m_chkSelfGeneratedLead.Name = "m_chkSelfGeneratedLead";
            this.m_chkSelfGeneratedLead.Properties.Caption = "Self &Generated Lead";
            this.m_chkSelfGeneratedLead.Size = new System.Drawing.Size(133, 19);
            this.m_chkSelfGeneratedLead.TabIndex = 12;
            // 
            // m_cmbProjectManager
            // 
            this.m_cmbProjectManager.Location = new System.Drawing.Point(96, 68);
            this.m_cmbProjectManager.Name = "m_cmbProjectManager";
            this.m_cmbProjectManager.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbProjectManager.Size = new System.Drawing.Size(170, 20);
            this.m_cmbProjectManager.TabIndex = 9;
            // 
            // m_lblNumber
            // 
            this.m_lblNumber.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblNumber.Appearance.Options.UseFont = true;
            this.m_lblNumber.Location = new System.Drawing.Point(100, 23);
            this.m_lblNumber.Name = "m_lblNumber";
            this.m_lblNumber.Size = new System.Drawing.Size(62, 13);
            this.m_lblNumber.TabIndex = 1;
            this.m_lblNumber.Text = "[Unknown]";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(6, 23);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(37, 13);
            this.labelControl7.TabIndex = 0;
            this.labelControl7.Text = "Number";
            // 
            // m_lblProgress
            // 
            this.m_lblProgress.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblProgress.Appearance.Options.UseFont = true;
            this.m_lblProgress.Location = new System.Drawing.Point(101, 142);
            this.m_lblProgress.Name = "m_lblProgress";
            this.m_lblProgress.Size = new System.Drawing.Size(67, 13);
            this.m_lblProgress.TabIndex = 7;
            this.m_lblProgress.Text = "[Undefined]";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(6, 142);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(42, 13);
            this.labelControl6.TabIndex = 6;
            this.labelControl6.Text = "Progress";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(6, 123);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(31, 13);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "Status";
            // 
            // m_lblStatus
            // 
            this.m_lblStatus.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblStatus.Appearance.Options.UseFont = true;
            this.m_lblStatus.Location = new System.Drawing.Point(101, 123);
            this.m_lblStatus.Name = "m_lblStatus";
            this.m_lblStatus.Size = new System.Drawing.Size(29, 13);
            this.m_lblStatus.TabIndex = 5;
            this.m_lblStatus.Text = "Open";
            // 
            // m_tabpgTransaction
            // 
            this.m_tabpgTransaction.Controls.Add(this.groupControl5);
            this.m_tabpgTransaction.Name = "m_tabpgTransaction";
            this.m_tabpgTransaction.Size = new System.Drawing.Size(809, 570);
            this.m_tabpgTransaction.Text = "Transactions";
            // 
            // groupControl5
            // 
            this.groupControl5.Controls.Add(this.m_lblBControlBillPays);
            this.groupControl5.Controls.Add(this.m_gridBillPayTransactions);
            this.groupControl5.Controls.Add(this.m_cmbBillPayType);
            this.groupControl5.Controls.Add(this.m_lblLastBillingDate);
            this.groupControl5.Controls.Add(this.labelControl17);
            this.groupControl5.Controls.Add(this.m_lblLastPaymentDate);
            this.groupControl5.Controls.Add(this.labelControl18);
            this.groupControl5.Location = new System.Drawing.Point(0, 14);
            this.groupControl5.Name = "groupControl5";
            this.groupControl5.Size = new System.Drawing.Size(801, 410);
            this.groupControl5.TabIndex = 1;
            this.groupControl5.Text = "Invoices / Payments";
            // 
            // m_lblBControlBillPays
            // 
            this.m_lblBControlBillPays.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblBControlBillPays.Location = new System.Drawing.Point(278, 44);
            this.m_lblBControlBillPays.Name = "m_lblBControlBillPays";
            this.m_lblBControlBillPays.Size = new System.Drawing.Size(0, 0);
            this.m_lblBControlBillPays.TabIndex = 6;
            this.m_lblBControlBillPays.Text = "&BControl";
            // 
            // m_gridBillPayTransactions
            // 
            this.m_gridBillPayTransactions.EmbeddedNavigator.Name = "";
            this.m_gridBillPayTransactions.Location = new System.Drawing.Point(5, 68);
            this.m_gridBillPayTransactions.MainView = this.m_gridViewBillPayTransactions;
            this.m_gridBillPayTransactions.Name = "m_gridBillPayTransactions";
            this.m_gridBillPayTransactions.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoExEdit1,
            this.repositoryItemImageComboBox1});
            this.m_gridBillPayTransactions.Size = new System.Drawing.Size(796, 337);
            this.m_gridBillPayTransactions.TabIndex = 7;
            this.m_gridBillPayTransactions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridViewBillPayTransactions});
            // 
            // m_gridViewBillPayTransactions
            // 
            this.m_gridViewBillPayTransactions.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.m_colIssueDate,
            this.gridColumn4,
            this.gridColumn1,
            this.m_colBillPayAmount,
            this.m_colBalance,
            this.gridColumn5,
            this.gridColumn6});
            this.m_gridViewBillPayTransactions.GridControl = this.m_gridBillPayTransactions;
            this.m_gridViewBillPayTransactions.Name = "m_gridViewBillPayTransactions";
            this.m_gridViewBillPayTransactions.OptionsCustomization.AllowFilter = false;
            this.m_gridViewBillPayTransactions.OptionsCustomization.AllowGroup = false;
            this.m_gridViewBillPayTransactions.OptionsCustomization.AllowSort = false;
            this.m_gridViewBillPayTransactions.OptionsMenu.EnableColumnMenu = false;
            this.m_gridViewBillPayTransactions.OptionsNavigation.UseTabKey = false;
            this.m_gridViewBillPayTransactions.OptionsView.ShowGroupPanel = false;
            // 
            // m_colIssueDate
            // 
            this.m_colIssueDate.Caption = "Date";
            this.m_colIssueDate.DisplayFormat.FormatString = "d";
            this.m_colIssueDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.m_colIssueDate.FieldName = "CreatedDate";
            this.m_colIssueDate.MinWidth = 56;
            this.m_colIssueDate.Name = "m_colIssueDate";
            this.m_colIssueDate.OptionsColumn.AllowEdit = false;
            this.m_colIssueDate.OptionsFilter.AllowAutoFilter = false;
            this.m_colIssueDate.OptionsFilter.AllowFilter = false;
            this.m_colIssueDate.Visible = true;
            this.m_colIssueDate.VisibleIndex = 0;
            this.m_colIssueDate.Width = 87;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Number";
            this.gridColumn4.FieldName = "Number";
            this.gridColumn4.MinWidth = 50;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 264;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Type";
            this.gridColumn1.FieldName = "TypeText";
            this.gridColumn1.MinWidth = 50;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 140;
            // 
            // m_colBillPayAmount
            // 
            this.m_colBillPayAmount.Caption = "Amount";
            this.m_colBillPayAmount.DisplayFormat.FormatString = "c";
            this.m_colBillPayAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_colBillPayAmount.FieldName = "TotalAmount";
            this.m_colBillPayAmount.MinWidth = 71;
            this.m_colBillPayAmount.Name = "m_colBillPayAmount";
            this.m_colBillPayAmount.OptionsColumn.AllowEdit = false;
            this.m_colBillPayAmount.Visible = true;
            this.m_colBillPayAmount.VisibleIndex = 3;
            this.m_colBillPayAmount.Width = 108;
            // 
            // m_colBalance
            // 
            this.m_colBalance.Caption = "Balance";
            this.m_colBalance.DisplayFormat.FormatString = "c";
            this.m_colBalance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_colBalance.FieldName = "m_colBalance";
            this.m_colBalance.MinWidth = 71;
            this.m_colBalance.Name = "m_colBalance";
            this.m_colBalance.OptionsColumn.AllowEdit = false;
            this.m_colBalance.OptionsColumn.ReadOnly = true;
            this.m_colBalance.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.m_colBalance.Visible = true;
            this.m_colBalance.VisibleIndex = 4;
            this.m_colBalance.Width = 93;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Notes";
            this.gridColumn5.ColumnEdit = this.repositoryItemMemoExEdit1;
            this.gridColumn5.FieldName = "Notes";
            this.gridColumn5.MinWidth = 41;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 54;
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            // 
            // gridColumn6
            // 
            this.gridColumn6.ColumnEdit = this.repositoryItemImageComboBox1;
            this.gridColumn6.FieldName = "StatusImageIndex";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            this.gridColumn6.Width = 29;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 0, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 0)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            this.repositoryItemImageComboBox1.SmallImages = this.m_imagesVoid;
            // 
            // m_imagesVoid
            // 
            this.m_imagesVoid.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imagesVoid.ImageStream")));
            this.m_imagesVoid.TransparentColor = System.Drawing.Color.Black;
            this.m_imagesVoid.Images.SetKeyName(0, "error.png");
            this.m_imagesVoid.Images.SetKeyName(1, "warning.png");
            // 
            // m_cmbBillPayType
            // 
            this.m_cmbBillPayType.EditValue = 0;
            this.m_cmbBillPayType.Location = new System.Drawing.Point(5, 42);
            this.m_cmbBillPayType.Name = "m_cmbBillPayType";
            this.m_cmbBillPayType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbBillPayType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("All", 0, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Invoices", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Payments", 2, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Credit Memos", 3, -1)});
            this.m_cmbBillPayType.Size = new System.Drawing.Size(127, 20);
            this.m_cmbBillPayType.TabIndex = 4;
            // 
            // m_lblLastBillingDate
            // 
            this.m_lblLastBillingDate.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.m_lblLastBillingDate.Appearance.Options.UseFont = true;
            this.m_lblLastBillingDate.Location = new System.Drawing.Point(95, 23);
            this.m_lblLastBillingDate.Name = "m_lblLastBillingDate";
            this.m_lblLastBillingDate.Size = new System.Drawing.Size(68, 13);
            this.m_lblLastBillingDate.TabIndex = 1;
            this.m_lblLastBillingDate.Text = "01/00/0000";
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(636, 23);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(91, 13);
            this.labelControl17.TabIndex = 2;
            this.labelControl17.Text = "Last Payment Date";
            // 
            // m_lblLastPaymentDate
            // 
            this.m_lblLastPaymentDate.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.m_lblLastPaymentDate.Appearance.Options.UseFont = true;
            this.m_lblLastPaymentDate.Location = new System.Drawing.Point(733, 23);
            this.m_lblLastPaymentDate.Name = "m_lblLastPaymentDate";
            this.m_lblLastPaymentDate.Size = new System.Drawing.Size(68, 13);
            this.m_lblLastPaymentDate.TabIndex = 3;
            this.m_lblLastPaymentDate.Text = "03/00/0000";
            // 
            // labelControl18
            // 
            this.labelControl18.Location = new System.Drawing.Point(5, 23);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(84, 13);
            this.labelControl18.TabIndex = 0;
            this.labelControl18.Text = "Last Invoice Date";
            // 
            // m_ctlCustomerLookup
            // 
            this.m_ctlCustomerLookup.Address = null;
            this.m_ctlCustomerLookup.BaseLead = null;
            this.m_ctlCustomerLookup.Customer = null;
            this.m_ctlCustomerLookup.EditButtonText = "Ed&it";
            this.m_ctlCustomerLookup.EmailVisible = true;
            this.m_ctlCustomerLookup.IsReadOnly = false;
            this.m_ctlCustomerLookup.Location = new System.Drawing.Point(3, 3);
            this.m_ctlCustomerLookup.Name = "m_ctlCustomerLookup";
            this.m_ctlCustomerLookup.Size = new System.Drawing.Size(275, 106);
            this.m_ctlCustomerLookup.TabIndex = 0;
            this.m_ctlCustomerLookup.TabStop = false;
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // CreateProjectView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(822, 780);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateProjectView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CreateProjectView";
            ((System.ComponentModel.ISupportInitialize)(this.m_txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl7)).EndInit();
            this.groupControl7.ResumeLayout(false);
            this.groupControl7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_tabProjetDetail)).EndInit();
            this.m_tabProjetDetail.ResumeLayout(false);
            this.m_tabpgGeneral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpCompleteDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpCompleteDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtJobNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpScopeDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpScopeDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpDeclineDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpDeclineDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtJobCost.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpSignUpDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpSignUpDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpLeadDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dtpLeadDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtEstimatedAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbProjectType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkSelfGeneratedLead.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbProjectManager.Properties)).EndInit();
            this.m_tabpgTransaction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
            this.groupControl5.ResumeLayout(false);
            this.groupControl5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridBillPayTransactions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewBillPayTransactions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbBillPayType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal SimpleButton m_btnOk;
        internal SimpleButton m_btnCancel;
        internal MemoEdit m_txtNotes;
        private PanelControl panelControl1;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        internal AddressViewEdit m_ctlAddressLookup;
        internal CustomerViewEditLookup m_ctlCustomerLookup;
        internal ProjectEdit m_ctlProjectEdit;
        private GroupControl groupControl3;
        private LabelControl labelControl8;
        private GroupControl groupControl2;
        private LabelControl labelControl6;
        private LabelControl labelControl4;
        private GroupControl groupControl1;
        internal Dalworth.Server.MainForm.Components.DateEdit m_dtpLeadDate;
        private LabelControl labelControl7;
        internal Dalworth.Server.MainForm.Components.DateEdit m_dtpDeclineDate;
        private LabelControl labelControl12;
        internal Dalworth.Server.MainForm.Components.DateEdit m_dtpSignUpDate;
        private LabelControl labelControl11;
        private LabelControl labelControl10;
        private LabelControl labelControl20;
        private LabelControl labelControl19;
        private LabelControl labelControl17;
        private LabelControl labelControl18;
        private LabelControl labelControl13;
        internal TextEdit m_txtEstimatedAmount;
        internal LabelControl m_lblProgress;
        internal LabelControl m_lblStatus;
        internal LabelControl m_lblNumber;
        private LabelControl labelControl2;
        internal CheckEdit m_chkSelfGeneratedLead;
        internal ImageComboBoxEdit m_cmbProjectManager;
        private LabelControl labelControl3;
        internal TextEdit m_txtJobCost;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridViewBillPayTransactions;
        internal DevExpress.XtraGrid.GridControl m_gridBillPayTransactions;
        internal ImageComboBoxEdit m_cmbBillPayType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        private GroupControl groupControl5;
        internal LabelControl m_lblLastBillingDate;
        internal LabelControl m_lblCollectedAmount;
        internal LabelControl m_lblLastPaymentDate;
        internal LabelControl m_lblBilledAmount;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private System.Windows.Forms.ImageList m_imagesVoid;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colIssueDate;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colBillPayAmount;
        internal DevExpress.XtraGrid.Columns.GridColumn m_colBalance;
        private LabelControl labelControl25;
        private LabelControl labelControl24;
        internal LabelControl m_lblOutstandingAmount;
        internal LabelControl m_lblCreditAmount2;
        internal Dalworth.Server.MainForm.Components.DateEdit m_dtpScopeDate;
        internal DevExpress.XtraTab.XtraTabPage m_tabpgGeneral;
        private GroupControl groupControl7;
        internal DalworthTabControl m_tabProjetDetail;
        private System.Windows.Forms.Label label2;
        internal TextEdit m_txtJobNumber;
        internal SimpleButton m_btnPrint;
        private LabelControl m_lblBControlBillPays;
        internal DevExpress.XtraTab.XtraTabPage m_tabpgTransaction;
        internal Dalworth.Server.MainForm.Components.DateEdit m_dtpCompleteDate;
        private LabelControl labelControl22;
        internal ImageComboBoxEdit m_cmbProjectType;
        private System.Windows.Forms.Label label3;
    }
}