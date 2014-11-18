namespace Dalworth.Server.MainForm.Dashboard
{
    partial class DateNavigatorToolbox
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
            this.m_dateNavigator = new DevExpress.XtraScheduler.DateNavigator();
            ((System.ComponentModel.ISupportInitialize)(this.m_dateNavigator)).BeginInit();
            this.SuspendLayout();
            // 
            // m_dateNavigator
            // 
            this.m_dateNavigator.DateTime = new System.DateTime(2007, 9, 10, 0, 0, 0, 0);
            this.m_dateNavigator.Location = new System.Drawing.Point(0, 0);
            this.m_dateNavigator.Name = "m_dateNavigator";
            this.m_dateNavigator.Size = new System.Drawing.Size(179, 165);
            this.m_dateNavigator.TabIndex = 0;
            this.m_dateNavigator.View = DevExpress.XtraEditors.Controls.DateEditCalendarViewType.MonthInfo;
            // 
            // DateNavigatorToolbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(178, 164);
            this.Controls.Add(this.m_dateNavigator);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DateNavigatorToolbox";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.m_dateNavigator)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraScheduler.DateNavigator m_dateNavigator;
    }
}