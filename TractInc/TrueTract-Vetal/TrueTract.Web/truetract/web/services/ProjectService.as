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
import truetract.web.util.TokenResponder;

public class ProjectService
{
    
    //--------------------------------------------------------------------------
    //
    //  Singleton stuff
    //
    //--------------------------------------------------------------------------
    
    private static var _instance : ProjectService;
    public static function getInstance() : ProjectService
    {
        if ( _instance == null )
            _instance = new ProjectService(new SingletonEnforcer());
            
        return _instance;
    }

    public function ProjectService(singletonEnforcer:SingletonEnforcer) 
    {
    }

    //--------------------------------------------------------------------------
    //
    //  Class members
    //
    //--------------------------------------------------------------------------

    [Bindable] public var serviceIsBusy:Boolean = false;

    private var _service:RemoteObject;

    public function get service():RemoteObject
    {
        if (_service == null) {
           
            _service = new RemoteObject( "GenericDestination" );
            _service.source = "TractInc.TrueTract.Project";
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

    public function getClientProjectList(client:Client, userId:int):AsyncToken
    {
        var asyncToken:AsyncToken = service.GetProjectsByClientAndUser(client.ClientId, userId);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                client.Projects = event.result as Array;
                for each (var project:Project in client.Projects)
                {
                    getProjectTabs(project);
                    getProjectAttachments(project);
                }
            },
            "Unable to load Project List for client [" + client.Name + "]"
            )
        );

        return asyncToken;
    }

    public function getProjectTabs(project:Project):AsyncToken
    {
        var asyncToken:AsyncToken = service.GetProjectTabs(project.ProjectId);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                project.Tabs = event.result as Array;
                for each (var tab:ProjectTab in project.Tabs)
                {
                    tab.TabProject = project;
                }
            },
            "Unable to load Project Tab List for project [" + project.ShortName + "]"
            )
        );
        
        return asyncToken;
    }

    public function getProjectAttachments(project:Project):AsyncToken
    {
        var asyncToken:AsyncToken = service.GetProjectAttachments(project.ProjectId);
        asyncToken.addResponder(new TokenResponder(
            function (event:ResultEvent):void
            {
                project.Attachments = event.result as Array;
                for each (var attachment:ProjectAttachment in project.Attachments)
                {
                    attachment.ProjectRef = project;
                }
            },
            "Unable to load Project Tab List for project [" + project.ShortName + "]"
            )
        );

        return asyncToken;
    }
}
}
class SingletonEnforcer {}