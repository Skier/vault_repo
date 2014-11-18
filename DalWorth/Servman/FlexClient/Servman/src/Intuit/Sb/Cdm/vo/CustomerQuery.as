
    /*******************************************************************
    * CustomerQuery.as
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
        import Intuit.Sb.Cdm.vo.SortByColumnCustomerWithOrder;import defaultPackage.vo.String;import defaultPackage.vo.String;        

        [Bindable]
        [RemoteClass(alias="Intuit.Sb.Cdm.CustomerQuery")]
        public class CustomerQuery extends Intuit.Sb.Cdm.vo.NameQueryBase
        {
          public function CustomerQuery(){}
        
        
          public var SortByColumn:Intuit.Sb.Cdm.vo.SortByColumnCustomerWithOrder;
        
          public var FirstLastName:defaultPackage.vo.String;
        
          public var MinimumBalance:Number;
        
          public var MinimumBalanceSpecified:Boolean;
        
          public var Item:defaultPackage.vo.String;
        
          public var ItemElementName:String;
        
          public var OpenBalanceWithJobs:Boolean;
        

          public override function toString():String
          {
           return  this.SortByColumn + ": " 
                  + this.FirstLastName + ": " 
                  + this.MinimumBalance + ": " 
                  + this.MinimumBalanceSpecified + ": " 
                  + this.Item + ": " 
                  + this.ItemElementName + ": " 
                  + this.OpenBalanceWithJobs;
          }
      }
      
      }
      