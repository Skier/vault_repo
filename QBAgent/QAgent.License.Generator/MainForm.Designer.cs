namespace QAgent.License.Generator
{
    partial class MainForm
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
            this.m_btnGenerate = new System.Windows.Forms.Button();
            this.m_txtFirstName = new System.Windows.Forms.TextBox();
            this.m_txtLastName = new System.Windows.Forms.TextBox();
            this.m_txtCompany = new System.Windows.Forms.TextBox();
            this.m_txtLicense = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_btnGenerate
            // 
            this.m_btnGenerate.Location = new System.Drawing.Point(214, 138);
            this.m_btnGenerate.Name = "m_btnGenerate";
            this.m_btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.m_btnGenerate.TabIndex = 0;
            this.m_btnGenerate.Text = "Generate";
            this.m_btnGenerate.UseVisualStyleBackColor = true;
            this.m_btnGenerate.Click += new System.EventHandler(this.OnGenerateClick);
            // 
            // m_txtFirstName
            // 
            this.m_txtFirstName.Location = new System.Drawing.Point(86, 6);
            this.m_txtFirstName.Name = "m_txtFirstName";
            this.m_txtFirstName.Size = new System.Drawing.Size(203, 20);
            this.m_txtFirstName.TabIndex = 1;
            // 
            // m_txtLastName
            // 
            this.m_txtLastName.Location = new System.Drawing.Point(86, 32);
            this.m_txtLastName.Name = "m_txtLastName";
            this.m_txtLastName.Size = new System.Drawing.Size(203, 20);
            this.m_txtLastName.TabIndex = 2;
            // 
            // m_txtCompany
            // 
            this.m_txtCompany.Location = new System.Drawing.Point(86, 58);
            this.m_txtCompany.Name = "m_txtCompany";
            this.m_txtCompany.Size = new System.Drawing.Size(203, 20);
            this.m_txtCompany.TabIndex = 3;
            // 
            // m_txtLicense
            // 
            this.m_txtLicense.Location = new System.Drawing.Point(86, 112);
            this.m_txtLicense.Name = "m_txtLicense";
            this.m_txtLicense.ReadOnly = true;
            this.m_txtLicense.Size = new System.Drawing.Size(203, 20);
            this.m_txtLicense.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "First Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Last Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Company";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "License Number";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 168);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_txtLicense);
            this.Controls.Add(this.m_txtCompany);
            this.Controls.Add(this.m_txtLastName);
            this.Controls.Add(this.m_txtFirstName);
            this.Controls.Add(this.m_btnGenerate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "QAgent License Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_btnGenerate;
        private System.Windows.Forms.TextBox m_txtFirstName;
        private System.Windows.Forms.TextBox m_txtLastName;
        private System.Windows.Forms.TextBox m_txtCompany;
        private System.Windows.Forms.TextBox m_txtLicense;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

