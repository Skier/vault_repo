using System;
using System.Data;
using System.Configuration;
using SRR = System.Runtime.Remoting;

using DPI.Interfaces;
using DPI.Components;

namespace DPI.Maint
{
	[Serializable]
	public class DpiEzTaxer : MarshalByRefObject, ITaxService
	{
		#region Data
		static SRR.Channels.Tcp.TcpChannel channel;
		#endregion
		
		#region Methods
		public IDmdTax[] ComputeTax(int prod, decimal priceAmt, string zip, DateTime date)
		{
			IBillSoftTax btax = null;

			UOW uow = new UOW();

			try
			{
				btax = new BillSoftTax(uow);
				
				
				return btax.ComputeTax(uow, prod, priceAmt, zip, date);				
			}
			catch (Exception ex)
			{
				ErrLogging.LogError("TaxService_Error", "DpiEzTaxService", ex.Message);
				return new IDmdTax[0];
			}
			finally
			{
				uow.close();
				
				if (btax != null)
					btax.ReleaseSession();
			}
		}
		
		public static void CreateChannel()
		{
			channel = new SRR.Channels.Tcp.TcpChannel(GetPort());
			SRR.Channels.IChannel[] regChnls = SRR.Channels.ChannelServices.RegisteredChannels; 
			
			SRR.Channels.ChannelServices.RegisterChannel(channel);			
			SRR.RemotingConfiguration.RegisterWellKnownServiceType(
				typeof(DpiEzTaxer), GetUri(), 
				SRR.WellKnownObjectMode.SingleCall);			
		}
		public static void StopChannel()
		{
			if (channel == null)
				return;

			SRR.Channels.ChannelServices.UnregisterChannel(channel);
			channel.StopListening(channel);
		}
		public static int GetPort()
		{
			return int.Parse(ConfigurationSettings.AppSettings["DpiEzTaxPort"]);
		}
		public static string GetUri()
		{
			return ConfigurationSettings.AppSettings["DpiEzTaxServiceName"];
		}
		#endregion
	}
}