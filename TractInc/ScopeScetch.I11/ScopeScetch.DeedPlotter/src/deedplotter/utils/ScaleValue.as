package src.deedplotter.utils
{
    import mx.formatters.NumberBaseRoundType;
    import mx.formatters.NumberFormatter;
    
    [Bindable]
    public class ScaleValue
    {

        public static var Default:ScaleValue = getDefaultValue();
        
        private static function getDefaultValue():ScaleValue
        {
            var result:ScaleValue = new ScaleValue();
            result.inchValue = 1;
            result.uomValue = 1;
            result.uom = UOMUtil.Instance().DefaultUOM;
            
            return result;
        }
        
        public var inchValue:Number = 1;
        public var uomValue:Number;
        public var uom:UnitOfMeasure;
        
        private var nf:NumberFormatter;
        
        public function ScaleValue()
        {
            nf = new NumberFormatter();
            nf.useThousandsSeparator = true;
            nf.rounding = NumberBaseRoundType.NEAREST;
            nf.precision = 2;
        }
        
        public function toString():String
        {
            return nf.format(inchValue) + " inch = " + nf.format(uomValue) + " " + uom.Name;
        }
        
        public function get FeetsInOneInch():Number
        {
            return 1 * (uom.RateToOneFeet * uomValue) / inchValue;
        }

        public function get PointsInOneFeet():Number
        {
            return 72 * inchValue / (uomValue * uom.RateToOneFeet);
        }
        
        public function set PointsInOneFeet(value:Number):void
        {
            // 1 feet == ( value / 72 ) inches == (1 * uom.rateToOneFeet) uom values
            // and
            // 1 inch == (72 / value) feets == ((72 / value) * uom.rateToOneFeet) uom values
            
            if ( value / 72 < 1 )
            {
                inchValue = 1;

                var feetValue:Number = (72 / value);

                uomValue = feetValue / uom.RateToOneFeet;
            } 
            else
            {
                uomValue = 1 / uom.RateToOneFeet;
                inchValue = value / 72;
            }
        }
        
        public function clone():ScaleValue
        {
            var result:ScaleValue = new ScaleValue();
            result.inchValue = inchValue;
            result.uom = uom;
            result.uomValue = uomValue;
            
            return result;
        }
    }
}