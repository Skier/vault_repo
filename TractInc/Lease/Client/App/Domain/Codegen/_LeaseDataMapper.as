
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.Lease;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _LeaseDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new Lease();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Lease.Domain.LeaseDataMapper";
          }
          
      		public function load(lease:Lease, responder:Responder = null):Lease
          {
            
              if(!lease.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(lease.getURI()))
              {
                lease = Lease(IdentityMap.extract(lease.getURI()));
                
                if(lease.IsLoaded)
                  return lease;
      
              } 
              else
               IdentityMap.add(lease);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                lease.LeaseId),null,lease);
            
              return lease;
          }
          
      
          public function findByPrimaryKey(  leaseId:int):Lease
          {
            var activeRecord:Lease = new Lease();

            
              activeRecord.LeaseId = leaseId;
            



      return load(activeRecord);
      }

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
        var item:Lease = Lease(activeRecord);
                   
        
              
              if(relationName == "leaseEditHistorys")
              {
   
                DataMapperRegistry.Instance.LeaseEditHistory.
                findByLeaseId(
                
                  item.LeaseId, activeCollection)
              ;
                
                return;
              }
            
         }
        }
      }
    