
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _Message extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _messageId: int;
      
        protected var _posted: Date;
      
        protected var _subject: String;
      
        protected var _body: String;
      
        protected var _isRead: Boolean;
      
        // parent tables
        internal var _relatedUser: User
            = new User()
        ;
      
        // parent tables
        internal var _relatedUser1: User
            = new User()
        ;
      
            public function get  MessageId(): int
            {
              return _messageId;
            }

            public function set  MessageId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _messageId = value;
            }
          
            public function get  SenderUserId(): int
            {
            

                  if(_relatedUser1 != null)
                  return _relatedUser1.UserId;

                
            
            
            return undefined;
            }
            protected function set SenderUserId(value:int):void
            {

            

                  if(_relatedUser1 == null)
                      _relatedUser1 = new User();

                  _relatedUser1.UserId = value;
                
            
            }
          
            public function get  ReceiverUserId(): int
            {
            

                  if(_relatedUser != null)
                  return _relatedUser.UserId;

                
            
            
            return undefined;
            }
            protected function set ReceiverUserId(value:int):void
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
          
            public function get  Subject(): String
            {
              return _subject;
            }

            public function set  Subject(value:String):void
            {
            
            _subject = value;
            }
          
            public function get  Body(): String
            {
              return _body;
            }

            public function set  Body(value:String):void
            {
            
            _body = value;
            }
          
            public function get  IsRead(): Boolean
            {
              return _isRead;
            }

            public function set  IsRead(value:Boolean):void
            {
            
            _isRead = value;
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
      
        
        public function get RelatedUser1():User
        {
        if(IsLoaded  && 
        
        !(_relatedUser1.IsLoaded || _relatedUser1.IsLoading))
        {
          _relatedUser1 = DataMapperRegistry.Instance.User.load(_relatedUser1);
          
          onParentChanged(_relatedUser1);
        }

        return _relatedUser1;
        }
        public function set RelatedUser1(value:User):void
        {
          _relatedUser1 = User(IdentityMap.register( value ));

          onParentChanged(_relatedUser1);
        }
      
        protected override function onDirtyChanged():void
        {
          
            
            if(RelatedUser != null)
              RelatedUser.onChildChanged(this);
          
            
            if(RelatedUser1 != null)
              RelatedUser1.onChildChanged(this);
          
        }
      
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:Message = new Message();
          
          
            object.MessageId = this.MessageId;
          
            object.SenderUserId = this.SenderUserId;
          
            object.ReceiverUserId = this.ReceiverUserId;
          
            object.Posted = this.Posted;
          
            object.Subject = this.Subject;
          
            object.Body = this.Body;
          
            object.IsRead = this.IsRead;
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
        
        public override function applyFields(object:Object):void
        {
        
                if(!IsPrimaryKeyInitialized)
                  MessageId = object["MessageId"];
              Posted = object["Posted"];
              Subject = object["Subject"];
              Body = object["Body"];
              IsRead = object["IsRead"];
              RelatedUser =
          object.RelatedUser;
        RelatedUser1 =
          object.RelatedUser1;
        
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.Message;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.Message." 
            
              + MessageId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    