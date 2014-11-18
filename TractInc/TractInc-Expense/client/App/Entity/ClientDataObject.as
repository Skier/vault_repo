package App.Entity
{
	
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.ClientDataObject")]
	public class ClientDataObject
	{
		
        public var ClientId:int;

        public var ClientName:String;

        public var ClientAddress:String;

        private var _active:Boolean;
        public function get Active():Boolean {
        	return _active;
        }
        public function set Active(value:Boolean):void {
        	_active = value;
        }
        public function get ActiveString():String {
        	return (_active)? "Active": "Inactive";
        }

        public var Deleted:Boolean;
        
        public var DefaultRates:Array = new Array();
        
	}
	
}
