
    /*******************************************************************
    * RoleBase.as
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
        [RemoteClass(alias="Intuit.Sb.Cdm.RoleBase")]
        public class RoleBase extends Intuit.Sb.Cdm.vo.CdmBase
        {
		public function RoleBase(){}
        
        public function get AddressStr():String 
        {
        	var result:String = "";
        	
        	if (Address != null && Address.length > 0) 
        	{
        		var addr:PhysicalAddress = Address[0] as PhysicalAddress;
        		if (addr.Line1 != null && addr.Line1.length > 0)
        			result += addr.Line1;
        		if (addr.Line2 != null && addr.Line1.length > 0)
        			result += (" " + addr.Line2);
        		if (addr.Line3 != null && addr.Line1.length > 0)
        			result += (" " + addr.Line3);
        		if (addr.Line4 != null && addr.Line1.length > 0)
        			result += (" " + addr.Line4);
        		if (addr.Line5 != null && addr.Line1.length > 0)
        			result += (" " + addr.Line5);
        		if (addr.City != null && addr.Line1.length > 0)
        			result += (", " + addr.City);
        		if (addr.CountrySubDivisionCode != null && addr.Line1.length > 0)
        			result += (" " + addr.CountrySubDivisionCode);
        		if (addr.PostalCode != null && addr.Line1.length > 0)
        			result += (" " + addr.PostalCode);
        	}
        	
        	return result;
        }
        
        public function get PhonesStr():String 
        {
        	var result:String = "";
        	
        	if (Phone != null && Phone.length > 0) 
        	{
        		for each (var phone:TelephoneNumber in Phone) 
        		{
        			if (result != "")
        				result += ", ";
        			result += phone.FreeFormNumber;
        		}
        	}
        	
        	return result;
        }

          public var PartyReferenceId:IdType;
        
          public var TypeOf:String;
        
          public var Name:String;
        
          public var Address:Array;
        
          public var Phone:Array;
        
          public var WebSite:Array;
        
          public var Email:Array;
        
          public var ExternalId:String;
        
          public var Title:String;
        
          public var GivenName:String;
        
          public var MiddleName:String;
        
          public var FamilyName:String;
        
          public var Suffix:String;
        
          public var Gender:String;
        
          public var GenderSpecified:Boolean;
        
          public var BirthDate:Date;
        
          public var BirthDateSpecified:Boolean;
        
          public var UserId:String;
        
          public var OrgId:String;
        
          public var LegalName:String;
        
          public var DBAName:String;
        
          public var Industry:String;
        
          public var NonProfit:Boolean;
        
          public var LegalStructure:String;
        
          public var Category:String;
        
          public var TaxIdentifier:String;
        
          public var Notes:Array;
        

          public override function toString():String
          {
           return  this.PartyReferenceId + ": " 
                  + this.TypeOf + ": " 
                  + this.Name + ": " 
                  + this.Address + ": " 
                  + this.Phone + ": " 
                  + this.WebSite + ": " 
                  + this.Email + ": " 
                  + this.ExternalId + ": " 
                  + this.Title + ": " 
                  + this.GivenName + ": " 
                  + this.MiddleName + ": " 
                  + this.FamilyName + ": " 
                  + this.Suffix + ": " 
                  + this.Gender + ": " 
                  + this.GenderSpecified + ": " 
                  + this.BirthDate + ": " 
                  + this.BirthDateSpecified + ": " 
                  + this.UserId + ": " 
                  + this.OrgId + ": " 
                  + this.LegalName + ": " 
                  + this.DBAName + ": " 
                  + this.Industry + ": " 
                  + this.NonProfit + ": " 
                  + this.LegalStructure + ": " 
                  + this.Category + ": " 
                  + this.TaxIdentifier + ": " 
                  + this.Notes;
          }
      }
      
      }
      