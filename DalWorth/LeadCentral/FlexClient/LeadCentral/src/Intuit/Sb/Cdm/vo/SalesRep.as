
    /*******************************************************************
    * SalesRep.as
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
    import Intuit.Sb.Cdm.enum.SalesRepTypeEnum;
            

    [Bindable]
    [RemoteClass(alias="Intuit.Sb.Cdm.SalesRep")]
    public class SalesRep extends Intuit.Sb.Cdm.vo.CdmBase
   	{
      	public function SalesRep(){}
    
    
    	private var nameOf:String;
		public function get NameOf():String { return nameOf; }
		public function set NameOf(value:String):void 
		{
			nameOf = value;
		}
    
      	public var NameOfSpecified:Boolean;
    
    	private var item:Object;
      	public function get Item():Object { return item; }
      	public function set Item(value:Object):void 
      	{
      		item = value;
      	}
    
      	public var Initials:String;
      
      	public var name:String;
      
      	public function updateName():void 
      	{
      		if (NameOf == null || Item == null)
      		{
      			name = "";
      			return;
      		}
      		
          	if (NameOf == SalesRepTypeEnum.Employee)
          		name = ("[" + SalesRepTypeEnum.Employee + "] " + Item["EmployeeName"] as String);
          	else if (NameOf == SalesRepTypeEnum.Vendor)
          		name = ("[" + SalesRepTypeEnum.Vendor + "] " + Item["VendorName"] as String);
          	else if (NameOf == SalesRepTypeEnum.Other)
          		name = ("[" + SalesRepTypeEnum.Other + "] " + Item["OtherNameName"] as String);
          	else
          		name = "undefined";
      	}
      
      	public override function toString():String
      	{
       		return  this.NameOf + ": " 
              	+ this.NameOfSpecified + ": " 
              	+ this.Item + ": " 
              	+ this.Initials;
      	}
  	}
  
}
      