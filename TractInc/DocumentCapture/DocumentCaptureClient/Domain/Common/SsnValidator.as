package Domain.Common
{
	import mx.validators.Validator;
	import mx.utils.StringUtil;
	import mx.validators.ValidationResult;

	public class SsnValidator extends Validator
	{
		override protected function doValidation(value:Object):Array {
			
			var results:Array = [];
			
			results = super.doValidation(value);
			
			if (results.length > 0)
				return results;
			
			var s:String = String(value);
			
			if (StringUtil.trim(s).length < 9 && StringUtil.trim(s).length > 0) {
				results.push(new ValidationResult(true, null, "",
					"SSN must have 9 digits"));
				return results;
			}
			
			return results;
		}
	}
}