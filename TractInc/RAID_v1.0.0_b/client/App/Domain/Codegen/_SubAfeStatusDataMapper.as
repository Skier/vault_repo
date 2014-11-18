
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.SubAfeStatus;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _SubAfeStatusDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new SubAfeStatus();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.SubAfeStatusDataMapper";
          }
          
      		public function load(subAfeStatus:SubAfeStatus, responder:Responder = null):SubAfeStatus
          {
            
              if(!subAfeStatus.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(subAfeStatus.getURI()))
              {
                subAfeStatus = SubAfeStatus(IdentityMap.extract(subAfeStatus.getURI()));
                
                if(subAfeStatus.IsLoaded || subAfeStatus.IsLoading)
                  return subAfeStatus;
      
              } 
              else
               IdentityMap.add(subAfeStatus);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                subAfeStatus.SubAFEStatus),null,subAfeStatus);
            
              return subAfeStatus;
          }
          
      
          public function findByPrimaryKey(  subAFEStatus:String):SubAfeStatus
          {
          
            var activeRecord:SubAfeStatus = new SubAfeStatus();
      
            
              activeRecord.SubAFEStatus = subAFEStatus;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:SubAfeStatus = SubAfeStatus(activeRecord);
                   
        
              
              if(relationName == "relatedSubAfe")
              {
   
                DataMapperRegistry.Instance.SubAfe.
                findBySubAFEStatus(
                
                  item.SubAFEStatus, activeCollection)
              ;
                
                return;
              }
            
         }
        }
      }
    