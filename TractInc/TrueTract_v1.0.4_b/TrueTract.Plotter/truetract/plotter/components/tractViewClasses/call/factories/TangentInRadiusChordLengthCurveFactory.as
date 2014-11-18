package truetract.plotter.components.tractViewClasses.call.factories
{
    import truetract.plotter.components.tractViewClasses.call.CallView;
    import truetract.plotter.components.tractViewClasses.call.propertyViews.CallPropertiesView;
    import truetract.plotter.components.tractViewClasses.call.propertyViews.TangentInRadiusChordLengthCPView;
    import truetract.plotter.domain.TractCall;
    import truetract.plotter.domain.callparams.ChordLengthParam;
    import truetract.plotter.domain.callparams.ParamCollection;
    import truetract.plotter.utils.GeoBearing;
    import truetract.plotter.utils.GeoCurve;
    import truetract.plotter.utils.UnitOfMeasure;
    import truetract.plotter.domain.callparams.TangentInBearingParam;
    import truetract.plotter.domain.callparams.RadiusParam;
    
    internal class TangentInRadiusChordLengthCurveFactory extends CurveFactory
    {
        private var m_tangentIn:GeoBearing;
        private var m_radius:Number;
        private var m_radiusUom:UnitOfMeasure;
        private var m_chordLength:Number;
        private var m_chordLengthUom:UnitOfMeasure;
        
        public function TangentInRadiusChordLengthCurveFactory(nextFactory:ICallViewFactory) {
            super(nextFactory);
        }
        
        override public function GetCallPropertiesView(call:TractCall):CallPropertiesView 
        {
            if (call.CallType != TractCall.CALL_TYPE_CURVE || !parseParams(call.Params)){
                
                return super.GetCallPropertiesView(call);
                
            } else {
                
                var result:TangentInRadiusChordLengthCPView = new TangentInRadiusChordLengthCPView();
                result.Model = call;
                result.tInBearingTxt.text = m_tangentIn.toString();
                result.radiusTxt.text = m_radius.toString();
                result.chordLengthTxt.text = m_chordLength.toString();
                result.radiusUom = m_radiusUom;
                result.chordLengthUom = m_chordLengthUom;
                return result;
            }
        }
        
        override protected function createGeoCurve():GeoCurve {
            var chordLengthFt:Number = m_chordLength * m_chordLengthUom.RateToOneFeet;
            var radiusFt:Number = m_radius * m_radiusUom.RateToOneFeet;

            if (chordLengthFt > (radiusFt * 2)){
                throw new Error("The Chord length is bigger than double Radius");
            }
            
            var delta:Number = (2 * Math.asin( (chordLengthFt/2) / radiusFt ) * 180) / Math.PI;
            
            return new GeoCurve(radiusFt, delta, m_tangentIn);
        }
        
        override protected function parseParams(params:ParamCollection):Boolean 
        {
            var tangentInParam:TangentInBearingParam = TangentInBearingParam(
                params.GetParamByName(TangentInBearingParam.NAME));
                
            var radiusParam:RadiusParam = RadiusParam(
                params.GetParamByName(RadiusParam.NAME));
                
            var chordLengthParam:ChordLengthParam = ChordLengthParam(
                params.GetParamByName(ChordLengthParam.NAME));
            
            if (tangentInParam && radiusParam && chordLengthParam) {
                
                m_chordLength = chordLengthParam.Value;
                m_chordLengthUom = chordLengthParam.UOM;

                m_radius = radiusParam.Value;
                m_radiusUom = radiusParam.UOM;

                m_tangentIn = tangentInParam.Value;
                
                return true;
            } else {
                return false;
            }
        }
    }
}