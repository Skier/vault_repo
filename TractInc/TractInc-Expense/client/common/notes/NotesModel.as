package common.notes
{
	import weborb.data.ActiveCollection;
	import App.Domain.User;
	import weborb.data.ActiveRecord;
	import App.Entity.UserDataObject;
	
	[Bindable]
	public class NotesModel
	{

		public var item:*;
		
		public var itemId:int;
		public var itemType:String;
	}
}