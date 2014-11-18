package UI.Compose
{

	import flash.events.Event;
	
	import mx.core.ClassFactory;
	
	public class EmailInputField extends AutoComplete {
		
		public function EmailInputField(){
			super;
			this.filterFunction = substringFilterFunction;
			
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
    	
 	}
}