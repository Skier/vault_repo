
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.State;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _StateDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new State();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Lease.Domain.StateDataMapper";
          }
          
      		public function load(state:State, responder:Responder = null):State
          {
            
              if(!state.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(state.getURI()))
              {
                state = State(IdentityMap.extract(state.getURI()));
                
                if(state.IsLoaded)
                  return state;
      
              } 
              else
               IdentityMap.add(state);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                state.StateId),null,state);
            
              return state;
          }
          
      
          public function findByPrimaryKey(  stateId:int):State
          {
            var activeRecord:State = new State();

            
              activeRecord.StateId = stateId;
            



      return load(activeRecord);
      }

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
        var item:State = State(activeRecord);
                   
        
              
              if(relationName == "countys")
              {
   
                DataMapperRegistry.Instance.County.
                findByStateId(
                
                  item.StateId, activeCollection)
              ;
                
                return;
              }
            
         }
        }
      }
    