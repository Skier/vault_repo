package com.tmnc.mail.view.controls
{
    import com.adobe.flex.extras.controls.AutoComplete;
    import mx.core.ClassFactory;
    import flash.events.Event;
    
    public class EmailInputField extends AutoComplete {
        
        public function EmailInputField(){
            super;
            this.filterFunction = substringFilterFunction;
            this.keepLocalHistory = false;
            
            this.itemRenderer = new ClassFactory(EmailInputIR);
            addEventListener("typedTextChange", typedTextChangedHandler);
        }

        private function substringFilterFunction(element:*, text:String):Boolean {
            var label:String = itemToLabel(element);
            return(label.toLowerCase().indexOf(text.toLowerCase())!=-1);
        }

        private function typedTextChangedHandler(event:Event):void {
            (this.itemRenderer as ClassFactory).properties = { searchText: this.typedText };
        }        
        
/*         override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void {
            var textBefore:String = text;
    
            super.updateDisplayList(unscaledWidth, unscaledHeight);
    
              if(selectedIndex == -1 && textInput.text != "")
                textInput.text = textBefore;
        }
 */    }
}