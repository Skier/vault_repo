//------------------------------------------------------------------------------
//     Runtime Version: 1.1.4322.2032
//------------------------------------------------------------------------------

using System.Diagnostics;
using System.Xml.Serialization;
using System;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Web.Services;

namespace DPI.Components.Partners.ServicePower
{
	/// <remarks/>
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Web.Services.WebServiceBindingAttribute(Name="callCancellationSoapBinding", Namespace="urn:callCancellation")]
	public class callCancellationService : System.Web.Services.Protocols.SoapHttpClientProtocol 
	{
    
		/// <remarks/>
		public callCancellationService() 
		{
			string urlSetting = System.Configuration.ConfigurationSettings.AppSettings["SrvPwrUrl"];
			if ((urlSetting != null)) 
			{
				this.Url = urlSetting;
			}
			else 
			{
				this.Url = "http://fssdev.servicepower.com:40223/FssWebservice/services/callCancellation";
			}
		}
    
		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
		[return: System.Xml.Serialization.XmlElementAttribute("CallCancelRsp", Namespace="urn:callCancellation")]
		public CallCancelRsp getCancellation([System.Xml.Serialization.XmlElementAttribute(Namespace="urn:callCancellation")] CallCancelReq CallCancelReq) 
		{
			object[] results = this.Invoke("getCancellation", new object[] {
																			   CallCancelReq});
			return ((CallCancelRsp)(results[0]));
		}
    
		/// <remarks/>
		public System.IAsyncResult BegingetCancellation(CallCancelReq CallCancelReq, System.AsyncCallback callback, object asyncState) 
		{
			return this.BeginInvoke("getCancellation", new object[] {
																		CallCancelReq}, callback, asyncState);
		}
    
		/// <remarks/>
		public CallCancelRsp EndgetCancellation(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((CallCancelRsp)(results[0]));
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:callCancellation")]
	public class CallCancelReq 
	{
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public UserAuth UserAuthentication;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public string CallNo;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public string CancelReasonCode;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:callCancellation")]
	public class UserAuth 
	{
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string MFGUserID;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string MFGPassword;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string MFGID;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:callCancellation")]
	public class CallCancelRsp 
	{
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public string MFGID;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public string CallNo;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public string AckMsg;
	}

}