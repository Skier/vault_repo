package  src
{
    import mx.collections.ArrayCollection;
    
    import src.deedplotter.domain.Tract;
    
    [Bindable]
    public class ScopeScetchModel
    {
        public const MENU_ITEM_FILE_CREATE:String = "file_create";
        public const MENU_ITEM_FILE_CREATE_ATTACHED:String = "file_create_attached";
        public const MENU_ITEM_FILE_OPEN:String = "file_open";
        public const MENU_ITEM_FILE_OPEN_ATTACHED:String = "file_open_attached";
        public const MENU_ITEM_FILE_SAVE:String = "file_save";
        public const MENU_ITEM_FILE_ATTACH:String = "file_attach";
        public const MENU_ITEM_FILE_SYNC:String = "file_sync";
        public const MENU_ITEM_FILE_RESET_LOCAL:String = "file_reset_local";
        public const MENU_ITEM_FILE_EXPORT_EXCEL:String = "file_export_excel";
        public const MENU_ITEM_FILE_EXPORT_PDF:String = "file_export_pdf";
        public const MENU_ITEM_FILE_PRINT:String = "file_print";
        public const MENU_ITEM_FILE_LOGOUT:String = "file_logout";
        public const MENU_ITEM_VIEW_ZOOM_ALL:String = "view_zoom_all";
        public const MENU_ITEM_VIEW_ZOOM_IN:String = "view_zoom_in";
        public const MENU_ITEM_VIEW_ZOOM_OUT:String = "view_zoom_out";
        public const MENU_ITEM_SETTINGS_SHOW_AREA:String = "settings_show_area";
        public const MENU_ITEM_SETTINGS_SHOW_ANNOTATION:String = "settings_show_annotation";
        public const MENU_ITEM_SETTINGS_MORE:String = "settings_more";

        public var MenuData:ArrayCollection;
        
        public var CurrentTract:Tract;
 
        public function Reset():void {
            CurrentTract = null;
            
            MenuData = new ArrayCollection ([
                {label: "File", children: [
                    {label: "New", children: [
                        {label: "Tract", id: MENU_ITEM_FILE_CREATE_ATTACHED},
                        {label: "Drawing", id: MENU_ITEM_FILE_CREATE}
                        ]},
                    {label: "Open", children: [
                        {label: "Tract", id: MENU_ITEM_FILE_OPEN_ATTACHED},
                        {label: "Drawing", id: MENU_ITEM_FILE_OPEN}
                        ]},
                    {label: "Save", id: MENU_ITEM_FILE_SAVE, enabled: false},
                    {label: "Attach to Document", id: MENU_ITEM_FILE_ATTACH},
                    {label: "Print", id: MENU_ITEM_FILE_PRINT, enabled: false},
                    {label: "Sync with server", id: MENU_ITEM_FILE_SYNC},
                    {label: "Import", children: [
                        {label: "from export file", enabled: false}, 
                        {label: "from ASCII", enabled: false}
                        ]},
                    {label: "Export", children: [
                        {label: "to Excel file", id: MENU_ITEM_FILE_EXPORT_EXCEL, enabled: false}, 
                        {label: "to PDF file", id: MENU_ITEM_FILE_EXPORT_PDF}, 
                        {label: "to XML", enabled: false},
                        {label: "to AutoCAD DXF", enabled: false}
                        ]},
                    {type: "separator"},
                    {label: "Reset Local data", id: MENU_ITEM_FILE_RESET_LOCAL},
                    {label: "Logout", id: MENU_ITEM_FILE_LOGOUT}
                    ]},
    
                {label: "View", children: [
                    {label: "Zoom All", id: MENU_ITEM_VIEW_ZOOM_ALL},
                    {label: "Zoom In", id: MENU_ITEM_VIEW_ZOOM_IN},
                    {label: "Zoom Out", id: MENU_ITEM_VIEW_ZOOM_OUT}
                ]},
    
                {label: "Settings", children: [
                    {label: "Show Annotations", type: "check", id: MENU_ITEM_SETTINGS_SHOW_ANNOTATION},
                    {label: "Show Area", type: "check", id: MENU_ITEM_SETTINGS_SHOW_AREA}, 
                    {type: "separator"},
                    {label: "More..", id: MENU_ITEM_SETTINGS_MORE}
                ]}
            ]);
        }
        
        public function GetMenuItembyId(id:String):Object {
            return _getMenuItemById(MenuData.source, id);
        }
        
        private function _getMenuItemById(menuData:Array, id:String):Object {
            var result:Object = null;
            
            for each (var o:Object in menuData) 
            {
                
                if (o.id != null && o.id == id) 
                    result = o;
                else if (o.children != null)
                    result = _getMenuItemById(o.children, id);
                
                if (result != null) break;
            }
            
            return result;
        }
        

    }
}