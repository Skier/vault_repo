
    /*******************************************************************
    * CreditChargeInfo.as
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
                

        [Bindable]
        [RemoteClass(alias="Intuit.Sb.Cdm.CreditChargeInfo")]
        public class CreditChargeInfo
        {
          public function CreditChargeInfo(){}
        
        
          public var Number:String;
        
          public var Token:String;
        
          public var Type:String;
        
          public var TypeSpecified:Boolean;
        
          public var NameOnAcct:String;
        
          public var CcExpirMn:int;
        
          public var CcExpirMnSpecified:Boolean;
        
          public var CcExpirYr:int;
        
          public var CcExpirYrSpecified:Boolean;
        
          public var BillAddrStreet:String;
        
          public var ZipCode:String;
        
          public var Cvv:String;
        
          public var CommercialCardCode:String;
        
          public var CCTxnMode:String;
        
          public var CCTxnModeSpecified:Boolean;
        
          public var CCTxnType:String;
        
          public var CCTxnTypeSpecified:Boolean;
        

          public virtual function toString():String
          {
           return  this.Number + ": " 
                  + this.Token + ": " 
                  + this.Type + ": " 
                  + this.TypeSpecified + ": " 
                  + this.NameOnAcct + ": " 
                  + this.CcExpirMn + ": " 
                  + this.CcExpirMnSpecified + ": " 
                  + this.CcExpirYr + ": " 
                  + this.CcExpirYrSpecified + ": " 
                  + this.BillAddrStreet + ": " 
                  + this.ZipCode + ": " 
                  + this.Cvv + ": " 
                  + this.CommercialCardCode + ": " 
                  + this.CCTxnMode + ": " 
                  + this.CCTxnModeSpecified + ": " 
                  + this.CCTxnType + ": " 
                  + this.CCTxnTypeSpecified;
          }
      }
      
      }
      