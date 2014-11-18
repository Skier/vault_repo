package com.tmnc.mail.vo
{
    [Bindable]
    [RemoteClass(alias="Weborb.Samples.Email.Entities.MailBoxStatus")]
    public class MailBoxStatus
    {
        public var MessagesOnServer:int;
        public var NewMessages:int;
        
    }
}