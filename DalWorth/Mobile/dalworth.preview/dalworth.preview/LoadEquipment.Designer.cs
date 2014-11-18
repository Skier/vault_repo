namespace dalworth.preview
{
    partial class LoadEquipment
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
            this.m_menuBack = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.m_menuNext = new System.Windows.Forms.MenuItem();
            this.m_table = new MobileTech.Windows.UI.Controls.Table();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_txtNotes = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.m_menuNext);
            // 
            // m_menuBack
            // 
            this.m_menuBack.Text = "Back";
            this.m_menuBack.Click += new System.EventHandler(this.OnBackClick);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.m_menuBack);
            this.menuItem1.Text = "Menu";
            // 
            // m_menuNext
            // 
            this.m_menuNext.Text = "Next";
            this.m_menuNext.Click += new System.EventHandler(this.OnNextClick);
            // 
            // m_table
            // 
            this.m_table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_table.Location = new System.Drawing.Point(0, 0);
            this.m_table.MultipleSelection = false;
            this.m_table.Name = "m_table";
            this.m_table.Size = new System.Drawing.Size(240, 268);
            this.m_table.TabIndex = 0;
            this.m_table.Text = "table1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_txtNotes);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 224);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 44);
            // 
            // m_txtNotes
            // 
            this.m_txtNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtNotes.Location = new System.Drawing.Point(0, 0);
            this.m_txtNotes.Multiline = true;
            this.m_txtNotes.Name = "m_txtNotes";
            this.m_txtNotes.ReadOnly = true;
            this.m_txtNotes.Size = new System.Drawing.Size(240, 44);
            this.m_txtNotes.TabIndex = 0;
            this.m_txtNotes.Text = "3 brooms\r\n5 floor-clothes";
            // 
            // LoadEquipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_table);
            this.Menu = this.mainMenu1;
            this.Name = "LoadEquipment";
            this.Text = "0120 Load Equipment";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem m_menuBack;
        private System.Windows.Forms.MenuItem m_menuNext;
        private MobileTech.Windows.UI.Controls.Table m_table;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox m_txtNotes;
        private System.Windows.Forms.MenuItem menuItem1;
    }
}