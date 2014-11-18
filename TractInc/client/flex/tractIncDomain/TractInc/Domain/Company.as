package TractInc.Domain
{
    import mx.collections.ArrayCollection;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.Company")]
    public class Company
    {
        public var CompanyId:int;      
        public var CompanyName:String;
        
        private var _cList:Array;
        public function get ClientList():Array { return _cList; }
        public function set ClientList(value:Array):void 
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