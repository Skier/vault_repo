
    /*******************************************************************
    * PaymentQuery.as
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
        import Intuit.Sb.Cdm.vo.SortByColumnPaymentWithOrder;        

        [Bindable]
        [RemoteClass(alias="Intuit.Sb.Cdm.PaymentQuery")]
        public class PaymentQuery extends Intuit.Sb.Cdm.vo.TransactionQueryBase
        {
          public function PaymentQuery(){}
        
        
          public var MinimumAmount:Number;
        
          public var MinimumAmountSpecified:Boolean;
        
          public var SortByColumn:Intuit.Sb.Cdm.vo.SortByColumnPaymentWithOrder;
        
          public var IncludeLine:Boolean;
        
          public var IncludeLineSpecified:Boolean;
        
          public var JobIdSet:Array;
        

          public override function toString():String
          {
           return  this.MinimumAmount + ": " 
                  + this.MinimumAmountSpecified + ": " 
                  + this.SortByColumn + ": " 
                  + this.IncludeLine + ": " 
                  + this.IncludeLineSpecified + ": " 
                  + this.JobIdSet;
          }
      }
      
      }
      