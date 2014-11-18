package truetract.plotter.components.tractViewClasses.call.factories
{
    import truetract.plotter.domain.*;
    import truetract.plotter.utils.*;
    import truetract.plotter.utils.*;
    import truetract.plotter.domain.callparams.*;
    import truetract.plotter.components.tractViewClasses.call.propertyViews.*;
    
    internal class RadialInRadialOutRadiusCurveFactory extends CurveFactory
    {
        private var m_radialIn:GeoBearing;
        private var m_radialOut:GeoBearing;        
        private var m_radius:Number;
        private var m_radiusUom:UnitOfMeasure;
        
        public function RadialInRadialOutRadiusCurveFactory(nextFactory:ICallViewFactory) {
            super(nextFactory);
        }
        
        override public function GetCallPropertiesView(call:TractCall):CallPropertiesView 
        {
            if (call.CallType != TractCall.CALL_TYPE_CURVE || !parseParams(call.Params)){
                
                return super.GetCallPropertiesView(call);
                
            } else {
                
                var result:RadialInRadialOutRadiusCPView = new RadialInRadialOutRadiusCPView();
                result.Model = call;
                result.rInBearingTxt.text = m_radialIn.toInputString();
                result.rOutBearingTxt.text = m_radialOut.toInputString();
                result.radiusTxt.text = m_radius.toString();
                result.radiusUom = m_radiusUom;
                return result;
            }
        }
        
        override protected function createGeoCurve():GeoCurve {
            var delta:Number = m_radialOut.Azimuth - 180 - m_radialIn.Azimuth;
            var radiusFt:Number = m_radius * m_radiusUom.RateToOneFeet;
            
            return new GeoCurve(radiusFt, delta, GeoBearing.CreateByAzimuth(m_radialIn.Azimuth - 90));
        }
        
        override protected function parseParams(params:ParamCollection):Boolean 
        {
            var radialInParam:RadialInBearingParam = RadialInBearingParam(
                params.GetParamByName(RadialInBearingParam.NAME));

            var radialOutParam:RadialOutBearingParam = RadialOutBearingParam(
                params.GetParamByName(RadialOutBearingParam.NAME));
                
            var radiusParam:RadiusParam = RadiusParam(
                params.GetParamByName(RadiusParam.NAME));
                
            if (radialInParam && radialOutParam && radiusParam) {
            
                m_radialIn = radialInParam.Value;
                m_radialOut = radialOutParam.Value;                
                m_radius = radiusParam.Value;
                m_radiusUom = radiusParam.UOM;
                
                return true;
            } else {
                return false;
            }
        }
    }
}