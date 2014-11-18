
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _User extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _userId: int;
      
        protected var _login: String;
      
        protected var _password: String;
      
        protected var _email: String;
      
        protected var _isActive: Boolean;
      
        protected var _hackingAttempts: int;
      
        protected var _deleted: Boolean;
      
            public function get  UserId(): int
            {
              return _userId;
            }

            public function set  UserId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _userId = value;
            }
          
            public function get  Login(): String
            {
              return _login;
            }

            public function set  Login(value:String):void
            {
            
            _login = value;
            }
          
            public function get  Password(): String
            {
              return _password;
            }

            public function set  Password(value:String):void
            {
            
            _password = value;
            }
          
            public function get  Email(): String
            {
              return _email;
            }

            public function set  Email(value:String):void
            {
            
            _email = value;
            }
          
            public function get  IsActive(): Boolean
            {
              return _isActive;
            }

            public function set  IsActive(value:Boolean):void
            {
            
            _isActive = value;
            }
          
            public function get  HackingAttempts(): int
            {
              return _hackingAttempts;
            }

            public function set  HackingAttempts(value:int):void
            {
            
            _hackingAttempts = value;
            }
          
            public function get  Deleted(): Boolean
            {
              return _deleted;
            }

            public function set  Deleted(value:Boolean):void
            {
            
            _deleted = value;
            }
          

            // one to many relation
            protected var _relatedNote:ActiveCollection;
            
            public function get RelatedNote():ActiveCollection
            {
              _relatedNote = onChildRelationRequest("relatedNote",_relatedNote);
              
              return _relatedNote;
            }
            
        

            // one to many relation
            protected var _relatedMessage:ActiveCollection;
            
            public function get RelatedMessage():ActiveCollection
            {
              _relatedMessage = onChildRelationRequest("relatedMessage",_relatedMessage);
              
              return _relatedMessage;
            }
            
        

            // one to many relation
            protected var _relatedUserAsset:ActiveCollection;
            
            public function get RelatedUserAsset():ActiveCollection
            {
              _relatedUserAsset = onChildRelationRequest("relatedUserAsset",_relatedUserAsset);
              
              return _relatedUserAsset;
            }
            
        

            // one to many relation
            protected var _relatedUserRole:ActiveCollection;
            
            public function get RelatedUserRole():ActiveCollection
            {
              _relatedUserRole = onChildRelationRequest("relatedUserRole",_relatedUserRole);
              
              return _relatedUserRole;
            }
            
        
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:User = new User();
          
          
            object.UserId = this.UserId;
          
            object.Login = this.Login;
          
            object.Password = this.Password;
          
            object.Email = this.Email;
          
            object.IsActive = this.IsActive;
          
            object.HackingAttempts = this.HackingAttempts;
          
            object.Deleted = this.Deleted;
          
            if(cascade)
            {
              
                    
                      for each(var note :Note in _relatedNote)
                      {
                        if(note.IsDirty)
                        {
                           var noteExtract:Object = note.extractRelevant(true);
                               noteExtract.RelatedUser = object;

                        object.RelatedNote.addItem(noteExtract);
                        }
                    }
                  
                    
                      for each(var message :Message in _relatedMessage)
                      {
                        if(message.IsDirty)
                        {
                           var messageExtract:Object = message.extractRelevant(true);
                               messageExtract.RelatedUser = object;

                        object.RelatedMessage.addItem(messageExtract);
                        }
                    }
                  
                    
                      for each(var userAsset :UserAsset in _relatedUserAsset)
                      {
                        if(userAsset.IsDirty)
                        {
                           var userAssetExtract:Object = userAsset.extractRelevant(true);
                               userAssetExtract.RelatedUser = object;

                        object.RelatedUserAsset.addItem(userAssetExtract);
                        }
                    }
                  
                    
                      for each(var userRole :UserRole in _relatedUserRole)
                      {
                        if(userRole.IsDirty)
                        {
                           var userRoleExtract:Object = userRole.extractRelevant(true);
                               userRoleExtract.RelatedUser = object;

                        object.RelatedUserRole.addItem(userRoleExtract);
                        }
                    }
                  
            }
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
          public override function extractChilds():Array
          {
          var childs:Array = new Array();

          
                if(this["relatedNote"])
                {
                  for each(var note :ActiveRecord in this["relatedNote"] as Array)
                    childs.push(note);
                }
              
                if(this["relatedMessage"])
                {
                  for each(var message :ActiveRecord in this["relatedMessage"] as Array)
                    childs.push(message);
                }
              
                if(this["relatedUserAsset"])
                {
                  for each(var userAsset :ActiveRecord in this["relatedUserAsset"] as Array)
                    childs.push(userAsset);
                }
              
                if(this["relatedUserRole"])
                {
                  for each(var userRole :ActiveRecord in this["relatedUserRole"] as Array)
                    childs.push(userRole);
                }
              

          return childs;
          }
        
        
        public override function applyFields(object:Object):void
        {
        
                if(!IsPrimaryKeyInitialized)
                  UserId = object["UserId"];
              Login = object["Login"];
              Password = object["Password"];
              Email = object["Email"];
              IsActive = object["IsActive"];
              HackingAttempts = object["HackingAttempts"];
              Deleted = object["Deleted"];
              
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.User;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.User." 
            
              + UserId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    