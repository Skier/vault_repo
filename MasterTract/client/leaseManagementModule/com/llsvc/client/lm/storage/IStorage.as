package com.llsvc.client.lm.storage
{
import com.llsvc.client.lms.view.lease.search.LeaseSearchCriterias;
import com.llsvc.domain.DocumentRecord;
import com.llsvc.domain.Lease;
import com.llsvc.domain.Project;
import com.llsvc.domain.Tract;
import com.llsvc.domain.User;

import mx.rpc.Responder;

public interface IStorage
{
    function getPackage(user:User, responder:Responder):void;
    
    function findLeases(mask:DocumentRecord, responder:Responder):void;
    
    function getLeasesCount(filter:LeaseSearchCriterias, responder:Responder):void;
    
    function getLeasesRange(start:int, limit:int, filter:LeaseSearchCriterias, responder:Responder):void;

    function getLeases(responder:Responder):void;
    
    function getProjects(responder:Responder):void;
    
    function getLeaseNames1(filter:LeaseSearchCriterias, responder:Responder):void;
    
    function createLease(lease:Lease, responder:Responder):void;
    
    function storeLease(lease:Lease, responder:Responder):void;

    function deleteLease(leaseId:int, respider:Responder):void;
    
    function getAssignmentByLease(leaseId:int, respider:Responder):void;
    
    function getLeaseAssignments(responder:Responder):void;
    
    function findTracts(mask:Tract, responder:Responder):void;
    
    function saveProject(project:Project, responder:Responder):void;
    
    function getLeasesByProject(projectId:int, responder:Responder):void;
    
    function searchLeases(criterias:LeaseSearchCriterias, responder:Responder):void;

    function getLeaseSummary(filter:LeaseSearchCriterias, responder:Responder):void;
    
}
}