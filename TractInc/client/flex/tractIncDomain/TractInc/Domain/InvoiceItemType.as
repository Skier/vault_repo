package TractInc.Domain
{
    import mx.collections.ArrayCollection;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.InvoiceItemType")]
    public class InvoiceItemType
    {
        public var InvoiceItemTypeId:int;      
        public var TypeName:String;
        public var IsCountable:Boolean;
        public var IsPresetRate:Boolean;
        public var IsSingle:Boolean;
        public var IsAttachRequired:Boolean;
    }
}