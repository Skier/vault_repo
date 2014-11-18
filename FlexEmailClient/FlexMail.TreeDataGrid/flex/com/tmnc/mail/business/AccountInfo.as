/**
 * $id:

 * Copyright(c) 2006 The Midnight Coders Coders Company. All Rights Reserved.
 */

package com.tmnc.mail.business
{
        [Bindable]
        [RemoteClass(alias="Weborb.Samples.Email.Entities.AccountInfo")]
        public class AccountInfo {
        public var Id:int;
        public var Email:String;
        public var Pop3SettingsId:int;
        public var SmtpSettingsId:int;        
        }
}