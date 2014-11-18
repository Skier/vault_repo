
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.TermUnit;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _TermUnitDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new TermUnit();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Lease.Domain.TermUnitDataMapper";
          }
          
      		public function load(termUnit:TermUnit, responder:Responder = null):TermUnit
          {
            
              if(!termUnit.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(termUnit.getURI()))
              {
                termUnit = TermUnit(IdentityMap.extract(termUnit.getURI()));
                
                if(termUnit.IsLoaded)
                  return termUnit;
      
              } 
              else
               IdentityMap.add(termUnit);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                termUnit.TermUnitId),null,termUnit);
            
              return termUnit;
          }
          
      
          public function findByPrimaryKey(  termUnitId:int):TermUnit
          {
            var activeRecord:TermUnit = new TermUnit();

            
              activeRecord.TermUnitId = termUnitId;
            



      return load(activeRecord);
      }

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
        var item:TermUnit = TermUnit(activeRecord);
                   
        
              
              if(relationName == "leases")
              {
   
                DataMapperRegistry.Instance.Lease.
                findByTermUnitId(
                
                  item.TermUnitId, activeCollection)
              ;
                
                return;
              }
            
         }
        }
      }
    