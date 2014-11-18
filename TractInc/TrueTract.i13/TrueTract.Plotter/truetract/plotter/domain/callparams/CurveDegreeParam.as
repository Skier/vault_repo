package truetract.plotter.domain.callparams
{
	import truetract.plotter.utils.Angle;
	
    public class CurveDegreeParam implements IParam
    {
        public static const NAME:String = "CURVE_DEGREE";
        
        private var m_value:Angle;
        
        public function CurveDegreeParam(value:*) {
        	if (value is Angle) {
        		m_value = value;
        	} else if (value is String) {
        		m_value = Angle.Parse(value);
        	} else {
        		m_value = new Angle(value);
        	}
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
            return m_value.toDbString();
        }
        
    }
}