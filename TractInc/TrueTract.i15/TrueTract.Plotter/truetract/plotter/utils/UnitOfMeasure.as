package truetract.plotter.utils

{
    public class UnitOfMeasure
    {
        private var _code:String;
        private var _feetRate:Number;
        private var _name:String;
        
        public function UnitOfMeasure(code:String, name:String, feetRate:Number) {
            _code = code;
            _name = name;
            _feetRate = feetRate;
        }

        public function get Code():String {
            return _code;
        }
        
        public function get Name():String { 
            return _name;
        }
        
        public function get label():String {
            return Name;
        }
        
        public function get RateToOneFeet():Number { 
            return _feetRate; 
        }
        
    }
}