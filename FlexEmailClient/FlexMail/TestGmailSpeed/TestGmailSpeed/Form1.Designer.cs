namespace TestGmailSpeed
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtMsgCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbA2 = new System.Windows.Forms.RadioButton();
            this.rbA1 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblConnectTime = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblRetrieveTime = new System.Windows.Forms.Label();
            this.lblLoginTime = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Location = new System.Drawing.Point(12, 66);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Retrieve";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtLogin
            // 
            this.txtLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLogin.Location = new System.Drawing.Point(96, 10);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(100, 20);
            this.txtLogin.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "User Login :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password :";
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Location = new System.Drawing.Point(96, 39);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtMsgCount
            // 
            this.txtMsgCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMsgCount.Location = new System.Drawing.Point(96, 66);
            this.txtMsgCount.Name = "txtMsgCount";
            this.txtMsgCount.Size = new System.Drawing.Size(100, 20);
            this.txtMsgCount.TabIndex = 5;
            this.txtMsgCount.Text = "10";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(202, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "headers";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbA2);
            this.groupBox1.Controls.Add(this.rbA1);
            this.groupBox1.Location = new System.Drawing.Point(205, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(143, 76);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Reading Algorithm";
            // 
            // rbA2
            // 
            this.rbA2.AutoSize = true;
            this.rbA2.Location = new System.Drawing.Point(7, 44);
            this.rbA2.Name = "rbA2";
            this.rbA2.Size = new System.Drawing.Size(65, 17);
            this.rbA2.TabIndex = 1;
            this.rbA2.Text = "algorith2";
            this.rbA2.UseVisualStyleBackColor = true;
            // 
            // rbA1
            // 
            this.rbA1.AutoSize = true;
            this.rbA1.Checked = true;
            this.rbA1.Location = new System.Drawing.Point(7, 20);
            this.rbA1.Name = "rbA1";
            this.rbA1.Size = new System.Drawing.Size(65, 17);
            this.rbA1.TabIndex = 0;
            this.rbA1.TabStop = true;
            this.rbA1.Text = "algorith1";
            this.rbA1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblConnectTime);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lblRetrieveTime);
            this.groupBox2.Controls.Add(this.lblLoginTime);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 107);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(336, 89);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Result : Time Spent";
            // 
            // lblConnectTime
            // 
            this.lblConnectTime.AutoSize = true;
            this.lblConnectTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblConnectTime.Location = new System.Drawing.Point(153, 21);
            this.lblConnectTime.Name = "lblConnectTime";
            this.lblConnectTime.Size = new System.Drawing.Size(14, 13);
            this.lblConnectTime.TabIndex = 18;
            this.lblConnectTime.Text = "_";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "for Establish Connection :";
            // 
            // lblRetrieveTime
            // 
            this.lblRetrieveTime.AutoSize = true;
            this.lblRetrieveTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblRetrieveTime.Location = new System.Drawing.Point(153, 59);
            this.lblRetrieveTime.Name = "lblRetrieveTime";
            this.lblRetrieveTime.Size = new System.Drawing.Size(14, 13);
            this.lblRetrieveTime.TabIndex = 16;
            this.lblRetrieveTime.Text = "_";
            // 
            // lblLoginTime
            // 
            this.lblLoginTime.AutoSize = true;
            this.lblLoginTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLoginTime.Location = new System.Drawing.Point(153, 40);
            this.lblLoginTime.Name = "lblLoginTime";
            this.lblLoginTime.Size = new System.Drawing.Size(14, 13);
            this.lblLoginTime.TabIndex = 15;
            this.lblLoginTime.Text = "_";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "for Messages Retrieving :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "for Authentication :";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 209);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMsgCount);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.btnConnect);
            this.Name = "Form1";
            this.Text = "GMail speed testing";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtMsgCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbA2;
        private System.Windows.Forms.RadioButton rbA1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblRetrieveTime;
        private System.Windows.Forms.Label lblLoginTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblConnectTime;
    }
}

