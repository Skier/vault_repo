using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

using DPI.Interfaces;
using DPI.Components;

namespace DPI.Maint
{
	public class TaxTest : System.Windows.Forms.Form
	{
		#region Windows Form Designer generated code
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnCalcTax;
		private System.Windows.Forms.TextBox txtMsg;
		private System.Windows.Forms.TextBox txtCounter;
		private System.ComponentModel.Container components = null;
		private void InitializeComponent()
		{
			this.btnStart = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnCalcTax = new System.Windows.Forms.Button();
			this.txtMsg = new System.Windows.Forms.TextBox();
			this.txtCounter = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// btnStart
			// 
			this.btnStart.Enabled = false;
			this.btnStart.Location = new System.Drawing.Point(16, 16);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(88, 23);
			this.btnStart.TabIndex = 0;
			this.btnStart.Text = "&Start";
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnClose
			// 
			this.btnClose.Enabled = false;
			this.btnClose.Location = new System.Drawing.Point(128, 16);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(88, 23);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnCalcTax
			// 
			this.btnCalcTax.Location = new System.Drawing.Point(16, 64);
			this.btnCalcTax.Name = "btnCalcTax";
			this.btnCalcTax.Size = new System.Drawing.Size(88, 23);
			this.btnCalcTax.TabIndex = 2;
			this.btnCalcTax.Text = "Calculate &Tax";
			this.btnCalcTax.Click += new System.EventHandler(this.btnCalcTax_Click);
			// 
			// txtMsg
			// 
			this.txtMsg.Location = new System.Drawing.Point(16, 96);
			this.txtMsg.Multiline = true;
			this.txtMsg.Name = "txtMsg";
			this.txtMsg.Size = new System.Drawing.Size(616, 368);
			this.txtMsg.TabIndex = 3;
			this.txtMsg.Text = "";
			// 
			// txtCounter
			// 
			this.txtCounter.Location = new System.Drawing.Point(16, 472);
			this.txtCounter.Name = "txtCounter";
			this.txtCounter.Size = new System.Drawing.Size(328, 20);
			this.txtCounter.TabIndex = 4;
			this.txtCounter.Text = "";
			// 
			// TaxTest
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(640, 501);
			this.Controls.Add(this.txtCounter);
			this.Controls.Add(this.txtMsg);
			this.Controls.Add(this.btnCalcTax);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnStart);
			this.Name = "TaxTest";
			this.Text = "Tax Calculation Test ---------------------";
			this.ResumeLayout(false);

		}

		public TaxTest()
		{
			InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion
		
		#region Data
		private int taxCounter = 0;
		private DateTime start;
		private ITaxService bTax;
		#endregion
		
		#region Event Handlers
		
		static void Main()
		{
			Application.Run(new TaxTest());
		}

		private void btnStart_Click(object sender, System.EventArgs e)
		{
			try
			{
				DpiEzTaxer.CreateChannel();
				btnStart.Enabled = false;
				btnClose.Enabled = true;
				btnCalcTax.Enabled = true;
				txtMsg.Text = "DPI EzTax Service started";
			}
			catch (Exception ex)
			{
				txtMsg.Text = ex.Message;
			}
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			try
			{
				DpiEzTaxer.StopChannel();
				this.Close();
			}
			catch (Exception ex)
			{
				txtMsg.Text = ex.Message;
			}
		}

		private void btnCalcTax_Click(object sender, System.EventArgs e)
		{
			txtMsg.Text = "";
			taxCounter = 0;
			start = DateTime.Now;			
			
			Thread[] threads = new Thread[100];
			try
			{				
				for (int i = 0; i <100; i++)
				{
					threads[i] = new Thread(new ThreadStart(DoTax));
					threads[i].Name = "Tax Thread: " + i.ToString();
					threads[i].Start();
				}
				
			}
			catch (Exception ex)
			{
				txtMsg.Text = ex.Message;
			}
		}


		#endregion		

		#region Implementations
		
		void DoTax()
		{
			++taxCounter;
			decimal amt = 29.99m;
			IDmdTax[] taxes = GetTaxProxy().ComputeTax(421, amt, "75063", DateTime.Now);
			ShowText(taxes);
		}
		void ShowText(IDmdTax[] taxes)
		{
			txtCounter.Text = "Total number of Tax calculated = " + taxCounter.ToString()
				+ "; Time spent = " + DateTime.Now.Subtract(start).ToString();
			for (int i = 0; i < taxes.Length; i++)
				txtMsg.Text += "Tax Desc: " + taxes[i].Description + " Tax Amount:" + taxes[i].TaxAmount + "\n";
			
		}
		ITaxService GetTaxProxy()
		{
			try
			{
				if (bTax != null)
					return bTax;

				bTax = (ITaxService)Activator.GetObject(typeof(ITaxService), System.Configuration.ConfigurationSettings.AppSettings["DpiEzTaxUri"]);
			}
			catch (Exception ex)
			{
				bTax = null;				
			}
			return bTax;
		}


		#endregion

	}


}
