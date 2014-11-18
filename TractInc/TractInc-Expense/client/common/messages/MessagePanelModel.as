package common.messages
{
	
	import mx.collections.ArrayCollection;
	import App.Entity.MessageDataObject;
	
	[Bindable]
	public class MessagePanelModel
	{
		
		public var userId:int;
		public var inboxMessages:ArrayCollection;
		public var sentMessages:ArrayCollection;
		public var isLoaded:Boolean = false;
		public var currentMessage:MessageDataObject;
		public var users:ArrayCollection;
	
		public function MessagePanelModel(userId:int):void {
			this.userId = userId;
		}
		
	}
	
}
