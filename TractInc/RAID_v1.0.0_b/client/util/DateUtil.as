package util
{
	
	import mx.formatters.DateFormatter;
	
	public class DateUtil
	{
		
		private static const DATE_FORMAT:String = 'MM/DD/YYYY';
		
		private static var m_formatter:DateFormatter = new DateFormatter();
		
		public static function format(date:Date):String {
			m_formatter.formatString = DateUtil.DATE_FORMAT;
			return m_formatter.format(date);
		}
		
	}
	
}
