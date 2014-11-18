/**
 * $id:

 * Copyright(c) 2006 The Midnight Coders Coders Company. All Rights Reserved.
 */

package com.tmnc.mail.business
{
[RemoteClass(alias="Weborb.Samples.Email.Entities.ServerSettingsInfo")]
public class ServerSettingsInfo 
{
            
    public static const REGULAR_CONNECTION_TYPE:String = "reqular";
    public static const SECURE_TLS_CONNECTION_TYPE:String = "tls";
        
    public var Id:int;
    public var Host:String;
    public var Port:int;
    public var UserName:String;
    public var Password:String;
    public var ConnectionType:String;
}
}