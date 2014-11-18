package tractInc.domain.storage
{
    import mx.rpc.Responder;
    import mx.rpc.AsyncToken;
    import mx.rpc.remoting.RemoteObject;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;

    import TractInc.Domain.Client;
    import TractInc.Domain.ClientContact;
    import TractInc.Domain.Company;
    import TractInc.Domain.CompanyContact;
    import TractInc.Domain.Contract;
    import TractInc.Domain.Project;
    import TractInc.Domain.ContractRate;
    import tractInc.domain.packages.ProjectManagerPackage;

    public class ProjectManagerStorage implements IProjectManagerStorage
    {
        private static const PROJECT_MANAGER_SERVICE:String = "TractInc.Server.WebOrbServices.ProjectManagerService";
        
        private static var _instance:IProjectManagerStorage;
        private var _services:Object = new Object();
        
        public static function get instance():IProjectManagerStorage
        {
            if (_instance == null) {
                _instance = new ProjectManagerStorage();
            }
            return _instance;
        }
        
        public function ProjectManagerStorage()
        {
            if (_instance != null) {
                throw new Error("Singleton!");
            }
        }
        
        public function getProjectManagerPackage(userId:int, clientId:int, responder:Responder):void
        {
            var asyncToken:AsyncToken = getProjectManagerService().GetProjectManagerPackage(userId, clientId);
            asyncToken.addResponder(responder);
        }

        public function getProjectList(userId:int, responder:Responder):void
        {
            var asyncToken:AsyncToken = getProjectManagerService().GetProjectList(userId);
            asyncToken.addResponder(responder);
        }

        public function saveProject(project:Project, responder:Responder):void
        {
            var asyncToken:AsyncToken = getProjectManagerService().SaveProject(project);
            asyncToken.addResponder(responder);
        }
        
        public function removeProject(project:Project, responder:Responder):void
        {
            var asyncToken:AsyncToken = getProjectManagerService().RemoveProject(project);
            asyncToken.addResponder(responder);
        }

        private function getProjectManagerService():RemoteObject {
            return getService(ProjectManagerStorage.PROJECT_MANAGER_SERVICE);
        }

        private function getService(name:String):RemoteObject {
            if (_services[name] == null) {
                var service:RemoteObject = new RemoteObject();
                service.destination = "GenericDestination";
                service.source = name;
                _services[name] = service;
            } 
            return _services[name];
        }
        
    }
}