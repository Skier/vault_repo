package truetract.plotter.components.tractViewClasses.call.factories
{
    import truetract.plotter.components.tractViewClasses.call.CallView;
    import truetract.plotter.domain.TractCall;
    import truetract.plotter.domain.callparams.ChordBearingParam;
    import truetract.plotter.domain.callparams.ChordLengthParam;
    import truetract.plotter.utils.GeoBearing;
    import truetract.plotter.utils.GeoCurve;
    import truetract.plotter.utils.UnitOfMeasure;
    import truetract.plotter.components.tractViewClasses.call.propertyViews.CallPropertiesView;
    import truetract.plotter.components.tractViewClasses.call.propertyViews.RadiusChordBearingChordLengthCPView;
    import truetract.plotter.domain.callparams.ParamCollection;
    import truetract.plotter.domain.callparams.RadiusParam;
    
    internal class RadiusChordBearingChordLengthCurveFactory extends CurveFactory
    {
        private var m_chordBearing:GeoBearing;
        private var m_radius:Number;
        private var m_radiusUom:UnitOfMeasure;
        private var m_chordLength:Number;
        private var m_chordLengthUom:UnitOfMeasure;
        
        public function RadiusChordBearingChordLengthCurveFactory(nextFactory:ICallViewFactory) {
            super(nextFactory);
        }

        override public function GetCallPropertiesView(call:TractCall):CallPropertiesView 
        {
            if (call.CallType != TractCall.CALL_TYPE_CURVE || !parseParams(call.Params)){
                
                return super.GetCallPropertiesView(call);
                
            } else {
                
                var result:RadiusChordBearingChordLengthCPView = new RadiusChordBearingChordLengthCPView();
                result.Model = call;
                
                result.radiusTxt.text = m_radius.toString();
                result.radiusUom = m_radiusUom;
                
                result.chordBearingTxt.text = m_chordBearing.toInputString();
                
                result.chordLengthTxt.text = m_chordLength.toString();
                result.chordLengthUom = m_chordLengthUom;
                
                return result;
            }
        }
        
        override protected function createGeoCurve():GeoCurve {
            var radiusFt:Number = m_radius * m_radiusUom.RateToOneFeet;
            var chordLengthFt:Number = m_chordLength * m_chordLengthUom.RateToOneFeet;

            if (chordLengthFt > (radiusFt * 2)){
                throw new Error("The Chord length is bigger than double Radius");
            }
            
            var delta:Number = (2 * Math.asin( (chordLengthFt/2) / radiusFt ) * 180) / Math.PI;
            var tangentIn:GeoBearing = GeoBearing.CreateByAzimuth(m_chordBearing.Azimuth - (delta / 2) );
            
            return new GeoCurve(radiusFt, delta, tangentIn);
        }
        
        override protected function parseParams(params:ParamCollection):Boolean 
        {
            var chordBearingParam:ChordBearingParam = ChordBearingParam(
                params.GetParamByName(ChordBearingParam.NAME));
                
            var radiusParam:RadiusParam = RadiusParam(
                params.GetParamByName(RadiusParam.NAME));
                
            var chordLengthParam:ChordLengthParam = ChordLengthParam(
                params.GetParamByName(ChordLengthParam.NAME));
            
            if (chordBearingParam && radiusParam && chordLengthParam) {
                
                m_chordLength = chordLengthParam.Value;
                m_chordLengthUom = chordLengthParam.UOM;
                m_chordBearing = chordBearingParam.Value;
                m_radius = radiusParam.Value;
                m_radiusUom = radiusParam.UOM;
                return true;
            } else {
                return false;
            };
        }
    }
}