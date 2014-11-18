namespace dalworth.preview
{
    partial class JobHistory
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
            this.m_table = new MobileTech.Windows.UI.Controls.Table();
            this.m_menuCancel = new System.Windows.Forms.MenuItem();
            this.m_menuDetails = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.m_menuCancel);
            this.mainMenu1.MenuItems.Add(this.m_menuDetails);
            // 
            // m_table
            // 
            this.m_table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_table.Location = new System.Drawing.Point(0, 0);
            this.m_table.Name = "m_table";
            this.m_table.Size = new System.Drawing.Size(240, 268);
            this.m_table.TabIndex = 0;
            this.m_table.Text = "m_table";
            // 
            // m_menuCancel
            // 
            this.m_menuCancel.Text = "Cancel";
            this.m_menuCancel.Click += new System.EventHandler(this.OnCancelClick);
            // 
            // m_menuDetails
            // 
            this.m_menuDetails.Text = "Details";
            this.m_menuDetails.Click += new System.EventHandler(this.OnDetailsClick);
            // 
            // JobHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.m_table);
            this.Menu = this.mainMenu1;
            this.Name = "JobHistory";
            this.Text = "0510 Job History";
            this.ResumeLayout(false);

        }

        #endregion

        private MobileTech.Windows.UI.Controls.Table m_table;
        private System.Windows.Forms.MenuItem m_menuCancel;
        private System.Windows.Forms.MenuItem m_menuDetails;
    }
}