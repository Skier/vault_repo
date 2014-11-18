package com.llsvc.client.lms.view.tract
{
	import mx.collections.ArrayCollection;
	import mx.containers.VBox;
	import mx.events.CollectionEvent;
	import mx.events.CollectionEventKind;

	public class SectionsContainer extends VBox
	{
		public function SectionsContainer()
		{
			super();
		}
		
		private var _sections:ArrayCollection;
		[Bindable]
		public function get sections():ArrayCollection { return _sections; }
		public function set sections(value:ArrayCollection):void 
		{
			_sections = value;
			_sections.addEventListener(CollectionEvent.COLLECTION_CHANGE, sectionsChangeHandler);
			refreshSections();
		}
		
		private function sectionsChangeHandler(event:CollectionEvent):void 
		{
			var i:uint;
			
		    switch (event.kind) {
		        case CollectionEventKind.ADD:
		            for (i = 0; i < event.items.length; i++) {
		            	addSectionBox(TractSection(event.items[i]));
		            }
		            break;
		
		        case CollectionEventKind.REMOVE:
		            for (i = 0; i < event.items.length; i++) {
		                removeSectionBox(TractSection(event.items[i]));
		            }
		            break;
		
		        case CollectionEventKind.RESET:
		            refreshSections();
		            break;
		    }
		}
		
		private function addSectionBox(section:TractSection):void 
		{
			var sectionBox:TractsSectionRenderer = new TractsSectionRenderer();
			sectionBox.percentWidth = 100;
			sectionBox.section = section;
			addChild(sectionBox);
		}
		
		private function removeSectionBox(section:TractSection):void 
		{
			var sectionBox:TractsSectionRenderer;
			for (var i:int = 0; i < getChildren().length; i++) 
			{
				sectionBox = this.getChildAt(i) as TractsSectionRenderer;
				if (sectionBox.section == section) 
				{
					removeChildAt(i);
					return;
				}
			}
		}
		
		private function refreshSections():void 
		{
			removeAllChildren();
			
			for each (var section:TractSection in sections) 
			{
				addSectionBox(section);
			}
		}
	}
}