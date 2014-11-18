
    /*******************************************************************
    * TaxLine.as
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
        import defaultPackage.vo.Number;import Intuit.Sb.Cdm.vo.IdType;import defaultPackage.vo.String;        

        [Bindable]
        [RemoteClass(alias="Intuit.Sb.Cdm.TaxLine")]
        public class TaxLine extends Intuit.Sb.Cdm.vo.LineBase
        {
          public function TaxLine(){}
        
        
          public var Amount:defaultPackage.vo.Number;
        
          public var AmountSpecified:Boolean;
        
          public var TaxId:Intuit.Sb.Cdm.vo.IdType;
        
          public var TaxName:defaultPackage.vo.String;
        

          public override function toString():String
          {
           return  this.Amount + ": " 
                  + this.AmountSpecified + ": " 
                  + this.TaxId + ": " 
                  + this.TaxName;
          }
      }
      
      }
      