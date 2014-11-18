package tractInc.domain.packages
{
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.Package.ClientManager.ClientManagerPackage")]
    public class ClientManagerPackage
    {
        private var _contractStatusList:Array;
        
        public function get ContractStatusList():Array 
        { 
            return _contractStatusList; 
        }
        
        public function set ContractStatusList(value:Array):void 
        { 
            _contractStatusList = value;
        }
    }
}