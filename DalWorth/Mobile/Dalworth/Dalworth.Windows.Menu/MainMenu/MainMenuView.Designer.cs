using System.Drawing;

namespace Dalworth.Windows.Menu.MainMenu
{
    partial class MainMenuView
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
            this.m_btnStartDay = new Dalworth.Controls.Menu.MenuButton();
            this.m_timerIncome = new System.Windows.Forms.Timer();
            this.m_btnEndDay = new Dalworth.Controls.Menu.MenuButton();
            this.m_timerGps = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // m_btnStartDay
            // 
            this.m_btnStartDay.Location = new System.Drawing.Point(3, 3);
            this.m_btnStartDay.Name = "m_btnStartDay";
            this.m_btnStartDay.Size = new System.Drawing.Size(114, 68);
            this.m_btnStartDay.TabIndex = 0;
            this.m_btnStartDay.Text = "Start Day";
            // 
            // m_timerIncome
            // 
            this.m_timerIncome.Interval = 15000;
            // 
            // m_btnEndDay
            // 
            this.m_btnEndDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnEndDay.Location = new System.Drawing.Point(123, 3);
            this.m_btnEndDay.Name = "m_btnEndDay";
            this.m_btnEndDay.Size = new System.Drawing.Size(114, 68);
            this.m_btnEndDay.TabIndex = 1;
            this.m_btnEndDay.Text = "End Day";
            // 
            // MainMenuView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_btnEndDay);
            this.Controls.Add(this.m_btnStartDay);
            this.Name = "MainMenuView";
            this.Size = new System.Drawing.Size(240, 294);
            this.ResumeLayout(false);

        }

        #endregion

        internal Dalworth.Controls.Menu.MenuButton m_btnStartDay;
        internal System.Windows.Forms.Timer m_timerIncome;
        internal Dalworth.Controls.Menu.MenuButton m_btnEndDay;
        internal System.Windows.Forms.Timer m_timerGps;



    }
}