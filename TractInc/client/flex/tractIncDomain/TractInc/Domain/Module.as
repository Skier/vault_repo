package TractInc.Domain
{
    import mx.collections.ArrayCollection;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.Module")]
    public class Module
    {
        public var ModuleId:int;      
        public var ModuleTypeId:int;      
        public var ShortName:String;
        public var Description:String;
        public var Url:String;
        
        private var _permissions:Array;
        public function get PermissionList():Array { return _permissions; }
        public function set PermissionList(value:Array):void 
        { 
            _permissions = value;
        }
    }
}