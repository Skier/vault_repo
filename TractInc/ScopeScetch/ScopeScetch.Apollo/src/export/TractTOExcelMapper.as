package src.export
{
	import src.deedplotter.domain.TractTextObject;
	
	public class TractTOExcelMapper
	{
		
		public static function getExcelXml(textObject:TractTextObject):String {
		
        	var result:String = "";
        	
        	if (!textObject) {
        		return result;
        	}
        	
        	if (textObject.Text) {
        	
	        	var newText:String = textObject.Text;
	        	
	        	newText = newText.replace(/</, "&lt;");
	        	newText = newText.replace(/>/, "&gt;");
	        	newText = newText.replace(/&/, "&amp;");
	        	newText = newText.replace(/"/, "&quot;");
	        	newText = newText.replace(/\\n/, "&#10;");
	
	    		result += '\n   <Row>';
	    		result += '\n    <Cell><Data ss:Type="String">';
	    		result += newText;
	    		result += '</Data></Cell>';
	    		result += '\n    <Cell><Data ss:Type="Number">';
	    		result += textObject.Easting.toString();
	    		result += '</Data></Cell>';
	    		result += '\n    <Cell><Data ss:Type="Number">';
	    		result += textObject.Northing.toString();
	    		result += '</Data></Cell>';
	    		result += '\n    <Cell><Data ss:Type="Number">';
	    		result += textObject.Rotation.toString();
	    		result += '</Data></Cell>';
	    		result += '\n   </Row>';

        	}

			return result;        	
        }
	}
}