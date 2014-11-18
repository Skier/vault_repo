package com.llsvc.framework.storage
{
import com.llsvc.domain.Client;
import com.llsvc.domain.Person;
import com.llsvc.domain.User;

import mx.rpc.AsyncToken;
import mx.rpc.Responder;
import mx.rpc.remoting.RemoteObject;

public class Storage 
    implements IStorage
{
    private static const SERVICE:String = "framework";
    
    private static var _instance:Storage;

    public static function get instance():Storage
    {
        if (_instance == null) {
            _instance = new Storage();
        }
        return _instance;
    }
    
    private var service:RemoteObject = null;
    
    public function Storage()
    {
        if (_instance != null) {
            throw new Error("Use instance getter instead of constructor. It's singleton !");
        }
    }
    
    public function ping(responder:Responder):void
    {
        var asyncToken:AsyncToken = getService().Ping();
        asyncToken.addResponder(responder);
    }

	public function getGeoServerUrl(responder:Responder):void {
        var asyncToken:AsyncToken = getService().getGeoServerUrl();
        asyncToken.addResponder(responder);
	}
	
    public function login(login:String, password:String, responder:Responder):void
    {
        var asyncToken:AsyncToken = getService().login(login, password);
        asyncToken.addResponder(responder);
    }
            
    public function register(person:Person, login:String, password:String, responder:Responder):void
    {
        var asyncToken:AsyncToken = getService().register(person, login, password);
        asyncToken.addResponder(responder);
    }
            
    public function restorePassword(login:String, responder:Responder):void
    {
        var asyncToken:AsyncToken = getService().RestorePassword(login);
        asyncToken.addResponder(responder);
    }
            
    public function getPackage(user:User, responder:Responder):void
    {
        var asyncToken:AsyncToken = getService().getPackage(user);
        asyncToken.addResponder(responder);
    }
    
    public function getClients(responder:Responder):void
    {
        var asyncToken:AsyncToken = getService().getClients();
        asyncToken.addResponder(responder);
    }
    
    public function storeClient(client:Client, responder:Responder):void
    {
        var asyncToken:AsyncToken = getService().storeClient(client);
        asyncToken.addResponder(responder);
    }
    
    public function getUsers(responder:Responder):void
    {
        var asyncToken:AsyncToken = getService().getUsers();
        asyncToken.addResponder(responder);
    }
    
    public function storeUser(user:User, responder:Responder):void
    {
        var asyncToken:AsyncToken = getService().storeUser(user);
        asyncToken.addResponder(responder);
    }
    
    private function getService():RemoteObject {
        if ( null == service ) {
            service = new RemoteObject();
            service.destination = Storage.SERVICE;
            service.source = Storage.SERVICE;
        } 
        return service;
    }
    
}
}
