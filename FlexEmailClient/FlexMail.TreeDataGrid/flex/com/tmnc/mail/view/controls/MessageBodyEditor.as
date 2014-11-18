package com.tmnc.mail.view.controls
{
    import mx.controls.RichTextEditor;
    import mx.controls.CheckBox;
    import flash.events.Event;	
    import mx.containers.ControlBar;
    import mx.controls.richTextEditorClasses.ToolBar;
    import com.tmnc.mail.view.utils.RTEHTMLTransformer;
    import mx.controls.Spacer;
    
	public class MessageBodyEditor extends RichTextEditor {
			
		private var usePlainTextCheck:CheckBox;
		
		public function MessageBodyEditor(){
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
			this.toolbar.includeInLayout = !value;
			 
			if (value){
				this.htmlText = this.text;
			}
		}

	    //----------------------------------
	    //  msgBodyText
	    //----------------------------------

		public function get msgBodyText():String{
			if (_usePlainText){
				return this.text;
			}
			
			var html:String = this.htmlText;
	        var doc:XML;
	        
	        // Start the HTML by wrapping it in a top level messageContent div.
	        html = "<html><body><div class=\"messageContent\">" + html + "</div></body></html>";
	        doc = new XML(html);

	        // Ensure there's a consistent, top-level namespace.
	        doc.setNamespace(RTEHTMLTransformer.XHTMLNS);
        	
        	return RTEHTMLTransformer.transformFromRTEHTML(doc.toXMLString());
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

            //create checkBox Html/Plain view switcher
			usePlainTextCheck = new CheckBox();
			usePlainTextCheck.label = "Use plain text.";
			usePlainTextCheck.addEventListener("click", changeContentType);
			usePlainTextCheck.selected = _usePlainText;

			var cb:ControlBar = (this.controlBar as ControlBar);
			cb.addChild(usePlainTextCheck);

            //move ToolBar at the top of panel
            var tb:ToolBar = cb.removeChildAt(0) as ToolBar;
            this.addChildAt(tb, 0);
                            
            this.controlBar.width = 5;
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