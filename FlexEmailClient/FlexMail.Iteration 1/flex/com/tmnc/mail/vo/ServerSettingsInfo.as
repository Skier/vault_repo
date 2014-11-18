/**
 * $id:

 * Copyright(c) 2006 The Midnight Coders Coders Company. All Rights Reserved.
 */

package com.tmnc.mail.vo
{
[RemoteClass(alias="Weborb.Samples.Email.Entities.ServerSettingsInfo")]
public class ServerSettingsInfo 
{
            
    public static const CONNECTION_TYPE_REGULAR:String = "reqular";
    public static const CONNECTION_TYPE_SECURE_TLS:String = "tls";
        
    public var Host:String;
    public var Port:int;
    public var UserName:String;
    public var UserPassword:String;
    public var ConnectionType:String;
}
}