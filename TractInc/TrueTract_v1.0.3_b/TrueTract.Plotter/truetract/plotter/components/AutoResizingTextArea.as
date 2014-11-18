package truetract.plotter.components
{
    import flash.events.KeyboardEvent;
    import flash.events.TextEvent;
    
    import mx.controls.TextArea;

    /**
     *  A TextArea control that will resizes immediately when text is changed.
     */
    public class AutoResizingTextArea extends TextArea
    {
        
        public function AutoResizingTextArea()
        {
            super();
            
            this.horizontalScrollPolicy = "off";
            this.verticalScrollPolicy = "off";
        }

        override protected function createChildren():void {
            super.createChildren();

            textField.useRichTextClipboard = false;
            textField.wordWrap = false;
            textField.addEventListener(KeyboardEvent.KEY_DOWN, textField_keyDownHandler);

            addEventListener(TextEvent.TEXT_INPUT, textInputHandler);
        }
        
        override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void
        {
            super.updateDisplayList(unscaledWidth, unscaledHeight);

            textField.scrollH = 0;
        }
        
        override public function validateSize(recursive:Boolean=false):void {
            super.validateSize(recursive);

            //adjust component size to textSize
            height = textField.textHeight + viewMetrics.bottom + viewMetrics.top + 5;
            width = getMaxRowWidth() + 10;
        }
        
        private function getMaxRowWidth():Number 
        {
            var result:Number = 0;
            
            for each (var rowString:String in textField.text.split("\r")) 
            {
                var rowWidth:Number = measureText(rowString).width;
                
                if (result < rowWidth) 
                {
                    result = rowWidth;
                }
            }
            
            return result;
        }
        
        protected function textInputHandler(event:TextEvent):void
        {
            invalidateSize();
        }

        protected function textField_keyDownHandler(event:KeyboardEvent):void
        {
            invalidateSize();
        }
                
    }
}