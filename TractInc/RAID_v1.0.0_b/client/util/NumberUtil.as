package util
{
	
	public class NumberUtil
	{
		
		public static function fraction(val:int, divider:int):String {
			if (val == 0) {
				return "0";
			}
			var days:int = val / divider;
			var hours:int = val % divider;
			if (hours == 0){
				return days.toString();
			}
			var hoursString:String = "";
			switch (hours) {
				case 1:
					hoursString = "1/8";
					break;
				case 2:
					hoursString = "1/4";
					break;
				case 3:
					hoursString = "3/8";
					break;
				case 4:
					hoursString = "1/2";
					break;
				case 5:
					hoursString = "5/8";
					break;
				case 6:
					hoursString = "3/4";
					break;
				case 7:
					hoursString = "7/8";
					break;
			}
			return ((0 == days) ? "" : String(days)) + " " + hoursString;
		}
		
	}
	
}
