namespace dalworth.preview
{
    partial class Tools
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
            this.m_btnCalculators = new MobileTech.Windows.UI.Controls.MenuButton();
            this.SuspendLayout();
            // 
            // m_btnCalculators
            // 
            this.m_btnCalculators.Location = new System.Drawing.Point(3, 3);
            this.m_btnCalculators.Name = "m_btnCalculators";
            this.m_btnCalculators.ShowBorder = false;
            this.m_btnCalculators.Size = new System.Drawing.Size(114, 68);
            this.m_btnCalculators.TabIndex = 1;
            this.m_btnCalculators.Text = "Calculators";
            // 
            // Tools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.m_btnCalculators);
            this.Menu = this.mainMenu1;
            this.Name = "Tools";
            this.Text = "0300 Tools";
            this.ResumeLayout(false);

        }

        #endregion

        private MobileTech.Windows.UI.Controls.MenuButton m_btnCalculators;
    }
}