using TextBoxReadOnly=dalworth.controls.TextBoxReadOnly;

namespace dalworth.preview
{
    partial class TicketInfo
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
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.m_menuArrived = new System.Windows.Forms.MenuItem();
            this.m_txtAddress = new dalworth.controls.TextBoxReadOnly();
            this.m_lblTicketNumber = new System.Windows.Forms.Label();
            this.m_lblJobType = new System.Windows.Forms.Label();
            this.m_lblMap = new System.Windows.Forms.Label();
            this.m_lblDate = new System.Windows.Forms.Label();
            this.m_lblCustomerName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txtNotes = new System.Windows.Forms.TextBox();
            this.m_linkPhone1 = new System.Windows.Forms.LinkLabel();
            this.m_linkPhone2 = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            this.mainMenu1.MenuItems.Add(this.m_menuArrived);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Menu";
            // 
            // m_menuArrived
            // 
            this.m_menuArrived.Text = "Arrived";
            this.m_menuArrived.Click += new System.EventHandler(this.OnArrivedClick);
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
            this.m_txtAddress.Size = new System.Drawing.Size(234, 49);
            this.m_txtAddress.TabIndex = 3;
            this.m_txtAddress.Text = "1234 Main Street, Plano, TX 75025";
            // 
            // m_lblTicketNumber
            // 
            this.m_lblTicketNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblTicketNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblTicketNumber.Location = new System.Drawing.Point(119, 106);
            this.m_lblTicketNumber.Name = "m_lblTicketNumber";
            this.m_lblTicketNumber.Size = new System.Drawing.Size(118, 20);
            this.m_lblTicketNumber.Text = "TKT: 1001";
            this.m_lblTicketNumber.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_lblJobType
            // 
            this.m_lblJobType.Location = new System.Drawing.Point(3, 106);
            this.m_lblJobType.Name = "m_lblJobType";
            this.m_lblJobType.Size = new System.Drawing.Size(110, 20);
            this.m_lblJobType.Text = "Rug Pickup";
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
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 20);
            this.label1.Text = "Notes:";
            // 
            // m_txtNotes
            // 
            this.m_txtNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtNotes.Location = new System.Drawing.Point(3, 140);
            this.m_txtNotes.Multiline = true;
            this.m_txtNotes.Name = "m_txtNotes";
            this.m_txtNotes.ReadOnly = true;
            this.m_txtNotes.Size = new System.Drawing.Size(234, 39);
            this.m_txtNotes.TabIndex = 15;
            this.m_txtNotes.Text = "Pick up 2 rugs. Cust would like 3-5 time frame. Please call in AM to confirm.";
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
            this.panel1.Location = new System.Drawing.Point(4, 89);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(109, 20);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.m_linkPhone2);
            this.panel2.Location = new System.Drawing.Point(119, 89);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(109, 20);
            // 
            // TicketInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_txtNotes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_txtAddress);
            this.Controls.Add(this.m_lblCustomerName);
            this.Controls.Add(this.m_lblDate);
            this.Controls.Add(this.m_lblMap);
            this.Controls.Add(this.m_lblJobType);
            this.Controls.Add(this.m_lblTicketNumber);
            this.Menu = this.mainMenu1;
            this.Name = "TicketInfo";
            this.Text = "0210 Ticket Info";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem m_menuArrived;
        private TextBoxReadOnly m_txtAddress;
        private System.Windows.Forms.Label m_lblTicketNumber;
        private System.Windows.Forms.Label m_lblJobType;
        private System.Windows.Forms.Label m_lblMap;
        private System.Windows.Forms.Label m_lblDate;
        private System.Windows.Forms.Label m_lblCustomerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_txtNotes;
        private System.Windows.Forms.LinkLabel m_linkPhone1;
        private System.Windows.Forms.LinkLabel m_linkPhone2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.MenuItem menuItem2;
    }
}