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
    import truetract.plotter.utils.Angle;
    
    internal class CurveWizardFactory extends CurveFactory
    {
        private var m_direction:String;
        private var m_tangentIn:GeoBearing;
        private var m_radius:Number;
        private var m_radiusUom:UnitOfMeasure;
        private var m_delta:Angle;
        private var m_chordBearing:GeoBearing;
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
                result.tangentInTxt.text = m_tangentIn.toString();
                result.sideList.selectedItem = (m_direction == GeoCurve.RIGHT) ? result.clockwise : result.counterclockwise;
                result.radiusTxt.text = isNaN(m_radius) ? "" : m_radius.toString();
                result.radiusUom = m_radiusUom;
                result.arcLengthTxt.text = isNaN(m_arcLength) ? "" : m_arcLength.toString();
                result.arcLengthUom = m_arcUom;
                result.chordBearingTxt.text = m_chordBearing == null ? "" : m_chordBearing.toString();
                result.chordLengthTxt.text = isNaN(m_chordLength) ? "" : m_chordLength.toString();
                result.chordLengthUom = m_chordUom;
                result.deltaTxt.text = m_delta == null ? "" : m_delta.toString();
                
                return result;
            }
        }
        
        override protected function createGeoCurve():GeoCurve {
            var radius:Number;
            var delta:Number;
            
            if (m_delta && m_radius)
            {
                radius = m_radius * m_radiusUom.RateToOneFeet;
                delta = m_delta.value;
            }
            else if (m_radius) 
            {
                radius = m_radius * m_radiusUom.RateToOneFeet;
                delta = 57.29 * m_arcLength * m_arcUom.RateToOneFeet / radius;
            } 
            else if (m_delta)
            {
                delta = m_delta.value;
                
                if ( m_arcLength ) {
                    radius = (360 * m_arcLength * m_arcUom.RateToOneFeet) / (2 * Math.PI * delta);
                } else {
                    radius = m_chordLength * m_chordUom.RateToOneFeet / (2 * Math.sin(delta/2));
                }
            }
            else 
            {
            	radius = m_chordLength * m_chordUom.RateToOneFeet / (2 * Math.sin( m_chordBearing.Radian - m_tangentIn.Radian ));
	            m_direction = radius > 0 ? GeoCurve.RIGHT : GeoCurve.LEFT;
	            radius = Math.abs(radius);
	            delta = (Math.abs(m_chordBearing.Azimuth - m_tangentIn.Azimuth) * 2) > 360 ? 720 - (Math.abs(m_chordBearing.Azimuth - m_tangentIn.Azimuth) * 2) : (Math.abs(m_chordBearing.Azimuth - m_tangentIn.Azimuth) * 2);
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

            var chordBearingParam:ChordBearingParam = ChordBearingParam(
                params.GetParamByName(ChordBearingParam.NAME));

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
            else if (chordBearingParam  && chordLengthParam) 
            {
                m_chordBearing = chordBearingParam.Value;
                m_chordLength = chordLengthParam.Value;
                m_chordUom = chordLengthParam.UOM;
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
            m_delta = null;
            m_chordBearing = null;
            m_chordLength = NaN;
            m_chordUom = null;
            m_arcLength = NaN;
            m_arcUom = null;
        }
    }
}