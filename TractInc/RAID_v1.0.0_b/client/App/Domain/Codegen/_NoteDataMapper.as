
      package App.Domain.Codegen
      {
        import weborb.data.*;

        import mx.rpc.AsyncToken;
        import mx.rpc.Responder;
        import mx.rpc.events.ResultEvent;
        import mx.rpc.remoting.RemoteObject;

        import App.Domain.Note;
        import App.Domain.DataMapperRegistry;
      
        public dynamic class _NoteDataMapper extends DataMapper
        {
        
          public override function createActiveRecordInstance():ActiveRecord
          {
            return new Note();
          }
        
          protected override function get RemoteClassName():String
          {
            return "TractInc.Expense.Domain.NoteDataMapper";
          }
          
      		public function load(note:Note, responder:Responder = null):Note
          {
            
              if(!note.IsPrimaryKeyInitialized)
          	    throw new Error("Record can be loaded only with initialized primary key");
          
              if(IdentityMap.exists(note.getURI()))
              {
                note = Note(IdentityMap.extract(note.getURI()));
                
                if(note.IsLoaded || note.IsLoading)
                  return note;
      
              } 
              else
               IdentityMap.add(note);
      
              var asyncToken:AsyncToken = new DatabaseAsyncToken(createRemoteObject().findByPrimaryKey(
                note.NoteId),null,note);
            
              return note;
          }
          
      
          public function findByPrimaryKey(  noteId:int):Note
          {
          
            var activeRecord:Note = new Note();
      
            
              activeRecord.NoteId = noteId;
            
      
            return load(activeRecord);
          }
        
        

      public override function loadChildRelation(activeRecord:ActiveRecord,relationName:String, activeCollection:ActiveCollection):void
      {
      var item:Note = Note(activeRecord);
                   
        
         }
        }
      }
    