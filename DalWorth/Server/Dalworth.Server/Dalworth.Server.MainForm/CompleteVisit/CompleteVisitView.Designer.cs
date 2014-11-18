using TimeEditEx=Dalworth.Server.MainForm.Components.TimeEditEx;

namespace Dalworth.Server.MainForm.CompleteVisit
{
    partial class CompleteVisitView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompleteVisitView));
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_lblVisitNumber = new System.Windows.Forms.Label();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.m_timeComplete = new Dalworth.Server.MainForm.Components.TimeEditEx();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.m_tabs = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.m_txtDropOff = new Dalworth.Server.MainForm.Components.EquipmentQuantityTextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblEndDayTime = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtPickup = new Dalworth.Server.MainForm.Components.EquipmentQuantityTextEdit();
            this.m_lblEquipmentVanTotals = new System.Windows.Forms.Label();
            this.m_lblEquipmentCustomerTotals = new System.Windows.Forms.Label();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.m_lblVisitNotesShortcut = new DevExpress.XtraEditors.LabelControl();
            this.m_txtVisitNotes = new DevExpress.XtraEditors.MemoEdit();
            this.m_ctlAddressLookup = new Dalworth.Server.MainForm.Components.AddressViewEdit();
            this.m_ctlCustomerLookup = new Dalworth.Server.MainForm.Components.CustomerViewEditLookup();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.m_lblShortcutTasks = new DevExpress.XtraEditors.LabelControl();
            this.m_ctlProjectEdit = new Dalworth.Server.MainForm.Components.ProjectEdit();
            this.m_ctlTaskEdit = new Dalworth.Server.MainForm.Components.TaskEditComplete();
            this.m_btnAddTask = new DevExpress.XtraEditors.SimpleButton();
            this.m_treeTasks = new DevExpress.XtraTreeList.TreeList();
            this.m_colTask = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.m_colTaskAction = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.m_cmbTaskActionDefloodFirstTime = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.m_cmbTaskActionDeflood = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.m_cmbTaskAction = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.m_cmbTaskActionNotApplicable = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.m_cmbTaskActionDefloodAdded = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.m_cmbTaskActionAdded = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.m_imagesTree = new System.Windows.Forms.ImageList(this.components);
            this.m_btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.m_btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.m_images = new System.Windows.Forms.ImageList(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barListItem1 = new DevExpress.XtraBars.BarListItem();
            this.barLinkContainerItem1 = new DevExpress.XtraBars.BarLinkContainerItem();
            this.barListItem2 = new DevExpress.XtraBars.BarListItem();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.m_menuAdd = new DevExpress.XtraBars.PopupMenu(this.components);
            this.m_menuAddRugPickup = new DevExpress.XtraBars.BarButtonItem();
            this.m_menuAddDeflood = new DevExpress.XtraBars.BarButtonItem();
            this.m_menuAddMiscellaneous = new DevExpress.XtraBars.BarButtonItem();
            this.m_barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.m_menuDeleteTask = new DevExpress.XtraBars.BarButtonItem();
            this.m_menuActionAddMiscellaneous = new DevExpress.XtraBars.BarButtonItem();
            this.m_menuActionAddRugPickup = new DevExpress.XtraBars.BarButtonItem();
            this.m_menuCompleteWithPayment = new DevExpress.XtraBars.BarButtonItem();
            this.m_menuCompleteWithoutPayment = new DevExpress.XtraBars.BarButtonItem();
            this.repositoryItemTextEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.m_menuAction = new DevExpress.XtraBars.PopupMenu(this.components);
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.m_menuVisitComplete = new DevExpress.XtraBars.PopupMenu(this.components);
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_timeComplete.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_tabs)).BeginInit();
            this.m_tabs.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtDropOff.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtPickup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtVisitNotes.Properties)).BeginInit();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_treeTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTaskActionDefloodFirstTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTaskActionDeflood)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTaskAction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTaskActionNotApplicable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTaskActionDefloodAdded)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTaskActionAdded)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_menuAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_menuAction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_menuVisitComplete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // gridBand2
            // 
            this.gridBand2.Caption = "gridBand1";
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.Width = 225;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_lblVisitNumber);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.m_timeComplete);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.m_tabs);
            this.panelControl1.Controls.Add(this.m_btnCancel);
            this.panelControl1.Controls.Add(this.m_btnOk);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(621, 731);
            this.panelControl1.TabIndex = 9;
            // 
            // m_lblVisitNumber
            // 
            this.m_lblVisitNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblVisitNumber.Location = new System.Drawing.Point(567, 8);
            this.m_lblVisitNumber.Name = "m_lblVisitNumber";
            this.m_lblVisitNumber.Size = new System.Drawing.Size(51, 13);
            this.m_lblVisitNumber.TabIndex = 15;
            this.m_lblVisitNumber.Text = "123456";
            this.m_lblVisitNumber.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(537, 8);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(30, 13);
            this.labelControl3.TabIndex = 15;
            this.labelControl3.Text = "Visit #";
            // 
            // m_timeComplete
            // 
            this.m_timeComplete.EditValue = new System.DateTime(2008, 2, 19, 23, 59, 0, 0);
            this.m_timeComplete.Location = new System.Drawing.Point(462, 676);
            this.m_timeComplete.Name = "m_timeComplete";
            this.m_timeComplete.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, true, DevExpress.Utils.HorzAlignment.Center, ((System.Drawing.Image)(resources.GetObject("m_timeComplete.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), "Visit End time"),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.Utils.HorzAlignment.Center, ((System.Drawing.Image)(resources.GetObject("m_timeComplete.Properties.Buttons1"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), "Current Time")});
            this.m_timeComplete.Properties.Mask.EditMask = "t";
            this.m_timeComplete.Size = new System.Drawing.Size(154, 20);
            this.m_timeComplete.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(382, 679);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "&Complete Time";
            // 
            // m_tabs
            // 
            this.m_tabs.Location = new System.Drawing.Point(3, 3);
            this.m_tabs.Name = "m_tabs";
            this.m_tabs.SelectedTabPage = this.xtraTabPage2;
            this.m_tabs.Size = new System.Drawing.Size(616, 668);
            this.m_tabs.TabIndex = 0;
            this.m_tabs.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage2,
            this.xtraTabPage1});
            this.m_tabs.Text = "Tabs";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.groupControl1);
            this.xtraTabPage2.Controls.Add(this.groupControl2);
            this.xtraTabPage2.Controls.Add(this.m_ctlAddressLookup);
            this.xtraTabPage2.Controls.Add(this.m_ctlCustomerLookup);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(607, 637);
            this.xtraTabPage2.Text = "&General";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.m_txtDropOff);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.m_lblEndDayTime);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.m_txtPickup);
            this.groupControl1.Controls.Add(this.m_lblEquipmentVanTotals);
            this.groupControl1.Controls.Add(this.m_lblEquipmentCustomerTotals);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Location = new System.Drawing.Point(313, 149);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(290, 173);
            this.groupControl1.TabIndex = 22;
            this.groupControl1.Text = "Equipment (Fan/Deh/Air)";
            // 
            // m_txtDropOff
            // 
            this.m_txtDropOff.EditValue = "0/0/0";
            this.m_txtDropOff.Location = new System.Drawing.Point(185, 131);
            this.m_txtDropOff.Name = "m_txtDropOff";
            this.m_txtDropOff.Properties.Mask.EditMask = "\\d{1,2}/\\d{1,2}/\\d{1,2}";
            this.m_txtDropOff.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.m_txtDropOff.Properties.NullText = "0/0/0";
            this.m_txtDropOff.Quantities = ((System.Collections.Generic.Dictionary<int, int>)(resources.GetObject("m_txtDropOff.Quantities")));
            this.m_txtDropOff.Size = new System.Drawing.Size(100, 20);
            this.m_txtDropOff.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.AllowDrop = true;
            this.labelControl1.Location = new System.Drawing.Point(5, 53);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(46, 13);
            this.labelControl1.TabIndex = 23;
            this.labelControl1.Text = "Customer";
            // 
            // m_lblEndDayTime
            // 
            this.m_lblEndDayTime.AllowDrop = true;
            this.m_lblEndDayTime.Location = new System.Drawing.Point(5, 33);
            this.m_lblEndDayTime.Name = "m_lblEndDayTime";
            this.m_lblEndDayTime.Size = new System.Drawing.Size(18, 13);
            this.m_lblEndDayTime.TabIndex = 22;
            this.m_lblEndDayTime.Text = "Van";
            // 
            // labelControl2
            // 
            this.labelControl2.AllowDrop = true;
            this.labelControl2.Location = new System.Drawing.Point(5, 134);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(42, 13);
            this.labelControl2.TabIndex = 25;
            this.labelControl2.Text = "Drop Off";
            // 
            // m_txtPickup
            // 
            this.m_txtPickup.EditValue = "0/0/0";
            this.m_txtPickup.Location = new System.Drawing.Point(185, 105);
            this.m_txtPickup.Name = "m_txtPickup";
            this.m_txtPickup.Properties.Mask.EditMask = "\\d{1,2}/\\d{1,2}/\\d{1,2}";
            this.m_txtPickup.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.m_txtPickup.Properties.NullText = "0/0/0";
            this.m_txtPickup.Quantities = ((System.Collections.Generic.Dictionary<int, int>)(resources.GetObject("m_txtPickup.Quantities")));
            this.m_txtPickup.Size = new System.Drawing.Size(100, 20);
            this.m_txtPickup.TabIndex = 0;
            // 
            // m_lblEquipmentVanTotals
            // 
            this.m_lblEquipmentVanTotals.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblEquipmentVanTotals.Location = new System.Drawing.Point(188, 35);
            this.m_lblEquipmentVanTotals.Name = "m_lblEquipmentVanTotals";
            this.m_lblEquipmentVanTotals.Size = new System.Drawing.Size(97, 13);
            this.m_lblEquipmentVanTotals.TabIndex = 19;
            this.m_lblEquipmentVanTotals.Text = "12/13/14";
            this.m_lblEquipmentVanTotals.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // m_lblEquipmentCustomerTotals
            // 
            this.m_lblEquipmentCustomerTotals.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblEquipmentCustomerTotals.Location = new System.Drawing.Point(188, 53);
            this.m_lblEquipmentCustomerTotals.Name = "m_lblEquipmentCustomerTotals";
            this.m_lblEquipmentCustomerTotals.Size = new System.Drawing.Size(97, 13);
            this.m_lblEquipmentCustomerTotals.TabIndex = 21;
            this.m_lblEquipmentCustomerTotals.Text = "12/13/14";
            this.m_lblEquipmentCustomerTotals.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelControl4
            // 
            this.labelControl4.AllowDrop = true;
            this.labelControl4.Location = new System.Drawing.Point(5, 108);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(30, 13);
            this.labelControl4.TabIndex = 24;
            this.labelControl4.Text = "Pickup";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.m_lblVisitNotesShortcut);
            this.groupControl2.Controls.Add(this.m_txtVisitNotes);
            this.groupControl2.Location = new System.Drawing.Point(3, 149);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(304, 173);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "N&otes";
            // 
            // m_lblVisitNotesShortcut
            // 
            this.m_lblVisitNotesShortcut.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblVisitNotesShortcut.Location = new System.Drawing.Point(5, 45);
            this.m_lblVisitNotesShortcut.Name = "m_lblVisitNotesShortcut";
            this.m_lblVisitNotesShortcut.Size = new System.Drawing.Size(0, 0);
            this.m_lblVisitNotesShortcut.TabIndex = 0;
            this.m_lblVisitNotesShortcut.Text = "&O notes shortcut";
            // 
            // m_txtVisitNotes
            // 
            this.m_txtVisitNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtVisitNotes.Location = new System.Drawing.Point(2, 20);
            this.m_txtVisitNotes.Name = "m_txtVisitNotes";
            this.m_txtVisitNotes.Properties.MaxLength = 1500;
            this.m_txtVisitNotes.Size = new System.Drawing.Size(300, 151);
            this.m_txtVisitNotes.TabIndex = 0;
            // 
            // m_ctlAddressLookup
            // 
            this.m_ctlAddressLookup.BaseAddress = null;
            this.m_ctlAddressLookup.BaseAddressName = null;
            this.m_ctlAddressLookup.Caption = null;
            this.m_ctlAddressLookup.CurrentAddress = null;
            this.m_ctlAddressLookup.EditButtonText = "Edi&t";
            this.m_ctlAddressLookup.IsBaseAddressActive = false;
            this.m_ctlAddressLookup.Location = new System.Drawing.Point(313, 3);
            this.m_ctlAddressLookup.Name = "m_ctlAddressLookup";
            this.m_ctlAddressLookup.Size = new System.Drawing.Size(290, 140);
            this.m_ctlAddressLookup.TabIndex = 1;
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
            this.m_ctlCustomerLookup.Size = new System.Drawing.Size(304, 140);
            this.m_ctlCustomerLookup.TabIndex = 0;
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.m_lblShortcutTasks);
            this.xtraTabPage1.Controls.Add(this.m_ctlProjectEdit);
            this.xtraTabPage1.Controls.Add(this.m_ctlTaskEdit);
            this.xtraTabPage1.Controls.Add(this.m_btnAddTask);
            this.xtraTabPage1.Controls.Add(this.m_treeTasks);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(607, 637);
            this.xtraTabPage1.Text = "&Tasks";
            // 
            // m_lblShortcutTasks
            // 
            this.m_lblShortcutTasks.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblShortcutTasks.Location = new System.Drawing.Point(16, 56);
            this.m_lblShortcutTasks.Name = "m_lblShortcutTasks";
            this.m_lblShortcutTasks.Size = new System.Drawing.Size(0, 0);
            this.m_lblShortcutTasks.TabIndex = 0;
            this.m_lblShortcutTasks.Text = "&B Shortcut";
            // 
            // m_ctlProjectEdit
            // 
            this.m_ctlProjectEdit.AreaId = null;
            this.m_ctlProjectEdit.IsEditable = false;
            this.m_ctlProjectEdit.IsInsuranceVisible = false;
            this.m_ctlProjectEdit.IsQbSalesRepRequired = false;
            this.m_ctlProjectEdit.IsQbSalesRepVisible = true;
            this.m_ctlProjectEdit.Location = new System.Drawing.Point(3, 231);
            this.m_ctlProjectEdit.Name = "m_ctlProjectEdit";
            this.m_ctlProjectEdit.Project = null;
            this.m_ctlProjectEdit.Size = new System.Drawing.Size(600, 406);
            this.m_ctlProjectEdit.TabIndex = 2;
            // 
            // m_ctlTaskEdit
            // 
            this.m_ctlTaskEdit.IsClosedAmountEditable = false;
            this.m_ctlTaskEdit.IsClosedAmountUnknownVisible = false;
            this.m_ctlTaskEdit.IsEditable = false;
            this.m_ctlTaskEdit.Items = null;
            this.m_ctlTaskEdit.Location = new System.Drawing.Point(3, 228);
            this.m_ctlTaskEdit.Name = "m_ctlTaskEdit";
            this.m_ctlTaskEdit.OriginalMessage = null;
            this.m_ctlTaskEdit.Size = new System.Drawing.Size(603, 410);
            this.m_ctlTaskEdit.TabIndex = 3;
            this.m_ctlTaskEdit.Task = null;
            this.m_ctlTaskEdit.Visible = false;
            // 
            // m_btnAddTask
            // 
            this.m_btnAddTask.Location = new System.Drawing.Point(529, 201);
            this.m_btnAddTask.Name = "m_btnAddTask";
            this.m_btnAddTask.Size = new System.Drawing.Size(75, 23);
            this.m_btnAddTask.TabIndex = 15;
            this.m_btnAddTask.Text = "&Add";
            // 
            // m_treeTasks
            // 
            this.m_treeTasks.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.m_colTask,
            this.treeListColumn3,
            this.treeListColumn4,
            this.m_colTaskAction});
            this.m_treeTasks.Location = new System.Drawing.Point(6, 3);
            this.m_treeTasks.Name = "m_treeTasks";
            this.m_treeTasks.OptionsBehavior.AllowExpandOnDblClick = false;
            this.m_treeTasks.ParentFieldName = "ParentId";
            this.m_treeTasks.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1,
            this.m_cmbTaskActionDefloodFirstTime,
            this.m_cmbTaskActionDeflood,
            this.m_cmbTaskAction,
            this.m_cmbTaskActionNotApplicable,
            this.m_cmbTaskActionDefloodAdded,
            this.m_cmbTaskActionAdded});
            this.m_treeTasks.SelectImageList = this.m_imagesTree;
            this.m_treeTasks.Size = new System.Drawing.Size(517, 221);
            this.m_treeTasks.TabIndex = 1;
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
            this.m_colTask.Width = 243;
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
            this.treeListColumn3.Width = 72;
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "Cost";
            this.treeListColumn4.FieldName = "CostText";
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.OptionsColumn.AllowEdit = false;
            this.treeListColumn4.Visible = true;
            this.treeListColumn4.VisibleIndex = 2;
            this.treeListColumn4.Width = 89;
            // 
            // m_colTaskAction
            // 
            this.m_colTaskAction.Caption = "Action";
            this.m_colTaskAction.FieldName = "TaskActionId";
            this.m_colTaskAction.Name = "m_colTaskAction";
            this.m_colTaskAction.Visible = true;
            this.m_colTaskAction.VisibleIndex = 3;
            this.m_colTaskAction.Width = 92;
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
            // 
            // m_cmbTaskActionDefloodFirstTime
            // 
            this.m_cmbTaskActionDefloodFirstTime.AutoHeight = false;
            this.m_cmbTaskActionDefloodFirstTime.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbTaskActionDefloodFirstTime.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Complete", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("In Process", 2, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Fail", 3, -1)});
            this.m_cmbTaskActionDefloodFirstTime.Name = "m_cmbTaskActionDefloodFirstTime";
            // 
            // m_cmbTaskActionDeflood
            // 
            this.m_cmbTaskActionDeflood.AutoHeight = false;
            this.m_cmbTaskActionDeflood.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbTaskActionDeflood.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Complete", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("In Process", 2, -1)});
            this.m_cmbTaskActionDeflood.Name = "m_cmbTaskActionDeflood";
            // 
            // m_cmbTaskAction
            // 
            this.m_cmbTaskAction.AutoHeight = false;
            this.m_cmbTaskAction.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbTaskAction.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Complete", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Fail", 3, -1)});
            this.m_cmbTaskAction.Name = "m_cmbTaskAction";
            // 
            // m_cmbTaskActionNotApplicable
            // 
            this.m_cmbTaskActionNotApplicable.AutoHeight = false;
            this.m_cmbTaskActionNotApplicable.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbTaskActionNotApplicable.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Not Applicable", 4, -1)});
            this.m_cmbTaskActionNotApplicable.Name = "m_cmbTaskActionNotApplicable";
            // 
            // m_cmbTaskActionDefloodAdded
            // 
            this.m_cmbTaskActionDefloodAdded.AutoHeight = false;
            this.m_cmbTaskActionDefloodAdded.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbTaskActionDefloodAdded.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Book", 5, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Complete", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("In Process", 2, -1)});
            this.m_cmbTaskActionDefloodAdded.Name = "m_cmbTaskActionDefloodAdded";
            // 
            // m_cmbTaskActionAdded
            // 
            this.m_cmbTaskActionAdded.AutoHeight = false;
            this.m_cmbTaskActionAdded.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbTaskActionAdded.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Book", 5, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Complete", 1, -1)});
            this.m_cmbTaskActionAdded.Name = "m_cmbTaskActionAdded";
            // 
            // m_imagesTree
            // 
            this.m_imagesTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imagesTree.ImageStream")));
            this.m_imagesTree.TransparentColor = System.Drawing.Color.Magenta;
            this.m_imagesTree.Images.SetKeyName(0, "fixed.bmp");
            this.m_imagesTree.Images.SetKeyName(1, "folder.bmp");
            this.m_imagesTree.Images.SetKeyName(2, "postponed.bmp");
            this.m_imagesTree.Images.SetKeyName(3, "fixedincluded.bmp");
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.Location = new System.Drawing.Point(543, 705);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 4;
            this.m_btnCancel.Text = "Cancel";
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.Location = new System.Drawing.Point(462, 705);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 3;
            this.m_btnOk.Text = "Complete";
            // 
            // m_images
            // 
            this.m_images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_images.ImageStream")));
            this.m_images.TransparentColor = System.Drawing.Color.Magenta;
            this.m_images.Images.SetKeyName(0, "plus.bmp");
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
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 11;
            this.barButtonItem1.Name = "barButtonItem1";
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
            this.m_barManager.DockControls.Add(this.barDockControl1);
            this.m_barManager.DockControls.Add(this.barDockControl2);
            this.m_barManager.DockControls.Add(this.barDockControl3);
            this.m_barManager.DockControls.Add(this.barDockControl4);
            this.m_barManager.Form = this;
            this.m_barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.m_menuAddRugPickup,
            this.m_menuAddDeflood,
            this.m_menuAddMiscellaneous,
            this.m_menuDeleteTask,
            this.m_menuActionAddMiscellaneous,
            this.m_menuActionAddRugPickup,
            this.m_menuCompleteWithPayment,
            this.m_menuCompleteWithoutPayment});
            this.m_barManager.MaxItemId = 20;
            this.m_barManager.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit4});
            // 
            // m_menuDeleteTask
            // 
            this.m_menuDeleteTask.Caption = "Delete";
            this.m_menuDeleteTask.Id = 10;
            this.m_menuDeleteTask.Name = "m_menuDeleteTask";
            // 
            // m_menuActionAddMiscellaneous
            // 
            this.m_menuActionAddMiscellaneous.Caption = "Add Miscellaneous";
            this.m_menuActionAddMiscellaneous.Id = 13;
            this.m_menuActionAddMiscellaneous.Name = "m_menuActionAddMiscellaneous";
            // 
            // m_menuActionAddRugPickup
            // 
            this.m_menuActionAddRugPickup.Caption = "Add Rug Pickup";
            this.m_menuActionAddRugPickup.Id = 15;
            this.m_menuActionAddRugPickup.Name = "m_menuActionAddRugPickup";
            // 
            // m_menuCompleteWithPayment
            // 
            this.m_menuCompleteWithPayment.Caption = "With Payment";
            this.m_menuCompleteWithPayment.Id = 18;
            this.m_menuCompleteWithPayment.Name = "m_menuCompleteWithPayment";
            // 
            // m_menuCompleteWithoutPayment
            // 
            this.m_menuCompleteWithoutPayment.Caption = "Without Payment";
            this.m_menuCompleteWithoutPayment.Id = 19;
            this.m_menuCompleteWithoutPayment.Name = "m_menuCompleteWithoutPayment";
            // 
            // repositoryItemTextEdit4
            // 
            this.repositoryItemTextEdit4.AutoHeight = false;
            this.repositoryItemTextEdit4.Name = "repositoryItemTextEdit4";
            // 
            // m_menuAction
            // 
            this.m_menuAction.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.m_menuActionAddRugPickup),
            new DevExpress.XtraBars.LinkPersistInfo(this.m_menuActionAddMiscellaneous),
            new DevExpress.XtraBars.LinkPersistInfo(this.m_menuDeleteTask, true)});
            this.m_menuAction.Manager = this.m_barManager;
            this.m_menuAction.Name = "m_menuAction";
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Action";
            this.treeListColumn1.FieldName = "TaskActionId";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 2;
            // 
            // m_menuVisitComplete
            // 
            this.m_menuVisitComplete.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.m_menuCompleteWithPayment),
            new DevExpress.XtraBars.LinkPersistInfo(this.m_menuCompleteWithoutPayment)});
            this.m_menuVisitComplete.Manager = this.m_barManager;
            this.m_menuVisitComplete.Name = "m_menuVisitComplete";
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // CompleteVisitView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 731);
            this.ControlBox = false;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlTop);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CompleteVisitView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CompleteRugPickupView";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_timeComplete.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_tabs)).EndInit();
            this.m_tabs.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtDropOff.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtPickup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_txtVisitNotes.Properties)).EndInit();
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_treeTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTaskActionDefloodFirstTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTaskActionDeflood)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTaskAction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTaskActionNotApplicable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTaskActionDefloodAdded)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbTaskActionAdded)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_menuAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_menuAction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_menuVisitComplete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.SimpleButton m_btnCancel;
        internal DevExpress.XtraEditors.SimpleButton m_btnOk;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        internal DevExpress.XtraTab.XtraTabControl m_tabs;
        internal System.Windows.Forms.ImageList m_images;
        internal DevExpress.XtraTreeList.TreeList m_treeTasks;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        internal DevExpress.XtraEditors.SimpleButton m_btnAddTask;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarListItem barListItem1;
        private DevExpress.XtraBars.BarLinkContainerItem barLinkContainerItem1;
        private DevExpress.XtraBars.BarListItem barListItem2;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        internal System.Windows.Forms.ImageList m_imagesTree;
        internal DevExpress.XtraBars.PopupMenu m_menuAdd;
        internal DevExpress.XtraBars.PopupMenu m_menuAction;
        internal DevExpress.XtraBars.BarManager m_barManager;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit4;
        internal DevExpress.XtraBars.BarButtonItem m_menuAddRugPickup;
        internal DevExpress.XtraBars.BarButtonItem m_menuAddDeflood;
        internal DevExpress.XtraBars.BarButtonItem m_menuAddMiscellaneous;
        internal DevExpress.XtraBars.BarButtonItem m_menuDeleteTask;
        internal DevExpress.XtraBars.BarButtonItem m_menuActionAddMiscellaneous;
        internal DevExpress.XtraBars.BarButtonItem m_menuActionAddRugPickup;
        internal Dalworth.Server.MainForm.Components.TaskEditComplete m_ctlTaskEdit;
        internal DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox m_cmbTaskActionDefloodFirstTime;
        internal DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox m_cmbTaskActionNotApplicable;
        internal DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox m_cmbTaskActionDeflood;
        internal DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox m_cmbTaskAction;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        internal DevExpress.XtraTreeList.Columns.TreeListColumn m_colTaskAction;
        internal DevExpress.XtraBars.BarButtonItem m_menuCompleteWithPayment;
        internal DevExpress.XtraBars.BarButtonItem m_menuCompleteWithoutPayment;
        internal DevExpress.XtraBars.PopupMenu m_menuVisitComplete;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        internal Dalworth.Server.MainForm.Components.ProjectEdit m_ctlProjectEdit;
        internal TimeEditEx m_timeComplete;
        private DevExpress.XtraEditors.LabelControl label1;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        internal System.Windows.Forms.Label m_lblEquipmentVanTotals;
        internal System.Windows.Forms.Label m_lblEquipmentCustomerTotals;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        internal Dalworth.Server.MainForm.Components.AddressViewEdit m_ctlAddressLookup;
        internal Dalworth.Server.MainForm.Components.CustomerViewEditLookup m_ctlCustomerLookup;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        internal DevExpress.XtraEditors.MemoEdit m_txtVisitNotes;
        internal DevExpress.XtraTreeList.Columns.TreeListColumn m_colTask;
        private DevExpress.XtraEditors.LabelControl m_lblVisitNotesShortcut;
        private DevExpress.XtraEditors.LabelControl m_lblShortcutTasks;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        internal System.Windows.Forms.Label m_lblVisitNumber;
        internal DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox m_cmbTaskActionDefloodAdded;
        internal DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox m_cmbTaskActionAdded;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        internal DevExpress.XtraEditors.LabelControl m_lblEndDayTime;
        internal DevExpress.XtraEditors.LabelControl labelControl2;
        internal DevExpress.XtraEditors.LabelControl labelControl4;
        internal DevExpress.XtraEditors.LabelControl labelControl1;
        internal Dalworth.Server.MainForm.Components.EquipmentQuantityTextEdit m_txtDropOff;
        internal Dalworth.Server.MainForm.Components.EquipmentQuantityTextEdit m_txtPickup;
    }
}