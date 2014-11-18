
    /*******************************************************************
    * PaymentLine.as
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
        import Intuit.Sb.Cdm.vo.IdType;import Intuit.Sb.Cdm.vo.IdType;import Intuit.Sb.Cdm.vo.IdType;        

        [Bindable]
        [RemoteClass(alias="Intuit.Sb.Cdm.PaymentLine")]
        public class PaymentLine extends Intuit.Sb.Cdm.vo.LineBase
        {
          public function PaymentLine(){}
        
        
          public var Amount:Number;
        
          public var AmountSpecified:Boolean;
        
          public var TxnId:Intuit.Sb.Cdm.vo.IdType;
        
          public var TxnNum:String;
        
          public var DiscountAmt:Number;
        
          public var DiscountAmtSpecified:Boolean;
        
          public var DiscountId:Intuit.Sb.Cdm.vo.IdType;
        
          public var DiscountName:String;
        
          public var DiscountAccountId:Intuit.Sb.Cdm.vo.IdType;
        
          public var DiscountAccountName:String;
        

          public override function toString():String
          {
           return  this.Amount + ": " 
                  + this.AmountSpecified + ": " 
                  + this.TxnId + ": " 
                  + this.TxnNum + ": " 
                  + this.DiscountAmt + ": " 
                  + this.DiscountAmtSpecified + ": " 
                  + this.DiscountId + ": " 
                  + this.DiscountName + ": " 
                  + this.DiscountAccountId + ": " 
                  + this.DiscountAccountName;
          }
      }
      
      }
      