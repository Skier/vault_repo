package tractInc.domain.storage
{
    import mx.rpc.Responder;
    import TractInc.Domain.Person;
    import TractInc.Domain.User;
    import tractInc.domain.packages.UserProfilePackage;
    
    public interface IUserProfileStorage
    {
        function getUserProfilePackage(userId:int, responder:Responder):void;
        
        function changePassword(userId:int, 
                oldPassword:String, 
                newPassword:String,
                responder:Responder):void;

        function savePerson(person:Person, responder:Responder):void;
    }
}