package UI
{
	import mx.collections.ArrayCollection;
	import mx.rpc.remoting.RemoteObject;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.events.FaultEvent;
	import mx.controls.Alert;
	import Domain.Documenttype;
	import Domain.DataMapperRegistry;
	import Domain.States;
	import Domain.Countys;
	import mx.rpc.Responder;
	
	[Bindable]
	public class AppModel
	{
		public var workflowState:int;
		
		public static const WORKFLOWSTATE_DOCUMENT_LIST:int = 0;
		public static const WORKFLOWSTATE_DOCUMENT_EDIT:int = 1;
		public static const WORKFLOWSTATE_PARTICIPANT_EDIT:int = 2;

		public static const WEBORB_SERVICE_NAME:String = "BaseService";
		
		public static var service:RemoteObject;

		public var documentTypes:ArrayCollection;
		public var states:ArrayCollection = new ArrayCollection();
		
		public static var storageBaseUrl:String = "";
		public static var uploaderUrl:String = "";
		
		public var initError:Boolean = false;
		
		public var initedStates:int = 0;

		private var countys:ArrayCollection;
		private var isCountysInited:Boolean = false;
		
		public function AppModel():void {

			documentTypes = new ArrayCollection(DataMapperRegistry.Instance.Documenttype.findAll().source);

			DataMapperRegistry.Instance.States.findAllAsync().addResponder(new Responder(onGetStatesOk, onFault));

/*   			DataMapperRegistry.Instance.Countys.findAllAsync().addResponder(
				new Responder(
					function(countyList:Array):void {
						countys = new ArrayCollection(countyList);
						var county:Countys = new Countys();
						county.OBJECTID = 0;
						county.NAME = " ";
						countys.addItemAt(county, 0);
						initedStates = 51;
					},
					onFault
				)
			)
 */		}
		
		private function onGetStatesOk(stateList:Array):void {

			states.source = new ArrayCollection(stateList).source;

// states loading
			if (true) {
				initedStates = states.length;
				return;
			} else {
	  			for each (var usState:States in states) {
		  			DataMapperRegistry.Instance.Countys.getByStateId(usState.STATE_ID,
						new Responder(
							function(countyList:Array):void {
								initedStates += 1;
								usState.countys = new ArrayCollection(countyList);
								usState.countys.source.sortOn("NAME");
								var county:Countys = new Countys();
								county.OBJECTID = 0;
								county.NAME = " ";
								usState.countys.addItemAt(county, 0);
							},
							onFault
						)
					)
				}
			}
		}
					
		private function initStates():void {
			
			countys.source.sortOn("STATE_ID");

			var prevState:States = new States();

			for each (var county:Countys in countys ) {

				var state:States;

				if (prevState.STATE_ID == county.STATE_ID) {
					state = prevState;
				} else {
					state = getStateById(county.STATE_ID);
				}

				if (state != null) {
					state.countys.addItem(county);
				}
			}
		}
		
		private function getStateById(id:int):States {
			
			for each (var state:States in states) {
				if (state.STATE_ID == id) {
					return state;
				}
			}
			
			return null;
		}
		
		private function onFault(event:FaultEvent):void {
			Alert.show(event.fault.message, "Init error");
			initError = true;
		}
			
	}
}