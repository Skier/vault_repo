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
	[System.Web.Services.WebServiceBindingAttribute(Name="callStatusEnquirySoapBinding", Namespace="urn:callStatusEnquiry")]
	public class callStatusEnquiryServicet : System.Web.Services.Protocols.SoapHttpClientProtocol 
	{
    
		/// <remarks/>
		public callStatusEnquiryServicet() 
		{
			string urlSetting = System.Configuration.ConfigurationSettings.AppSettings["SrvPwrUrl"];
			if ((urlSetting != null)) 
			{
				this.Url = urlSetting;
			}
			else 
			{
				this.Url = "http://fssdev.servicepower.com:40223/FssWebservice/services/callStatusEnquiry";
			}
		}
    
		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
		[return: System.Xml.Serialization.XmlElementAttribute("callStatusEnquiryRsp", Namespace="urn:callStatusEnquiry")]
		public callStatusEnquiryRsp getStatusEnquiry([System.Xml.Serialization.XmlElementAttribute(Namespace="urn:callStatusEnquiry")] callStatusEnquiryReq in0) 
		{
			object[] results = this.Invoke("getStatusEnquiry", new object[] {
																				in0});
			return ((callStatusEnquiryRsp)(results[0]));
		}
    
		/// <remarks/>
		public System.IAsyncResult BegingetStatusEnquiry(callStatusEnquiryReq in0, System.AsyncCallback callback, object asyncState) 
		{
			return this.BeginInvoke("getStatusEnquiry", new object[] {
																		 in0}, callback, asyncState);
		}
    
		/// <remarks/>
		public callStatusEnquiryRsp EndgetStatusEnquiry(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((callStatusEnquiryRsp)(results[0]));
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:callStatusEnquiry")]
	public class callStatusEnquiryReq 
	{
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public CallStatusEnquiryReqUserAuthentication UserAuthentication;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public string CallNo;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="date")]
		public System.DateTime StartDate;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="date")]
		public System.DateTime EndDate;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public System.DateTime StartTime;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public System.DateTime EndTime;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public CallStatusEnquiryReqCallStatus CallStatus;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool CallStatusSpecified;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public CallStatusEnquiryReqSecStatus SecStatus;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool SecStatusSpecified;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:callStatusEnquiry")]
	public class CallStatusEnquiryReqUserAuthentication 
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
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:callStatusEnquiry")]
	public class CallStatusEnquiryRspSvcrStatCollection 
	{
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public string MFGID;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public string CallNo;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public string ServiceCenterID;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public string SvcrName;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public CallStatusEnquiryRspSvcrStatCollectionCallStatus CallStatus;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool CallStatusSpecified;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public string SecStatus;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public System.Single ExpiryTime;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public System.Single ElapsedTime;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="date")]
		public System.DateTime SchdDelDate;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public string SchdDelTime;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public string DispatchCode;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public string DispatchStatus;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public System.DateTime UpdatedDate;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public string SubStatus;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public string ReasonCode;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:callStatusEnquiry")]
	public enum CallStatusEnquiryRspSvcrStatCollectionCallStatus 
	{
    
		/// <remarks/>
		Open,
    
		/// <remarks/>
		Accepted,
    
		/// <remarks/>
		Completed,
    
		/// <remarks/>
		Cancelled,
    
		/// <remarks/>
		Rejected,
    
		/// <remarks/>
		Rescheduled,
    
		/// <remarks/>
		Claimed,
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:callStatusEnquiry")]
	public class callStatusEnquiryRsp 
	{
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public string AckMsg;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("SvcrStatCollection", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public CallStatusEnquiryRspSvcrStatCollection[] SvcrStatCollection;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:callStatusEnquiry")]
	public enum CallStatusEnquiryReqCallStatus 
	{
    
		/// <remarks/>
		ALL,
    
		/// <remarks/>
		Open,
    
		/// <remarks/>
		Accepted,
    
		/// <remarks/>
		Cancelled,
    
		/// <remarks/>
		Completed,
    
		/// <remarks/>
		Rejected,
    
		/// <remarks/>
		Rescheduled,
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:callStatusEnquiry")]
	public enum CallStatusEnquiryReqSecStatus 
	{
    
		/// <remarks/>
		ALL,
    
		/// <remarks/>
		UNASSIGNED,
    
		/// <remarks/>
		MAXATTEMPT,
    
		/// <remarks/>
		MAXTIME,
	}

}