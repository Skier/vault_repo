﻿using System.Diagnostics;
using System.Xml.Serialization;
using System;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Web.Services;


/// <remarks/>
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name="PaceWSSoap", Namespace="http://192.168.30.12/PaceWS/PaceWS")]
public class TelispireWS : System.Web.Services.Protocols.SoapHttpClientProtocol 
{
    
	/// <remarks/>
	public TelispireWS() 
	{
		string urlSetting = System.Configuration.ConfigurationSettings.AppSettings["TelispireWebSvcURL"];
		if ((urlSetting != null)) 
		{
			this.Url = urlSetting;
		}
		else 
		{
			this.Url = "https://www.paceb2b.com/pacewsv2/pacews.asmx";
		}
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://192.168.30.12/PaceWS/PaceWS/SetSubscriberData", RequestNamespace="http://192.168.30.12/PaceWS/PaceWS", ResponseNamespace="http://192.168.30.12/PaceWS/PaceWS", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public System.Xml.XmlNode SetSubscriberData(string PhoneNumber, string FirstName, string LastName, string City, string State, string Zip, string UserName, string Password) 
	{
		object[] results = this.Invoke("SetSubscriberData", new object[] {
																			 PhoneNumber,
																			 FirstName,
																			 LastName,
																			 City,
																			 State,
																			 Zip,
																			 UserName,
																			 Password});
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginSetSubscriberData(string PhoneNumber, string FirstName, string LastName, string City, string State, string Zip, string UserName, string Password, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("SetSubscriberData", new object[] {
																	  PhoneNumber,
																	  FirstName,
																	  LastName,
																	  City,
																	  State,
																	  Zip,
																	  UserName,
																	  Password}, callback, asyncState);
	}
    
	/// <remarks/>
	public System.Xml.XmlNode EndSetSubscriberData(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://192.168.30.12/PaceWS/PaceWS/GetSubscriberData", RequestNamespace="http://192.168.30.12/PaceWS/PaceWS", ResponseNamespace="http://192.168.30.12/PaceWS/PaceWS", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public System.Xml.XmlNode GetSubscriberData(string PhoneNumber, string UserName, string Password) 
	{
		object[] results = this.Invoke("GetSubscriberData", new object[] {
																			 PhoneNumber,
																			 UserName,
																			 Password});
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginGetSubscriberData(string PhoneNumber, string UserName, string Password, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("GetSubscriberData", new object[] {
																	  PhoneNumber,
																	  UserName,
																	  Password}, callback, asyncState);
	}
    
	/// <remarks/>
	public System.Xml.XmlNode EndGetSubscriberData(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://192.168.30.12/PaceWS/PaceWS/GetServicePlanList", RequestNamespace="http://192.168.30.12/PaceWS/PaceWS", ResponseNamespace="http://192.168.30.12/PaceWS/PaceWS", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public System.Xml.XmlNode GetServicePlanList(string PhoneNumber, string ESN, string UserName, string Password) 
	{
		object[] results = this.Invoke("GetServicePlanList", new object[] {
																			  PhoneNumber,
																			  ESN,
																			  UserName,
																			  Password});
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginGetServicePlanList(string PhoneNumber, string ESN, string UserName, string Password, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("GetServicePlanList", new object[] {
																	   PhoneNumber,
																	   ESN,
																	   UserName,
																	   Password}, callback, asyncState);
	}
    
	/// <remarks/>
	public System.Xml.XmlNode EndGetServicePlanList(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://192.168.30.12/PaceWS/PaceWS/GetWirelessDeviceData", RequestNamespace="http://192.168.30.12/PaceWS/PaceWS", ResponseNamespace="http://192.168.30.12/PaceWS/PaceWS", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public System.Xml.XmlNode GetWirelessDeviceData(string PhoneNumber, string ESN, string UserName, string Password) 
	{
		object[] results = this.Invoke("GetWirelessDeviceData", new object[] {
																				 PhoneNumber,
																				 ESN,
																				 UserName,
																				 Password});
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginGetWirelessDeviceData(string PhoneNumber, string ESN, string UserName, string Password, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("GetWirelessDeviceData", new object[] {
																		  PhoneNumber,
																		  ESN,
																		  UserName,
																		  Password}, callback, asyncState);
	}
    
	/// <remarks/>
	public System.Xml.XmlNode EndGetWirelessDeviceData(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://192.168.30.12/PaceWS/PaceWS/GetServicePlanData", RequestNamespace="http://192.168.30.12/PaceWS/PaceWS", ResponseNamespace="http://192.168.30.12/PaceWS/PaceWS", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public System.Xml.XmlNode GetServicePlanData(string PhoneNumber, string ESN, string CallHistoryStartDate, string CallhistoryEndDate, string UserName, string Password) 
	{
		object[] results = this.Invoke("GetServicePlanData", new object[] {
																			  PhoneNumber,
																			  ESN,
																			  CallHistoryStartDate,
																			  CallhistoryEndDate,
																			  UserName,
																			  Password});
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginGetServicePlanData(string PhoneNumber, string ESN, string CallHistoryStartDate, string CallhistoryEndDate, string UserName, string Password, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("GetServicePlanData", new object[] {
																	   PhoneNumber,
																	   ESN,
																	   CallHistoryStartDate,
																	   CallhistoryEndDate,
																	   UserName,
																	   Password}, callback, asyncState);
	}
    
	/// <remarks/>
	public System.Xml.XmlNode EndGetServicePlanData(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://192.168.30.12/PaceWS/PaceWS/ReplenishServicePlan", RequestNamespace="http://192.168.30.12/PaceWS/PaceWS", ResponseNamespace="http://192.168.30.12/PaceWS/PaceWS", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public System.Xml.XmlNode ReplenishServicePlan(string PIN, string PhoneNumber, string UserName, string Password) 
	{
		object[] results = this.Invoke("ReplenishServicePlan", new object[] {
																				PIN,
																				PhoneNumber,
																				UserName,
																				Password});
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginReplenishServicePlan(string PIN, string PhoneNumber, string UserName, string Password, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("ReplenishServicePlan", new object[] {
																		 PIN,
																		 PhoneNumber,
																		 UserName,
																		 Password}, callback, asyncState);
	}
    
	/// <remarks/>
	public System.Xml.XmlNode EndReplenishServicePlan(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://192.168.30.12/PaceWS/PaceWS/ReplenishServicePlanBySubID", RequestNamespace="http://192.168.30.12/PaceWS/PaceWS", ResponseNamespace="http://192.168.30.12/PaceWS/PaceWS", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public System.Xml.XmlNode ReplenishServicePlanBySubID(string PIN, string SubscriberID, string DeviceID, string UserName, string Password) 
	{
		object[] results = this.Invoke("ReplenishServicePlanBySubID", new object[] {
																					   PIN,
																					   SubscriberID,
																					   DeviceID,
																					   UserName,
																					   Password});
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginReplenishServicePlanBySubID(string PIN, string SubscriberID, string DeviceID, string UserName, string Password, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("ReplenishServicePlanBySubID", new object[] {
																				PIN,
																				SubscriberID,
																				DeviceID,
																				UserName,
																				Password}, callback, asyncState);
	}
    
	/// <remarks/>
	public System.Xml.XmlNode EndReplenishServicePlanBySubID(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://192.168.30.12/PaceWS/PaceWS/ActivatePhone", RequestNamespace="http://192.168.30.12/PaceWS/PaceWS", ResponseNamespace="http://192.168.30.12/PaceWS/PaceWS", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public System.Xml.XmlNode ActivatePhone(string NewESN, string PIN, string ZIP, string CSA, string UserName, string Password) 
	{
		object[] results = this.Invoke("ActivatePhone", new object[] {
																		 NewESN,
																		 PIN,
																		 ZIP,
																		 CSA,
																		 UserName,
																		 Password});
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginActivatePhone(string NewESN, string PIN, string ZIP, string CSA, string UserName, string Password, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("ActivatePhone", new object[] {
																  NewESN,
																  PIN,
																  ZIP,
																  CSA,
																  UserName,
																  Password}, callback, asyncState);
	}
    
	/// <remarks/>
	public System.Xml.XmlNode EndActivatePhone(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://192.168.30.12/PaceWS/PaceWS/DeactivatePhone", RequestNamespace="http://192.168.30.12/PaceWS/PaceWS", ResponseNamespace="http://192.168.30.12/PaceWS/PaceWS", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public System.Xml.XmlNode DeactivatePhone(string MDN, string UserName, string Password) 
	{
		object[] results = this.Invoke("DeactivatePhone", new object[] {
																		   MDN,
																		   UserName,
																		   Password});
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginDeactivatePhone(string MDN, string UserName, string Password, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("DeactivatePhone", new object[] {
																	MDN,
																	UserName,
																	Password}, callback, asyncState);
	}
    
	/// <remarks/>
	public System.Xml.XmlNode EndDeactivatePhone(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://192.168.30.12/PaceWS/PaceWS/SwapESN", RequestNamespace="http://192.168.30.12/PaceWS/PaceWS", ResponseNamespace="http://192.168.30.12/PaceWS/PaceWS", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public System.Xml.XmlNode SwapESN(string NewESN, string ESN, string UserName, string Password) 
	{
		object[] results = this.Invoke("SwapESN", new object[] {
																   NewESN,
																   ESN,
																   UserName,
																   Password});
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginSwapESN(string NewESN, string ESN, string UserName, string Password, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("SwapESN", new object[] {
															NewESN,
															ESN,
															UserName,
															Password}, callback, asyncState);
	}
    
	/// <remarks/>
	public System.Xml.XmlNode EndSwapESN(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://192.168.30.12/PaceWS/PaceWS/CheckActivation", RequestNamespace="http://192.168.30.12/PaceWS/PaceWS", ResponseNamespace="http://192.168.30.12/PaceWS/PaceWS", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public System.Xml.XmlNode CheckActivation(string ESN, string UserName, string Password) 
	{
		object[] results = this.Invoke("CheckActivation", new object[] {
																		   ESN,
																		   UserName,
																		   Password});
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginCheckActivation(string ESN, string UserName, string Password, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("CheckActivation", new object[] {
																	ESN,
																	UserName,
																	Password}, callback, asyncState);
	}
    
	/// <remarks/>
	public System.Xml.XmlNode EndCheckActivation(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://192.168.30.12/PaceWS/PaceWS/ResetPin", RequestNamespace="http://192.168.30.12/PaceWS/PaceWS", ResponseNamespace="http://192.168.30.12/PaceWS/PaceWS", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public System.Xml.XmlNode ResetPin(string PIN, string UserName, string Password) 
	{
		object[] results = this.Invoke("ResetPin", new object[] {
																	PIN,
																	UserName,
																	Password});
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginResetPin(string PIN, string UserName, string Password, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("ResetPin", new object[] {
															 PIN,
															 UserName,
															 Password}, callback, asyncState);
	}
    
	/// <remarks/>
	public System.Xml.XmlNode EndResetPin(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://192.168.30.12/PaceWS/PaceWS/GeneratePINs", RequestNamespace="http://192.168.30.12/PaceWS/PaceWS", ResponseNamespace="http://192.168.30.12/PaceWS/PaceWS", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public System.Xml.XmlNode GeneratePINs(string ItemNumber, string AccountNumber, string Qty, string UserName, string Password) 
	{
		object[] results = this.Invoke("GeneratePINs", new object[] {
																		ItemNumber,
																		AccountNumber,
																		Qty,
																		UserName,
																		Password});
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginGeneratePINs(string ItemNumber, string AccountNumber, string Qty, string UserName, string Password, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("GeneratePINs", new object[] {
																 ItemNumber,
																 AccountNumber,
																 Qty,
																 UserName,
																 Password}, callback, asyncState);
	}
    
	/// <remarks/>
	public System.Xml.XmlNode EndGeneratePINs(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://192.168.30.12/PaceWS/PaceWS/GetPins", RequestNamespace="http://192.168.30.12/PaceWS/PaceWS", ResponseNamespace="http://192.168.30.12/PaceWS/PaceWS", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public System.Xml.XmlNode GetPins(string LotNumber, string username, string password) 
	{
		object[] results = this.Invoke("GetPins", new object[] {
																   LotNumber,
																   username,
																   password});
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginGetPins(string LotNumber, string username, string password, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("GetPins", new object[] {
															LotNumber,
															username,
															password}, callback, asyncState);
	}
    
	/// <remarks/>
	public System.Xml.XmlNode EndGetPins(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://192.168.30.12/PaceWS/PaceWS/DisablePIN", RequestNamespace="http://192.168.30.12/PaceWS/PaceWS", ResponseNamespace="http://192.168.30.12/PaceWS/PaceWS", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public System.Xml.XmlNode DisablePIN(string PIN, string username, string password) 
	{
		object[] results = this.Invoke("DisablePIN", new object[] {
																	  PIN,
																	  username,
																	  password});
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginDisablePIN(string PIN, string username, string password, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("DisablePIN", new object[] {
															   PIN,
															   username,
															   password}, callback, asyncState);
	}
    
	/// <remarks/>
	public System.Xml.XmlNode EndDisablePIN(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://192.168.30.12/PaceWS/PaceWS/EnablePIN", RequestNamespace="http://192.168.30.12/PaceWS/PaceWS", ResponseNamespace="http://192.168.30.12/PaceWS/PaceWS", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public System.Xml.XmlNode EnablePIN(string PIN, string username, string password) 
	{
		object[] results = this.Invoke("EnablePIN", new object[] {
																	 PIN,
																	 username,
																	 password});
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginEnablePIN(string PIN, string username, string password, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("EnablePIN", new object[] {
															  PIN,
															  username,
															  password}, callback, asyncState);
	}
    
	/// <remarks/>
	public System.Xml.XmlNode EndEnablePIN(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://192.168.30.12/PaceWS/PaceWS/TransferFunds", RequestNamespace="http://192.168.30.12/PaceWS/PaceWS", ResponseNamespace="http://192.168.30.12/PaceWS/PaceWS", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public System.Xml.XmlNode TransferFunds(string FromAccount, string ToAccount, string Amount, string VendorTransactionID, string Comments, string UserName, string Password) 
	{
		object[] results = this.Invoke("TransferFunds", new object[] {
																		 FromAccount,
																		 ToAccount,
																		 Amount,
																		 VendorTransactionID,
																		 Comments,
																		 UserName,
																		 Password});
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginTransferFunds(string FromAccount, string ToAccount, string Amount, string VendorTransactionID, string Comments, string UserName, string Password, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("TransferFunds", new object[] {
																  FromAccount,
																  ToAccount,
																  Amount,
																  VendorTransactionID,
																  Comments,
																  UserName,
																  Password}, callback, asyncState);
	}
    
	/// <remarks/>
	public System.Xml.XmlNode EndTransferFunds(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://192.168.30.12/PaceWS/PaceWS/GetAvailableBalance", RequestNamespace="http://192.168.30.12/PaceWS/PaceWS", ResponseNamespace="http://192.168.30.12/PaceWS/PaceWS", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public System.Xml.XmlNode GetAvailableBalance(string PhoneNumber, string ESN, string UserName, string Password) 
	{
		object[] results = this.Invoke("GetAvailableBalance", new object[] {
																			   PhoneNumber,
																			   ESN,
																			   UserName,
																			   Password});
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginGetAvailableBalance(string PhoneNumber, string ESN, string UserName, string Password, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("GetAvailableBalance", new object[] {
																		PhoneNumber,
																		ESN,
																		UserName,
																		Password}, callback, asyncState);
	}
    
	/// <remarks/>
	public System.Xml.XmlNode EndGetAvailableBalance(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://192.168.30.12/PaceWS/PaceWS/SubmitReport", RequestNamespace="http://192.168.30.12/PaceWS/PaceWS", ResponseNamespace="http://192.168.30.12/PaceWS/PaceWS", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public System.Xml.XmlNode SubmitReport(string ReportType, string AccountNumber, string StartDate, string EndDate, string UserName, string Password) 
	{
		object[] results = this.Invoke("SubmitReport", new object[] {
																		ReportType,
																		AccountNumber,
																		StartDate,
																		EndDate,
																		UserName,
																		Password});
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginSubmitReport(string ReportType, string AccountNumber, string StartDate, string EndDate, string UserName, string Password, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("SubmitReport", new object[] {
																 ReportType,
																 AccountNumber,
																 StartDate,
																 EndDate,
																 UserName,
																 Password}, callback, asyncState);
	}
    
	/// <remarks/>
	public System.Xml.XmlNode EndSubmitReport(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://192.168.30.12/PaceWS/PaceWS/RetrieveReport", RequestNamespace="http://192.168.30.12/PaceWS/PaceWS", ResponseNamespace="http://192.168.30.12/PaceWS/PaceWS", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public System.Xml.XmlNode RetrieveReport(string ReportID, string UserName, string Password) 
	{
		object[] results = this.Invoke("RetrieveReport", new object[] {
																		  ReportID,
																		  UserName,
																		  Password});
		return ((System.Xml.XmlNode)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BeginRetrieveReport(string ReportID, string UserName, string Password, System.AsyncCallback callback, object asyncState) 
	{
		return this.BeginInvoke("RetrieveReport", new object[] {
																   ReportID,
																   UserName,
																   Password}, callback, asyncState);
	}
    
	/// <remarks/>
	public System.Xml.XmlNode EndRetrieveReport(System.IAsyncResult asyncResult) 
	{
		object[] results = this.EndInvoke(asyncResult);
		return ((System.Xml.XmlNode)(results[0]));
	}
}
