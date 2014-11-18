namespace QuickBooksAgent.Windows.UI.Controls.Menu
{
    partial class MenuManager
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
            this.m_vScrollBar = new System.Windows.Forms.VScrollBar();
            this.SuspendLayout();
            // 
            // m_vScrollBar
            // 
            this.m_vScrollBar.Location = new System.Drawing.Point(0, 0);
            this.m_vScrollBar.Name = "m_vScrollBar";
            this.m_vScrollBar.Size = new System.Drawing.Size(13, 100);
            this.m_vScrollBar.TabIndex = 0;
            // 
            // MenuManager
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar m_vScrollBar;
    }
    
}
