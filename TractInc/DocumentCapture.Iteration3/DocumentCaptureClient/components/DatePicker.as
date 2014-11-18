package components
{
	import mx.utils.StringUtil;
	
	
	public class DatePicker extends MaskedInput
	{
		
		public function getDate():Date {

			var result:Date = null;

			var year:int = int(text.substr(0,4));
			var month:int = int(text.substr(4,2));
			var day:int = int(text.substr(6,2));
			
			if ( StringUtil.trim(text).length == 0 ) {
				return null;
			} else {
				return new Date(year, month - 1, day > 0 ? day : 1);
			}
			
		}
		
	}
		
}