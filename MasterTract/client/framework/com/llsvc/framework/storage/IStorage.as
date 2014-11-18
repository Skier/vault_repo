package com.llsvc.framework.storage
{
import com.llsvc.domain.Client;
import com.llsvc.domain.Person;
import com.llsvc.domain.User;

import mx.rpc.Responder;

public interface IStorage
{
    function ping(responder:Responder):void;

	function getGeoServerUrl(reponder:Responder):void;
	
    function login(login:String, password:String, responder:Responder):void;
    
    function register(person:Person, login:String, password:String, responder:Responder):void;
            
    function restorePassword(login:String, responder:Responder):void;
            
    function getPackage(user:User, responder:Responder):void;
    
    function getClients(responder:Responder):void;
    
    function storeClient(client:Client, responder:Responder):void;
    
    function getUsers(responder:Responder):void;
    
    function storeUser(user:User, responder:Responder):void;
    
}
}