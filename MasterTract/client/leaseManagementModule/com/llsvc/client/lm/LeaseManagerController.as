package com.llsvc.client.lm
{
import flash.events.Event;
import mx.events.ItemClickEvent;
import mx.rpc.Responder;
import mx.rpc.events.ResultEvent;
import mx.rpc.events.FaultEvent;
import mx.controls.Alert;
import mx.collections.ArrayCollection;

import com.llsvc.domain.User;
import com.llsvc.client.lm.storage.Storage;
import com.llsvc.domain.Document;
import com.llsvc.domain.DocumentRecord;
import com.llsvc.domain.Lease;

/*
import TractInc.Domain.Company;
import TractInc.Domain.Client;
import TractInc.Domain.User;
import tractInc.domain.packages.LeaseManagerPackage;
import tractInc.domain.storage.ILeaseManagerStorage;
import tractInc.domain.storage.LeaseManagerStorage;
import TractInc.Domain.Person;
import TractInc.Domain.Role;
*/

[Bindable]
public class LeaseManagerController
{
    private static var instance:LeaseManagerController = null;
    
    public static function getInstance():LeaseManagerController
    {
        return instance;
    }

/*    
    private var userTab:Object = {label:"System Users", data:"SystemUsers"}; 
    private var roleTab:Object = {label:"System Roles", data:"SystemRoles"}; 
    public var tabData:ArrayCollection = new ArrayCollection();
*/    
    public var view:LeaseManagerView = null;
/*    
    public var user:User = null;
    public var model:LeaseManagerModel = null;
    public var storage:ILeaseManagerStorage = null;
    
    private var roleView:RoleView = null;
    private var userView:UserView = null;
*/    
    public function LeaseManagerController():void 
    {
        instance = this;    
    }
    
    public function init(u:User):void 
    {
        var responder:Responder = new Responder(
                getPackageResultHandler, 
                getPackageFaultHandler);
                
    	Storage.instance.getPackage(u, responder);
/*    	
        user = u;
        model = new LeaseManagerModel();
        storage = LeaseManagerStorage.instance;

        tabData.addItem(userTab);
        tabData.addItem(roleTab);
            
        reloadModel();
*/        
        var dlResponder:Responder = new Responder(
                getLeasesResultHandler, 
                getLeasesFaultHandler);
                
//    	Storage.instance.getLeases(dlResponder);

        var dlaResponder:Responder = new Responder(
                getLeaseAssignmentsResultHandler, 
                getLeaseAssignmentsFaultHandler);
                
//    	Storage.instance.getLeaseAssignments(dlaResponder);
    }
    
    private function testResultHandler(event:ResultEvent):void {
    	if ( null != event.result ) {
    		if ( null != event.result as ArrayCollection ) {
        		Alert.show("testResultHandler: result has " + ArrayCollection(event.result).length + " elements.");
      		} else {
        		Alert.show("testResultHandler: result is " + event.result.toString());
      		}
    	} else {
        	Alert.show("testResultHandler: result is null.");
     	}
    }
    
    private function testFaultHandler(event:FaultEvent):void {
        Alert.show(event.fault.message);
    }
    
    private function getPackageResultHandler(event:ResultEvent):void {
    	var pkg:DocumentPackage = event.result as DocumentPackage;
    	this.view.cLMDocContent.cbState.dataProvider = pkg.stateList;

// test
        var tr:Responder = new Responder(
                testResultHandler, 
                testFaultHandler);
                
        var mask:DocumentRecord = new DocumentRecord();
//        mask.state = pkg.stateList[1];
        mask.state = pkg.stateList[0];
        mask.county = pkg.stateList[0].counties[0];
        Alert.show("stateId=" + mask.state.id + ", countyId=" + mask.county.id);
//        mask.docNo = "23456";
        mask.volume = "888";
        mask.page = "9999";
        
//    	Storage.instance.findLeases(mask, tr);
		Storage.instance.getAssignmentByLease(6, tr);
    	
/*    	
    	if ( null == pkg.stateList ) {
    		Alert.show("stateList is null.");
    	} else {
    		if ( 0 == pkg.stateList.length ) {
    			Alert.show("there are not any state in list.");
    		} else {
    			Alert.show("there are " + pkg.stateList.length + " states");
    			var counties:ArrayCollection = pkg.stateList[40].counties;
    			if ( null == counties ) {
    				Alert.show("counties is null.");
    			} else {
    				Alert.show("there are " + counties.length + " counties.");
    			}
    		}
    	}
*/    	
/*    	
        model.isBusy = false;
        model.init(event.result as LeaseManagerPackage);
        view.roleGrid.dataProvider = new ArrayCollection(
                model.userManagerPackage.RoleList);
        view.userGrid.dataProvider = new ArrayCollection(
                model.userManagerPackage.UserList);
                
        var roles:Array = [null];
        for each (var r:Role in this.model.userManagerPackage.RoleList) {
            roles.push(r);
        }
        this.view.cbRoleFilter.dataProvider = roles;
        this.view.cbRoleFilter.labelField = "Name";

        var companies:Array = [null];
        for each (var co:Company in this.model.userManagerPackage.CompanyList) {
            companies.push(co);
        }
        this.view.cbCompanyFilter.dataProvider = companies;
        this.view.cbCompanyFilter.labelField = "CompanyName";

        var clients:Array = [null];
        for each (var cl:Client in this.model.userManagerPackage.ClientList) {
            clients.push(cl);
        }
        this.view.cbClientFilter.dataProvider = clients;
        this.view.cbClientFilter.labelField = "ClientName";
*/        
    }
    
    private function getPackageFaultHandler(event:FaultEvent):void {
        Alert.show(event.fault.message);
    }
    
    private function getLeasesResultHandler(event:ResultEvent):void {
    	this.view.cLMList.dgLeaseList.dataProvider = event.result as ArrayCollection;
    }
    
    private function getLeasesFaultHandler(event:FaultEvent):void {
        Alert.show(event.fault.message);
    }
    
    private function getLeaseAssignmentsResultHandler(event:ResultEvent):void {
    	this.view.cLMListAssignment.dgLeaseAssignmentList.dataProvider = event.result as ArrayCollection;
    }
    
    private function getLeaseAssignmentsFaultHandler(event:FaultEvent):void {
        Alert.show(event.fault.message);
    }
    
    public function onAddLPRClick():void {
    	this.view.vsLM.selectedChild = this.view.cLMDoc;
    }
    
    public function onSaveLPRClick():void {
		var lease:Lease = this.view.cLMDocContent.getLease();
		    	
        var responder:Responder = new Responder(
                createLeaseResultHandler, 
                createLeaseFaultHandler);
        
        if ( 0 == lease.document.id  ) {
    		Storage.instance.createLease(lease, responder);
        } else {
    		Storage.instance.saveLease(lease, responder);
        }
    }
    private function createLeaseResultHandler(event:ResultEvent):void {
    	this.view.vsLM.selectedChild = this.view.cLMList;
    }
    private function createLeaseFaultHandler(event:FaultEvent):void {
        Alert.show(event.fault.message);
    }
    
    public function onCancelLPRClick():void {
    	this.view.vsLM.selectedChild = this.view.cLMList;
    }
    
    public function doEditLease(lease:Lease):void {
    	this.view.cLMDocContent.setLease(lease);
    	this.view.vsLM.selectedChild = this.view.cLMDoc;
    }

    public function onAddAssignmentClick():void {
//    	this.view.vsLM.selectedChild = this.view.cLMDoc;
    }
    
/*
    private function reloadModel():void
    {
        var responder:Responder = new Responder(
                getLeaseManagerPackageResultHandler, 
                getLeaseManagerPackageFaultHandler);
        model.isBusy = true;
        storage.getLeaseManagerPackage(user.UserId, responder);
    }
    
    public function logout():Boolean 
    {
        if (model.isBusy) {
            Alert.show("User profile service is running");
            return false;
        }
        
        if (isDirty) {
            Alert.show("User profile changes not saved");
            return false;
        }
        
        return true;
    }
    
    public function tabChanged(event:ItemClickEvent):void 
    {
        view.tabStack.selectedChild.visible = false;
        view.tabStack.selectedIndex = event.index;
        view.tabStack.selectedChild.visible = true;
    }
    
    public function addUserButtonOnClickHandler(event:Event):void 
    {
        if ( model.userManagerPackage.canManageUsers ) {
            userView = UserView.open(this, null, true);
        } else {
            Alert.show("No permissions to manage Users");
        }
    }
    
    public function searchButtonOnClickHandler(event:Event):void 
    {
        var responder:Responder = new Responder(
                searchUserResultHandler, 
                searchUserFaultHandler);

        model.isBusy = true;
        storage.searchUser(this.view.txtLoginFilter.text,
                this.view.txtFirstNameFilter.text,
                this.view.txtLastNameFilter.text,
                (null != this.view.cbRoleFilter.selectedItem 
                        ? (this.view.cbRoleFilter.selectedItem as Role).RoleId
                        : 0),
                (0 == this.view.cbIsActiveFilter.selectedIndex ? true : false),
                (null != this.view.cbCompanyFilter.selectedItem 
                        ? (this.view.cbCompanyFilter.selectedItem as Company).CompanyId
                        : 0),
                (null != this.view.cbClientFilter.selectedItem 
                        ? (this.view.cbClientFilter.selectedItem as Client).ClientId
                        : 0), responder);
    }
    
    public function addRoleButtonOnClickHandler(event:Event):void 
    {
        if ( model.userManagerPackage.canManageRoles ) {
            roleView = RoleView.open(this, null, true);
        } else {
            Alert.show("No permissions to manage Roles");
        }
    }
*/    
/*        
    public function roleGridOnClickHandler(event:Event):void 
    {
        trace("LeaseManagerController.roleGridOnClickHandler: event=" + event);
            var role:Role = model.userManagerPackage.RoleList[view.roleGrid.selectedIndex] as Role;
            openRole(role);    
    }
*/
/*        
    public function saveUser(user:User):void 
    {
        var responder:Responder = new Responder(
                saveUserResultHandler, 
                saveUserFaultHandler);

        model.isBusy = true;
        trace("LeaseManagerController.saveUser: password=" + user.Password);
        storage.saveUser(user, responder);
    }
    
    public function openUser(user:User):void
    {
        trace("LeaseManagerController.openUser: user=" + user);
        userView = UserView.open(this, user, true);
    }
    
    public function saveRole(role:Role):void 
    {
        var responder:Responder = new Responder(
                saveRoleResultHandler, 
                saveRoleFaultHandler);

        model.isBusy = true;
        storage.saveRole(role, responder);
    }
    
    public function openRole(role:Role):void
    {
        trace("LeaseManagerController.openRole: role=" + role);
        roleView = RoleView.open(this, role, true);
    }
    
    public function removeRole(role:Role):void
    {
        trace("LeaseManagerController.removeRole: role=" + role);
        var responder:Responder = new Responder(
                removeRoleResultHandler, 
                removeRoleFaultHandler);

        model.isBusy = true;
        storage.removeRole(role, responder);
    }
    
    private function get isDirty():Boolean 
    {
        return false;
    }
    
    private function saveUserResultHandler(event:ResultEvent):void 
    {
        model.isBusy = false;
        userView.close();
        reloadModel();
    }
    
    private function saveUserFaultHandler(event:FaultEvent):void 
    {
        model.isBusy = false;
        Alert.show(event.fault.faultString);
    }
    
    private function searchUserResultHandler(event:ResultEvent):void 
    {
        model.isBusy = false;
        model.userManagerPackage.UserList = event.result as Array;
        view.userGrid.dataProvider = new ArrayCollection(
                model.userManagerPackage.UserList);
    }
    
    private function searchUserFaultHandler(event:FaultEvent):void 
    {
        model.isBusy = false;
        Alert.show(event.fault.faultString);
    }
    
    private function saveRoleResultHandler(event:ResultEvent):void 
    {
        model.isBusy = false;
        roleView.close();
        reloadModel();
    }
    
    private function saveRoleFaultHandler(event:FaultEvent):void 
    {
        model.isBusy = false;
        Alert.show(event.fault.faultString);
    }
    
    private function removeRoleResultHandler(event:ResultEvent):void 
    {
        model.isBusy = false;
        reloadModel();
    }
    
    private function removeRoleFaultHandler(event:FaultEvent):void 
    {
        model.isBusy = false;
        Alert.show(event.fault.faultString);
    }
*/    
}
}
