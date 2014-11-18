package UI.Compose
{
    import flash.events.Event;
    
    import mx.containers.ControlBar;
    import mx.controls.CheckBox;
    import mx.controls.RichTextEditor;
    import mx.managers.IFocusManagerComponent;
    import flash.events.MouseEvent;
    import flash.events.KeyboardEvent;
    import mx.controls.Alert;
    import flash.events.FocusEvent;
    import mx.core.EventPriority;

    
    public class MessageBodyEditor extends RichTextEditor {
            
        private var usePlainTextCheck:CheckBox;
        
        //--------------------------------------------------------------------------
        //
        //  Properties
        //
        //--------------------------------------------------------------------------
    
        //----------------------------------
        //  usePlainText
        //----------------------------------
        private var _usePlainText:Boolean;
    
        public function get usePlainText():Boolean 
        {
            return this._usePlainText;
        }
        
        public function set usePlainText(value:Boolean):void 
        {
            this._usePlainText = value;
            this.toolbar.visible = !value;
            this.toolbar.includeInLayout = !value;
             
            if (value)
            {
                this.htmlText = this.text;
            }
        }

        //----------------------------------
        //  htmlText
        //----------------------------------

        override public function get htmlText():String{
            
            var html:String = super.htmlText;
            html = "<HTML><BODY><DIV STYLE=\"padding-top: 10px;\">" + html + "</DIV></BODY></HTML>";

            XML.ignoreWhitespace = false;

            var document:XML = new XML(html);
            
            /* convert font tags from the RTE version */
            convertFontsFromRTEHTML(document);
            adaptParagraphs(document);

            XML.prettyPrinting = false;
            html = replaceEmptyParagraps(document.toXMLString());
            
            return html;
        }
        
        override public function set htmlText(value:String):void 
        {
            super.htmlText = value;
            commitProperties();
        }
                
        //--------------------------------------------------------------------------
        //
        //  Overridden methods
        //
        //--------------------------------------------------------------------------
        
        override protected function createChildren():void 
        {
            super.createChildren();
              
            //create checkBox Html/Plain view switcher
            usePlainTextCheck = new CheckBox();
            usePlainTextCheck.label = "Use plain text.";
            usePlainTextCheck.addEventListener(MouseEvent.CLICK, changeContentType);
            usePlainTextCheck.selected = _usePlainText;
            usePlainTextCheck.tabEnabled = false;

            var cb:ControlBar = (this.controlBar as ControlBar);
            cb.addChild(usePlainTextCheck);

            //move ToolBar at the top of a panel
            cb.removeChildAt( 0 );
            addChildAt( toolbar, 0 );
                            
            textArea.tabIndex = 0;
            textArea.tabEnabled = true;
            
            linkTextInput.addEventListener(FocusEvent.FOCUS_OUT, onLinkTextFocusOut, false, 
                EventPriority.EFFECT);
            
            var child:*;
            for each (child in toolbar.getChildren()) 
            {
                if (child is IFocusManagerComponent)
                    child.tabEnabled = false;
            }

            for each (child in toolBar2.getChildren()) 
            {
                if (child is IFocusManagerComponent)
                    child.tabEnabled = false;
            }
        }
            
        //--------------------------------------------------------------------------
        //
        //  Methods
        //
        //--------------------------------------------------------------------------

        private function onLinkTextFocusOut(event:FocusEvent):void
        {
            var s:String = textArea.htmlText;
            var result:String = s.replace(/((?=<A HREF[^\>]*?>).*?<\/A>)/ig, "<U><FONT COLOR=\"#6495ED\">$1</FONT></U>");
            textArea.htmlText = result;    
        }
        
        private function changeContentType(event:Event):void
        {
            usePlainText = usePlainTextCheck.selected;
        }
        
        private function replaceEmptyParagraps(s:String):String 
        {
            var result:String = s.replace(/(<TEXTFORMAT((?!<\/FONT).)*?<\/TEXTFORMAT>)/g, "<br>");
            return result;
        }
        
        private function adaptParagraphs(document:XML) : void 
        {
            for each (var paragraph:XML in document..P) 
            {
                paragraph.@STYLE = "margin-top: -10px;";
            }
        }
        
        private function convertFontsFromRTEHTML(document:XML) : void 
        {
            
            for each (var font:XML in document..FONT) 
            {
                var sizeStr:String = font.@SIZE;
                if (sizeStr && sizeStr.length > 0) 
                {
                    // The RTE's idea of size is very different from HTML
                    // font sizes.  We need to scale this so it looks
                    // somewhat appropriate on the web.
                    var size:int = font.@SIZE;
                    size = size - 10;
                    font.@SIZE = size;
                }
            }
        }        
        
    }
}