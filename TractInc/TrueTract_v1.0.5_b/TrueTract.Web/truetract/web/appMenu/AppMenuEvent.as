package truetract.web.appMenu
{
import flash.events.Event;

public class AppMenuEvent extends Event
{

    public static const FILE_CREATE_DRAWING:String = "fileCreateDrawing";
    public static const FILE_CREATE_TRACT:String = "fileCreateTract";
    public static const FILE_OPEN_DRAWING:String = "fileOpenDrawing";
    public static const FILE_OPEN_TRACT:String = "fileOpenTract";
    public static const FILE_SAVE:String = "fileSave";
    public static const FILE_ATTACH_DRAWING:String = "fileAttachDrawing";
    public static const FILE_EXPORT_EXCEL:String = "fileExportExcel";
    public static const FILE_EXPORT_PDF:String = "fileExportPdf";
    public static const FILE_PRINT:String = "filePrint";
    public static const FILE_LOGOUT:String = "fileLogout";
    public static const VIEW_ZOOM_ALL:String = "viewZoomAll";
    public static const VIEW_ZOOM_IN:String = "viewZoomIn";
    public static const VIEW_ZOOM_OUT:String = "viewZoomOut";
    public static const SETTINGS_SHOW_AREA:String = "settingsShowArea";
    public static const SETTINGS_SHOW_ANNOTATION:String = "settingsShowAnnotation";
    public static const SETTINGS_MORE:String = "settingsMore";

    public var item:Object;

    public function AppMenuEvent(type:String, item:Object,
       bubbles:Boolean=true, cancelable:Boolean=false)
    {
        super(type, bubbles, cancelable);
        
        this.item = item;
    }
            
    override public function clone():Event
    {
        return new AppMenuEvent(type, item, bubbles, cancelable);
    }

}
}