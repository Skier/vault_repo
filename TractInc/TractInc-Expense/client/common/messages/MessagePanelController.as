package common.messages
{
	import UI.manager.ManagerController;
	import mx.events.CollectionEvent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.controls.Alert;
	import App.Entity.MessagesDataObject;
	import mx.collections.ArrayCollection;
	import App.Entity.MessageDataObject;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.RemoteObject;
	import App.Entity.UserDataObject;
	
	[Bindable]
	public class MessagePanelController
	{
		
		public var view:MessagePanelView;
		public var model:MessagePanelModel;
		
		public function MessagePanelController(view:MessagePanelView) 
		{
			this.view = view;
		}
		
		public function init(userId:int):void 
		{
			model = new MessagePanelModel(userId);
			loadMessages();
		}
		
		public function onDgMessagesChanged():void 
		{
			var message:MessageDataObject = view.dgInboxMessages.selectedItem as MessageDataObject;
			if (message) {
        		var userService:RemoteObject = new RemoteObject("GenericDestination");
   			    userService.source = "TractInc.Expense.UserService";
       			userService.MarkAsRead.addEventListener(ResultEvent.RESULT,
       				function(result:ResultEvent):void {
       					var messageInfo:MessageDataObject = MessageDataObject(result.result);
       					message.IsRead = messageInfo.IsRead;
	       			}
    	   		);
       			userService.MarkAsRead.addEventListener(FaultEvent.FAULT, onFault);
       			userService.MarkAsRead(message);
			}
		}
		
		public function onClickCreate():void 
		{
			model.currentMessage = new MessageDataObject();
			model.currentMessage.SenderUserId = model.userId;
			view.vsMessages.selectedChild = view.msgDetail;
		}
		
		public function onClickDelete():void 
		{
			var message:MessageDataObject = view.dgInboxMessages.selectedItem as MessageDataObject;
			if (message) {
	       		var userService:RemoteObject = new RemoteObject("GenericDestination");
			    userService.source = "TractInc.Expense.UserService";
   				userService.RemoveMessage.addEventListener(ResultEvent.RESULT,
   					function(result:ResultEvent):void {
   						if (model.inboxMessages.contains(message)) {
   							model.inboxMessages.removeItemAt(model.inboxMessages.getItemIndex(message));
   						}
       				}
	   	   		);
   				userService.RemoveMessage.addEventListener(FaultEvent.FAULT, onFault);
   				userService.RemoveMessage(message.MessageId);
			}
		}
		
		public function onClickSend():void 
		{
			var toUser:UserDataObject = view.cbUsers.selectedItem as UserDataObject;
			if (!toUser) {
				return;
			}
			model.currentMessage.SenderUserId = model.userId;
			model.currentMessage.ReceiverUserId = toUser.UserId;
			model.currentMessage.Posted = new Date();
			model.currentMessage.Subject = view.txtSubject.text;
			model.currentMessage.Body = view.txtBody.text;
			
			view.vsMessages.selectedChild = view.msgLists;
			
       		var userService:RemoteObject = new RemoteObject("GenericDestination");
		    userService.source = "TractInc.Expense.UserService";
   			userService.PostMessage.addEventListener(ResultEvent.RESULT,
   				function(result:ResultEvent):void {
   					var messageInfo:MessageDataObject = MessageDataObject(result.result);
   					model.currentMessage.ReceiverLogin = messageInfo.ReceiverLogin;
   					model.currentMessage.SenderLogin = messageInfo.SenderLogin;
   					model.currentMessage.MessageId = messageInfo.MessageId;
   					model.sentMessages.addItem(model.currentMessage);
       			}
   	   		);
   			userService.PostMessage.addEventListener(FaultEvent.FAULT, onFault);
   			userService.PostMessage(model.currentMessage);
		}
		
		public function onClickCancel():void 
		{
			model.currentMessage = null;

			view.vsMessages.selectedChild = view.msgLists;
		}
		
		public function loadMessages():void 
		{
        	var userService:RemoteObject;
			userService = new RemoteObject("GenericDestination");
   		    userService.source = "TractInc.Expense.UserService";
       		userService.CheckMessages.addEventListener(ResultEvent.RESULT,
       			function(result:ResultEvent):void {
       				var messagesInfo:MessagesDataObject = MessagesDataObject(result.result);
       				
       				model.inboxMessages = new ArrayCollection(messagesInfo.InboxMessages);
       				model.sentMessages = new ArrayCollection(messagesInfo.SentMessages);
       				model.users = new ArrayCollection(messagesInfo.Users);
       				model.isLoaded = true;
       			}
       		);
       		userService.CheckMessages.addEventListener(FaultEvent.FAULT, onFault);
       		userService.CheckMessages(model.userId);
		}
		
		private function onFault(event:FaultEvent):void 
		{
			Alert.show(event.fault.message);
		}
		
	}
}
