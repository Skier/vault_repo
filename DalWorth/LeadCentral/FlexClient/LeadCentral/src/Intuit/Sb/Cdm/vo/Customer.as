
    /*******************************************************************
    * Customer.as
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
        import Intuit.Sb.Cdm.vo.IdType;import Intuit.Sb.Cdm.vo.Money;import Intuit.Sb.Cdm.vo.JobInfo;        

        [Bindable]
        [RemoteClass(alias="Intuit.Sb.Cdm.Customer")]
        public class Customer extends Intuit.Sb.Cdm.vo.RoleBase
        {
          public function Customer(){}
        
        
          public var Active:Boolean;
        
          public var ActiveSpecified:Boolean;
        
          public var ShowAs:String;
        
          public var CustomerTypeId:Intuit.Sb.Cdm.vo.IdType;
        
          public var CustomerTypeName:String;
        
          public var SalesTermId:Intuit.Sb.Cdm.vo.IdType;
        
          public var SalesTermName:String;
        
          public var SalesRepId:Intuit.Sb.Cdm.vo.IdType;
        
          public var SalesRepName:String;
        
          public var SalesTaxCodeId:Intuit.Sb.Cdm.vo.IdType;
        
          public var SalesTaxCodeName:String;
        
          public var TaxId:Intuit.Sb.Cdm.vo.IdType;
        
          public var TaxName:String;
        
          public var TaxGroupId:Intuit.Sb.Cdm.vo.IdType;
        
          public var TaxGroupName:String;
        
          public var PaymentMethodId:Intuit.Sb.Cdm.vo.IdType;
        
          public var PaymentMethodName:String;
        
          public var PriceLevelId:Intuit.Sb.Cdm.vo.IdType;
        
          public var PriceLevelName:String;
        
          public var OpenBalance:Intuit.Sb.Cdm.vo.Money;
        
          public var OpenBalanceDate:Date;
        
          public var OpenBalanceDateSpecified:Boolean;
        
          public var OpenBalanceWithJobs:Intuit.Sb.Cdm.vo.Money;
        
          public var CreditLimit:Intuit.Sb.Cdm.vo.Money;
        
          public var AcctNum:String;
        
          public var OverDueBalance:Intuit.Sb.Cdm.vo.Money;
        
          public var TotalRevenue:Intuit.Sb.Cdm.vo.Money;
        
          public var TotalExpense:Intuit.Sb.Cdm.vo.Money;
        
          public var JobInfo:Intuit.Sb.Cdm.vo.JobInfo;
        

          public override function toString():String
          {
           return  this.Active + ": " 
                  + this.ActiveSpecified + ": " 
                  + this.ShowAs + ": " 
                  + this.CustomerTypeId + ": " 
                  + this.CustomerTypeName + ": " 
                  + this.SalesTermId + ": " 
                  + this.SalesTermName + ": " 
                  + this.SalesRepId + ": " 
                  + this.SalesRepName + ": " 
                  + this.SalesTaxCodeId + ": " 
                  + this.SalesTaxCodeName + ": " 
                  + this.TaxId + ": " 
                  + this.TaxName + ": " 
                  + this.TaxGroupId + ": " 
                  + this.TaxGroupName + ": " 
                  + this.PaymentMethodId + ": " 
                  + this.PaymentMethodName + ": " 
                  + this.PriceLevelId + ": " 
                  + this.PriceLevelName + ": " 
                  + this.OpenBalance + ": " 
                  + this.OpenBalanceDate + ": " 
                  + this.OpenBalanceDateSpecified + ": " 
                  + this.OpenBalanceWithJobs + ": " 
                  + this.CreditLimit + ": " 
                  + this.AcctNum + ": " 
                  + this.OverDueBalance + ": " 
                  + this.TotalRevenue + ": " 
                  + this.TotalExpense + ": " 
                  + this.JobInfo;
          }
      }
      
      }
      