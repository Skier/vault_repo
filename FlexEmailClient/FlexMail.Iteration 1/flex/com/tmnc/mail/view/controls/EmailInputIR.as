package com.tmnc.mail.view.controls
{
    import mx.core.IDataRenderer;
    import mx.controls.Text;
    
    public class EmailInputIR extends Text
    {
        private var _searchText:String = "";
        
        public function set searchText(value:String):void{
            _searchText = value;
        }

        // We need to make bold searched text KEEPING original letter case
        override protected function updateDisplayList(w:Number, h:Number):void {
            super.updateDisplayList(w, h);
            
            if (_searchText != "" && data != null){
                var originalString:String = String(data);
                
                var startIndex:int = originalString.toLowerCase().indexOf(_searchText.toLowerCase());
                
                if (startIndex != -1){
                    
                    var pattern:RegExp = new RegExp("(" + _searchText + ")", "i");
                    var highlightString:String = originalString.replace(pattern, "<b>$1</b>");
                    highlightString = replaceHtmlTagsEntities(highlightString);
                    
                    htmlText = highlightString;
                }
            }
        }
        
        //replace HTML tags symbols to the corresponding character entities.
        //But do not change <b>..</b> tags
        private function replaceHtmlTagsEntities(s:String):String {
            s = s.replace(/<(\/?)b>/g, "__LT__$1b__GT__");
            s = s.replace(/</g, "&lt;");
            s = s.replace(/>/g, "&gt;");
            s = s.replace(/"/g, "&quot;");
            s = s.replace(/__LT__(\/?)b__GT__/g, "<$1b>");
            return s;
        }
        
    }
}
