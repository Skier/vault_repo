package truetract.domain.callparams
{
    public class RadialOutBearingParam extends BearingParam
    {
        public static const NAME:String = "RADIAL_OUT_BEARING";
        
        public function RadialOutBearingParam(value:*) {
            super (value);
        }
        
        override public function get DisplayName():String {
            return "Rad Out";
        }
        
        override public function get DBName():String {
            return NAME;
        }
        
    }
}