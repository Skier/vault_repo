using System.Diagnostics;
using System.Xml.Serialization;
using System;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Web.Services;


/// <remarks/>
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name="GatewaySoap", Namespace="http://localhost/oc.portal.gravity/")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseInformation))]
public class GravityProxy : System.Web.Services.Protocols.SoapHttpClientProtocol
{
	
	/// <remarks/>
	public GravityProxy() {
		this.Url = "http://gravity.telecomh2o.com/gateway.asmx";
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/HelloWorld", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public string HelloWorld() {
		object[] results = this.Invoke("HelloWorld", new object[0]);
		return ((string)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginHelloWorld(System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("HelloWorld", new object[0], callback, asyncState);
	}
	
	/// <remarks/>
	public string EndHelloWorld(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((string)(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/GetStateByTN", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public string GetStateByTN(string SecureKey, string TN) {
		object[] results = this.Invoke("GetStateByTN", new object[] {
					SecureKey,
					TN});
		return ((string)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginGetStateByTN(string SecureKey, string TN, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("GetStateByTN", new object[] {
					SecureKey,
					TN}, callback, asyncState);
	}
	
	/// <remarks/>
	public string EndGetStateByTN(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((string)(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/FaxSearchByDateRange", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public Fax_Log_Information[] FaxSearchByDateRange(string SecureKey, System.DateTime DateStart, System.DateTime DateEnd) {
		object[] results = this.Invoke("FaxSearchByDateRange", new object[] {
					SecureKey,
					DateStart,
					DateEnd});
		return ((Fax_Log_Information[])(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginFaxSearchByDateRange(string SecureKey, System.DateTime DateStart, System.DateTime DateEnd, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("FaxSearchByDateRange", new object[] {
					SecureKey,
					DateStart,
					DateEnd}, callback, asyncState);
	}
	
	/// <remarks/>
	public Fax_Log_Information[] EndFaxSearchByDateRange(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((Fax_Log_Information[])(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/FaxSearchByPON", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public Fax_Log_Information[] FaxSearchByPON(string SecureKey, string PON) {
		object[] results = this.Invoke("FaxSearchByPON", new object[] {
					SecureKey,
					PON});
		return ((Fax_Log_Information[])(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginFaxSearchByPON(string SecureKey, string PON, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("FaxSearchByPON", new object[] {
					SecureKey,
					PON}, callback, asyncState);
	}
	
	/// <remarks/>
	public Fax_Log_Information[] EndFaxSearchByPON(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((Fax_Log_Information[])(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/FaxSearchErrorsByDateRange", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public Fax_Log_Information[] FaxSearchErrorsByDateRange(string SecureKey, System.DateTime DateStart, System.DateTime DateEnd) {
		object[] results = this.Invoke("FaxSearchErrorsByDateRange", new object[] {
					SecureKey,
					DateStart,
					DateEnd});
		return ((Fax_Log_Information[])(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginFaxSearchErrorsByDateRange(string SecureKey, System.DateTime DateStart, System.DateTime DateEnd, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("FaxSearchErrorsByDateRange", new object[] {
					SecureKey,
					DateStart,
					DateEnd}, callback, asyncState);
	}
	
	/// <remarks/>
	public Fax_Log_Information[] EndFaxSearchErrorsByDateRange(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((Fax_Log_Information[])(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/FaxSearchSuccessByDateRange", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public Fax_Log_Information[] FaxSearchSuccessByDateRange(string SecureKey, System.DateTime DateStart, System.DateTime DateEnd) {
		object[] results = this.Invoke("FaxSearchSuccessByDateRange", new object[] {
					SecureKey,
					DateStart,
					DateEnd});
		return ((Fax_Log_Information[])(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginFaxSearchSuccessByDateRange(string SecureKey, System.DateTime DateStart, System.DateTime DateEnd, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("FaxSearchSuccessByDateRange", new object[] {
					SecureKey,
					DateStart,
					DateEnd}, callback, asyncState);
	}
	
	/// <remarks/>
	public Fax_Log_Information[] EndFaxSearchSuccessByDateRange(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((Fax_Log_Information[])(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/FaxSearchPendingByDateRange", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public Fax_Log_Information[] FaxSearchPendingByDateRange(string SecureKey, System.DateTime DateStart, System.DateTime DateEnd) {
		object[] results = this.Invoke("FaxSearchPendingByDateRange", new object[] {
					SecureKey,
					DateStart,
					DateEnd});
		return ((Fax_Log_Information[])(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginFaxSearchPendingByDateRange(string SecureKey, System.DateTime DateStart, System.DateTime DateEnd, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("FaxSearchPendingByDateRange", new object[] {
					SecureKey,
					DateStart,
					DateEnd}, callback, asyncState);
	}
	
	/// <remarks/>
	public Fax_Log_Information[] EndFaxSearchPendingByDateRange(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((Fax_Log_Information[])(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/FaxSearchPending", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public Fax_Log_Information[] FaxSearchPending(string SecureKey, System.DateTime DateStart, System.DateTime DateEnd) {
		object[] results = this.Invoke("FaxSearchPending", new object[] {
					SecureKey,
					DateStart,
					DateEnd});
		return ((Fax_Log_Information[])(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginFaxSearchPending(string SecureKey, System.DateTime DateStart, System.DateTime DateEnd, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("FaxSearchPending", new object[] {
					SecureKey,
					DateStart,
					DateEnd}, callback, asyncState);
	}
	
	/// <remarks/>
	public Fax_Log_Information[] EndFaxSearchPending(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((Fax_Log_Information[])(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/Availability", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public Availability_Response Availability(Availability_Request myRequest) {
		object[] results = this.Invoke("Availability", new object[] {
					myRequest});
		return ((Availability_Response)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginAvailability(Availability_Request myRequest, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("Availability", new object[] {
					myRequest}, callback, asyncState);
	}
	
	/// <remarks/>
	public Availability_Response EndAvailability(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((Availability_Response)(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/AvailableTNs", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public AvailableTNs_Response AvailableTNs(AvailableTNs_Request myRequest) {
		object[] results = this.Invoke("AvailableTNs", new object[] {
					myRequest});
		return ((AvailableTNs_Response)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginAvailableTNs(AvailableTNs_Request myRequest, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("AvailableTNs", new object[] {
					myRequest}, callback, asyncState);
	}
	
	/// <remarks/>
	public AvailableTNs_Response EndAvailableTNs(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((AvailableTNs_Response)(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/CancelTN", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public CancelTN_Response CancelTN(CancelTN_Request myRequest) {
		object[] results = this.Invoke("CancelTN", new object[] {
					myRequest});
		return ((CancelTN_Response)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginCancelTN(CancelTN_Request myRequest, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("CancelTN", new object[] {
					myRequest}, callback, asyncState);
	}
	
	/// <remarks/>
	public CancelTN_Response EndCancelTN(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((CancelTN_Response)(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/CancelOrder", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public Order_Response CancelOrder(string SecureKey, int ProfileId, string Pon, string Tn, Enum_Product_Type ProductType, string State, Enum_Order_Type OrderType) {
		object[] results = this.Invoke("CancelOrder", new object[] {
					SecureKey,
					ProfileId,
					Pon,
					Tn,
					ProductType,
					State,
					OrderType});
		return ((Order_Response)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginCancelOrder(string SecureKey, int ProfileId, string Pon, string Tn, Enum_Product_Type ProductType, string State, Enum_Order_Type OrderType, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("CancelOrder", new object[] {
					SecureKey,
					ProfileId,
					Pon,
					Tn,
					ProductType,
					State,
					OrderType}, callback, asyncState);
	}
	
	/// <remarks/>
	public Order_Response EndCancelOrder(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((Order_Response)(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/EstimateDueDate", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public EstimateDueDate_Response EstimateDueDate(EstimateDueDate_Request myRequest) {
		object[] results = this.Invoke("EstimateDueDate", new object[] {
					myRequest});
		return ((EstimateDueDate_Response)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginEstimateDueDate(EstimateDueDate_Request myRequest, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("EstimateDueDate", new object[] {
					myRequest}, callback, asyncState);
	}
	
	/// <remarks/>
	public EstimateDueDate_Response EndEstimateDueDate(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((EstimateDueDate_Response)(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/LookupProfile", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public Profile_Information LookupProfile(string SecureKey, Enum_Carrier_Code CarrierCode, string State, bool Preorder, Enum_Product_Type ProductType) {
		object[] results = this.Invoke("LookupProfile", new object[] {
					SecureKey,
					CarrierCode,
					State,
					Preorder,
					ProductType});
		return ((Profile_Information)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginLookupProfile(string SecureKey, Enum_Carrier_Code CarrierCode, string State, bool Preorder, Enum_Product_Type ProductType, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("LookupProfile", new object[] {
					SecureKey,
					CarrierCode,
					State,
					Preorder,
					ProductType}, callback, asyncState);
	}
	
	/// <remarks/>
	public Profile_Information EndLookupProfile(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((Profile_Information)(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/StatusChangeOrderWithBAN", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public Order_Response StatusChangeOrderWithBAN(string SecureKey, Enum_Carrier_Code CarrierCode, string TN, Customer_Information Customer, Enum_Product_Type ProductType, Enum_Order_Type OrderType, string oState, string oPON, System.DateTime oDueDate, string[] oAdditionalTNs, bool oAllowDuplicates, string oBAN) {
		object[] results = this.Invoke("StatusChangeOrderWithBAN", new object[] {
					SecureKey,
					CarrierCode,
					TN,
					Customer,
					ProductType,
					OrderType,
					oState,
					oPON,
					oDueDate,
					oAdditionalTNs,
					oAllowDuplicates,
					oBAN});
		return ((Order_Response)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginStatusChangeOrderWithBAN(string SecureKey, Enum_Carrier_Code CarrierCode, string TN, Customer_Information Customer, Enum_Product_Type ProductType, Enum_Order_Type OrderType, string oState, string oPON, System.DateTime oDueDate, string[] oAdditionalTNs, bool oAllowDuplicates, string oBAN, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("StatusChangeOrderWithBAN", new object[] {
					SecureKey,
					CarrierCode,
					TN,
					Customer,
					ProductType,
					OrderType,
					oState,
					oPON,
					oDueDate,
					oAdditionalTNs,
					oAllowDuplicates,
					oBAN}, callback, asyncState);
	}
	
	/// <remarks/>
	public Order_Response EndStatusChangeOrderWithBAN(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((Order_Response)(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/StatusChangeOrder", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public Order_Response StatusChangeOrder(string SecureKey, Enum_Carrier_Code CarrierCode, string TN, Customer_Information Customer, Enum_Product_Type ProductType, Enum_Order_Type OrderType, string oState, string oPON, System.DateTime oDueDate, string[] oAdditionalTNs, bool oAllowDuplicates) {
		object[] results = this.Invoke("StatusChangeOrder", new object[] {
					SecureKey,
					CarrierCode,
					TN,
					Customer,
					ProductType,
					OrderType,
					oState,
					oPON,
					oDueDate,
					oAdditionalTNs,
					oAllowDuplicates});
		return ((Order_Response)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginStatusChangeOrder(string SecureKey, Enum_Carrier_Code CarrierCode, string TN, Customer_Information Customer, Enum_Product_Type ProductType, Enum_Order_Type OrderType, string oState, string oPON, System.DateTime oDueDate, string[] oAdditionalTNs, bool oAllowDuplicates, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("StatusChangeOrder", new object[] {
					SecureKey,
					CarrierCode,
					TN,
					Customer,
					ProductType,
					OrderType,
					oState,
					oPON,
					oDueDate,
					oAdditionalTNs,
					oAllowDuplicates}, callback, asyncState);
	}
	
	/// <remarks/>
	public Order_Response EndStatusChangeOrder(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((Order_Response)(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/OrderStatus", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public OrderStatus_Response OrderStatus(OrderStatus_Request myRequest) {
		object[] results = this.Invoke("OrderStatus", new object[] {
					myRequest});
		return ((OrderStatus_Response)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginOrderStatus(OrderStatus_Request myRequest, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("OrderStatus", new object[] {
					myRequest}, callback, asyncState);
	}
	
	/// <remarks/>
	public OrderStatus_Response EndOrderStatus(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((OrderStatus_Response)(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/GetNotificationCSOTS", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public Notification_Information GetNotificationCSOTS(string PON, string Username, string Password) {
		object[] results = this.Invoke("GetNotificationCSOTS", new object[] {
					PON,
					Username,
					Password});
		return ((Notification_Information)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginGetNotificationCSOTS(string PON, string Username, string Password, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("GetNotificationCSOTS", new object[] {
					PON,
					Username,
					Password}, callback, asyncState);
	}
	
	/// <remarks/>
	public Notification_Information EndGetNotificationCSOTS(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((Notification_Information)(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/OrderStatusSimple", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public OrderStatus_Response OrderStatusSimple(string PON, int ProfileId, string SecureKey) {
		object[] results = this.Invoke("OrderStatusSimple", new object[] {
					PON,
					ProfileId,
					SecureKey});
		return ((OrderStatus_Response)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginOrderStatusSimple(string PON, int ProfileId, string SecureKey, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("OrderStatusSimple", new object[] {
					PON,
					ProfileId,
					SecureKey}, callback, asyncState);
	}
	
	/// <remarks/>
	public OrderStatus_Response EndOrderStatusSimple(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((OrderStatus_Response)(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/CustomerServiceRecord", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public CustomerServiceRecord_Response CustomerServiceRecord(CustomerServiceRecord_Request myRequest) {
		object[] results = this.Invoke("CustomerServiceRecord", new object[] {
					myRequest});
		return ((CustomerServiceRecord_Response)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginCustomerServiceRecord(CustomerServiceRecord_Request myRequest, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("CustomerServiceRecord", new object[] {
					myRequest}, callback, asyncState);
	}
	
	/// <remarks/>
	public CustomerServiceRecord_Response EndCustomerServiceRecord(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((CustomerServiceRecord_Response)(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/LookupResponse", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public BaseResponse LookupResponse(string SecureKey, int LogId) {
		object[] results = this.Invoke("LookupResponse", new object[] {
					SecureKey,
					LogId});
		return ((BaseResponse)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginLookupResponse(string SecureKey, int LogId, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("LookupResponse", new object[] {
					SecureKey,
					LogId}, callback, asyncState);
	}
	
	/// <remarks/>
	public BaseResponse EndLookupResponse(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((BaseResponse)(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/GetCertificate", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public Certificate_Information GetCertificate(string SecureKey, int Id) {
		object[] results = this.Invoke("GetCertificate", new object[] {
					SecureKey,
					Id});
		return ((Certificate_Information)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginGetCertificate(string SecureKey, int Id, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("GetCertificate", new object[] {
					SecureKey,
					Id}, callback, asyncState);
	}
	
	/// <remarks/>
	public Certificate_Information EndGetCertificate(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((Certificate_Information)(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/GetProfile", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public Profile_Information GetProfile(string SecureKey, int Id) {
		object[] results = this.Invoke("GetProfile", new object[] {
					SecureKey,
					Id});
		return ((Profile_Information)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginGetProfile(string SecureKey, int Id, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("GetProfile", new object[] {
					SecureKey,
					Id}, callback, asyncState);
	}
	
	/// <remarks/>
	public Profile_Information EndGetProfile(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((Profile_Information)(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/GetProfiles", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public Profile_Information[] GetProfiles(string SecureKey) {
		object[] results = this.Invoke("GetProfiles", new object[] {
					SecureKey});
		return ((Profile_Information[])(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginGetProfiles(string SecureKey, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("GetProfiles", new object[] {
					SecureKey}, callback, asyncState);
	}
	
	/// <remarks/>
	public Profile_Information[] EndGetProfiles(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((Profile_Information[])(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/GetLoss", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public Loss_Response GetLoss(string SecureKey, Enum_Carrier_Code CarrierCode) {
		object[] results = this.Invoke("GetLoss", new object[] {
					SecureKey,
					CarrierCode});
		return ((Loss_Response)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginGetLoss(string SecureKey, Enum_Carrier_Code CarrierCode, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("GetLoss", new object[] {
					SecureKey,
					CarrierCode}, callback, asyncState);
	}
	
	/// <remarks/>
	public Loss_Response EndGetLoss(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((Loss_Response)(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/GetUnprocessedNotifications", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public Notifications_Response GetUnprocessedNotifications(string SecureKey, bool AutoProcess, Enum_Carrier_Code CarrierCode) {
		object[] results = this.Invoke("GetUnprocessedNotifications", new object[] {
					SecureKey,
					AutoProcess,
					CarrierCode});
		return ((Notifications_Response)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginGetUnprocessedNotifications(string SecureKey, bool AutoProcess, Enum_Carrier_Code CarrierCode, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("GetUnprocessedNotifications", new object[] {
					SecureKey,
					AutoProcess,
					CarrierCode}, callback, asyncState);
	}
	
	/// <remarks/>
	public Notifications_Response EndGetUnprocessedNotifications(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((Notifications_Response)(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/ProcessNotification", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public bool ProcessNotification(string SecureKey, Notification_Information myNotification) {
		object[] results = this.Invoke("ProcessNotification", new object[] {
					SecureKey,
					myNotification});
		return ((bool)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginProcessNotification(string SecureKey, Notification_Information myNotification, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("ProcessNotification", new object[] {
					SecureKey,
					myNotification}, callback, asyncState);
	}
	
	/// <remarks/>
	public bool EndProcessNotification(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((bool)(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/ReserveTN", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public ReserveTN_Response ReserveTN(ReserveTN_Request myRequest) {
		object[] results = this.Invoke("ReserveTN", new object[] {
					myRequest});
		return ((ReserveTN_Response)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginReserveTN(ReserveTN_Request myRequest, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("ReserveTN", new object[] {
					myRequest}, callback, asyncState);
	}
	
	/// <remarks/>
	public ReserveTN_Response EndReserveTN(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((ReserveTN_Response)(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/SearchLogDateRange", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public Log_Information[] SearchLogDateRange(string SecureKey, System.DateTime DateStart, System.DateTime DateEnd) {
		object[] results = this.Invoke("SearchLogDateRange", new object[] {
					SecureKey,
					DateStart,
					DateEnd});
		return ((Log_Information[])(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginSearchLogDateRange(string SecureKey, System.DateTime DateStart, System.DateTime DateEnd, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("SearchLogDateRange", new object[] {
					SecureKey,
					DateStart,
					DateEnd}, callback, asyncState);
	}
	
	/// <remarks/>
	public Log_Information[] EndSearchLogDateRange(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((Log_Information[])(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/SearchLogMostRecent", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public Log_Information[] SearchLogMostRecent(string SecureKey, int Quantity) {
		object[] results = this.Invoke("SearchLogMostRecent", new object[] {
					SecureKey,
					Quantity});
		return ((Log_Information[])(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginSearchLogMostRecent(string SecureKey, int Quantity, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("SearchLogMostRecent", new object[] {
					SecureKey,
					Quantity}, callback, asyncState);
	}
	
	/// <remarks/>
	public Log_Information[] EndSearchLogMostRecent(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((Log_Information[])(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/SendOrder", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public Order_Response SendOrder(Order_Request myRequest) {
		object[] results = this.Invoke("SendOrder", new object[] {
					myRequest});
		return ((Order_Response)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginSendOrder(Order_Request myRequest, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("SendOrder", new object[] {
					myRequest}, callback, asyncState);
	}
	
	/// <remarks/>
	public Order_Response EndSendOrder(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((Order_Response)(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/ValidateAddress", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public ValidateAddress_Response ValidateAddress(ValidateAddress_Request myRequest) {
		object[] results = this.Invoke("ValidateAddress", new object[] {
					myRequest});
		return ((ValidateAddress_Response)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginValidateAddress(ValidateAddress_Request myRequest, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("ValidateAddress", new object[] {
					myRequest}, callback, asyncState);
	}
	
	/// <remarks/>
	public ValidateAddress_Response EndValidateAddress(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((ValidateAddress_Response)(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/ValidateAddressByTN", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public ValidateAddress_Response ValidateAddressByTN(ValidateAddressByTN_Request myRequest) {
		object[] results = this.Invoke("ValidateAddressByTN", new object[] {
					myRequest});
		return ((ValidateAddress_Response)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginValidateAddressByTN(ValidateAddressByTN_Request myRequest, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("ValidateAddressByTN", new object[] {
					myRequest}, callback, asyncState);
	}
	
	/// <remarks/>
	public ValidateAddress_Response EndValidateAddressByTN(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((ValidateAddress_Response)(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/ViewLogEntry", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public string ViewLogEntry(string SecureKey, int LogId) {
		object[] results = this.Invoke("ViewLogEntry", new object[] {
					SecureKey,
					LogId});
		return ((string)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginViewLogEntry(string SecureKey, int LogId, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("ViewLogEntry", new object[] {
					SecureKey,
					LogId}, callback, asyncState);
	}
	
	/// <remarks/>
	public string EndViewLogEntry(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((string)(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/GetLog", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public Log_Information GetLog(string SecureKey, int LogId) {
		object[] results = this.Invoke("GetLog", new object[] {
					SecureKey,
					LogId});
		return ((Log_Information)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginGetLog(string SecureKey, int LogId, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("GetLog", new object[] {
					SecureKey,
					LogId}, callback, asyncState);
	}
	
	/// <remarks/>
	public Log_Information EndGetLog(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((Log_Information)(results[0]));
	}
	
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://localhost/oc.portal.gravity/BellSouthEmailPDF", RequestNamespace="http://localhost/oc.portal.gravity/", ResponseNamespace="http://localhost/oc.portal.gravity/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
	public string BellSouthEmailPDF(Order_Request myRequest, string EmailTo) {
		object[] results = this.Invoke("BellSouthEmailPDF", new object[] {
					myRequest,
					EmailTo});
		return ((string)(results[0]));
	}
	
	/// <remarks/>
	public System.IAsyncResult BeginBellSouthEmailPDF(Order_Request myRequest, string EmailTo, System.AsyncCallback callback, object asyncState) {
		return this.BeginInvoke("BellSouthEmailPDF", new object[] {
					myRequest,
					EmailTo}, callback, asyncState);
	}
	
	/// <remarks/>
	public string EndBellSouthEmailPDF(System.IAsyncResult asyncResult) {
		object[] results = this.EndInvoke(asyncResult);
		return ((string)(results[0]));
	}
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class Fax_Log_Information : BaseInformation {
	
	/// <remarks/>
	public int Fax_Log_ID;
	
	/// <remarks/>
	public int Client_ID;
	
	/// <remarks/>
	public int Order_ID;
	
	/// <remarks/>
	public int TransId;
	
	/// <remarks/>
	public bool IsComplete;
	
	/// <remarks/>
	public bool IsSuccessful;
	
	/// <remarks/>
	public string NotifyEmail;
	
	/// <remarks/>
	public string FaxNumber;
	
	/// <remarks/>
	public string PON;
	
	/// <remarks/>
	public System.DateTime Created_Date;
	
	/// <remarks/>
	public int StatusCode;
	
	/// <remarks/>
	public string StatusMessage;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Log_Information))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseResponse))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CustomerServiceRecord_Response))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(AvailableTNs_Response))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CancelTN_Response))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Notifications_Response))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Availability_Response))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(EstimateDueDate_Response))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ValidateAddress_Response))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Loss_Response))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(OrderStatus_Response))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Order_Response))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ReserveTN_Response))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Loss_Information))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Certificate_Information))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Fax_Log_Information))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Notification_Information))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Order_Options_Information))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Profile_Information))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(BaseRequest))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ValidateAddressByTN_Request))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(EstimateDueDate_Request))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Order_Request))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Availability_Request))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(OrderStatus_Request))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ReserveTN_Request))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ValidateAddress_Request))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CustomerServiceRecord_Request))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(AvailableTNs_Request))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CancelTN_Request))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Product_Information))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Line_Information))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Customer_Information))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CSR_Information))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Address_Information))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Listing_Information))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(AuthenticationResponse))]
public class BaseInformation {
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class ListedName {
	
	/// <remarks/>
	public string Name;
	
	/// <remarks/>
	public string TN;
	
	/// <remarks/>
	public string Status;
	
	/// <remarks/>
	public string ClassOfService;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class Order_Information {
	
	/// <remarks/>
	public string PON;
	
	/// <remarks/>
	public int Version;
	
	/// <remarks/>
	public Enum_Revision_Type Revision_Type;
	
	/// <remarks/>
	public Enum_Appointment_Time Appointment_Time;
	
	/// <remarks/>
	public System.DateTime Requested_Due_Date;
	
	/// <remarks/>
	public System.DateTime Requested_Due_Date_Out;
	
	/// <remarks/>
	public Enum_Order_Type Order_Type;
	
	/// <remarks/>
	public Enum_Product_Type Product_Type;
	
	/// <remarks/>
	public string AddressSerialNumber;
	
	/// <remarks/>
	public Customer_Information Customer;
	
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("LineList")]
	public Line_Information[] LineList;
	
	/// <remarks/>
	public Order_Options_Information Order_Options;
	
	/// <remarks/>
	public string Remarks;
	
	/// <remarks/>
	public string CustomerContactName;
	
	/// <remarks/>
	public string CustomerContactTN;
	
	/// <remarks/>
	public string TN;
	
	/// <remarks/>
	public string TN_New;
	
	/// <remarks/>
	public string Billing_Account_Number;
	
	/// <remarks/>
	public string Service_Order_Number;
	
	/// <remarks/>
	public int OrderId;
	
	/// <remarks/>
	public string Initiator_Name;
	
	/// <remarks/>
	public string Initiator_Email;
	
	/// <remarks/>
	public string Initiator_Telephone_Number;
	
	/// <remarks/>
	public string Initiator_Fax_Number;
	
	/// <remarks/>
	public string Implementation_Name;
	
	/// <remarks/>
	public string Implementation_Telephone_Number;
	
	/// <remarks/>
	public string TransferCallsTN;
	
	/// <remarks/>
	public bool TransferCallsOption_Indicator;
	
	/// <remarks/>
	public string TOS;
	
	/// <remarks/>
	public Enum_Suspend_Type SuspendType;
	
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("HuntList")]
	public string[] HuntList;
	
	/// <remarks/>
	public bool Migration_Indicator;
	
	/// <remarks/>
	public bool ListingsAsAdditionalMain;
	
	/// <remarks/>
	public bool ChangePIC;
	
	/// <remarks/>
	public string ProjectIdentification;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public enum Enum_Revision_Type {
	
	/// <remarks/>
	NOT_SPECIFIED,
	
	/// <remarks/>
	NONE,
	
	/// <remarks/>
	CANCEL,
	
	/// <remarks/>
	CHANGE_DUE_DATE,
	
	/// <remarks/>
	OTHER_CHANGE,
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public enum Enum_Appointment_Time {
	
	/// <remarks/>
	DEFAULT,
	
	/// <remarks/>
	AM,
	
	/// <remarks/>
	PM,
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public enum Enum_Order_Type {
	
	/// <remarks/>
	NOT_SPECIFIED,
	
	/// <remarks/>
	NONE,
	
	/// <remarks/>
	NEW,
	
	/// <remarks/>
	CONVERSION,
	
	/// <remarks/>
	MIGRATION,
	
	/// <remarks/>
	TRANSFER,
	
	/// <remarks/>
	CHANGE_FEATURE,
	
	/// <remarks/>
	CHANGE_TN,
	
	/// <remarks/>
	DISCONNECT,
	
	/// <remarks/>
	DENY,
	
	/// <remarks/>
	SUSPEND,
	
	/// <remarks/>
	RESTORE,
	
	/// <remarks/>
	LD_BLOCK,
	
	/// <remarks/>
	LD_RESTORE,
	
	/// <remarks/>
	CHANGE_LISTING,
	
	/// <remarks/>
	ADD_LINE,
	
	/// <remarks/>
	PORT,
	
	/// <remarks/>
	CONVERSION_AS_IS,
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public enum Enum_Product_Type {
	
	/// <remarks/>
	NOT_SPECIFIED,
	
	/// <remarks/>
	OTHER,
	
	/// <remarks/>
	UNEP,
	
	/// <remarks/>
	LWC,
	
	/// <remarks/>
	RESALE,
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class Customer_Information : BaseInformation {
	
	/// <remarks/>
	public string Name_Company;
	
	/// <remarks/>
	public string Name_First;
	
	/// <remarks/>
	public string Name_Last;
	
	/// <remarks/>
	public Enum_Customer_Class Class;
	
	/// <remarks/>
	public string SIC;
	
	/// <remarks/>
	public Address_Information Address;
	
	/// <remarks/>
	public bool Deny_Indicator;
	
	/// <remarks/>
	public System.DateTime Deny_Date;
	
	/// <remarks/>
	public bool Inactive;
	
	/// <remarks/>
	public string Yellow_Page_Heading;
	
	/// <remarks/>
	public string Yellow_Page_Heading_Verbiage;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public enum Enum_Customer_Class {
	
	/// <remarks/>
	Business,
	
	/// <remarks/>
	Residential,
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class Address_Information : BaseInformation {
	
	/// <remarks/>
	public string Street_Number;
	
	/// <remarks/>
	public string Street_Number_Suffix;
	
	/// <remarks/>
	public string Street_Pre_Direction;
	
	/// <remarks/>
	public string Street_Name;
	
	/// <remarks/>
	public string Street_Name_Suffix;
	
	/// <remarks/>
	public string Street_Post_Direction;
	
	/// <remarks/>
	public string Secondary_Designation;
	
	/// <remarks/>
	public string Secondary_Number;
	
	/// <remarks/>
	public string Secondary_Designation2;
	
	/// <remarks/>
	public string Secondary_Number2;
	
	/// <remarks/>
	public string Secondary_Designation3;
	
	/// <remarks/>
	public string Secondary_Number3;
	
	/// <remarks/>
	public string Additional_Address_Information;
	
	/// <remarks/>
	public string Geocode;
	
	/// <remarks/>
	public string City;
	
	/// <remarks/>
	public string State;
	
	/// <remarks/>
	public string Zip;
	
	/// <remarks/>
	public string Zip_4;
	
	/// <remarks/>
	public string Line_1;
	
	/// <remarks/>
	public string Line_2;
	
	/// <remarks/>
	public string AccessInformation;
	
	/// <remarks/>
	public string LEC_NPANXX;
	
	/// <remarks/>
	public string LEC_CLLI;
	
	/// <remarks/>
	public bool Is_Parsed;
	
	/// <remarks/>
	public string Range_Min;
	
	/// <remarks/>
	public string Range_Max;
	
	/// <remarks/>
	public bool IsRange;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class Line_Information : BaseInformation {
	
	/// <remarks/>
	public Enum_Service_Action Action;
	
	/// <remarks/>
	public string TN;
	
	/// <remarks/>
	public string TN_New;
	
	/// <remarks/>
	public string TN_Voicemail;
	
	/// <remarks/>
	public string PIC;
	
	/// <remarks/>
	public string LPIC;
	
	/// <remarks/>
	public string LTOS;
	
	/// <remarks/>
	public string Class_of_Service;
	
	/// <remarks/>
	public Enum_LD_Freeze_Type LD_Freeze_Type;
	
	/// <remarks/>
	public Enum_Service_Type Service_Type;
	
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("ProductList")]
	public Product_Information[] ProductList;
	
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("ListingList")]
	public Listing_Information[] ListingList;
	
	/// <remarks/>
	public int Hunt_Sequence;
	
	/// <remarks/>
	public string Hunt_Identifier;
	
	/// <remarks/>
	public string Hunt_Type;
	
	/// <remarks/>
	public Enum_Hunt_Action Hunt_Action;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public enum Enum_Service_Action {
	
	/// <remarks/>
	NOT_SPECIFIED,
	
	/// <remarks/>
	NONE,
	
	/// <remarks/>
	ACTIVATE,
	
	/// <remarks/>
	CHANGE,
	
	/// <remarks/>
	DISCONNECT,
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public enum Enum_LD_Freeze_Type {
	
	/// <remarks/>
	NOT_SPECIFIED,
	
	/// <remarks/>
	NONE,
	
	/// <remarks/>
	BOTH,
	
	/// <remarks/>
	INTRALATA,
	
	/// <remarks/>
	INTERLATA,
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public enum Enum_Service_Type {
	
	/// <remarks/>
	NOT_SPECIFIED,
	
	/// <remarks/>
	SPLIT_LINE,
	
	/// <remarks/>
	ANALOG,
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class Product_Information : BaseInformation {
	
	/// <remarks/>
	public Enum_Service_Action Action;
	
	/// <remarks/>
	public Enum_Product_Type_Code ProductType;
	
	/// <remarks/>
	public string Description;
	
	/// <remarks/>
	public string Code1;
	
	/// <remarks/>
	public string Code2;
	
	/// <remarks/>
	public string Code3;
	
	/// <remarks/>
	public string Code4;
	
	/// <remarks/>
	public System.DateTime Activity_Date;
	
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("FidArray")]
	public string[] FidArray;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public enum Enum_Product_Type_Code {
	
	/// <remarks/>
	NOT_SPECIFIED,
	
	/// <remarks/>
	RECURRING_PIC_LPIC,
	
	/// <remarks/>
	RECURRING_COMMISSION1,
	
	/// <remarks/>
	RECURRING_COMMISSION2,
	
	/// <remarks/>
	RECURRING_REMOTE_CALL_FORWARDING,
	
	/// <remarks/>
	RECURRING_LOCAL,
	
	/// <remarks/>
	RECURRING_LOCAL_BUNDLED,
	
	/// <remarks/>
	RECURRING_DIALUP,
	
	/// <remarks/>
	RECURRING_FEATURE,
	
	/// <remarks/>
	RECURRING_LD_PREPAID,
	
	/// <remarks/>
	RECURRING_DIRECTORY_LISTING,
	
	/// <remarks/>
	RECURRING_BLOCK,
	
	/// <remarks/>
	RECURRING_LD_POSTPAID,
	
	/// <remarks/>
	RECURRING_NONTAX,
	
	/// <remarks/>
	RECURRING_FCC_SLC,
	
	/// <remarks/>
	RECURRING_FCC_PICC,
	
	/// <remarks/>
	RECURRING_COLOCATION,
	
	/// <remarks/>
	RECURRING_PAGING,
	
	/// <remarks/>
	RECURRING_FREE_MINS_INTERSTATE,
	
	/// <remarks/>
	RECURRING_FREE_MINS_INTRALATA_LOCAL,
	
	/// <remarks/>
	RECURRING_FREE_MINS_INTRASTATE,
	
	/// <remarks/>
	RECURRING_FREE_MINS_DOMESTIC,
	
	/// <remarks/>
	RECURRING_FREE_MINS_INTRALATA,
	
	/// <remarks/>
	RECURRING_FREE_MINS_LOCAL,
	
	/// <remarks/>
	RECURRING_FREE_MINS_ALL,
	
	/// <remarks/>
	RECURRING_FREE_MINS_INTERNATIONAL,
	
	/// <remarks/>
	RECURRING_GENERAL_SALES,
	
	/// <remarks/>
	RECURRING_DSL,
	
	/// <remarks/>
	RECURRING_VOICE_DSL,
	
	/// <remarks/>
	RECURRING_FEATURE_NON_COMM,
	
	/// <remarks/>
	RECURRING_VOICEMAIL,
	
	/// <remarks/>
	RECURRING_LD_PREPAID_UNLIMITED,
	
	/// <remarks/>
	RECURRING_LD_POSTPAID_UNLIMITED,
	
	/// <remarks/>
	RECURRING_T1,
	
	/// <remarks/>
	RECURRING_VOICE_T1,
	
	/// <remarks/>
	RECURRING_INTEGRATED_T1,
	
	/// <remarks/>
	RECURRING_DATA_T1,
	
	/// <remarks/>
	RECURRING_DATA_P2P,
	
	/// <remarks/>
	RECURRING_VOICE_P2P,
	
	/// <remarks/>
	RECURRING_PRI,
	
	/// <remarks/>
	RECURRING_ISDN,
	
	/// <remarks/>
	RECURRING_DATA_ISDN,
	
	/// <remarks/>
	RECURRING_VOICE_ISDN,
	
	/// <remarks/>
	RECURRING_CONFERENCE_CALLING,
	
	/// <remarks/>
	RECURRING_USAGE_LOCAL,
	
	/// <remarks/>
	RECURRING_USAGE_INTRALATA,
	
	/// <remarks/>
	RECURRING_USAGE_INTRASTATE,
	
	/// <remarks/>
	RECURRING_USAGE_INTERSTATE,
	
	/// <remarks/>
	USAGE_DIRECTORY_ASSISTANCE,
	
	/// <remarks/>
	USAGE_DIRECTORY_INTRALATA,
	
	/// <remarks/>
	USAGE_DIRECTORY_INTRASTATE,
	
	/// <remarks/>
	USAGE_DIRECTORY_INTERSTATE,
	
	/// <remarks/>
	USAGE_CALL_COMPLETION,
	
	/// <remarks/>
	USAGE_CALL_RETURN,
	
	/// <remarks/>
	USAGE_COLLECT,
	
	/// <remarks/>
	USAGE_OPERATOR_ASSISTED,
	
	/// <remarks/>
	USAGE_REPEAT_DIALING,
	
	/// <remarks/>
	USAGE_BUSY_LINE_VERIFICATION,
	
	/// <remarks/>
	USAGE_BUSY_LINE_INTERRUPTION,
	
	/// <remarks/>
	USAGE_LOCAL,
	
	/// <remarks/>
	USAGE_0_PLUS,
	
	/// <remarks/>
	USAGE_INTL_OPERATOR,
	
	/// <remarks/>
	USAGE_CONFERENCE_CALLING,
	
	/// <remarks/>
	USAGE_CALL_TRACE,
	
	/// <remarks/>
	USAGE_OPERATOR_VERIFIED,
	
	/// <remarks/>
	USAGE_E911,
	
	/// <remarks/>
	USAGE_E511,
	
	/// <remarks/>
	USAGE_LD_INTERSTATE,
	
	/// <remarks/>
	USAGE_LD_INTRASTATE,
	
	/// <remarks/>
	USAGE_LD_INTRALATA,
	
	/// <remarks/>
	USAGE_LD_INTERNATIONAL,
	
	/// <remarks/>
	USAGE_LD_CELLULAR_INTERNATIONAL,
	
	/// <remarks/>
	USAGE_LD_800_LOCAL,
	
	/// <remarks/>
	USAGE_LD_800_INTERSTATE,
	
	/// <remarks/>
	USAGE_LD_800_INTRASTATE,
	
	/// <remarks/>
	USAGE_LD_800_INTRALATA,
	
	/// <remarks/>
	USAGE_LD_800_INTERNATIONAL,
	
	/// <remarks/>
	ONETIME_GENERAL_SALES,
	
	/// <remarks/>
	ONETIME_LOCAL,
	
	/// <remarks/>
	ONETIME_LATE_FEE,
	
	/// <remarks/>
	ONETIME_REPAIR,
	
	/// <remarks/>
	ONETIME_BALANCE_ADJUSTMENT,
	
	/// <remarks/>
	ONETIME_NONTAX,
	
	/// <remarks/>
	ONETIME_DSL,
	
	/// <remarks/>
	ONETIME_DIALUP,
	
	/// <remarks/>
	ONETIME_RESTORE_FEE,
	
	/// <remarks/>
	ONETIME_TRANSFER_FEE,
	
	/// <remarks/>
	ONETIME_CHANGE_FEE,
	
	/// <remarks/>
	ONETIME_CONNECTION_FEE,
	
	/// <remarks/>
	ONETIME_CONVERSION_FEE,
	
	/// <remarks/>
	ONETIME_DEPOSIT,
	
	/// <remarks/>
	ONETIME_DEPOSIT_REFUND,
	
	/// <remarks/>
	ONETIME_REFUND,
	
	/// <remarks/>
	ONETIME_LONG_DISTANCE,
	
	/// <remarks/>
	ONETIME_DISCONNECT_FEE,
	
	/// <remarks/>
	ONETIME_RETURNED_CHECK,
	
	/// <remarks/>
	ONETIME_RETURNED_CHECK_FEE,
	
	/// <remarks/>
	ONETIME_PAYMENT_ARRANGEMENT_ADJUSTMENT,
	
	/// <remarks/>
	ONETIME_EQUIPMENT,
	
	/// <remarks/>
	RECURRING_HANDSET,
	
	/// <remarks/>
	ONETIME_HANDSET,
	
	/// <remarks/>
	RECURRING_SEAT,
	
	/// <remarks/>
	ONETIME_CONVENIENCE_FEE,
	
	/// <remarks/>
	RECURRING_HOSTING,
	
	/// <remarks/>
	RECURRING_AUTO_DEBIT_CREDIT,
	
	/// <remarks/>
	ONETIME_WIRELESS_CONNECTION,
	
	/// <remarks/>
	USAGE_WIRELESS_TEXT_MESSAGE,
	
	/// <remarks/>
	USAGE_WIRELESS_INTRASTATE_FRIENDS_FAMILY,
	
	/// <remarks/>
	USAGE_WIRELESS_INTERSTATE_FRIENDS_FAMILY,
	
	/// <remarks/>
	USAGE_WIRELESS_INTRASTATE_ROAMING,
	
	/// <remarks/>
	USAGE_WIRELESS_INTERSTATE_ROAMING,
	
	/// <remarks/>
	USAGE_WIRELESS_INTRASTATE_PEAK,
	
	/// <remarks/>
	USAGE_WIRELESS_INTERSTATE_PEAK,
	
	/// <remarks/>
	USAGE_WIRELESS_INTRASTATE_OFF_PEAK,
	
	/// <remarks/>
	USAGE_WIRELESS_INTERSTATE_OFF_PEAK,
	
	/// <remarks/>
	RECURRING_WIRELESS_BASIC_PLAN,
	
	/// <remarks/>
	RECURRING_WIRELESS_TEXT_PLAN,
	
	/// <remarks/>
	RECURRING_WIRELESS_FEATURE,
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class Listing_Information : BaseInformation {
	
	/// <remarks/>
	public Enum_Listing_Action Action;
	
	/// <remarks/>
	public string Name_Company;
	
	/// <remarks/>
	public string Name_Nickname;
	
	/// <remarks/>
	public string Name_Prefix;
	
	/// <remarks/>
	public string Name_First;
	
	/// <remarks/>
	public string Name_Middle;
	
	/// <remarks/>
	public string Name_Last;
	
	/// <remarks/>
	public string Name_Suffix;
	
	/// <remarks/>
	public Address_Information Address;
	
	/// <remarks/>
	public Enum_Listing_Type Listing_Type;
	
	/// <remarks/>
	public string Style_Code;
	
	/// <remarks/>
	public string Type_Description;
	
	/// <remarks/>
	public Enum_Listing_Location Location;
	
	/// <remarks/>
	public string Degree_of_Indention;
	
	/// <remarks/>
	public string Yellow_Page_Heading;
	
	/// <remarks/>
	public string Yellow_Page_Heading_Verbiage;
	
	/// <remarks/>
	public string Listing_Instruction;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public enum Enum_Listing_Action {
	
	/// <remarks/>
	NOT_SPECIFIED,
	
	/// <remarks/>
	NONE,
	
	/// <remarks/>
	NEW,
	
	/// <remarks/>
	DELETE,
	
	/// <remarks/>
	CHANGE_INSERT,
	
	/// <remarks/>
	CHANGE_REMOVE,
	
	/// <remarks/>
	NO_CHANGE,
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public enum Enum_Listing_Type {
	
	/// <remarks/>
	NOT_SPECIFIED,
	
	/// <remarks/>
	LISTED,
	
	/// <remarks/>
	UNLISTED,
	
	/// <remarks/>
	NON_PUBLISHED,
	
	/// <remarks/>
	OMIT_ADDRESS,
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public enum Enum_Listing_Location {
	
	/// <remarks/>
	NOT_SPECIFIED,
	
	/// <remarks/>
	FOREIGN,
	
	/// <remarks/>
	LOCAL,
	
	/// <remarks/>
	SECONDARY,
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public enum Enum_Hunt_Action {
	
	/// <remarks/>
	NONE,
	
	/// <remarks/>
	ADD,
	
	/// <remarks/>
	EXISTING,
	
	/// <remarks/>
	REMOVE,
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class Order_Options_Information : BaseInformation {
	
	/// <remarks/>
	public bool New_Unit_Flag;
	
	/// <remarks/>
	public bool New_Service_Address_Flag;
	
	/// <remarks/>
	public bool Abandoned_Station_Flag;
	
	/// <remarks/>
	public Enum_Local_Freeze_Type Local_Freeze_Type;
	
	/// <remarks/>
	public bool Retain_Listing_Flag;
	
	/// <remarks/>
	public bool Expedite_Flag;
	
	/// <remarks/>
	public bool Additional_Line_Flag;
	
	/// <remarks/>
	public bool Omit_From_Mailing_List_Flag;
	
	/// <remarks/>
	public bool Paper_Order_Indicator_Flag;
	
	/// <remarks/>
	public bool Convert_As_Is;
	
	/// <remarks/>
	public bool Convert_And_Change_PIC;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public enum Enum_Local_Freeze_Type {
	
	/// <remarks/>
	NOT_SPECIFIED,
	
	/// <remarks/>
	NONE,
	
	/// <remarks/>
	ADD,
	
	/// <remarks/>
	REMOVE,
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public enum Enum_Suspend_Type {
	
	/// <remarks/>
	ALL,
	
	/// <remarks/>
	INCOMING,
	
	/// <remarks/>
	OUTGOING,
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class Log_Information : BaseInformation {
	
	/// <remarks/>
	public int Log_ID;
	
	/// <remarks/>
	public int Profile_ID;
	
	/// <remarks/>
	public int IA_Config_ID;
	
	/// <remarks/>
	public string PON;
	
	/// <remarks/>
	public string PONVersion;
	
	/// <remarks/>
	public string Version;
	
	/// <remarks/>
	public string Reference_Number;
	
	/// <remarks/>
	public string AckData;
	
	/// <remarks/>
	public string Sent;
	
	/// <remarks/>
	public string Sent_Translated;
	
	/// <remarks/>
	public string Received;
	
	/// <remarks/>
	public string Received_Translated;
	
	/// <remarks/>
	public System.DateTime Created_Date;
	
	/// <remarks/>
	public Enum_Log_Status Status;
	
	/// <remarks/>
	public bool Inbound_Flag;
	
	/// <remarks/>
	public bool To_Be_Processed_Flag;
	
	/// <remarks/>
	public bool To_Be_Acked;
	
	/// <remarks/>
	public System.DateTime Date_Translated_Out;
	
	/// <remarks/>
	public System.DateTime Date_Translated_In;
	
	/// <remarks/>
	public System.DateTime Date_Received;
	
	/// <remarks/>
	public System.DateTime Date_Acked;
	
	/// <remarks/>
	public System.DateTime Date_Sent;
	
	/// <remarks/>
	public bool Send_Receipt;
	
	/// <remarks/>
	public string ReceiptOutData;
	
	/// <remarks/>
	public string ReceiptInData;
	
	/// <remarks/>
	public System.DateTime Date_Receipt_Sent;
	
	/// <remarks/>
	public System.DateTime Date_Receipt_Received;
	
	/// <remarks/>
	public string RequestType;
	
	/// <remarks/>
	public string MessageCode;
	
	/// <remarks/>
	public string Message;
	
	/// <remarks/>
	public string MessageType;
	
	/// <remarks/>
	public int FaxLogId;
	
	/// <remarks/>
	public Enum_Fax_Status FaxStatus;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public enum Enum_Log_Status {
	
	/// <remarks/>
	NOT_SPECIFIED,
	
	/// <remarks/>
	COMPLETE,
	
	/// <remarks/>
	ERROR,
	
	/// <remarks/>
	SEND_IA,
	
	/// <remarks/>
	SENDING_IA,
	
	/// <remarks/>
	SENT_IA,
	
	/// <remarks/>
	TRANSLATE_IN,
	
	/// <remarks/>
	TRANSLATING_IN,
	
	/// <remarks/>
	TRANSLATE_OUT,
	
	/// <remarks/>
	TRANSLATING_OUT,
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public enum Enum_Fax_Status {
	
	/// <remarks/>
	NA,
	
	/// <remarks/>
	PRE_ERROR,
	
	/// <remarks/>
	SENDING,
	
	/// <remarks/>
	FAILED,
	
	/// <remarks/>
	COMPLETED,
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CustomerServiceRecord_Response))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(AvailableTNs_Response))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CancelTN_Response))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Notifications_Response))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Availability_Response))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(EstimateDueDate_Response))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ValidateAddress_Response))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Loss_Response))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(OrderStatus_Response))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Order_Response))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ReserveTN_Response))]
public class BaseResponse : BaseInformation {
	
	/// <remarks/>
	public string Version;
	
	/// <remarks/>
	public string LogId;
	
	/// <remarks/>
	public bool Valid;
	
	/// <remarks/>
	public string MessageCode;
	
	/// <remarks/>
	public Enum_Message_Type MessageType;
	
	/// <remarks/>
	public string Message;
	
	/// <remarks/>
	public string ReferenceNumber;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public enum Enum_Message_Type {
	
	/// <remarks/>
	_NOT_SPECIFIED,
	
	/// <remarks/>
	INVALID_INPUT,
	
	/// <remarks/>
	SUCCESSFUL,
	
	/// <remarks/>
	SYSTEM_ILEC,
	
	/// <remarks/>
	SYSTEM_GRAVITY,
	
	/// <remarks/>
	UNSUCCESSFUL,
	
	/// <remarks/>
	WARNING,
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class CustomerServiceRecord_Response : BaseResponse {
	
	/// <remarks/>
	public CSR_Information CSR;
	
	/// <remarks/>
	public bool Unparsed;
	
	/// <remarks/>
	public string UnparsedData;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class CSR_Information : BaseInformation {
	
	/// <remarks/>
	public Customer_Information Customer;
	
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("LineList")]
	public Line_Information[] LineList;
	
	/// <remarks/>
	public string Billing_Telephone_Number;
	
	/// <remarks/>
	public bool Local_Service_Freeze;
	
	/// <remarks/>
	public string Billing_Name;
	
	/// <remarks/>
	public string Billing_Address1;
	
	/// <remarks/>
	public string Billing_Address2;
	
	/// <remarks/>
	public string Billing_Address3;
	
	/// <remarks/>
	public string Remarks;
	
	/// <remarks/>
	public string CC;
	
	/// <remarks/>
	public string RSID;
	
	/// <remarks/>
	public int LineCount;
	
	/// <remarks/>
	public string ClassOfService;
	
	/// <remarks/>
	public string Exchange;
	
	/// <remarks/>
	public string CustomerCode;
	
	/// <remarks/>
	public string TypeOfService;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class AvailableTNs_Response : BaseResponse {
	
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("TN_List")]
	public string[] TN_List;
	
	/// <remarks/>
	public bool Reserved;
	
	/// <remarks/>
	public string ReservationNumber;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class CancelTN_Response : BaseResponse {
	
	/// <remarks/>
	public string ReservationNumber;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class Notifications_Response : BaseResponse {
	
	/// <remarks/>
	public Notification_Information[] NotificationList;
	
	/// <remarks/>
	public Loss_Information[] LossList;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class Notification_Information : BaseInformation {
	
	/// <remarks/>
	public int Notification_ID;
	
	/// <remarks/>
	public int Profile_ID;
	
	/// <remarks/>
	public string PON;
	
	/// <remarks/>
	public string Version;
	
	/// <remarks/>
	public string LEO_Message;
	
	/// <remarks/>
	public Enum_Notification_Type Notification_Type;
	
	/// <remarks/>
	public string Message;
	
	/// <remarks/>
	public string MessageCode;
	
	/// <remarks/>
	public bool IsAcknowledged;
	
	/// <remarks/>
	public bool IsProcessed;
	
	/// <remarks/>
	public System.DateTime Created_Date;
	
	/// <remarks/>
	public System.DateTime Notification_Date;
	
	/// <remarks/>
	public System.DateTime Due_Date;
	
	/// <remarks/>
	public string Service_Order_Number;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public enum Enum_Notification_Type {
	
	/// <remarks/>
	NOT_SPECIFIED,
	
	/// <remarks/>
	UNKNOWN,
	
	/// <remarks/>
	CLARIFY,
	
	/// <remarks/>
	COMPLETED,
	
	/// <remarks/>
	BILLING_COMPLETION,
	
	/// <remarks/>
	FOC,
	
	/// <remarks/>
	CANCELED,
	
	/// <remarks/>
	JEOPARDY,
	
	/// <remarks/>
	REJECTED,
	
	/// <remarks/>
	PENDING,
	
	/// <remarks/>
	VOID,
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class Loss_Information : BaseInformation {
	
	/// <remarks/>
	public int Loss_ID;
	
	/// <remarks/>
	public int Profile_ID;
	
	/// <remarks/>
	public string TN;
	
	/// <remarks/>
	public string New_TN;
	
	/// <remarks/>
	public System.DateTime Event_Date;
	
	/// <remarks/>
	public Enum_Loss_Type Loss_Type;
	
	/// <remarks/>
	public bool IsProcessed;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public enum Enum_Loss_Type {
	
	/// <remarks/>
	DISCONNECT,
	
	/// <remarks/>
	CHANGE_TN,
	
	/// <remarks/>
	MOVE,
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class Availability_Response : BaseResponse {
	
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("UnavailableList")]
	public Product_Information[] UnavailableList;
	
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("AvailableList")]
	public Product_Information[] AvailableList;
	
	/// <remarks/>
	public bool All_Available_Flag;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class EstimateDueDate_Response : BaseResponse {
	
	/// <remarks/>
	public System.DateTime Due_Date;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class ValidateAddress_Response : BaseResponse {
	
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("AddressList")]
	public Address_Information[] AddressList;
	
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("ListedNames")]
	public ListedName[] ListedNames;
	
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("TN_List_Working")]
	public string[] TN_List_Working;
	
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("TN_List_Inactive")]
	public string[] TN_List_Inactive;
	
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("TN_List_Quick_Service")]
	public string[] TN_List_Quick_Service;
	
	/// <remarks/>
	public bool WorkingService;
	
	/// <remarks/>
	public bool QuickService;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class Loss_Response : BaseResponse {
	
	/// <remarks/>
	public Loss_Information[] LossList;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class OrderStatus_Response : BaseResponse {
	
	/// <remarks/>
	public Enum_Notification_Type Status;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class Order_Response : BaseResponse {
	
	/// <remarks/>
	public bool ResponseReceived;
	
	/// <remarks/>
	public System.DateTime Due_Date;
	
	/// <remarks/>
	public bool Dispatch_Required;
	
	/// <remarks/>
	public string Service_Order_Number;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class ReserveTN_Response : BaseResponse {
	
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("TN_List")]
	public string[] TN_List;
	
	/// <remarks/>
	public string ReservationNumber;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class Certificate_Information : BaseInformation {
	
	/// <remarks/>
	public int Certificate_ID;
	
	/// <remarks/>
	public string Public_Key;
	
	/// <remarks/>
	public string Description;
	
	/// <remarks/>
	public string LocalMachineName;
	
	/// <remarks/>
	public System.DateTime Expiration_Date;
	
	/// <remarks/>
	public string PFX_Path;
	
	/// <remarks/>
	public string PFX_Password;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class Profile_Information : BaseInformation {
	
	/// <remarks/>
	public int Profile_ID;
	
	/// <remarks/>
	public int Shared_Client_ID;
	
	/// <remarks/>
	public int IA_Config_ID;
	
	/// <remarks/>
	public int Test_IA_Config_ID;
	
	/// <remarks/>
	public int Certificate_ID;
	
	/// <remarks/>
	public string Description;
	
	/// <remarks/>
	public string Application_ID;
	
	/// <remarks/>
	public string Application_Password;
	
	/// <remarks/>
	public string CLEC_ID;
	
	/// <remarks/>
	public string User_Name;
	
	/// <remarks/>
	public string Company_Code;
	
	/// <remarks/>
	public string CCNA;
	
	/// <remarks/>
	public string ECNA;
	
	/// <remarks/>
	public string URL;
	
	/// <remarks/>
	public bool Local_Service_Freeze_Flag;
	
	/// <remarks/>
	public string LD_Service_Freeze;
	
	/// <remarks/>
	public bool Active_Flag;
	
	/// <remarks/>
	public bool Notifications_Flag;
	
	/// <remarks/>
	public bool Acknowledge_Flag;
	
	/// <remarks/>
	public bool Process_Flag;
	
	/// <remarks/>
	public bool Backup_Status_Check_Flag;
	
	/// <remarks/>
	public Enum_Carrier_Code Carrier_Code;
	
	/// <remarks/>
	public bool Log_Flag;
	
	/// <remarks/>
	public string SEF_File;
	
	/// <remarks/>
	public string Version;
	
	/// <remarks/>
	public bool Preorder_Flag;
	
	/// <remarks/>
	public bool Order_Flag;
	
	/// <remarks/>
	public string Receiver_ID;
	
	/// <remarks/>
	public bool Test_Mode_Flag;
	
	/// <remarks/>
	public bool DoNotSend;
	
	/// <remarks/>
	public Enum_Verizon_Region Verizon_Region;
	
	/// <remarks/>
	public string StateList;
	
	/// <remarks/>
	public Enum_Product_Type DefaultProductType;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public enum Enum_Carrier_Code {
	
	/// <remarks/>
	NONE,
	
	/// <remarks/>
	ANDIAMO,
	
	/// <remarks/>
	ATX,
	
	/// <remarks/>
	BAYOU_INTERNET,
	
	/// <remarks/>
	BELLSOUTH,
	
	/// <remarks/>
	BROADVIEW,
	
	/// <remarks/>
	BROADWING,
	
	/// <remarks/>
	CLEARTEL,
	
	/// <remarks/>
	COVAD,
	
	/// <remarks/>
	ECI,
	
	/// <remarks/>
	EMAIL,
	
	/// <remarks/>
	FDN,
	
	/// <remarks/>
	GLOBAL_CROSSING,
	
	/// <remarks/>
	GULFTEL,
	
	/// <remarks/>
	INHOUSE,
	
	/// <remarks/>
	INTELECOM,
	
	/// <remarks/>
	LEVEL3,
	
	/// <remarks/>
	MCI,
	
	/// <remarks/>
	NECA,
	
	/// <remarks/>
	OTHER,
	
	/// <remarks/>
	PAETEC,
	
	/// <remarks/>
	PREMIER_GLOBAL,
	
	/// <remarks/>
	QWEST,
	
	/// <remarks/>
	RADIUS,
	
	/// <remarks/>
	RED_RIVER,
	
	/// <remarks/>
	RED_RIVER_PREPAID,
	
	/// <remarks/>
	REUNION,
	
	/// <remarks/>
	REUNION_DEBIT,
	
	/// <remarks/>
	SPRINT,
	
	/// <remarks/>
	SBC,
	
	/// <remarks/>
	TMC,
	
	/// <remarks/>
	TXLINK,
	
	/// <remarks/>
	UNIFIED_ARTS,
	
	/// <remarks/>
	USLEC,
	
	/// <remarks/>
	VERIZON,
	
	/// <remarks/>
	XO,
	
	/// <remarks/>
	CORDIA,
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public enum Enum_Verizon_Region {
	
	/// <remarks/>
	NA,
	
	/// <remarks/>
	EAST,
	
	/// <remarks/>
	WEST,
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ValidateAddressByTN_Request))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(EstimateDueDate_Request))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Order_Request))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(Availability_Request))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(OrderStatus_Request))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ReserveTN_Request))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(ValidateAddress_Request))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CustomerServiceRecord_Request))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(AvailableTNs_Request))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(CancelTN_Request))]
public class BaseRequest : BaseInformation {
	
	/// <remarks/>
	public string Version;
	
	/// <remarks/>
	public string RefPon;
	
	/// <remarks/>
	public string RefVersion;
	
	/// <remarks/>
	public int LogId;
	
	/// <remarks/>
	public string SecureKey;
	
	/// <remarks/>
	public Profile_Information myProfile;
	
	/// <remarks/>
	public AuthenticationResponse Auth;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class AuthenticationResponse : BaseInformation {
	
	/// <remarks/>
	public string Client;
	
	/// <remarks/>
	public string RequestIP;
	
	/// <remarks/>
	public string URL;
	
	/// <remarks/>
	public int TimeZoneOffset;
	
	/// <remarks/>
	public bool TimeZoneDST;
	
	/// <remarks/>
	public string ConnH2O;
	
	/// <remarks/>
	public string ConnGravity;
	
	/// <remarks/>
	public string ConnShared;
	
	/// <remarks/>
	public int AuthLogId;
	
	/// <remarks/>
	public bool Passed;
	
	/// <remarks/>
	public string ErrorReason;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class ValidateAddressByTN_Request : BaseRequest {
	
	/// <remarks/>
	public string TN;
	
	/// <remarks/>
	public string State;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class EstimateDueDate_Request : BaseRequest {
	
	/// <remarks/>
	public Order_Information myOrder;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class Order_Request : BaseRequest {
	
	/// <remarks/>
	public Order_Information myOrder;
	
	/// <remarks/>
	public bool AllowDuplicates;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class Availability_Request : BaseRequest {
	
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("LineList")]
	public Line_Information[] LineList;
	
	/// <remarks/>
	public Enum_Customer_Class CustomerClass;
	
	/// <remarks/>
	public string CLLI;
	
	/// <remarks/>
	public string NPANXX;
	
	/// <remarks/>
	public string State;
	
	/// <remarks/>
	public int HoursAge;
	
	/// <remarks/>
	public bool DontReturnAvailable;
	
	/// <remarks/>
	public string AvailableFilter;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class OrderStatus_Request : BaseRequest {
	
	/// <remarks/>
	public string PON;
	
	/// <remarks/>
	public string State;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class ReserveTN_Request : BaseRequest {
	
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("TN_List")]
	public string[] TN_List;
	
	/// <remarks/>
	public string State;
	
	/// <remarks/>
	public string CLLI;
	
	/// <remarks/>
	public string NPANXX;
	
	/// <remarks/>
	public Enum_TN_Special_Option Special_Option;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public enum Enum_TN_Special_Option {
	
	/// <remarks/>
	NOT_SPECIFIED,
	
	/// <remarks/>
	NONE,
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class ValidateAddress_Request : BaseRequest {
	
	/// <remarks/>
	public Address_Information myAddress;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class CustomerServiceRecord_Request : BaseRequest {
	
	/// <remarks/>
	public string TN;
	
	/// <remarks/>
	public string State;
	
	/// <remarks/>
	public string BAN;
	
	/// <remarks/>
	public Enum_Product_Type ProductType;
	
	/// <remarks/>
	public bool CarrierRecord;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class AvailableTNs_Request : BaseRequest {
	
	/// <remarks/>
	public int Quantity;
	
	/// <remarks/>
	public Address_Information myAddress;
	
	/// <remarks/>
	public Enum_TN_Special_Option Special_Option;
	
	/// <remarks/>
	public Enum_Customer_Class CustomerClass;
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://localhost/oc.portal.gravity/")]
public class CancelTN_Request : BaseRequest {
	
	/// <remarks/>
	public string ReservationNumber;
	
	/// <remarks/>
	public string State;
	
	/// <remarks/>
	public string CLLI;
}



