package truetract.web.services
{
import mx.rpc.AbstractService;
import mx.rpc.AsyncToken;
import mx.rpc.Responder;
import mx.rpc.events.FaultEvent;
import mx.rpc.events.InvokeEvent;
import mx.rpc.events.ResultEvent;
import mx.rpc.remoting.mxml.RemoteObject;

import truetract.domain.*;

public class TrueTractService
{
    
    //--------------------------------------------------------------------------
    //
    //  Singleton stuff
    //
    //--------------------------------------------------------------------------
    
    private static var _instance : TrueTractService;
    public static function getInstance() : TrueTractService
    {
        if ( _instance == null )
            _instance = new TrueTractService(new SingletonEnforcer);
            
        return _instance;
    }

    public function TrueTractService(singletonEnforcer:SingletonEnforcer) 
    {
    }

    //--------------------------------------------------------------------------
    //
    //  Class members
    //
    //--------------------------------------------------------------------------

    [Bindable] public var serviceIsBusy:Boolean = false;
    
    private var _service:RemoteObject;

    private function get service():RemoteObject
    {
        if (_service == null) {
           
            _service = new RemoteObject( "GenericDestination" );
            _service.source = "TractInc.TrueTract.TrueTractService";
            _service.showBusyCursor = true;
            
            _service.addEventListener(InvokeEvent.INVOKE, 
                function(event:InvokeEvent):void { serviceIsBusy = true });
            
            _service.addEventListener(ResultEvent.RESULT,
                function(event:ResultEvent):void { serviceIsBusy = false });
            
            _service.addEventListener(FaultEvent.FAULT,
                function(event:FaultEvent):void { serviceIsBusy = false });
        }

        return _service;
    }

    public function saveUserGroup(group:UserGroup, newGroupName:String):AsyncToken
    {
        var asyncToken:AsyncToken = service.SaveUserGroup(group.groupId, newGroupName);
        asyncToken.addResponder(new Responder(
            function (event:ResultEvent):void
            {
                group.groupName = newGroupName;
            },
            function (event:FaultEvent):void
            {
                trace("saveUserGroup.onFault: " + event.fault.faultString);
            })
        );

        return asyncToken;
    }

    public function createUserGroup(groupName:String, userId:int):AsyncToken
    {
        return service.CreateUserGroup(groupName, userId);
    }

    public function deleteUserGroup(groupId:int):AsyncToken
    {
        return service.DeleteUserGroup(groupId);
    }

    public function getGroupListByUser(userId:int):AsyncToken
    {
        return service.GetGroupListByUser(userId);
    }
    
    public function getClientListByUser(userId:int):AsyncToken
    {
        return service.GetClientListByUser(userId);
    }

    public function getModuleListByUser(userId:int):AsyncToken
    {
        return service.GetModuleListByUser(userId);
    }

    public function addDocumentToGroup(groupId:int, docBranchUid:String):AsyncToken
    {
        return service.AddDocumentToGroup(groupId, docBranchUid);
    }

    public function removeDocumentFromGroup(groupId:int, docBranchUid:String):AsyncToken
    {
        return service.RemoveDocumentFromGroup(groupId, docBranchUid);
    }

}
}
class SingletonEnforcer {}