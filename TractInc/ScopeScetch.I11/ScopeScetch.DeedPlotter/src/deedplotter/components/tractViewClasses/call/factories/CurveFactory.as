package src.deedplotter.components.tractViewClasses.call.factories
{
    import src.deedplotter.components.tractViewClasses.call.CallCurveView;
    import src.deedplotter.components.tractViewClasses.call.CallView;
    import src.deedplotter.components.tractViewClasses.call.propertyViews.CallPropertiesView;
    import src.deedplotter.components.tractViewClasses.call.propertyViews.RadialInRadialOutRadiusCPView;
    import src.deedplotter.domain.TractCall;
    import src.deedplotter.domain.callparams.ParamCollection;
    import src.deedplotter.domain.callparams.RadialInBearingParam;
    import src.deedplotter.utils.GeoBearing;
    import src.deedplotter.utils.GeoCurve;
    import src.deedplotter.utils.IGeoShape;
    
    internal class CurveFactory extends CallViewFactory
    {
        
        public function CurveFactory(nextFactory:ICallViewFactory) {
            super(nextFactory);
        }
        
        override public function GetCallView(call:TractCall):CallView
        {
            if (call.CallType.toUpperCase() == TractCall.CALL_TYPE_CURVE && parseParams(call.Params)) {
                var result:CallView = new CallCurveView(createGeoCurve());
                result.Model = call;
                return result;        
            } else {
                return super.GetCallView(call);
            }
        }

        override public function GetCallGeoShape(call:TractCall):IGeoShape 
        {
            if (call.CallType.toUpperCase() == TractCall.CALL_TYPE_CURVE && parseParams(call.Params)) {
                return createGeoCurve();
            } else {
                return super.GetCallGeoShape(call);
            }
        }
        
        protected virtual function createGeoCurve():GeoCurve 
        {
            throw new Error("This method must be overriden");
        }
        
        protected virtual function parseParams(params:ParamCollection):Boolean 
        {
            throw new Error("This method must be overriden");
        }
    }
}