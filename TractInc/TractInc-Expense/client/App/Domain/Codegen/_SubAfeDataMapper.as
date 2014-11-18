
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.SubAfe;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _SubAfeDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new SubAfe();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.SubAfeDataMapper";
          }
          
      		public function load(subAfe:SubAfe, responder:Responder = null):SubAfe
          {
            
              if(!subAfe.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(subAfe.getURI()))
              {
                subAfe = SubAfe(IdentityMap.extract(subAfe.getURI()));
                
                if(subAfe.IsLoaded || subAfe.IsLoading)
                  return subAfe;
      
              } 
              else
               IdentityMap.add(subAfe);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                subAfe.SubAFE),null,subAfe);
            
              return subAfe;
          }
          
      
          public function findByPrimaryKey(  subAFE:String):SubAfe
          {
          
            var activeRecord:SubAfe = new SubAfe();
      
            
              activeRecord.SubAFE = subAFE;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:SubAfe = SubAfe(activeRecord);
                   
        
              
              if(relationName == "relatedAssetAssignment")
              {
   
                DataMapperRegistry.Instance.AssetAssignment.
                findBySubAFE(
                
                  item.SubAFE, activeCollection)
              ;
                
                return;
              }
            
         }
        }
      }
    