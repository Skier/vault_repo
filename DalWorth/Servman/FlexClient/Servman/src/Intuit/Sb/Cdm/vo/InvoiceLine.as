
    /*******************************************************************
    * InvoiceLine.as
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
        import defaultPackage.vo.Date;import Intuit.Sb.Cdm.vo.IdType;import Intuit.Sb.Cdm.vo.IdType;        

        [Bindable]
        [RemoteClass(alias="Intuit.Sb.Cdm.InvoiceLine")]
        public class InvoiceLine extends Intuit.Sb.Cdm.vo.LineSales
        {
          public function InvoiceLine(){}
        
        
          public var ServiceDate:defaultPackage.vo.Date;
        
          public var ServiceDateSpecified:Boolean;
        
          public var TxnId:Intuit.Sb.Cdm.vo.IdType;
        
          public var TxnLineId:Intuit.Sb.Cdm.vo.IdType;
        

          public override function toString():String
          {
           return  this.ServiceDate + ": " 
                  + this.ServiceDateSpecified + ": " 
                  + this.TxnId + ": " 
                  + this.TxnLineId;
          }
      }
      
      }
      