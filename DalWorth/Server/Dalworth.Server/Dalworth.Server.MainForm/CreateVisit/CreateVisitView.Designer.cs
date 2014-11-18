using DevExpress.XtraEditors;

namespace Dalworth.Server.MainForm.CreateVisit
{
    partial class CreateVisitView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateVisitView));
            Dalworth.Server.Domain.Visit visit2 = new Dalworth.Server.Domain.Visit();
            this.m_colTask = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.m_btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_tabs = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.m_lblOrderHistoryShortcut = new DevExpress.XtraEditors.LabelControl();
            this.m_gridHistoryOrders = new DevExpress.XtraGrid.GridControl();
            this.m_gridViewHistoryOrders = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.m_lnkCustomer = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.m_ctlAddressLookup = new Dalworth.Server.MainForm.Components.AddressViewEdit();
            this.m_ctlCustomerLookup = new Dalworth.Server.MainForm.Components.CustomerViewEditLookup();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.m_ctlVisitHeader = new Dalworth.Server.MainForm.Components.VisitHeaderEdit();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.m_lblGridTasksShortcut = new DevExpress.XtraEditors.LabelControl();
            this.m_ctlProjectEdit = new Dalworth.Server.MainForm.Components.ProjectEdit();
            this.m_chkShowFailed = new DevExpress.XtraEditors.CheckEdit();
            this.m_ctlTaskEdit = new Dalworth.Server.MainForm.Components.TaskEdit();
            this.m_treeTasks = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.m_colStatus = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.m_imagesYes = new System.Windows.Forms.ImageList(this.components);
            this.m_images = new System.Windows.Forms.ImageList(this.components);
            this.m_chkShowCompleted = new DevExpress.XtraEditors.CheckEdit();
            this.m_btnAddTask = new DevExpress.XtraEditors.SimpleButton();
            this.m_chkShowNotReady = new DevExpress.XtraEditors.CheckEdit();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.m_menuAdd = new DevExpress.XtraBars.PopupMenu(this.components);
            this.m_menuAddRugPickup = new DevExpress.XtraBars.BarButtonItem();
            this.m_menuAddDeflood = new DevExpress.XtraBars.BarButtonItem();
            this.m_menuAddMiscellaneous = new DevExpress.XtraBars.BarButtonItem();
            this.m_barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barListItem1 = new DevExpress.XtraBars.BarListItem();
            this.barLinkContainerItem1 = new DevExpress.XtraBars.BarLinkContainerItem();
            this.barListItem2 = new DevExpress.XtraBars.BarListItem();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.m_menuDeleteTask = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.m_menuActionAddMonitoring = new DevExpress.XtraBars.BarButtonItem();
            this.m_menuActionAddMiscellaneous = new DevExpress.XtraBars.BarButtonItem();
            this.m_menuActionAddHelp = new DevExpress.XtraBars.BarButtonItem();
            this.m_menuActionAddRugPickup = new DevExpress.XtraBars.BarButtonItem();
            this.m_menuActionIncludeInVisit = new DevExpress.XtraBars.BarButtonItem();
            this.m_menuActionExcludeFromVisit = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.m_menuCancelTask = new DevExpress.XtraBars.BarButtonItem();
            this.m_menuAction = new DevExpress.XtraBars.PopupMenu(this.components);
            this.m_errorProvide = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.m_gridCustomers = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.visitHeaderEdit1 = new Dalworth.Server.MainForm.Components.VisitHeaderEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_tabs)).BeginInit();
            this.m_tabs.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridHistoryOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewHistoryOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_lnkCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkShowFailed.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_treeTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkShowCompleted.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkShowNotReady.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_menuAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_menuAction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // m_colTask
            // 
            this.m_colTask.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_colTask.AppearanceCell.ForeColor = System.Drawing.Color.Blue;
            this.m_colTask.AppearanceCell.Options.UseFont = true;
            this.m_colTask.AppearanceCell.Options.UseForeColor = true;
            this.m_colTask.Caption = "Task";
            this.m_colTask.FieldName = "Name";
            this.m_colTask.MinWidth = 43;
            this.m_colTask.Name = "m_colTask";
            this.m_colTask.OptionsColumn.AllowEdit = false;
            this.m_colTask.Visible = true;
            this.m_colTask.VisibleIndex = 0;
            this.m_colTask.Width = 149;
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.Location = new System.Drawing.Point(390, 673);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 0;
            this.m_btnOk.Text = "OK";
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(474, 673);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 1;
            this.m_btnCancel.Text = "Cancel";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_tabs);
            this.panelControl1.Controls.Add(this.m_btnOk);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(552, 699);
            this.panelControl1.TabIndex = 9;
            // 
            // m_tabs
            // 
            this.m_tabs.Location = new System.Drawing.Point(3, 3);
            this.m_tabs.Name = "m_tabs";
            this.m_tabs.SelectedTabPage = this.xtraTabPage1;
            this.m_tabs.Size = new System.Drawing.Size(547, 668);
            this.m_tabs.TabIndex = 0;
            this.m_tabs.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            this.m_tabs.TabStop = false;
            this.m_tabs.Text = "xtraTabControl1";
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.m_lblOrderHistoryShortcut);
            this.xtraTabPage1.Controls.Add(this.m_gridHistoryOrders);
            this.xtraTabPage1.Controls.Add(this.m_ctlAddressLookup);
            this.xtraTabPage1.Controls.Add(this.m_ctlCustomerLookup);
            this.xtraTabPage1.Controls.Add(this.groupControl1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(538, 637);
            this.xtraTabPage1.Text = "&General";
            // 
            // m_lblOrderHistoryShortcut
            // 
            this.m_lblOrderHistoryShortcut.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblOrderHistoryShortcut.Location = new System.Drawing.Point(15, 305);
            this.m_lblOrderHistoryShortcut.Name = "m_lblOrderHistoryShortcut";
            this.m_lblOrderHistoryShortcut.Size = new System.Drawing.Size(0, 0);
            this.m_lblOrderHistoryShortcut.TabIndex = 2;
            this.m_lblOrderHistoryShortcut.Text = "&B Shortcut";
            // 
            // m_gridHistoryOrders
            // 
            this.m_gridHistoryOrders.EmbeddedNavigator.Name = "";
            this.m_gridHistoryOrders.Location = new System.Drawing.Point(3, 270);
            this.m_gridHistoryOrders.MainView = this.m_gridViewHistoryOrders;
            this.m_gridHistoryOrders.Name = "m_gridHistoryOrders";
            this.m_gridHistoryOrders.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.m_lnkCustomer,
            this.repositoryItemMemoEdit1});
            this.m_gridHistoryOrders.Size = new System.Drawing.Size(531, 366);
            this.m_gridHistoryOrders.TabIndex = 3;
            this.m_gridHistoryOrders.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.m_gridViewHistoryOrders});
            this.m_gridHistoryOrders.Visible = false;
            // 
            // m_gridViewHistoryOrders
            // 
            this.m_gridViewHistoryOrders.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.m_gridViewHistoryOrders.GridControl = this.m_gridHistoryOrders;
            this.m_gridViewHistoryOrders.Name = "m_gridViewHistoryOrders";
            this.m_gridViewHistoryOrders.OptionsBehavior.Editable = false;
            this.m_gridViewHistoryOrders.OptionsCustomization.AllowFilter = false;
            this.m_gridViewHistoryOrders.OptionsCustomization.AllowGroup = false;
            this.m_gridViewHistoryOrders.OptionsMenu.EnableColumnMenu = false;
            this.m_gridViewHistoryOrders.OptionsNavigation.UseTabKey = false;
            this.m_gridViewHistoryOrders.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Serviced";
            this.gridColumn1.FieldName = "date";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 67;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Service Type";
            this.gridColumn2.FieldName = "ServiceTypeText";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 148;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Completed";
            this.gridColumn3.FieldName = "CompletionTypeText";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 83;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Amount";
            this.gridColumn4.DisplayFormat.FormatString = "C";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "amount";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 61;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Technician";
            this.gridColumn5.FieldName = "TechnicianName";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 151;
            // 
            // m_lnkCustomer
            // 
            this.m_lnkCustomer.AutoHeight = false;
            this.m_lnkCustomer.Name = "m_lnkCustomer";
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // m_ctlAddressLookup
            // 
            this.m_ctlAddressLookup.BaseAddress = null;
            this.m_ctlAddressLookup.BaseAddressName = null;
            this.m_ctlAddressLookup.Caption = null;
            this.m_ctlAddressLookup.CurrentAddress = null;
            this.m_ctlAddressLookup.EditButtonText = "Edi&t";
            this.m_ctlAddressLookup.IsBaseAddressActive = false;
            this.m_ctlAddressLookup.Location = new System.Drawing.Point(3, 143);
            this.m_ctlAddressLookup.Name = "m_ctlAddressLookup";
            this.m_ctlAddressLookup.Size = new System.Drawing.Size(275, 123);
            this.m_ctlAddressLookup.TabIndex = 5;
            this.m_ctlAddressLookup.TabStop = false;
            // 
            // m_ctlCustomerLookup
            // 
            this.m_ctlCustomerLookup.Address = null;
            this.m_ctlCustomerLookup.BaseLead = null;
            this.m_ctlCustomerLookup.Customer = null;
            this.m_ctlCustomerLookup.EditButtonText = "Ed&it";
            this.m_ctlCustomerLookup.EmailVisible = false;
            this.m_ctlCustomerLookup.IsReadOnly = false;
            this.m_ctlCustomerLookup.Location = new System.Drawing.Point(3, 3);
            this.m_ctlCustomerLookup.Name = "m_ctlCustomerLookup";
            this.m_ctlCustomerLookup.Size = new System.Drawing.Size(275, 134);
            this.m_ctlCustomerLookup.TabIndex = 1;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.m_ctlVisitHeader);
            this.groupControl1.Location = new System.Drawing.Point(284, 3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(250, 263);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Visit";
            // 
            // m_ctlVisitHeader
            // 
            this.m_ctlVisitHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_ctlVisitHeader.IsReadOnly = false;
            this.m_ctlVisitHeader.Location = new System.Drawing.Point(2, 20);
            this.m_ctlVisitHeader.Name = "m_ctlVisitHeader";
            this.m_ctlVisitHeader.Size = new System.Drawing.Size(246, 241);
            this.m_ctlVisitHeader.TabIndex = 2;
            visit1.ClosedDollarAmount = new decimal(new int[] {
            0,
            0,
            0,
            0});
            visit1.ConfirmBusy = false;
            visit1.ConfirmDateTime = null;
            visit1.ConfirmedFrameBegin = null;
            visit1.ConfirmedFrameEnd = null;
            visit1.ConfirmLeftMessage = false;
            visit1.CreateDate = new System.DateTime(((long)(0)));
            visit1.CustomerId = null;
            visit1.DurationMin = null;
            visit1.ID = 0;
            visit1.IsCallOnYourWay = false;
            visit1.IsWillCall = false;
            visit1.Notes = "";
            visit1.PreferedTimeFrom = null;
            visit1.PreferedTimeTo = null;
            visit1.ServiceAddressId = null;
            visit1.ServiceDate = new System.DateTime(2011, 6, 15, 13, 28, 20, 281);
            visit1.SyncToolPrintDate = null;
            visit1.VisitStatus = Dalworth.Server.Domain.VisitStatusEnum.Pending;
            visit1.VisitStatusId = 1;
            this.m_ctlVisitHeader.Visit = visit1;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.m_lblGridTasksShortcut);
            this.xtraTabPage2.Controls.Add(this.m_ctlProjectEdit);
            this.xtraTabPage2.Controls.Add(this.m_chkShowFailed);
            this.xtraTabPage2.Controls.Add(this.m_ctlTaskEdit);
            this.xtraTabPage2.Controls.Add(this.m_treeTasks);
            this.xtraTabPage2.Controls.Add(this.m_chkShowCompleted);
            this.xtraTabPage2.Controls.Add(this.m_btnAddTask);
            this.xtraTabPage2.Controls.Add(this.m_chkShowNotReady);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(538, 637);
            this.xtraTabPage2.Text = "&Tasks";
            // 
            // m_lblGridTasksShortcut
            // 
            this.m_lblGridTasksShortcut.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblGridTasksShortcut.Location = new System.Drawing.Point(6, 30);
            this.m_lblGridTasksShortcut.Name = "m_lblGridTasksShortcut";
            this.m_lblGridTasksShortcut.Size = new System.Drawing.Size(0, 0);
            this.m_lblGridTasksShortcut.TabIndex = 0;
            this.m_lblGridTasksShortcut.Text = "&B Shortcut";
            // 
            // m_ctlProjectEdit
            // 
            this.m_ctlProjectEdit.AreaId = null;
            this.m_ctlProjectEdit.IsEditable = false;
            this.m_ctlProjectEdit.IsInsuranceVisible = false;
            this.m_ctlProjectEdit.IsQbSalesRepRequired = false;
            this.m_ctlProjectEdit.IsQbSalesRepVisible = true;
            this.m_ctlProjectEdit.Location = new System.Drawing.Point(0, 227);
            this.m_ctlProjectEdit.Name = "m_ctlProjectEdit";
            this.m_ctlProjectEdit.Project = null;
            this.m_ctlProjectEdit.ProjectInsurance = null;
            this.m_ctlProjectEdit.Size = new System.Drawing.Size(537, 410);
            this.m_ctlProjectEdit.TabIndex = 1;
            this.m_ctlProjectEdit.Visible = false;
            // 
            // m_chkShowFailed
            // 
            this.m_chkShowFailed.Location = new System.Drawing.Point(427, 101);
            this.m_chkShowFailed.Name = "m_chkShowFailed";
            this.m_chkShowFailed.Properties.Caption = "Show &Failed";
            this.m_chkShowFailed.Size = new System.Drawing.Size(90, 19);
            this.m_chkShowFailed.TabIndex = 3;
            // 
            // m_ctlTaskEdit
            // 
            this.m_ctlTaskEdit.IsClosedAmountUnknownVisible = false;
            this.m_ctlTaskEdit.IsEditable = false;
            this.m_ctlTaskEdit.Items = null;
            this.m_ctlTaskEdit.Location = new System.Drawing.Point(0, 227);
            this.m_ctlTaskEdit.Name = "m_ctlTaskEdit";
            this.m_ctlTaskEdit.OriginalMessage = null;
            this.m_ctlTaskEdit.Size = new System.Drawing.Size(538, 416);
            this.m_ctlTaskEdit.TabIndex = 2;
            this.m_ctlTaskEdit.Task = null;
            this.m_ctlTaskEdit.Visible = false;
            // 
            // m_treeTasks
            // 
            this.m_treeTasks.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.m_colTask,
            this.treeListColumn3,
            this.m_colStatus,
            this.treeListColumn2,
            this.treeListColumn4});
            this.m_treeTasks.Location = new System.Drawing.Point(0, 0);
            this.m_treeTasks.Name = "m_treeTasks";
            this.m_treeTasks.OptionsBehavior.AllowExpandOnDblClick = false;
            this.m_treeTasks.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.m_treeTasks.ParentFieldName = "ParentId";
            this.m_treeTasks.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1});
            this.m_treeTasks.SelectImageList = this.m_images;
            this.m_treeTasks.Size = new System.Drawing.Size(421, 221);
            this.m_treeTasks.TabIndex = 0;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "Number";
            this.treeListColumn3.FieldName = "Number";
            this.treeListColumn3.MinWidth = 25;
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.OptionsColumn.AllowEdit = false;
            this.treeListColumn3.OptionsColumn.FixedWidth = true;
            this.treeListColumn3.Visible = true;
            this.treeListColumn3.VisibleIndex = 1;
            this.treeListColumn3.Width = 58;
            // 
            // m_colStatus
            // 
            this.m_colStatus.Caption = "Status";
            this.m_colStatus.FieldName = "StatusImageIndex";
            this.m_colStatus.Name = "m_colStatus";
            this.m_colStatus.OptionsColumn.AllowEdit = false;
            this.m_colStatus.OptionsColumn.FixedWidth = true;
            this.m_colStatus.Visible = true;
            this.m_colStatus.VisibleIndex = 2;
            this.m_colStatus.Width = 43;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Fail Cnt";
            this.treeListColumn2.FieldName = "FailCount";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.OptionsColumn.AllowEdit = false;
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 3;
            this.treeListColumn2.Width = 53;
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "Last Fail";
            this.treeListColumn4.FieldName = "LastFailDate";
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.OptionsColumn.AllowEdit = false;
            this.treeListColumn4.Visible = true;
            this.treeListColumn4.VisibleIndex = 4;
            this.treeListColumn4.Width = 97;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.Utils.HorzAlignment.Center, null)});
            this.repositoryItemImageComboBox1.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 0, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 1)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            this.repositoryItemImageComboBox1.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
            this.repositoryItemImageComboBox1.SmallImages = this.m_imagesYes;
            // 
            // m_imagesYes
            // 
            this.m_imagesYes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imagesYes.ImageStream")));
            this.m_imagesYes.TransparentColor = System.Drawing.Color.Magenta;
            this.m_imagesYes.Images.SetKeyName(0, "yesblack.bmp");
            // 
            // m_images
            // 
            this.m_images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_images.ImageStream")));
            this.m_images.TransparentColor = System.Drawing.Color.Magenta;
            this.m_images.Images.SetKeyName(0, "fixed.bmp");
            this.m_images.Images.SetKeyName(1, "folder.bmp");
            this.m_images.Images.SetKeyName(2, "postponed.bmp");
            this.m_images.Images.SetKeyName(3, "fixedincluded.bmp");
            // 
            // m_chkShowCompleted
            // 
            this.m_chkShowCompleted.Location = new System.Drawing.Point(427, 151);
            this.m_chkShowCompleted.Name = "m_chkShowCompleted";
            this.m_chkShowCompleted.Properties.Caption = "Sho&w Completed";
            this.m_chkShowCompleted.Size = new System.Drawing.Size(108, 19);
            this.m_chkShowCompleted.TabIndex = 5;
            // 
            // m_btnAddTask
            // 
            this.m_btnAddTask.Location = new System.Drawing.Point(429, 198);
            this.m_btnAddTask.Name = "m_btnAddTask";
            this.m_btnAddTask.Size = new System.Drawing.Size(75, 23);
            this.m_btnAddTask.TabIndex = 6;
            this.m_btnAddTask.Text = "&Add";
            // 
            // m_chkShowNotReady
            // 
            this.m_chkShowNotReady.Location = new System.Drawing.Point(427, 126);
            this.m_chkShowNotReady.Name = "m_chkShowNotReady";
            this.m_chkShowNotReady.Properties.Caption = "Show &not Ready";
            this.m_chkShowNotReady.Size = new System.Drawing.Size(108, 19);
            this.m_chkShowNotReady.TabIndex = 4;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Task";
            this.treeListColumn1.FieldName = "TaskTypeText";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.OptionsColumn.AllowEdit = false;
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // m_menuAdd
            // 
            this.m_menuAdd.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.m_menuAddRugPickup),
            new DevExpress.XtraBars.LinkPersistInfo(this.m_menuAddDeflood),
            new DevExpress.XtraBars.LinkPersistInfo(this.m_menuAddMiscellaneous)});
            this.m_menuAdd.Manager = this.m_barManager;
            this.m_menuAdd.Name = "m_menuAdd";
            // 
            // m_menuAddRugPickup
            // 
            this.m_menuAddRugPickup.Caption = "&Rug Pickup";
            this.m_menuAddRugPickup.Id = 7;
            this.m_menuAddRugPickup.Name = "m_menuAddRugPickup";
            // 
            // m_menuAddDeflood
            // 
            this.m_menuAddDeflood.Caption = "&Deflood";
            this.m_menuAddDeflood.Id = 8;
            this.m_menuAddDeflood.Name = "m_menuAddDeflood";
            // 
            // m_menuAddMiscellaneous
            // 
            this.m_menuAddMiscellaneous.Caption = "&Miscellaneous";
            this.m_menuAddMiscellaneous.Id = 9;
            this.m_menuAddMiscellaneous.Name = "m_menuAddMiscellaneous";
            // 
            // m_barManager
            // 
            this.m_barManager.DockControls.Add(this.barDockControlTop);
            this.m_barManager.DockControls.Add(this.barDockControlBottom);
            this.m_barManager.DockControls.Add(this.barDockControlLeft);
            this.m_barManager.DockControls.Add(this.barDockControlRight);
            this.m_barManager.Form = this;
            this.m_barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barListItem1,
            this.barLinkContainerItem1,
            this.barListItem2,
            this.barEditItem1,
            this.m_menuAddRugPickup,
            this.m_menuAddDeflood,
            this.m_menuAddMiscellaneous,
            this.m_menuDeleteTask,
            this.barButtonItem1,
            this.m_menuActionAddMonitoring,
            this.m_menuActionAddMiscellaneous,
            this.m_menuActionAddHelp,
            this.m_menuActionAddRugPickup,
            this.m_menuActionIncludeInVisit,
            this.m_menuActionExcludeFromVisit,
            this.barButtonItem2,
            this.m_menuCancelTask});
            this.m_barManager.MaxItemId = 20;
            this.m_barManager.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            // 
            // barListItem1
            // 
            this.barListItem1.Caption = "barListItem1";
            this.barListItem1.Id = 0;
            this.barListItem1.Name = "barListItem1";
            // 
            // barLinkContainerItem1
            // 
            this.barLinkContainerItem1.Caption = "Bla";
            this.barLinkContainerItem1.Id = 4;
            this.barLinkContainerItem1.Name = "barLinkContainerItem1";
            // 
            // barListItem2
            // 
            this.barListItem2.Caption = "sdfsd";
            this.barListItem2.Id = 5;
            this.barListItem2.Name = "barListItem2";
            // 
            // barEditItem1
            // 
            this.barEditItem1.Caption = "sdfsd";
            this.barEditItem1.Edit = this.repositoryItemTextEdit1;
            this.barEditItem1.Id = 6;
            this.barEditItem1.Name = "barEditItem1";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // m_menuDeleteTask
            // 
            this.m_menuDeleteTask.Caption = "&Delete";
            this.m_menuDeleteTask.Id = 10;
            this.m_menuDeleteTask.Name = "m_menuDeleteTask";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 11;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // m_menuActionAddMonitoring
            // 
            this.m_menuActionAddMonitoring.Caption = "Add &Monitoring";
            this.m_menuActionAddMonitoring.Id = 12;
            this.m_menuActionAddMonitoring.Name = "m_menuActionAddMonitoring";
            // 
            // m_menuActionAddMiscellaneous
            // 
            this.m_menuActionAddMiscellaneous.Caption = "Add Mi&scellaneous";
            this.m_menuActionAddMiscellaneous.Id = 13;
            this.m_menuActionAddMiscellaneous.Name = "m_menuActionAddMiscellaneous";
            // 
            // m_menuActionAddHelp
            // 
            this.m_menuActionAddHelp.Caption = "Add &Help";
            this.m_menuActionAddHelp.Id = 14;
            this.m_menuActionAddHelp.Name = "m_menuActionAddHelp";
            // 
            // m_menuActionAddRugPickup
            // 
            this.m_menuActionAddRugPickup.Caption = "Add &Rug Pickup";
            this.m_menuActionAddRugPickup.Id = 15;
            this.m_menuActionAddRugPickup.Name = "m_menuActionAddRugPickup";
            // 
            // m_menuActionIncludeInVisit
            // 
            this.m_menuActionIncludeInVisit.Caption = "&Include in Visit";
            this.m_menuActionIncludeInVisit.Id = 16;
            this.m_menuActionIncludeInVisit.Name = "m_menuActionIncludeInVisit";
            // 
            // m_menuActionExcludeFromVisit
            // 
            this.m_menuActionExcludeFromVisit.Caption = "&Exclude from Visit";
            this.m_menuActionExcludeFromVisit.Id = 17;
            this.m_menuActionExcludeFromVisit.Name = "m_menuActionExcludeFromVisit";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "barButtonItem2";
            this.barButtonItem2.Id = 18;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // m_menuCancelTask
            // 
            this.m_menuCancelTask.Caption = "&Cancel";
            this.m_menuCancelTask.Id = 19;
            this.m_menuCancelTask.Name = "m_menuCancelTask";
            // 
            // m_menuAction
            // 
            this.m_menuAction.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.m_menuActionAddRugPickup),
            new DevExpress.XtraBars.LinkPersistInfo(this.m_menuActionAddMonitoring),
            new DevExpress.XtraBars.LinkPersistInfo(this.m_menuActionAddMiscellaneous),
            new DevExpress.XtraBars.LinkPersistInfo(this.m_menuActionAddHelp),
            new DevExpress.XtraBars.LinkPersistInfo(this.m_menuActionIncludeInVisit, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.m_menuActionExcludeFromVisit),
            new DevExpress.XtraBars.LinkPersistInfo(this.m_menuCancelTask, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.m_menuDeleteTask)});
            this.m_menuAction.Manager = this.m_barManager;
            this.m_menuAction.Name = "m_menuAction";
            // 
            // m_errorProvide
            // 
            this.m_errorProvide.ContainerControl = this;
            // 
            // m_gridCustomers
            // 
            this.m_gridCustomers.EmbeddedNavigator.Name = "";
            this.m_gridCustomers.Location = new System.Drawing.Point(3, 231);
            this.m_gridCustomers.MainView = this.gridView2;
            this.m_gridCustomers.Name = "m_gridCustomers";
            this.m_gridCustomers.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.m_lnkCustomer,
            this.repositoryItemMemoEdit1});
            this.m_gridCustomers.Size = new System.Drawing.Size(531, 403);
            this.m_gridCustomers.TabIndex = 17;
            this.m_gridCustomers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.m_gridCustomers;
            this.gridView2.Name = "gridView2";
            // 
            // visitHeaderEdit1
            // 
            this.visitHeaderEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.visitHeaderEdit1.IsReadOnly = false;
            this.visitHeaderEdit1.Location = new System.Drawing.Point(2, 20);
            this.visitHeaderEdit1.Name = "visitHeaderEdit1";
            this.visitHeaderEdit1.Size = new System.Drawing.Size(246, 200);
            this.visitHeaderEdit1.TabIndex = 18;
            visit2.ClosedDollarAmount = new decimal(new int[] {
            0,
            0,
            0,
            0});
            visit2.ConfirmBusy = false;
            visit2.ConfirmDateTime = null;
            visit2.ConfirmedFrameBegin = null;
            visit2.ConfirmedFrameEnd = null;
            visit2.ConfirmLeftMessage = false;
            visit2.CreateDate = new System.DateTime(((long)(0)));
            visit2.CustomerId = null;
            visit2.DurationMin = null;
            visit2.ID = 0;
            visit2.IsCallOnYourWay = false;
            visit2.IsWillCall = false;
            visit2.Notes = "";
            visit2.PreferedTimeFrom = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            visit2.PreferedTimeTo = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            visit2.ServiceAddressId = null;
            visit2.ServiceDate = null;
            visit2.SyncToolPrintDate = null;
            visit2.VisitStatus = Dalworth.Server.Domain.VisitStatusEnum.Pending;
            visit2.VisitStatusId = 1;
            this.visitHeaderEdit1.Visit = visit2;
            // 
            // CreateVisitView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 699);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateVisitView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CreateVisitView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_tabs)).EndInit();
            this.m_tabs.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_gridHistoryOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridViewHistoryOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_lnkCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_chkShowFailed.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_treeTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkShowCompleted.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkShowNotReady.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_menuAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_menuAction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_gridCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal SimpleButton m_btnOk;
        internal SimpleButton m_btnCancel;
        private PanelControl panelControl1;
        internal DevExpress.XtraTreeList.TreeList m_treeTasks;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        internal DevExpress.XtraBars.PopupMenu m_menuAdd;
        internal SimpleButton m_btnAddTask;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarListItem barListItem1;
        internal DevExpress.XtraBars.BarManager m_barManager;
        private DevExpress.XtraBars.BarLinkContainerItem barLinkContainerItem1;
        private DevExpress.XtraBars.BarListItem barListItem2;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        internal DevExpress.XtraBars.BarButtonItem m_menuAddRugPickup;
        internal DevExpress.XtraBars.BarButtonItem m_menuAddDeflood;
        internal DevExpress.XtraBars.BarButtonItem m_menuAddMiscellaneous;
        internal DevExpress.XtraBars.BarButtonItem m_menuDeleteTask;
        internal DevExpress.XtraBars.PopupMenu m_menuAction;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        internal DevExpress.XtraBars.BarButtonItem m_menuActionAddMonitoring;
        internal DevExpress.XtraBars.BarButtonItem m_menuActionAddMiscellaneous;
        internal DevExpress.XtraBars.BarButtonItem m_menuActionAddHelp;
        internal DevExpress.XtraBars.BarButtonItem m_menuActionAddRugPickup;
        private System.Windows.Forms.ImageList m_images;
        internal CheckEdit m_chkShowCompleted;
        internal CheckEdit m_chkShowNotReady;
        internal CheckEdit m_chkShowFailed;
        internal DevExpress.XtraTreeList.Columns.TreeListColumn m_colStatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private System.Windows.Forms.ImageList m_imagesYes;
        internal DevExpress.XtraBars.BarButtonItem m_menuActionIncludeInVisit;
        internal DevExpress.XtraBars.BarButtonItem m_menuActionExcludeFromVisit;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        internal Dalworth.Server.MainForm.Components.TaskEdit m_ctlTaskEdit;
        private GroupControl groupControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        internal DevExpress.XtraTab.XtraTabControl m_tabs;
        internal Dalworth.Server.MainForm.Components.CustomerViewEditLookup m_ctlCustomerLookup;
        internal Dalworth.Server.MainForm.Components.AddressViewEdit m_ctlAddressLookup;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvide;
        internal Dalworth.Server.MainForm.Components.ProjectEdit m_ctlProjectEdit;
        internal DevExpress.XtraGrid.GridControl m_gridHistoryOrders;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        internal DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit m_lnkCustomer;
        internal DevExpress.XtraGrid.GridControl m_gridCustomers;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        internal DevExpress.XtraGrid.Views.Grid.GridView m_gridViewHistoryOrders;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private Dalworth.Server.MainForm.Components.VisitHeaderEdit visitHeaderEdit1;
        internal Dalworth.Server.MainForm.Components.VisitHeaderEdit m_ctlVisitHeader;
        internal DevExpress.XtraTreeList.Columns.TreeListColumn m_colTask;
        private LabelControl m_lblOrderHistoryShortcut;
        private LabelControl m_lblGridTasksShortcut;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        internal DevExpress.XtraBars.BarButtonItem m_menuCancelTask;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
    }
}