package Domain.Common
{
	import mx.validators.Validator;
	import mx.utils.StringUtil;
	import mx.validators.ValidationResult;

	public class DatesValidator extends Validator
	{
		public var minDate:Date = new Date(1753, 0, 1);
		public var maxDate:Date = new Date();
		
		override protected function doValidation(value:Object):Array {
			
			var results:Array = [];
			
			results = super.doValidation(value);
			
			if (results.length > 0)
				return results;
			
			var s:String = String(value);
			
			var year:int = int(StringUtil.trim(s.substr(0,4)));
			var month:int = int(StringUtil.trim(s.substr(4,2)));
			var day:int = int(StringUtil.trim(s.substr(6,2)));
			
			if (year == 0 && month == 0 && day == 0) {
				return results;
			}
			
			if (year < minDate.fullYear || year > maxDate.fullYear) {
				results.push(new ValidationResult(true, null, "",
					"Year must be between " + minDate.fullYear.toString() + " and " + maxDate.fullYear.toString() ));
				return results;
			}

			if (month < 1 || month > 12) {
				results.push(new ValidationResult(true, null, "",
					"Please input month from 1 to 12" ));
				return results;
			}

			if (month < 1 || month > 12) {
				results.push(new ValidationResult(true, null, "",
					"Please input month from 1 to 12" ));
				return results;
			}

			var maxDay:int = 0;

			if ( month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12 ) {
				maxDay = 31;
			} else if ( month == 4 || month == 6 || month == 9 || month == 11 ) {
				maxDay = 30;
			} else if ( month == 2 && !isLeapYear(year) ) {
				maxDay = 28;
			} else {
				maxDay = 29;
			}

			if (day < 1 || day > maxDay) {
				results.push(new ValidationResult(true, null, "",
					"Please input correct day number"));
				return results;
			}
			
			return results;
		}
		
		private function isLeapYear(y:int):Boolean {
			if ((y % 4) == 0 ) {
				return true;
			} else {
				return false;
			}
		
		}
		
		
	}
}