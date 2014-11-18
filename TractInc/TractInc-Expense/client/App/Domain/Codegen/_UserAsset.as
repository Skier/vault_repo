
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _UserAsset extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _userAssetId: int;
      
        protected var _deleted: Boolean;
      
        // parent tables
        internal var _relatedAsset: Asset
            = new Asset()
        ;
      
        // parent tables
        internal var _relatedUser: User
            = new User()
        ;
      
            public function get  UserAssetId(): int
            {
              return _userAssetId;
            }

            public function set  UserAssetId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _userAssetId = value;
            }
          
            public function get  UserId(): int
            {
            

                  if(_relatedUser != null)
                  return _relatedUser.UserId;

                
            
            
            return undefined;
            }
            protected function set UserId(value:int):void
            {

            

                  if(_relatedUser == null)
                      _relatedUser = new User();

                  _relatedUser.UserId = value;
                
            
            }
          
            public function get  AssetId(): int
            {
            

                  if(_relatedAsset != null)
                  return _relatedAsset.AssetId;

                
            
            
            return undefined;
            }
            protected function set AssetId(value:int):void
            {

            

                  if(_relatedAsset == null)
                      _relatedAsset = new Asset();

                  _relatedAsset.AssetId = value;
                
            
            }
          
            public function get  Deleted(): Boolean
            {
              return _deleted;
            }

            public function set  Deleted(value:Boolean):void
            {
            
            _deleted = value;
            }
          
        
        public function get RelatedAsset():Asset
        {
        if(IsLoaded  && 
        
        !(_relatedAsset.IsLoaded || _relatedAsset.IsLoading))
        {
          _relatedAsset = DataMapperRegistry.Instance.Asset.load(_relatedAsset);
          
          onParentChanged(_relatedAsset);
        }

        return _relatedAsset;
        }
        public function set RelatedAsset(value:Asset):void
        {
          _relatedAsset = Asset(IdentityMap.register( value ));

          onParentChanged(_relatedAsset);
        }
      
        
        public function get RelatedUser():User
        {
        if(IsLoaded  && 
        
        !(_relatedUser.IsLoaded || _relatedUser.IsLoading))
        {
          _relatedUser = DataMapperRegistry.Instance.User.load(_relatedUser);
          
          onParentChanged(_relatedUser);
        }

        return _relatedUser;
        }
        public function set RelatedUser(value:User):void
        {
          _relatedUser = User(IdentityMap.register( value ));

          onParentChanged(_relatedUser);
        }
      
        protected override function onDirtyChanged():void
        {
          
            
            if(RelatedAsset != null)
              RelatedAsset.onChildChanged(this);
          
            
            if(RelatedUser != null)
              RelatedUser.onChildChanged(this);
          
        }
      
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:UserAsset = new UserAsset();
          
          
            object.UserAssetId = this.UserAssetId;
          
            object.UserId = this.UserId;
          
            object.AssetId = this.AssetId;
          
            object.Deleted = this.Deleted;
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
        
        public override function applyFields(object:Object):void
        {
        
                if(!IsPrimaryKeyInitialized)
                  UserAssetId = object["UserAssetId"];
              Deleted = object["Deleted"];
              RelatedAsset =
          object.RelatedAsset;
        RelatedUser =
          object.RelatedUser;
        
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.UserAsset;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.UserAsset." 
            
              + UserAssetId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    