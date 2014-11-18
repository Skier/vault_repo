package TractInc.Domain
{
    import mx.collections.ArrayCollection;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.Account")]
    public class Account
    {
        public var AccountId:int;      
        public var ClientId:int;      
        public var CompanyId:int;      
        public var AccountTypeId:int;      
        public var ParentAccountId:int;      
        public var AccountName:String;
        public var AccountNumber:String;

/*
        private var _cList:Array;
        public function get ContractRateList():Array { return _cList; }
        public function set ContractRateList(value:Array):void 
        { 
            _cList = value;
        }
*/
    }
}