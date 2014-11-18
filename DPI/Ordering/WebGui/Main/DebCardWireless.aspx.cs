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
	public class DebCardWireless : BasePage
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
			this.ddlVendor.SelectedIndexChanged += new System.EventHandler(this.ddlVendor_SelectedIndexChanged);
			this.ddlProduct.SelectedIndexChanged += new System.EventHandler(this.ddlProduct_SelectedIndexChanged);
			this.ddlPayMethod.SelectedIndexChanged += new System.EventHandler(this.ddlPayMethod_SelectedIndexChanged);
			this.btnChangeDue.Click += new System.Web.UI.ImageClickEventHandler(this.btnChangeDue_Click);
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

	#region Events	
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
		void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				MultiBlock();

				if(IsPostBack)
					return;
		
				BindVendors();
				BindProducts();
				// setup demand, dmd item, and payinfo 
				CalculateChange();

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
				decimal amt = ((IPayInfo)wipper.Wip["payInfo"]).ChangeAmount;

				CalculateChange();
				
				if (!ValidateIt())
					return;

				if (((IPayInfo)wipper.Wip["payInfo"]).ChangeAmount != amt)
					return;
						
				wipper.Wip["receipt"] = PinSvc.OrderProduct(wipper.IMap, 
					                                       (IUser)Session["User"],
														   (IPayInfo)wipper.Wip["payInfo"],
														   (IPinProduct)wipper.Wip["selectedPinProduct"],
														   (IDemand)wipper.Wip["Demand"],
														   ((int)wipper.Wip.WipId).ToString());
				
				if (wipper.Wip.HasNext)
					Response.Redirect(wipper.Wip.Next(), false);
				else
					Response.Redirect("MenuScreen.aspx", false);

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
	//			if ((PaymentType)int.Parse(ddlPayMethod.SelectedValue) != PaymentType.Cash)			
	//			{			
	//				CalculateChange();
	//				txtAmountTendered.Text = ((IPayInfo)wipper.Wip["payinfo"]).TotalAmountDue.ToString("C");
	//			}
				CalculateChange();
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);			
			}		
		}

	#endregion
		
	#region Implementations
		void BindVendors()
		{
			//add websvcque
			IPinVendor[] pinVendors = PinSvc.GetDebitCardVendors(wipper.IMap, DPI.ClientComp.User.GetUser(this));

			ddlVendor.DataSource = pinVendors;
			ddlVendor.DataTextField = "Name";
			ddlVendor.DataValueField = "Id";
			ddlVendor.DataBind();
		}
		void BindProducts()
		{
			//add websvcque
			wipper.Wip["pinproducts"] = PinSvc.GetProducts(wipper.IMap, DPI.ClientComp.User.GetUser(this),
				int.Parse(ddlVendor.SelectedValue));

			ddlProduct.DataSource = wipper.Wip["pinproducts"];
			ddlProduct.DataTextField = "Product_Name";
			ddlProduct.DataValueField = "Product_Id";
			ddlProduct.DataBind();									
			SelectProduct();
		}
		void SelectProduct()
		{
			IPinProduct[] pinProducts = (IPinProduct[])wipper.Wip["pinproducts"];			
			wipper.Wip["selectedpinproduct"] = pinProducts[ddlProduct.SelectedIndex];
			lblProductSummary.Text = pinProducts[ddlProduct.SelectedIndex].Product_Name + " - " + pinProducts[ddlProduct.SelectedIndex].Expiration;
			lblAmountDue.Text = pinProducts[ddlProduct.SelectedIndex].Price.ToString("C");
		}
				
		void CalculateChange()
		{
			try
			{ 
				lblErrMsg.Text = "";
				
				CheckPayInfo();
				ToPage();
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
				lblChangeDue.Text = "Amount must be numeric";	
			}
		}

		void CheckPayInfo()
		{
			CheckDemand();

			if (wipper.Wip["PayInfo"] == null)
				 wipper.Wip["PayInfo"] = PaySvc.GetNewPayInfo(wipper.IMap, (IDemand)wipper.Wip["Demand"], PayInfoClass.PayInfo);

			IPayInfo pi = (IPayInfo)wipper.Wip["PayInfo"];

			pi.Status = PaymentStatus.Pend.ToString();			
			pi.TotalAmountPaid = pi.TotalAmountDue = Money.Truncate(lblAmountDue.Text);
			pi.AmountTendered = Money.Truncate(txtAmountTendered.Text);
			pi.PaymentType = (PaymentType)int.Parse(ddlPayMethod.SelectedValue);
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
			//AddDmdItems();
		}
//		void AddDmdItems()
//		{		
//			((IDemand)wipper.Wip["Demand"]).ClearDmdItems();
//
//			wipper.Wip["DItem"] = ProdSvc.AddDmdItem(wipper.IMap, 
//				(IDemand)wipper.Wip["Demand"], 
//				"",
//				OrderType.Add); //??????
//			
//			((IDmdItem)wipper.Wip["DItem"]).DmdItemType = DItemType.Selected.ToString();
//			//di.Status = "Pending"; 
//		}
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
		bool ValidateIt()
		{
			lblErrMsg.Visible = false;
			btnNext.Visible = false;

			if (((IPinProduct)wipper.Wip["selectedpinproduct"]) == null)
			{				
				lblErrMsg.Text = "Please select an Internet Product.";
				lblErrMsg.Visible = true;
				return false;
			}		
			if (((IPayInfo)wipper.Wip["payinfo"]).AmountTendered  < ((IPayInfo)wipper.Wip["payinfo"]).TotalAmountDue)
			{
				lblErrMsg.Text = "Please enter a Total Amount Collected greater than or equal to the Total Amount Due.";
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
	#endregion
	}
}