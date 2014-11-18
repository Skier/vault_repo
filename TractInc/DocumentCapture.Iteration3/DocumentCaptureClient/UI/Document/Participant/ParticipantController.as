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
			
		}
		
		public function OnClickComplete():void {
			
			if (!View.IsValid()) {
				return;
			}
			
			Model.save();
			
			Parent.Parent.SwitchToDocument();
		}
		
		public function OnClickCancel():void {
			Parent.Parent.SwitchToDocument();
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
		
	}
}