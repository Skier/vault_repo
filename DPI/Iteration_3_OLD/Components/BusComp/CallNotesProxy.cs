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
	[System.Web.Services.WebServiceBindingAttribute(Name="callNotesSoapBinding", Namespace="urn:callNotes")]
	public class callNotesService : System.Web.Services.Protocols.SoapHttpClientProtocol 
	{
    
		/// <remarks/>
		public callNotesService() 
		{
			string urlSetting = System.Configuration.ConfigurationSettings.AppSettings["SrvPwrUrl"];
			if ((urlSetting != null)) 
			{
				this.Url = urlSetting;
			}
			else 
			{
				this.Url = "http://fssdev.servicepower.com:40223/FssWebservice/services/callNotes";
			}
		}
    
		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
		[return: System.Xml.Serialization.XmlElementAttribute("CallNotesUpdateRsp", Namespace="urn:callNotes")]
		public CallNotesUpdateRsp updateCallNotes([System.Xml.Serialization.XmlElementAttribute(Namespace="urn:callNotes")] CallNotesUpdateReq CallNotesUpdateReq) 
		{
			object[] results = this.Invoke("updateCallNotes", new object[] {
																			   CallNotesUpdateReq});
			return ((CallNotesUpdateRsp)(results[0]));
		}
    
		/// <remarks/>
		public System.IAsyncResult BeginupdateCallNotes(CallNotesUpdateReq CallNotesUpdateReq, System.AsyncCallback callback, object asyncState) 
		{
			return this.BeginInvoke("updateCallNotes", new object[] {
																		CallNotesUpdateReq}, callback, asyncState);
		}
    
		/// <remarks/>
		public CallNotesUpdateRsp EndupdateCallNotes(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((CallNotesUpdateRsp)(results[0]));
		}
    
		/// <remarks/>
		[System.Web.Services.Protocols.SoapDocumentMethodAttribute("", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
		[return: System.Xml.Serialization.XmlElementAttribute("getServicerCallNotesRsp", Namespace="urn:callNotes")]
		public getServicerCallNotesRsp getServicerNotes([System.Xml.Serialization.XmlElementAttribute(Namespace="urn:callNotes")] getServicerCallNotesReq getServicerCallNotesReq) 
		{
			object[] results = this.Invoke("getServicerNotes", new object[] {
																				getServicerCallNotesReq});
			return ((getServicerCallNotesRsp)(results[0]));
		}
    
		/// <remarks/>
		public System.IAsyncResult BegingetServicerNotes(getServicerCallNotesReq getServicerCallNotesReq, System.AsyncCallback callback, object asyncState) 
		{
			return this.BeginInvoke("getServicerNotes", new object[] {
																		 getServicerCallNotesReq}, callback, asyncState);
		}
    
		/// <remarks/>
		public getServicerCallNotesRsp EndgetServicerNotes(System.IAsyncResult asyncResult) 
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((getServicerCallNotesRsp)(results[0]));
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:callNotes")]
	public class CallNotesUpdateReq 
	{
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public UserAuth UserAuthentication;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public string CallNo;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public string CallNotes;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="date")]
		public System.DateTime NotesDate;
	}

	/// <remarks/>
//	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:callNotes")]
//	public class UserAuth 
//	{
//    
//		/// <remarks/>
//		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
//		public string MFGUserID;
//    
//		/// <remarks/>
//		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
//		public string MFGPassword;
//    
//		/// <remarks/>
//		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
//		public string MFGID;
//	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:callNotes")]
	public class Note 
	{
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public System.DateTime createdOn;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string note;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:callNotes")]
	public class CallNotesList 
	{
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public string CallNo;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("notes", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public Note[] notes;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:callNotes")]
	public class getServicerCallNotesRsp 
	{
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public string AckMsg;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public CallNotesList CallNotes;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:callNotes")]
	public class getServicerCallNotesReq 
	{
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public UserAuth UserAuthentication;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
		public string CallNo;
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:callNotes")]
	public class CallNotesUpdateRsp 
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