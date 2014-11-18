package truetract.plotter.domain.callparams
{
    import truetract.plotter.utils.GeoBearing;
    import mx.controls.Alert;
    
    public class BearingParam implements IParam
    {
        public static const NAME:String = "BEARING";
        
        private var m_value:GeoBearing;
        
        public function BearingParam(value:*) {
            
            if (value is String) {
//                m_value = GeoBearing.CreateByAzimuth(Number(value));
				try {
	                m_value = GeoBearing.Parse(value);
				} catch (e:Error){
					Alert.show("Error loading Geo Bearing parameter: " + e.message);
					m_value = new GeoBearing();
				}
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
//            return m_value.Azimuth.toString();
			return m_value.toDbString();
        }
        
    }
}