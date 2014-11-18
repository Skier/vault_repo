
    /*******************************************************************
    * Invoice.as
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
        import Intuit.Sb.Cdm.vo.InvoiceHeader;        

        [Bindable]
        [RemoteClass(alias="Intuit.Sb.Cdm.Invoice")]
        public class Invoice extends Intuit.Sb.Cdm.vo.CdmBase
        {
          public function Invoice(){}
        
        
          public var Header:Intuit.Sb.Cdm.vo.InvoiceHeader;
        
          public var Line:Array;
        
          public var TaxLine:Array;
        

          public override function toString():String
          {
           return  this.Header + ": " 
                  + this.Line + ": " 
                  + this.TaxLine;
          }
      }
      
      }
      