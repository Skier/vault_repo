
    /*******************************************************************
    * HeaderSales.as
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
        import Intuit.Sb.Cdm.vo.IdType;import defaultPackage.vo.String;import Intuit.Sb.Cdm.vo.IdType;import defaultPackage.vo.String;import Intuit.Sb.Cdm.vo.IdType;import defaultPackage.vo.String;import Intuit.Sb.Cdm.vo.IdType;import defaultPackage.vo.String;import Intuit.Sb.Cdm.vo.IdType;import defaultPackage.vo.String;import Intuit.Sb.Cdm.vo.IdType;import defaultPackage.vo.String;import defaultPackage.vo.String;import defaultPackage.vo.String;import defaultPackage.vo.Date;import defaultPackage.vo.Number;import Intuit.Sb.Cdm.vo.IdType;import defaultPackage.vo.String;import Intuit.Sb.Cdm.vo.IdType;import defaultPackage.vo.String;import defaultPackage.vo.Number;import defaultPackage.vo.Number;import defaultPackage.vo.Number;import defaultPackage.vo.String;        

        [Bindable]
        [RemoteClass(alias="Intuit.Sb.Cdm.HeaderSales")]
        public class HeaderSales extends Intuit.Sb.Cdm.vo.HeaderBase
        {
          public function HeaderSales(){}
        
        
          public var CustomerId:Intuit.Sb.Cdm.vo.IdType;
        
          public var CustomerName:defaultPackage.vo.String;
        
          public var JobId:Intuit.Sb.Cdm.vo.IdType;
        
          public var JobName:defaultPackage.vo.String;
        
          public var RemitToId:Intuit.Sb.Cdm.vo.IdType;
        
          public var RemitToName:defaultPackage.vo.String;
        
          public var ClassId:Intuit.Sb.Cdm.vo.IdType;
        
          public var ClassName:defaultPackage.vo.String;
        
          public var SalesRepId:Intuit.Sb.Cdm.vo.IdType;
        
          public var SalesRepName:defaultPackage.vo.String;
        
          public var SalesTaxCodeId:Intuit.Sb.Cdm.vo.IdType;
        
          public var SalesTaxCodeName:defaultPackage.vo.String;
        
          public var PONumber:defaultPackage.vo.String;
        
          public var FOB:defaultPackage.vo.String;
        
          public var ShipDate:defaultPackage.vo.Date;
        
          public var ShipDateSpecified:Boolean;
        
          public var SubTotalAmt:defaultPackage.vo.Number;
        
          public var SubTotalAmtSpecified:Boolean;
        
          public var TaxId:Intuit.Sb.Cdm.vo.IdType;
        
          public var TaxName:defaultPackage.vo.String;
        
          public var TaxGroupId:Intuit.Sb.Cdm.vo.IdType;
        
          public var TaxGroupName:defaultPackage.vo.String;
        
          public var TaxRate:defaultPackage.vo.Number;
        
          public var TaxRateSpecified:Boolean;
        
          public var TaxAmt:defaultPackage.vo.Number;
        
          public var TaxAmtSpecified:Boolean;
        
          public var TotalAmt:defaultPackage.vo.Number;
        
          public var TotalAmtSpecified:Boolean;
        
          public var ToBePrinted:Boolean;
        
          public var ToBePrintedSpecified:Boolean;
        
          public var ToBeEmailed:Boolean;
        
          public var ToBeEmailedSpecified:Boolean;
        
          public var Custom:defaultPackage.vo.String;
        

          public override function toString():String
          {
           return  this.CustomerId + ": " 
                  + this.CustomerName + ": " 
                  + this.JobId + ": " 
                  + this.JobName + ": " 
                  + this.RemitToId + ": " 
                  + this.RemitToName + ": " 
                  + this.ClassId + ": " 
                  + this.ClassName + ": " 
                  + this.SalesRepId + ": " 
                  + this.SalesRepName + ": " 
                  + this.SalesTaxCodeId + ": " 
                  + this.SalesTaxCodeName + ": " 
                  + this.PONumber + ": " 
                  + this.FOB + ": " 
                  + this.ShipDate + ": " 
                  + this.ShipDateSpecified + ": " 
                  + this.SubTotalAmt + ": " 
                  + this.SubTotalAmtSpecified + ": " 
                  + this.TaxId + ": " 
                  + this.TaxName + ": " 
                  + this.TaxGroupId + ": " 
                  + this.TaxGroupName + ": " 
                  + this.TaxRate + ": " 
                  + this.TaxRateSpecified + ": " 
                  + this.TaxAmt + ": " 
                  + this.TaxAmtSpecified + ": " 
                  + this.TotalAmt + ": " 
                  + this.TotalAmtSpecified + ": " 
                  + this.ToBePrinted + ": " 
                  + this.ToBePrintedSpecified + ": " 
                  + this.ToBeEmailed + ": " 
                  + this.ToBeEmailedSpecified + ": " 
                  + this.Custom;
          }
      }
      
      }
      