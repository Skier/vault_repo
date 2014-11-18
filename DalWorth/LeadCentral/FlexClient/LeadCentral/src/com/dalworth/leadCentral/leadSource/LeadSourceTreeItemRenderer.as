package com.dalworth.leadCentral.leadSource
{
	import com.dalworth.leadCentral.domain.LeadSource;
	
	import flash.events.MouseEvent;
	
	import mx.containers.HBox;
	import mx.controls.Label;
	import mx.controls.treeClasses.TreeItemRenderer;

	public class LeadSourceTreeItemRenderer extends TreeItemRenderer
	{
        [Embed(source="/assets/icons16/user.png")]
        [Bindable]
        private var imgUser:Class;

		private var hBox:HBox;
		private var hUserBox:HBox;
        private var lblName:Label;
        private var lblEmail:Label;
        private var lblPhone:Label;

        private var leadSource:LeadSource;

		public function LeadSourceTreeItemRenderer()
		{
			super();
			mouseEnabled = false;
		}
		
        override public function set data(value:Object):void
        {
            if(value != null)
            {
                super.data = value;
                this.leadSource = LeadSource(value);
            }
        }
        
        override protected function createChildren():void
        {
            super.createChildren();
            
            hBox = new HBox();
            hBox.percentWidth = 100;
            hBox.percentHeight = 100;
            hBox.setStyle("verticalAlign", "middle");

            if (leadSource != null) 
            {
	            lblName = new Label();
	            lblName.text = leadSource.Name;
	            lblName.width = 150;
	            lblName.setStyle("fontSize", 12);
	            lblName.setStyle("fontWeight", "bold");
	            
	            hBox.addChild(lblName);
	            
	            if (leadSource.RelatedUser != null)
	            {
		            hUserBox = new HBox();
		            hUserBox.percentWidth = 100;
		            hUserBox.percentHeight = 100;
		            hUserBox.setStyle("verticalAlign", "middle");
		            hUserBox.setStyle("paddingLeft", 2);
		            hUserBox.setStyle("paddingRight", 2);
		            hUserBox.setStyle("paddingTop", 2);
		            hUserBox.setStyle("paddingBottom", 2);
		            hUserBox.setStyle("backgroundColor", 0xFFFFCC);
		            hUserBox.setStyle("backgroundAlpha", 0.5);

		            lblEmail = new Label();
		            lblEmail.text = leadSource.RelatedUser.Email;
		            lblEmail.width = 150;
		            lblEmail.setStyle("fontSize", 12);

		            lblPhone = new Label();
		            lblPhone.text = leadSource.RelatedUser.Phone;
		            lblPhone.width = 150;
		            lblPhone.setStyle("fontSize", 12);
		            
		            hUserBox.addChild(lblEmail);
		            hUserBox.addChild(lblPhone);
		            
		            hBox.addChild(hUserBox);
	            }
            }
            
            addChild(hBox);
        }
        
        override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void
        {
            super.updateDisplayList(unscaledWidth,unscaledHeight);

            if(super.data)
            {
            	this.hBox.x = label.x;
            	this.hBox.y = label.y;
            	
                //this.chk.x = super.label.x
                //super.label.x = this.chk.x + 17;
                //this.chk.y = super.label.y+10;
            }
        }
/*                 
        private function handleChkClick(evt:MouseEvent):void
        {
            if(this.chk.selected)
                this.itemXml.@selected = "true";
            else
                this.itemXml.@selected = "false";
        }
 */
    }
}