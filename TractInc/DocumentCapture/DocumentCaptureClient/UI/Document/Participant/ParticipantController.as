package UI.Document.Participant
{
	import UI.Document.DocumentController;
	import Domain.Participant;
	import Domain.DataMapperRegistry;
	import Domain.Participantaddress;
	import mx.rpc.Responder;
	import Domain.Participantentityparty;
	import mx.collections.ArrayCollection;
	import mx.rpc.events.FaultEvent;
	import mx.controls.Alert;
	import Domain.Participantreservation;
	import mx.managers.PopUpManager;
	import mx.controls.CheckBox;
	import mx.states.State;
	import mx.events.CloseEvent;
	import mx.events.CollectionEvent;
	import mx.events.CollectionEventKind;
	
	[Bindable]
	public class ParticipantController
	{
		
		public var View:ParticipantView;
		public var Model:ParticipantModel;
		public var Parent:DocumentController;
		
		public function ParticipantController(view:ParticipantView, parent:DocumentController):void {
			View = view;
			Parent = parent;
		}
		
		public function Init(model:ParticipantModel):void {

			Model = model;

			if (Model.participant.isNew){
				Model.participant.TypeId = 2;
				var popupWin:ParticipantTypeView = ParticipantTypeView(PopUpManager.createPopUp(View, ParticipantTypeView, true));
				popupWin.participant = Model.participant;
			}

			Model.entityParts.addEventListener(CollectionEvent.COLLECTION_CHANGE, onParticipantsChanged);
			Model.reservations.addEventListener(CollectionEvent.COLLECTION_CHANGE, onReservationsChanged);
		}
		
		public function OnClickComplete():void {
			
			if (!View.IsValid()) {
				return;
			}
			
			if (Model.reservations.length == 0 && Model.participant.isSeller && Model.participant.isNew) {
	            Alert.show("Do you want to enter reservations?", "Save Participant", 3, View, 
	            	function(event:CloseEvent):void {
		                if (event.detail == Alert.YES) {
							OnClickAddParticipantReservation();
							return;
		                } else {
							saveModel();
		                }
	            	});
			} else {
				saveModel();
			}
			
		}
		
		private function saveModel():void {

			if (Model.participant.isNew) {
				Model.parentCollection.addItem(Model);
				Model.participant.isNew = false;
			}

			Model.save();
			Parent.CloseParticipant();
		}
		
		public function OnClickCancel():void {
			Parent.CloseParticipant();
		}
		
		public function OnClickAddParticipantReservation():void {
			var popupWin:ParticipantReservationView = ParticipantReservationView(PopUpManager.createPopUp(View, ParticipantReservationView, true));
			popupWin.reservation = new Participantreservation();
			popupWin.reservation.isNew = true;
			popupWin.parentCollection = Model.reservations;
		}
		
		public function OnClickEditParticipantReservation():void {

			var reservation:Participantreservation;

			if (Model.participant.TypeId == 0) {
				reservation = View.companyForm.reservations.dgParticipantReservations.selectedItem as Participantreservation;
			} else {
				reservation = View.individualForm.reservations.dgParticipantReservations.selectedItem as Participantreservation;
			}

			var popupWin:ParticipantReservationView = ParticipantReservationView(PopUpManager.createPopUp(View, ParticipantReservationView, true));
			popupWin.reservation = reservation;
			popupWin.reservation.isNew = false;
			popupWin.parentCollection = Model.reservations;
		}
		
		public function OnClickRemoveParticipantReservation():void {

			var reservation:Participantreservation;

			if (Model.participant.TypeId == 0) {
				reservation = View.companyForm.reservations.dgParticipantReservations.selectedItem as Participantreservation;
			} else {
				reservation = View.individualForm.reservations.dgParticipantReservations.selectedItem as Participantreservation;
			}

			var idx:int = Model.reservations.getItemIndex(reservation);
			reservation.remove();
			if (idx != -1) {
				Model.reservations.removeItemAt(idx);
			}
		}
		
		public function OnClickAddParticipantEntityParty():void {
			var popupWin:ParticipantEntityPartyView = ParticipantEntityPartyView(PopUpManager.createPopUp(View, ParticipantEntityPartyView, true));
			popupWin.entityParty = new Participantentityparty();
			popupWin.entityParty.isNew = true;
			popupWin.parentCollection = Model.entityParts;
		}
		
		public function OnClickEditParticipantEntityParty():void {
			var popupWin:ParticipantEntityPartyView = ParticipantEntityPartyView(PopUpManager.createPopUp(View, ParticipantEntityPartyView, true));
			popupWin.entityParty = View.companyForm.parts.dgParticipantEntityParts.selectedItem as Participantentityparty;
			popupWin.entityParty.isNew = false;
			popupWin.parentCollection = Model.entityParts;
		}
		
		public function OnClickRemoveParticipantEntityParty():void {
			var entityParty:Participantentityparty = View.companyForm.parts.dgParticipantEntityParts.selectedItem as Participantentityparty;
			var idx:int = Model.entityParts.getItemIndex(entityParty);
			entityParty.remove();
			if (idx != -1) {
				Model.entityParts.removeItemAt(idx);
			}
		}
		
		private function onParticipantsChanged(event:CollectionEvent):void {
			if (event.kind == CollectionEventKind.ADD) {
				var gridLastIndex:int = Model.entityParts.length - 1;
				View.companyForm.parts.dgParticipantEntityParts.selectedIndex = gridLastIndex;
				View.companyForm.parts.dgParticipantEntityParts.scrollToIndex(gridLastIndex);
			}
		}
		
		private function onReservationsChanged(event:CollectionEvent):void {
			if (event.kind == CollectionEventKind.ADD) {
				var gridLastIndex:int = Model.reservations.length - 1;
				if (Model.participant.TypeId == 0) {
					View.companyForm.reservations.dgParticipantReservations.selectedIndex = gridLastIndex;
					View.companyForm.reservations.dgParticipantReservations.scrollToIndex(gridLastIndex);
				} else {
					View.individualForm.reservations.dgParticipantReservations.selectedIndex = gridLastIndex;
					View.individualForm.reservations.dgParticipantReservations.scrollToIndex(gridLastIndex);
				}
			}
		}
		
	}
}