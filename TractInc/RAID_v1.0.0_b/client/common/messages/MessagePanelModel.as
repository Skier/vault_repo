package common.messages
{
	import mx.collections.ArrayCollection;
	import App.Domain.User;
	import App.Domain.Message;
	
	[Bindable]
	public class MessagePanelModel
	{
		public var currentUser:User;
		public var inboxMessages:ArrayCollection;
		public var sentMessages:ArrayCollection;
		public var isLoaded:Boolean = false;
		public var currentMessage:Message;
	
		public function MessagePanelModel(user:User):void {
			currentUser = user;
			inboxMessages = new ArrayCollection();
			sentMessages = new ArrayCollection();
		}
	}
}