package com.affilia.util
{
	import mx.collections.ArrayCollection;
	import mx.formatters.DateFormatter;
	
	public class DateUtil
	{
		private static const weekDaysShort:Array = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];

		public static function getDateTimeStr(value:Date):String 
		{
        	var df:DateFormatter = new DateFormatter();
        	df.formatString = "MM/DD/YYYY L:NN A";

        	return df.format(value);
		}
		
		public static function getDateStr(value:Date):String 
		{
        	var df:DateFormatter = new DateFormatter();
        	df.formatString = "MM/DD/YYYY";

        	return df.format(value);
		}
		
		public static function getTimeStr(value:Date):String 
		{
        	var df:DateFormatter = new DateFormatter();
        	df.formatString = "L:NN A";

        	return df.format(value);
		}
		
		public static function getTime24Str(value:Date):String 
		{
        	var df:DateFormatter = new DateFormatter();
        	df.formatString = "J:NN";

        	return df.format(value);
		}
		
		public static function getWeekDayName(index:int):String 
		{
			return weekDaysShort[index - 1] as String;
		}

		public static function getWeekDays():ArrayCollection 
		{
			var result:ArrayCollection = new ArrayCollection();
			
			for (var i:int = 1; i < 8; i++)
			{
				result.addItem({data:(i), label:getWeekDayName(i)});
			}
			
			return result;
		}

		public static function getDayTimes():ArrayCollection 
		{
			var result:ArrayCollection = new ArrayCollection();
			
			var halfHour:Number = 1800000;
			var startDate:Date = new Date(1900,0,1,0,0,0,0);

			for (var i:int = 0; i < 48; i++)
			{
				var itemDate:Date = new Date(startDate.getTime() + ( i*halfHour ));
				var item:Object = new Object();
					item.data = getTimeStr(itemDate);
					item.label = getTimeStr(itemDate);
					item.value = getTime24Str(itemDate);
				result.addItem(item);
			}
			return result;
		}

		public function DateUtil()
		{
		}

	}
}