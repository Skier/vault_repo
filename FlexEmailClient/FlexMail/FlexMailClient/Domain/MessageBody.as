package Domain
{
    [Bindable]
    [RemoteClass(alias="Weborb.Samples.Email.Entities.MessageBodyInfo")]
    public class MessageBody {

        public var PlainBody:String = "";
        public var HtmlBody:File = null;
        public var Attachments:Array = [];
        public var Uid:String = "";
        
    }

}