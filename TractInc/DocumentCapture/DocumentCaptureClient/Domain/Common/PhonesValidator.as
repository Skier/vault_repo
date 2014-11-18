package Domain.Common
{
	import mx.validators.Validator;
	import mx.validators.ValidationResult;
	import mx.utils.StringUtil;

	public class PhonesValidator extends Validator
	{
		override protected function doValidation(value:Object):Array {
			
			var results:Array = [];
			
			results = super.doValidation(value);
			
			if (results.length > 0)
				return results;
			
			var s:String = String(value);
			
			if (StringUtil.trim(s).length < 10 && StringUtil.trim(s).length > 0) {
				results.push(new ValidationResult(true, null, "",
					"Phone number must have 10 digits"));
				return results;
			}
			
			return results;
		}
	}
}