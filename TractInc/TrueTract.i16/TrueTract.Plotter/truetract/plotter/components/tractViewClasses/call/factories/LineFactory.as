package truetract.plotter.components.tractViewClasses.call.factories
{
    import truetract.plotter.components.tractViewClasses.call.CallLineView;
    import truetract.plotter.components.tractViewClasses.call.CallView;
    import truetract.plotter.components.tractViewClasses.call.propertyViews.CallPropertiesView;
    import truetract.plotter.components.tractViewClasses.call.propertyViews.LinePropertiesView;
    import truetract.plotter.domain.TractCall;
    import truetract.plotter.domain.callparams.BearingParam;
    import truetract.plotter.domain.callparams.DistanceParam;
    import truetract.plotter.domain.callparams.ParamCollection;
    import truetract.plotter.utils.GeoBearing;
    import truetract.plotter.utils.GeoLine;
    import truetract.plotter.utils.IGeoShape;
    import truetract.plotter.utils.UnitOfMeasure;
    
    internal class LineFactory extends CallViewFactory
    {
        private var m_bearing:GeoBearing;
        private var m_distance:Number;
        private var m_distanceUom:UnitOfMeasure;
        
        public function LineFactory(nextFactory:ICallViewFactory) {
            super(nextFactory);
        }
        
        override public function GetCallView(call:TractCall):CallView
        {
            if (call.CallType.toUpperCase() == TractCall.CALL_TYPE_LINE && parseParams(call.Params)) {
                
                var result:CallView = new CallLineView( 
                    new GeoLine(m_bearing, m_distance * m_distanceUom.RateToOneFeet) );
                    
                result.Model = call;
                
                return result;
                
            } else {
                
                return super.GetCallView(call);
                
            }
        }

        override public function GetCallGeoShape(call:TractCall):IGeoShape {
            
            if (call.CallType.toUpperCase() == TractCall.CALL_TYPE_LINE && parseParams(call.Params)) {
                
                return new GeoLine(m_bearing, m_distance * m_distanceUom.RateToOneFeet);
                
            } else {
                
                return super.GetCallGeoShape(call);
                
            }
        }

        override public function GetCallPropertiesView(call:TractCall):CallPropertiesView 
        {
            if (call.CallType.toUpperCase() != TractCall.CALL_TYPE_LINE || !parseParams(call.Params)){
                
                return super.GetCallPropertiesView(call);
                
            } else {
                
                var result:LinePropertiesView = new LinePropertiesView();
                result.Model = call;
                result.directionTxt.text = m_bearing.toInputString();
                result.distanceTxt.text = m_distance.toString();
                result.uom = m_distanceUom;
                
                return result;
            }
        }
        
        private function parseParams(params:ParamCollection):Boolean 
        {
            var bearingParam:BearingParam = BearingParam( params.GetParamByName(BearingParam.NAME));
            var distanceParam:DistanceParam = DistanceParam(params.GetParamByName(DistanceParam.NAME));
            
            if (bearingParam && distanceParam) {
                m_bearing = bearingParam.Value;
                m_distance = distanceParam.Value;
                m_distanceUom = distanceParam.UOM;
                
                return true;
            } else {
                return false;
            }
        }
      
    }
}