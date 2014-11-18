namespace DPI.Ordering.control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Web.SessionState;

	using DPI.Services;
	using DPI.Interfaces;
	using DPI.Ordering;
	using DPI.ClientComp;

	public class subheader : System.Web.UI.UserControl
	{
	#region Data
		protected System.Web.UI.WebControls.Label lblSteps;
		protected System.Web.UI.WebControls.Label lblFeature;
		protected System.Web.UI.WebControls.Label lblMTDRank;
		protected System.Web.UI.WebControls.Label lblActCustRank;
		protected System.Web.UI.WebControls.Label lblActiveCust;
		protected System.Web.UI.WebControls.Label lblMTD;
		protected System.Web.UI.WebControls.Label lblMTDRev;
		protected System.Web.UI.WebControls.Label lblMTDRevenue;
		protected System.Web.UI.WebControls.ImageButton btnActCustRank;
		protected System.Web.UI.WebControls.ImageButton btnMtdRank;
		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.WebControls.Label lblLocation;
		protected System.Web.UI.WebControls.Label lblLocationLabel;
		protected System.Web.UI.WebControls.Label lblMTDRevRank;
		protected System.Web.UI.WebControls.ImageButton btnMTDRevRank;
		protected System.Web.UI.WebControls.Label lblStoreNum;
	#endregion
	#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/*		Event Handlers		*/
		void Page_Load(object sender, System.EventArgs e)
		{
			SetupAttributes(); // Mel's stuff

			SetLabels();
			
			try
			{
				if ((IMap)Session["IMap"] != null)
					GetStepsInfo((WIP)((IMap)Session["IMap"]).find(WIP.IKeyS));	

				SetupQuickStats();
			}
			catch(Exception ex)
			{
				if ((IMap)Session["IMap"] != null)
					ErrLogSvc.LogError(
						(IMap)Session["Imap"], this.ToString(), HttpContext.Current.User.Identity.Name, 
						ex.Message + ", " + ex.StackTrace);				
			}
		}
		/*		Implementation		*/
		void SetupAttributes()
		{
			string active   =  StatType.Active.ToString();
			string newSales =  StatType.New.ToString();
			string revenue  =  StatType.Revenues.ToString();

			btnActCustRank.Attributes.Add("onClick","clickedButton=true; ");
			btnMtdRank.Attributes.Add("onClick","clickedButton=true; ");

			btnActCustRank.Attributes.Add("onclick", "window.open('TopPerformers.aspx?statType=" 
				+ active 
				+ "', '_blank' ,'height= 500, width=405 ,toolbar=no, location=no,directories=no,status=no,menubar=no,"
				+ "scrollbars=no,resizable=yes')");
		
			btnMtdRank.Attributes.Add("onclick", "window.open('TopPerformers.aspx?statType=" 
				+ newSales 
				+ "', '_blank' ,'height= 500, width=405 ,toolbar=no, location=no,directories=no,status=no,menubar=no,"
				+ "scrollbars=no,resizable=yes')");

			btnMTDRevRank.Attributes.Add("onclick", "window.open('TopPerformers.aspx?statType="
				+ revenue 
				+ "', '_blank' ,'height= 500, width=405 ,toolbar=no, location=no,directories=no,status=no,menubar=no,"
				+ "scrollbars=no,resizable=yes')");

//			btnActCustRank.Attributes.Add("onclick", "window.open('TopPerformers.aspx?statType=Active', '_blank' ,'height= 500, width=405 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
//			btnMtdRank.Attributes.Add("onclick", "window.open('TopPerformers.aspx?statType=NewSales', '_blank' ,'height= 500, width=405 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
//			btnMTDRevRank.Attributes.Add("onclick", "window.open('TopPerformers.aspx?statType=Revenues', '_blank' ,'height= 500, width=405 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
		}

		void SetLabels()
		{
			lblStoreNum.Text = "  " + User.GetUser(this).LoginStoreCode;
			lblLocation.Text = "<b><font color='darkgray'>Home ></font> <font color='chocolate'>Portal</font></b>";
		
			lblActiveCust.ForeColor = lblActCustRank.ForeColor = lblMTDRank.ForeColor = lblMTD.ForeColor = lblMTDRevenue.ForeColor = lblMTDRevRank.ForeColor = Color.Gray;
			lblActiveCust.Font.Size = lblActCustRank.Font.Size = lblMTDRank.Font.Size =  lblMTD.Font.Size = lblMTDRevenue.Font.Size = lblMTDRev.Font.Size = lblMTDRevRank.Font.Size = 8;
			lblActiveCust.Font.Bold = lblActCustRank.Font.Bold = lblMTDRank.Font.Bold = lblMTD.Font.Bold = lblMTDRevenue.Font.Bold = lblMTDRev.Font.Bold = lblMTDRevRank.Font.Bold =  true;
			
			lblMTDRevenue.Text = 
				"<A onmouseover='return escape(\""
				+"Total  month to date revenue collected from all sales in your store.  Includes:  Basic Service, LD Calling Card, Cellular Recharge and Internet."
				+"\")' href=\"javascript:void(0);\">"	
				+"Total Revenue Collected MTD: </a>";
			
			lblActCustRank.Text = "RANK: N/A";
			lblMTDRank.Text = "RANK: N/A";
			lblMTDRevRank.Text = "RANK: N/A";

		}
		void SetupQuickStats()
		{
			IStoreStats stat = 
				StoreSvc.GetStoreStats(DPI.ClientComp.User.GetUser(this).LoginStoreCode);
			
			ICorp corp = StoreSvc.GetCorp(stat.CorpId);
			if (corp == null)
				return;	

			SetupActiveCust(corp, stat);	
			SetupNewCust(corp, stat);
			SetupRevenue(corp, stat);
		}
	
		void SetupRevenue(ICorp corp, IStoreStats stat)
		{
			IStoreStats[] topRevenue = corp.Top10Revenue; 

			lblMTDRev.Text = "<font color='Chocolate'>"
							+ stat.Revenue.ToString("C")
							+ @"</font>";
			
			if (stat.Revenue > 0)
				lblMTDRevRank.Text = 
					"<A onmouseover='return escape(\""
					+"Rank of your store compared to all the other stores within your corporation in terms of revenue collection."
					+"\")' href=\"javascript:void(0);\">"
					+"Rank <font color='Chocolate'>" + stat.RevenueRank.ToString() + "</font>"
					+" out of <font color='Chocolate'>" + corp.Stores.Length.ToString() + "</font> stores</a>";
		}

		void SetupNewCust(ICorp corp, IStoreStats stat)
		{
			IStoreStats[] topNewSales = corp.Top10NewCust;

			lblMTD.Text = "New Sales MTD: <font color='Chocolate'>"
						+ stat.MDT_NewCust.ToString()
						+ @"</font>";
			
			if (stat.MDT_NewCust > 0)
				lblMTDRank.Text = 
					"<A onmouseover='return escape(\""
					+"Rank of the number of new sales month to date at your store compared to all the other stores within your corporation."
					+"\")' href=\"javascript:void(0);\">"
					+"Rank <font color='Chocolate'>" + stat.MDT_NewCustRank.ToString() + "</font>"
					+" out of <font color='Chocolate'>" + corp.Stores.Length.ToString() + "</font> stores</a>";
		}

		void SetupActiveCust(ICorp corp, IStoreStats stat)
		{
			IStoreStats[] topActive = corp.Top10Active;

			lblActiveCust.Text = "Active Customers: <font color='Chocolate'>" 
								+ stat.ActiveCust.ToString()
								+ @"</font>";

			if (stat.ActiveCust > 0)
				lblActCustRank.Text = 
					"<A onmouseover='return escape(\""
					+"Rank of the number of active customers at your store compared to all the other stores within your corporation."
					+"\")' href=\"javascript:void(0);\">"
					+"Rank <font color='Chocolate'>" + stat.ActiveCustRank.ToString() + "</font>"
					+" out of <font color='Chocolate'>" + corp.Stores.Length.ToString() + "</font> stores</a>";
		}

		void GetStepsInfo(WIP wip)
		{
			lblLocation.Text = "<b><font color='darkgray'>Home > Portal ></font> <font color='chocolate'>"+ wip.Workflow.Name +"</font></b>";
			lblFeature.Text = wip.Workflow.Name + " - ";
			lblSteps.Text = "Step " + wip.StepNumber.ToString() + " of "+ wip.StepCount.ToString();

		}

	}
}
