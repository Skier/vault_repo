using System;

using DPI.Services;
using DPI.Interfaces;
using DPI.ClientComp;

namespace DPI.Ordering
{
	public class Wireless : BasePage
	{

	#region Web Form Designer generated code

		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label lblAmountDue;
		protected System.Web.UI.WebControls.Label lblChangeDue;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;
		protected System.Web.UI.WebControls.ImageButton btnNext;
		protected System.Web.UI.WebControls.Label lblProductSummary;
		protected System.Web.UI.WebControls.Label txtNonRefund;
		protected System.Web.UI.WebControls.ImageButton btnChangeDue;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtAmountTendered;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtSalesId;
		protected System.Web.UI.WebControls.DropDownList ddlVendor;
		protected System.Web.UI.WebControls.DropDownList ddlPayMethod;
		protected System.Web.UI.HtmlControls.HtmlInputText txtNpa;
		protected System.Web.UI.HtmlControls.HtmlInputText txtNxx;
		protected System.Web.UI.HtmlControls.HtmlInputText txtNumber;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnProducts;
		protected System.Web.UI.HtmlControls.HtmlSelect cboProduct;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnSelectedProd;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnAmtDue;
		

		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
			CustomInit();
		}
		void InitializeComponent()
		{    
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
	#endregion		

	#region Event Handlers
		void Page_Load(object sender, System.EventArgs e)
		{
			try
			{				
				
				MultiBlock();
				AddAttrs();
				
				LoadVendors();
				LoadProducts();				
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
//		void ddlVendor_SelectedIndexChanged(object sender, System.EventArgs e)
//		{
//			try
//			{
//				LoadProducts();
//				wipper.Wip.ProdGroup = ddlVendor.SelectedItem.Text;
//			}
//			catch (Exception ex)
//			{
//				ErrorHandler(ex);
//			}
//		}
//		void ddlProduct_SelectedIndexChanged(object sender, System.EventArgs e)
//		{
//			try
//			{
//				if (ddlProduct.SelectedIndex < 1)
//				{
//					wipper.Wip["SelectedPinProduct"] =  null;
//					return;
//				}
//		
//				IPinProduct[] prods = (IPinProduct[])wipper.Wip["PinProducts"];	
//				for (int i = 0; i < prods.Length; i++)
//					if (prods[i].Product_Id == int.Parse((ddlProduct.SelectedValue)))
//						wipper.Wip["SelectedPinProduct"] = prods[ddlProduct.SelectedIndex];
//			
//				if (wipper.Wip["SelectedPinProduct"] == null)
//					return;
//
//				wipper.Wip.WLProd = ((IPinProduct)wipper.Wip["SelectedPinProduct"]).Product_Id;
//				lblProductSummary.Text 
//					= ((IPinProduct)wipper.Wip["SelectedPinProduct"]).Product_Name
//					+ " - " 
//					+ ((IPinProduct)wipper.Wip["SelectedPinProduct"]).Expiration;
//				lblAmountDue.Text = ((IPinProduct)wipper.Wip["SelectedPinProduct"]).Price.ToString("C");
//			}
//			catch (Exception ex)
//			{
//				ErrorHandler(ex);
//			}
//		}
//		void btnChangeDue_Click(object sender, System.Web.UI.ImageClickEventArgs e)
//		{
//			try
//			{
//				CalculateChange();
//				ValidateIt();
//			}
//			catch (Exception ex)
//			{
//				ErrorHandler(ex);
//			}					
//		}
//		void ddlPayMethod_SelectedIndexChanged(object sender, System.EventArgs e)
//		{ 
//			try
//			{	
//				CalculateChange();
//				
//			}
//			catch (Exception ex)
//			{
//				ErrorHandler(ex);
//			}		
//		}
		void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				if (!(ValidateSalesId()))
					return;
				
				//decimal amt = ((IPayInfo)wipper.Wip["payInfo"]).ChangeAmount;
				CalculateChange();
				
				SetWip();

				//wipper.Wip["SelectedPinProduct"]
				if (!ValidateIt())
					return;

				//if (((IPayInfo)wipper.Wip["PayInfo"]).ChangeAmount != amt)
				//	return;

				SavePhoneNumber();
				CustSvc.PreSave(wipper.IMap);
	
				if(IsActivationReq())
				{
					SwitchToActivationWF();
					return;
				}
				if (IsNewInfinity())
					SwitchToWirelessWF2();

				if (!ValidateBeforeProceed())
					return;

				wipper.Wip["receipt"] = PinSvc.OrderProduct(wipper.IMap, DPI.ClientComp.User.GetUser(this),
						(IPayInfo)wipper.Wip["PayInfo"],
						(IPinProduct)wipper.Wip["selectedPinProduct"],
						(IDemand)wipper.Wip["Demand"],
						wipper.Wip.WipId.ToString(),
						(ICellPhoneInfo)wipper.Wip["PhoneInfo"]);

				if (wipper.Wip is WirelessWip2)
					wipper.Wip["IsCompleted"] = ((ICellPhoneReceipt)wipper.Wip["Receipt"]).Pass;

				//Server.Transfer(wipper.Wip.Next(), false);
				Response.Redirect(wipper.Wip.Next(), false);
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
				Page_Load(sender, e);
			}		
		}
	#endregion
	
	#region Implementation
		bool IsActivationReq()
		{
			return PinSvc.FindProduct
				(wipper.IMap, ((IPinProduct)wipper.Wip["selectedPinProduct"]).Product_Id).IsActivationReq;
		}
		bool IsNewInfinity()
		{
			return PinSvc.GetVendor(wipper.IMap, ((IPinProduct)wipper.Wip["selectedPinProduct"]).Product_Id)
				.DefaultWSProvider.Trim().ToLower() == Const.INFINITY_MOBILE;
			
		}
		void SwitchToActivationWF()
		{
			IPayInfo payInfo = (IPayInfo)wipper.Wip["payInfo"];
			IPinProduct prod = (IPinProduct)wipper.Wip["selectedPinProduct"];
			IDemand dmd      = (IDemand)wipper.Wip["Demand"];
	
			int wlProdSave = wipper.Wip.WLProd;
			string prodGroupSave = wipper.Wip.ProdGroup;

			wipper.Wip = new WirelessActivationWIP((IUser)Session["User"]);
			
			wipper.Wip.WLProd = wlProdSave;
			wipper.Wip.ProdGroup = prodGroupSave;

			wipper.Wip["SelectedPinProduct"] = prod;
			wipper.Wip["Demand"] = dmd;
			wipper.Wip["PayInfo"] =	payInfo;
	
			Response.Redirect(wipper.Wip.Current(), false);
		}
		void SwitchToWirelessWF2()
		{
			IPayInfo payInfo = (IPayInfo)wipper.Wip["payInfo"];
			IPinProduct prod = (IPinProduct)wipper.Wip["selectedPinProduct"];
			IDemand dmd      = (IDemand)wipper.Wip["Demand"];
			ICellPhoneInfo cell = (ICellPhoneInfo)wipper.Wip["PhoneInfo"];
	
			int wlProdSave = wipper.Wip.WLProd;
			string prodGroupSave = wipper.Wip.ProdGroup;

			wipper.Wip = new WirelessWip2((IUser)Session["User"]);
			
			wipper.Wip.WLProd = wlProdSave;
			wipper.Wip.ProdGroup = prodGroupSave;

			wipper.Wip["SelectedPinProduct"] = prod;
			wipper.Wip["Demand"] = dmd;
			wipper.Wip["PayInfo"] =	payInfo;
			wipper.Wip["PhoneInfo"] = cell;
		}
		void CustomInit()
		{
			try
			{
				btnPrevious.Visible = wipper.Wip.HasPrev;
				btnNext.Visible = wipper.Wip.HasNext;
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		void LoadVendors()
		{
			IPinVendor[] pinVendors = PinSvc.GetWirelessVendors(wipper.IMap, DPI.ClientComp.User.GetUser(this));

			ddlVendor.DataSource = pinVendors;
			ddlVendor.DataTextField = "Name";
			ddlVendor.DataValueField = "Id";

			ddlVendor.DataBind();
		}
		void LoadProducts()
		{
			hdnProducts.Value = PinSvc.GetProducts(wipper.IMap, (IUser)Session["User"], ProdCategory.Wireless.ToString());
//			wipper.Wip["PinProducts"] 
//				= PinSvc.GetWirelessProds(wipper.IMap, DPI.ClientComp.User.GetUser(this), int.Parse(ddlVendor.SelectedValue));
//
//			ddlProduct.DataSource = wipper.Wip["PinProducts"];
//			ddlProduct.DataTextField = "Product_Name";
//			ddlProduct.DataValueField = "Product_Id";
//
//			ddlProduct.DataBind();									
		}
		void CalculateChange()
		{
			CheckDemand();	
			CheckPayInfo();			
			
		}
		void CheckPayInfo()
		{
			if (wipper.Wip["PayInfo"] == null)
				wipper.Wip["PayInfo"] = PaySvc.GetNewPayInfo(wipper.IMap, (IDemand)wipper.Wip["Demand"], PayInfoClass.PayInfo);

			IPayInfo pi = (IPayInfo)wipper.Wip["PayInfo"];

			pi.Status          = PaymentStatus.Pend.ToString();			
			pi.TotalAmountPaid = pi.TotalAmountDue = Money.Truncate(hdnAmtDue.Value);
			pi.AmountTendered  = Money.Truncate(txtAmountTendered.Text);
			pi.PaymentType     = (PaymentType)int.Parse(ddlPayMethod.SelectedValue);
			pi.PayInfoSource   = PayInfoSource.Wireless;
		}
		void CheckDemand()
		{
			if (wipper.Wip["Demand"] != null)
				return;

			IDemand demand = DmdFactory.GetDemand(wipper.IMap, DemandType.Wireless.ToString(), true);
			
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
		void ToPage()
		{
			IPayInfo pi = (IPayInfo)wipper.Wip["PayInfo"];

			lblAmountDue.Text = pi.TotalAmountDue.ToString("C");		

			if (pi.AmountTendered != 0)
				txtAmountTendered.Text = pi.AmountTendered.ToString("C");					
				
			lblChangeDue.Text = "";
			if (pi.ChangeAmount >= 0)
				lblChangeDue.Text = pi.ChangeAmount.ToString("C");
		}
		void MultiBlock()
		{
			MultiClickBlocker.Block(this, btnNext);
			MultiClickBlocker.Block(this, btnPrevious);			
			MultiClickBlocker.Block(this, btnChangeDue);
		}
		bool ValidateBeforeProceed()
		{
			lblErrMsg.Visible = false;
			
			if (!ValidatePhone())
				return false;
	
			if (!ValidatePhoneDueDate())
				return false;

			return true;
		}
		bool ValidateIt()
		{
			lblErrMsg.Visible = false;
			
			if (((IPinProduct)wipper.Wip["SelectedPinProduct"]) == null)
			{				
				lblErrMsg.Text = "Please select a Wireless Product.";
				lblErrMsg.Visible = true;
				return false;
			}		

			if (((IPayInfo)wipper.Wip["PayInfo"]).AmountTendered  < ((IPayInfo)wipper.Wip["payinfo"]).TotalAmountDue)
			{
				lblErrMsg.Text = "Please enter a Total Amount Collected greater than or equal to the Total Amount Due.";
				lblErrMsg.Visible = true;
				return false;
			}			
			
			return true;
		}
		bool ValidatePhone()
		{
			if (!PinSvc.IsPhoneReq(wipper.IMap, (IPinProduct)wipper.Wip["SelectedPinProduct"]))
				return true;

			if (!IsValid)
				return false;
			
			if (!PinSvc.IsActive(wipper.IMap, 
				(IUser)Session["User"], 
				(IPinProduct)wipper.Wip["SelectedPinProduct"], 
				txtNpa.Value.Trim() +  txtNxx.Value.Trim() + txtNumber.Value.Trim()))
			{
				lblErrMsg.Visible = true;
				lblErrMsg.Text = "This phone number is inactive. Select one of the activation products";
				return false;																									return false;
			}
		return true;
		}
		bool ValidatePhoneDueDate()
		{
			if (!PinSvc.IsPerValidationReq(wipper.IMap, (IPinProduct)wipper.Wip["SelectedPinProduct"]))
				return true;

			if (!PinSvc.IsDateValid(wipper.IMap, (IUser)Session["User"], 
				(IPinProduct)wipper.Wip["SelectedPinProduct"], 
				txtNpa.Value.Trim() +  txtNxx.Value.Trim() + txtNumber.Value.Trim()))
			{
				lblErrMsg.Visible = true;
				lblErrMsg.Text = "Unable to apply Cash Card due to account status";
				return false;
			}

			return true;
		}
		void SavePhoneNumber()
		{
			if (!PinSvc.IsPhoneReq(wipper.IMap, (IPinProduct)wipper.Wip["SelectedPinProduct"])) 
				wipper.Wip["PhoneInfo"] = null;

			if (wipper.Wip["PhoneInfo"] == null)
				wipper.Wip["PhoneInfo"]= PinSvc.GetCellInfo();

			((ICellPhoneInfo)wipper.Wip["PhoneInfo"]).PhoneNumber 
				= txtNpa.Value.Trim() +  txtNxx.Value.Trim() + txtNumber.Value.Trim();

			((ICellPhoneInfo)wipper.Wip["PhoneInfo"]).WireleesProduct 
				= ((IPinProduct)wipper.Wip["SelectedPinProduct"]).Product_Id;
		}
		void ErrorHandler(Exception ex)
		{
			FatalError.SaveErr( wipper.IMap, ex, Session["User"], this.ToString());				
			lblErrMsg.Text = ex.Message;
			lblErrMsg.Visible = true;
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
		void AddAttrs()
		{
			ddlVendor.Attributes.Add("onChange", "FillProduct(this);");
			btnChangeDue.Attributes.Add("onClick", "Calculate();");
			btnNext.Attributes.Add("onClick", "Submit();");
		}
		void SetWip()
		{
			wipper.Wip["SelectedPinProduct"] = PinSvc.GetProduct(wipper.IMap, int.Parse(hdnSelectedProd.Value));
			wipper.Wip.WLProd = ((IPinProduct)wipper.Wip["SelectedPinProduct"]).Product_Id;
		}
	#endregion
	}
}