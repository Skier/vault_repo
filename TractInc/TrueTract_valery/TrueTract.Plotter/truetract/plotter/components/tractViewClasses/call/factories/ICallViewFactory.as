package truetract.plotter.components.tractViewClasses.call.factories
{
    
    import truetract.plotter.components.tractViewClasses.call.CallView;
    import truetract.plotter.components.tractViewClasses.call.propertyViews.CallPropertiesView;
    import truetract.utils.IGeoShape;
    import truetract.domain.TractCall;
    
    internal interface ICallViewFactory
    {
        function GetCallView(call:TractCall):CallView;
        
        function GetCallPropertiesView(call:TractCall):CallPropertiesView;
        
        function GetCallGeoShape(call:TractCall):IGeoShape;
    }
}