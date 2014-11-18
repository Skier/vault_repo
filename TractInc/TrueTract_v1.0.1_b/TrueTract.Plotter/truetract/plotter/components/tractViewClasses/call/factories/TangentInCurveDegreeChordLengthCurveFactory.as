package truetract.plotter.components.tractViewClasses.call.factories
{
    import truetract.plotter.components.tractViewClasses.call.CallView;
    import truetract.plotter.components.tractViewClasses.call.propertyViews.CallPropertiesView;
    import truetract.plotter.components.tractViewClasses.call.propertyViews.TangentInCurveDegreeChordLengthCPView;
    import truetract.plotter.domain.TractCall;
    import truetract.plotter.domain.callparams.ChordLengthParam;
    import truetract.plotter.domain.callparams.CurveDegreeParam;
    import truetract.plotter.domain.callparams.ParamCollection;
    import truetract.plotter.utils.GeoBearing;
    import truetract.plotter.utils.GeoCurve;
    import truetract.plotter.utils.UnitOfMeasure;
    import truetract.plotter.domain.callparams.TangentInBearingParam;
    
    internal class TangentInCurveDegreeChordLengthCurveFactory extends CurveFactory
    {
        private var m_tangentIn:GeoBearing;
        private var m_curveDegree:Number;
        private var m_chordLength:Number;
        private var m_chordLengthUom:UnitOfMeasure;
        
        public function TangentInCurveDegreeChordLengthCurveFactory(nextFactory:ICallViewFactory) {
            super(nextFactory);
        }
        
        override public function GetCallPropertiesView(call:TractCall):CallPropertiesView 
        {
            if (call.CallType != TractCall.CALL_TYPE_CURVE || !parseParams(call.Params)){
                
                return super.GetCallPropertiesView(call);
                
            } else {
                
                var result:TangentInCurveDegreeChordLengthCPView = new TangentInCurveDegreeChordLengthCPView();
                result.Model = call;
                result.tInBearingTxt.text = m_tangentIn.toInputString();
                result.curveDegreeTxt.text = m_curveDegree.toString();
                result.chordLengthTxt.text = m_chordLength.toString();
                result.chordLengthUom = m_chordLengthUom;
                return result;
            }
        }
        
        override protected function createGeoCurve():GeoCurve {
            var radius:Number = 5729.58 / m_curveDegree;
            var chordLengthFt:Number = m_chordLength * m_chordLengthUom.RateToOneFeet;
                
            if (chordLengthFt > (radius * 2)){
                throw new Error("The Chord length is bigger than double Radius");
            }
            
            var delta:Number = (2 * Math.asin( (chordLengthFt/2) / radius ) * 180) / Math.PI;
            
            return new GeoCurve(radius, delta, m_tangentIn);
        }
        
        override protected function parseParams(params:ParamCollection):Boolean 
        {
            var tangentInParam:TangentInBearingParam = TangentInBearingParam(
                params.GetParamByName(TangentInBearingParam.NAME));
                
            var curveDegreeParam:CurveDegreeParam = CurveDegreeParam(
                params.GetParamByName(CurveDegreeParam.NAME));
                
            var chordLengthParam:ChordLengthParam = ChordLengthParam(
                params.GetParamByName(ChordLengthParam.NAME));
            
            if (tangentInParam && curveDegreeParam && chordLengthParam) {
                
                m_chordLength = chordLengthParam.Value;
                m_chordLengthUom = chordLengthParam.UOM;
                m_curveDegree = curveDegreeParam.Value;
                m_tangentIn = tangentInParam.Value;
                
                return true;
            } else {
                return false;
            }
        }
    }
}