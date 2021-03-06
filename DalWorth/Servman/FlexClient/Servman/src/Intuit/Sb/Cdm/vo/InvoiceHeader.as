
    /*******************************************************************
    * InvoiceHeader.as
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
        import Intuit.Sb.Cdm.vo.IdType;import defaultPackage.vo.String;import Intuit.Sb.Cdm.vo.IdType;import defaultPackage.vo.String;import defaultPackage.vo.Date;import Intuit.Sb.Cdm.vo.PhysicalAddress;import Intuit.Sb.Cdm.vo.PhysicalAddress;import defaultPackage.vo.String;import Intuit.Sb.Cdm.vo.IdType;import defaultPackage.vo.String;import defaultPackage.vo.Number;import defaultPackage.vo.Number;import Intuit.Sb.Cdm.vo.IdType;import defaultPackage.vo.String;        

        [Bindable]
        [RemoteClass(alias="Intuit.Sb.Cdm.InvoiceHeader")]
        public class InvoiceHeader extends Intuit.Sb.Cdm.vo.HeaderSales
        {
          public function InvoiceHeader(){}
        
        
          public var ARAccountId:Intuit.Sb.Cdm.vo.IdType;
        
          public var ARAccountName:defaultPackage.vo.String;
        
          public var SalesTermId:Intuit.Sb.Cdm.vo.IdType;
        
          public var SalesTermName:defaultPackage.vo.String;
        
          public var DueDate:defaultPackage.vo.Date;
        
          public var DueDateSpecified:Boolean;
        
          public var BillAddr:Intuit.Sb.Cdm.vo.PhysicalAddress;
        
          public var ShipAddr:Intuit.Sb.Cdm.vo.PhysicalAddress;
        
          public var BillEmail:defaultPackage.vo.String;
        
          public var ShipMethodId:Intuit.Sb.Cdm.vo.IdType;
        
          public var ShipMethodName:defaultPackage.vo.String;
        
          public var Balance:defaultPackage.vo.Number;
        
          public var BalanceSpecified:Boolean;
        
          public var Item:defaultPackage.vo.Number;
        
          public var ItemElementName:String;
        
          public var DiscountAccountId:Intuit.Sb.Cdm.vo.IdType;
        
          public var DiscountAccountName:defaultPackage.vo.String;
        
          public var DiscountTaxable:Boolean;
        
          public var DiscountTaxableSpecified:Boolean;
        
          public var TxnId:Array;
        

          public override function toString():String
          {
           return  this.ARAccountId + ": " 
                  + this.ARAccountName + ": " 
                  + this.SalesTermId + ": " 
                  + this.SalesTermName + ": " 
                  + this.DueDate + ": " 
                  + this.DueDateSpecified + ": " 
                  + this.BillAddr + ": " 
                  + this.ShipAddr + ": " 
                  + this.BillEmail + ": " 
                  + this.ShipMethodId + ": " 
                  + this.ShipMethodName + ": " 
                  + this.Balance + ": " 
                  + this.BalanceSpecified + ": " 
                  + this.Item + ": " 
                  + this.ItemElementName + ": " 
                  + this.DiscountAccountId + ": " 
                  + this.DiscountAccountName + ": " 
                  + this.DiscountTaxable + ": " 
                  + this.DiscountTaxableSpecified + ": " 
                  + this.TxnId;
          }
      }
      
      }
      