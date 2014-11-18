namespace Dalworth.Windows.StartDay.LoadEquipment
{
    partial class LoadEquipmentView
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_txtEquipmentNotes = new System.Windows.Forms.TextBox();
            this.m_table = new Dalworth.Controls.Table();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_txtEquipmentNotes);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 218);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 50);
            // 
            // m_txtEquipmentNotes
            // 
            this.m_txtEquipmentNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtEquipmentNotes.Location = new System.Drawing.Point(0, 0);
            this.m_txtEquipmentNotes.Multiline = true;
            this.m_txtEquipmentNotes.Name = "m_txtEquipmentNotes";
            this.m_txtEquipmentNotes.ReadOnly = true;
            this.m_txtEquipmentNotes.Size = new System.Drawing.Size(240, 50);
            this.m_txtEquipmentNotes.TabIndex = 0;
            // 
            // m_table
            // 
            this.m_table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_table.Location = new System.Drawing.Point(0, 0);
            this.m_table.Name = "m_table";
            this.m_table.Size = new System.Drawing.Size(240, 218);
            this.m_table.TabIndex = 1;
            this.m_table.Text = "table1";
            // 
            // LoadEquipmentView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_table);
            this.Controls.Add(this.panel1);
            this.Name = "LoadEquipmentView";
            this.Size = new System.Drawing.Size(240, 268);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.TextBox m_txtEquipmentNotes;
        internal Dalworth.Controls.Table m_table;
    }
}
