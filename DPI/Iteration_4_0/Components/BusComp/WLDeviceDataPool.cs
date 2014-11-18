using System;
using System.Threading;
using System.Configuration;

using DPI.Interfaces;

namespace DPI.Components
{
	[Serializable]
	public class WLDeviceDataPool
	{
		//Thread thread;
		IWirelessDeviceData phoneTecPhoneResp;
		IWirelessDeviceData phoneTecESNResp;
		IWirelessDeviceData telispirePhoneResp;
		IWirelessDeviceData telispireESNResp;
		string phoneOrEsn;
		public WLDeviceDataPool(string phoneOrEsn)
		{
			this.phoneOrEsn = phoneOrEsn;
		}

		public IWirelessDeviceData GetWLDeviceDataResp()
		{
			Thread t1 = new Thread( new ThreadStart(PhoneTecDeviceDataByPhone));
			t1.Name = "PhoneTecDeviceDataByPhone";
			t1.Start();

			Thread t2 = new Thread( new ThreadStart(PhoneTecDeviceDataByESN));
			t2.Name = "PhoneTecDeviceDataByESN";
			t2.Start();

			Thread t3 = new Thread( new ThreadStart(TelispireDeviceDataByPhone));
			t3.Name = "TelispireDeviceDataByPhone";
			t3.Start();

			Thread t4 = new Thread( new ThreadStart(TelispireDeviceDataByESN));
			t4.Name = "TelispireDeviceDataByESN";
			t4.Start();

			//DateTime start = DateTime.Now;
			
			while (!Pass())
				Thread.Sleep(100);

			t1.Abort();
			t2.Abort();
			t3.Abort();
			t4.Abort();

			//DateTime end = DateTime.Now;

			//TimeSpan timeSpent = end - start;
			//double totalTime = timeSpent.TotalMilliseconds;

			return GetPrioritizedResp();			
		}

		void PhoneTecDeviceDataByPhone()
		{
			phoneTecPhoneResp = new PhoneTecWSGateway(Const.PHONETEC).GetWLDeviceDataResp(phoneOrEsn, "");			
		}
		void PhoneTecDeviceDataByESN()
		{
			phoneTecESNResp = new PhoneTecWSGateway(Const.PHONETEC).GetWLDeviceDataResp("", phoneOrEsn);			
		}
		void TelispireDeviceDataByPhone()
		{
			telispirePhoneResp = new TelispireWSGateway(Const.TELISPIRE).GetWLDeviceDataResp(phoneOrEsn, "");
		}
		void TelispireDeviceDataByESN()
		{
			telispireESNResp = new TelispireWSGateway(Const.TELISPIRE).GetWLDeviceDataResp("", phoneOrEsn);
		}

		bool Pass()
		{
			//If any one of the response is null return false
			if (phoneTecPhoneResp == null || phoneTecESNResp == null || telispirePhoneResp == null || telispireESNResp == null)
				return false;

			//If any one status is pending return false 
			if (phoneTecPhoneResp.StatusPending || phoneTecESNResp.StatusPending || 
				telispirePhoneResp.StatusPending || telispireESNResp.StatusPending )
				return false;

			return true;
		}
		IWirelessDeviceData GetPrioritizedResp()
		{
			//Replanish always done with the existing provider.
			if (IsReplanish(telispirePhoneResp))
				return telispirePhoneResp;

			if (IsReplanish(telispireESNResp))
				return telispireESNResp;
			
			if (IsReplanish(phoneTecESNResp))
				return phoneTecESNResp;

			if (IsReplanish(phoneTecPhoneResp))
				return phoneTecPhoneResp;
			
		
			
			return GetActivationPrioritizedResp();
		}
		IWirelessDeviceData GetActivationPrioritizedResp()
		{
			//Activation Rule: If ESN is loaded in one system (PhoneTec or Telispire) then take response from the loaded system.
			//In case of ESN is loaded with both system use percentage rule. Percentage rule will be defined by the key dPiWirelessActivationPriority. 
			//This key will have six digits. First three digits refers Phonetec percentage of common ESN's and last three digits will refer to Telispire percentage of total common ESNs.
			//Phonetec and Telispire together will be 100%.
			
			//If ESN is present in both system use percentage rule based on dPiWirelessActivationPriority key else return the Response where ESn is loaded.
			if ((phoneTecPhoneResp.Pass || phoneTecESNResp.Pass) &&  (telispirePhoneResp.Pass || telispireESNResp.Pass))
				return GetPercentagePrioritizedResp();

			if (telispirePhoneResp.Pass)
				return telispirePhoneResp;

			if (telispireESNResp.Pass)
				return telispireESNResp;
			
			if (phoneTecPhoneResp.Pass)
				return phoneTecPhoneResp;
			
			//if none of them are successful then just send phoneTecESNResp (don't care if it is successful or not 
			return phoneTecESNResp;
		}
		IWirelessDeviceData GetPercentagePrioritizedResp()
		{
			int i = new Random().Next(100);

			//if random number is greater then phonetec percent then take Telispire resp else take PhoneTec response
			if ( i > int.Parse(ConfigurationSettings.AppSettings["dPiWirelessActivationPriority"].Substring(0, 3)))
			{
				if (telispirePhoneResp.Pass)
					return telispirePhoneResp;

				return telispireESNResp;
			}
			
			if (phoneTecPhoneResp.Pass)
				return phoneTecPhoneResp;
			
			return phoneTecESNResp;
		}
		bool IsReplanish(IWirelessDeviceData data)
		{
			if (data == null)
				return false;

			if (!data.Pass)
				return false;

			if (data.StatusPending)
				return false;

			if (data.PlanStatus == DpiWLPlanStatus.Suspended || 
				data.PlanStatus == DpiWLPlanStatus.Expended || 
				data.PlanStatus == DpiWLPlanStatus.Expired ||
				data.PlanStatus == DpiWLPlanStatus.Active)
				return true;

			return false;
		}

	}
}
