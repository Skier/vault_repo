package truetract.plotter.components.tractViewClasses.call.factories
{
    import truetract.domain.TractCall;
    import truetract.domain.callparams.ParamCollection;
    import truetract.domain.callparams.RadialInBearingParam;
    import truetract.plotter.components.tractViewClasses.call.CallCurveView;
    import truetract.plotter.components.tractViewClasses.call.CallView;
    import truetract.plotter.components.tractViewClasses.call.propertyViews.CallPropertiesView;
    import truetract.plotter.components.tractViewClasses.call.propertyViews.RadialInRadialOutRadiusCPView;
    import truetract.utils.*;
    
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