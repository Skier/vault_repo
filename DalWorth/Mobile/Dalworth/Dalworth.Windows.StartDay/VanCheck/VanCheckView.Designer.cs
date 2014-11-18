using Dalworth.Controls;

namespace Dalworth.Windows.StartDay.VanCheck
{
    partial class VanCheckView
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
            this.m_txtSpecialNeeds = new System.Windows.Forms.TextBox();
            this.m_lblSpecialNeeds = new System.Windows.Forms.Label();
            this.m_txtOdometer = new DigitsEdit();
            this.m_txtHoursMeter = new DigitsEdit();
            this.m_lblOdometer = new System.Windows.Forms.Label();
            this.m_lblPumpReading = new System.Windows.Forms.Label();
            this.m_chkOilChecked = new System.Windows.Forms.CheckBox();
            this.m_chkVanClean = new System.Windows.Forms.CheckBox();
            this.m_chkUnitClean = new System.Windows.Forms.CheckBox();
            this.m_chkSuppliesStocked = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.m_lblOilChangeDue = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_txtSpecialNeeds
            // 
            this.m_txtSpecialNeeds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtSpecialNeeds.Location = new System.Drawing.Point(82, 110);
            this.m_txtSpecialNeeds.Multiline = true;
            this.m_txtSpecialNeeds.Name = "m_txtSpecialNeeds";
            this.m_txtSpecialNeeds.Size = new System.Drawing.Size(153, 42);
            this.m_txtSpecialNeeds.TabIndex = 0;
            // 
            // m_lblSpecialNeeds
            // 
            this.m_lblSpecialNeeds.ForeColor = System.Drawing.Color.Red;
            this.m_lblSpecialNeeds.Location = new System.Drawing.Point(1, 110);
            this.m_lblSpecialNeeds.Name = "m_lblSpecialNeeds";
            this.m_lblSpecialNeeds.Size = new System.Drawing.Size(80, 18);
            this.m_lblSpecialNeeds.Text = "Special Needs";
            // 
            // m_txtOdometer
            // 
            this.m_txtOdometer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtOdometer.Location = new System.Drawing.Point(82, 46);
            this.m_txtOdometer.Name = "m_txtOdometer";
            this.m_txtOdometer.Size = new System.Drawing.Size(153, 21);
            this.m_txtOdometer.TabIndex = 10;
            // 
            // m_txtHoursMeter
            // 
            this.m_txtHoursMeter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtHoursMeter.Location = new System.Drawing.Point(127, 70);
            this.m_txtHoursMeter.Name = "m_txtHoursMeter";
            this.m_txtHoursMeter.Size = new System.Drawing.Size(108, 21);
            this.m_txtHoursMeter.TabIndex = 20;
            // 
            // m_lblOdometer
            // 
            this.m_lblOdometer.ForeColor = System.Drawing.Color.Red;
            this.m_lblOdometer.Location = new System.Drawing.Point(1, 48);
            this.m_lblOdometer.Name = "m_lblOdometer";
            this.m_lblOdometer.Size = new System.Drawing.Size(80, 18);
            this.m_lblOdometer.Text = "Odometer";
            // 
            // m_lblPumpReading
            // 
            this.m_lblPumpReading.ForeColor = System.Drawing.Color.Red;
            this.m_lblPumpReading.Location = new System.Drawing.Point(1, 72);
            this.m_lblPumpReading.Name = "m_lblPumpReading";
            this.m_lblPumpReading.Size = new System.Drawing.Size(121, 20);
            this.m_lblPumpReading.Text = "Hours (Hobbs) Meter";
            // 
            // m_chkOilChecked
            // 
            this.m_chkOilChecked.Location = new System.Drawing.Point(0, 0);
            this.m_chkOilChecked.Name = "m_chkOilChecked";
            this.m_chkOilChecked.Size = new System.Drawing.Size(100, 20);
            this.m_chkOilChecked.TabIndex = 30;
            this.m_chkOilChecked.Text = "Oil Checked";
            // 
            // m_chkVanClean
            // 
            this.m_chkVanClean.Location = new System.Drawing.Point(0, 0);
            this.m_chkVanClean.Name = "m_chkVanClean";
            this.m_chkVanClean.Size = new System.Drawing.Size(100, 20);
            this.m_chkVanClean.TabIndex = 40;
            this.m_chkVanClean.Text = "Van Clean";
            // 
            // m_chkUnitClean
            // 
            this.m_chkUnitClean.Location = new System.Drawing.Point(0, 0);
            this.m_chkUnitClean.Name = "m_chkUnitClean";
            this.m_chkUnitClean.Size = new System.Drawing.Size(100, 20);
            this.m_chkUnitClean.TabIndex = 50;
            this.m_chkUnitClean.Text = "Unit Clean";
            // 
            // m_chkSuppliesStocked
            // 
            this.m_chkSuppliesStocked.Location = new System.Drawing.Point(0, 0);
            this.m_chkSuppliesStocked.Name = "m_chkSuppliesStocked";
            this.m_chkSuppliesStocked.Size = new System.Drawing.Size(121, 20);
            this.m_chkSuppliesStocked.TabIndex = 60;
            this.m_chkSuppliesStocked.Text = "Supplies Stocked";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(1, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 18);
            this.label4.Text = "Oil Change Due";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_chkOilChecked);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(100, 20);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.m_chkVanClean);
            this.panel2.Location = new System.Drawing.Point(3, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(92, 21);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.m_chkUnitClean);
            this.panel3.Location = new System.Drawing.Point(114, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(121, 21);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.m_chkSuppliesStocked);
            this.panel4.Location = new System.Drawing.Point(114, 25);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(121, 21);
            // 
            // m_lblOilChangeDue
            // 
            this.m_lblOilChangeDue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblOilChangeDue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.m_lblOilChangeDue.Location = new System.Drawing.Point(100, 92);
            this.m_lblOilChangeDue.Name = "m_lblOilChangeDue";
            this.m_lblOilChangeDue.Size = new System.Drawing.Size(135, 18);
            this.m_lblOilChangeDue.Text = "3200 miles";
            this.m_lblOilChangeDue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // VanCheckView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.m_lblOilChangeDue);
            this.Controls.Add(this.m_txtHoursMeter);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_lblPumpReading);
            this.Controls.Add(this.m_lblOdometer);
            this.Controls.Add(this.m_txtOdometer);
            this.Controls.Add(this.m_txtSpecialNeeds);
            this.Controls.Add(this.m_lblSpecialNeeds);
            this.Name = "VanCheckView";
            this.Size = new System.Drawing.Size(240, 268);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        internal System.Windows.Forms.TextBox m_txtSpecialNeeds;
        internal System.Windows.Forms.Label m_lblSpecialNeeds;
        internal DigitsEdit m_txtOdometer;
        internal DigitsEdit m_txtHoursMeter;
        internal System.Windows.Forms.Label m_lblOdometer;
        internal System.Windows.Forms.Label m_lblPumpReading;
        internal System.Windows.Forms.CheckBox m_chkOilChecked;
        internal System.Windows.Forms.CheckBox m_chkVanClean;
        internal System.Windows.Forms.CheckBox m_chkUnitClean;
        internal System.Windows.Forms.CheckBox m_chkSuppliesStocked;
        internal System.Windows.Forms.Label m_lblOilChangeDue;
    }
}
