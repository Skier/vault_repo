package truetract.web.dashboard.plotter
{
import mx.binding.utils.BindingUtils;
import mx.collections.ArrayCollection;

import truetract.plotter.domain.Tract;

[Bindable]
public class PlotterModel
{
    public var tract:Tract;
        
    public var menuData:ArrayCollection = new ArrayCollection ([
        {label: "File", children: [
            {label: "Save", id: FILE_SAVE, enabled: false},
            {label: "Print", id: FILE_PRINT, enabled: false},
            {type: "separator"},
            {label: "Close", id: FILE_CLOSE}
            ]},

        {label: "View", children: [
            {label: "Zoom All", id: VIEW_ZOOM_ALL},
            {label: "Zoom In", id: VIEW_ZOOM_IN},
            {label: "Zoom Out", id: VIEW_ZOOM_OUT}
        ]},

        {label: "Settings", children: [
            {label: "Show Annotations", type: "check", id: SETTINGS_SHOW_ANNOTATION},
            {label: "Show Area", type: "check", id: SETTINGS_SHOW_AREA}, 
            {type: "separator"},
            {label: "More..", id: SETTINGS_MORE}
        ]}
    ]);

    public function resetMenu():void
    {
        var tractNotNull:Boolean = (null != tract);
            
        getMenuItem(FILE_PRINT).enabled = tractNotNull;
        getMenuItem(VIEW_ZOOM_ALL).enabled = tractNotNull;
        getMenuItem(VIEW_ZOOM_IN).enabled = tractNotNull;
        getMenuItem(VIEW_ZOOM_OUT).enabled = tractNotNull;

        getMenuItem(FILE_SAVE).enabled = tractNotNull && tract.IsDirty;

        menuData.refresh();

        if (tractNotNull)
        {

            var tractIsDirtyHandler:Function = function (value:Boolean):void
            {
                getMenuItem(FILE_SAVE).enabled = value;
    
                menuData.refresh();
            }

            BindingUtils.bindSetter(tractIsDirtyHandler, tract, "IsDirty");
        }
    }

    private function getMenuItem(id:String):Object
    {
        return getMenuNodeItem(menuData.source, id);
    }

    private function getMenuNodeItem(nodes:Array, id:String):Object
    {
        var result:Object = null;
        
        for each (var o:Object in nodes) 
        {
            if (o.id != null && o.id == id) 
                result = o;
                
            else if (o.children != null)
                result = getMenuNodeItem(o.children, id);
            
            if (result != null) break;
        }

        return result;
    }

    public static const FILE_SAVE:String = "fileSave";
    public static const FILE_PRINT:String = "filePrint";
    public static const FILE_CLOSE:String = "fileClose";

    public static const VIEW_ZOOM_ALL:String = "viewZoomAll";
    public static const VIEW_ZOOM_IN:String = "viewZoomIn";
    public static const VIEW_ZOOM_OUT:String = "viewZoomOut";

    public static const SETTINGS_SHOW_AREA:String = "settingsShowArea";
    public static const SETTINGS_SHOW_ANNOTATION:String = "settingsShowAnnotation";
    public static const SETTINGS_MORE:String = "settingsMore";
    
}
}