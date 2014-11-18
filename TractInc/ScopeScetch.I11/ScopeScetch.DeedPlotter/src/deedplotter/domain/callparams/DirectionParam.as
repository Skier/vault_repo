package src.deedplotter.domain.callparams
{
    import src.deedplotter.utils.UnitOfMeasure;
    import src.deedplotter.utils.UOMUtil;
    import src.deedplotter.utils.GeoCurve;
    
    public class DirectionParam implements IParam
    {
        public static const NAME:String = "DIRECTION";

        private var m_value:String;

        public function DirectionParam(value:String) {
            if (value.toLowerCase() != GeoCurve.LEFT && value.toLowerCase() != GeoCurve.RIGHT) {
                throw new Error("Invalid direction");
            }
            
            m_value = value.toLowerCase();
        }
        
        public function get Value():*
        {
            return m_value;
        }
        
        public function get DisplayValue():String
        {
            return m_value == GeoCurve.LEFT ? "L" : "R";
        }
        
        public function get DisplayName():String
        {
            return "Dir";
        }
        
        public function get DBName():String {
            return NAME;
        }
        
        public function get DBValue():String
        {
            return m_value;
        }
        
    }
}