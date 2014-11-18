package TractInc.Domain.storage
{
	import mx.rpc.Responder;
	import TractInc.Domain.User;
	import TractInc.Domain.packages.DashboardPackage;
	import TractInc.Domain.Person;
	
	public interface ITractStorage
	{
	    function ping(responder:Responder):void;

        function login(login:String, password:String, responder:Responder):void;
        
        function signup(person:Person, login:String, password:String, responder:Responder):void;
        	    
        function restorePassword(login:String, responder:Responder):void;
        	    
		function getDashboardPackage(user:User, responder:Responder):void;

//		function saveUser(user:User, responder:Responder):void;

	}
}