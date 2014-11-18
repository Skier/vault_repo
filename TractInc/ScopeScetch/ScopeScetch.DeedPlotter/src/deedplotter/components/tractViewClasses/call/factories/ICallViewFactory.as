package src.deedplotter.components.tractViewClasses.call.factories
{
    
    import src.deedplotter.components.tractViewClasses.call.CallView;
    import src.deedplotter.components.tractViewClasses.call.propertyViews.CallPropertiesView;
    import src.deedplotter.utils.IGeoShape;
    import src.deedplotter.domain.TractCall;
    
    internal interface ICallViewFactory
    {
        function GetCallView(call:TractCall):CallView;
        
        function GetCallPropertiesView(call:TractCall):CallPropertiesView;
        
        function GetCallGeoShape(call:TractCall):IGeoShape;
    }
}