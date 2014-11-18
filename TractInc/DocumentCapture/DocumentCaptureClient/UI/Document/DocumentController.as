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
	import mx.events.CollectionEvent;
	import mx.events.CollectionEventKind;
	
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
		
		public function Init(document:Document):void {
			
			SetViewState(DocumentModel.VIEWSTATE_WAITING);

			Model.CurrentDocument = document;

			Model.SellersDetailed.addEventListener(CollectionEvent.COLLECTION_CHANGE, onSellersDetailedChanged);
			Model.BuyersDetailed.addEventListener(CollectionEvent.COLLECTION_CHANGE, onBuyersDetailedChanged);
			Model.Tracts.addEventListener(CollectionEvent.COLLECTION_CHANGE, onTractsChanged);
			
			initType();

			initParticipants();
			initTracts();
			
			if (document.DocID == 0) {
				View.createView.DocumentType.selectedIndex = 0;
				View.createView.UsState.selectedIndex = 0;
				Model.Counties.removeAll();
			} else {
				View.focusManager.setFocus(View.editView.participants.sellerAsNamed);
			}
			
			initSearch();

		}
		
		public function OnClickCreate():void
		{
			if (!View.createView.IsValid()) {
				return;
			}
			
 			Model.CurrentDocument.DateFiled = View.createView.dateFiled.getDate();		
			Model.CurrentDocument.IsNew = false;
			Model.CurrentDocument.save(false, new Responder(Init, onFault));
		}
		
		public function OnClickComplete():void
		{
			if (!View.IsValid()){
				return;
			}

 			Model.CurrentDocument.DateSigned = View.editView.dateSigned.getDate();

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

 			Model.CurrentDocument.DateSigned = View.editView.dateSigned.getDate();

			needToClose = false;

			SaveCurrentDocument();
		}

		public function CloseParticipant():void {
			SetViewState(DocumentModel.VIEWSTATE_DOCUMENT_EDIT);
		}

		public function CloseTract():void {
			SetViewState(DocumentModel.VIEWSTATE_DOCUMENT_EDIT);
		}

		private function SaveCurrentDocument():void {

			SetViewState(DocumentModel.VIEWSTATE_WAITING);

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

			SetViewState(DocumentModel.VIEWSTATE_DOCUMENT_EDIT);

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
			var usState:States = View.createView.UsState.selectedItem as States;
			
			if (Model.Counties.length > 0) {
				View.createView.County.selectedIndex = 0;
			}
			
			if (usState != null && View.createView.UsState.selectedIndex > 0) {
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

			if ((		doc.DocTypeId != 0
					&&  doc.State != null && StringUtil.trim(doc.State) != ""
					&&  doc.County != null && StringUtil.trim(doc.County) != ""
					&&  View.createView.dateFiled.text != "" 
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
			var tract:Tract = View.editView.tracts.dgTracts.selectedItem as Tract;
			tract.isNew = false;
			editTract(tract);
		}
		
		private function editTract(tract:Tract):void {
			View.tractView.parentCollection = Model.Tracts;
			View.tractView.Controller.Init(tract);
			SetViewState(DocumentModel.VIEWSTATE_TRACT_EDIT);
//			var popupWin:TractView = TractView(PopUpManager.createPopUp(View, TractView, true));
//			popupWin.parentCollection = Model.Tracts;
//			popupWin.Controller.Init(tract);
		}

		public function OnClickRemoveTract():void {
			var tract:Tract = View.editView.tracts.dgTracts.selectedItem as Tract;
			var idx:int = View.editView.tracts.dgTracts.selectedIndex;
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
			SwitchToParticipant( new ParticipantModel(participant, Model.SellersDetailed));
		}
		
		public function OnClickAddBuyerDetailed():void {
			var participant:Participant = new Participant();
			participant.DocID = Model.CurrentDocument.DocID;
			participant.DocRoleID = Model.BuyerRole.DocRoleID;
			participant.ParentID = Model.BuyerAsNamed.ParticipantID;
			participant.isNew = true;
			participant.isSeller = false;
			SwitchToParticipant(new ParticipantModel(participant, Model.BuyersDetailed));
		}
		
		public function OnClickEditSellerDetailed():void {
			var model:ParticipantModel = View.editView.participants.dgSellersDetailed.selectedItem as ParticipantModel;
			model.participant.isNew = false;
			model.participant.isSeller = true;
			SwitchToParticipant(model);
		}
		
		public function OnClickEditBuyerDetailed():void {
			var model:ParticipantModel = View.editView.participants.dgBuyersDetailed.selectedItem as ParticipantModel;
			model.participant.isNew = false;
			model.participant.isSeller = false;
			SwitchToParticipant(model);
		}
		
		public function OnClickRemoveSellerDetailed():void {
			var sellerModel:ParticipantModel = View.editView.participants.dgSellersDetailed.selectedItem as ParticipantModel;
			var idx:int = Model.SellersDetailed.getItemIndex(sellerModel);
			sellerModel.remove();
			if (idx != -1){
				Model.SellersDetailed.removeItemAt(idx);
			}
		}
		
		public function OnClickRemoveBuyerDetailed():void {
			var buyerModel:ParticipantModel = View.editView.participants.dgBuyersDetailed.selectedItem as ParticipantModel;
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
			
			if (View.createView.DocumentType.selectedIndex != 0){
				Model.CurrentDocumentType = Documenttype(View.createView.DocumentType.selectedItem);
				doc.DocTypeId = Model.CurrentDocumentType.DocTypeID;
			} else {
				doc.DocTypeId = 0;
			}
			
			var usState:States = View.createView.UsState.selectedItem as States;
			if (usState != null) {
				doc.State = usState.STATE_NAME;
			}
			
			var county:Countys = View.createView.County.selectedItem as Countys;
			if (county!= null) {
				doc.County = county.NAME;
			}

 			doc.DocumentNo = View.createView.DocumentNo.text.toUpperCase();
			doc.Pg = View.createView.Page.text.toUpperCase();
			doc.Vol = View.createView.Volume.text.toUpperCase();

			doc.ResearchNote = View.editView.ResearchNote.text.toUpperCase();
		}
		
		private function initSearch():void
		{
			View.createView.documentSearch.Controller.Template = Model.CurrentDocument;
		}

		private function initTracts():void {
			
			Model.Tracts.removeAll();
			Model.IsTractsInited = false;
			
			var asyncTokenSeller:AsyncToken = DataMapperRegistry.Instance.Tract.getByDocID(Model.CurrentDocument.DocID);
			asyncTokenSeller.addResponder(new Responder(
                function(tractsList:Array):void {
                	Model.Tracts = new ArrayCollection(tractsList);
                	Model.IsTractsInited = true;
                	trySwitch();
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
			
			Model.IsBuyerAsNamedInited = false;
			Model.IsSellerAsNamedInited = false;
			Model.IsBuyersDetailedInited = false;
			Model.IsSellersDetailedInited = false;
			
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
                	Model.IsSellersDetailedInited = true;
                	trySwitch();
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
                	Model.IsBuyersDetailedInited = true;
                	trySwitch();
                }, 
                onFault
			));
		}
		
		private function initSellerAsNamedReservations():void {
			var asyncToken:AsyncToken = DataMapperRegistry.Instance.Participantreservation.getByParticipantId(Model.SellerAsNamed.ParticipantID);
			asyncToken.addResponder(new Responder(
                function(resultList:Array):void {
                	Model.SellerAsNamedReservations = new ArrayCollection(resultList);
                	Model.IsSellerAsNamedInited = true;
                	trySwitch();
                }, 
                onFault
			));
		}

		private function initBuyerAsNamedReservations():void {
			var asyncToken:AsyncToken = DataMapperRegistry.Instance.Participantreservation.getByParticipantId(Model.BuyerAsNamed.ParticipantID);
			asyncToken.addResponder(new Responder(
                function(resultList:Array):void {
                	Model.BuyerAsNamedReservations = new ArrayCollection(resultList);
                	Model.IsBuyerAsNamedInited = true;
                	trySwitch();
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
		
		private function initType():void {
			for each (var type:Documenttype in Model.Types) {
				if (Model.CurrentDocument.DocTypeId == type.DocTypeID) {
					Model.CurrentDocumentType = type;
					return;
				}
			}
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
						View.createView.DocumentType.selectedIndex = 0;
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
						View.createView.UsState.selectedIndex = 0;
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
			View.createView.County.selectedIndex = 0;

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
			Parent.CloseDocument();
		}

		private function onSellersDetailedChanged(event:CollectionEvent):void {
			if (event.kind == CollectionEventKind.ADD) {
				var lastGridIndex:int = Model.SellersDetailed.length - 1;
				View.editView.participants.dgSellersDetailed.selectedIndex = lastGridIndex;
				View.editView.participants.dgSellersDetailed.scrollToIndex(lastGridIndex);
			}
		}
		
		private function onBuyersDetailedChanged(event:CollectionEvent):void {
			if (event.kind == CollectionEventKind.ADD) {
				var lastGridIndex:int = Model.BuyersDetailed.length - 1;
				View.editView.participants.dgBuyersDetailed.selectedIndex = lastGridIndex;
				View.editView.participants.dgBuyersDetailed.scrollToIndex(lastGridIndex);
			}
		}
		
		private function onTractsChanged(event:CollectionEvent):void {
			if (event.kind == CollectionEventKind.ADD) {
				var lastGridIndex:int = Model.Tracts.length - 1;
				View.editView.tracts.dgTracts.selectedIndex = lastGridIndex;
				View.editView.tracts.dgTracts.scrollToIndex(lastGridIndex);
			}
		}
		
		private function trySwitch():void {
			if (Model.IsBuyerAsNamedInited && Model.IsBuyersDetailedInited 
					&& Model.IsSellerAsNamedInited && Model.IsSellersDetailedInited
					&& Model.IsTractsInited) {
				if (Model.CurrentDocument.DocID == 0) {
					SetViewState(DocumentModel.VIEWSTATE_DOCUMENT_CREATE);
				} else {
					SetViewState(DocumentModel.VIEWSTATE_DOCUMENT_EDIT);
				}
			}
		}
		
		private function SwitchToParticipant(model:ParticipantModel):void {
			View.participantView.Controller.Init(model);
			SetViewState(DocumentModel.VIEWSTATE_PARTICIPANT_EDIT);
		}
		
        private function SetViewState(state:int):void 
        {
            switch (state)
            {
                case DocumentModel.VIEWSTATE_DOCUMENT_CREATE :
                    View.documentViewStack.selectedChild = View.createView;
                    break;
                                                                
                case DocumentModel.VIEWSTATE_DOCUMENT_EDIT :
                    View.documentViewStack.selectedChild = View.editView;
                    break;
                        
                case DocumentModel.VIEWSTATE_PARTICIPANT_EDIT :
                    View.documentViewStack.selectedChild = View.participantView;
                    break;
                        
                case DocumentModel.VIEWSTATE_TRACT_EDIT :
                    View.documentViewStack.selectedChild = View.tractView;
                    break;
                        
                case DocumentModel.VIEWSTATE_WAITING :
                    View.documentViewStack.selectedChild = View.waitingView;
                    break;
                        
                default :
                    throw new Error("Workflow state is invalid");
            }
        }        
		
	}
}