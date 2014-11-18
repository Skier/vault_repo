
    /*******************************************************************
    * PaymentHeader.as
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
        import Intuit.Sb.Cdm.vo.IdType;import Intuit.Sb.Cdm.vo.IdType;import Intuit.Sb.Cdm.vo.IdType;import Intuit.Sb.Cdm.vo.IdType;import Intuit.Sb.Cdm.vo.IdType;import Intuit.Sb.Cdm.vo.IdType;import Intuit.Sb.Cdm.vo.PaymentDetail;        

        [Bindable]
        [RemoteClass(alias="Intuit.Sb.Cdm.PaymentHeader")]
        public class PaymentHeader extends Intuit.Sb.Cdm.vo.HeaderBase
        {
          public function PaymentHeader(){}
        
        
          public var CustomerId:Intuit.Sb.Cdm.vo.IdType;
        
          public var CustomerName:String;
        
          public var JobId:Intuit.Sb.Cdm.vo.IdType;
        
          public var JobName:String;
        
          public var RemitToId:Intuit.Sb.Cdm.vo.IdType;
        
          public var RemitToName:String;
        
          public var ARAccountId:Intuit.Sb.Cdm.vo.IdType;
        
          public var ARAccountName:String;
        
          public var DepositToAccountId:Intuit.Sb.Cdm.vo.IdType;
        
          public var DepositToAccountName:String;
        
          public var PaymentMethodId:Intuit.Sb.Cdm.vo.IdType;
        
          public var PaymentMethodName:String;
        
          public var Detail:Intuit.Sb.Cdm.vo.PaymentDetail;
        
          public var TotalAmt:Number;
        
          public var TotalAmtSpecified:Boolean;
        
          public var UnappliedAmt:Number;
        
          public var UnappliedAmtSpecified:Boolean;
        
          public var ProcessPayment:Boolean;
        
          public var ProcessPaymentSpecified:Boolean;
        

          public override function toString():String
          {
           return  this.CustomerId + ": " 
                  + this.CustomerName + ": " 
                  + this.JobId + ": " 
                  + this.JobName + ": " 
                  + this.RemitToId + ": " 
                  + this.RemitToName + ": " 
                  + this.ARAccountId + ": " 
                  + this.ARAccountName + ": " 
                  + this.DepositToAccountId + ": " 
                  + this.DepositToAccountName + ": " 
                  + this.PaymentMethodId + ": " 
                  + this.PaymentMethodName + ": " 
                  + this.Detail + ": " 
                  + this.TotalAmt + ": " 
                  + this.TotalAmtSpecified + ": " 
                  + this.UnappliedAmt + ": " 
                  + this.UnappliedAmtSpecified + ": " 
                  + this.ProcessPayment + ": " 
                  + this.ProcessPaymentSpecified;
          }
      }
      
      }
      