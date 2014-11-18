package src.deedplotter.domain
{
    [Bindable] [RemoteClass(alias="TractInc.DeedPro.Entity.TractListInfo")]
    public class TractListInfo
    {
        public var tractId:int;
        public var referenceName:String;
        public var docId:int;
    }
}