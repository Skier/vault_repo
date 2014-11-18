
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _LeaseEditHistory extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _editHistoryId: int;
      
        protected var _dateEdited: Date;
      
        protected var _status: String;
      
        // parent tables
        internal var _parentLease: Lease
            = new Lease()
        ;
      
        // parent tables
        internal var _parentUser: User
            = new User()
        ;
      
            public function get  EditHistoryId(): int
            {
              return _editHistoryId;
            }

            public function set  EditHistoryId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;
            
            _editHistoryId = value;
            }
          
            public function get  UserId(): int
            {
            

                  if(_parentUser != null)
                  return _parentUser.UserId;

                
            
            
            return undefined;
            }
            public function set UserId(value:int):void
            {

            

                  if(_parentUser == null)
                      _parentUser = new User();

                  _parentUser.UserId = value;
                
            
            }
          
            public function get  LeaseId(): int
            {
            

                  if(_parentLease != null)
                  return _parentLease.LeaseId;

                
            
            
            return undefined;
            }
            public function set LeaseId(value:int):void
            {

            

                  if(_parentLease == null)
                      _parentLease = new Lease();

                  _parentLease.LeaseId = value;
                
            
            }
          
            public function get  DateEdited(): Date
            {
              return _dateEdited;
            }

            public function set  DateEdited(value:Date):void
            {
            
            _dateEdited = value;
            }
          
            public function get  Status(): String
            {
              return _status;
            }

            public function set  Status(value:String):void
            {
            
            _status = value;
            }
          
        
        public function get ParentLease():Lease
        {
        if(IsLoaded  && 
        
        !(_parentLease.IsLoaded || _parentLease.IsLoading))
        {
          _parentLease = DataMapperRegistry.Instance.Lease.load(_parentLease);
          
          onParentChanged(_parentLease);
        }

        return _parentLease;
        }
        public function set ParentLease(value:Lease):void
        {
          _parentLease = Lease(IdentityMap.register( value ));

          onParentChanged(_parentLease);
        }
      
        
        public function get ParentUser():User
        {
        if(IsLoaded  && 
        
        !(_parentUser.IsLoaded || _parentUser.IsLoading))
        {
          _parentUser = DataMapperRegistry.Instance.User.load(_parentUser);
          
          onParentChanged(_parentUser);
        }

        return _parentUser;
        }
        public function set ParentUser(value:User):void
        {
          _parentUser = User(IdentityMap.register( value ));

          onParentChanged(_parentUser);
        }
      
        protected override function onDirtyChanged():void
        {
          
            
            if(ParentLease != null)
              ParentLease.onChildChanged(this);
          
            
            if(ParentUser != null)
              ParentUser.onChildChanged(this);
          
        }
      
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:LeaseEditHistory = new LeaseEditHistory();
          
          
            object.EditHistoryId = this.EditHistoryId;
          
            object.UserId = this.UserId;
          
            object.LeaseId = this.LeaseId;
          
            object.DateEdited = this.DateEdited;
          
            object.Status = this.Status;
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
        
        public override function applyFields(object:Object):void
        {
        EditHistoryId = object["EditHistoryId"];
        UserId = object["UserId"];
        LeaseId = object["LeaseId"];
        DateEdited = object["DateEdited"];
        Status = object["Status"];
        
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.LeaseEditHistory;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractInc.LeaseEditHistory." 
            
              + EditHistoryId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    