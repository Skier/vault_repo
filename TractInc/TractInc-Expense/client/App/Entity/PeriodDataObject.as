package App.Entity
{
	import mx.formatters.DateFormatter;
	
	[Bindable]
	public class PeriodDataObject
	{
		public var label:String;
		public var year:int;
		public var month:int;
		public var isFirstPart:Boolean;
		
		public function PeriodDataObject(year:int, month:int, isFirstPart:Boolean) 
		{
			this.year = year;
			this.month = month;
			this.isFirstPart = isFirstPart;
			setLabel();
		}
		
		private function setLabel():void 
		{
			label = "";
			var date:Date = new Date(year, month - 1);

			var df:DateFormatter = new DateFormatter();

			if (isFirstPart) {
				df.formatString = "MMM D - 15, YYYY";
			} else {
				var lastDay:int = 31;
				switch (date.month) {
					case 1:
					lastDay = 28;
					break;

					case 3:
					lastDay = 30;
					break;

					case 5:
					lastDay = 30;
					break;

					case 8:
					lastDay = 30;
					break;

					case 10:
					lastDay = 30;
				}
				df.formatString = "MMM 16 - " + lastDay.toString() + ", YYYY";
			}

			label += df.format(date);

		}

	}
}