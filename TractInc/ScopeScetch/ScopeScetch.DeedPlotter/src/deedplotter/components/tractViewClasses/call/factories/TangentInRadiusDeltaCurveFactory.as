package src.deedplotter.components.tractViewClasses.call.factories
{
    import src.deedplotter.components.tractViewClasses.call.CallView;
    import src.deedplotter.components.tractViewClasses.call.propertyViews.CallPropertiesView;
    import src.deedplotter.components.tractViewClasses.call.propertyViews.TangentInRadiusDeltaCPView;
    import src.deedplotter.domain.TractCall;
    import src.deedplotter.domain.callparams.DeltaParam;
    import src.deedplotter.domain.callparams.ParamCollection;
    import src.deedplotter.utils.GeoBearing;
    import src.deedplotter.utils.GeoCurve;
    import src.deedplotter.utils.UnitOfMeasure;
    import src.deedplotter.domain.callparams.TangentInBearingParam;
    import src.deedplotter.domain.callparams.RadiusParam;
    
    internal class TangentInRadiusDeltaCurveFactory extends CurveFactory
    {
        private var m_tangentIn:GeoBearing;
        private var m_radius:Number;
        private var m_radiusUom:UnitOfMeasure;
        private var m_delta:Number;

        public function TangentInRadiusDeltaCurveFactory(nextFactory:ICallViewFactory) {
            super(nextFactory);
        }
        
        override public function GetCallPropertiesView(call:TractCall):CallPropertiesView 
        {
            if (call.CallType != TractCall.CALL_TYPE_CURVE || !parseParams(call.Params)){
                
                return super.GetCallPropertiesView(call);
                
            } else {
                
                var result:TangentInRadiusDeltaCPView = new TangentInRadiusDeltaCPView();
                result.Model = call;
                result.tInBearingTxt.text = m_tangentIn.toInputString();
                result.radiusTxt.text = m_radius.toString();
                result.deltaTxt.text = m_delta.toString();
                result.radiusUom = m_radiusUom;
                return result;
            }
        }
        
        override protected function createGeoCurve():GeoCurve {
            return new GeoCurve(m_radius * m_radiusUom.RateToOneFeet, m_delta, m_tangentIn);
        }
        
        override protected function parseParams(params:ParamCollection):Boolean 
        {
            var tangentInParam:TangentInBearingParam = TangentInBearingParam(
                params.GetParamByName(TangentInBearingParam.NAME));
                
            var radiusParam:RadiusParam = RadiusParam(
                params.GetParamByName(RadiusParam.NAME));
                
            var deltaParam:DeltaParam = DeltaParam(
                params.GetParamByName(DeltaParam.NAME));
            
            if (tangentInParam && radiusParam && deltaParam && params.length == 3) {
            
                m_tangentIn = tangentInParam.Value;
                m_radius = radiusParam.Value;
                m_radiusUom = radiusParam.UOM;
                m_delta = deltaParam.Value;
                
                return true;
            } else {
                return false;
            }
        }
    }
}