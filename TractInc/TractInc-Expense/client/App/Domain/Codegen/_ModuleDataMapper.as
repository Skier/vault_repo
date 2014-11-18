
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.Module;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _ModuleDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new Module();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.ModuleDataMapper";
          }
          
      		public function load(module:Module, responder:Responder = null):Module
          {
            
              if(!module.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(module.getURI()))
              {
                module = Module(IdentityMap.extract(module.getURI()));
                
                if(module.IsLoaded || module.IsLoading)
                  return module;
      
              } 
              else
               IdentityMap.add(module);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                module.ModuleId),null,module);
            
              return module;
          }
          
      
          public function findByPrimaryKey(  moduleId:int):Module
          {
          
            var activeRecord:Module = new Module();
      
            
              activeRecord.ModuleId = moduleId;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:Module = Module(activeRecord);
                   
        
              
              if(relationName == "relatedPermission")
              {
   
                DataMapperRegistry.Instance.Permission.
                findByModuleId(
                
                  item.ModuleId, activeCollection)
              ;
                
                return;
              }
            
         }
        }
      }
    