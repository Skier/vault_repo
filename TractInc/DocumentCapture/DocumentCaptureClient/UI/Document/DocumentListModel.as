package UI.Document
{
	import mx.collections.ArrayCollection;
	import Domain.Document;
	
	[Bindable]
	public class DocumentListModel
	{
		public var Documents:ArrayCollection;
		public var CurrentDocument:Document;
	
		public function DocumentListModel():void{
			
		}
	}
}