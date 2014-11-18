
    /*******************************************************************
    * TelephoneNumber.as
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
        import Intuit.Sb.Cdm.vo.IdType;        

        [Bindable]
        [RemoteClass(alias="Intuit.Sb.Cdm.TelephoneNumber")]
        public class TelephoneNumber
        {
          public function TelephoneNumber(){}
        
        
          public var Id:IdType;
        
          public var DeviceType:String;
        
          public var CountryCode:String;
        
          public var AreaCode:String;
        
          public var ExchangeCode:String;
        
          public var Extension:String;
        
          public var FreeFormNumber:String;
        
          public var PIN:String;
        
          public var DateLastVerified:Date;
        
          public var DateLastVerifiedSpecified:Boolean;
        
          public var Default:Boolean;
        
          public var DefaultSpecified:Boolean;
        
          public var Tag:Array;
        

          public virtual function toString():String
          {
           return  this.Id + ": " 
                  + this.DeviceType + ": " 
                  + this.CountryCode + ": " 
                  + this.AreaCode + ": " 
                  + this.ExchangeCode + ": " 
                  + this.Extension + ": " 
                  + this.FreeFormNumber + ": " 
                  + this.PIN + ": " 
                  + this.DateLastVerified + ": " 
                  + this.DateLastVerifiedSpecified + ": " 
                  + this.Default + ": " 
                  + this.DefaultSpecified + ": " 
                  + this.Tag;
          }
      }
      
      }
      