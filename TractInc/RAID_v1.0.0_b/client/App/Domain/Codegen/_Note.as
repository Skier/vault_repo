
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _Note extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _noteId: int;
      
        protected var _relatedItemId: int;
      
        protected var _itemType: String;
      
        protected var _posted: Date;
      
        protected var _noteText: String;
      
        // parent tables
        internal var _relatedUser: User
            = new User()
        ;
      
            public function get  NoteId(): int
            {
              return _noteId;
            }

            public function set  NoteId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _noteId = value;
            }
          
            public function get  RelatedItemId(): int
            {
              return _relatedItemId;
            }

            public function set  RelatedItemId(value:int):void
            {
            
            _relatedItemId = value;
            }
          
            public function get  ItemType(): String
            {
              return _itemType;
            }

            public function set  ItemType(value:String):void
            {
            
            _itemType = value;
            }
          
            public function get  SenderId(): int
            {
            

                  if(_relatedUser != null)
                  return _relatedUser.UserId;

                
            
            
            return undefined;
            }
            protected function set SenderId(value:int):void
            {

            

                  if(_relatedUser == null)
                      _relatedUser = new User();

                  _relatedUser.UserId = value;
                
            
            }
          
            public function get  Posted(): Date
            {
              return _posted;
            }

            public function set  Posted(value:Date):void
            {
            
            _posted = value;
            }
          
            public function get  NoteText(): String
            {
              return _noteText;
            }

            public function set  NoteText(value:String):void
            {
            
            _noteText = value;
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
          
            
            if(RelatedUser != null)
              RelatedUser.onChildChanged(this);
          
        }
      
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:Note = new Note();
          
          
            object.NoteId = this.NoteId;
          
            object.RelatedItemId = this.RelatedItemId;
          
            object.ItemType = this.ItemType;
          
            object.SenderId = this.SenderId;
          
            object.Posted = this.Posted;
          
            object.NoteText = this.NoteText;
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
        
        public override function applyFields(object:Object):void
        {
        
                if(!IsPrimaryKeyInitialized)
                  NoteId = object["NoteId"];
              RelatedItemId = object["RelatedItemId"];
              ItemType = object["ItemType"];
              Posted = object["Posted"];
              NoteText = object["NoteText"];
              RelatedUser =
          object.RelatedUser;
        
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.Note;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.Note." 
            
              + NoteId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    