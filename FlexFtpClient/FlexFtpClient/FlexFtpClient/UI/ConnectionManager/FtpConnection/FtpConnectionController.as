/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package UI.ConnectionManager.FtpConnection
{
    import Domain.*;
    import Domain.Common.*;

    import UI.AppModel;
    import UI.ConnectionManager.FtpConnection.SelectDirectory.*;
    import UI.ConnectionManager.FtpConnection.CreateDirectory.*;
    import UI.ConnectionManager.ConnectionManagerController;

    import mx.core.*;
    import mx.rpc.remoting.mxml.RemoteObject;
    import mx.rpc.events.*;
    import mx.rpc.AsyncToken;
    import mx.rpc.Responder;
    import mx.controls.*;
    import mx.controls.dataGridClasses.*;
    import mx.managers.DragManager;
    import mx.events.*;
    import mx.utils.ObjectUtil;
    import mx.managers.PopUpManager;
    import mx.collections.ArrayCollection;
    import mx.formatters.NumberFormatter;
    import flash.events.*;
    import flash.net.URLRequest;
    import flash.net.FileReference;
    import flash.ui.ContextMenuItem;
    import flash.display.DisplayObject;
    import flash.net.FileReferenceList;

    
    public class FtpConnectionController
    {
        private var m_canRename:Boolean = false;
        private var fr:FileReference = new FileReference();
        private var deleteItemMenu:ContextMenuItem;
        private var renameItemMenu:ContextMenuItem;
        
        public var View:FtpConnectionView;
        [Bindable]
        public var Model:FtpConnectionModel;
        public var Parent:ConnectionManagerController;
        public var Service:RemoteObject;
        
        public function FtpConnectionController(view:FtpConnectionView, parent:ConnectionManagerController)
        {
            View = view;
            Parent = parent;
            Service = new RemoteObject(AppModel.WEBORB_SERVICE_NAME);
            Service.CloseConnection.addEventListener(FaultEvent.FAULT, Parent.OnFault);
            Service.DeleteFtpFiles.addEventListener(ResultEvent.RESULT, OnGeneralResult);
            Service.RenameFtpFile.addEventListener(ResultEvent.RESULT, OnGeneralResult);
            Service.MoveFtpFiles.addEventListener(ResultEvent.RESULT, OnGeneralResult);
            Service.addEventListener(FaultEvent.FAULT, OnGeneralFault);
            
            Application.application.addEventListener(CreateDirectoryEvent.EVENT_CREATE_DIRECTORY, OnCreateDirectory);
            Application.application.addEventListener(CommitUploadEvent.EVENT_COMMIT_UPLOAD_PROCESS, OnCommitUpload);

            deleteItemMenu = new ContextMenuItem("Delete selected item");
            deleteItemMenu.addEventListener(ContextMenuEvent.MENU_ITEM_SELECT, OnDelete);
            
            renameItemMenu = new ContextMenuItem("Rename selected item");
            renameItemMenu.addEventListener(ContextMenuEvent.MENU_ITEM_SELECT, OnRename);
        }
        
        public function Init(connectionModel:FtpConnectionModel):void
        {
            Model = connectionModel;
        }

        public function OnCreationComplete():void
        {
            ConnectToFtp();
            CreateContextMenu();
        }
        
        public function CreateContextMenu():void
        {
            Application.application.contextMenu.customItems.push(deleteItemMenu);
            Application.application.contextMenu.customItems.push(renameItemMenu);
        }
        
        public function ClearContextMenu():void
        {
            while (Application.application.contextMenu.customItems.length > 0) {
                Application.application.contextMenu.customItems.pop();
            }
        }
        
        public function ConnectToFtp():void
        {
            
            View.enabled = false;
            
            var asyncToken:AsyncToken = Service.ConnectToFtp(Model.ConnectionInfo);
            asyncToken.addResponder (new Responder(
                function(event:ResultEvent):void {
            		View.enabled = true;
                    Model.ConnectionInfo.ConnectionId = String(event.result);
            		SetCurrentDirectory(Model.Root);
                }, 
                function(event:FaultEvent):void {
					CloseConnection();
                }
            ));

        }

        public function CloseConnection():void
        {
            var event:DisconnectEvent = new DisconnectEvent(View);
            
            Application.application.dispatchEvent(event);
            
            Service.CloseConnection(Model.ConnectionInfo.ConnectionId);
        }
            
        public function OnRefresh():void  
        {
            RetrieveDirectory(Model.CurrentDirectory);
        }       
        
        public function RetrieveDirectory(directory:FtpDirectory):void
        {
            View.content.enabled = false;
            
            Model.ConnectionInfo.CurrentDir = directory.getPath();

            var asyncToken:AsyncToken = Service.GetFtpDirectory(Model.ConnectionInfo);
            asyncToken.addResponder (new Responder(
                function(event:ResultEvent):void {
                    Model.ConnectionInfo.CurrentDir = Model.CurrentDirectory.getPath();
                    directory.Files = new ArrayCollection(event.result as Array);
                    SyncTree();
                    View.content.enabled = true;
                    View.dgCurrentDir.setFocus();
                }, 
                function(event:FaultEvent):void {
                }
            ));

        }

        public function OnDirTreeChange(event:Event):void  
        {
            var tree:Tree = Tree(event.target);
			
			ChangeDir(FtpFile(tree.selectedItem));
        }       
        
        public function OnPopUpTreeChange(event:Event):void 
        {
            var tree:Tree = Tree(event.target);
            var directory:FtpDirectory =  FtpDirectory(tree.selectedItem);
            
            tree.expandItem(tree.selectedItem, true);
            
            RetrieveDirectory(directory);
        }       
        
        public function OnCurrentDirDoubleClick(event:Event):void 
        {
            if (event.currentTarget is DataGrid) {
                var dg:DataGrid = DataGrid(event.currentTarget);
                if (dg.selectedItem != null) {
                    ChangeDir(FtpFile(dg.selectedItem));
                }
            }
        }   
        
        public function OnCurrentDirKeyUp(event:KeyboardEvent):void   
        {
        	var dg:DataGrid = event.currentTarget as DataGrid;
        	if (dg.selectedItem != null && event.keyCode == 13){
        		ChangeDir(FtpFile(event.currentTarget.selectedItem));
        	} else if (dg.selectedItems.length > 0 && event.keyCode == 46 && !View.dgCurrentDir.editable){
                OnDelete(event);
        	}
        }   

        public function OnUpload():void
        {
			var fileRefList:FileReferenceList = new FileReferenceList();
			
			fileRefList.addEventListener(Event.SELECT, 
				function(ev:Event):void{
					for each (var fileRef:FileReference in fileRefList.fileList){
						if (FtpFileExists(fileRef.name)) {
							ConfirmOverriding(fileRef);
						} else {
				            Application.application.dispatchEvent(new AddUploadEvent(fileRef, Model.ConnectionInfo));							
						}
						
					}
				});
			
			fileRefList.browse();
        }
        
        public function OnDownload():void
        {
            for each (var file:FtpFile in View.dgCurrentDir.selectedItems){
                if (!file.IsParentLink) {
		            var event:AddDownloadEvent = new AddDownloadEvent(file, Model.ConnectionInfo);
		            Application.application.dispatchEvent(event);
                }
            }
        }
            
        public function OnDelete(event:Event):void
        {
            if (View.dgCurrentDir.selectedItems.length > 0){
                var file:FtpFile = FtpFile(View.dgCurrentDir.selectedItems[0])
                if (View.dgCurrentDir.selectedItems.length == 1 && file.IsParentLink){
                    return;
                }
	            Alert.show('Are you really want to delete selected items?', 'Alert',
	                Alert.YES | Alert.NO, View,
	                    function(event:CloseEvent):void {
	                        if (event.detail == Alert.YES) {
					        	var files:Array = new Array;
					            for each (var file:FtpFile in View.dgCurrentDir.selectedItems) {
					                if (!file.IsParentLink){
					                    files.push(file.SimpleClone());
					                }
					            }
            					View.content.enabled = false;

	                            Service.DeleteFtpFiles(files, Model.ConnectionInfo);
	                        }
	                    });
            }
        }

        public function OnRename(event:Event):void
        {
            if (View.dgCurrentDir.selectedItem == null) {
                return;
            }
            
            var file:FtpFile = FtpFile(View.dgCurrentDir.selectedItems[0]);
            
            if (file.IsParentLink) {
                return;
            }
            
            View.dgCurrentDir.editable = true;
            View.dgCurrentDir.editedItemPosition = {columnIndex:0, rowIndex:View.dgCurrentDir.selectedIndex};
        }
            
        public function OnEditEnd(event:DataGridEvent):void
        {
           	View.dgCurrentDir.editable = false;
            event.preventDefault();
            
            if (event.reason == DataGridEventReason.CANCELLED) {
                View.dgCurrentDir.destroyItemEditor();
                return;
            }            
            
            var newName:String = FilenameEditor(event.currentTarget.itemEditorInstance).FileName;
            var oldFile:FtpFile = FtpFile(event.currentTarget.selectedItem);
           	
           	View.dgCurrentDir.destroyItemEditor();
			
			if (FtpFileExists(newName)) {
				Alert.show("File " + newName + " already exists", "Alert");
			} else if (newName.length > 0) {
            	Service.RenameFtpFile(oldFile.SimpleClone(), newName, Model.ConnectionInfo);
            }
        }
            
        public function OnMoveClick(event:Event):void
        {
            var file:FtpFile = FtpFile(View.dgCurrentDir.selectedItem);
            if (file.IsParentLink) {
                return;
            }
        
            var selectTree:SelectDirectoryView = SelectDirectoryView(PopUpManager.createPopUp(View, SelectDirectoryView, true));
            selectTree.ParentController = this;
        }
            
        public function Move(directory:FtpDirectory):void
        {
            View.content.enabled = false;

        	var files:Array = new Array;
        
            for each (var file:FtpFile in View.dgCurrentDir.selectedItems) {
                if (!file.IsParentLink) {
                    if (file.IsDirectory && (file.getPath() == directory.getPath().substr(0, file.getPath().length))) {
                        Alert.show("Sorry, but you cannot move directory to itself [" + directory.getPath() + "]");
                    } else {
                        files.push(file.SimpleClone());
                    }
                }
            }
        
            Service.MoveFtpFiles(files, directory.getPath(), Model.ConnectionInfo);
        }
            
		public function OnCreateDirectoryClick():void
		{
			CreateDirectoryView(PopUpManager.createPopUp(View, CreateDirectoryView, true));
		}

        public function FormatSize(data:FtpFile, column:DataGridColumn):String
        {
            var result:String;
            if (data.IsDirectory) {
                result = "<dir>";
            } else {
            	var nf:NumberFormatter = new NumberFormatter();
            	nf.precision = 2;
            	nf.useThousandsSeparator = true;
                
                if (data.Size > 1073741824) 
                    result = nf.format(data.Size/1073741824) + "Gb";
                else if (data.Size > 1048576) 
                    result = nf.format(data.Size/1048576) + "Mb";
                else if (data.Size > 1024) 
                    result = nf.format(data.Size/1024) + "Kb";
                else 
                {
                    nf.precision = 0;
                    result = nf.format(data.Size);
                }
            }
            
            return result;
        }
            
        public function OnTreeDragEnter(event:DragEvent):void
        {
            if (event.dragInitiator is DataGrid) {
                var dg:DataGrid;
                dg = event.dragInitiator as DataGrid;

                if (dg.selectedItems[0] is FtpFile || dg.selectedItems[0] is FtpDirectory) {
                    var file:FtpFile = FtpFile(dg.selectedItems[0]);
                    if (!file.IsParentLink) {
                        View.dirTree.validateNow();
                        DragManager.acceptDragDrop(UIComponent(event.target));
                    }
                }
            }
        }

        public function OnTreeDragOver(event:DragEvent):void
        {
           var dropTarget:Tree = Tree(event.currentTarget);
           var r:int = dropTarget.calculateDropIndex(event);
           
           View.dirTree.selectedIndex = r;
        }


        public function OnTreeDragDrop(event:DragEvent):void
        {
            Move(FtpDirectory(View.dirTree.selectedItem));
        }  

        public function OnGridDragEnter(event:DragEvent):void
        {
        	DragManager.acceptDragDrop(UIComponent(event.target));
        }

        public function OnGridDragDrop(event:DragEvent):void
        {
           	var dataGrid:DataGrid = DataGrid(event.currentTarget);
       		var dp:ArrayCollection = ArrayCollection(View.dgCurrentDir.dataProvider);
       		var idx:int = View.dgCurrentDir.calculateDropIndex(event);
       		
       		if (idx >= dp.length)
       			return;
       		
       		var target:FtpFile = FtpFile(dp.getItemAt(idx));
       		
       		if (target.IsDirectory) {
       			if (target.IsParentLink && (Model.CurrentDirectory.Directory != null)) {
       				Move(Model.CurrentDirectory.Directory);
       			} else {
       				Move(FtpDirectory(dp.getItemAt(idx)));
       			}
       		}
       		else {
       			return;
       		}
        }  

        private function SetCurrentDirectory(directory:FtpDirectory):void
        {

            Model.CurrentDirectory = directory;
            RetrieveDirectory(directory);
        }

		private function SyncTree():void 
		{
            var arr:ArrayCollection = new ArrayCollection();
            var parent:FtpDirectory = Model.CurrentDirectory;
            
            while(parent != null) {
                arr.addItemAt(parent, 0);
                parent = parent.Directory
            }
            
            for each(var dir:FtpDirectory in arr) {
                View.dirTree.expandItem(dir, true, true);
            }

            View.dirTree.selectedItem = Model.CurrentDirectory;
            View.dirTree.scrollToIndex(View.dirTree.selectedIndex);
		}

        private function ChangeDir(file:FtpFile):void
        {
            if(file.IsDirectory) {
                if (file.IsParentLink) {
                    GoParent();
                } 
                else {
                    SetCurrentDirectory(FtpDirectory(file));
                }
            }
        }

        private function GoParent():void
        {
            if (Model.CurrentDirectory.Directory != null) {
                SetCurrentDirectory(Model.CurrentDirectory.Directory);
            }
        }
        
        private  function ConfirmOverriding(fr:FileReference):void 
        {
			Alert.show("File " + fr.name + " already exists.\nDo you want to overwrite the file?", "FileExists", 3, View, 
        		function (event:CloseEvent):void{
        			if (event.detail == Alert.YES) {
			            Application.application.dispatchEvent(new AddUploadEvent(fr, Model.ConnectionInfo));							
        			}
        		});
        }
        
        private function FtpFileExists(fileName:String):Boolean 
        {
        	for each (var ftpFile:FtpFile in Model.CurrentDirectory.Files) {
        		if (ftpFile.Name == fileName){
        			return true;
        		}
        	}
        	
        	return false;
        }
        
        private function OnCommitUpload(event:CommitUploadEvent):void
        {
            var connectionInfo:FtpConnectionInfo = event.connectionInfo;
            
            if (Model.ConnectionInfo.Host == connectionInfo.Host 
                && Model.ConnectionInfo.CurrentDir == connectionInfo.CurrentDir
                && View.content.enabled == true) {
                RetrieveDirectory(Model.CurrentDirectory);
            }
        }

        private function OnCreateDirectory(event:CreateDirectoryEvent):void
        {
            if (Parent.Model.CurrentFtpConnectionModel != Model)
            	return;
            
            View.content.enabled = false;
            
            var dirName:String = event.newDirectoryName;
            var parent:FtpDirectory = Model.CurrentDirectory;
            
            var asyncToken:AsyncToken = Service.CreateFtpDirectory(dirName, Model.ConnectionInfo);
            asyncToken.addResponder (new Responder(
                function(event:ResultEvent):void{
                	var newDir:FtpDirectory = FtpDirectory.Create(dirName, parent);
                	
                    View.content.enabled = true;

                    if (parent == Model.CurrentDirectory){
                    	View.dgCurrentDir.selectedItem = newDir;
                    	View.dgCurrentDir.scrollToIndex(View.dgCurrentDir.selectedIndex);
                    }

                }, 
                function(event:FaultEvent):void{
                }
            ));

        }
        
        private function OnGeneralResult(event:ResultEvent):void
        {
            View.content.enabled = true;
            RetrieveDirectory(Model.CurrentDirectory);
        }

        private function OnGeneralFault(event:FaultEvent):void
        {
            View.content.enabled = true;
            Alert.show("Error : " + event.fault.faultString, "Server error");
        }

    }

}