package TractInc.Domain
{
    import mx.collections.ArrayCollection;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.Contract")]
    public class Contract
    {
        public var ContractId:int;      
        public var ClientId:int;      
        public var CompanyId:int;      
        public var ContractStatusId:int;      
        public var ContractName:String;
        public var StartDate:Date;
        public var EndDate:Date;

        private var _cList:Array;
        public function get ContractRateList():Array { return _cList; }
        public function set ContractRateList(value:Array):void 
        { 
            _cList = value;
        }
    }
}