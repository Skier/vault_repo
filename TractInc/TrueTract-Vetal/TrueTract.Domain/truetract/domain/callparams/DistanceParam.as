package truetract.domain.callparams
{
    import truetract.utils.*;

    public class DistanceParam implements IParam
    {
        public static const NAME:String = "DISTANCE";

        protected static const uomSplitter:String = '|';
        
        private var m_value:Number;
        private var m_uom:UnitOfMeasure;

        public function DistanceParam(value:Number, uom:UnitOfMeasure) {
            m_value = value;
            m_uom = uom;
        }
        
        public function get Value():*
        {
            return m_value;
        }
        
        public function get DisplayValue():String
        {
            return m_value.toFixed(2) + " (" + m_uom.Name + ")";
        }
        
        public function get DisplayName():String
        {
            return "D";
        }
        
        public function get DBName():String {
            return NAME;
        }
        
        public function get DBValue():String
        {
            return m_value.toString() + uomSplitter + m_uom.Code;
        }
        
        public function get UOM():UnitOfMeasure {
            return m_uom;
        }
        
        public static function CreateByDBValue(value:String):IParam {
            var params:Array = value.split(uomSplitter);
            var numValue:Number = params[0];
            var uom:UnitOfMeasure = UOMUtil.getInstance().getByCode(params[1]);
            
            if (isNaN(numValue) && !uom) {
                throw new Error("Invalid value");
            }
            
            return new DistanceParam(numValue, uom);
        }
        
    }
}