package src.deedplotter.utils
{
    import mx.collections.ArrayCollection;
    
    public class UOMUtil
    {

        private const defaultUOMCode:String = "ft.us";
        
        private var _unitList:ArrayCollection;

        private static var _instance:UOMUtil;
        
        public static function Instance():UOMUtil {
            if (!_instance)
                _instance = new UOMUtil;
                
            return _instance;
        }
        
        public function UOMUtil() {
            
            if (UOMUtil._instance)
                throw new Error("Only one UOMUtil instance should be instantiated!");

            _unitList = new ArrayCollection ();

            _unitList.addItem(new UnitOfMeasure(defaultUOMCode, "Feet US", 1));
            _unitList.addItem(new UnitOfMeasure("ft.survey", "Feet Survey", 0.999998));
            _unitList.addItem(new UnitOfMeasure("chains", "Chains", 0.015151485));
            _unitList.addItem(new UnitOfMeasure("pole", "Pole", 0.060606061));
            _unitList.addItem(new UnitOfMeasure("rod", "Rod", 0.060606061));
            _unitList.addItem(new UnitOfMeasure("vera.calf", "Vera (California)", 0.363635635));
            _unitList.addItem(new UnitOfMeasure("vera.mexico", "Vera (Mexico)", 0.36371447));
            _unitList.addItem(new UnitOfMeasure("vera.prtg", "Vera (Portuguese)", 0.277090909));
            _unitList.addItem(new UnitOfMeasure("vera.sa", "Vera (South America)", 0.352777778));
            _unitList.addItem(new UnitOfMeasure("vera.spnsh", "Vera (Spanish)", 0.364650005));
            _unitList.addItem(new UnitOfMeasure("vera.texas", "Vera (Texas)", 0.35999928));
            _unitList.addItem(new UnitOfMeasure("link", "Link", 1.515148485));
            _unitList.addItem(new UnitOfMeasure("perche", "Perche", 0.060606061));
        }

        public function get DefaultUOM():UnitOfMeasure {
            return GetByCode(defaultUOMCode);
        }
        
        public function get UnitList():ArrayCollection {
            return _unitList;
        }
        
        public function GetByCode(code:String):UnitOfMeasure {
            for each (var uom:UnitOfMeasure in _unitList){
                if (uom.Code == code) return uom;
            }
            
            return null;
        }
    }
}