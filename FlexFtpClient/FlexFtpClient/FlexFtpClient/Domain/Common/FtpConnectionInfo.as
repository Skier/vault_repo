/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package Domain.Common
{

 	[Bindable]
	[RemoteClass(alias="Weborb.Samples.Ftp.Entities.FtpConnectionInfo")]
	public class FtpConnectionInfo {
		public var ConnectionId:String = "";
    	public var Host:String = "";
    	public var User:String = "";
    	public var Password:String = "";
    	public var CurrentDir:String = "";
    }
}
