
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _County extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _countyId: int;
      
        protected var _name: String;
      
        protected var _stateName: String;
      
        protected var _stateFips: String;
      
        protected var _countyFips: String;
      
        protected var _fips: String;
      
        // parent tables
        internal var _parentState: State
            = new State()
        ;
      
            public function get  CountyId(): int
            {
              return _countyId;
            }

            public function set  CountyId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;
            
            _countyId = value;
            }
          
            public function get  Name(): String
            {
              return _name;
            }

            public function set  Name(value:String):void
            {
            
            _name = value;
            }
          
            public function get  StateId(): int
            {
            

                  if(_parentState != null)
                  return _parentState.StateId;

                
            
            
            return undefined;
            }
            public function set StateId(value:int):void
            {

            

                  if(_parentState == null)
                      _parentState = new State();

                  _parentState.StateId = value;
                
            
            }
          
            public function get  StateName(): String
            {
              return _stateName;
            }

            public function set  StateName(value:String):void
            {
            
            _stateName = value;
            }
          
            public function get  StateFips(): String
            {
              return _stateFips;
            }

            public function set  StateFips(value:String):void
            {
            
            _stateFips = value;
            }
          
            public function get  CountyFips(): String
            {
              return _countyFips;
            }

            public function set  CountyFips(value:String):void
            {
            
            _countyFips = value;
            }
          
            public function get  Fips(): String
            {
              return _fips;
            }

            public function set  Fips(value:String):void
            {
            
            _fips = value;
            }
          
        
        public function get ParentState():State
        {
        if(IsLoaded  && 
        
        !(_parentState.IsLoaded || _parentState.IsLoading))
        {
          _parentState = DataMapperRegistry.Instance.State.load(_parentState);
          
          onParentChanged(_parentState);
        }

        return _parentState;
        }
        public function set ParentState(value:State):void
        {
          _parentState = State(IdentityMap.register( value ));

          onParentChanged(_parentState);
        }
      
        protected override function onDirtyChanged():void
        {
          
            
            if(ParentState != null)
              ParentState.onChildChanged(this);
          
        }
      
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:County = new County();
          
          
            object.CountyId = this.CountyId;
          
            object.Name = this.Name;
          
            object.StateId = this.StateId;
          
            object.StateName = this.StateName;
          
            object.StateFips = this.StateFips;
          
            object.CountyFips = this.CountyFips;
          
            object.Fips = this.Fips;
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
        
        public override function applyFields(object:Object):void
        {
        CountyId = object["CountyId"];
        Name = object["Name"];
        StateId = object["StateId"];
        StateName = object["StateName"];
        StateFips = object["StateFips"];
        CountyFips = object["CountyFips"];
        Fips = object["Fips"];
        
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.County;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractInc.County." 
            
              + CountyId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    