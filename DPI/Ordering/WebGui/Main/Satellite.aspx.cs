using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DPI.Services;
using DPI.Interfaces;
using DPI.ClientComp;

namespace DPI.Ordering
{
	public class Satellite : BasePage
	{

	#region Data
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label lblAmountDue;
		protected System.Web.UI.WebControls.Label lblChangeDue;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;
		protected System.Web.UI.WebControls.ImageButton btnNext;
		protected System.Web.UI.WebControls.Label lblProductSummary;
		protected System.Web.UI.WebControls.Label txtNonRefund;
		protected System.Web.UI.WebControls.ImageButton btnChangeDue;
		protected System.Web.UI.WebControls.DropDownList ddlPayMethod;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtAmountTendered;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtSalesId;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnProducts;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnSelectedProd;
		protected System.Web.UI.WebControls.ImageButton btnSvcPwrSchedule;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnSelVendor;
		protected System.Web.UI.WebControls.RadioButtonList ddlProduct;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnAmtDue;			
	#endregion

	#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
			CustomInit();
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnSvcPwrSchedule.Click += new System.Web.UI.ImageClickEventHandler(this.btnSvcPwrSchedule_Click);
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

	#region Event Handlers		
		void Page_Load(object sender, System.EventArgs e)
		{
			try
			{				
				if(IsPostBack)
					return;
		
				BindVendors();
				BindProducts();
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}

		void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				SetWip();
				CalculateChange();				

				if (!ValidateBeforeOrdering())
					return;
					 
				CustSvc.PreSave(wipper.IMap);

				wipper.Wip["Receipt"] = PinSvc.OrderSatellite(wipper.IMap, 
															(IUser)Session["User"],
															(IPayInfo[])wipper.Wip["PayInfos"],
															(IPinProduct)wipper.Wip["SelectedPinProduct"],
															(IDemand)wipper.Wip["Demand"],
															((int)wipper.Wip.WipId).ToString());
				
				Response.Redirect(wipper.Wip.Next(), false);
			}			
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}		
		}

		void btnSvcPwrSchedule_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				Response.Redirect("SatelliteSchedular.aspx", false);
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}			
		}
		
		void CustomInit()
		{
			try
			{
				btnPrevious.Visible = wipper.Wip.HasPrev;
				btnNext.Visible = wipper.Wip.HasNext;
				MultiBlock();
				AddAttrs();
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
	#endregion
		
	#region Implementations	
		
		void AddAttrs()
		{
//			ddlVendor.Attributes.Add("onChange", "FillProduct(this);");
			//ddlProduct.Attributes.Add("onChange", "SetData(this.value);");
			ddlProduct.Attributes.Add("onClick", "SetData();");
			btnChangeDue.Attributes.Add("onClick", "Calculate();");
			btnNext.Attributes.Add("onClick", "Submit();");
			btnSvcPwrSchedule.Attributes.Add("onClick", "clickedButton = true;");
		}
		void BindVendors()
		{
//			IPinVendor[] pinVendors = PinSvc.GetInternetVendors(wipper.IMap, DPI.ClientComp.User.GetUser(this));
//
//			ddlVendor.DataSource = pinVendors;
//			ddlVendor.DataTextField = "Name";
//			ddlVendor.DataValueField = "Id";
//			ddlVendor.DataBind();
		}

		void BindProducts()
		{
			hdnProducts.Value = PinSvc.GetProducts(wipper.IMap, (IUser)Session["User"], ProdCategory.Satellite.ToString());
			
			wipper.Wip["PinProducts"] 
				= PinSvc.GetWirelessProds(wipper.IMap, DPI.ClientComp.User.GetUser(this), int.Parse(hdnSelVendor.Value));
			
			ddlProduct.DataSource = wipper.Wip["PinProducts"];
			ddlProduct.DataTextField = "Product_Name";
			ddlProduct.DataValueField = "Product_Id";
			
			ddlProduct.DataBind();		
			
			if (ddlProduct.Items.Count > 1)
				ddlProduct.Items.RemoveAt(0);

			
		}

		void CalculateChange()
		{
			CheckPayInfo();			
		}
		void SetWip()
		{
			wipper.Wip["SelectedPinProduct"] = PinSvc.GetProduct(wipper.IMap, int.Parse(hdnSelectedProd.Value));
			wipper.Wip.WLProd = ((IPinProduct)wipper.Wip["SelectedPinProduct"]).Product_Id;
		}
		void CheckPayInfo()
		{
			CheckDemand();
			
			wipper.Wip["PayInfos"] = GetPayInfos(int.Parse(((IPinProduct)wipper.Wip["SelectedPinProduct"]).ReqItems));
		}
		IPayInfo[] GetPayInfos(int nos)
		{
			ArrayList ar = new ArrayList();

			for (int i = 0; i < nos + 1; i++)
			{
				IPayInfo pi = PaySvc.GetNewPayInfo(wipper.IMap, (IDemand)wipper.Wip["Demand"], PayInfoClass.PayInfo);

				pi.Status = PaymentStatus.Pend.ToString();			
				
				pi.TotalAmountPaid = pi.TotalAmountDue = Money.Truncate(hdnAmtDue.Value)/nos;
				if (i == 0)
					pi.TotalAmountPaid = pi.TotalAmountDue = Money.Truncate(hdnAmtDue.Value);
				
				pi.AmountTendered = Money.Truncate(txtAmountTendered.Text);
				pi.PaymentType = (PaymentType)int.Parse(ddlPayMethod.SelectedValue);
				pi.PayInfoSource = PayInfoSource.Wireless;

				ar.Add(pi);
			}

			IPayInfo[] pis = new IPayInfo[ar.Count];
			ar.CopyTo(pis);

			return pis;
		}
		void CheckDemand()
		{
			if (wipper.Wip["Demand"] != null)
				return;

			IDemand demand = DmdFactory.GetDemand(wipper.IMap, DemandType.Satellite.ToString(), true);
			
			IUser user = (IUser)Session["User"];
			demand.StoreCode = user.LoginStoreCode;
			demand.Loc = LocSvc.FindStoreZipId(wipper.IMap, user.LoginStoreCode);
			demand.ConsumerAgent = user.ClerkId; 
			demand.Status = DemandStatus.Submited.ToString();
			demand.IsUnderWF		= true;
			demand.WFStep			= wipper.Wip.Url;
			demand.Workflow		= wipper.Workflow;

			wipper.Wip["Demand"] = demand;
		}
		bool ValidateBeforeOrdering()
		{
			if (!(ValidateSalesId()))
				return false;
	
			if ((IPayInfo[])wipper.Wip["PayInfos"] == null)
			{
				CalculateChange();
				return false;
			}

			if (!ValidateIt())
				return false;

			return true;
		}

		void MultiBlock()
		{
			MultiClickBlocker.Block(this, btnNext);
			MultiClickBlocker.Block(this, btnPrevious);			
			MultiClickBlocker.Block(this, btnChangeDue);
			//MultiClickBlocker.Block(this, btnGetProducts);
		}
		bool ValidateIt()
		{
			lblErrMsg.Visible = false;
			btnNext.Visible = false;

			if (((IPinProduct)wipper.Wip["SelectedPinProduct"]) == null)
			{				
				lblErrMsg.Text = "Please select an Internet Product.";
				lblErrMsg.Visible = true;
				return false;
			}		

			if (((IPayInfo[])wipper.Wip["PayInfos"])[0].AmountTendered  < ((IPayInfo[])wipper.Wip["PayInfos"])[0].TotalAmountDue)
			{
				lblErrMsg.Text = "Please enter an Amount greater than or equal to Amount Due.";
				lblErrMsg.Visible = true;
				return false;
			}

			btnNext.Visible = true;
			return true;
		}

		void ErrorHandler(Exception ex)
		{
			FatalError.SaveErr(wipper.IMap, ex, Session["User"], this.ToString());
			btnNext.Visible = false;
			lblErrMsg.Visible = true;
			lblErrMsg.Text = Const.GENERAL_ERROR;
		}			
		
		bool ValidateSalesId()
		{
			this.lblErrMsg.Visible = false;
			this.lblErrMsg.Text = "";

			if(!(LoginSvc.GetIfClerkIDRequested((IUser)Session["User"])))
				return true;
			
			if (txtSalesId.Text.Trim().Length == 0)
			{
				lblErrMsg.Text = "Please enter Co-Worker Id";
				lblErrMsg.Visible = true;
				return false;
			}

			wipper.Wip.ClerkId = txtSalesId.Text.Trim();
			return true;
		}	
	#endregion

		
	}
}