package common.notes
{
	import weborb.data.ActiveCollection;
	import App.Domain.User;
	import weborb.data.ActiveRecord;
	
	[Bindable]
	public class NotesModel
	{
/* 
		private var _isLoaded:Boolean = false;
		public function set isLoaded(flag:Boolean):void 
		{ 
			_isLoaded = flag; 
		}
		public function get isLoaded():Boolean { return _isLoaded; }
 */		
		public var currentUser:User;
		public var item:*;
		
		public var itemId:int;
		public var itemType:String;
	}
}