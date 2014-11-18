package truetract.plotter.domain.callparams
{
    public class DeltaParam implements IParam
    {
        public static const NAME:String = "DELTA";
        
        private var m_value:Number;
        
        public function DeltaParam(value:*) {
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
            return "Delta";
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