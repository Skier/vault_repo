package src.deedplotter.components.tractViewClasses.call.factories
{
    import src.deedplotter.components.tractViewClasses.call.CallView;
    import src.deedplotter.components.tractViewClasses.call.propertyViews.CallPropertiesView;
    import src.deedplotter.components.tractViewClasses.call.propertyViews.RadialInRadiusArcLengthCPView;
    import src.deedplotter.domain.TractCall;
    import src.deedplotter.domain.callparams.ArcLengthParam;
    import src.deedplotter.domain.callparams.ParamCollection;
    import src.deedplotter.utils.GeoBearing;
    import src.deedplotter.utils.GeoCurve;
    import src.deedplotter.utils.UnitOfMeasure;
    import src.deedplotter.domain.callparams.RadialInBearingParam;
    import src.deedplotter.domain.callparams.RadiusParam;
    
    internal class RadialInRadiusArcLengthCurveFactory extends CurveFactory
    {
        private var m_radialIn:GeoBearing;
        
        private var m_radius:Number;
        private var m_radiusUOM:UnitOfMeasure;
        
        private var m_arcLength:Number;
        private var m_arcLengthUOM:UnitOfMeasure;
        
        public function RadialInRadiusArcLengthCurveFactory(nextFactory:ICallViewFactory) {
            super(nextFactory);
        }
        
        override public function GetCallPropertiesView(call:TractCall):CallPropertiesView 
        {
            if (call.CallType != TractCall.CALL_TYPE_CURVE || !parseParams(call.Params)){
                
                return super.GetCallPropertiesView(call);
                
            } else {
                
                var result:RadialInRadiusArcLengthCPView = new RadialInRadiusArcLengthCPView();
                result.Model = call;
                
                result.rInBearingTxt.text = m_radialIn.toInputString();
                
                result.radiusTxt.text = m_radius.toString();
                result.radiusUom = m_radiusUOM;
                
                result.arcLengthTxt.text = m_arcLength.toString();
                result.arcLengthUom = m_arcLengthUOM;
                
                return result;
            }
        }
        
        override protected function createGeoCurve():GeoCurve {
            var radiusFt:Number = m_radius * m_radiusUOM.RateToOneFeet;
            var arcLength:Number = m_arcLength * m_arcLengthUOM.RateToOneFeet;
            
            var delta:Number = 57.29 * arcLength / radiusFt;
            
            return new GeoCurve(radiusFt, delta, GeoBearing.CreateByAzimuth(m_radialIn.Azimuth - 90));
        }
        
        override protected function parseParams(params:ParamCollection):Boolean 
        {
            var radialInParam:RadialInBearingParam = RadialInBearingParam(
                params.GetParamByName(RadialInBearingParam.NAME));
                
            var radiusParam:RadiusParam = RadiusParam(
               params.GetParamByName(RadiusParam.NAME));
                
            var arcLengthParam:ArcLengthParam = ArcLengthParam(
                params.GetParamByName(ArcLengthParam.NAME));
            
            if (radialInParam && radiusParam && arcLengthParam) {
                
                m_arcLength = arcLengthParam.Value;
                m_arcLengthUOM = arcLengthParam.UOM;
                
                m_radius = radiusParam.Value;
                m_radiusUOM = radiusParam.UOM;
                m_radialIn = radialInParam.Value;
                
                return true;
            } else {
                return false;
            }
        }
    }
}