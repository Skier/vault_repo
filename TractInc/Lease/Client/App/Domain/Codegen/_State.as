
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _State extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _stateId: int;
      
        protected var _name: String;
      
        protected var _stateFips: String;
      
        protected var _stateAbbr: String;
      
            public function get  StateId(): int
            {
              return _stateId;
            }

            public function set  StateId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;
            
            _stateId = value;
            }
          
            public function get  Name(): String
            {
              return _name;
            }

            public function set  Name(value:String):void
            {
            
            _name = value;
            }
          
            public function get  StateFips(): String
            {
              return _stateFips;
            }

            public function set  StateFips(value:String):void
            {
            
            _stateFips = value;
            }
          
            public function get  StateAbbr(): String
            {
              return _stateAbbr;
            }

            public function set  StateAbbr(value:String):void
            {
            
            _stateAbbr = value;
            }
          

            // one to many relation
            protected var _countys:ActiveCollection;
            
            public function get Countys():ActiveCollection
            {
              _countys = onChildRelationRequest("countys",_countys);
              
              return _countys;
            }
            
        
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:State = new State();
          
          
            object.StateId = this.StateId;
          
            object.Name = this.Name;
          
            object.StateFips = this.StateFips;
          
            object.StateAbbr = this.StateAbbr;
          
            if(cascade)
            {
              
                    
                      for each(var county :County in _countys)
                      {
                        if(county.IsDirty)
                        {
                           var countyExtract:Object = county.extractRelevant(true);
                               countyExtract.ParentState = object;

                        object.Countys.addItem(countyExtract);
                        }
                    }
                  
            }
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
          public override function extractChilds():Array
          {
          var childs:Array = new Array();

          
                if(this["countys"])
                {
                  for each(var county :ActiveRecord in this["countys"] as Array)
                    childs.push(county);
                }
              

          return childs;
          }
        
        
        public override function applyFields(object:Object):void
        {
        StateId = object["StateId"];
        Name = object["Name"];
        StateFips = object["StateFips"];
        StateAbbr = object["StateAbbr"];
        
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.State;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractInc.State." 
            
              + StateId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    