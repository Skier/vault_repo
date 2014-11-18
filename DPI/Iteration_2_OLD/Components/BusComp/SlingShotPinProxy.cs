using System.Diagnostics;
using System.Xml.Serialization;
using System;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Web.Services;


/// <remarks/>
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name="DynamicPosaSoap", Namespace="https://pos.slingshot.com/PosWebService/DynamicPosa/")]
public class SlingShotPinProxy : System.Web.Services.Protocols.SoapHttpClientProtocol 
{
    
	/// <remarks/>
	public SlingShotPinProxy() 
	{
		string urlSetting = System.Configuration.ConfigurationSettings.AppSettings["SlingshotWebSvcURL"];
		if ((urlSetting != null)) 
		{
			this.Url = urlSetting + "DynamicPosa.asmx";
		}
		else 
		{
			this.Url = "https://recharge.slingshot.com/PosWebService/DynamicPosa.asmx";
		}
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://pos.slingshot.com/PosWebService/DynamicPosa/GetACodeXml", RequestNamespace="https://pos.slingshot.com/PosWebService/DynamicPosa/", ResponseNamespace="https://pos.slingshot.com/PosWebService/DynamicPosa/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public System.Xml.XmlNode GetACodeXml(System.Xml.XmlNode request) 
	{
		object[] results = this.Invoke("GetACodeXml", new object[] {
																	   request});
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginGetACodeXml(System.Xml.XmlNode request, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("GetACodeXml", new object[] {
																request}, callback, asyncState);
	}
    
	/// <remarks/>
	public System.Xml.XmlNode EndGetACodeXml(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://pos.slingshot.com/PosWebService/DynamicPosa/VoidACodeXml", RequestNamespace="https://pos.slingshot.com/PosWebService/DynamicPosa/", ResponseNamespace="https://pos.slingshot.com/PosWebService/DynamicPosa/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public System.Xml.XmlNode VoidACodeXml(System.Xml.XmlNode request) 
	{
		object[] results = this.Invoke("VoidACodeXml", new object[] {
																		request});
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginVoidACodeXml(System.Xml.XmlNode request, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("VoidACodeXml", new object[] {
																 request}, callback, asyncState);
	}
    
	/// <remarks/>
	public System.Xml.XmlNode EndVoidACodeXml(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://pos.slingshot.com/PosWebService/DynamicPosa/UpcForPhoneXml", RequestNamespace="https://pos.slingshot.com/PosWebService/DynamicPosa/", ResponseNamespace="https://pos.slingshot.com/PosWebService/DynamicPosa/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public System.Xml.XmlNode UpcForPhoneXml(System.Xml.XmlNode request) 
	{
		object[] results = this.Invoke("UpcForPhoneXml", new object[] {
																		  request});
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginUpcForPhoneXml(System.Xml.XmlNode request, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("UpcForPhoneXml", new object[] {
																   request}, callback, asyncState);
	}
    
	/// <remarks/>
	public System.Xml.XmlNode EndUpcForPhoneXml(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://pos.slingshot.com/PosWebService/DynamicPosa/GetACode", RequestNamespace="https://pos.slingshot.com/PosWebService/DynamicPosa/", ResponseNamespace="https://pos.slingshot.com/PosWebService/DynamicPosa/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public System.Xml.XmlNode GetACode(string source, string pwd, string tranid, string retailer, string storeid, string upc, System.Decimal price) 
	{
		object[] results = this.Invoke("GetACode", new object[] {
																	source,
																	pwd,
																	tranid,
																	retailer,
																	storeid,
																	upc,
																	price});
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginGetACode(string source, string pwd, string tranid, string retailer, string storeid, string upc, System.Decimal price, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("GetACode", new object[] {
															 source,
															 pwd,
															 tranid,
															 retailer,
															 storeid,
															 upc,
															 price}, callback, asyncState);
	}
    
	/// <remarks/>
	public System.Xml.XmlNode EndGetACode(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://pos.slingshot.com/PosWebService/DynamicPosa/VoidACode", RequestNamespace="https://pos.slingshot.com/PosWebService/DynamicPosa/", ResponseNamespace="https://pos.slingshot.com/PosWebService/DynamicPosa/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public System.Xml.XmlNode VoidACode(string source, string pwd, string tranid, string retailer, string storeid, string acode, int manid) 
	{
		object[] results = this.Invoke("VoidACode", new object[] {
																	 source,
																	 pwd,
																	 tranid,
																	 retailer,
																	 storeid,
																	 acode,
																	 manid});
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginVoidACode(string source, string pwd, string tranid, string retailer, string storeid, string acode, int manid, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("VoidACode", new object[] {
															  source,
															  pwd,
															  tranid,
															  retailer,
															  storeid,
															  acode,
															  manid}, callback, asyncState);
	}
    
	/// <remarks/>
	public System.Xml.XmlNode EndVoidACode(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://pos.slingshot.com/PosWebService/DynamicPosa/UpcForPhone", RequestNamespace="https://pos.slingshot.com/PosWebService/DynamicPosa/", ResponseNamespace="https://pos.slingshot.com/PosWebService/DynamicPosa/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public System.Xml.XmlNode UpcForPhone(string source, string pwd, int areacode, int prefix) 
	{
		object[] results = this.Invoke("UpcForPhone", new object[] {
																	   source,
																	   pwd,
																	   areacode,
																	   prefix});
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginUpcForPhone(string source, string pwd, int areacode, int prefix, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("UpcForPhone", new object[] {
																source,
																pwd,
																areacode,
																prefix}, callback, asyncState);
	}
    
	/// <remarks/>
	public System.Xml.XmlNode EndUpcForPhone(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((System.Xml.XmlNode)(results[0]));
	}
}
