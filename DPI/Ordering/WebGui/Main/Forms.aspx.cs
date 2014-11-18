using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;

using DPI.ClientComp;
using DPI.Ordering;
using DPI.Reports;
using DPI.Interfaces;
using DPI.Services;

namespace DPI.Ordering
{
	public class Forms : System.Web.UI.Page
	{
		#region Member Variables

		protected System.Web.UI.WebControls.Label lblReportMenu;
		protected System.Web.UI.WebControls.ImageButton imgStore_CustomerList;
		protected System.Web.UI.WebControls.ImageButton imgAppLocPhone;
		protected System.Web.UI.WebControls.LinkButton lnkAppLocPhone;
		protected System.Web.UI.WebControls.ImageButton imgNCServiceAg;
		protected System.Web.UI.WebControls.LinkButton lnkNCServiceAg;
		protected System.Web.UI.WebControls.ImageButton imgWebLoa;
		protected System.Web.UI.WebControls.LinkButton lnkWebLoa;
		protected System.Web.UI.WebControls.ImageButton btn_GotoMain;
		protected System.Web.UI.WebControls.ImageButton imgDebCard;
		protected System.Web.UI.WebControls.LinkButton lnkDebCard;
		protected System.Web.UI.WebControls.ImageButton Imagebutton1;
		protected System.Web.UI.WebControls.LinkButton lbFees;
		protected System.Web.UI.WebControls.LinkButton lbIntRates;
		protected System.Web.UI.WebControls.ImageButton imgIntRates;
		protected System.Web.UI.WebControls.ImageButton imgLifeline;
		protected System.Web.UI.WebControls.LinkButton lbLifeline;
		protected System.Web.UI.WebControls.ImageButton imgSatellitePerm;
		protected System.Web.UI.WebControls.LinkButton lbSatellitePerm;
		protected System.Web.UI.WebControls.LinkButton lnkStore_CustomerList;		
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			if(!HttpContext.Current.User.IsInRole(Const.PERMISSION_REPORTING))
				Response.Redirect("MenuScreen.aspx", false);

			InitializeComponent();
			base.OnInit(e);
			foreach(object ctrl in Controls)
				if ((ctrl is System.Web.UI.WebControls.ImageButton) || (ctrl is System.Web.UI.WebControls.LinkButton))
				{
					((System.Web.UI.WebControls.ImageButton)ctrl).Attributes.Add("onClick", "clickedButton=true; ");
					((System.Web.UI.WebControls.LinkButton)ctrl).Attributes.Add("onClick", "clickedButton=true; ");
				}
		}
		
		private void InitializeComponent()
		{    
			this.btn_GotoMain.Click += new System.Web.UI.ImageClickEventHandler(this.btn_GotoMain_Click);
			this.imgAppLocPhone.Click += new System.Web.UI.ImageClickEventHandler(this.imgAppLocPhone_Click);
			this.lnkAppLocPhone.Click += new System.EventHandler(this.lnkAppLocPhone_Click);
			this.imgNCServiceAg.Click += new System.Web.UI.ImageClickEventHandler(this.imgNCServiceAg_Click);
			this.lnkNCServiceAg.Click += new System.EventHandler(this.lnkNCServiceAg_Click);
			this.imgWebLoa.Click += new System.Web.UI.ImageClickEventHandler(this.imgWebLoa_Click);
			this.lnkWebLoa.Click += new System.EventHandler(this.lnkWebLoa_Click);
			this.imgDebCard.Click += new System.Web.UI.ImageClickEventHandler(this.imgDebCard_Click);
			this.lnkDebCard.Click += new System.EventHandler(this.lnkDebCard_Click);
			this.Imagebutton1.Click += new System.Web.UI.ImageClickEventHandler(this.Imagebutton1_Click);
			this.lbFees.Click += new System.EventHandler(this.lbFees_Click);
			this.imgIntRates.Click += new System.Web.UI.ImageClickEventHandler(this.imgIntRates_Click);
			this.lbIntRates.Click += new System.EventHandler(this.lbIntRates_Click);
			this.imgLifeline.Click += new System.Web.UI.ImageClickEventHandler(this.imgLifeline_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}

		protected System.Web.UI.HtmlControls.HtmlTable ReportMenu;
		#endregion	
	
		#region Event Handlers

		private void Page_Load(object sender, System.EventArgs e)
		{
			SetAttributes();			
		}

		void lnkAppLocPhone_Click(object sender, System.EventArgs e)
		{
		}

		void imgAppLocPhone_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			lnkAppLocPhone_Click(sender, e);
		}

		void lnkNCServiceAg_Click(object sender, System.EventArgs e)
		{
		}
		private void imgNCServiceAg_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			lnkNCServiceAg_Click(sender, e);
		}

		private void lnkWebLoa_Click(object sender, System.EventArgs e)
		{
		}
		void imgWebLoa_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			lnkWebLoa_Click(sender, e);
		}

		void lnkDebCard_Click(object sender, System.EventArgs e)
		{
		
		}
		void imgDebCard_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			lnkDebCard_Click(sender, e);
		}
		
		void btn_GotoMain_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Session["IMap"] = null;
			Response.Redirect("MenuScreen.aspx", false);
		}

		void lbFees_Click(object sender, System.EventArgs e)
		{
		
		}

		#endregion

		#region Implementations
		
		void SetAttributes()
		{
			imgAppLocPhone.Attributes.Add("onclick","window.open('" + GetDocPath() + "/Application for Local Telephone Service.pdf', '_blank' ,'height= 600, width=660 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			lnkAppLocPhone.Attributes.Add("onclick","window.open('" + GetDocPath() + "/Application for Local Telephone Service.pdf', '_blank' ,'height= 600, width=660 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			imgNCServiceAg.Attributes.Add("onclick","window.open('" + GetDocPath() + "/North Carolina Service Agreement.pdf', '_blank' ,'height= 600, width=660 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			lnkNCServiceAg.Attributes.Add("onclick","window.open('" + GetDocPath() + "/North Carolina Service Agreement.pdf', '_blank' ,'height= 600, width=660 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			imgWebLoa.Attributes.Add("onclick","window.open('" + GetDocPath() + "/WEB LOA.pdf', '_blank' ,'height= 600, width=660 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			lnkWebLoa.Attributes.Add("onclick","window.open('" + GetDocPath() + "/WEB LOA.pdf', '_blank' ,'height= 600, width=660 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			imgDebCard.Attributes.Add("onclick","window.open('" + GetDocPath() + "/LoadForm.pdf', '_blank' ,'height= 600, width=660 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			lnkDebCard.Attributes.Add("onclick","window.open('" + GetDocPath() + "/LoadForm.pdf', '_blank' ,'height= 600, width=660 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			lbFees.Attributes.Add("onclick","window.open('" + GetDocPath() + "/ScheduleOfCardholderFees.pdf', '_blank' ,'height= 600, width=660 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			Imagebutton1.Attributes.Add("onclick","window.open('" + GetDocPath() + "/ScheduleOfCardholderFees.pdf', '_blank' ,'height= 600, width=660 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			lbIntRates.Attributes.Add("onclick","window.open('" + GetDocPath() + "/InternationalTerminationRates.pdf', '_blank' ,'height= 600, width=660 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			imgIntRates.Attributes.Add("onclick","window.open('" + GetDocPath() + "/InternationalTerminationRates.pdf', '_blank' ,'height= 600, width=660 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			lbLifeline.Attributes.Add("onclick","window.open('" + GetDocPath() + "/Lifeline Application.pdf', '_blank' ,'height= 600, width=660 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			imgLifeline.Attributes.Add("onclick","window.open('" + GetDocPath() + "/Lifeline Application.pdf', '_blank' ,'height= 600, width=660 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			lbSatellitePerm.Attributes.Add("onclick","window.open('" + GetDocPath() + "/Landlord Permission Form.pdf', '_blank' ,'height= 600, width=660 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			imgSatellitePerm.Attributes.Add("onclick","window.open('" + GetDocPath() + "/Landlord Permission Form.pdf', '_blank' ,'height= 600, width=660 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
		}
		
		
		string GetDocPath()
		{
			return ConfigurationSettings.AppSettings["DocPath"];			
		}

		#endregion

		void lbIntRates_Click(object sender, System.EventArgs e)
		{
		
		}

		void Imagebutton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
		
		}

		void imgIntRates_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			lbIntRates_Click(sender, e);
		}

		void imgLifeline_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
		
		}

		
	}
}
