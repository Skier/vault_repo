package Domain
{
    [Bindable]
    [RemoteClass(alias="Weborb.Samples.Email.Entities.MailBoxStatus")]
    public class MailBoxStatus
    {
        
        public var TotalMessages:int;
        public var NewMessages:int;
        
    }

}