package common.messages
{
	import UI.manager.ManagerController;
	import App.Domain.User;
	import mx.events.CollectionEvent;
	import App.Domain.ActiveRecords;
	import App.Domain.Message;
	import weborb.data.DynamicLoadEvent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.controls.Alert;
	
	[Bindable]
	public class MessagePanelController
	{
		
		public var view:MessagePanelView;
		public var model:MessagePanelModel;
		
		public function MessagePanelController(view:MessagePanelView) 
		{
			this.view = view;
		}
		
		public function init(user:User):void 
		{
			model = new MessagePanelModel(user);
			loadMessages();
		}
		
		public function onDgMessagesChanged():void 
		{
			var message:Message = view.dgInboxMessages.selectedItem as Message;
			if (message) {
				message.IsRead = true;
				message.save();
			}
		}
		
		public function onClickCreate():void 
		{
			model.currentMessage = new Message();
			model.currentMessage.RelatedUser = model.currentUser;
			view.vsMessages.selectedChild = view.msgDetail;
		}
		
		public function onClickDelete():void 
		{
			var message:Message = view.dgInboxMessages.selectedItem as Message;
			if (message) {
				message.remove();
			}
		}
		
		public function onClickSend():void 
		{
			var toUser:User = view.cbUsers.selectedItem as User;
			if (!toUser) {
				return;
			}
			model.currentMessage.RelatedUser1 = model.currentUser;
			model.currentMessage.RelatedUser = toUser;
			model.currentMessage.Posted = new Date();
			model.currentMessage.Subject = view.txtSubject.text;
			model.currentMessage.Body = view.txtBody.text;
			
			view.vsMessages.selectedChild = view.msgLists;
			
			model.currentMessage.save(false, new Responder(onMessageSaved, onFault));
			
		}
		
		public function onClickCancel():void 
		{
			model.currentMessage = null;

			view.vsMessages.selectedChild = view.msgLists;
		}
		
		public function loadMessages():void 
		{
			model.inboxMessages = ActiveRecords.Message.findByReceiverUserId(model.currentUser.UserId);
			model.inboxMessages.addEventListener("loaded", onInboxMessagesLoaded);

			model.sentMessages = ActiveRecords.Message.findBySenderUserId(model.currentUser.UserId);
			model.sentMessages.addEventListener("loaded", onSentMessagesLoaded);
		}
		
		private function onInboxMessagesLoaded(event:DynamicLoadEvent):void 
		{
			model.inboxMessages.removeEventListener("loaded", onInboxMessagesLoaded);
		}
		
		private function onSentMessagesLoaded(event:DynamicLoadEvent):void 
		{
			model.sentMessages.removeEventListener("loaded", onSentMessagesLoaded);
		}
		
		private function onMessageSaved(message:Message):void 
		{
			model.sentMessages.addItem(message);
		}
		
		private function onFault(event:FaultEvent):void 
		{
			Alert.show(event.fault.message);
		}
		
	}
}