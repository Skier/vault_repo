namespace MobileTech.Windows.UI.FlagEditor
{
    partial class FlagEditorView
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
            this.label1 = new System.Windows.Forms.Label();
            this.menuButton1 = new MobileTech.Windows.UI.Controls.MenuButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_rbTypeCustomer = new System.Windows.Forms.RadioButton();
            this.m_rbTypeRoute = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_rbModeEditValues = new System.Windows.Forms.RadioButton();
            this.m_rbModeCollection = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "Flag type";
            // 
            // menuButton1
            // 
            this.menuButton1.BackDownColor = System.Drawing.Color.Black;
            this.menuButton1.ButtonShape = MobileTech.Windows.UI.Controls.Shape.Rectangle;
            this.menuButton1.ForeDownColor = System.Drawing.Color.White;
            this.menuButton1.IconMargin = 3;
            this.menuButton1.IconShift = false;
            this.menuButton1.IconTextSpace = 3;
            this.menuButton1.ImageDown = null;
            this.menuButton1.ImageUp = null;
            this.menuButton1.Location = new System.Drawing.Point(175, 231);
            this.menuButton1.Name = "menuButton1";
            this.menuButton1.Picture = MobileTech.Windows.UI.ImageKeys.None;
            this.menuButton1.PictureDisabled = MobileTech.Windows.UI.ImageKeys.None;
            this.menuButton1.PictureFocus = MobileTech.Windows.UI.ImageKeys.None;
            this.menuButton1.ShowBorder = true;
            this.menuButton1.ShowFocusBorder = true;
            this.menuButton1.Size = new System.Drawing.Size(54, 51);
            this.menuButton1.TabIndex = 1;
            this.menuButton1.Text = "Edit";
            this.menuButton1.TextShift = false;
            this.menuButton1.TransparentIcon = true;
            this.menuButton1.TransparentImage = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_rbTypeCustomer);
            this.panel1.Controls.Add(this.m_rbTypeRoute);
            this.panel1.Location = new System.Drawing.Point(12, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(217, 60);
            // 
            // m_rbTypeCustomer
            // 
            this.m_rbTypeCustomer.Location = new System.Drawing.Point(4, 30);
            this.m_rbTypeCustomer.Name = "m_rbTypeCustomer";
            this.m_rbTypeCustomer.Size = new System.Drawing.Size(100, 20);
            this.m_rbTypeCustomer.TabIndex = 1;
            this.m_rbTypeCustomer.TabStop = false;
            this.m_rbTypeCustomer.Text = "Customer";
            // 
            // m_rbTypeRoute
            // 
            this.m_rbTypeRoute.Checked = true;
            this.m_rbTypeRoute.Location = new System.Drawing.Point(4, 4);
            this.m_rbTypeRoute.Name = "m_rbTypeRoute";
            this.m_rbTypeRoute.Size = new System.Drawing.Size(100, 20);
            this.m_rbTypeRoute.TabIndex = 0;
            this.m_rbTypeRoute.Text = "Route";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_rbModeEditValues);
            this.panel2.Controls.Add(this.m_rbModeCollection);
            this.panel2.Location = new System.Drawing.Point(12, 119);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(217, 60);
            // 
            // m_rbModeEditValues
            // 
            this.m_rbModeEditValues.Location = new System.Drawing.Point(4, 30);
            this.m_rbModeEditValues.Name = "m_rbModeEditValues";
            this.m_rbModeEditValues.Size = new System.Drawing.Size(138, 20);
            this.m_rbModeEditValues.TabIndex = 1;
            this.m_rbModeEditValues.TabStop = false;
            this.m_rbModeEditValues.Text = "Edit assigned values";
            // 
            // m_rbModeCollection
            // 
            this.m_rbModeCollection.Checked = true;
            this.m_rbModeCollection.Location = new System.Drawing.Point(4, 4);
            this.m_rbModeCollection.Name = "m_rbModeCollection";
            this.m_rbModeCollection.Size = new System.Drawing.Size(163, 20);
            this.m_rbModeCollection.TabIndex = 0;
            this.m_rbModeCollection.Text = "Name/Value Collection";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(12, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Edit mode";
            // 
            // FlagEditorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuButton1);
            this.Controls.Add(this.label1);
            this.Name = "FlagEditorView";
            this.Text = "Flag editor";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private MobileTech.Windows.UI.Controls.MenuButton menuButton1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton m_rbTypeCustomer;
        private System.Windows.Forms.RadioButton m_rbTypeRoute;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton m_rbModeEditValues;
        private System.Windows.Forms.RadioButton m_rbModeCollection;
        private System.Windows.Forms.Label label2;
    }
}