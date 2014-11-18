package TractInc.Domain
{
    import mx.collections.ArrayCollection;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.Asset")]
    public class Asset
    {
        public var AssetId:int;      
        public var AssetTypeId:int;      
        public var CompanyId:int;      
        public var PersonId:int;      
        public var AssetName:String;

        private var _cList:Array;
        public function get AssetRateList():Array { return _cList; }
        public function set AssetRateList(value:Array):void 
        { 
            _cList = value;
        }
    }
}