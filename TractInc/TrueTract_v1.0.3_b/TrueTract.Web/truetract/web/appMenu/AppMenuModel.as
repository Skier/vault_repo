package truetract.web.appMenu
{
import mx.collections.ArrayCollection;

public class AppMenuModel
{

    [Bindable]
    public var data:ArrayCollection = new ArrayCollection ([
        {label: "File", children: [
            {label: "New", children: [
                {label: "Tract", eventType: AppMenuEvent.FILE_CREATE_TRACT},
                {label: "Drawing", eventType: AppMenuEvent.FILE_CREATE_DRAWING}
            ]},
            {label: "Open", children: [
                {label: "Tract", eventType: AppMenuEvent.FILE_OPEN_TRACT},
                {label: "Drawing", eventType: AppMenuEvent.FILE_OPEN_DRAWING}
            ]},
            {label: "Save", eventType: AppMenuEvent.FILE_SAVE, enabled: false},
            {label: "Attach to Document", eventType: AppMenuEvent.FILE_ATTACH_DRAWING},
            {label: "Print", eventType: AppMenuEvent.FILE_PRINT, enabled: false},
            {label: "Import", children: [
                {label: "from export file", enabled: false}, 
                {label: "from ASCII", enabled: false}
                ]},
            {label: "Export", children: [
                {label: "to Excel file", eventType: AppMenuEvent.FILE_EXPORT_EXCEL, enabled: false}, 
                {label: "to PDF file", eventType: AppMenuEvent.FILE_EXPORT_PDF}, 
                {label: "to XML", enabled: false},
                {label: "to AutoCAD DXF", enabled: false}
                ]},
            {type: "separator"},
            {label: "Logout", eventType: AppMenuEvent.FILE_LOGOUT}
            ]},

        {label: "View", children: [
            {label: "Zoom All", eventType: AppMenuEvent.VIEW_ZOOM_ALL},
            {label: "Zoom In", eventType: AppMenuEvent.VIEW_ZOOM_IN},
            {label: "Zoom Out", eventType: AppMenuEvent.VIEW_ZOOM_OUT}
        ]},

        {label: "Settings", children: [
            {label: "Show Annotations", type: "check", eventType: AppMenuEvent.SETTINGS_SHOW_ANNOTATION},
            {label: "Show Area", type: "check", eventType: AppMenuEvent.SETTINGS_SHOW_AREA}, 
            {type: "separator"},
            {label: "More..", eventType: AppMenuEvent.SETTINGS_MORE}
        ]}
    ]);

    public function getMenuItemByEvent(node:Array, eventType:String):Object {
        var result:Object = null;
        
        for each (var o:Object in node) 
        {
            if (o.eventType != null && o.eventType == eventType) 
                result = o;
                
            else if (o.children != null)
                result = getMenuItemByEvent(o.children, eventType);
            
            if (result != null) break;
        }

        return result;
    }
}
}