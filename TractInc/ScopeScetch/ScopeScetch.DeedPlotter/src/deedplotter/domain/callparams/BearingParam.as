package src.deedplotter.domain.callparams
{
    import src.deedplotter.utils.GeoBearing;
    
    public class BearingParam implements IParam
    {
        public static const NAME:String = "BEARING";
        
        private var m_value:GeoBearing;
        
        public function BearingParam(value:*) {
            
            if (value is String) {
                m_value = GeoBearing.CreateByAzimuth(Number(value));
            } else {
                m_value = value;
            }
        }

        public function get Value():*
        {
            return m_value;
        }

        public function get DisplayName():String
        {
            return "B";
        }

        public function get DisplayValue():String
        {
            return m_value.toString();
        }

        public function get DBName():String
        {
            return NAME;
        }

        public function get DBValue():String
        {
            return m_value.Azimuth.toString();
        }
        
    }
}