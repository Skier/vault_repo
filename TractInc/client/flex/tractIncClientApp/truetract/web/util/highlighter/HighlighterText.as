package truetract.web.util.highlighter
{
//	import mx.controls.TextArea;
	import mx.binding.utils.ChangeWatcher;
	import mx.events.PropertyChangeEvent;
	import mx.core.UITextField;
	import mx.controls.Label;

	public class HighlighterText extends Label
	{
        private var _highlightString:String;
        [Bindable]
        public function get highlightString():String { return _highlightString; }
        public function set highlightString(value:String):void 
        {
        	_highlightString = value;

        	ChangeWatcher.watch(this, "highlightString", highlightStringChangeHandler);
        	ChangeWatcher.watch(this, "text", highlightStringChangeHandler);
        	
        	callLater(showHighlight);
        }

        private function highlightStringChangeHandler(event:*):void
        {
        	callLater(showHighlight);
        }
        
        public function showHighlight():void 
        {
        	if (highlightString == null || highlightString.length == 0 || this.text == null || this.text.length == 0)
        		return;
        	
        	var ind:int = this.text.toUpperCase().indexOf(highlightString.toUpperCase());
        	var html:String;
        	
        	if (ind != -1) 
        	{
        		html = this.text.slice(0, ind);
        		html += "<font color='#ff0000'>";
        		html += this.text.slice(ind, ind + highlightString.length);
        		html += "</font>";
        		html += this.text.slice(ind + highlightString.length);
	        	this.htmlText = html;
        	}
        }
        
        public function hideHighlight():void 
        {
        	this.htmlText = null;
        }
        
	}
}