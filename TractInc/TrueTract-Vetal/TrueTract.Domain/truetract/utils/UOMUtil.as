package truetract.utils
{
    import mx.collections.ArrayCollection;
    
    public class UOMUtil
    {
        private const defaultUOMCode:String = "ft.us";

        [Bindable] public var unitList:ArrayCollection;

        private static var _instance:UOMUtil;

        public static function getInstance():UOMUtil {
            if (!_instance)
                _instance = new UOMUtil;
                
            return _instance;
        }
        
        public function UOMUtil() {
            
            if (UOMUtil._instance)
                throw new Error("Only one UOMUtil instance should be instantiated!");

            unitList = new ArrayCollection ();

            unitList.addItem(new UnitOfMeasure(defaultUOMCode, "Feet US", 1));
            unitList.addItem(new UnitOfMeasure("ft.survey", "Feet Survey", 1.000002));
            unitList.addItem(new UnitOfMeasure("chains", "Chains", 66.000132));
            unitList.addItem(new UnitOfMeasure("pole", "Pole", 16.5));
            unitList.addItem(new UnitOfMeasure("rod", "Rod", 16.5));
            unitList.addItem(new UnitOfMeasure("vera.calf", "Vera (California)", 2.750005512));
            unitList.addItem(new UnitOfMeasure("vera.mexico", "Vera (Mexico)", 2.749409449));
            unitList.addItem(new UnitOfMeasure("vera.prtg", "Vera (Portuguese)", 3.608923885));
            unitList.addItem(new UnitOfMeasure("vera.sa", "Vera (South America)", 2.834645669));
            unitList.addItem(new UnitOfMeasure("vera.spnsh", "Vera (Spanish)", 2.742355643));
            unitList.addItem(new UnitOfMeasure("vera.texas", "Vera (Texas)", 2.777783333));
            unitList.addItem(new UnitOfMeasure("link", "Link", 0.66000132));
            unitList.addItem(new UnitOfMeasure("perche", "Perche", 16.5));
        }

        public function get defaultUOM():UnitOfMeasure
        {
            return getByCode(defaultUOMCode);
        }

        public function getByCode(code:String):UnitOfMeasure
        {
            for each (var uom:UnitOfMeasure in unitList){
                if (uom.Code == code) return uom;
            }
            
            return null;
        }
    }
}