package src.deedplotter.domain.callparams
{
    import mx.collections.ArrayCollection;
    
    public class ParamCollection extends ArrayCollection
    {

        private static const PARAMETERS_DB_SPLITTER:String = ";";
        private static const PARAMETERS_DISPLAY_SPLITTER:String = "; ";
                
        private static const NAME_DB_SPLITTER:String = ":";
        private static const NAME_DISPLAY_SPLITTER:String = ": ";

        public function ParamCollection(source:Array=null):void {
            super(source);
        }
        
        public static function CreateByDBString(value:String):ParamCollection {
            var tokens:Array = value.split(';');
            
            var result:ParamCollection = new ParamCollection();
            
            for each (var token:String in tokens){
                var paramName:String = token.substring( 0, token.indexOf(NAME_DB_SPLITTER) );
                var paramDBValue:String = token.substring( token.indexOf(NAME_DB_SPLITTER) + 1 );
                
                result.addItem( createParamByDBValue(paramName, paramDBValue) );
            }
            
            return result;
        }
        
        public function GetDBString():String {
            var result:String = "";
            
            for each (var param:IParam in source){
                if (result.length > 0) result += PARAMETERS_DB_SPLITTER;
                
                result += param.DBName + NAME_DB_SPLITTER + param.DBValue;
            }
            
            return result;
        }
        
        public function GetDisplayString():String {
            var result:String = "";
            
            for each (var param:IParam in source){
                if (result.length > 0) result += PARAMETERS_DISPLAY_SPLITTER;
                
                result += param.DisplayName + NAME_DISPLAY_SPLITTER + param.DisplayValue;
            }
            
            return result;
        }
        
        public function GetParamByName(paramName:String):IParam 
        {
            for each (var param:IParam in source){
                if (param.DBName == paramName){
                    return param;
                }
            }
            
            return null;
        }
        
        private static function createParamByDBValue(paramName:String, paramDBValue:String):IParam {
            var result:IParam;
            
            switch (paramName) {
                case BearingParam.NAME:
                    result = new BearingParam(paramDBValue);
                    break;
                
                case TangentInBearingParam.NAME:
                    result = new TangentInBearingParam(paramDBValue);
                    break;
                    
                case TangentOutBearingParam.NAME:
                    result = new TangentOutBearingParam(paramDBValue);
                    break;
                    
                case RadialInBearingParam.NAME:
                    result = new RadialInBearingParam(paramDBValue);
                    break;
                    
                case RadialOutBearingParam.NAME:
                    result = new RadialOutBearingParam(paramDBValue);
                    break;

                case RadiusParam.NAME:
                    result = RadiusParam.CreateByDBValue(paramDBValue);
                    break;

                case DeltaParam.NAME:
                    result = new DeltaParam(paramDBValue);
                    break;

                case ChordBearingParam.NAME:
                    result = new ChordBearingParam(paramDBValue);
                    break;
                    
                case ChordLengthParam.NAME:
                    result = ChordLengthParam.CreateByDBValue(paramDBValue);
                    break;
                    
                case ArcLengthParam.NAME:
                    result = ArcLengthParam.CreateByDBValue(paramDBValue);
                    break;
                    
                case CurveDegreeParam.NAME:
                    result = new CurveDegreeParam(paramDBValue);
                    break;
                    
                case DistanceParam.NAME:
                    result = DistanceParam.CreateByDBValue(paramDBValue);
                    break;
                    
                case DirectionParam.NAME:
                    result = new DirectionParam(paramDBValue);
                    break;
                    
                default:
                    throw new Error("Unknown parameter Name (" + paramName + ")");
            }
            
            return result;
        }        
    }
}