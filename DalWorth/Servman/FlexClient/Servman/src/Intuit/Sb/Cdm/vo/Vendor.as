
    /*******************************************************************
    * Vendor.as
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
        import defaultPackage.vo.String;import Intuit.Sb.Cdm.vo.IdType;import defaultPackage.vo.String;import Intuit.Sb.Cdm.vo.IdType;import defaultPackage.vo.String;import Intuit.Sb.Cdm.vo.Money;import defaultPackage.vo.Date;import Intuit.Sb.Cdm.vo.Money;import defaultPackage.vo.String;        

        [Bindable]
        [RemoteClass(alias="Intuit.Sb.Cdm.Vendor")]
        public class Vendor extends Intuit.Sb.Cdm.vo.RoleBase
        {
          public function Vendor(){}
        
        
          public var Active:Boolean;
        
          public var ActiveSpecified:Boolean;
        
          public var ShowAs:defaultPackage.vo.String;
        
          public var VendorTypeId:Intuit.Sb.Cdm.vo.IdType;
        
          public var VendorTypeName:defaultPackage.vo.String;
        
          public var SalesTermId:Intuit.Sb.Cdm.vo.IdType;
        
          public var SalesTermName:defaultPackage.vo.String;
        
          public var OpenBalance:Intuit.Sb.Cdm.vo.Money;
        
          public var OpenBalanceDate:defaultPackage.vo.Date;
        
          public var OpenBalanceDateSpecified:Boolean;
        
          public var CreditLimit:Intuit.Sb.Cdm.vo.Money;
        
          public var AcctNum:defaultPackage.vo.String;
        
          public var Vendor1099:Boolean;
        
          public var Vendor1099Specified:Boolean;
        

          public override function toString():String
          {
           return  this.Active + ": " 
                  + this.ActiveSpecified + ": " 
                  + this.ShowAs + ": " 
                  + this.VendorTypeId + ": " 
                  + this.VendorTypeName + ": " 
                  + this.SalesTermId + ": " 
                  + this.SalesTermName + ": " 
                  + this.OpenBalance + ": " 
                  + this.OpenBalanceDate + ": " 
                  + this.OpenBalanceDateSpecified + ": " 
                  + this.CreditLimit + ": " 
                  + this.AcctNum + ": " 
                  + this.Vendor1099 + ": " 
                  + this.Vendor1099Specified;
          }
      }
      
      }
      