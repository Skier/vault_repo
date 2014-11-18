package sjd.validators
{
	import mx.validators.ValidationResult;

	/**
	 * @class MultipTipsValidationResult
	 * @brief ValidationResult that uesd in MultipTipsValidation, returned in the result array of MultipTipsValidator.doValidation
	 * @author Jove
	 * @version 1.2
	 */
	public class MultipTipsValidationResult extends ValidationResult{
		
		public var errorFieldId:String = "";
		
		public function MultipTipsValidationResult(fieldId:String, isError:Boolean, subField:String="", errorCode:String="", errorMessage:String=""){
			this.errorFieldId = fieldId;
			super(isError, subField, errorCode, errorMessage);
		}
	}
}