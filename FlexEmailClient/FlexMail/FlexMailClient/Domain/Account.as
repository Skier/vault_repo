package Domain
{
    [Bindable]
    [RemoteClass(alias="Weborb.Samples.Email.Entities.AccountInfo")]
    public class Account
    {
        
        public var Email:String;
        public var Pop3Settings:ServerSettings;
        public var SmtpSettings:ServerSettings;        
        
    }
}