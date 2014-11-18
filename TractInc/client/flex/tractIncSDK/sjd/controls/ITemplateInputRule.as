package sjd.controls
{
	public interface ITemplateInputRule{
		
		/**
		 * Validate if the template char is a wildcard.
		 * @param templateChar The template char
		 * @return The validate result
		 */
		function validateChar(templateChar:String):Boolean;
		
		/**
		 * Validate if the input char matchs the template char.
		 * @param templateChar The template char
		 * @param inputChar The input char
		 * @return The validate result
		 */
		function validateCharType(templateChar:String, inputChar:String):Boolean;
	}
}