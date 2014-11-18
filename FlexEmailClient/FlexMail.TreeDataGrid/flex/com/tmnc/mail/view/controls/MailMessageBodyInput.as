package com.tmnc.mail.view.controls
{
	import mx.controls.RichTextEditor;
	import mx.controls.CheckBox;
    import flash.events.Event;	
    import mx.containers.ControlBar;
    import mx.controls.richTextEditorClasses.ToolBar;      
    
	public class MailMessageBodyInput extends RichTextEditor {
			
		private var usePlainTextCheck:CheckBox;
		
		public function MailMessageBodyInput(){
			super;
		}

	    //--------------------------------------------------------------------------
	    //
	    //  Properties
	    //
	    //--------------------------------------------------------------------------
	
	    //----------------------------------
	    //  usePlainText
	    //----------------------------------
		private var _usePlainText:Boolean;
    
		public function get usePlainText ():Boolean {
			return this._usePlainText;
		}
		
		public function set usePlainText (value:Boolean):void {
			this._usePlainText = value;
			this.toolbar.visible = !value;
			
			if (value){
				this.htmlText = this.text;
			}
		}

	    //----------------------------------
	    //  msgBodyText
	    //----------------------------------

		public function get msgBodyText():String{
			return (_usePlainText) 
				? this.text 
				: this.htmlText ;
		}
		
		public function set msgBodyText(value:String):void{
			this.text = value;
		}
				
	    //--------------------------------------------------------------------------
	    //
	    //  Overridden methods
	    //
	    //--------------------------------------------------------------------------
	    
    	override protected function createChildren():void {
      		super.createChildren();

			usePlainTextCheck = new CheckBox();
			usePlainTextCheck.label = "Use plain text.";
			usePlainTextCheck.addEventListener("click", changeContentType);
			usePlainTextCheck.selected = _usePlainText;

			var cb:ControlBar = (this.controlBar as ControlBar);
			cb.addChild(usePlainTextCheck);
			
			this.removeChild(this.controlBar);
			this.addChildAt(cb, 0);
    	}
		
	    //--------------------------------------------------------------------------
	    //
	    //  Methods
	    //
	    //--------------------------------------------------------------------------

		private function changeContentType(event:Event):void{
			usePlainText = usePlainTextCheck.selected;
		}
	}
}