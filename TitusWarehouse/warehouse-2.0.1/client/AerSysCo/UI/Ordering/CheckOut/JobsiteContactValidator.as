package AerSysCo.UI.Ordering.CheckOut
{
    import mx.validators.StringValidator;
    import mx.validators.ValidationResult;

    public class JobsiteContactValidator 
        extends StringValidator 
    {
        public function JobsiteContactValidator(){
            super();
        }
        
        protected override function doValidation(val:Object):Array {
            var results:Array = super.doValidation(val);
            var v:String = String(val);
            var check:RegExp = /.*[0-9].*[0-9].*[0-9].*[0-9].*/;
            if ( !check.test(v) ) {
                results.push(new ValidationResult(true, "txtJobsite", "txtJobsite", "Jobsite phone must have at less 4 digits")); 
            } 
            return results;
        }
        
    }
}