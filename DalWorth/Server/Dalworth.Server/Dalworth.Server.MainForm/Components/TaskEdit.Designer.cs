namespace Dalworth.Server.MainForm.Components
{
    partial class TaskEdit
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_ctlMonitoring = new Dalworth.Server.MainForm.Components.MonitoringEdit();
            this.m_ctlDeflood = new Dalworth.Server.MainForm.Components.DefloodEdit();
            this.m_layoutFail = new System.Windows.Forms.TableLayoutPanel();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.m_layoutNotes = new System.Windows.Forms.TableLayoutPanel();
            this.m_txtNotesPrevious = new DevExpress.XtraEditors.MemoEdit();
            this.m_txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.m_lblNotesShortcut = new DevExpress.XtraEditors.LabelControl();
            this.m_groupFailSection = new DevExpress.XtraEditors.GroupControl();
            this.m_lblReason = new DevExpress.XtraEditors.LabelControl();
            this.m_txtFailReason = new DevExpress.XtraEditors.MemoEdit();
            this.m_cmbFailType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.m_ctlItems = new Dalworth.Server.MainForm.Components.RugsView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.m_lblDiscount = new DevExpress.XtraEditors.LabelControl();
            this.m_spinDiscount = new DevExpress.XtraEditors.SpinEdit();
            this.m_chkIsRugDepartment = new DevExpress.XtraEditors.CheckEdit();
            this.m_lblEstimateClosedAmountValue = new System.Windows.Forms.Label();
            this.m_lblEstimateClosedAmountLabel = new DevExpress.XtraEditors.LabelControl();
            this.m_chkAmountNotKnown = new DevExpress.XtraEditors.CheckEdit();
            this.m_lblTaskStatus = new DevExpress.XtraEditors.LabelControl();
            this.m_chkAmountAutoCalculated = new DevExpress.XtraEditors.CheckEdit();
            this.m_lblClosedAmount = new DevExpress.XtraEditors.LabelControl();
            this.m_txtClosedAmount = new DevExpress.XtraEditors.TextEdit();
            this.m_lblCreated = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.m_chkReady = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblNumber = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblType = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.m_layoutFail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.m_layoutNotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtNotesPrevious.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_groupFailSection)).BeginInit();
            this.m_groupFailSection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtFailReason.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbFailType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_spinDiscount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkIsRugDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkAmountNotKnown.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkAmountAutoCalculated.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtClosedAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkReady.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_ctlMonitoring);
            this.panelControl1.Controls.Add(this.m_ctlDeflood);
            this.panelControl1.Controls.Add(this.m_layoutFail);
            this.panelControl1.Controls.Add(this.m_ctlItems);
            this.panelControl1.Controls.Add(this.groupControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(540, 412);
            this.panelControl1.TabIndex = 0;
            // 
            // m_ctlMonitoring
            // 
            this.m_ctlMonitoring.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ctlMonitoring.IsEditable = false;
            this.m_ctlMonitoring.Location = new System.Drawing.Point(3, 215);
            this.m_ctlMonitoring.MonitoringDetail = null;
            this.m_ctlMonitoring.Name = "m_ctlMonitoring";
            this.m_ctlMonitoring.Size = new System.Drawing.Size(533, 193);
            this.m_ctlMonitoring.TabIndex = 9;
            // 
            // m_ctlDeflood
            // 
            this.m_ctlDeflood.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ctlDeflood.DefloodDetail = null;
            this.m_ctlDeflood.IsEditable = false;
            this.m_ctlDeflood.Location = new System.Drawing.Point(3, 215);
            this.m_ctlDeflood.Name = "m_ctlDeflood";
            this.m_ctlDeflood.Size = new System.Drawing.Size(533, 193);
            this.m_ctlDeflood.TabIndex = 10;
            // 
            // m_layoutFail
            // 
            this.m_layoutFail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_layoutFail.ColumnCount = 2;
            this.m_layoutFail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.m_layoutFail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.m_layoutFail.Controls.Add(this.groupControl2, 0, 0);
            this.m_layoutFail.Controls.Add(this.m_groupFailSection, 1, 0);
            this.m_layoutFail.Location = new System.Drawing.Point(172, 3);
            this.m_layoutFail.Name = "m_layoutFail";
            this.m_layoutFail.RowCount = 1;
            this.m_layoutFail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.m_layoutFail.Size = new System.Drawing.Size(364, 206);
            this.m_layoutFail.TabIndex = 4;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.m_layoutNotes);
            this.groupControl2.Controls.Add(this.m_lblNotesShortcut);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(179, 206);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "N&otes";
            // 
            // m_layoutNotes
            // 
            this.m_layoutNotes.ColumnCount = 1;
            this.m_layoutNotes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.m_layoutNotes.Controls.Add(this.m_txtNotesPrevious, 0, 0);
            this.m_layoutNotes.Controls.Add(this.m_txtNotes, 0, 1);
            this.m_layoutNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_layoutNotes.Location = new System.Drawing.Point(2, 20);
            this.m_layoutNotes.Name = "m_layoutNotes";
            this.m_layoutNotes.RowCount = 2;
            this.m_layoutNotes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.m_layoutNotes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.m_layoutNotes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.m_layoutNotes.Size = new System.Drawing.Size(175, 184);
            this.m_layoutNotes.TabIndex = 2;
            // 
            // m_txtNotesPrevious
            // 
            this.m_txtNotesPrevious.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtNotesPrevious.Location = new System.Drawing.Point(0, 1);
            this.m_txtNotesPrevious.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.m_txtNotesPrevious.Name = "m_txtNotesPrevious";
            this.m_txtNotesPrevious.Properties.MaxLength = 500;
            this.m_txtNotesPrevious.Properties.ReadOnly = true;
            this.m_txtNotesPrevious.Size = new System.Drawing.Size(175, 91);
            this.m_txtNotesPrevious.TabIndex = 1;
            // 
            // m_txtNotes
            // 
            this.m_txtNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtNotes.Location = new System.Drawing.Point(0, 93);
            this.m_txtNotes.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.m_txtNotes.Name = "m_txtNotes";
            this.m_txtNotes.Properties.MaxLength = 500;
            this.m_txtNotes.Size = new System.Drawing.Size(175, 91);
            this.m_txtNotes.TabIndex = 2;
            // 
            // m_lblNotesShortcut
            // 
            this.m_lblNotesShortcut.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblNotesShortcut.Location = new System.Drawing.Point(5, 26);
            this.m_lblNotesShortcut.Name = "m_lblNotesShortcut";
            this.m_lblNotesShortcut.Size = new System.Drawing.Size(0, 0);
            this.m_lblNotesShortcut.TabIndex = 0;
            this.m_lblNotesShortcut.Text = "N&otes";
            // 
            // m_groupFailSection
            // 
            this.m_groupFailSection.Controls.Add(this.m_lblReason);
            this.m_groupFailSection.Controls.Add(this.m_txtFailReason);
            this.m_groupFailSection.Controls.Add(this.m_cmbFailType);
            this.m_groupFailSection.Controls.Add(this.labelControl9);
            this.m_groupFailSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_groupFailSection.Location = new System.Drawing.Point(185, 0);
            this.m_groupFailSection.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.m_groupFailSection.Name = "m_groupFailSection";
            this.m_groupFailSection.Size = new System.Drawing.Size(179, 206);
            this.m_groupFailSection.TabIndex = 2;
            this.m_groupFailSection.Text = "Fail";
            // 
            // m_lblReason
            // 
            this.m_lblReason.Location = new System.Drawing.Point(6, 51);
            this.m_lblReason.Name = "m_lblReason";
            this.m_lblReason.Size = new System.Drawing.Size(36, 13);
            this.m_lblReason.TabIndex = 7;
            this.m_lblReason.Text = "Rea&son";
            // 
            // m_txtFailReason
            // 
            this.m_txtFailReason.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtFailReason.Location = new System.Drawing.Point(6, 70);
            this.m_txtFailReason.Name = "m_txtFailReason";
            this.m_txtFailReason.Properties.MaxLength = 500;
            this.m_txtFailReason.Size = new System.Drawing.Size(168, 134);
            this.m_txtFailReason.TabIndex = 8;
            // 
            // m_cmbFailType
            // 
            this.m_cmbFailType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbFailType.Location = new System.Drawing.Point(46, 23);
            this.m_cmbFailType.Name = "m_cmbFailType";
            this.m_cmbFailType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbFailType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Must Return", 1, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Cancel", 3, -1)});
            this.m_cmbFailType.Size = new System.Drawing.Size(128, 20);
            this.m_cmbFailType.TabIndex = 6;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(5, 26);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(24, 13);
            this.labelControl9.TabIndex = 5;
            this.labelControl9.Text = "T&ype";
            // 
            // m_ctlItems
            // 
            this.m_ctlItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ctlItems.IsEditable = false;
            this.m_ctlItems.IsPartOfFlood = false;            
            this.m_ctlItems.Location = new System.Drawing.Point(3, 215);
            this.m_ctlItems.Name = "m_ctlItems";
            this.m_ctlItems.Size = new System.Drawing.Size(534, 193);
            this.m_ctlItems.TabIndex = 11;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.m_lblDiscount);
            this.groupControl1.Controls.Add(this.m_spinDiscount);
            this.groupControl1.Controls.Add(this.m_chkIsRugDepartment);
            this.groupControl1.Controls.Add(this.m_lblEstimateClosedAmountValue);
            this.groupControl1.Controls.Add(this.m_lblEstimateClosedAmountLabel);
            this.groupControl1.Controls.Add(this.m_chkAmountNotKnown);
            this.groupControl1.Controls.Add(this.m_lblTaskStatus);
            this.groupControl1.Controls.Add(this.m_chkAmountAutoCalculated);
            this.groupControl1.Controls.Add(this.m_lblClosedAmount);
            this.groupControl1.Controls.Add(this.m_txtClosedAmount);
            this.groupControl1.Controls.Add(this.m_lblCreated);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.m_chkReady);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.m_lblNumber);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.m_lblType);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(3, 3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(163, 206);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Task Info";
            // 
            // m_lblDiscount
            // 
            this.m_lblDiscount.Location = new System.Drawing.Point(5, 160);
            this.m_lblDiscount.Name = "m_lblDiscount";
            this.m_lblDiscount.Size = new System.Drawing.Size(59, 13);
            this.m_lblDiscount.TabIndex = 23;
            this.m_lblDiscount.Text = "Di&scount, %";
            // 
            // m_spinDiscount
            // 
            this.m_spinDiscount.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.m_spinDiscount.Location = new System.Drawing.Point(78, 157);
            this.m_spinDiscount.Name = "m_spinDiscount";
            this.m_spinDiscount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.m_spinDiscount.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.m_spinDiscount.Properties.IsFloatValue = false;
            this.m_spinDiscount.Properties.Mask.EditMask = "N00";
            this.m_spinDiscount.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.m_spinDiscount.Size = new System.Drawing.Size(78, 20);
            this.m_spinDiscount.TabIndex = 24;
            // 
            // m_chkIsRugDepartment
            // 
            this.m_chkIsRugDepartment.Location = new System.Drawing.Point(3, 132);
            this.m_chkIsRugDepartment.Name = "m_chkIsRugDepartment";
            this.m_chkIsRugDepartment.Properties.Caption = "Rug Cleaning Department";
            this.m_chkIsRugDepartment.Size = new System.Drawing.Size(153, 19);
            this.m_chkIsRugDepartment.TabIndex = 21;
            // 
            // m_lblEstimateClosedAmountValue
            // 
            this.m_lblEstimateClosedAmountValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblEstimateClosedAmountValue.Location = new System.Drawing.Point(79, 182);
            this.m_lblEstimateClosedAmountValue.Name = "m_lblEstimateClosedAmountValue";
            this.m_lblEstimateClosedAmountValue.Size = new System.Drawing.Size(80, 13);
            this.m_lblEstimateClosedAmountValue.TabIndex = 20;
            this.m_lblEstimateClosedAmountValue.Text = "$50.00";
            this.m_lblEstimateClosedAmountValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblEstimateClosedAmountLabel
            // 
            this.m_lblEstimateClosedAmountLabel.Location = new System.Drawing.Point(5, 182);
            this.m_lblEstimateClosedAmountLabel.Name = "m_lblEstimateClosedAmountLabel";
            this.m_lblEstimateClosedAmountLabel.Size = new System.Drawing.Size(72, 13);
            this.m_lblEstimateClosedAmountLabel.TabIndex = 18;
            this.m_lblEstimateClosedAmountLabel.Text = "Est Closed Amt";
            // 
            // m_chkAmountNotKnown
            // 
            this.m_chkAmountNotKnown.Location = new System.Drawing.Point(3, 131);
            this.m_chkAmountNotKnown.Name = "m_chkAmountNotKnown";
            this.m_chkAmountNotKnown.Properties.Caption = "Amount not &known";
            this.m_chkAmountNotKnown.Size = new System.Drawing.Size(138, 19);
            this.m_chkAmountNotKnown.TabIndex = 2;
            this.m_chkAmountNotKnown.Visible = false;
            // 
            // m_lblTaskStatus
            // 
            this.m_lblTaskStatus.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblTaskStatus.Appearance.Options.UseFont = true;
            this.m_lblTaskStatus.Location = new System.Drawing.Point(79, 64);
            this.m_lblTaskStatus.Name = "m_lblTaskStatus";
            this.m_lblTaskStatus.Size = new System.Drawing.Size(61, 13);
            this.m_lblTaskStatus.TabIndex = 17;
            this.m_lblTaskStatus.Text = "Completed";
            // 
            // m_chkAmountAutoCalculated
            // 
            this.m_chkAmountAutoCalculated.Location = new System.Drawing.Point(3, 132);
            this.m_chkAmountAutoCalculated.Name = "m_chkAmountAutoCalculated";
            this.m_chkAmountAutoCalculated.Properties.Caption = "A&utocalculated";
            this.m_chkAmountAutoCalculated.Size = new System.Drawing.Size(93, 19);
            this.m_chkAmountAutoCalculated.TabIndex = 16;
            // 
            // m_lblClosedAmount
            // 
            this.m_lblClosedAmount.Location = new System.Drawing.Point(5, 109);
            this.m_lblClosedAmount.Name = "m_lblClosedAmount";
            this.m_lblClosedAmount.Size = new System.Drawing.Size(54, 13);
            this.m_lblClosedAmount.TabIndex = 0;
            this.m_lblClosedAmount.Text = "&Closed Amt";
            // 
            // m_txtClosedAmount
            // 
            this.m_txtClosedAmount.Location = new System.Drawing.Point(78, 106);
            this.m_txtClosedAmount.Name = "m_txtClosedAmount";
            this.m_txtClosedAmount.Properties.DisplayFormat.FormatString = "C";
            this.m_txtClosedAmount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.m_txtClosedAmount.Properties.Mask.EditMask = "$###,###,###,##0.00;($###,###,###,##0.00)";
            this.m_txtClosedAmount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.m_txtClosedAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.m_txtClosedAmount.Size = new System.Drawing.Size(78, 20);
            this.m_txtClosedAmount.TabIndex = 1;
            // 
            // m_lblCreated
            // 
            this.m_lblCreated.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblCreated.Appearance.Options.UseFont = true;
            this.m_lblCreated.Location = new System.Drawing.Point(79, 87);
            this.m_lblCreated.Name = "m_lblCreated";
            this.m_lblCreated.Size = new System.Drawing.Size(61, 13);
            this.m_lblCreated.TabIndex = 9;
            this.m_lblCreated.Text = "1/15/2007";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(5, 87);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(39, 13);
            this.labelControl7.TabIndex = 8;
            this.labelControl7.Text = "Created";
            // 
            // m_chkReady
            // 
            this.m_chkReady.Location = new System.Drawing.Point(102, 132);
            this.m_chkReady.Name = "m_chkReady";
            this.m_chkReady.Properties.Caption = "R&eady";
            this.m_chkReady.Size = new System.Drawing.Size(54, 19);
            this.m_chkReady.TabIndex = 3;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(5, 64);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(31, 13);
            this.labelControl6.TabIndex = 4;
            this.labelControl6.Text = "Status";
            // 
            // m_lblNumber
            // 
            this.m_lblNumber.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblNumber.Appearance.Options.UseFont = true;
            this.m_lblNumber.Location = new System.Drawing.Point(78, 23);
            this.m_lblNumber.Name = "m_lblNumber";
            this.m_lblNumber.Size = new System.Drawing.Size(28, 13);
            this.m_lblNumber.TabIndex = 3;
            this.m_lblNumber.Text = "1000";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(5, 23);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(37, 13);
            this.labelControl4.TabIndex = 2;
            this.labelControl4.Text = "Number";
            // 
            // m_lblType
            // 
            this.m_lblType.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblType.Appearance.Options.UseFont = true;
            this.m_lblType.Location = new System.Drawing.Point(78, 42);
            this.m_lblType.Name = "m_lblType";
            this.m_lblType.Size = new System.Drawing.Size(62, 13);
            this.m_lblType.TabIndex = 1;
            this.m_lblType.Text = "Rug Pickup";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 42);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Type";
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(5, 109);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(54, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "&Closed Amt";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(5, 181);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(72, 13);
            this.labelControl5.TabIndex = 18;
            this.labelControl5.Text = "Est Closed Amt";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(5, 178);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(41, 13);
            this.labelControl3.TabIndex = 23;
            this.labelControl3.Text = "Discount";
            // 
            // TaskEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "TaskEdit";
            this.Size = new System.Drawing.Size(540, 412);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.m_layoutFail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.m_layoutNotes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_txtNotesPrevious.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_groupFailSection)).EndInit();
            this.m_groupFailSection.ResumeLayout(false);
            this.m_groupFailSection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtFailReason.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbFailType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_spinDiscount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkIsRugDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkAmountNotKnown.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkAmountAutoCalculated.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtClosedAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chkReady.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraEditors.PanelControl panelControl1;
        protected DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl m_lblCreated;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        protected DevExpress.XtraEditors.CheckEdit m_chkReady;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl m_lblNumber;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl m_lblType;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        protected DevExpress.XtraEditors.GroupControl m_groupFailSection;
        protected DevExpress.XtraEditors.MemoEdit m_txtFailReason;
        protected DevExpress.XtraEditors.ImageComboBoxEdit m_cmbFailType;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        protected System.Windows.Forms.TableLayoutPanel m_layoutFail;
        protected DevExpress.XtraEditors.MemoEdit m_txtNotes;
        protected DevExpress.XtraEditors.LabelControl m_lblReason;
        private DevExpress.XtraEditors.LabelControl m_lblClosedAmount;
        protected DevExpress.XtraEditors.CheckEdit m_chkAmountAutoCalculated;
        protected DefloodEdit m_ctlDeflood;
        protected MonitoringEdit m_ctlMonitoring;
        private DevExpress.XtraEditors.LabelControl m_lblTaskStatus;
        protected DevExpress.XtraEditors.TextEdit m_txtClosedAmount;
        internal RugsView m_ctlItems;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        protected DevExpress.XtraEditors.CheckEdit m_chkAmountNotKnown;
        private DevExpress.XtraEditors.LabelControl m_lblNotesShortcut;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl m_lblEstimateClosedAmountLabel;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private System.Windows.Forms.Label m_lblEstimateClosedAmountValue;
        private System.Windows.Forms.TableLayoutPanel m_layoutNotes;
        protected DevExpress.XtraEditors.MemoEdit m_txtNotesPrevious;
        protected DevExpress.XtraEditors.CheckEdit m_chkIsRugDepartment;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        protected DevExpress.XtraEditors.LabelControl m_lblDiscount;
        protected DevExpress.XtraEditors.SpinEdit m_spinDiscount;
    }
}
