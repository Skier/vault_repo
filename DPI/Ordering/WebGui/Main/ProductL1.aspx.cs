using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls; 
using System.Web.UI.HtmlControls;

using DPI.Services;
using DPI.Interfaces;
using DPI.Ordering;
using DPI.ClientComp;

namespace DPI.Ordering
{
	public class ProductL1 : BasePage
	{	 
	
		#region Web Form Designer generated code

		protected System.Web.UI.HtmlControls.HtmlInputHidden txtHidden1;
		protected System.Web.UI.HtmlControls.HtmlForm form_ProductL1;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;
		protected System.Web.UI.WebControls.ImageButton btnNext;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.Label lblIlec;
		protected System.Web.UI.WebControls.Label lblZipCode;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.ImageButton btnPrint;
		protected System.Web.UI.WebControls.PlaceHolder FeaturesGrid;
		
		override protected void OnInit(EventArgs ea)
		{
			InitializeComponent();
			base.OnInit(ea);
		}
		private void InitializeComponent()
		{    
			this.btnPrevious.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrevious_Click);
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion 

		#region Event Handlers
	
		void Page_Load(object sender, System.EventArgs e)
		{
			bool loaded = false;

			try
			{	
				loaded = Pre_Load();
				
				if (IsPostBack)
					return;		

				CustomInit();
				
				lblIlec.Text = ((IILECInfo)wipper.Wip["selectedilec"]).ILECName;
				lblZipCode.Text = (string)wipper.Wip["zip"];
			
				if (!ValidateZip())
					return;

				if (!loaded)
					LoadFeaturesGrid();
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
				btnNext.Visible = false;
				lblErrMsg.Visible = true;
				lblErrMsg.Text = Const.GENERAL_ERROR;
			}
		}
		
		void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs ea)
		{
			try
			{
				if (!CheckSelectedBasicService())
					return;

				Response.Redirect(wipper.Wip.Next(), false);
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
				btnNext.Visible = false;
				lblErrMsg.Visible = true;
				lblErrMsg.Text = Const.GENERAL_ERROR;
			}
		}
		void ControlOnClick(object sender, EventArgs ea)
		{
			try
			{
				string prod = ((WebControl)sender).ID;

				IProdPrice[] prods = (IProdPrice[])wipper.Wip["TopProducts"];	
				for (int j = 0; j < prods.Length; j++)
					if (prod == prods[j].ProdId.ToString()) 
						wipper.Wip["SelectedBasicService"] = prods[j];

				if (wipper.Wip["SelectedBasicService"] == null)
					throw new ApplicationException("Can't find selected product");
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
				btnNext.Visible = false;
				lblErrMsg.Visible = true;
				lblErrMsg.Text = Const.GENERAL_ERROR;
			}
		}
		void btnPrevious_Click(object sender, System.Web.UI.ImageClickEventArgs ea)
		{
			try
			{
				wipper.Wip["TopProducts"] = null;
				wipper.Wip["SelectedBasicService"] = null; 
				Response.Redirect(GoBack(), false);
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
				btnNext.Visible = false;
				lblErrMsg.Visible = true;
				lblErrMsg.Text = Const.GENERAL_ERROR;
			}
		}
		#endregion

		#region Implementation
		string GoBack()
		{
			if (wipper.Wip["avaIlecs"] == null)
				return wipper.Wip.Rework();

			if (((IILECInfo[])wipper.Wip["avaIlecs"]).Length > 1)
				return wipper.Wip.Prev();

			wipper.Wip["avaIlecs"] = null;
			wipper.Wip["SelectedIlec"] = null;
			return wipper.Wip.Rework();
		}
		bool Pre_Load()
		{	
			MultiClickBlocker.Block(this, btnNext);
			MultiClickBlocker.Block(this, btnPrevious);
			MultiClickBlocker.Block(this, btnPrint);

			btnPrint.Attributes.Add("onClick", "window.open('PrintL1.aspx', '_blank' ,'height= 600, width=660 ,"
				+ "toolbar=no, location=no, directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			
			return  LoadFeaturesGrid();
		}
		void CustomInit()
		{	
			btnNext.Visible = wipper.Wip.HasNext;
			btnPrevious.Visible = wipper.Wip.HasPrev;

			if (wipper.Wip["Discounts"] == null)
				wipper.Wip["Discounts"] = ProdSvc.GetAllProdDiscounts(wipper.IMap, OrderType.New.ToString());

			
			if (wipper.Wip["TopProducts"] == null)
				wipper.Wip["TopProducts"]= ProdSvc.GetTopProd(wipper.IMap, 
					(IILECInfo)wipper.Wip["SelectedIlec"],
					(string)wipper.Wip["Zip"],
					GetUser().Role,
					GetUser().LoginStoreCode,
					(string)wipper.Wip["Criteria"]);
			if (!ValidateZip())
				return;

			//			for (int i = 0; i < ((IProdPrice[])wipper.Wip["TopProducts"]).Length; i++)
			//				if (((IProdPrice[])wipper.Wip["TopProducts"])[i].ProdSelState == ProdSelectionState.Selected)
			//					wipper.Wip["SelectedBasicService"] = ((IProdPrice[])wipper.Wip["TopProducts"])[i];
		}
		IUser GetUser()
		{
			return (IUser)Session["User"];
		}
		bool ValidateZip()
		{
			if (((IProdPrice[])wipper.Wip["TopProducts"]).Length > 0)
				return true;

			ErrorHandler("No products are found for zip " + (string)wipper.Wip["Zip"]);
			this.lblErrMsg.Text = "No products are available";
			lblErrMsg.Visible = true;

			return false;
		}
		bool LoadFeaturesGrid()
		{
			if (wipper.Wip["TopProducts"] == null)
				return false;

			FeaturesGrid.Controls.Add(
				new TableProdL1((IKeyVal[])wipper.Wip["Discounts"], (IProdPrice[])wipper.Wip["TopProducts"], 
				FeatureMatrixAdapter.GetMatrix((IProdPrice[])wipper.Wip["TopProducts"]),
				new EventHandler(ControlOnClick)));

			return true;
		}
		void ErrorHandler(string msg)
		{
			FatalError.SaveErr( wipper.IMap, msg, Session["User"], this.ToString());
		}
		void ErrorHandler(Exception ex)
		{
			FatalError.SaveErr( wipper.IMap, ex, Session["User"], this.ToString());
		}
		bool CheckSelectedBasicService()
		{
			if (wipper.Wip["SelectedBasicService"] == null)
			{
				lblErrMsg.Text = "Please Select a Local Service";
				lblErrMsg.Visible = true;
				return false;
			}
			
			return true;
		}
		#endregion
	}
}