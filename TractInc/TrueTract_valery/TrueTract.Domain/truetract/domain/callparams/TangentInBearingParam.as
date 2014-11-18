package truetract.domain.callparams
{
    import truetract.utils.GeoBearing;
    
    public class TangentInBearingParam extends BearingParam
    {
        public static const NAME:String = "TANGENT_IN_BEARING";
        
        public function TangentInBearingParam(value:*) {
            super (value);
        }
        
        override public function get DisplayName():String {
            return "Tan In";
        }

        override public function get DBName():String {
            return NAME;
        }
        
    }
}