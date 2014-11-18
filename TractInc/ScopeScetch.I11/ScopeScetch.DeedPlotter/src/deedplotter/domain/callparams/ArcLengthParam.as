package src.deedplotter.domain.callparams
{
    import src.deedplotter.utils.UnitOfMeasure;
    import src.deedplotter.utils.UOMUtil;
    
    public class ArcLengthParam extends DistanceParam
    {
        public static const NAME:String = "ARC_LENGTH";
        
        public function ArcLengthParam(value:Number, uom:UnitOfMeasure) {
            super(value, uom);
        }
        
        override public function get DisplayName():String
        {
            return "Arc Ln";
        }
        
        override public function get DBName():String {
            return ArcLengthParam.NAME;
        }
                
        public static function CreateByDBValue(value:String):IParam {
            var params:Array = value.split(uomSplitter);
            var numValue:Number = params[0];
            
            var uom:UnitOfMeasure = UOMUtil.Instance().GetByCode(params[1]);
            
            if (isNaN(numValue) && !uom) {
                throw new Error("Invalid value");
            }
            
            return new ArcLengthParam(numValue, uom);
        }        
    }
}