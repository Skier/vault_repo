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
			    var startIndex:int = data.toString().indexOf(_searchText);
			    var endIndex:int = data.toString().lastIndexOf(_searchText);
                
                if (startIndex > 0 && endIndex > 0){
                    var _searchTextCS:String = data.toString().substring(startIndex, endIndex);
				    super.textField.htmlText = data.toString().replace(_searchTextCS, "<b>" + _searchTextCS + "</b>");
                }
			}
		}
		
	}
}
