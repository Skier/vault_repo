/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package UI.ConnectionManager.FtpConnection
{
	import Domain.Common.FtpConnectionInfo;
	import Domain.Common.FtpDirectory;
	

	[Bindable]
	public class FtpConnectionModel
	{
		public function FtpConnectionModel(connectionInfo:FtpConnectionInfo)
		{
			ConnectionInfo = connectionInfo;
			Root = new FtpDirectory();
			Root.Name = connectionInfo.CurrentDir;
			CurrentDirectory = Root;
		}

		public var ConnectionInfo:FtpConnectionInfo;

		public var CurrentDirectory:FtpDirectory;
				
		public var Root:FtpDirectory;
		
	}
}