package src.deedplotter.components.tractViewClasses.call.factories
{
    import src.deedplotter.components.tractViewClasses.call.CallLineView;
    import src.deedplotter.components.tractViewClasses.call.CallView;
    import src.deedplotter.components.tractViewClasses.call.propertyViews.CallPropertiesView;
    import src.deedplotter.components.tractViewClasses.call.propertyViews.LinePropertiesView;
    import src.deedplotter.domain.TractCall;
    import src.deedplotter.domain.callparams.BearingParam;
    import src.deedplotter.domain.callparams.DistanceParam;
    import src.deedplotter.domain.callparams.ParamCollection;
    import src.deedplotter.utils.GeoBearing;
    import src.deedplotter.utils.GeoLine;
    import src.deedplotter.utils.IGeoShape;
    import src.deedplotter.utils.UnitOfMeasure;
    
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