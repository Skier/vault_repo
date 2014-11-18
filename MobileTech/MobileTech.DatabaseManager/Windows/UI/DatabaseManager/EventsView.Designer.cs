namespace MobileTech.Windows.UI.DatabaseManager
{
    partial class EventsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventsView));
            this.m_mbBack = new MobileTech.Windows.UI.Controls.MenuButton();
            this.m_table = new MobileTech.Windows.UI.Controls.Table();
            this.m_images = new System.Windows.Forms.ImageList();
            this.SuspendLayout();
            // 
            // m_mbBack
            // 
            this.m_mbBack.Location = new System.Drawing.Point(192, 230);
            this.m_mbBack.Name = "m_mbBack";
            this.m_mbBack.Picture = MobileTech.Windows.UI.ImageKeys.Done_Small;
            this.m_mbBack.PictureDisabled = MobileTech.Windows.UI.ImageKeys.Done_SmallDisabled;
            this.m_mbBack.PictureFocus = MobileTech.Windows.UI.ImageKeys.Done_SmallFocus;
            this.m_mbBack.ShowBorder = true;
            this.m_mbBack.ShowFocusBorder = true;
            this.m_mbBack.Size = new System.Drawing.Size(48, 64);
            this.m_mbBack.TabIndex = 2;
            this.m_mbBack.Text = "Back";
            this.m_mbBack.Click += new System.EventHandler(this.OnBackClick);
            // 
            // m_table
            // 
            this.m_table.AllowColumnResize = false;
            this.m_table.AltBackColor = System.Drawing.Color.Linen;
            this.m_table.AltForeColor = System.Drawing.Color.Black;
            this.m_table.AutoColumnSize = true;
            this.m_table.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.m_table.ColumnBackColor = System.Drawing.Color.LightGray;
            this.m_table.ColumnFont = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.m_table.ColumnForeColor = System.Drawing.Color.Black;
            this.m_table.DefaultLineAligment = System.Drawing.StringAlignment.Center;
            this.m_table.DefaultRowHeight = 20;
            this.m_table.DefaultTextAligment = System.Drawing.StringAlignment.Center;
            this.m_table.FocusCellBackColor = System.Drawing.Color.Black;
            this.m_table.FocusCellForeColor = System.Drawing.Color.White;
            this.m_table.Location = new System.Drawing.Point(3, 4);
            this.m_table.Name = "m_table";
            this.m_table.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.m_table.SelectionForeColor = System.Drawing.Color.Black;
            this.m_table.ShowSplitterValue = true;
            this.m_table.ShowStartSplitter = true;
            this.m_table.Size = new System.Drawing.Size(234, 219);
            this.m_table.SplitterColor = System.Drawing.Color.Red;
            this.m_table.SplitterMode = MobileTech.Windows.UI.Controls.SplitterMode.Default;
            this.m_table.SplitterStartColor = System.Drawing.Color.Brown;
            this.m_table.SplitterWidth = 1;
            this.m_table.TabIndex = 1;
            this.m_images.Images.Clear();
            this.m_images.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
            this.m_images.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
            this.m_images.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
            // 
            // EventsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.m_table);
            this.Controls.Add(this.m_mbBack);
            this.Name = "EventsView";
            this.Text = "Events";
            this.ResumeLayout(false);

        }

        #endregion

        private MobileTech.Windows.UI.Controls.MenuButton m_mbBack;
        private MobileTech.Windows.UI.Controls.Table m_table;
        private System.Windows.Forms.ImageList m_images;
    }
}