﻿////------------------------------------------------------------------------------
//// <autogenerated>
////     This code was generated by a tool.
////     Runtime Version: 1.1.4322.2032
////
////     Changes to this file may cause incorrect behavior and will be lost if 
////     the code is regenerated.
//// </autogenerated>
////------------------------------------------------------------------------------
//
//// 
//// This source code was auto-generated by wsdl, Version=1.1.4322.2032.
//// 
//using System.Diagnostics;
//using System.Xml.Serialization;
//using System;
//using System.Web.Services.Protocols;
//using System.ComponentModel;
//using System.Web.Services;
//
//
///// <remarks/>
//[System.Diagnostics.DebuggerStepThroughAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Web.Services.WebServiceBindingAttribute(Name="DPIPaymentSoapBinding", Namespace="http://dpi.service.webservices.rentway.com")]
//public class RentwayPayServiceProxy : System.Web.Services.Protocols.SoapHttpClientProtocol {
//    
//    /// <remarks/>
//    public RentwayPayServiceProxy() {
//        this.Url = "https://rwlptst.rentway.com:11080/webServices/services/DPIPayment";
//    }
//    
//    /// <remarks/>
//    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://dpi.service.webservices.rentway.com", ResponseNamespace="http://dpi.service.webservices.rentway.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
//    [return: System.Xml.Serialization.XmlElementAttribute("postDPIPaymentReturn", IsNullable=true)]
//    public object postDPIPayment([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] object xmlData) {
//        object[] results = this.Invoke("postDPIPayment", new object[] {
//                    xmlData});
//        return ((object)(results[0]));
//    }
//    
//    /// <remarks/>
//    public System.IAsyncResult BeginpostDPIPayment(object xmlData, System.AsyncCallback callback, object asyncState) {
//        return this.BeginInvoke("postDPIPayment", new object[] {
//                    xmlData}, callback, asyncState);
//    }
//    
//    /// <remarks/>
//    public object EndpostDPIPayment(System.IAsyncResult asyncResult) {
//        object[] results = this.EndInvoke(asyncResult);
//        return ((object)(results[0]));
//    }
//    
//    /// <remarks/>
//    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://dpi.service.webservices.rentway.com", ResponseNamespace="http://dpi.service.webservices.rentway.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
//    [return: System.Xml.Serialization.XmlElementAttribute("reverseDPIPaymentReturn", IsNullable=true)]
//    public object reverseDPIPayment([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] object xmlData) {
//        object[] results = this.Invoke("reverseDPIPayment", new object[] {
//                    xmlData});
//        return ((object)(results[0]));
//    }
//    
//    /// <remarks/>
//    public System.IAsyncResult BeginreverseDPIPayment(object xmlData, System.AsyncCallback callback, object asyncState) {
//        return this.BeginInvoke("reverseDPIPayment", new object[] {
//                    xmlData}, callback, asyncState);
//    }
//    
//    /// <remarks/>
//    public object EndreverseDPIPayment(System.IAsyncResult asyncResult) {
//        object[] results = this.EndInvoke(asyncResult);
//        return ((object)(results[0]));
//    }
//    
//    /// <remarks/>
//    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://dpi.service.webservices.rentway.com", ResponseNamespace="http://dpi.service.webservices.rentway.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
//    [return: System.Xml.Serialization.XmlElementAttribute("retrieveDailyDPITransactionsReturn", IsNullable=true)]
//    public object retrieveDailyDPITransactions([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] object xmlData) {
//        object[] results = this.Invoke("retrieveDailyDPITransactions", new object[] {
//                    xmlData});
//        return ((object)(results[0]));
//    }
//    
//    /// <remarks/>
//    public System.IAsyncResult BeginretrieveDailyDPITransactions(object xmlData, System.AsyncCallback callback, object asyncState) {
//        return this.BeginInvoke("retrieveDailyDPITransactions", new object[] {
//                    xmlData}, callback, asyncState);
//    }
//    
//    /// <remarks/>
//    public object EndretrieveDailyDPITransactions(System.IAsyncResult asyncResult) {
//        object[] results = this.EndInvoke(asyncResult);
//        return ((object)(results[0]));
//    }
//}
