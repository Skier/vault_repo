package truetract.plotter.domain.callparams
{
    public class CurveDegreeParam implements IParam
    {
        public static const NAME:String = "CURVE_DEGREE";
        
        private var m_value:Number;
        
        public function CurveDegreeParam(value:*) {
            m_value = Number (value);
        }
        
        public function get Value():*
        {
            return m_value;
        }
        
        public function get DisplayValue():String
        {
            return m_value.toString();
        }
        
        public function get DisplayName():String
        {
            return "Crv Dgr";
        }
        
        public function get DBName():String {
            return NAME;
        }
        
        public function get DBValue():String
        {
            return DisplayValue;
        }
        
    }
}