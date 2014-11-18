using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account.AccountSetup
{
	/// <summary>
	/// Summary description for zip_request_processor.
	/// </summary>
	public class ZipRequestProcessor : BaseAccountSetupPage
	{
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.OnPageLoad);
		}
		#endregion

	    #region OnPageLoad

	    private void OnPageLoad(object sender, EventArgs e) {
	        if(!IsPostBack ) {
	            string newProvider = Request["SelectedProvider"];
	            string zipCode = Request["ZipCode"];
			    			    
	            Response.Clear();
			
	            string output = GetXml(zipCode, newProvider);			    			    
	            output = output.Replace("utf-16", "utf-8");
			    
	            Response.Clear();
	            Response.ContentType ="text/xml";
				
	            Response.Write(output);
	            Response.End();
	        }
	        else {
	            Response.Clear();
	            Response.End();
	        }			
	    }

	    #endregion

	    #region GetXml
	    
	    private string GetNewProviderXml(string zipCode, string newProvider)
	    {	
	        bool isLowIncomeQualifyAllowed = false;
	        
            IProdPrice[] topProducts = ProdSvc.GetTopProd(Map, int.Parse(newProvider), zipCode, "Agent", "DPI-Web", null);
            if (topProducts != null) {
                foreach (IProdPrice product in topProducts) {
                    if (Model.IsProductLifeLine(product)) {
                        isLowIncomeQualifyAllowed = true;
                        break;
                    }
	                    
                }
            }
	        
            XmlDocument document = new XmlDocument();
            XmlElement root = document.CreateElement("root");
            document.AppendChild(root);
	        	        
            XmlElement isLowIncomeQualifyAllowedNode = document.CreateElement("ProviderChangedOnly");
            root.AppendChild(isLowIncomeQualifyAllowedNode);
            isLowIncomeQualifyAllowedNode.InnerText = isLowIncomeQualifyAllowed.ToString().ToLower();
            
            StringBuilder stringBuilder = new StringBuilder();
            StringWriter writer = new StringWriter(stringBuilder);	        

            document.Save(writer);
            return stringBuilder.ToString();	        
	    }

	    private string GetXml(string zipCode, string newProvider) {
	        
	        if (newProvider != null && newProvider.Length != 0)
	            return GetNewProviderXml(zipCode, newProvider);
	        
	        IILECInfo[] providers = Model.GetProviders(zipCode);
	        
	        string errorMessage = string.Empty;
	        bool isLowIncomeQualifyAllowed = false;
	        
            if (providers.Length == 0) {
                errorMessage = Model.GetProvidersErrorMessage(zipCode);
            } else
            {
                IProdPrice[] topProducts = ProdSvc.GetTopProd(Map, providers[0], zipCode, "Agent", "DPI-Web", null);
                if (topProducts != null) {
                    foreach (IProdPrice product in topProducts) {
                        if (Model.IsProductLifeLine(product)) {
                            isLowIncomeQualifyAllowed = true;
                            break;
                        }
	                    
                    }
                }                
            }                              

	        	        
	        XmlDocument providersDoc = new XmlDocument();
	        XmlElement root = providersDoc.CreateElement("root");
	        providersDoc.AppendChild(root);
                       
	        if (errorMessage != string.Empty) //Some Error Occured
	        {
                XmlElement provider = providersDoc.CreateElement("error");
	            provider.InnerText = errorMessage;
                root.AppendChild(provider);	            
	        } else
	        {
	            foreach (IILECInfo info in providers)
	            {
                    XmlElement provider = providersDoc.CreateElement("provider");

                    XmlElement code = providersDoc.CreateElement("code");
                    XmlElement name = providersDoc.CreateElement("name");
                    XmlElement isDefault = providersDoc.CreateElement("isdefault");
	                
                    code.InnerText = info.OrgId.ToString();
                    name.InnerText = info.ILECName;
	                isDefault.InnerText = info.IsDefault.ToString().ToLower();

                    root.AppendChild(provider);
                    provider.AppendChild(code);
                    provider.AppendChild(name);
                    provider.AppendChild(isDefault);	                
	            }	            	            
	        }
	        
            XmlElement isLowIncomeQualifyAllowedNode = providersDoc.CreateElement("IsLowIncomeQualifyAllowedForFirstProvider");
            root.AppendChild(isLowIncomeQualifyAllowedNode);
	        isLowIncomeQualifyAllowedNode.InnerText = isLowIncomeQualifyAllowed.ToString().ToLower();
            
	        StringBuilder stringBuilder = new StringBuilder();
	        StringWriter writer = new StringWriter(stringBuilder);
	        

	        providersDoc.Save(writer);
	        return stringBuilder.ToString();
	        
	    }

	    #endregion

	}
}
