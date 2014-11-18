package src 
{
    import flash.events.IOErrorEvent;
    import flash.filesystem.*;
    import flash.printing.PrintJob;
    
    import mx.binding.utils.BindingUtils;
    import mx.binding.utils.ChangeWatcher;
    import mx.collections.ArrayCollection;
    import mx.collections.ItemResponder;
    import mx.controls.Alert;
    import mx.controls.Menu;
    import mx.core.Application;
    import mx.events.*;
    import mx.managers.PopUpManager;
    import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.remoting.mxml.RemoteObject;
    import mx.styles.CSSStyleDeclaration;
    import mx.styles.StyleManager;
    
    import src.common.ClientIdleTimeOut;
    import src.common.UserTractRegistry;
    import src.deedplotter.components.*;
    import src.deedplotter.components.tractViewClasses.SelectTractDialog;
    import src.deedplotter.components.tractViewClasses.SelectTractNameDialog;
    import src.deedplotter.domain.*;
    import src.deedplotter.domain.settings.UserSettingsRegistry;
    import src.export.TractExcelMapper;
    import src.fileSystem.FileSaveDialog;
    
    public class ScopeScetchController
    {
        
        [Bindable]
        public var Model:ScopeScetchModel = new ScopeScetchModel();
        
        [Bindable]
        public var appController:AppController;

        public var view:ScopeScetchView;
        
        public function ScopeScetchController( view:ScopeScetchView, parent:AppController):void
        {
            this.view = view;
            
            appController = parent;

            view.addEventListener(FlexEvent.CREATION_COMPLETE, view_creationCompeteHandler);
            Application.application.addEventListener(ClientIdleTimeOut.APP_TIME_OUT_EVENT,
                app_timeOutHandler);
        }
        
        public function Init():void {
            Reset();
            view.stage.window.maximize();

            view.callLater(CreateNewTract);
        }
        
        public function Reset():void 
        {
            Model.Reset();
            applyUserSettings();

            Model.GetMenuItembyId(Model.MENU_ITEM_FILE_SYNC).enabled = appController.IsOnline;

            Model.GetMenuItembyId(Model.MENU_ITEM_SETTINGS_SHOW_ANNOTATION).toggled =
                UserSettingsRegistry.getInstance().ShowAnnotations;
                
            Model.GetMenuItembyId(Model.MENU_ITEM_SETTINGS_SHOW_AREA).toggled =
                UserSettingsRegistry.getInstance().ShowArea;

            Model.MenuData.refresh();
            view.timeOutWatcher.startTimer();
        }
        
        public function OpenTract(tract:Tract):void 
        {
            view.deedplotter.InitTract(tract);
            
            BindingUtils.bindSetter(currentTract_IsDirtyHandler, tract, "IsDirty");

            Model.CurrentTract = tract;
            Model.GetMenuItembyId(Model.MENU_ITEM_FILE_PRINT).enabled = true;
            Model.GetMenuItembyId(Model.MENU_ITEM_FILE_EXPORT_EXCEL).enabled = true;
        }
        
        public function SaveTract(tract:Tract):void {
            if (tract.Description.length == 0) {
                var selectTractNameDialog:SelectTractNameDialog = SelectTractNameDialog.Open(view, true);
                
                selectTractNameDialog.TractList = appController.GetTracts();
                selectTractNameDialog.Responder = new ItemResponder(
                    function (tractName:String, token:Object = null):void {
                        tract.Description = tractName;
                        appController.SaveTract(tract);
                    }, null
                );
                
            } else {
                appController.SaveTract(tract);
            }
        }
        
        public function CreateNewTract(checkIsDirty:Boolean=true):void 
        {
        	if (appController.CurrentUser.NewTracts <= appController.Model.Storage.NewTracts.length && appController.CurrentUser.NewTracts != 0) {
        		Alert.show("You cannot create more than " + appController.CurrentUser.NewTracts.toString() + " tracts. Make sync with server to resolve this problem.");
        		return;
        	}
        	
        	var currentTract:Tract = Model.CurrentTract;
            
            if (checkIsDirty && currentTract && currentTract.IsDirty) 
            {
                var tractIsNewAndEmpty:Boolean = 
                    (!currentTract.TractId && currentTract.Calls.length == 0 && currentTract.TextObjects.length == 0);

                if (!tractIsNewAndEmpty) {
                    ConfirmSaving(CreateNewTract);
                    return;
                }
            }

            var newTract:Tract = new Tract();
            newTract.Description = "";
            newTract.Easting = 0;
            newTract.Northing = 0;
            newTract.CreatedBy = appController.CurrentUser.UserId;

            OpenTract(newTract);
        }
        
        private function SelectTract(checkIsDirty:Boolean=true):void 
        {
        	var currentTract:Tract = Model.CurrentTract;

            if (checkIsDirty && currentTract && currentTract.IsDirty) 
            {
                var tractIsNewAndEmpty:Boolean = 
                    (!currentTract.TractId && currentTract.Calls.length == 0 && currentTract.TextObjects.length == 0);

                if (!tractIsNewAndEmpty) {
                    ConfirmSaving(SelectTract);
                    return;
                }
            }

            var selectTractDialog:SelectTractDialog = SelectTractDialog.Open(view, true);

            selectTractDialog.TractList = appController.GetTracts();
            selectTractDialog.Responder = new ItemResponder(
                function (tractUid:String, token:Object = null):void 
                {
                    OpenTract(appController.Model.Storage.GetTract(tractUid));
                }, 
                null
            );
        }
        
        private function ConfirmSaving(f:Function):void {
            
            Alert.show("Current Tract has been modified. Save changes ?", "Save tract", 
                Alert.YES | Alert.NO | Alert.CANCEL, null, 
                function (event:CloseEvent):void 
                {
                    if (event.detail == Alert.CANCEL) {
                        return;
                    } 
                    
                    if (event.detail == Alert.YES) {
                        appController.SaveTract(Model.CurrentTract);
                    } 

                    f.call(null, false);

                }, null, Alert.YES);
        }

        private function tractExport():void {

            var fileSavePanel:FileSaveDialog;

            fileSavePanel = FileSaveDialog.Open(File.documentsDirectory);
            fileSavePanel.addEventListener(FileEvent.SELECT, saveSelectedFile)
        }
        
        private function saveSelectedFile(event:FileEvent):void {
            var exportFile:File = event.file;
            
            if (exportFile) {
            
                var stream:FileStream = new FileStream();
                stream.openAsync(exportFile, FileMode.WRITE);
                stream.addEventListener(IOErrorEvent.IO_ERROR, saveFile_errorHandler);
                
                var exportString:String = TractExcelMapper.getExcelXml(Model.CurrentTract);
                exportString = exportString.replace(/\n/g, File.lineEnding);
                stream.writeUTFBytes(exportString);
                
                Alert.show("Tract exporting succeeded");
            } 
            else
            {
                tractExport();
            }
            
        }
        
        private function applyUserSettings():void 
        {
            var popupAlpha:Number = UserSettingsRegistry.getInstance().PopUpAlpha;

            var popupDeclaration:CSSStyleDeclaration = 
                StyleManager.getStyleDeclaration("ExtendedTitleWindow");
                
            if (!isNaN(popupAlpha))
            {
                popupDeclaration.setStyle('backgroundAlpha', popupAlpha);
                popupDeclaration.setStyle('borderAlpha', popupAlpha - 0.1);
            }

            view.deedplotter.tractView.showCallAnnotations = 
                UserSettingsRegistry.getInstance().ShowAnnotations;

            view.deedplotter.tractView.showArea = 
                UserSettingsRegistry.getInstance().ShowArea;
            
            StyleManager.setStyleDeclaration("ExtendedTitleWindow", popupDeclaration, true);            
        }
        
        private function saveFile_errorHandler(event:Event):void 
        {
            Alert.show("The export File cannot be saved.", "Error");
        }

        private function view_creationCompeteHandler(event:FlexEvent):void 
        {
            view.mainMenu.addEventListener(MenuEvent.ITEM_CLICK, menu_itemClickHandler);
        }

        private function currentTract_IsDirtyHandler(value:Boolean):void {
            Model.GetMenuItembyId(Model.MENU_ITEM_FILE_SAVE).enabled = value;

            Model.MenuData.refresh();
        }

        private function menu_itemClickHandler(event:MenuEvent):void {
            //Keyboard events are being dispatched even if menu item is disabled. So skip them.
            if (event.item.enabled == false) return;
            
            switch (event.item.id) {
                case Model.MENU_ITEM_FILE_CREATE:
                    CreateNewTract();
                    break;

                case Model.MENU_ITEM_FILE_OPEN:
                    SelectTract();
                    break;
                    
                case Model.MENU_ITEM_FILE_SAVE:
                    SaveTract(Model.CurrentTract);
                    break;
                    
                case Model.MENU_ITEM_FILE_SYNC:
                    appController.Sync();
                    break;

                case Model.MENU_ITEM_FILE_EXPORT_EXCEL:
                    tractExport();
                    break;
                    
                case Model.MENU_ITEM_FILE_PRINT:
                    view.deedplotter.Print();
                    break;

                case Model.MENU_ITEM_FILE_RESET_LOCAL:
                    appController.ResetLocalData();
                    break;
                    
                case Model.MENU_ITEM_FILE_LOGOUT:
                    view.deedplotter.Clean();
                    appController.Logout();
                    break;
                    
                case Model.MENU_ITEM_VIEW_ZOOM_ALL:
                    view.deedplotter.ZoomAll();
                    break;

                case Model.MENU_ITEM_VIEW_ZOOM_IN:
                    view.deedplotter.ZoomIn();
                    break;

                case Model.MENU_ITEM_VIEW_ZOOM_OUT:
                    view.deedplotter.ZoomOut();
                    break;

                case Model.MENU_ITEM_SETTINGS_SHOW_AREA:
                    UserSettingsRegistry.getInstance().ShowArea = event.item.toggled;
                    view.deedplotter.tractView.showArea = event.item.toggled;
                    break;

                case Model.MENU_ITEM_SETTINGS_SHOW_ANNOTATION:
                    UserSettingsRegistry.getInstance().ShowAnnotations = event.item.toggled;
                    view.deedplotter.tractView.showCallAnnotations = event.item.toggled;
                    break;
                    
                case Model.MENU_ITEM_SETTINGS_MORE:
                    SettingsView.Open(view, true);
                    break;
                    
                default:
                    break;
            }
        }
        
        private function app_timeOutHandler(event:DynamicEvent):void 
        {
            if (Model.CurrentTract) 
            {
                if (Model.CurrentTract.Description.length == 0) 
                {
                    Model.CurrentTract.Description = "!auto_saved_tract_" + (new Date).toLocaleString();
                }

                if (appController.IsOnline) 
                {
                    appController.Sync();
                    appController.SaveTract(Model.CurrentTract);
                    appController.Sync();
                } 
                else 
                {
                    var userStorage:UserTractRegistry = appController.Model.Storage;
                    if (userStorage.NewTracts.length < userStorage.user.NewTracts ) 
                    {
                        appController.SaveTract(Model.CurrentTract);
                    }
                }

            }

            appController.Logout();
        }
    }
}