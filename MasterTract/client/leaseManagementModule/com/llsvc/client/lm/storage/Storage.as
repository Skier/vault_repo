package com.llsvc.client.lm.storage
{
import com.llsvc.client.lms.view.lease.search.LeaseSearchCriterias;
import com.llsvc.domain.DocumentRecord;
import com.llsvc.domain.Lease;
import com.llsvc.domain.Project;
import com.llsvc.domain.Tract;
import com.llsvc.domain.User;

import mx.rpc.AsyncToken;
import mx.rpc.Responder;
import mx.rpc.remoting.RemoteObject;

public class Storage 
    implements IStorage
{
    private static const SERVICE:String = "documentService";
    
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
    
    public function getPackage(user:User, responder:Responder):void {
        var asyncToken:AsyncToken = getService().getPackage(user);
        asyncToken.addResponder(responder);
    }
    
    public function findLeases(mask:DocumentRecord, responder:Responder):void {
        var asyncToken:AsyncToken = getService().findLeases(mask);
        asyncToken.addResponder(responder);
    }
    
    public function getLeasesCount(filter:LeaseSearchCriterias, responder:Responder):void {
        var asyncToken:AsyncToken = getService().getLeasesCount();
        asyncToken.addResponder(responder);
    }
    
    public function getLeasesRange(start:int, limit:int, filter:LeaseSearchCriterias, responder:Responder):void {
        var asyncToken:AsyncToken = getService().getLeasesRange(start, limit, filter);
        asyncToken.addResponder(responder);
    }

    public function getLeases(responder:Responder):void {
        var asyncToken:AsyncToken = getService().getLeases();
        asyncToken.addResponder(responder);
    }
    
    public function getProjects(responder:Responder):void {
        var asyncToken:AsyncToken = getService().getProjects();
        asyncToken.addResponder(responder);
    }
    
    public function getLeaseNames1(filter:LeaseSearchCriterias, responder:Responder):void {
        var asyncToken:AsyncToken = getService().getLeaseNames(filter);
        asyncToken.addResponder(responder);
    }
    
    public function createLease(lease:Lease, responder:Responder):void {
        var asyncToken:AsyncToken = getService().createLease(lease);
        asyncToken.addResponder(responder);
    }
    
    public function storeLease(lease:Lease, responder:Responder):void {
        var asyncToken:AsyncToken = getService().storeLease(lease);
        asyncToken.addResponder(responder);
    }
    
    public function deleteLease(leaseId:int, responder:Responder):void {
        var asyncToken:AsyncToken = getService().deleteLease(leaseId);
        asyncToken.addResponder(responder);
    }
    
    public function getAssignmentByLease(leaseId:int, responder:Responder):void {
        var asyncToken:AsyncToken = getService().getAssignmentByLease(leaseId);
        asyncToken.addResponder(responder);
    }
    
    public function getLeaseAssignments(responder:Responder):void {
        var asyncToken:AsyncToken = getService().getLeaseAssignments();
        asyncToken.addResponder(responder);
    }
    
    public function findTracts(mask:Tract, responder:Responder):void {
        var asyncToken:AsyncToken = getService().findTracts(mask);
        asyncToken.addResponder(responder);
    }

    public function saveProject(project:Project, responder:Responder):void {
        var asyncToken:AsyncToken = getService().saveProject(project);
        asyncToken.addResponder(responder);
    }
    
    public function getLeasesByProject(projectId:int, responder:Responder):void {
        var asyncToken:AsyncToken = getService().getLeasesByProject(projectId);
        asyncToken.addResponder(responder);
    }

    public function searchLeases(criterias:LeaseSearchCriterias, responder:Responder):void {
        var asyncToken:AsyncToken = getService().findLeases(criterias);
        asyncToken.addResponder(responder);
    }

    public function getLeaseSummary(filter:LeaseSearchCriterias, responder:Responder):void {
        var asyncToken:AsyncToken = getService().getLeaseSummary(filter);
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
    
    public function getRefDocsByDocId(docId:int, responder:Responder):void {
    	return;
    }
    
}
}
