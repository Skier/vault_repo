package src.common
{
	import flash.net.SharedObject;
	
	import mx.collections.ArrayCollection;
	import mx.utils.UIDUtil;
	
	import src.deedplotter.domain.Tract;
	import src.deedplotter.domain.User;
	import flash.net.registerClassAlias;
	import src.deedplotter.domain.Document;

	[Bindable]
	public class UserTractRegistry
	{
		private static const BASE_SO_NAME:String = "_TRACT_REGISTRY";

		public static function hasUser(userName:String):Boolean 
		{
			var so:SharedObject = SharedObject.getLocal(userName + BASE_SO_NAME);

			return so.data.user != null;
		}
		
		private var so:SharedObject;

		private var deviceId:String;

		public var TractList:ArrayCollection;

		public var ChangedTracts:ArrayCollection;

		public var NewTracts:ArrayCollection;
		
		
		public function UserTractRegistry(userName:String):void {
			
			so = SharedObject.getLocal(userName + BASE_SO_NAME);

            registerClassAlias("src.deedplotter.domain.Tract", Tract);
            registerClassAlias("src.deedplotter.domain.Document", Document);

            if (so.data.TractList != null){
                TractList = ArrayCollection(so.data.TractList);
            } else{
                TractList = new ArrayCollection();
            }

            user = User(so.data.user);

            DeviceId = String(so.data.DeviceId);

            if (so.data.ChangedTracts != null){
                ChangedTracts = ArrayCollection(so.data.ChangedTracts);
            } else{
                ChangedTracts = new ArrayCollection();
            }

            if (so.data.NewTracts != null){
                NewTracts = ArrayCollection(so.data.NewTracts);
            } else{
                NewTracts = new ArrayCollection();
            }

		}
		
		private var _user:User;
		public function get user():User { return _user; }
		public function set user(user:User):void 
		{
			_user = user;
			so.data.user = user;
			so.flush();
		}
		
		public function Save():void {
			so.data.user = user;
			so.data.DeviceId = deviceId;
			so.data.NewTracts = NewTracts;
            so.data.TractList = TractList;
			so.data.ChangedTracts = ChangedTracts;
			so.flush();
		}
		
		public function get DeviceId():String {
			return deviceId;
		}
		
		public function set DeviceId(value:String):void {
			deviceId = value;
			so.data.DeviceId = value;
			so.flush();
		}
		
		public function GetTract(uid:String):Tract {
			var so:SharedObject = SharedObject.getLocal(user.Login + "_" + uid);
			return Tract(so.data.tract);
		}
		
		public function AddTract(tract:Tract, addToList:Boolean):void 
		{
			if (!tract)
				return;

			if (NewTracts.length < user.NewTracts || user.NewTracts == 0) {
				
				if (!tract.Uid) {
					tract.Uid = UIDUtil.createUID().toUpperCase();
				}
				
				var tractSO:SharedObject = SharedObject.getLocal(user.Login + "_" + tract.Uid);
	            tractSO.data.tract = tract;
				tractSO.flush();
	            
	            tract.IsDirty = false;

	            var record:Object = {Uid:tract.Uid, Description:tract.Description};
	            TractList.addItem(record);
	            
	            if (addToList) {
		            NewTracts.addItem(tract.Uid);
	            }
	            
				so.data.NewTracts = NewTracts;
	            so.data.TractList = TractList;
	            so.flush();
			} else 
			{
				throw new Error("You cannot create more than " + user.NewTracts + " tracts. Make sync with server to resolve this problem." );
			}
			
		}
		
		public function SaveTract(tract:Tract, addToList:Boolean):void {

			if (!tract) {
				return;
			}

			var tractSO:SharedObject = SharedObject.getLocal(user.Login + "_" + tract.Uid);
            tractSO.data.tract = tract;
			tractSO.flush();

            tract.IsDirty = false;

            var record:Object = GetTractRecordByUid(tract.Uid)
            record.Description = tract.Description;
            
            so.data.TractList = TractList;

			if ( !isTractInCollection(tract, NewTracts) && 
			     !isTractInCollection(tract, ChangedTracts) && addToList )
			{
				ChangedTracts.addItem(tract.Uid);
			}

            so.flush();

		}
		
		public function GetTractRecordByUid(uid:String):Object
		{
			for each (var record:Object in TractList)
			{
				if ( record.Uid.toUpperCase() == uid.toUpperCase())
				{
					return record;
				}
			}
			
			return null;
		}
		
		public function isTractInCollection(tract:Tract, collection:ArrayCollection):Boolean
		{
			for each (var collectionItemUid:String in collection)
			{
				if ( collectionItemUid.toUpperCase() == tract.Uid.toUpperCase() )
				{
					return true;
				}
			}

			return false;
		}

		public function ClearChanges():void {

			NewTracts.removeAll();
			ChangedTracts.removeAll();

			so.data.NewTracts = NewTracts;
			so.data.ChangedTracts = ChangedTracts;
			so.flush();

		}
		
		public function ClearAll():void {
			
			var tractSO:SharedObject;
			
			for each (var record:Object in TractList) {
				tractSO = SharedObject.getLocal(user.Login + "_" + record.Uid);
	            tractSO.clear();
				tractSO.flush();
			}
			
            if (TractList){
                TractList.removeAll();
            }

            user = new User();

            DeviceId = "";

            if (ChangedTracts){
                ChangedTracts.removeAll();
            }

            if (NewTracts){
                NewTracts.removeAll();
            }
			
			so.clear();
			so.flush();
		
		}
		
	}
}