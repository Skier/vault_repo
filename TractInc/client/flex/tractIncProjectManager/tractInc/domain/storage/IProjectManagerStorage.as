package tractInc.domain.storage
{
    import mx.rpc.Responder;
    
    import TractInc.Domain.User;
    import TractInc.Domain.Client;
    import TractInc.Domain.ClientContact;
    import TractInc.Domain.Company;
    import TractInc.Domain.CompanyContact;
    import TractInc.Domain.Contract;
    import TractInc.Domain.Project;
    import TractInc.Domain.ContractRate;
    import tractInc.domain.packages.ProjectManagerPackage;
    
    public interface IProjectManagerStorage
    {
        function getProjectManagerPackage(userId:int, clientId:int, responder:Responder):void;
        
        function getProjectList(userId:int, responder:Responder):void;
        function saveProject(project:Project, responder:Responder):void;
        function removeProject(project:Project, responder:Responder):void;
    }
}