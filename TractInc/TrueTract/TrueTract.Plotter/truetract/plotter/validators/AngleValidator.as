package truetract.plotter.validators
{
    import mx.validators.ValidationResult;
    import mx.validators.Validator;

    import truetract.utils.Angle;

    public class AngleValidator extends Validator
    {
    	
       private var results:Array;

       override protected function doValidation(value:Object):Array {

            results = super.doValidation(value);
            
            if (results.length > 0)
                return results;
        	
        	var angle:Angle;
            try {
                angle = Angle.Parse(String(value));

				if (angle.degree > 360 || angle.degree < 0) {
	                results.push(new ValidationResult(true, null, null,  "Angle should be between 0 and 360 degrees."));
				}
            } catch (e:Error){
                results.push(new ValidationResult(true, null, null,  e.message));
            }

            return results;
        }
        
    }
}