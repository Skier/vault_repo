package com.tmnc.mail.view.controls
{
    import mx.controls.RichTextEditor;
    import mx.controls.CheckBox;
    import flash.events.Event;    
    import mx.containers.ControlBar;
    import mx.controls.richTextEditorClasses.ToolBar;
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
        
        override public function set htmlText(value:String):void {
            super.htmlText = value;
            commitProperties();
        }
            
        //--------------------------------------------------------------------------
        //
        //  Methods
        //
        //--------------------------------------------------------------------------

        private function changeContentType(event:Event):void{
            usePlainText = usePlainTextCheck.selected;
        }
        
        private function replaceEmptyParagraps(s:String):String {
            var result:String = s.replace(/(<TEXTFORMAT((?!<\/FONT).)*?<\/TEXTFORMAT>)/g, "<br>");
            return result;
        }
        
        private function adaptParagraphs(document:XML) : void {
            for each (var paragraph:XML in document..P) {
                paragraph.@STYLE = "margin-top: -10px;";
            }
        }
        
        private function convertFontsFromRTEHTML(document:XML) : void {
            
            for each (var font:XML in document..FONT) {
                var sizeStr:String = font.@SIZE;
                if (sizeStr && sizeStr.length > 0) {
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