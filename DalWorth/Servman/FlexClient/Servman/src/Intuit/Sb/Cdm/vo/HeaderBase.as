
    /*******************************************************************
    * HeaderBase.as
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
        import defaultPackage.vo.Date;        

        [Bindable]
        [RemoteClass(alias="Intuit.Sb.Cdm.HeaderBase")]
        public class HeaderBase
        {
          public function HeaderBase(){}
        
        
          public var DocNumber:String;
        
          public var TxnDate:defaultPackage.vo.Date;
        
          public var TxnDateSpecified:Boolean;
        
          public var Currency:String;
        
          public var CurrencySpecified:Boolean;
        
          public var Msg:String;
        
          public var Note:String;
        
          public var Status:String;
        

          public virtual function toString():String
          {
           return  this.DocNumber + ": " 
                  + this.TxnDate + ": " 
                  + this.TxnDateSpecified + ": " 
                  + this.Currency + ": " 
                  + this.CurrencySpecified + ": " 
                  + this.Msg + ": " 
                  + this.Note + ": " 
                  + this.Status;
          }
      }
      
      }
      