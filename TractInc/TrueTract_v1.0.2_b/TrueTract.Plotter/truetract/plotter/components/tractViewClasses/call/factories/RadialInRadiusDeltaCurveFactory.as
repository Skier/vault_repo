package truetract.plotter.components.tractViewClasses.call.factories
{
    import truetract.plotter.domain.*;
    import truetract.plotter.utils.*;
    import truetract.plotter.utils.*;
    import truetract.plotter.domain.callparams.*;
    import truetract.plotter.components.tractViewClasses.call.propertyViews.*;
    
    internal class RadialInRadiusDeltaCurveFactory extends CurveFactory
    {
        private var m_radialIn:GeoBearing;
        private var m_radius:Number;
        private var m_delta:Angle;
        private var m_radiusUom:UnitOfMeasure;
        
        public function RadialInRadiusDeltaCurveFactory(nextFactory:ICallViewFactory) {
            super(nextFactory);
        }
        
        override public function GetCallPropertiesView(call:TractCall):CallPropertiesView
        {
            if (call.CallType != TractCall.CALL_TYPE_CURVE || !parseParams(call.Params)){
                
                return super.GetCallPropertiesView(call);
                
            } else {
                
                var result:RadialInDeltaRadiusCPView = new RadialInDeltaRadiusCPView();
                result.Model = call;
                result.rInBearingTxt.text = m_radialIn.toString();
                result.radiusTxt.text = m_radius.toString();
                result.deltaTxt.text = m_delta.toString();
                result.radiusUom = m_radiusUom;
                return result;
            }
        }
        
        override protected function createGeoCurve():GeoCurve {
            return new GeoCurve(m_radius * m_radiusUom.RateToOneFeet, m_delta.value, GeoBearing.CreateByAzimuth(m_radialIn.Azimuth - 90));
        }
        
        override protected function parseParams(params:ParamCollection):Boolean 
        {
            var radialInParam:RadialInBearingParam = RadialInBearingParam(
                params.GetParamByName(RadialInBearingParam.NAME));
                
            var radiusParam:RadiusParam = RadiusParam(
                params.GetParamByName(RadiusParam.NAME));
                
            var deltaParam:DeltaParam = DeltaParam(
                params.GetParamByName(DeltaParam.NAME));
            
            if (radialInParam && radiusParam && deltaParam) {
            
                m_radialIn = radialInParam.Value;
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