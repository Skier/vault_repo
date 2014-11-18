package truetract.plotter.domain
{
	import truetract.plotter.domain.callparams.ParamCollection;
	
    [Bindable]
	[RemoteClass(alias="TractInc.TrueTract.Entity.TractCallInfo")]
    public class TractCall
    {
        
        public static const CALL_TYPE_LINE:String = "LINE";
        public static const CALL_TYPE_CURVE:String = "CURVE";

        public var TractCallId:int;
        public var TractId:int;
        public var CallType:String;
        public var CallOrder:Number;
        public var CreatedByMouse:int;

        private var params:ParamCollection = new ParamCollection();
        
        public function get CallDBValue():String {
            return (params) ? params.GetDBString() : "";
        }
        
        public function set CallDBValue(value:String):void {
            params = ParamCollection.CreateByDBString(value);
        }
        
        public function get Params():ParamCollection {
            return params;
        }

        public function get ParamsDisplayValue():String {
            return (params) ? params.GetDisplayString() : "";
        }
        
        public function get IsApproximate():String {
            return CreatedByMouse ? "yes" : "no";
        }
            
        public function get AnnotationId():String {
            if (CallType == CALL_TYPE_LINE) {
                return "L" + CallOrder;
            } else {
                return "C" + CallOrder;
            }
        }        
    }
}