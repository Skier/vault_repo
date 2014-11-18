/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package UI.ConnectionManager.FtpConnection.CreateDirectory
{
	import Domain.CreateDirectoryEvent;

	import mx.core.Application;
	import mx.managers.PopUpManager;
	import mx.events.ValidationResultEvent;
	import flash.events.Event;
	
	public class CreateDirectoryController
	{
		public var View:CreateDirectoryView;
		public var Model:CreateDirectoryModel;
		
		public function CreateDirectoryController(view:CreateDirectoryView):void
		{
			View = view;
			Model = new CreateDirectoryModel();
		}

        public function OnCreationComplete():void 
        {
            PopUpManager.centerPopUp(View);
            View.focusManager.setFocus(View.newDir);
        }

        public function CloseWin(e:Event):void 
        {
            PopUpManager.removePopUp(View);
        }
        
        public function DoCreate(e:Event):void 
        {
            var vResult:ValidationResultEvent = View.nameValidator.validate();
            
            if (vResult.type==ValidationResultEvent.INVALID) {
                return;
            }

            Model.NewDirectoryName = View.newDir.text;

            Application.application.dispatchEvent(new CreateDirectoryEvent(Model.NewDirectoryName));

            PopUpManager.removePopUp(View);
        }

	}

}