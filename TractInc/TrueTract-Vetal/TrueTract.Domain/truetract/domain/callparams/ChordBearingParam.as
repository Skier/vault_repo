package truetract.domain.callparams
{
    public class ChordBearingParam extends BearingParam
    {
        public static const NAME:String = "CHORD_BEARING";
        
        public function ChordBearingParam(value:*) {
            super (value);
        }
        
        override public function get DisplayName():String {
            return "Ch B";
        }

        override public function get DBName():String {
            return NAME;
        }
        
    }
}