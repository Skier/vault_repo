/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package UI.ConnectionManager.FtpConnection.SelectDirectory
{
	import Domain.Common.FtpDirectory;

	import UI.ConnectionManager.FtpConnection.*;

	import mx.managers.PopUpManager;
	import flash.events.Event;
	
	public class SelectDirectoryController
	{
		public var View:SelectDirectoryView;

		[Bindable]
		public var Parent:FtpConnectionController;

		public var Model:FtpConnectionModel;
		
		public function SelectDirectoryController(view:SelectDirectoryView, parent:FtpConnectionController):void{
			View = view;
			Parent = parent;
			Model = Parent.Model;
            View.directoryTree.selectedItem = Model.CurrentDirectory;
		}

        public function OnCreationComplete():void {
            PopUpManager.centerPopUp(View);
        }

        public function OnClose(event:Event):void {
            PopUpManager.removePopUp(View);
        }
        
        public function OnMove(event:Event):void {
            Parent.Move(FtpDirectory(View.directoryTree.selectedItem));   
            PopUpManager.removePopUp(View);
        }
	}
}