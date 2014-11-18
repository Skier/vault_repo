package Domain
{
    [Bindable]
    [RemoteClass(alias="Weborb.Samples.Email.Entities.MessageInfo")]
    public class Message {

        public var Sent:Date;
        public var From:MailAddress;
        public var Subject:String = "";
        public var To:Array = [];
        public var Cc:Array = [];
        public var Bcc:Array = [];
        public var Size:int = 0;
        public var AttachmentDir:String = "";
        public var Uid:String = "";
        public var HasAttachments:Boolean = false;
        public var MessageId:String = "";
        public var InReplyTo:String = "";
        public var Body:MessageBody = null;
        public var Status:int = NORMAL;
        
        public static const NORMAL:int = 0;
        public static const SENDING:int = 1;
        public static const BODY_RETRIEVING:int = 2;
        public static const BODY_ERROR:int = 3;
    }
}