package UI
{
	import UI.AppModel;
	import Domain.Document;
	import mx.rpc.remoting.RemoteObject;
	import Domain.Participant;
	import Domain.Tract;
	import mx.collections.ArrayCollection;
	import UI.Document.Participant.ParticipantModel;
	
	[Bindable]
	public class AppController
	{
		public var View:AppView;
		public var Model:AppModel;
		
		private var service:RemoteObject;
		
		public function AppController(view:AppView):void{
			View = view;
			Model = new AppModel();
		}
		
		public function Init():void {
			Model = new AppModel();
		}
		
		public function CreateNewDocument():void{
			OpenDocument(new Document());
		}

		public function OpenDocument(document:Document):void{
			SetWorkflowState(AppModel.WORKFLOWSTATE_DOCUMENT_EDIT);
			View.documentView.Controller.Match(document);
		}

		public function CloseDocument():void{
			SetWorkflowState(AppModel.WORKFLOWSTATE_DOCUMENT_LIST);
			View.documentListView.Controller.Reload();
		}

		public function SwitchToDocument():void {
			SetWorkflowState(AppModel.WORKFLOWSTATE_DOCUMENT_EDIT);
		}

		public function SwitchToParticipant(participantModel:ParticipantModel):void {
			SetWorkflowState(AppModel.WORKFLOWSTATE_PARTICIPANT_EDIT);
			View.participantView.Controller.Init(participantModel);
		}

        private function SetWorkflowState(state:int):void 
        {
            Model.workflowState = state;
            
            switch (state)
            {
                case AppModel.WORKFLOWSTATE_DOCUMENT_LIST :
                    View.mainViewStack.selectedChild = View.documentListView;
                    break;
                                                                
                case AppModel.WORKFLOWSTATE_DOCUMENT_EDIT :
                    View.mainViewStack.selectedChild = View.documentView;
                    break;
                        
                case AppModel.WORKFLOWSTATE_PARTICIPANT_EDIT :
                	View.mainViewStack.selectedChild = View.participantView;
                	break;
                        
                default :
                    throw new Error("Workflow state is invalid");
            }
        }        
	}
}