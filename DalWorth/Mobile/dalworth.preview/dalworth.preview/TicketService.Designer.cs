namespace dalworth.preview
{
    partial class TicketService
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.m_menuAddRug = new System.Windows.Forms.MenuItem();
            this.m_menuEditRug = new System.Windows.Forms.MenuItem();
            this.m_menuViewRug = new System.Windows.Forms.MenuItem();
            this.m_menuDeleteRug = new System.Windows.Forms.MenuItem();
            this.m_menuRugSeparator = new System.Windows.Forms.MenuItem();
            this.m_menuSubmitETC = new System.Windows.Forms.MenuItem();
            this.m_menuNoGo = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.m_menuComplete = new System.Windows.Forms.MenuItem();
            this.m_tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_linkPhone2 = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_linkPhone1 = new System.Windows.Forms.LinkLabel();
            this.m_txtNotes = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtAddress = new dalworth.controls.TextBoxReadOnly();
            this.m_lblCustomerName = new System.Windows.Forms.Label();
            this.m_lblDate = new System.Windows.Forms.Label();
            this.m_lblMap = new System.Windows.Forms.Label();
            this.m_lblJobType = new System.Windows.Forms.Label();
            this.m_lblTicketNumber = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.m_txtMessage = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.m_table = new MobileTech.Windows.UI.Controls.Table();
            this.panel3 = new System.Windows.Forms.Panel();
            this.m_lblJobTotal = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.m_txtUserNotes = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_lblJobTax = new System.Windows.Forms.Label();
            this.m_lblJobSubTotal = new System.Windows.Forms.Label();
            this.m_tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = " ";
            // 
            // menuItem2
            // 
            this.menuItem2.MenuItems.Add(this.m_menuAddRug);
            this.menuItem2.MenuItems.Add(this.m_menuEditRug);
            this.menuItem2.MenuItems.Add(this.m_menuViewRug);
            this.menuItem2.MenuItems.Add(this.m_menuDeleteRug);
            this.menuItem2.MenuItems.Add(this.m_menuRugSeparator);
            this.menuItem2.MenuItems.Add(this.m_menuSubmitETC);
            this.menuItem2.MenuItems.Add(this.m_menuNoGo);
            this.menuItem2.MenuItems.Add(this.menuItem5);
            this.menuItem2.MenuItems.Add(this.m_menuComplete);
            this.menuItem2.Text = "Menu";
            // 
            // m_menuAddRug
            // 
            this.m_menuAddRug.Text = "Add Rug";
            this.m_menuAddRug.Click += new System.EventHandler(this.OnAddRugClick);
            // 
            // m_menuEditRug
            // 
            this.m_menuEditRug.Text = "Edit Rug";
            this.m_menuEditRug.Click += new System.EventHandler(this.OnEditRugClick);
            // 
            // m_menuViewRug
            // 
            this.m_menuViewRug.Text = "View Rug";
            this.m_menuViewRug.Click += new System.EventHandler(this.OnViewRugClick);
            // 
            // m_menuDeleteRug
            // 
            this.m_menuDeleteRug.Text = "Delete Rug";
            this.m_menuDeleteRug.Click += new System.EventHandler(this.OnDeleteRugClick);
            // 
            // m_menuRugSeparator
            // 
            this.m_menuRugSeparator.Text = "-";
            // 
            // m_menuSubmitETC
            // 
            this.m_menuSubmitETC.Text = "Submit ETC";
            this.m_menuSubmitETC.Click += new System.EventHandler(this.OnSubmitETCClick);
            // 
            // m_menuNoGo
            // 
            this.m_menuNoGo.Text = "No Go";
            this.m_menuNoGo.Click += new System.EventHandler(this.OnNoGoClick);
            // 
            // menuItem5
            // 
            this.menuItem5.Text = "-";
            // 
            // m_menuComplete
            // 
            this.m_menuComplete.Text = "Complete";
            this.m_menuComplete.Click += new System.EventHandler(this.OnCompleteClick);
            // 
            // m_tabs
            // 
            this.m_tabs.Controls.Add(this.tabPage1);
            this.m_tabs.Controls.Add(this.tabPage2);
            this.m_tabs.Controls.Add(this.tabPage4);
            this.m_tabs.Controls.Add(this.tabPage3);
            this.m_tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tabs.Location = new System.Drawing.Point(0, 0);
            this.m_tabs.Name = "m_tabs";
            this.m_tabs.SelectedIndex = 0;
            this.m_tabs.Size = new System.Drawing.Size(240, 268);
            this.m_tabs.TabIndex = 0;
            this.m_tabs.SelectedIndexChanged += new System.EventHandler(this.OnTabChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.m_txtNotes);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.m_txtAddress);
            this.tabPage1.Controls.Add(this.m_lblCustomerName);
            this.tabPage1.Controls.Add(this.m_lblDate);
            this.tabPage1.Controls.Add(this.m_lblMap);
            this.tabPage1.Controls.Add(this.m_lblJobType);
            this.tabPage1.Controls.Add(this.m_lblTicketNumber);
            this.tabPage1.Location = new System.Drawing.Point(0, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(240, 245);
            this.tabPage1.Text = "Info";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.m_linkPhone2);
            this.panel2.Location = new System.Drawing.Point(119, 76);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(109, 20);
            // 
            // m_linkPhone2
            // 
            this.m_linkPhone2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_linkPhone2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Underline);
            this.m_linkPhone2.Location = new System.Drawing.Point(0, 0);
            this.m_linkPhone2.Name = "m_linkPhone2";
            this.m_linkPhone2.Size = new System.Drawing.Size(109, 20);
            this.m_linkPhone2.TabIndex = 27;
            this.m_linkPhone2.Text = "BS (972) 471-9223";
            this.m_linkPhone2.Click += new System.EventHandler(this.OnPhone2Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_linkPhone1);
            this.panel1.Location = new System.Drawing.Point(4, 76);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(109, 20);
            // 
            // m_linkPhone1
            // 
            this.m_linkPhone1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_linkPhone1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Underline);
            this.m_linkPhone1.Location = new System.Drawing.Point(0, 0);
            this.m_linkPhone1.Name = "m_linkPhone1";
            this.m_linkPhone1.Size = new System.Drawing.Size(109, 20);
            this.m_linkPhone1.TabIndex = 26;
            this.m_linkPhone1.Text = "HM (972) 314-6984";
            this.m_linkPhone1.Click += new System.EventHandler(this.OnPhone1Click);
            // 
            // m_txtNotes
            // 
            this.m_txtNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtNotes.Location = new System.Drawing.Point(3, 127);
            this.m_txtNotes.Multiline = true;
            this.m_txtNotes.Name = "m_txtNotes";
            this.m_txtNotes.ReadOnly = true;
            this.m_txtNotes.Size = new System.Drawing.Size(234, 37);
            this.m_txtNotes.TabIndex = 27;
            this.m_txtNotes.Text = "Pick up 2 rugs. Cust would like 3-5 time frame. Please call in AM to confirm.";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 20);
            this.label1.Text = "Notes:";
            // 
            // m_txtAddress
            // 
            this.m_txtAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtAddress.BackColor = System.Drawing.SystemColors.Control;
            this.m_txtAddress.ForeColor = System.Drawing.Color.Red;
            this.m_txtAddress.Location = new System.Drawing.Point(3, 37);
            this.m_txtAddress.Multiline = true;
            this.m_txtAddress.Name = "m_txtAddress";
            this.m_txtAddress.Size = new System.Drawing.Size(234, 37);
            this.m_txtAddress.TabIndex = 26;
            this.m_txtAddress.Text = "1234 Main Street, Plano, TX 75025";
            // 
            // m_lblCustomerName
            // 
            this.m_lblCustomerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblCustomerName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblCustomerName.Location = new System.Drawing.Point(3, 19);
            this.m_lblCustomerName.Name = "m_lblCustomerName";
            this.m_lblCustomerName.Size = new System.Drawing.Size(234, 20);
            this.m_lblCustomerName.Text = "Love, Rob";
            // 
            // m_lblDate
            // 
            this.m_lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblDate.Location = new System.Drawing.Point(3, 3);
            this.m_lblDate.Name = "m_lblDate";
            this.m_lblDate.Size = new System.Drawing.Size(149, 20);
            this.m_lblDate.Text = "Sat, Apr 14, 2007";
            // 
            // m_lblMap
            // 
            this.m_lblMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblMap.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblMap.ForeColor = System.Drawing.Color.Red;
            this.m_lblMap.Location = new System.Drawing.Point(158, 3);
            this.m_lblMap.Name = "m_lblMap";
            this.m_lblMap.Size = new System.Drawing.Size(79, 20);
            this.m_lblMap.Text = "MAP: 45Q";
            this.m_lblMap.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblJobType
            // 
            this.m_lblJobType.Location = new System.Drawing.Point(3, 93);
            this.m_lblJobType.Name = "m_lblJobType";
            this.m_lblJobType.Size = new System.Drawing.Size(110, 20);
            this.m_lblJobType.Text = "Rug Pickup";
            // 
            // m_lblTicketNumber
            // 
            this.m_lblTicketNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblTicketNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblTicketNumber.Location = new System.Drawing.Point(119, 93);
            this.m_lblTicketNumber.Name = "m_lblTicketNumber";
            this.m_lblTicketNumber.Size = new System.Drawing.Size(118, 20);
            this.m_lblTicketNumber.Text = "TKT: 1001";
            this.m_lblTicketNumber.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_txtMessage);
            this.tabPage2.Location = new System.Drawing.Point(0, 0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(232, 242);
            this.tabPage2.Text = "Message";
            // 
            // m_txtMessage
            // 
            this.m_txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtMessage.Location = new System.Drawing.Point(0, 0);
            this.m_txtMessage.Multiline = true;
            this.m_txtMessage.Name = "m_txtMessage";
            this.m_txtMessage.ReadOnly = true;
            this.m_txtMessage.Size = new System.Drawing.Size(232, 242);
            this.m_txtMessage.TabIndex = 3;
            this.m_txtMessage.Text = "Be careful - evil dog wandering outside\r\nPlease call before come in";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.panel4);
            this.tabPage4.Controls.Add(this.panel3);
            this.tabPage4.Location = new System.Drawing.Point(0, 0);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(240, 245);
            this.tabPage4.Text = "Rug Pickup";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.m_table);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(240, 188);
            // 
            // m_table
            // 
            this.m_table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_table.Location = new System.Drawing.Point(0, 0);
            this.m_table.MultipleSelection = true;
            this.m_table.Name = "m_table";
            this.m_table.Size = new System.Drawing.Size(240, 188);
            this.m_table.TabIndex = 0;
            this.m_table.Text = "table1";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.m_lblJobSubTotal);
            this.panel3.Controls.Add(this.m_lblJobTax);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.m_lblJobTotal);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 188);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(240, 57);
            // 
            // m_lblJobTotal
            // 
            this.m_lblJobTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblJobTotal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblJobTotal.Location = new System.Drawing.Point(65, 37);
            this.m_lblJobTotal.Name = "m_lblJobTotal";
            this.m_lblJobTotal.Size = new System.Drawing.Size(172, 16);
            this.m_lblJobTotal.Tag = "";
            this.m_lblJobTotal.Text = "$50.00";
            this.m_lblJobTotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Location = new System.Drawing.Point(3, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 16);
            this.label2.Tag = "";
            this.label2.Text = "Job Total:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.m_txtUserNotes);
            this.tabPage3.Location = new System.Drawing.Point(0, 0);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(232, 242);
            this.tabPage3.Text = "Notes";
            // 
            // m_txtUserNotes
            // 
            this.m_txtUserNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtUserNotes.Location = new System.Drawing.Point(0, 0);
            this.m_txtUserNotes.Multiline = true;
            this.m_txtUserNotes.Name = "m_txtUserNotes";
            this.m_txtUserNotes.Size = new System.Drawing.Size(232, 242);
            this.m_txtUserNotes.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 16);
            this.label3.Tag = "";
            this.label3.Text = "Job Subtotal:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.Location = new System.Drawing.Point(3, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 16);
            this.label4.Tag = "";
            this.label4.Text = "Tax:";
            // 
            // m_lblJobTax
            // 
            this.m_lblJobTax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblJobTax.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblJobTax.Location = new System.Drawing.Point(76, 19);
            this.m_lblJobTax.Name = "m_lblJobTax";
            this.m_lblJobTax.Size = new System.Drawing.Size(161, 16);
            this.m_lblJobTax.Tag = "";
            this.m_lblJobTax.Text = "$50.00";
            this.m_lblJobTax.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblJobSubTotal
            // 
            this.m_lblJobSubTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblJobSubTotal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblJobSubTotal.Location = new System.Drawing.Point(82, 3);
            this.m_lblJobSubTotal.Name = "m_lblJobSubTotal";
            this.m_lblJobSubTotal.Size = new System.Drawing.Size(155, 16);
            this.m_lblJobSubTotal.Tag = "";
            this.m_lblJobSubTotal.Text = "$50.00";
            this.m_lblJobSubTotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TicketService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.m_tabs);
            this.Menu = this.mainMenu1;
            this.Name = "TicketService";
            this.Text = "0230 Ticket Service";
            this.m_tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem m_menuSubmitETC;
        private System.Windows.Forms.MenuItem m_menuNoGo;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem m_menuComplete;
        private System.Windows.Forms.TabControl m_tabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox m_txtMessage;
        private System.Windows.Forms.TextBox m_txtUserNotes;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox m_txtNotes;
        private System.Windows.Forms.Label label1;
        private dalworth.controls.TextBoxReadOnly m_txtAddress;
        private System.Windows.Forms.Label m_lblCustomerName;
        private System.Windows.Forms.Label m_lblDate;
        private System.Windows.Forms.Label m_lblMap;
        private System.Windows.Forms.Label m_lblJobType;
        private System.Windows.Forms.Label m_lblTicketNumber;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.LinkLabel m_linkPhone2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel m_linkPhone1;
        private System.Windows.Forms.MenuItem m_menuAddRug;
        private System.Windows.Forms.MenuItem m_menuEditRug;
        private System.Windows.Forms.MenuItem m_menuDeleteRug;
        private System.Windows.Forms.MenuItem m_menuRugSeparator;
        private System.Windows.Forms.Panel panel4;
        private MobileTech.Windows.UI.Controls.Table m_table;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuItem m_menuViewRug;
        private System.Windows.Forms.Label m_lblJobTotal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label m_lblJobSubTotal;
        private System.Windows.Forms.Label m_lblJobTax;
    }
}