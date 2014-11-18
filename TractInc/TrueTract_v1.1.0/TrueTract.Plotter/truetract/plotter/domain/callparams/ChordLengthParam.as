package truetract.plotter.domain.callparams
{
    import truetract.plotter.utils.UOMUtil;
    import truetract.plotter.utils.UnitOfMeasure;
    
    public class ChordLengthParam extends DistanceParam
    {
        public static const NAME:String = "CHORD_LENGTH";
        
        public function ChordLengthParam(value:Number, uom:UnitOfMeasure) {
            super(value, uom);
        }
        
        override public function get DisplayName():String
        {
            return "Ch Ln";
        }
        
        override public function get DBName():String {
            return ChordLengthParam.NAME;
        }
        
        public static function CreateByDBValue(value:String):IParam {
            var params:Array = value.split(uomSplitter);
            var numValue:Number = params[0];
            
            var uom:UnitOfMeasure = UOMUtil.getInstance().getByCode(params[1]);
            
            if (isNaN(numValue) && !uom) {
                throw new Error("Invalid value");
            }
            
            return new ChordLengthParam(numValue, uom);
        }                
    }
}