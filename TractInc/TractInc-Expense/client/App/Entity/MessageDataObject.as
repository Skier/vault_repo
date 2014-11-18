package App.Entity
{
	
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.MessageDataObject")]
	public class MessageDataObject
	{
		
        public var MessageId:int;

        public var SenderUserId:int;

        public var ReceiverUserId:int;

        public var Posted:Date;

        public var Subject:String;

        public var Body:String;

        public var IsRead:Boolean;

        public var SenderLogin:String;

        public var ReceiverLogin:String;

	}
	
}
