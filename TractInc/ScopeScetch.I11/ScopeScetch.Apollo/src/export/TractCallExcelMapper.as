package src.export
{
	import src.deedplotter.domain.TractCall;
	
	public class TractCallExcelMapper
	{
		
        public static function getExcelXml(tractCall:TractCall):String {
        	
        	var result:String = "";

        	if (!tractCall) {
        		return result;
        	}
        	
        	if (tractCall.CallDBValue) {
        	
				var newText:String = tractCall.CallDBValue;
	
	        	newText = newText.replace(/</, "&lt;");
	        	newText = newText.replace(/>/, "&gt;");
	        	newText = newText.replace(/&/, "&amp;");
	        	newText = newText.replace(/"/, "&quot;");
	        	newText = newText.replace(/\\n/, "&#10;");
	
	    		result += '\n   <Row>';
	    		result += '\n    <Cell><Data ss:Type="String">';
	    		result += newText;
	    		result += '</Data></Cell>';
	    		result += '\n    <Cell><Data ss:Type="String">';
	    		result += tractCall.CallType;
	    		result += '</Data></Cell>';
	    		result += '\n    <Cell><Data ss:Type="Number">';
	    		result += tractCall.CallOrder.toString();
	    		result += '</Data></Cell>';
	    		result += '\n    <Cell><Data ss:Type="Boolean">';
	    		result += tractCall.CreatedByMouse ? "1" : "0";
	    		result += '</Data></Cell>';
	    		result += '\n   </Row>';
        	}
    		
    		return result;
        	
        }
	}
}