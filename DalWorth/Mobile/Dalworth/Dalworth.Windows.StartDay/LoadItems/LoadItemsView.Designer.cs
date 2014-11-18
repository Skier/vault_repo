namespace Dalworth.Windows.StartDay.LoadItems
{
    partial class LoadItemsView
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
            this.m_table = new Dalworth.Controls.Table();
            this.SuspendLayout();
            // 
            // m_table
            // 
            this.m_table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_table.Location = new System.Drawing.Point(0, 0);
            this.m_table.MultipleSelection = true;
            this.m_table.Name = "m_table";
            this.m_table.Size = new System.Drawing.Size(240, 268);
            this.m_table.TabIndex = 0;
            this.m_table.Text = "table1";
            // 
            // LoadItemsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_table);
            this.Name = "LoadItemsView";
            this.Size = new System.Drawing.Size(240, 268);
            this.ResumeLayout(false);

        }

        #endregion

        internal Dalworth.Controls.Table m_table;

    }
}
