package WDM
{
	import mx.rpc.AsyncToken;
	import mx.rpc.remoting.RemoteObject;
	import mx.rpc.Responder;
	
	public dynamic class DataMapper extends DataMapperProxy
	{
		 protected function lock(activeRecord:ActiveRecord):void
		 {
			activeRecord.lock();
		 }
		
		 protected function unlock(activeRecord:ActiveRecord):void
		 {
			activeRecord.unlock();
		 }		
		
		 public function findAll(... args):AsyncToken
		 {
		 	var remoteObject:RemoteObject = createRemoteObject();
		 	var responder:Responder = extractResponder(args);
		 	
		 	return new DatabaseAsyncToken(remoteObject.findAll(args), responder);
		 }
		 
		 public function findFirst(... args):AsyncToken
		 {
		 	var remoteObject:RemoteObject = createRemoteObject();
		 	var responder:Responder = extractResponder(args);
		 	
		 	return new DatabaseAsyncToken(remoteObject.findFirst(args), responder );
		 }
		 
		 public function findBySql(sqlQuery:String, ... args):AsyncToken
		 {
		 	var remoteObject:RemoteObject = createRemoteObject();
		 	var responder:Responder = extractResponder(args);
		 
		 	return new DatabaseAsyncToken(remoteObject.findBySql(sqlQuery,args),responder);
		 }		 
		 
		 public function save(activeRecord:ActiveRecord, responder:Responder = null):AsyncToken
		 {
			if(activeRecord.IsLocked)
				throw new Error("Record is locked and can't be saved");
				
			if(activeRecord.IsReadOnly)
				throw new Error("Record is read only and can't be saved");


		 	var remoteObject:RemoteObject = createRemoteObject();
		 	
		 	return new DatabaseAsyncToken(remoteObject.save(activeRecord),responder, activeRecord);
		 }
		 
		 public function insert(activeRecord:ActiveRecord, responder:Responder = null):AsyncToken
		 {
			if(activeRecord.IsLocked)
				throw new Error("Record is locked and can't be saved");
				
			var remoteObject:RemoteObject = createRemoteObject();
			
			return new DatabaseAsyncToken(remoteObject.insert(activeRecord),responder,activeRecord);
		 }
		 
		 public function removeAll(responder:Responder = null):AsyncToken
		 {
			var remoteObject:RemoteObject = createRemoteObject();
			
			return new DatabaseAsyncToken(remoteObject.removeAll(),responder);
		 }
		 
		 public function remove(activeRecord:ActiveRecord,responder:Responder = null):AsyncToken
		 {
			if(activeRecord.IsLocked)
				throw new Error("Record is locked and can't be deleted");
				
			if(activeRecord.IsReadOnly)
				throw new Error("Record is read only and can't be deleted");


		 	var remoteObject:RemoteObject = createRemoteObject();
		 	
		 	return new DatabaseAsyncToken(remoteObject.remove(activeRecord),responder, activeRecord);
		 }
	}
}