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
using System.Text.RegularExpressions;

using DPI.Services;
using DPI.Interfaces;
using DPI.Ordering;
using DPI.ClientComp;

namespace DPI.Ordering.Main
{
	public class DCSummary : BasePage
	{
	#region Data
		// labels
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label Label16;		
		protected System.Web.UI.WebControls.Label lblChange;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.Label lblEnrollFee;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label txtOrderTotal;

		// input fields
		protected System.Web.UI.WebControls.CheckBox ckValidID;
		protected System.Web.UI.WebControls.TextBox txtInitLoadAmnt;

		// buttons
		protected System.Web.UI.WebControls.RadioButton rbYes;
		protected System.Web.UI.WebControls.RadioButton rbNo;
		protected System.Web.UI.WebControls.DropDownList ddlPayType;
		protected System.Web.UI.WebControls.ImageButton btnChangeDue;
		protected System.Web.UI.WebControls.ImageButton btnNext;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;

		// screen objects
		protected System.Web.UI.WebControls.Image imgWorkflow;
		protected System.Web.UI.WebControls.PlaceHolder phError;

		// validators
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary2;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator3;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator5;
		protected System.Web.UI.WebControls.TextBox txtAmountCollected;
		protected System.Web.UI.WebControls.TextBox txtCardNum;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
	#endregion

	#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		//	CustomInit();
		}

		void InitializeComponent()
		{    
			this.rbNo.CheckedChanged += new System.EventHandler(this.rbNo_CheckedChanged);
			this.txtInitLoadAmnt.TextChanged += new System.EventHandler(this.txtInitLoadAmnt_TextChanged);
			this.btnChangeDue.Click += new System.Web.UI.ImageClickEventHandler(this.btnChangeDue_Click);
			this.btnPrevious.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrevious_Click);
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
	#endregion
	
	#region Event Handlers
			
		void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				Pre_Load();

				if(IsPostBack)
					return;

				CustomInit();
				GetDemand();
				GetPayInfo();
				GetDebCardApp();

				ToPage();
			}
			catch (Exception ex)
			{
				FatalError.ShowErr(phError, ex.Message);
				FatalError.SaveErr(ex, Session["User"], this.ToString());
			}
		}
		
		void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{	
				if (!IsValid)
					return;

				decimal amt = ((IPayInfo)wipper.Wip["payInfo"]).ChangeAmount;
				ToBusObj();

				if (!ValidateData())
					return;

				ToPage();
				
				if (((IPayInfo)wipper.Wip["payInfo"]).ChangeAmount != amt)
					return; 
				
				Response.Redirect(wipper.Wip.Next(), false);
			}
			catch (Exception ex)
			{				
				FatalError.ShowErr(phError, ex.Message);
				FatalError.SaveErr(ex, Session["User"], this.ToString());
			}
		}
		void btnPrevious_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			// remove all objects that were created in this class
			try
			{
				Destroyer.Destroy(wipper.Wip["Demand"]);
				wipper.Wip["Demand"] = null;

				Destroyer.Destroy(wipper.Wip["PayInfo"]);
				wipper.Wip["PayInfo"] = null;

				Destroyer.Destroy(wipper.Wip["DebCardApp"]);
				wipper.Wip["DebCardApp"] = null;

				Destroyer.Destroy(wipper.Wip["DebCard"]);
				wipper.Wip["DebCard"] = null;
			}
			catch {}
		}

		void btnChangeDue_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				if (!IsValid)
					return;

				RefreshData();
			}
			catch (Exception ex)
			{				
				FatalError.ShowErr(phError, ex.Message);
				FatalError.SaveErr(ex, Session["User"], this.ToString());
			}
		}
		void rbNo_CheckedChanged(object sender, System.EventArgs e)
		{
			try
			{
				txtCardNum.Visible = false;
				((ICardApp)wipper.Wip["DebCardApp"]).PrevCard = null; 
				RefreshData();
			}
			catch (Exception ex)
			{				
				FatalError.ShowErr(phError, ex.Message);
				FatalError.SaveErr(ex, Session["User"], this.ToString());
			}
		}
		void rbYes_CheckedChanged(object sender, System.EventArgs e)
		{
			try
			{
				txtCardNum.Visible = true;
				RefreshData();
			}
			catch (Exception ex)
			{
				FatalError.ShowErr(phError, ex.Message);
				FatalError.SaveErr(ex, Session["User"], this.ToString());
			}
		}
		void txtInitLoadAmnt_TextChanged(object sender, System.EventArgs e)
		{
			try
			{
				if (!ValTxtInitLoadAmnt())
					return;

				RefreshData();
			}
			catch (Exception ex)
			{				
				FatalError.ShowErr(phError, ex.Message);
				FatalError.SaveErr(ex, Session["User"], this.ToString());			
			}
		}
	#endregion

	#region Implementation	
		void CustomInit()
		{
			imgWorkflow.ImageUrl = wipper.Wip.Workflow.ImageTag.ToString();	
		}
		void Pre_Load()
		{
			btnPrevious.Visible = wipper.Wip.HasPrev;
			btnNext.Visible = wipper.Wip.HasNext;

			MultiClickBlocker.Block(this, btnNext);		
			MultiClickBlocker.Block(this, btnPrevious);	
		}
		void RefreshData()
		{
			ToBusObj();
			ValidateData();
			ToPage();
		}
		void ToBusObj()
		{
			((ICardApp)wipper.Wip["DebCardApp"]).PrevCard = null;
			
//			if (rbYes.Checked)
//			{
//				((ICardApp)wipper.Wip["DebCardApp"]).PrevCard = txtCardNum.Text;
//				RemoveFees();
//			}
//			if (rbNo.Checked)
//			{
//				((ICardApp)wipper.Wip["DebCardApp"]).PrevCard = null;
//				AddDmdItems();
//			}				
			((IDmdItem)wipper.Wip["DItem"]).AdjustPrice(Money.Truncate(txtInitLoadAmnt.Text));
			IOrderSum sumry = ((IDemand)wipper.Wip["Demand"]).OrderSummary(null);

			((IPayInfo)wipper.Wip["PayInfo"]).PaymentType = (PaymentType)int.Parse(ddlPayType.SelectedItem.Value);			
	
			((IPayInfo)wipper.Wip["PayInfo"]).TotalAmountDue = ((IPayInfo)wipper.Wip["PayInfo"]).TotalAmountPaid = sumry.GetTotalAmtDue();
			((IPayInfo)wipper.Wip["PayInfo"]).AmountTendered = Money.Truncate(txtAmountCollected.Text);			
		}

		void ToPage()
		{
			txtInitLoadAmnt.Text = txtAmountCollected.Text  = lblChange.Text =  txtOrderTotal.Text = string.Empty;
			// from order summary
		
			IOrderSum sumry = CustSvc.GetOrderSummary(wipper.IMap, (IDemand)wipper.Wip["Demand"]);
			lblEnrollFee.Text    = sumry.GetFeeAmt().ToString("C");

			if (sumry.GetProdAmt() > 0)
				txtInitLoadAmnt.Text = sumry.GetProdAmt().ToString("C");

			if (sumry.GetProdAmt() > 0)
				txtOrderTotal.Text   = sumry.GetTotalAmtDue().ToString("C");
			
			if (((IPayInfo)wipper.Wip["PayInfo"]).AmountTendered > 0)
				txtAmountCollected.Text = ((IPayInfo)wipper.Wip["PayInfo"]).AmountTendered.ToString("C");
			
			if (((IPayInfo)wipper.Wip["PayInfo"]).ChangeAmount > 0m)
				lblChange.Text = ((IPayInfo)wipper.Wip["PayInfo"]).ChangeAmount.ToString("C");

			DoYesNoRButtons();			
		}
		void DoYesNoRButtons()
		{
			if(((ICardApp)wipper.Wip["DebCardApp"]).PrevCard != null)
				if (((ICardApp)wipper.Wip["DebCardApp"]).PrevCard.Trim().Length > 0)
				{
					txtCardNum.Text = ((ICardApp)wipper.Wip["DebCardApp"]).PrevCard; 		
					txtCardNum.Visible = true;
				}
		}
		bool ValidateData()
		{
				phError.Visible = false;
				ArrayList ar = new ArrayList();

				ValOrderRule(ar);
				ValId(ar);
				ValTotalAmtPaid(ar);
				ValMasterCard(ar);

				if (ar.Count == 0)
					return true;

				phError.Controls.Add(new ErrorTable(ar));
				phError.Visible = true;
				return false;
		}
		void ValTotalAmtPaid(ArrayList ar)
		{
			if(((IDemand)wipper.Wip["Demand"]).OrderSummary(null).GetTotalAmtDue()
				> ((IPayInfo)wipper.Wip["PayInfo"]).AmountTendered)
				ar.Add("Amount Collected cannot be less than Amount Due");
		}
		void ValOrderRule(ArrayList ar)
		{
			if ((txtInitLoadAmnt.Text.Trim().Length == 0))
				ar.Add("Please enter Initial Load Amount");

			IProductOrderRule rule 
				= DebCardSvc.GetProductOrderRule(wipper.IMap, 
				((IDemand)wipper.Wip["Demand"]).DmdType, 
				((IProdPrice)wipper.Wip["debCard"]).ProdId);
			
			if (rule == null)
				return;
			
			if(Money.Truncate(txtInitLoadAmnt.Text) < rule.MinAmt) 
				ar.Add("Load amount less than " + rule.MinAmt.ToString("C") + " is not allowed");

			if(Money.Truncate(txtInitLoadAmnt.Text) > rule.MaxAmt)
				ar.Add("Load Amount more than " + rule.MaxAmt.ToString("C") + " is not allowed");
		}
		bool ValTxtInitLoadAmnt()
		{
			phError.Visible = false;
			ArrayList ar = new ArrayList();

			ValRegEx(ar);

			if (ar.Count == 0)
				return true;

			phError.Controls.Add(new ErrorTable(ar));
			phError.Visible = true;
			return false;		
		}
		void ValRegEx(ArrayList ar)
		{
			Regex reg = new Regex(@"^\$?[\d,]*\.?\d{0,2}$");

			if (!reg.IsMatch(txtInitLoadAmnt.Text))
				ar.Add("Please enter valid Initial Load Amount");
		}
		void ValId(ArrayList ar)
		{
			((ICardApp)wipper.Wip["DebCardApp"]).Verified = true;
			
			if(ckValidID.Checked)
				return;

			((ICardApp)wipper.Wip["DebCardApp"]).Verified = false;
			ar.Add("Please confirm that customer has presented valid id and check box if compliant.");
		}
		void ValMasterCard(ArrayList ar)
		{
			if (!rbYes.Checked)
				return;

			if((txtCardNum.Text.Trim().Length) == 4)
				return;

			ar.Add("Please enter Existing Master Card");
		}
  		void RemoveFees()
		{
			IDmdItem[] adi = ((IDemand)wipper.Wip["Demand"]).GetDmdItems();
			for (int i  = 0; i < adi.Length; i++)
				adi[i].RemoveTagAlongs();
		}
	#endregion

	#region Bus objects

		void GetDebCardApp()
		{
			if ((ICardApp)wipper.Wip["DebCardApp"] != null)
				return;

			GetDemand();
			wipper.Wip["DebCardApp"] = DebCardSvc.GetDebitCardApp(wipper.IMap, (IDemand)wipper.Wip["Demand"]);
			((ICardApp)wipper.Wip["DebCardApp"]).Status = CardAppStatus.Pend.ToString();
		}
		void GetDemand()
		{
			if (wipper.Wip["Demand"] != null)
				return;

			IDemand demand = DmdFactory.GetDemand(wipper.IMap, DemandType.DebCardNew.ToString(), true);
			
			IUser user = (IUser)Session["User"];
			demand.StoreCode = user.LoginStoreCode;
			demand.Loc = LocSvc.FindStoreZipId(wipper.IMap, user.LoginStoreCode);
			demand.ConsumerAgent = wipper.Wip.ClerkId; 
			demand.Status = DemandStatus.Pend.ToString();

			wipper.Wip["Demand"] = demand;
			AddDmdItems();
		}
		void AddDmdItems()
		{		
			((IDemand)wipper.Wip["Demand"]).ClearDmdItems();

			if (wipper.Wip["DebCard"] == null)
				wipper.Wip["DebCard"] = DebCardSvc.GetDebCardProd(wipper.IMap, 
									                            ((IUser)HttpContext.Current.Session["User"]).LoginStoreCode);

			wipper.Wip["DItem"] = ProdSvc.AddDmdItem(wipper.IMap, 
				                            (IDemand)wipper.Wip["Demand"], 
				                            (IProdPrice)wipper.Wip["DebCard"],
				                             OrderType.New); 
			
			((IDmdItem)wipper.Wip["DItem"]).DmdItemType = DItemType.Selected.ToString();
			//di.Status = "Pending"; 
		}
		void GetPayInfo()
		{
			if (wipper.Wip["PayInfo"] != null)
				return;

			IPayInfo pi = PaySvc.GetNewPayInfo(wipper.IMap,(IDemand)wipper.Wip["Demand"], PayInfoClass.PayInfo);
			pi.Status = PaymentStatus.Pend.ToString();
			pi.ParDemand = (IDemand)wipper.Wip["Demand"];

			wipper.Wip["PayInfo"] = pi;
		}
	#endregion
	}
}