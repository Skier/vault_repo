package src.deedplotter.domain.callparams
{
    public class RadialInBearingParam extends BearingParam
    {
        public static const NAME:String = "RADIAL_IN_BEARING";
        
        public function RadialInBearingParam(value:*) {
            super (value);
        }
        
        override public function get DisplayName():String {
            return "Rad In";
        }

        override public function get DBName():String {
            return NAME;
        }
        
    }
}