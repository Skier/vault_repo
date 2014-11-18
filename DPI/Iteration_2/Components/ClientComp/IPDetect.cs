using System;

namespace DPI.ClientComp
{
	public class IPDetect
	{
		/*		Data		*/
		string publicIP;
		string privateIP;

		/*		Properties		*/
		public string PublicIP
		{
			get{ return publicIP; }
		}
		public string PrivateIP
		{
			get{ return privateIP; }
		}

		/*		Constructor		*/
		public IPDetect(string privateIP, string publicIP)
		{
			if (privateIP == null)
				throw new ArgumentException("Public IP address is required");

			if (publicIP == null)
				throw new ArgumentException("Public IP address is required");

			this.privateIP = privateIP.Trim();
			this.publicIP  = publicIP.Trim();
		}
	}
}