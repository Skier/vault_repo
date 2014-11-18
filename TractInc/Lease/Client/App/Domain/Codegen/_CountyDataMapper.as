
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.County;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _CountyDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new County();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Lease.Domain.CountyDataMapper";
          }
          
      		public function load(county:County, responder:Responder = null):County
          {
            
              if(!county.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(county.getURI()))
              {
                county = County(IdentityMap.extract(county.getURI()));
                
                if(county.IsLoaded)
                  return county;
      
              } 
              else
               IdentityMap.add(county);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                county.CountyId),null,county);
            
              return county;
          }
          
      
          public function findByPrimaryKey(  countyId:int):County
          {
            var activeRecord:County = new County();

            
              activeRecord.CountyId = countyId;
            



      return load(activeRecord);
      }

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
        var item:County = County(activeRecord);
                   
        
         }
        }
      }
    