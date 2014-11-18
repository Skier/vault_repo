package util
{
	
	import mx.formatters.DateFormatter;
	import App.Entity.PeriodDataObject;
	
	public class DateUtil
	{
		
		private static const DATE_FORMAT:String = 'MM/DD/YYYY';
		
		private static var m_formatter:DateFormatter = new DateFormatter();
		
		public static function format(date:Date):String {
			m_formatter.formatString = DateUtil.DATE_FORMAT;
			return m_formatter.format(date);
		}
		
		public static function formatFromPeriod(period:PeriodDataObject):String {
			return ((period.month < 10)? "0": "") + period.month.toString() + "/" + ((period.isFirstPart)? "01": "16") + "/" + period.year.toString();
		}
		
	}
	
}
