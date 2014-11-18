package truetract.plotter.domain.callparams
{
    import truetract.plotter.utils.UOMUtil;
    import truetract.plotter.utils.UnitOfMeasure;
    
    public class RadiusParam extends DistanceParam
    {
        public static const NAME:String = "RADIUS";
        
        private var m_value:Number;
        
        public function RadiusParam(value:Number, uom:UnitOfMeasure) {
            super(value, uom);
        }
        
        override public function get DisplayName():String
        {
            return "R";
        }
        
        override public function get DBName():String {
            return RadiusParam.NAME;
        }
                
        public static function CreateByDBValue(value:String):IParam {
            var params:Array = value.split(uomSplitter);
            var numValue:Number = params[0];
            
            var uom:UnitOfMeasure = UOMUtil.Instance().GetByCode(params[1]);
            
            if (isNaN(numValue) && !uom) {
                throw new Error("Invalid value");
            }
            
            return new RadiusParam(numValue, uom);
        }        

        
    }
}