
    /*******************************************************************
    * CreditChargeResponse.as
    * Copyright (C) 2006-2010 Midnight Coders, Inc.
    *
    * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
    * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
    * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
    * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
    * LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
    * OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
    * WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
    ********************************************************************/
    
        package Intuit.Sb.Cdm.vo
        {
        import flash.utils.ByteArray;
        import mx.collections.ArrayCollection;
        import defaultPackage.vo.Date;        

        [Bindable]
        [RemoteClass(alias="Intuit.Sb.Cdm.CreditChargeResponse")]
        public class CreditChargeResponse
        {
          public function CreditChargeResponse(){}
        
        
          public var CCTransId:String;
        
          public var Status:String;
        
          public var ResultCode:String;
        
          public var ResultMsg:String;
        
          public var MerchantAcctNum:String;
        
          public var CardSecurityCodeMatch:String;
        
          public var CardSecurityCodeMatchSpecified:Boolean;
        
          public var AuthCode:String;
        
          public var AvsStreet:String;
        
          public var AvsZip:String;
        
          public var SecurityCode:String;
        
          public var ReconBatchId:String;
        
          public var PaymentGroupingCode:String;
        
          public var TxnAuthorizationTime:defaultPackage.vo.Date;
        
          public var TxnAuthorizationTimeSpecified:Boolean;
        
          public var TxnAuthorizationStamp:String;
        
          public var ClientTransID:String;
        

          public virtual function toString():String
          {
           return  this.CCTransId + ": " 
                  + this.Status + ": " 
                  + this.ResultCode + ": " 
                  + this.ResultMsg + ": " 
                  + this.MerchantAcctNum + ": " 
                  + this.CardSecurityCodeMatch + ": " 
                  + this.CardSecurityCodeMatchSpecified + ": " 
                  + this.AuthCode + ": " 
                  + this.AvsStreet + ": " 
                  + this.AvsZip + ": " 
                  + this.SecurityCode + ": " 
                  + this.ReconBatchId + ": " 
                  + this.PaymentGroupingCode + ": " 
                  + this.TxnAuthorizationTime + ": " 
                  + this.TxnAuthorizationTimeSpecified + ": " 
                  + this.TxnAuthorizationStamp + ": " 
                  + this.ClientTransID;
          }
      }
      
      }
      