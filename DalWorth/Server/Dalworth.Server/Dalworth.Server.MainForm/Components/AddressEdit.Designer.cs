namespace Dalworth.Server.MainForm.Components
{
    partial class AddressEdit
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
            this.m_cmbState = new DevExpress.XtraEditors.ComboBoxEdit();
            this.m_txtMapsco = new DevExpress.XtraEditors.TextEdit();
            this.m_txtZip = new DevExpress.XtraEditors.TextEdit();
            this.m_txtCity = new DevExpress.XtraEditors.TextEdit();
            this.m_lblCityStateZip = new DevExpress.XtraEditors.LabelControl();
            this.m_lblMapsco = new DevExpress.XtraEditors.LabelControl();
            this.m_errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.m_btnMapscoLookup = new DevExpress.XtraEditors.SimpleButton();
            this.m_lblArea = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.m_cmbSuffux = new DevExpress.XtraEditors.ComboBoxEdit();
            this.m_cmbPrefix = new DevExpress.XtraEditors.ComboBoxEdit();
            this.m_txtAddress2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtUnit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.m_txtStreet = new DevExpress.XtraEditors.TextEdit();
            this.m_txtBlock = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.m_lblAddressTip = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtMapsco.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtZip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtCity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbSuffux.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbPrefix.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtAddress2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtStreet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtBlock.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // m_cmbState
            // 
            this.m_cmbState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbState.EditValue = "TX";
            this.m_cmbState.Location = new System.Drawing.Point(438, 78);
            this.m_cmbState.Name = "m_cmbState";
            this.m_cmbState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbState.Properties.Items.AddRange(new object[] {
            "AL",
            "AK",
            "AZ",
            "AR",
            "CA",
            "CO",
            "CT",
            "DC",
            "DE",
            "FL",
            "GA",
            "HI",
            "ID",
            "IL",
            "IN",
            "IA",
            "KS",
            "KY",
            "LA",
            "ME",
            "MD",
            "MA",
            "MI",
            "MN",
            "MS",
            "MO",
            "MT",
            "NE",
            "NV",
            "NH",
            "NJ",
            "NM",
            "NY",
            "NC",
            "ND",
            "OH",
            "OK",
            "OR",
            "PA",
            "RI",
            "SC",
            "SD",
            "TN",
            "TX",
            "UT",
            "VT",
            "VA",
            "WA",
            "WV",
            "WI",
            "WY"});
            this.m_cmbState.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.m_cmbState.Size = new System.Drawing.Size(78, 20);
            this.m_cmbState.TabIndex = 17;
            // 
            // m_txtMapsco
            // 
            this.m_txtMapsco.Location = new System.Drawing.Point(44, 78);
            this.m_txtMapsco.Name = "m_txtMapsco";
            this.m_txtMapsco.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_txtMapsco.Properties.MaxLength = 6;
            this.m_txtMapsco.Size = new System.Drawing.Size(67, 20);
            this.m_txtMapsco.TabIndex = 13;
            // 
            // m_txtZip
            // 
            this.m_txtZip.Location = new System.Drawing.Point(44, 0);
            this.m_txtZip.Name = "m_txtZip";
            this.m_txtZip.Properties.Mask.EditMask = "\\d{5}";
            this.m_txtZip.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.m_txtZip.Properties.Mask.SaveLiteral = false;
            this.m_txtZip.Size = new System.Drawing.Size(67, 20);
            this.m_txtZip.TabIndex = 1;
            // 
            // m_txtCity
            // 
            this.m_txtCity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtCity.Location = new System.Drawing.Point(227, 78);
            this.m_txtCity.Name = "m_txtCity";
            this.m_txtCity.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_txtCity.Properties.MaxLength = 24;
            this.m_txtCity.Size = new System.Drawing.Size(205, 20);
            this.m_txtCity.TabIndex = 16;
            // 
            // m_lblCityStateZip
            // 
            this.m_lblCityStateZip.AllowDrop = true;
            this.m_lblCityStateZip.Location = new System.Drawing.Point(169, 81);
            this.m_lblCityStateZip.Name = "m_lblCityStateZip";
            this.m_lblCityStateZip.Size = new System.Drawing.Size(52, 13);
            this.m_lblCityStateZip.TabIndex = 15;
            this.m_lblCityStateZip.Text = "C&ity, State";
            // 
            // m_lblMapsco
            // 
            this.m_lblMapsco.AllowDrop = true;
            this.m_lblMapsco.Location = new System.Drawing.Point(2, 81);
            this.m_lblMapsco.Name = "m_lblMapsco";
            this.m_lblMapsco.Size = new System.Drawing.Size(36, 13);
            this.m_lblMapsco.TabIndex = 12;
            this.m_lblMapsco.Text = "&Mapsco";
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.m_lblAddressTip);
            this.panelControl1.Controls.Add(this.m_btnMapscoLookup);
            this.panelControl1.Controls.Add(this.m_lblArea);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.m_cmbSuffux);
            this.panelControl1.Controls.Add(this.m_cmbPrefix);
            this.panelControl1.Controls.Add(this.m_txtAddress2);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.m_txtUnit);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.m_txtStreet);
            this.panelControl1.Controls.Add(this.m_txtBlock);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.m_lblMapsco);
            this.panelControl1.Controls.Add(this.m_lblCityStateZip);
            this.panelControl1.Controls.Add(this.m_cmbState);
            this.panelControl1.Controls.Add(this.m_txtMapsco);
            this.panelControl1.Controls.Add(this.m_txtZip);
            this.panelControl1.Controls.Add(this.m_txtCity);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(516, 99);
            this.panelControl1.TabIndex = 0;
            // 
            // m_btnMapscoLookup
            // 
            this.m_btnMapscoLookup.Enabled = false;
            this.m_btnMapscoLookup.Location = new System.Drawing.Point(113, 79);
            this.m_btnMapscoLookup.Name = "m_btnMapscoLookup";
            this.m_btnMapscoLookup.Size = new System.Drawing.Size(20, 19);
            this.m_btnMapscoLookup.TabIndex = 14;
            this.m_btnMapscoLookup.Text = "&...";
            // 
            // m_lblArea
            // 
            this.m_lblArea.AllowDrop = true;
            this.m_lblArea.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblArea.Appearance.Options.UseFont = true;
            this.m_lblArea.Location = new System.Drawing.Point(227, 3);
            this.m_lblArea.Name = "m_lblArea";
            this.m_lblArea.Size = new System.Drawing.Size(27, 13);
            this.m_lblArea.TabIndex = 36;
            this.m_lblArea.Text = "Area";
            // 
            // labelControl6
            // 
            this.labelControl6.AllowDrop = true;
            this.labelControl6.Location = new System.Drawing.Point(198, 3);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(23, 13);
            this.labelControl6.TabIndex = 35;
            this.labelControl6.Text = "Area";
            // 
            // labelControl5
            // 
            this.labelControl5.AllowDrop = true;
            this.labelControl5.Location = new System.Drawing.Point(2, 3);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(14, 13);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "&Zip";
            // 
            // m_cmbSuffux
            // 
            this.m_cmbSuffux.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbSuffux.EditValue = "";
            this.m_cmbSuffux.Location = new System.Drawing.Point(438, 26);
            this.m_cmbSuffux.Name = "m_cmbSuffux";
            this.m_cmbSuffux.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbSuffux.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_cmbSuffux.Properties.Items.AddRange(new object[] {
            "AVE",
            "BLVD",
            "BND",
            "CIR",
            "CR",
            "CRK",
            "CT",
            "CV",
            "DR",
            "E",
            "EXPWY",
            "EXPY",
            "FRW",
            "FRWY",
            "FWY",
            "HL",
            "HOLW",
            "HWY",
            "LANE",
            "LN",
            "LOOP",
            "N",
            "PARK",
            "PASS",
            "PKWY",
            "PL",
            "PLZ",
            "PT",
            "RD",
            "RDG",
            "ROW",
            "RUN",
            "S",
            "SQ",
            "ST",
            "TER",
            "TR",
            "TRAIL",
            "TRCE",
            "TRL",
            "VLY",
            "W",
            "WAY",
            "WY",
            "XING"});
            this.m_cmbSuffux.Properties.MaxLength = 5;
            this.m_cmbSuffux.Size = new System.Drawing.Size(78, 20);
            this.m_cmbSuffux.TabIndex = 7;
            // 
            // m_cmbPrefix
            // 
            this.m_cmbPrefix.EditValue = "";
            this.m_cmbPrefix.Location = new System.Drawing.Point(227, 26);
            this.m_cmbPrefix.Name = "m_cmbPrefix";
            this.m_cmbPrefix.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.m_cmbPrefix.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_cmbPrefix.Properties.Items.AddRange(new object[] {
            "N",
            "S",
            "E",
            "W",
            "NE",
            "NW",
            "SE",
            "SW"});
            this.m_cmbPrefix.Properties.MaxLength = 2;
            this.m_cmbPrefix.Size = new System.Drawing.Size(62, 20);
            this.m_cmbPrefix.TabIndex = 5;
            // 
            // m_txtAddress2
            // 
            this.m_txtAddress2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtAddress2.Location = new System.Drawing.Point(227, 52);
            this.m_txtAddress2.Name = "m_txtAddress2";
            this.m_txtAddress2.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_txtAddress2.Properties.MaxLength = 40;
            this.m_txtAddress2.Size = new System.Drawing.Size(289, 20);
            this.m_txtAddress2.TabIndex = 11;
            // 
            // labelControl4
            // 
            this.labelControl4.AllowDrop = true;
            this.labelControl4.Location = new System.Drawing.Point(173, 55);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 13);
            this.labelControl4.TabIndex = 10;
            this.labelControl4.Text = "A&ddress 2";
            // 
            // m_txtUnit
            // 
            this.m_txtUnit.Location = new System.Drawing.Point(44, 52);
            this.m_txtUnit.Name = "m_txtUnit";
            this.m_txtUnit.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_txtUnit.Properties.MaxLength = 8;
            this.m_txtUnit.Size = new System.Drawing.Size(67, 20);
            this.m_txtUnit.TabIndex = 9;
            // 
            // labelControl3
            // 
            this.labelControl3.AllowDrop = true;
            this.labelControl3.Location = new System.Drawing.Point(2, 59);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(19, 13);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "&Unit";
            // 
            // labelControl2
            // 
            this.labelControl2.AllowDrop = true;
            this.labelControl2.Location = new System.Drawing.Point(117, 29);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(104, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "&Prefix, Street, Suffux";
            // 
            // m_txtStreet
            // 
            this.m_txtStreet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtStreet.Location = new System.Drawing.Point(295, 26);
            this.m_txtStreet.Name = "m_txtStreet";
            this.m_txtStreet.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_txtStreet.Properties.MaxLength = 30;
            this.m_txtStreet.Size = new System.Drawing.Size(137, 20);
            this.m_txtStreet.TabIndex = 6;
            // 
            // m_txtBlock
            // 
            this.m_txtBlock.Location = new System.Drawing.Point(44, 26);
            this.m_txtBlock.Name = "m_txtBlock";
            this.m_txtBlock.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.m_txtBlock.Properties.Mask.EditMask = "\\d{0,8}";
            this.m_txtBlock.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.m_txtBlock.Size = new System.Drawing.Size(67, 20);
            this.m_txtBlock.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.AllowDrop = true;
            this.labelControl1.Location = new System.Drawing.Point(2, 29);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Bl&ock";
            // 
            // m_lblAddressTip
            // 
            this.m_lblAddressTip.Appearance.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.m_lblAddressTip.Appearance.ForeColor = System.Drawing.Color.MediumBlue;
            this.m_lblAddressTip.Appearance.Options.UseFont = true;
            this.m_lblAddressTip.Appearance.Options.UseForeColor = true;
            this.m_lblAddressTip.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.m_lblAddressTip.Location = new System.Drawing.Point(227, 15);
            this.m_lblAddressTip.Name = "m_lblAddressTip";
            this.m_lblAddressTip.Size = new System.Drawing.Size(289, 11);
            this.m_lblAddressTip.TabIndex = 37;
            this.m_lblAddressTip.ToolTip = "Original Address String";
            // 
            // AddressEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "AddressEdit";
            this.Size = new System.Drawing.Size(516, 99);
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtMapsco.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtZip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtCity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbSuffux.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_cmbPrefix.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtAddress2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtStreet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_txtBlock.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraEditors.ComboBoxEdit m_cmbState;
        internal DevExpress.XtraEditors.TextEdit m_txtMapsco;
        internal DevExpress.XtraEditors.TextEdit m_txtZip;
        internal DevExpress.XtraEditors.TextEdit m_txtCity;
        internal DevExpress.XtraEditors.LabelControl m_lblCityStateZip;
        internal DevExpress.XtraEditors.LabelControl m_lblMapsco;
        internal DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider m_errorProvider;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        internal DevExpress.XtraEditors.LabelControl labelControl2;
        internal DevExpress.XtraEditors.TextEdit m_txtStreet;
        internal DevExpress.XtraEditors.TextEdit m_txtBlock;
        internal DevExpress.XtraEditors.LabelControl labelControl1;
        internal DevExpress.XtraEditors.TextEdit m_txtAddress2;
        internal DevExpress.XtraEditors.LabelControl labelControl4;
        internal DevExpress.XtraEditors.TextEdit m_txtUnit;
        internal DevExpress.XtraEditors.LabelControl labelControl3;
        internal DevExpress.XtraEditors.ComboBoxEdit m_cmbSuffux;
        internal DevExpress.XtraEditors.ComboBoxEdit m_cmbPrefix;
        internal DevExpress.XtraEditors.LabelControl labelControl5;
        internal DevExpress.XtraEditors.LabelControl labelControl6;
        internal DevExpress.XtraEditors.LabelControl m_lblArea;
        internal DevExpress.XtraEditors.SimpleButton m_btnMapscoLookup;
        internal DevExpress.XtraEditors.LabelControl m_lblAddressTip;
    }
}