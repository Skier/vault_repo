package TractInc.Domain
{
    import mx.collections.ArrayCollection;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.Client")]
    public class Client
    {
        public var ClientId:int;      
        public var ClientName:String;
        public var ClientAddress:String;
        
        private var _cList:Array;
        public function get CompanyList():Array { return _cList; }
        public function set CompanyList(value:Array):void 
        { 
            _cList = value;
        }

        private var _pList:Array;
        public function get PersonList():Array { return _pList; }
        public function set PersonList(value:Array):void 
        { 
            _pList = value;
        }
    }
}