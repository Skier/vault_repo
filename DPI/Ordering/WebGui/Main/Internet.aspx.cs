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
	public class Internet : BasePage
	{

	#region Data
		protected System.Web.UI.HtmlControls.HtmlForm Form1;		
		protected System.Web.UI.WebControls.DropDownList ddlProduct;
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
		protected System.Web.UI.WebControls.TextBox txtNpa;
		protected System.Web.UI.WebControls.TextBox txtNxx;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnProducts;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnSelectedProd;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnAmtDue;
		protected System.Web.UI.WebControls.DropDownList ddlVendor;			
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

		void ddlVendor_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				CalculateChange();
				
				if (VendorSvc.IsNpaNxxReq(wipper.IMap, ddlVendor.SelectedValue))  
				{
					ClearProducts();   // wait until NPA-NXX is entered
					return;
				}

				BindProducts();
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		void ddlProduct_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				SelectProduct();
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
				CalculateChange();

				SetWip();

				if (!ValidateBeforeOrdering())
					return;
					 
				CustSvc.PreSave(wipper.IMap);

				wipper.Wip["Receipt"] = PinSvc.OrderProduct( wipper.IMap, 
															(IUser)Session["User"],
															(IPayInfo)wipper.Wip["PayInfo"],
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

		void btnChangeDue_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				CalculateChange();
				ValidateIt();
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}					
		}
		void ddlPayMethod_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{					
				CalculateChange();
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);			
			}		
		}
		void btnGetProducts_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				if (!IsValid)
					return;

				BindProducts(int.Parse(txtNpa.Text), int.Parse(txtNxx.Text));
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
			ddlVendor.Attributes.Add("onChange", "FillProduct(this);");
			ddlProduct.Attributes.Add("onChange", "SetData(this.value);");
			btnChangeDue.Attributes.Add("onClick", "Calculate();");
			btnNext.Attributes.Add("onClick", "Submit();");
		}
		void BindVendors()
		{
			IPinVendor[] pinVendors = PinSvc.GetInternetVendors(wipper.IMap, DPI.ClientComp.User.GetUser(this));

			ddlVendor.DataSource = pinVendors;
			ddlVendor.DataTextField = "Name";
			ddlVendor.DataValueField = "Id";
			ddlVendor.DataBind();
		}

		void BindProducts()
		{
			hdnProducts.Value = PinSvc.GetProducts(wipper.IMap, (IUser)Session["User"], ProdCategory.Internet.ToString());
//			wipper.Wip["PinProducts"] 
//				= PinSvc.GetProducts(wipper.IMap, DPI.ClientComp.User.GetUser(this), int.Parse(ddlVendor.SelectedValue));
//
//			ddlProduct.DataSource = wipper.Wip["PinProducts"];
//			ddlProduct.DataTextField = "Product_Name";
//			ddlProduct.DataValueField = "Product_Id";
//			ddlProduct.DataBind();									
//
//			SelectProduct();
		}

		void BindProducts(int areaCode, int prefix)
		{
			wipper.Wip["PinProducts"] = PinSvc.GetProducts(wipper.IMap, DPI.ClientComp.User.GetUser(this),
				int.Parse(ddlVendor.SelectedValue), areaCode, prefix);

			ddlProduct.DataSource = wipper.Wip["PinProducts"];
			ddlProduct.DataTextField = "Product_Name";
			ddlProduct.DataValueField = "Product_Id";
			ddlProduct.DataBind();									
			SelectProduct();
		}

		void ClearProducts()
		{
			ddlProduct.Items.Clear();	
			lblProductSummary.Text = "";
			lblAmountDue.Text = "";
			wipper.Wip["SelectedPinProduct"] = null;
		}

		void SelectProduct()
		{
			IPinProduct[] pinProducts = (IPinProduct[])wipper.Wip["PinProducts"];			

			wipper.Wip["SelectedPinProduct"] = null;
			lblProductSummary.Text = "";
			lblAmountDue.Text = "";

			if (ddlProduct.SelectedIndex < 1)// Index 0 is a blank product
				return;

			wipper.Wip["SelectedPinProduct"] = pinProducts[ddlProduct.SelectedIndex];
			
			lblProductSummary.Text 
				= pinProducts[ddlProduct.SelectedIndex].Product_Name 
				+ " - " 
				+ pinProducts[ddlProduct.SelectedIndex].Expiration;

			lblAmountDue.Text = pinProducts[ddlProduct.SelectedIndex].Price.ToString("C");
		}
				
		void CalculateChange()
		{
			CheckPayInfo();
			ToPage();
		}
		void SetWip()
		{
			wipper.Wip["SelectedPinProduct"] = PinSvc.GetProduct(wipper.IMap, int.Parse(hdnSelectedProd.Value));
			wipper.Wip.WLProd = ((IPinProduct)wipper.Wip["SelectedPinProduct"]).Product_Id;
		}
		void CheckPayInfo()
		{
			CheckDemand();

			if (wipper.Wip["PayInfo"] == null)
				 wipper.Wip["PayInfo"] = PaySvc.GetNewPayInfo(wipper.IMap, (IDemand)wipper.Wip["Demand"], PayInfoClass.PayInfo);

			IPayInfo pi = (IPayInfo)wipper.Wip["PayInfo"];

			pi.Status = PaymentStatus.Pend.ToString();			
			pi.TotalAmountPaid = pi.TotalAmountDue = Money.Truncate(hdnAmtDue.Value);
			pi.AmountTendered = Money.Truncate(txtAmountTendered.Text);
			pi.PaymentType = (PaymentType)int.Parse(ddlPayMethod.SelectedValue);
			pi.PayInfoSource = PayInfoSource.Wireless;
		}

		void CheckDemand()
		{
			if (wipper.Wip["Demand"] != null)
				return;

			IDemand demand = DmdFactory.GetDemand(wipper.IMap, DemandType.Internet.ToString(), true);
			
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
			if (!ValidateNpaNxx())
				return false;
			
			if (!(ValidateSalesId()))
				return false;
	
			if ((IPayInfo)wipper.Wip["payInfo"] == null)
			{
				CalculateChange();
				return false;
			}

			if (!ValidateIt())
				return false;

			return true;
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

			if (((IPayInfo)wipper.Wip["PayInfo"]).AmountTendered  < ((IPayInfo)wipper.Wip["payinfo"]).TotalAmountDue)
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

		bool ValidateNpaNxx()
		{
			if (!VendorSvc.IsNpaNxxReq(wipper.IMap, ddlVendor.SelectedValue))
				return true;

			if (!IsValid)
				return false;

			return true;
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