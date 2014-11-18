package truetract.plotter.components.tractViewClasses.call.factories
{
    
    import truetract.plotter.components.tractViewClasses.call.CallView;
    import truetract.plotter.components.tractViewClasses.call.propertyViews.CallPropertiesView;
    import truetract.plotter.utils.IGeoShape;
    import truetract.plotter.domain.TractCall;
    
    internal interface ICallViewFactory
    {
        function GetCallView(call:TractCall):CallView;
        
        function GetCallPropertiesView(call:TractCall):CallPropertiesView;
        
        function GetCallGeoShape(call:TractCall):IGeoShape;
    }
}