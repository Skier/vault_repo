package truetract.plotter.components.tractViewClasses.call.factories
{
    import truetract.plotter.domain.TractCall;
    
    import truetract.plotter.utils.GeoBearing;
    import truetract.plotter.utils.GeoCurve;
    import truetract.plotter.utils.IGeoShape;
    import truetract.plotter.utils.UnitOfMeasure;
    import truetract.plotter.components.tractViewClasses.call.CallView;
    import truetract.plotter.domain.callparams.*;
    import truetract.plotter.components.tractViewClasses.call.propertyViews.CallPropertiesView;
    import truetract.plotter.components.tractViewClasses.call.propertyViews.CurveWizard;
    
    internal class CurveWizardFactory extends CurveFactory
    {
        private var m_direction:String;
        private var m_tangentIn:GeoBearing;
        private var m_radius:Number;
        private var m_radiusUom:UnitOfMeasure;
        private var m_delta:Number;
        private var m_chordLength:Number;
        private var m_chordUom:UnitOfMeasure;
        private var m_arcLength:Number;
        private var m_arcUom:UnitOfMeasure;
        
        public function CurveWizardFactory(nextFactory:ICallViewFactory) {
            super(nextFactory);
        }
        
        override public function GetCallPropertiesView(call:TractCall):CallPropertiesView 
        {
            if (call.CallType != TractCall.CALL_TYPE_CURVE || !parseParams(call.Params)){
                
                return super.GetCallPropertiesView(call);
                
            } else {
                
                var result:CurveWizard = new CurveWizard();
                result.Model = call;
                result.tangentInTxt.text = m_tangentIn.toInputString();
                result.sideList.selectedItem = (m_direction == GeoCurve.RIGHT) ? result.clockwise : result.counterclockwise;
                result.radiusTxt.text = isNaN(m_radius) ? "" : m_radius.toString();
                result.radiusUom = m_radiusUom;
                result.arcLengthTxt.text = isNaN(m_arcLength) ? "" : m_arcLength.toString();
                result.arcLengthUom = m_arcUom;
                result.chordLengthTxt.text = isNaN(m_chordLength) ? "" : m_chordLength.toString();
                result.chordLengthUom = m_chordUom;
                result.deltaTxt.text = isNaN(m_delta) ? "" : m_delta.toString();
                
                return result;
            }
        }
        
        override protected function createGeoCurve():GeoCurve {
            var radius:Number;
            var delta:Number;
            
            if (m_delta && m_radius)
            {
                radius = m_radius * m_radiusUom.RateToOneFeet;
                delta = m_delta;
            }
            else if (m_radius) 
            {
                radius = m_radius * m_radiusUom.RateToOneFeet;
                delta = 57.29 * m_arcLength * m_arcUom.RateToOneFeet / radius;
            } 
            else 
            {
                delta = m_delta;
                
                if ( m_arcLength ) {
                    radius = (360 * m_arcLength * m_arcUom.RateToOneFeet) / (2 * Math.PI * delta);
                } else {
                    radius = m_chordLength * m_chordUom.RateToOneFeet / (2 * Math.sin(delta/2));
                }
            }
            
            return new GeoCurve(radius, delta, m_tangentIn, null, m_direction);
        }
        
        override protected function parseParams(params:ParamCollection):Boolean 
        {
            reset();
            
            var tangentInParam:TangentInBearingParam = TangentInBearingParam(
                params.GetParamByName(TangentInBearingParam.NAME));

            var radiusParam:RadiusParam = RadiusParam(
                params.GetParamByName(RadiusParam.NAME));

            var deltaParam:DeltaParam = DeltaParam(
                params.GetParamByName(DeltaParam.NAME));

            var chordLengthParam:ChordLengthParam = ChordLengthParam(
                params.GetParamByName(ChordLengthParam.NAME));

            var arcLengthParam:ArcLengthParam = ArcLengthParam(
                params.GetParamByName(ArcLengthParam.NAME));

            var directionParam:DirectionParam = DirectionParam(
                params.GetParamByName(DirectionParam.NAME));

            if (!directionParam || !tangentInParam) return false; //this params are required
            
            if (radiusParam && deltaParam) 
            {
                m_radius = radiusParam.Value;
                m_radiusUom = radiusParam.UOM;
                m_delta = deltaParam.Value;
            } 
            else if (radiusParam) 
            {
                if (!arcLengthParam) {
                    return false;
                } else {
                    m_radius = radiusParam.Value;
                    m_radiusUom = radiusParam.UOM;
                    m_arcLength = arcLengthParam.Value;
                    m_arcUom = arcLengthParam.UOM;
                }
            }
            else if (deltaParam) 
            {
                if (arcLengthParam) {
                    m_arcLength = arcLengthParam.Value;
                    m_arcUom = arcLengthParam.UOM;
                } else if (chordLengthParam){
                    m_chordLength = chordLengthParam.Value;
                    m_chordUom = chordLengthParam.UOM;
                } else {
                    return false;
                }
                
                m_delta = deltaParam.Value;
            } 
            else 
            {
                //we have non radius non delta
                return false;
            }
            
            m_direction = directionParam.Value;
            m_tangentIn = tangentInParam.Value;
            
            return true;
        }
        
        private function reset():void {
            m_tangentIn = null;
            m_direction = null;
            m_radius = NaN;
            m_radiusUom = null;
            m_delta = NaN;
            m_chordLength = NaN;
            m_chordUom = null;
            m_arcLength = NaN;
            m_arcUom = null;
        }
    }
}