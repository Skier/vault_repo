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
using System.Data.SqlClient;
using System.Text.RegularExpressions;

using DPI.ClientComp; 
using DPI.Services;
using DPI.Ordering;
using DPI.Interfaces;

namespace DPI.Ordering
{
	public class ProductL2 : BasePage
	{
	#region Web Form Designer generated code
		override protected void OnInit(EventArgs ea)
		{
			InitializeComponent();
			base.OnInit(ea);
			//customInit();
		}
		
		private void InitializeComponent()
		{    
			this.btnPrevious.Click += new System.Web.UI.ImageClickEventHandler(this.cmdPrev_Click);
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.cmdNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		protected System.Web.UI.WebControls.CheckBox CheckBox1;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.ImageButton ImageButton2;
		protected System.Web.UI.WebControls.CheckBox c;
		protected System.Web.UI.WebControls.Label lblErrorMsg;
		protected System.Web.UI.WebControls.ImageButton btnNext;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;
		protected System.Web.UI.WebControls.Label lblIlec;
		protected System.Web.UI.WebControls.Label lblZipCode;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label lblOrdTotal;
		protected System.Web.UI.WebControls.PlaceHolder Level2Prods;
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

				loaded = Pre_Load();

				lblZipCode.Text = (string)wipper.Wip["zip"];
				lblIlec.Text = ((IILECInfo)wipper.Wip["selectedilec"]).ILECName;
				customInit();
				
				if (!loaded)
					LoadProdTable();
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
				btnNext.Visible = false;
				lblErrorMsg.Visible = true;
				lblErrorMsg.Text = Const.GENERAL_ERROR;
			}
		}
		void OnCheckedChanged(object sender, EventArgs ea)
		{
			try 
			{
				int prodID = int.Parse(((CheckBox)sender).ID);

				for( int i = 0; i < ((IProdPrice[])wipper.Wip["prods"]).Length; i++)
					if(((IProdPrice[])wipper.Wip["prods"])[i].ProdId == prodID)
						AddRemove(i, ((CheckBox)sender).Checked);

				LoadProdTable();// Repaint to show results of click / unclick
				ResetErrorMsg();
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
				
				btnNext.Visible = false;
				lblErrorMsg.Visible = true;
				lblErrorMsg.Text = Const.GENERAL_ERROR;
			}
		}
		
		void cmdNext_Click(object sender, System.Web.UI.ImageClickEventArgs ea)
		{
			try
			{
				ResetErrorMsg();
				string error 
					= ProdSvc.ValidateOrder(wipper.IMap, (IProdPrice[])wipper.Wip["prods"], (string)wipper.Wip["DmdType"]);
				if (error == string.Empty)
					Response.Redirect(wipper.Wip.Next(), false);
				
				btnNext.Visible = false;
				lblErrorMsg.Visible = true;
				lblErrorMsg.Text = error;
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
				
				btnNext.Visible = false;
				lblErrorMsg.Visible = true;
				lblErrorMsg.Text = Const.GENERAL_ERROR;
			}
		}
		
		void cmdPrev_Click(object sender, System.Web.UI.ImageClickEventArgs ea)
		{
			try
			{
				wipper.Wip["prods"] = null;
				wipper.Wip["SelectedBasicService"] = null;
				ResetErrorMsg();
				Response.Redirect(wipper.Wip.Prev(),false);
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
				
				btnNext.Visible = false;
				lblErrorMsg.Visible = true;
				lblErrorMsg.Text = Const.GENERAL_ERROR;
			}
		}		
	#endregion	

	#region Implementation
		bool Pre_Load()
		{
			MultiClickBlocker.Block(this, btnNext);
			MultiClickBlocker.Block(this, btnPrevious);
			return LoadProdTable();
		}

		void customInit()
		{	
			btnNext.Visible = wipper.Wip.HasNext;
			btnPrevious.Visible = wipper.Wip.HasPrev;
			CheckProducts();
			ResetErrorMsg();
		} 
		void CheckProducts()
		{
			if (wipper.Wip["prods"] != null)
				return;

			wipper.Wip["prods"]	
				= ProdSvc.GetDependentProds(wipper.IMap, 
				 						   (IProdPrice)wipper.Wip["SelectedBasicService"], 
										   (IILECInfo)wipper.Wip["SelectedIlec"], 
									       (string)wipper.Wip["Zip"],
										   GetUser().Role,
										   GetUser().LoginStoreCode,	
										   (string)wipper.Wip["DmdType"]);
		}
		IUser GetUser()
		{
			return (IUser)Session["User"];
		}
		bool LoadProdTable()
		{
			if (wipper.Wip["prods"]	== null)
				return false;

			Level2Prods.Controls.Clear();
			Level2Prods.Controls.Add(new TableProdL2((IProdPrice[])wipper.Wip["prods"],
				new EventHandler(OnCheckedChanged),
				IsPostBack));
		
			lblOrdTotal.Text = ProdL2Total.CalcProdL2Total((IProdPrice[])wipper.Wip["prods"]).ToString("C");
			return true;
		}
		void AddRemove(int prod, bool isChecked)
		{
			if(!isChecked)
			{
				wipper.Wip["prods"] = ProdSvc.RemoveProd(wipper.IMap, (IProdPrice[])wipper.Wip["prods"], 
				                      ((IProdPrice[])wipper.Wip["prods"])[prod]);				
				return;
			}

			wipper.Wip["prods"] = ProdSvc.AddProd(wipper.IMap, (IProdPrice[])wipper.Wip["prods"], 
					                  ((IProdPrice[])wipper.Wip["prods"])[prod]);
		}

		void ResetErrorMsg()
		{
			lblErrorMsg.Visible = false;
			this.btnNext.Visible = true;
		}
		void ErrorHandler(Exception ex)
		{
			FatalError.SaveErr( wipper.IMap, ex, GetUser(), this.ToString());
		}
	}
	#endregion	
}