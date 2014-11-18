package testLayouts.steps
{
	import flash.events.MouseEvent;
	
	import mx.controls.CheckBox;
	import mx.controls.treeClasses.TreeItemRenderer;
	import mx.controls.treeClasses.TreeListData;

	public class CheckBoxTreeItemRenderer extends TreeItemRenderer
	{
        public var chk:CheckBox;
        public var itemXml:XML;

		public function CheckBoxTreeItemRenderer()
		{
			super();
			mouseEnabled = false;
			icon = null;
		}
		
        override public function set data(value:Object):void
        {
            if(value != null)
            {
                super.data = value;
                
                this.itemXml = XML(value);
                if(this.itemXml.@selected == "true"){
                    this.chk.selected = true;
                }else{
                    this.chk.selected = false;
                }
            }
        }
        
        override protected function createChildren():void
        {
            super.createChildren();
            
            chk = new CheckBox();
            chk.addEventListener(MouseEvent.CLICK, handleChkClick);
            addChild(chk);
        }
        
        override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void
        {
            super.updateDisplayList(unscaledWidth,unscaledHeight);
        
            if(super.data)
            {
                this.chk.x = super.label.x
                super.label.x = this.chk.x + 17;
                this.chk.y = super.label.y+10;
            }
        }
                
        private function handleChkClick(evt:MouseEvent):void
        {
            if(this.chk.selected)
                this.itemXml.@selected = "true";
            else
                this.itemXml.@selected = "false";
        }
    }
}