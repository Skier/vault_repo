package truetract.plotter.validators
{
    import mx.validators.ValidationResult;
    import mx.validators.Validator;

    import truetract.plotter.utils.GeoBearing;

    public class GeoBearingValidator extends Validator
    {

       private var results:Array;

       override protected function doValidation(value:Object):Array {

            results = super.doValidation(value);
            
            if (results.length > 0)
                return results;
        
            try {
                GeoBearing.Parse(String(value));
            } catch (e:Error){
                results.push(new ValidationResult(true, null, null,  e.message));
            }
            
            return results;
        }
        
    }
}