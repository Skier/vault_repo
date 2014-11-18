namespace QuickBooksAgent.Windows.UI.ManageTime.Weekly
{
    partial class WeeklyTimeTrackingView
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
            this.m_pnlTop = new System.Windows.Forms.Panel();
            this.m_pnlWeek = new System.Windows.Forms.Panel();
            this.m_btnWeek = new System.Windows.Forms.Button();
            this.m_pnlNext = new System.Windows.Forms.Panel();
            this.m_btnNextWeek = new System.Windows.Forms.Button();
            this.m_pnlPrev = new System.Windows.Forms.Panel();
            this.m_btnPrevWeek = new System.Windows.Forms.Button();
            this.m_cmbPerson = new System.Windows.Forms.ComboBox();
            this.m_cmbPersonType = new System.Windows.Forms.ComboBox();
            this.m_table = new QuickBooksAgent.Windows.UI.Controls.Table();
            this.m_monthCalendar = new System.Windows.Forms.MonthCalendar();
            this.m_pnlMonthCalendar = new System.Windows.Forms.Panel();
            this.m_pnlTop.SuspendLayout();
            this.m_pnlWeek.SuspendLayout();
            this.m_pnlNext.SuspendLayout();
            this.m_pnlPrev.SuspendLayout();
            this.m_pnlMonthCalendar.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_pnlTop
            // 
            this.m_pnlTop.Controls.Add(this.m_pnlWeek);
            this.m_pnlTop.Controls.Add(this.m_pnlNext);
            this.m_pnlTop.Controls.Add(this.m_pnlPrev);
            this.m_pnlTop.Controls.Add(this.m_cmbPerson);
            this.m_pnlTop.Controls.Add(this.m_cmbPersonType);
            this.m_pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_pnlTop.Location = new System.Drawing.Point(0, 0);
            this.m_pnlTop.Name = "m_pnlTop";
            this.m_pnlTop.Size = new System.Drawing.Size(240, 48);
            // 
            // m_pnlWeek
            // 
            this.m_pnlWeek.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pnlWeek.Controls.Add(this.m_btnWeek);
            this.m_pnlWeek.Location = new System.Drawing.Point(37, 24);
            this.m_pnlWeek.Name = "m_pnlWeek";
            this.m_pnlWeek.Size = new System.Drawing.Size(166, 22);
            // 
            // m_btnWeek
            // 
            this.m_btnWeek.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnWeek.BackColor = System.Drawing.Color.White;
            this.m_btnWeek.Location = new System.Drawing.Point(0, 2);
            this.m_btnWeek.Name = "m_btnWeek";
            this.m_btnWeek.Size = new System.Drawing.Size(166, 20);
            this.m_btnWeek.TabIndex = 3;
            // 
            // m_pnlNext
            // 
            this.m_pnlNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pnlNext.Controls.Add(this.m_btnNextWeek);
            this.m_pnlNext.Location = new System.Drawing.Point(204, 24);
            this.m_pnlNext.Name = "m_pnlNext";
            this.m_pnlNext.Size = new System.Drawing.Size(33, 26);
            // 
            // m_btnNextWeek
            // 
            this.m_btnNextWeek.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnNextWeek.BackColor = System.Drawing.Color.White;
            this.m_btnNextWeek.Location = new System.Drawing.Point(0, 2);
            this.m_btnNextWeek.Name = "m_btnNextWeek";
            this.m_btnNextWeek.Size = new System.Drawing.Size(33, 20);
            this.m_btnNextWeek.TabIndex = 4;
            this.m_btnNextWeek.Text = ">>";
            // 
            // m_pnlPrev
            // 
            this.m_pnlPrev.Controls.Add(this.m_btnPrevWeek);
            this.m_pnlPrev.Location = new System.Drawing.Point(3, 24);
            this.m_pnlPrev.Name = "m_pnlPrev";
            this.m_pnlPrev.Size = new System.Drawing.Size(33, 22);
            // 
            // m_btnPrevWeek
            // 
            this.m_btnPrevWeek.BackColor = System.Drawing.Color.White;
            this.m_btnPrevWeek.Location = new System.Drawing.Point(0, 2);
            this.m_btnPrevWeek.Name = "m_btnPrevWeek";
            this.m_btnPrevWeek.Size = new System.Drawing.Size(33, 20);
            this.m_btnPrevWeek.TabIndex = 2;
            this.m_btnPrevWeek.Text = "<<";
            // 
            // m_cmbPerson
            // 
            this.m_cmbPerson.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cmbPerson.DisplayMember = "Name";
            this.m_cmbPerson.Location = new System.Drawing.Point(83, 3);
            this.m_cmbPerson.Name = "m_cmbPerson";
            this.m_cmbPerson.Size = new System.Drawing.Size(154, 22);
            this.m_cmbPerson.TabIndex = 1;
            // 
            // m_cmbPersonType
            // 
            this.m_cmbPersonType.Location = new System.Drawing.Point(3, 3);
            this.m_cmbPersonType.Name = "m_cmbPersonType";
            this.m_cmbPersonType.Size = new System.Drawing.Size(77, 22);
            this.m_cmbPersonType.TabIndex = 0;
            // 
            // m_table
            // 
            this.m_table.AllowColumnResize = false;
            this.m_table.AltBackColor = System.Drawing.Color.Linen;
            this.m_table.AltForeColor = System.Drawing.Color.Black;
            this.m_table.AutoColumnSize = true;
            this.m_table.AutoMoveRow = true;
            this.m_table.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.m_table.ColumnBackColor = System.Drawing.Color.LightGray;
            this.m_table.ColumnFont = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.m_table.ColumnForeColor = System.Drawing.Color.Black;
            this.m_table.DefaultLineAligment = System.Drawing.StringAlignment.Center;
            this.m_table.DefaultRowHeight = 20;
            this.m_table.DefaultTextAligment = System.Drawing.StringAlignment.Center;
            this.m_table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_table.DrawGridBorder = true;
            this.m_table.FocusCellBackColor = System.Drawing.Color.Black;
            this.m_table.FocusCellForeColor = System.Drawing.Color.White;
            this.m_table.GreyOut = false;
            this.m_table.LeftHeader = false;
            this.m_table.Location = new System.Drawing.Point(0, 48);
            this.m_table.MultipleSelection = false;
            this.m_table.Name = "m_table";
            this.m_table.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.m_table.SelectionForeColor = System.Drawing.Color.Black;
            this.m_table.ShowSplitterValue = true;
            this.m_table.ShowStartSplitter = true;
            this.m_table.Size = new System.Drawing.Size(240, 246);
            this.m_table.SplitterColor = System.Drawing.Color.Red;
            this.m_table.SplitterMode = QuickBooksAgent.Windows.UI.Controls.SplitterMode.Default;
            this.m_table.SplitterStartColor = System.Drawing.Color.Brown;
            this.m_table.SplitterWidth = 1;
            this.m_table.TabIndex = 5;
            this.m_table.Text = "table1";
            // 
            // m_monthCalendar
            // 
            this.m_monthCalendar.Location = new System.Drawing.Point(1, 0);
            this.m_monthCalendar.Name = "m_monthCalendar";
            this.m_monthCalendar.Size = new System.Drawing.Size(163, 149);
            this.m_monthCalendar.TabIndex = 4;
            // 
            // m_pnlMonthCalendar
            // 
            this.m_pnlMonthCalendar.BackColor = System.Drawing.Color.Black;
            this.m_pnlMonthCalendar.Controls.Add(this.m_monthCalendar);
            this.m_pnlMonthCalendar.Location = new System.Drawing.Point(37, 46);
            this.m_pnlMonthCalendar.Name = "m_pnlMonthCalendar";
            this.m_pnlMonthCalendar.Size = new System.Drawing.Size(166, 153);
            this.m_pnlMonthCalendar.Visible = false;
            // 
            // WeeklyTimeTrackingView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.m_pnlMonthCalendar);
            this.Controls.Add(this.m_table);
            this.Controls.Add(this.m_pnlTop);
            this.Name = "WeeklyTimeTrackingView";
            this.Size = new System.Drawing.Size(240, 294);
            this.m_pnlTop.ResumeLayout(false);
            this.m_pnlWeek.ResumeLayout(false);
            this.m_pnlNext.ResumeLayout(false);
            this.m_pnlPrev.ResumeLayout(false);
            this.m_pnlMonthCalendar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_pnlTop;
        internal System.Windows.Forms.ComboBox m_cmbPerson;
        internal System.Windows.Forms.ComboBox m_cmbPersonType;
        internal QuickBooksAgent.Windows.UI.Controls.Table m_table;
        internal System.Windows.Forms.Button m_btnPrevWeek;
        internal System.Windows.Forms.Button m_btnWeek;
        internal System.Windows.Forms.Button m_btnNextWeek;
        internal System.Windows.Forms.MonthCalendar m_monthCalendar;
        private System.Windows.Forms.Panel m_pnlWeek;
        private System.Windows.Forms.Panel m_pnlNext;
        private System.Windows.Forms.Panel m_pnlPrev;
        internal System.Windows.Forms.Panel m_pnlMonthCalendar;

    }
}
