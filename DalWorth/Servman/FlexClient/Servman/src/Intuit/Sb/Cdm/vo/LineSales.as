
    /*******************************************************************
    * LineSales.as
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
        import defaultPackage.vo.Number;import Intuit.Sb.Cdm.vo.IdType;import defaultPackage.vo.String;import Intuit.Sb.Cdm.vo.IdType;import defaultPackage.vo.String;import defaultPackage.vo.String;import defaultPackage.vo.String;        

        [Bindable]
        [RemoteClass(alias="Intuit.Sb.Cdm.LineSales")]
        public class LineSales extends Intuit.Sb.Cdm.vo.LineBase
        {
          public function LineSales(){}
        
        
          public var Amount:defaultPackage.vo.Number;
        
          public var AmountSpecified:Boolean;
        
          public var ClassId:Intuit.Sb.Cdm.vo.IdType;
        
          public var ClassName:defaultPackage.vo.String;
        
          public var Taxable:Boolean;
        
          public var TaxableSpecified:Boolean;
        
          public var Items:Array;
        
          public var ItemsElementName:Array;
        
          public var SalesTaxCodeId:Intuit.Sb.Cdm.vo.IdType;
        
          public var SalesTaxCodeName:defaultPackage.vo.String;
        
          public var Custom1:defaultPackage.vo.String;
        
          public var Custom2:defaultPackage.vo.String;
        

          public override function toString():String
          {
           return  this.Amount + ": " 
                  + this.AmountSpecified + ": " 
                  + this.ClassId + ": " 
                  + this.ClassName + ": " 
                  + this.Taxable + ": " 
                  + this.TaxableSpecified + ": " 
                  + this.Items + ": " 
                  + this.ItemsElementName + ": " 
                  + this.SalesTaxCodeId + ": " 
                  + this.SalesTaxCodeName + ": " 
                  + this.Custom1 + ": " 
                  + this.Custom2;
          }
      }
      
      }
      