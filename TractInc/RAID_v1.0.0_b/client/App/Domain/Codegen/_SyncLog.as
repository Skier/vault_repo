
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _SyncLog extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _syncLogId: int;
      
        protected var _assetId: int;
      
        protected var _deviceId: String;
      
            public function get  SyncLogId(): int
            {
              return _syncLogId;
            }

            public function set  SyncLogId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _syncLogId = value;
            }
          
            public function get  AssetId(): int
            {
              return _assetId;
            }

            public function set  AssetId(value:int):void
            {
            
            _assetId = value;
            }
          
            public function get  DeviceId(): String
            {
              return _deviceId;
            }

            public function set  DeviceId(value:String):void
            {
            
            _deviceId = value;
            }
          
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:SyncLog = new SyncLog();
          
          
            object.SyncLogId = this.SyncLogId;
          
            object.AssetId = this.AssetId;
          
            object.DeviceId = this.DeviceId;
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
        
        public override function applyFields(object:Object):void
        {
        
                if(!IsPrimaryKeyInitialized)
                  SyncLogId = object["SyncLogId"];
              AssetId = object["AssetId"];
              DeviceId = object["DeviceId"];
              
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.SyncLog;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.SyncLog." 
            
              + SyncLogId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    