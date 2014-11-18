package common.attachment
{
	import UI.landman.Composition;
	

	[Bindable]
	public class CompositeAttachmentsModel
	{
		public var isReadOnly:Boolean;
		
		public var uploadingInProgress:Boolean = false;
		
		public var composition:Composition;
		
		public function CompositeAttachmentsModel(isRO:Boolean) {
			isReadOnly = isRO;
		}
	}
	
}
