using System;
using Dpi.Central.Web.Account;
using Dpi.Central.Web.Controls;

namespace Dpi.Central.Web.Account.AccountSetup
{
	public class BaseAccountSetupPage : BaseAccountPage
	{
        #region Constants

        protected const string ZIP_CODE_FORMAT = "{0} {1}";

        #endregion
	    
	    #region WriteLog

	    protected void WriteLog(string s) {
//	        StreamWriter stream = File.AppendText("C:\\temp\\create_account.log");
//	        stream.WriteLine(DateTime.Now.ToString() + " - " + s);
//	        stream.Close();	        
	    }

	    #endregion

        protected override void InitLayout() 
        {
            base.InitLayout();

            ProcessMap processMap = new ProcessMap();

            processMap.CssClass = "process_map";
            processMap.CssClassPrevious = "previous_step";
            processMap.CssClassCurrent = "current_step";
            processMap.CssClassNext = "next_step";
            processMap.Provider = ProcessMapProvider;

            Form.Controls.AddAt(2, processMap);
        }

        protected override void AddErrorControlToPage() 
        {
            Form.Controls.AddAt(3, ErrorControl);
        }

	    #region OnLoad

	    protected override void OnLoad(EventArgs e)
	    {		        	        
	        if (!(this is SelectProvider) && !(this is ZipRequestProcessor) 
	            && (Model.Info.Zip == null || Model.Info.Zip == string.Empty))
	        {
	            Response.Redirect(SiteMap.NEW_ACC_SELECT_PROVIDER_URL);	        	        
	            return;
	        }
	            
	        
	        base.OnLoad(e);
	    }

	    #endregion

	    #region Properties

	    private AccountSetupProcessMapProvider ProcessMapProvider
	    {
	        get
	        {
	            if (Application["AccountSetupProcessMapProvider"] == null) {
	                Application["AccountSetupProcessMapProvider"] = new AccountSetupProcessMapProvider();
	            }

	            return (AccountSetupProcessMapProvider) Application["AccountSetupProcessMapProvider"];
	        }
	    }

	    #region Model

	    internal virtual AccountSetupModel Model
	    {
	        get
	        {
	            if (Session["AccountSetupModel"] == null)
	                Session["AccountSetupModel"] = new AccountSetupModel(Map);
	            
	            return (AccountSetupModel) Session["AccountSetupModel"];
	        }
	    }

	    #endregion

	    #endregion

	    #region ResetModel

	    protected void ResetModel()
	    {
	        Session["AccountSetupModel"] = null;
	    }

	    #endregion
	}
}
