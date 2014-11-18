
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
      
        protected var _newTracts: int;
      
            public function get  UserId(): int
            {
              return _userId;
            }

            public function set  UserId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;
            
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
          
            public function get  NewTracts(): int
            {
              return _newTracts;
            }

            public function set  NewTracts(value:int):void
            {
            
            _newTracts = value;
            }
          

            // one to many relation
            protected var _leaseEditHistorys:ActiveCollection;
            
            public function get LeaseEditHistorys():ActiveCollection
            {
              _leaseEditHistorys = onChildRelationRequest("leaseEditHistorys",_leaseEditHistorys);
              
              return _leaseEditHistorys;
            }
            
        

            // one to many relation
            protected var _userRoles:ActiveCollection;
            
            public function get UserRoles():ActiveCollection
            {
              _userRoles = onChildRelationRequest("userRoles",_userRoles);
              
              return _userRoles;
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
          
            object.NewTracts = this.NewTracts;
          
            if(cascade)
            {
              
                    
                      for each(var leaseEditHistory :LeaseEditHistory in _leaseEditHistorys)
                      {
                        if(leaseEditHistory.IsDirty)
                        {
                           var leaseEditHistoryExtract:Object = leaseEditHistory.extractRelevant(true);
                               leaseEditHistoryExtract.ParentUser = object;

                        object.LeaseEditHistorys.addItem(leaseEditHistoryExtract);
                        }
                    }
                  
                    
                      for each(var userRole :UserRole in _userRoles)
                      {
                        if(userRole.IsDirty)
                        {
                           var userRoleExtract:Object = userRole.extractRelevant(true);
                               userRoleExtract.ParentUser = object;

                        object.UserRoles.addItem(userRoleExtract);
                        }
                    }
                  
            }
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
          public override function extractChilds():Array
          {
          var childs:Array = new Array();

          
                if(this["leaseEditHistorys"])
                {
                  for each(var leaseEditHistory :ActiveRecord in this["leaseEditHistorys"] as Array)
                    childs.push(leaseEditHistory);
                }
              
                if(this["userRoles"])
                {
                  for each(var userRole :ActiveRecord in this["userRoles"] as Array)
                    childs.push(userRole);
                }
              

          return childs;
          }
        
        
        public override function applyFields(object:Object):void
        {
        UserId = object["UserId"];
        Login = object["Login"];
        Password = object["Password"];
        Email = object["Email"];
        IsActive = object["IsActive"];
        HackingAttempts = object["HackingAttempts"];
        NewTracts = object["NewTracts"];
        
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
           _uri = "TractInc.User." 
            
              + UserId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    