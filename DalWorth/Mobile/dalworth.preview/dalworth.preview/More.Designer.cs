namespace dalworth.preview
{
    partial class More
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
            this.m_btnMessageToDispatch = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_btnJobHistory = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_btnTools = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_btnMessageToTechnician = new MobileTech.Windows.UI.Controls.MenuButton();
            this.SuspendLayout();
            // 
            // m_btnMessageToDispatch
            // 
            this.m_btnMessageToDispatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_btnMessageToDispatch.Location = new System.Drawing.Point(3, 197);
            this.m_btnMessageToDispatch.Name = "m_btnMessageToDispatch";
            this.m_btnMessageToDispatch.ShowBorder = false;
            this.m_btnMessageToDispatch.Size = new System.Drawing.Size(114, 68);
            this.m_btnMessageToDispatch.TabIndex = 6;
            this.m_btnMessageToDispatch.Text = "Msg to Dispatch";
            this.m_btnMessageToDispatch.Click += new System.EventHandler(this.OnMessageToDispatchClick);
            // 
            // m_btnJobHistory
            // 
            this.m_btnJobHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnJobHistory.Location = new System.Drawing.Point(123, 3);
            this.m_btnJobHistory.Name = "m_btnJobHistory";
            this.m_btnJobHistory.ShowBorder = false;
            this.m_btnJobHistory.Size = new System.Drawing.Size(114, 68);
            this.m_btnJobHistory.TabIndex = 5;
            this.m_btnJobHistory.Text = "Job History";
            this.m_btnJobHistory.Click += new System.EventHandler(this.OnJobHistoryClick);
            // 
            // m_btnTools
            // 
            this.m_btnTools.Location = new System.Drawing.Point(3, 3);
            this.m_btnTools.Name = "m_btnTools";
            this.m_btnTools.ShowBorder = false;
            this.m_btnTools.Size = new System.Drawing.Size(114, 68);
            this.m_btnTools.TabIndex = 4;
            this.m_btnTools.Text = "Tools";
            this.m_btnTools.Click += new System.EventHandler(this.OnToolsClick);
            // 
            // m_btnMessageToTechnician
            // 
            this.m_btnMessageToTechnician.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnMessageToTechnician.Location = new System.Drawing.Point(123, 197);
            this.m_btnMessageToTechnician.Name = "m_btnMessageToTechnician";
            this.m_btnMessageToTechnician.ShowBorder = false;
            this.m_btnMessageToTechnician.Size = new System.Drawing.Size(114, 68);
            this.m_btnMessageToTechnician.TabIndex = 7;
            this.m_btnMessageToTechnician.Text = "Msg to Technician";
            this.m_btnMessageToTechnician.Click += new System.EventHandler(this.OnMessageToTechnicianClick);
            // 
            // More
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.m_btnMessageToTechnician);
            this.Controls.Add(this.m_btnMessageToDispatch);
            this.Controls.Add(this.m_btnJobHistory);
            this.Controls.Add(this.m_btnTools);
            this.Menu = this.mainMenu1;
            this.Name = "More";
            this.Text = "0500 - More";
            this.ResumeLayout(false);

        }

        #endregion

        private MobileTech.Windows.UI.Controls.MenuButton m_btnMessageToDispatch;
        private MobileTech.Windows.UI.Controls.MenuButton m_btnJobHistory;
        private MobileTech.Windows.UI.Controls.MenuButton m_btnTools;
        private MobileTech.Windows.UI.Controls.MenuButton m_btnMessageToTechnician;
    }
}