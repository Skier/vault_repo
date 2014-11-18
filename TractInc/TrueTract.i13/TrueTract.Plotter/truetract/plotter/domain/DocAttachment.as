package truetract.plotter.domain
{
    [Bindable]
	[RemoteClass(alias="TractInc.TrueTract.Entity.DocAttachmentInfo")]
	public class DocAttachment
	{
        public var DocumentAttachmentId:int;
        public var DocId:int;
        public var FileName:String;
        public var OriginalFileName:String;
	}
}