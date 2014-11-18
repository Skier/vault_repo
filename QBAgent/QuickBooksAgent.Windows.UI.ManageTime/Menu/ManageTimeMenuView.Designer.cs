namespace QuickBooksAgent.Windows.UI.ManageTime.Menu
{
    partial class ManageTimeMenuView
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
            this.m_mbSingleTimeSheet = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.m_mbWeeklyTimeSheet = new QuickBooksAgent.Windows.UI.Controls.MenuButton();
            this.SuspendLayout();
            // 
            // m_mbSingleTimeSheet
            // 
            this.m_mbSingleTimeSheet.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.m_mbSingleTimeSheet.Location = new System.Drawing.Point(5, 5);
            this.m_mbSingleTimeSheet.Name = "m_mbSingleTimeSheet";
            this.m_mbSingleTimeSheet.Picture = QuickBooksAgent.Windows.UI.ImageKeys.SingleTimeSheet;
            this.m_mbSingleTimeSheet.ShowBorder = true;
            this.m_mbSingleTimeSheet.Size = new System.Drawing.Size(115, 82);
            this.m_mbSingleTimeSheet.TabIndex = 1;
            this.m_mbSingleTimeSheet.Tag = "";
            this.m_mbSingleTimeSheet.Text = "Single Time Sheet";
            // 
            // m_mbWeeklyTimeSheet
            // 
            this.m_mbWeeklyTimeSheet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_mbWeeklyTimeSheet.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.m_mbWeeklyTimeSheet.Location = new System.Drawing.Point(122, 5);
            this.m_mbWeeklyTimeSheet.Name = "m_mbWeeklyTimeSheet";
            this.m_mbWeeklyTimeSheet.Picture = QuickBooksAgent.Windows.UI.ImageKeys.WeeklyTimeSheet;
            this.m_mbWeeklyTimeSheet.ShowBorder = true;
            this.m_mbWeeklyTimeSheet.Size = new System.Drawing.Size(115, 82);
            this.m_mbWeeklyTimeSheet.TabIndex = 2;
            this.m_mbWeeklyTimeSheet.Tag = "";
            this.m_mbWeeklyTimeSheet.Text = "Weekly Time Sheet";
            // 
            // ManageTimeMenuView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_mbWeeklyTimeSheet);
            this.Controls.Add(this.m_mbSingleTimeSheet);
            this.Name = "ManageTimeMenuView";
            this.Size = new System.Drawing.Size(240, 294);
            this.ResumeLayout(false);

        }

        #endregion

        internal QuickBooksAgent.Windows.UI.Controls.MenuButton m_mbSingleTimeSheet;
        internal QuickBooksAgent.Windows.UI.Controls.MenuButton m_mbWeeklyTimeSheet;
    }
}
