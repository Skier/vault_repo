package tractInc.domain.storage
{
    import mx.rpc.Responder;
    
    import TractInc.Domain.Role;
    import TractInc.Domain.User;
    import tractInc.domain.packages.UserManagerPackage;
    
    public interface IUserManagerStorage
    {
        function getUserManagerPackage(userId:int, responder:Responder):void;
        function saveUser(user:User, responder:Responder):void;
        function searchUser(login:String, firstName:String, lastName:String,
                roleId:int, isActive:Boolean, companyId:int, clientId:int, responder:Responder):void;
        function saveRole(role:Role, responder:Responder):void;
        function removeRole(role:Role, responder:Responder):void;
    }
}