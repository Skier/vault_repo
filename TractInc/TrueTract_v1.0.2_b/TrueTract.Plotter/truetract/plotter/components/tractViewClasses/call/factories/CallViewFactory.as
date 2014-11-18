package truetract.plotter.components.tractViewClasses.call.factories
{
    import truetract.plotter.components.tractViewClasses.call.CallView;
    import truetract.plotter.components.tractViewClasses.call.propertyViews.CallPropertiesView;
    import truetract.plotter.utils.IGeoShape;
    import truetract.plotter.domain.TractCall;
    
    public class CallViewFactory implements ICallViewFactory
    {
        private static var m_instance:CallViewFactory;
        
        private var m_nextFactory:ICallViewFactory;
        
        public function CallViewFactory(nextFactory:ICallViewFactory) {
            
            if ( CallViewFactory.m_instance ){
                throw new Error("Only one Factory instance should be instantiated");
            }
            
            m_nextFactory = nextFactory;
        }
        
        public static function Instance():CallViewFactory {
            
            if ( CallViewFactory.m_instance == null ){

                var cf0:CurveWizardFactory =
                    new CurveWizardFactory(null);

                var cf1:RadialInRadialOutRadiusCurveFactory =
                    new RadialInRadialOutRadiusCurveFactory(cf0);

                var cf2:RadialInRadiusDeltaCurveFactory =
                    new RadialInRadiusDeltaCurveFactory(cf1);

                var cf3:TangentInTangentOutRadiusCurveFactory =
                    new TangentInTangentOutRadiusCurveFactory(cf2);

                var cf4:RadialInRadiusArcLengthCurveFactory =
                    new RadialInRadiusArcLengthCurveFactory(cf3);
                    
                var cf5:TangentInCurveDegreeChordLengthCurveFactory = 
                    new TangentInCurveDegreeChordLengthCurveFactory(cf4);

                var cf6:RadiusChordBearingChordLengthCurveFactory = 
                    new RadiusChordBearingChordLengthCurveFactory(cf5);

                var cf7:TangentInRadiusChordLengthCurveFactory = 
                    new TangentInRadiusChordLengthCurveFactory(cf6);

                var cf8:TangentInRadiusDeltaCurveFactory = 
                    new TangentInRadiusDeltaCurveFactory(cf7);

                var cf9:TangentInChordBearingChordLengthCurveFactory = 
                    new TangentInChordBearingChordLengthCurveFactory(cf8);

                var lf:LineFactory = new LineFactory(cf9);

                CallViewFactory.m_instance = new CallViewFactory(lf);
            }

            return m_instance;
        }

        public function GetCallView(call:TractCall):CallView
        {
            if (m_nextFactory){
                return m_nextFactory.GetCallView(call);
            } else {
                throw new Error("Unable to create call view. Unknown call type.");
            }
        }

        public function GetCallGeoShape(call:TractCall):IGeoShape
        {
            if (m_nextFactory){
                return m_nextFactory.GetCallGeoShape(call);
            } else {
                throw new Error("Unable to create call shape. Unknown call type.");
            }
        }


        public function GetCallPropertiesView(call:TractCall):CallPropertiesView {
            if (m_nextFactory){
                return m_nextFactory.GetCallPropertiesView(call);
            } else {
                throw new Error("Unable to create call properties view. Unknown call type.");
            }
        }
        
    }
}