namespace MobileTech.Windows.UI.DatabaseManager
{
    partial class DatabaseManagerView
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
            this.m_progress = new System.Windows.Forms.ProgressBar();
            this.m_lbProgressMessage = new System.Windows.Forms.Label();
            this.m_btPopulateItems = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.m_txItemsCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_lbIndexItems = new System.Windows.Forms.LinkLabel();
            this.m_lbCreateRestore = new System.Windows.Forms.LinkLabel();
            this.m_linkImport = new System.Windows.Forms.LinkLabel();
            this.m_linkExport = new System.Windows.Forms.LinkLabel();
            this.m_chClear = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // m_progress
            // 
            this.m_progress.Location = new System.Drawing.Point(3, 245);
            this.m_progress.Name = "m_progress";
            this.m_progress.Size = new System.Drawing.Size(234, 20);
            // 
            // m_lbProgressMessage
            // 
            this.m_lbProgressMessage.Location = new System.Drawing.Point(4, 219);
            this.m_lbProgressMessage.Name = "m_lbProgressMessage";
            this.m_lbProgressMessage.Size = new System.Drawing.Size(233, 20);
            // 
            // m_btPopulateItems
            // 
            this.m_btPopulateItems.Location = new System.Drawing.Point(154, 30);
            this.m_btPopulateItems.Name = "m_btPopulateItems";
            this.m_btPopulateItems.Size = new System.Drawing.Size(73, 21);
            this.m_btPopulateItems.TabIndex = 2;
            this.m_btPopulateItems.Text = "Start";
            this.m_btPopulateItems.Click += new System.EventHandler(this.OnPopulateItemsClick);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(5, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "Populate items";
            // 
            // m_txItemsCount
            // 
            this.m_txItemsCount.Location = new System.Drawing.Point(56, 30);
            this.m_txItemsCount.Name = "m_txItemsCount";
            this.m_txItemsCount.Size = new System.Drawing.Size(70, 21);
            this.m_txItemsCount.TabIndex = 4;
            this.m_txItemsCount.Text = "20000";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(5, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 20);
            this.label2.Text = "Count:";
            // 
            // m_lbIndexItems
            // 
            this.m_lbIndexItems.Location = new System.Drawing.Point(5, 69);
            this.m_lbIndexItems.Name = "m_lbIndexItems";
            this.m_lbIndexItems.Size = new System.Drawing.Size(76, 20);
            this.m_lbIndexItems.TabIndex = 6;
            this.m_lbIndexItems.Text = "Index items";
            this.m_lbIndexItems.Click += new System.EventHandler(this.OnIndexItemsClick);
            // 
            // m_lbCreateRestore
            // 
            this.m_lbCreateRestore.Location = new System.Drawing.Point(5, 98);
            this.m_lbCreateRestore.Name = "m_lbCreateRestore";
            this.m_lbCreateRestore.Size = new System.Drawing.Size(156, 20);
            this.m_lbCreateRestore.TabIndex = 6;
            this.m_lbCreateRestore.Text = "Create / Restore Database";
            this.m_lbCreateRestore.Click += new System.EventHandler(this.OnCreateRestoreClick);
            // 
            // m_linkImport
            // 
            this.m_linkImport.Location = new System.Drawing.Point(5, 128);
            this.m_linkImport.Name = "m_linkImport";
            this.m_linkImport.Size = new System.Drawing.Size(100, 20);
            this.m_linkImport.TabIndex = 11;
            this.m_linkImport.Text = "Import";
            this.m_linkImport.Click += new System.EventHandler(this.m_linkImport_Click);
            // 
            // m_linkExport
            // 
            this.m_linkExport.Location = new System.Drawing.Point(5, 158);
            this.m_linkExport.Name = "m_linkExport";
            this.m_linkExport.Size = new System.Drawing.Size(100, 20);
            this.m_linkExport.TabIndex = 11;
            this.m_linkExport.Text = "Export";
            this.m_linkExport.Click += new System.EventHandler(this.m_linkExport_Click);
            // 
            // m_chClear
            // 
            this.m_chClear.Checked = true;
            this.m_chClear.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chClear.Location = new System.Drawing.Point(60, 125);
            this.m_chClear.Name = "m_chClear";
            this.m_chClear.Size = new System.Drawing.Size(137, 20);
            this.m_chClear.TabIndex = 16;
            this.m_chClear.Text = "Clear existing data";
            // 
            // DatabaseManagerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.m_chClear);
            this.Controls.Add(this.m_linkExport);
            this.Controls.Add(this.m_linkImport);
            this.Controls.Add(this.m_lbCreateRestore);
            this.Controls.Add(this.m_lbIndexItems);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_txItemsCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_btPopulateItems);
            this.Controls.Add(this.m_lbProgressMessage);
            this.Controls.Add(this.m_progress);
            this.Menu = this.mainMenu1;
            this.Name = "DatabaseManagerView";
            this.Text = "Database manager";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar m_progress;
        private System.Windows.Forms.Label m_lbProgressMessage;
        private System.Windows.Forms.Button m_btPopulateItems;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_txItemsCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel m_lbIndexItems;
        private System.Windows.Forms.LinkLabel m_lbCreateRestore;
        private System.Windows.Forms.LinkLabel m_linkImport;
        private System.Windows.Forms.LinkLabel m_linkExport;
        private System.Windows.Forms.CheckBox m_chClear;
    }
}