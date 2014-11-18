package truetract.domain.callparams
{
    public class TangentOutBearingParam extends BearingParam
    {
        public static const NAME:String = "TANGENT_OUT_BEARING";

        public function TangentOutBearingParam(value:*) {
            super (value);
        }
        
        override public function get DisplayName():String {
            return "Tan Out";
        }
        
        override public function get DBName():String {
            return NAME;
        }
        
    }
}