package App.Entity
{
	
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.MessagesDataObject")]
	public class MessagesDataObject
	{
		
        public var InboxMessages:Array;

        public var SentMessages:Array;
        
        public var Users:Array;

	}
	
}
