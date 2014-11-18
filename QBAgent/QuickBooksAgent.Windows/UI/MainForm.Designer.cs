namespace QuickBooksAgent.Windows.UI
{
    partial class MainForm
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
            this.m_mainMenu = new System.Windows.Forms.MainMenu();
            this.m_defaultAction = new System.Windows.Forms.MenuItem();
            this.m_subMenu = new System.Windows.Forms.MenuItem();
            this.m_viewPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // m_mainMenu
            // 
            this.m_mainMenu.MenuItems.Add(this.m_defaultAction);
            this.m_mainMenu.MenuItems.Add(this.m_subMenu);
            // 
            // m_defaultAction
            // 
            this.m_defaultAction.Text = "Default Action";
            // 
            // m_subMenu
            // 
            this.m_subMenu.Text = "Menu";
            // 
            // m_viewPanel
            // 
            this.m_viewPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_viewPanel.Location = new System.Drawing.Point(0, 0);
            this.m_viewPanel.Name = "m_viewPanel";
            this.m_viewPanel.Size = new System.Drawing.Size(240, 268);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.m_viewPanel);
            this.Menu = this.m_mainMenu;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.MenuItem m_subMenu;
        internal System.Windows.Forms.MenuItem m_defaultAction;
        internal System.Windows.Forms.MainMenu m_mainMenu;
        internal System.Windows.Forms.Panel m_viewPanel;
    }
}