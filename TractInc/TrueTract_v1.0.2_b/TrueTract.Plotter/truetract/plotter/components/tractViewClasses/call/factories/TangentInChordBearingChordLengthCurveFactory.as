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
    import truetract.plotter.components.tractViewClasses.call.propertyViews.TangentInChordBearingChordLengthCPView;
    import truetract.plotter.domain.callparams.TangentInBearingParam;
    
    internal class TangentInChordBearingChordLengthCurveFactory extends CurveFactory
    {
        private var m_chordBearing:GeoBearing;
        private var m_tangentIn:GeoBearing;
        private var m_chordLength:Number;
        private var m_chordLengthUom:UnitOfMeasure;
        
        public function TangentInChordBearingChordLengthCurveFactory(nextFactory:ICallViewFactory) {
            super(nextFactory);
        }

        override public function GetCallPropertiesView(call:TractCall):CallPropertiesView 
        {
            if (call.CallType != TractCall.CALL_TYPE_CURVE || !parseParams(call.Params)){
                
                return super.GetCallPropertiesView(call);
                
            } else {
                
                var result:TangentInChordBearingChordLengthCPView = new TangentInChordBearingChordLengthCPView();
                result.Model = call;
                
                result.tangentInTxt.text = m_tangentIn.toString();
                
                result.chordBearingTxt.text = m_chordBearing.toString();
                
                result.chordLengthTxt.text = m_chordLength.toString();
                result.chordLengthUom = m_chordLengthUom;
                
                return result;
            }
        }
        
        override protected function createGeoCurve():GeoCurve 
        {
            var chordLengthFt:Number = m_chordLength * m_chordLengthUom.RateToOneFeet;
            
            if (m_tangentIn.Azimuth == m_chordBearing.Azimuth){
                throw new Error("The Chord bearing canot be the same as Tangent In bearing");
            }
            
            var radiusFt:Number = chordLengthFt / (2 * Math.sin( m_chordBearing.Radian - m_tangentIn.Radian ));

            var delta:Number = (2 * Math.asin( (chordLengthFt/2) / radiusFt ) * 180) / Math.PI;
            var tangentIn:GeoBearing = GeoBearing.CreateByAzimuth(m_chordBearing.Azimuth - (delta / 2) );
            
            return new GeoCurve(radiusFt, delta, tangentIn);
        }
        
        override protected function parseParams(params:ParamCollection):Boolean 
        {
            var tangentInParam:TangentInBearingParam = TangentInBearingParam(
                params.GetParamByName(TangentInBearingParam.NAME));
                
            var chordBearingParam:ChordBearingParam = ChordBearingParam(
                params.GetParamByName(ChordBearingParam.NAME));
                
            var chordLengthParam:ChordLengthParam = ChordLengthParam(
                params.GetParamByName(ChordLengthParam.NAME));
            
            if (tangentInParam && chordBearingParam  && chordLengthParam) {
                
                m_chordLength = chordLengthParam.Value;
                m_chordLengthUom = chordLengthParam.UOM;
                m_chordBearing = chordBearingParam.Value;
                m_tangentIn = tangentInParam.Value;
                return true;
            } else {
                return false;
            };
        }
    }
}