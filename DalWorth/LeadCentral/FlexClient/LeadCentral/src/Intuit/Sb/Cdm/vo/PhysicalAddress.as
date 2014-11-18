
    /*******************************************************************
    * PhysicalAddress.as
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
        [RemoteClass(alias="Intuit.Sb.Cdm.PhysicalAddress")]
        public class PhysicalAddress
        {
          public function PhysicalAddress(){}
        
        
          public var Id:IdType;
        
          public var Line1:String;
        
          public var Line2:String;
        
          public var Line3:String;
        
          public var Line4:String;
        
          public var Line5:String;
        
          public var City:String;
        
          public var County:String;
        
          public var CountyCode:String;
        
          public var Country:String;
        
          public var CountryCode:String;
        
          public var CountrySubDivisionCode:String;
        
          public var PostalCode:String;
        
          public var PostalCodeSuffix:String;
        
          public var GeoCode:String;
        
          public var DateLastVerified:Date;
        
          public var DateLastVerifiedSpecified:Boolean;
        
          public var Default:Boolean;
        
          public var DefaultSpecified:Boolean;
        
          public var Tag:Array;
        

          public virtual function toString():String
          {
           return  this.Id + ": " 
                  + this.Line1 + ": " 
                  + this.Line2 + ": " 
                  + this.Line3 + ": " 
                  + this.Line4 + ": " 
                  + this.Line5 + ": " 
                  + this.City + ": " 
                  + this.County + ": " 
                  + this.CountyCode + ": " 
                  + this.Country + ": " 
                  + this.CountryCode + ": " 
                  + this.CountrySubDivisionCode + ": " 
                  + this.PostalCode + ": " 
                  + this.PostalCodeSuffix + ": " 
                  + this.GeoCode + ": " 
                  + this.DateLastVerified + ": " 
                  + this.DateLastVerifiedSpecified + ": " 
                  + this.Default + ": " 
                  + this.DefaultSpecified + ": " 
                  + this.Tag;
          }
      }
      
      }
      