package WDM
{
	import flash.utils.Proxy;
	import flash.utils.flash_proxy;
	import mx.rpc.remoting.RemoteObject;
	import mx.rpc.AsyncToken;
	import mx.rpc.AbstractOperation;
	import mx.rpc.Responder;
	
	public  dynamic class DataMapperProxy extends Proxy
	{
	     override flash_proxy function callProperty(name:*, ... args):* 
	     {
	       var methodName:String = name.toString();
	       
	       trace(methodName);

	       var remoteObject:RemoteObject = createRemoteObject();
	       var asyncToken:AsyncToken = null;
	       var responder:Responder = extractResponder(args);
	       	          
	       try 
	       {
		       	if(methodName.indexOf("find") == 0)
		       		asyncToken = remoteObject.findDynamic(methodName, args);
		       	else
		       	{
			         var remoteMethod:AbstractOperation = remoteObject.getOperation(methodName);
		
			         if(args != null && (args as Array).length > 0)
			         	remoteMethod.arguments = args;
			         
			         asyncToken = remoteMethod.send();
		        }
	       }
	       catch (e:Error) 
	       {
	         throw new ArgumentError("Unable to call method: " + methodName);
       	   }  
       	   
       	   if(asyncToken == null)
       	   		throw new ArgumentError("Invalid method name: " + methodName);
       	   
       	   return new DatabaseAsyncToken( asyncToken, responder );
         }
         

	     override flash_proxy function nextNameIndex (index:int):int 
	     {
			return 0;
	     }
	     
	     override flash_proxy function nextName(index:int):String {
	         return null;
	     }


		protected function createRemoteObject():RemoteObject
		{
			var remoteObject:RemoteObject = new RemoteObject("GenericDestination");
			remoteObject.source = RemoteClassName;
			
			return remoteObject;
		}

		protected virtual function get RemoteClassName():String
		{
			throw new Error("Not implemented");
		}
		
		protected function extractResponder(array:*):Responder
		{
			if(array == null || !(array is Array) || (array as Array).length == 0)
				return null;
			
			var arr:Array = array as Array;
			
			if(arr[arr.length-1] is Responder)
				return arr.pop() as Responder;
			 
			return null;
		}
	}
}