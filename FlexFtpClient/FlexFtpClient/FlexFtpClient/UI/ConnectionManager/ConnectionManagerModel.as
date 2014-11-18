/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package UI.ConnectionManager
{
	import UI.AppModel;
	import UI.ConnectionManager.FtpConnection.FtpConnectionModel;

	import mx.collections.ArrayCollection;

	import flash.net.SharedObject;
	
	public class ConnectionManagerModel
	{

        public var FtpConnectionModels:ArrayCollection;

        public var CurrentFtpConnectionModel:FtpConnectionModel;

		public var sharedObject:SharedObject;

		public function ConnectionManagerModel():void
		{
			FtpConnectionModels = new ArrayCollection;
			sharedObject = SharedObject.getLocal(AppModel.WEBORB_SERVICE_NAME);
		}
	}
}