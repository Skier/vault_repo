package UI.Document.Participant
{
	import Domain.Participant;
	import mx.collections.ArrayCollection;
	import Domain.Participantaddress;
	import mx.rpc.events.FaultEvent;
	import Domain.DataMapperRegistry;
	import mx.rpc.Responder;
	import mx.controls.Alert;
	import Domain.Participantreservation;
	import Domain.Participantentityparty;
	
	[Bindable]
	public class ParticipantModel
	{
		public var participant:Participant;
		
		public var reservations:ArrayCollection;
		
//		public var addresses:ArrayCollection = new ArrayCollection();
		
		public var entityParts:ArrayCollection;
		
		//-- 
		public var mailingAddress:Participantaddress;
		public var phisicalAddress:Participantaddress;
		public var phisicalTheSameAsMailing:Boolean = false;
		//--
	
		public var parentCollection:ArrayCollection;
		
		public function ParticipantModel(participant:Participant, parentCollection:ArrayCollection):void {
			
			this.participant = participant;
			this.parentCollection = parentCollection;
			
			if (participant.ParticipantID == 0) {
				mailingAddress = new Participantaddress();
				mailingAddress.AddressTypeID = 0;
				
				phisicalAddress = new Participantaddress();
				phisicalAddress.AddressTypeID = 1;
				
				entityParts = new ArrayCollection();
				reservations = new ArrayCollection();
			} else {
				getAddresses();
				getParts();
				getReservations();
			}
		}
		
		public function save():void {

			participant.save(false, new Responder(onParticipantSavedOk, onFault));

		}
		
		public function remove():void {

			for each (var reservation:Participantreservation in reservations) {
				if (reservation.ParticipantID != 0) {
					reservation.remove();
				}
			}
			
			for each (var entityParty:Participantentityparty in entityParts) {
				if (entityParty.ParticipantID != 0) {
					entityParty.remove();
				}
			}
			
			if (mailingAddress.AddressID != 0) {
				mailingAddress.remove();
			}
			
			if (phisicalAddress.AddressID != 0) {
				phisicalAddress.remove();
			}
			
			if (participant.ParticipantID != 0) {
				participant.remove();
			}
		}
		
		private function onParticipantSavedOk(current:Participant):void {

			for each (var reservation:Participantreservation in reservations) {
				reservation.ParticipantID = current.ParticipantID;
				reservation.save();
			}
			
			for each (var entityParty:Participantentityparty in entityParts) {
				entityParty.ParticipantID = current.ParticipantID;
				entityParty.save();
			}
			
			mailingAddress.ParticipantlID = current.ParticipantID;
			mailingAddress.save();

			if (phisicalTheSameAsMailing) {
				phisicalAddress.Line1 = mailingAddress.Line1;
				phisicalAddress.Line2 = mailingAddress.Line2;
				phisicalAddress.City  = mailingAddress.City;
				phisicalAddress.State = mailingAddress.State;
				phisicalAddress.Zip   = mailingAddress.Zip;
			}
			
			phisicalAddress.ParticipantlID = current.ParticipantID;
			phisicalAddress.save();

			if (participant.isNew) {
				parentCollection.addItem(this);
			}

		}
		
		private function getAddresses():void {

			mailingAddress = null;
			phisicalAddress = null;

			DataMapperRegistry.Instance.Participantaddress.getByParticipantId(participant.ParticipantID,
				new Responder(onGetAddresses, onFault));
		}
		
		private function getParts():void {
			DataMapperRegistry.Instance.Participantentityparty.getByParticipantId(participant.ParticipantID,
				new Responder(
					function(parts:Array):void {
						entityParts = new ArrayCollection(parts);
					}, 
					onFault));
		}
		
		private function getReservations():void {
			DataMapperRegistry.Instance.Participantreservation.getByParticipantId(participant.ParticipantID,
				new Responder(
					function(reservationList:Array):void {
						reservations = new ArrayCollection(reservationList);
					}, 
					onFault));
		}
		
		private function onGetAddresses(addresses:Array):void {

			var list:ArrayCollection = new ArrayCollection(addresses);

			for each (var address:Participantaddress in list){
				if (address.AddressTypeID == 0) {
					mailingAddress = address;
				} else if (address.AddressTypeID == 1) {
					phisicalAddress = address;
				}
			}

			if (mailingAddress == null){
				mailingAddress = new Participantaddress();
				mailingAddress.AddressTypeID = 0;
			}

			if (phisicalAddress == null){
				phisicalAddress = new Participantaddress();
				phisicalAddress.AddressTypeID = 1;
			}
			
		}
		
		private function onFault(event:FaultEvent):void {
			Alert.show(event.fault.message, "Fault");
		}
	}
}