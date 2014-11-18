package UI.Document
{
	import Domain.DataMapperRegistry;
	import Domain.Document;
	import Domain.Documenttype;
	
	import UI.AppController;
	import UI.AppView;
	
	import mx.collections.ArrayCollection;
	import mx.controls.Alert;
	import mx.rpc.AsyncToken;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.states.State;
	import mx.utils.StringUtil;
	import Domain.Participant;
	import mx.rpc.events.ResultEvent;
	import Domain.Participantrole;
	import Domain.Participantreservation;
	import Domain.Tract;
	import UI.Document.Participant.ParticipantReservationView;
	import UI.Document.Tract.TractView;
	import mx.managers.PopUpManager;
	import Domain.States;
	import Domain.Countys;
	import UI.Document.Participant.ParticipantModel;
	import UI.AppModel;
	import mx.events.ValidationResultEvent;
	
	[Bindable]
	public class DocumentController
	{
		public var View:DocumentView;
		public var Model:DocumentModel;
		public var typ:Documenttype;
		public var Parent:AppController;
		
		private var needToClose:Boolean;
		
		public function DocumentController(view:DocumentView, parent:AppController):void 
		{
			View = view;
			Parent = parent;
			Model = new DocumentModel();

			getTypes();
			getStates();
		}
		
		public function OnShow():void
		{
			initSearch();
		}

		public function Init():void {

			initParticipants();
			initTracts();
			
			if (Model.CurrentDocument.DocID == 0) {
				View.keyFields.DocumentType.selectedIndex = 0;
				View.keyFields.UsState.selectedIndex = 0;
				Model.Counties.removeAll();
			}

//			if (Model.CurrentDocument.DateFiled == null) 
//				Model.CurrentDocument.DateFiled = new Date();

//			if (Model.CurrentDocument.DateSigned == null) 
//				Model.CurrentDocument.DateSigned = new Date();
			
		}
		
		public function Match(document:Document):void {

			Model.CurrentDocument = document;
			
			Init();

			View.focusManager.setFocus(View.participants.sellerAsNamed);
		}
		
		public function OnClickCreate():void
		{
			if (!View.keyFields.IsValid()) {
				return;
			}
			
/* 			Model.CurrentDocument.DateFiled = new Date(
				int(StringUtil.trim(View.keyFields.year.text)),
				int(StringUtil.trim(View.keyFields.month.text)) - 1,
				int(StringUtil.trim(View.keyFields.date.text)) );
 */	
 
 			Model.CurrentDocument.DateFiled = View.keyFields.dateFiled.getDate();		
			Model.CurrentDocument.IsNew = false;
			Model.CurrentDocument.save();
			Init();
		}

		public function OnClickComplete():void
		{
			if (!View.IsValid()){
				return;
			}

 			Model.CurrentDocument.DateSigned = View.dateSigned.getDate();

			needToClose = true;

			SaveCurrentDocument();
		}

		public function OnClickCancel():void
		{
			Parent.CloseDocument();
		}

		public function OnClickApply():void
		{
			if (!View.IsValid()){
				return;
			}

 			Model.CurrentDocument.DateSigned = View.dateSigned.getDate();

			needToClose = false;

			SaveCurrentDocument();
		}

		private function SaveCurrentDocument():void {
			Model.CurrentDocument.IsNew = false;
			Model.CurrentDocument.save(false, new Responder(onDocSavedOk, onFault));
		}

		private function onDocSavedOk(doc:Document):void {

			Model.SellerAsNamed.save(false, new Responder(onSellerAsNamedSavedOk, onFault));
			Model.BuyerAsNamed.save(false, new Responder(onBuyerAsNamedSavedOk, onFault));

			for each (var tract:Tract in Model.Tracts){
				tract.DocID = Model.CurrentDocument.DocID;
				tract.save();
			}
			
			if (needToClose) {

			Parent.CloseDocument();

			}
 		}

		private function onSellerAsNamedSavedOk(seller:Participant):void {

			var reservation:Participantreservation;
			for each (reservation in Model.SellerAsNamedReservations) {
				reservation.ParticipantID = seller.ParticipantID;
				reservation.save();
			}
			
 		}

		private function onBuyerAsNamedSavedOk(buyer:Participant):void {

			var reservation:Participantreservation;
			for each (reservation in Model.BuyerAsNamedReservations) {
				reservation.ParticipantID = buyer.ParticipantID;
				reservation.save();
			}
			
 		}

		public function OnKeyFieldsChanged():void
		{
			propagateDoc();
			initSearch();
			
			Model.IsKeysFilled = isKeysFilled();
		}

		public function OnStatesChanged():void {
			var usState:States = View.keyFields.UsState.selectedItem as States;
			
			if (Model.Counties.length > 0) {
				View.keyFields.County.selectedIndex = 0;
			}
			
			if (usState != null && View.keyFields.UsState.selectedIndex > 0) {
				getCountys(usState);
			} else {
				Model.Counties.removeAll();
			}
			
			OnKeyFieldsChanged();
		}

		public function OnImageLoaded():void {
			Alert.show("Image uploaded Ok", "Upload");
		}

		public function isKeysFilled():Boolean {
			
			var doc:Document = Model.CurrentDocument;

/* 			var year:String = View.keyFields.year.text;
			var month:String = View.keyFields.month.text;
			var date:String = View.keyFields.date.text;
 */
			if ((		doc.DocTypeId != 0
					&&  doc.State != null && StringUtil.trim(doc.State) != ""
					&&  doc.County != null && StringUtil.trim(doc.County) != ""
					&&  View.keyFields.dateFiled.text != "" 
					) && (( doc.DocumentNo != null && StringUtil.trim(doc.DocumentNo) != "" )
							||((doc.Vol != null && StringUtil.trim(doc.Vol) != "")
								&&(doc.Pg != null && StringUtil.trim(doc.Pg) != "")))) 
			{
				return true;
			} else {
				return false;
			}
		}
		
		public function OnClickAddTract():void {
			var tract:Tract = new Tract();
			tract.isNew = true;
			editTract(tract);
		}
		
		public function OnClickEditTract():void {
			var tract:Tract = View.dgTracts.selectedItem as Tract;
			tract.isNew = false;
			editTract(tract);
		}
		
		private function editTract(tract:Tract):void {
			var popupWin:TractView = TractView(PopUpManager.createPopUp(View, TractView, true));
			popupWin.parentCollection = Model.Tracts;
			popupWin.Controller.Init(tract);
		}

		public function OnClickRemoveTract():void {
			var tract:Tract = View.dgTracts.selectedItem as Tract;
			var idx:int = View.dgTracts.selectedIndex;
			tract.remove();
			Model.Tracts.removeItemAt(idx);
		}
		
		public function OnClickAddSellerDetailed():void {
			var participant:Participant = new Participant();
			participant.DocID = Model.CurrentDocument.DocID;
			participant.DocRoleID = Model.SellerRole.DocRoleID;
			participant.ParentID = Model.SellerAsNamed.ParticipantID;
			participant.isNew = true;
			participant.isSeller = true;
			Parent.SwitchToParticipant( new ParticipantModel(participant, Model.SellersDetailed));
		}
		
		public function OnClickAddBuyerDetailed():void {
			var participant:Participant = new Participant();
			participant.DocID = Model.CurrentDocument.DocID;
			participant.DocRoleID = Model.BuyerRole.DocRoleID;
			participant.ParentID = Model.BuyerAsNamed.ParticipantID;
			participant.isNew = true;
			participant.isSeller = false;
			Parent.SwitchToParticipant(new ParticipantModel(participant, Model.BuyersDetailed));
		}
		
		public function OnClickEditSellerDetailed():void {
			var model:ParticipantModel = View.participants.dgSellersDetailed.selectedItem as ParticipantModel;
			model.participant.isNew = false;
			model.participant.isSeller = true;
			Parent.SwitchToParticipant(model);
		}
		
		public function OnClickEditBuyerDetailed():void {
			var model:ParticipantModel = View.participants.dgBuyersDetailed.selectedItem as ParticipantModel;
			model.participant.isNew = false;
			model.participant.isSeller = false;
			Parent.SwitchToParticipant(model);
		}
		
		public function OnClickRemoveSellerDetailed():void {
			var sellerModel:ParticipantModel = View.participants.dgSellersDetailed.selectedItem as ParticipantModel;
			var idx:int = Model.SellersDetailed.getItemIndex(sellerModel);
			sellerModel.remove();
			if (idx != -1){
				Model.SellersDetailed.removeItemAt(idx);
			}
		}
		
		public function OnClickRemoveBuyerDetailed():void {
			var buyerModel:ParticipantModel = View.participants.dgBuyersDetailed.selectedItem as ParticipantModel;
			var idx:int = Model.BuyersDetailed.getItemIndex(buyerModel);
			buyerModel.remove();
			if (idx != -1){
				Model.BuyersDetailed.removeItemAt(idx);
			}
		}

 		public function OnClickSellerAsNamedReservations():void {
			var popupWin:ReservationsView = ReservationsView(PopUpManager.createPopUp(View, ReservationsView, true));
			popupWin.reservations = Model.SellerAsNamedReservations;
		}
		
		private function propagateDoc():void{
			
			var doc:Document = Model.CurrentDocument;
			
			if (View.keyFields.DocumentType.selectedIndex != 0){
				Model.CurrentDocumentType = Documenttype(View.keyFields.DocumentType.selectedItem);
				doc.DocTypeId = Model.CurrentDocumentType.DocTypeID;
			} else {
				doc.DocTypeId = 0;
			}
			
			var usState:States = View.keyFields.UsState.selectedItem as States;
			if (usState != null) {
				doc.State = usState.STATE_NAME;
			}
			
			var county:Countys = View.keyFields.County.selectedItem as Countys;
			if (county!= null) {
				doc.County = county.NAME;
			}

 			doc.DocumentNo = View.keyFields.DocumentNo.text.toUpperCase();
			doc.Pg = View.keyFields.Page.text.toUpperCase();
			doc.Vol = View.keyFields.Volume.text.toUpperCase();

//			doc.DateSigned = View.DateSigned.selectedDate;
			doc.ResearchNote = View.ResearchNote.text.toUpperCase();
		}
		
		private function initSearch():void
		{
			View.keyFields.documentSearch.Controller.Template = Model.CurrentDocument;
		}

		private function initTracts():void {
			
			Model.Tracts.removeAll();
			
			var asyncTokenSeller:AsyncToken = DataMapperRegistry.Instance.Tract.getByDocID(Model.CurrentDocument.DocID);
			asyncTokenSeller.addResponder(new Responder(
                function(tractsList:Array):void {
                	Model.Tracts = new ArrayCollection(tractsList);
                }, 
                onFault
			));
 
		}
		
		private function initParticipants():void {

			Model.BuyerAsNamed = null;
			Model.BuyerAsNamedReservations.removeAll();
			Model.BuyerRole = null;
			Model.BuyersDetailed.removeAll();
			
			Model.SellerAsNamed = null;
			Model.SellerAsNamedReservations.removeAll();
			Model.SellerRole = null;
			Model.SellersDetailed.removeAll();
			
			var asyncTokenSeller:AsyncToken = DataMapperRegistry.Instance.Participantrole.getRoleByDocType(Model.CurrentDocument.DocTypeId, true);
			asyncTokenSeller.addResponder(new Responder(
                function(result:Participantrole):void {
                	Model.SellerRole = result;
                	initSellerAsNamed();
                	initSellersDetailed();
                }, 
                onFault
			));
 
			var asyncTokenBuyer:AsyncToken = DataMapperRegistry.Instance.Participantrole.getRoleByDocType(Model.CurrentDocument.DocTypeId, false);
			asyncTokenBuyer.addResponder(new Responder(
                function(result:Participantrole):void {
                	Model.BuyerRole = result;
                	initBuyerAsNamed();
                	initBuyersDetailed();
                }, 
                onFault
			));

 		}

		private function initSellerAsNamed():void {

			var asyncToken:AsyncToken = DataMapperRegistry.Instance.Participant.getRootParticipant(
													Model.CurrentDocument.DocID, Model.SellerRole.DocRoleID);
			asyncToken.addResponder(new Responder(
                function(seller:Participant):void {
                	Model.SellerAsNamed = seller;
//                	Model.SellerAsNamed.DocID = Model.CurrentDocument.DocID;
//               	Model.SellerAsNamed.DocRoleID = Model.SellerRole.DocRoleID;
//					Model.SellerAsNamed.ParentID = Model.SellerAsNamed.ParticipantID;
//                	Model.SellerAsNamed.save();
                	initSellerAsNamedReservations();
                }, 
                onFault
			));
		}

		private function initBuyerAsNamed():void {
			
			var asyncToken:AsyncToken = DataMapperRegistry.Instance.Participant.getRootParticipant(
													Model.CurrentDocument.DocID, Model.BuyerRole.DocRoleID);
			asyncToken.addResponder(new Responder(
                function(buyer:Participant):void {
                	Model.BuyerAsNamed = buyer;
//                	Model.BuyerAsNamed.DocID = Model.CurrentDocument.DocID;
//                	Model.BuyerAsNamed.DocRoleID = Model.BuyerRole.DocRoleID;
//					Model.BuyerAsNamed.ParentID = Model.BuyerAsNamed.ParticipantID;
//                	Model.BuyerAsNamed.save();
                	initBuyerAsNamedReservations();
                }, 
                onFault
			));

		}

		private function initSellersDetailed():void{

			var asyncToken:AsyncToken = DataMapperRegistry.Instance.Participant.getDetailParticipants(
													Model.CurrentDocument.DocID, Model.SellerRole.DocRoleID);
			asyncToken.addResponder(new Responder(
                function(resultList:Array):void {
                	for each (var participant:Participant in resultList) {
                		participant.isSeller = true;
                		Model.SellersDetailed.addItem(new ParticipantModel(participant, Model.SellersDetailed));
                	}
                }, 
                onFault
			));
		}

		private function initBuyersDetailed():void{

			var asyncToken:AsyncToken = DataMapperRegistry.Instance.Participant.getDetailParticipants(
													Model.CurrentDocument.DocID, Model.BuyerRole.DocRoleID);
			asyncToken.addResponder(new Responder(
                function(resultList:Array):void {
                	for each (var participant:Participant in resultList) {
                		participant.isSeller = false;
	                	Model.BuyersDetailed.addItem(new ParticipantModel(participant, Model.BuyersDetailed));
                	}
                }, 
                onFault
			));
		}
		
		private function initSellerAsNamedReservations():void {
			var asyncToken:AsyncToken = DataMapperRegistry.Instance.Participantreservation.getByParticipantId(Model.SellerAsNamed.ParticipantID);
			asyncToken.addResponder(new Responder(
                function(resultList:Array):void {
                	Model.SellerAsNamedReservations = new ArrayCollection(resultList);
                }, 
                onFault
			));
		}

		private function initBuyerAsNamedReservations():void {
			var asyncToken:AsyncToken = DataMapperRegistry.Instance.Participantreservation.getByParticipantId(Model.BuyerAsNamed.ParticipantID);
			asyncToken.addResponder(new Responder(
                function(resultList:Array):void {
                	Model.BuyerAsNamedReservations = new ArrayCollection(resultList);
                }, 
                onFault
			));
		}

		private function getDocumentTypes():void
		{
			var asyncToken:AsyncToken = DataMapperRegistry.Instance.Documenttype.findAllAsync();
			asyncToken.addResponder(new Responder(getDocumentTypesSuccess, onFault));
		}
		
		private function getDocumentTypesSuccess(typeList:Array):void
		{
			Model.Types = new ArrayCollection(typeList);
			var type:Documenttype = new Documenttype();
			type.DocTypeID = 0;
			type.Name = " ";
			Model.Types.addItemAt(type, 0);
		}
		
		private function getTypes():void {
			
/*    			Model.Types = Parent.Model.documentTypes;
			var type:Documenttype = new Documenttype();
			type.DocTypeID = 0;
			type.Name = " ";
			Model.Types.addItemAt(type, 0);
			View.keyFields.DocumentType.selectedIndex = 0;
 */			
   			DataMapperRegistry.Instance.Documenttype.findAllAsync().addResponder(
				new Responder(
					function(types:Array):void {
						Model.Types = new ArrayCollection(types);
						var type:Documenttype = new Documenttype();
						type.DocTypeID = 0;
						type.Name = " ";
						Model.Types.addItemAt(type, 0);
						View.keyFields.DocumentType.selectedIndex = 0;
					},
					onFault
				)
			)
		}
		
		private function getStates():void {

/*    			Model.States = Parent.Model.states;
			var state:States = new States();
			state.STATE_ID = 0;
			state.STATE_NAME = " ";
			Model.States.addItemAt(state, 0);
			View.keyFields.UsState.selectedIndex = 0;
 */

   			DataMapperRegistry.Instance.States.findAllAsync().addResponder(
				new Responder(
					function(states:Array):void {
						Model.States = new ArrayCollection(states);
						var state:States = new States();
						state.STATE_ID = 0;
						state.STATE_NAME = " ";
						Model.States.addItemAt(state, 0);
						View.keyFields.UsState.selectedIndex = 0;
					},
					onFault
				)
			)
		}
		
		private function getCountys(usState:States):void {

/*    			Model.Counties = View.keyFields.UsState.selectedItem.countys;
			View.keyFields.County.selectedIndex = 0;
 */			

			Model.Counties = usState.getCountys();
						var county:Countys = new Countys();
						county.OBJECTID = 0;
						county.NAME = " ";
						Model.Counties.addItemAt(county, 0);
			View.keyFields.County.selectedIndex = 0;

/*    			DataMapperRegistry.Instance.Countys.findBySTATE_ID(usState.STATE_ID,
				new Responder(
					function(countys:Array):void {
						Model.Counties = new ArrayCollection(countys);
						Model.Counties.source.sortOn("NAME");
						var county:Countys = new Countys();
						county.OBJECTID = 0;
						county.NAME = " ";
						Model.Counties.addItemAt(county, 0);
						View.keyFields.County.selectedIndex = 0;
					},
					onFault
				)
			)
 */		}
		
		private function onFault(faultEvent:FaultEvent):void
		{
			Alert.show(faultEvent.fault.message);
		}
		
	}
}