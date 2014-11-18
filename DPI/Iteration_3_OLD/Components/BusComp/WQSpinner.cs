using System;
using System.Threading;

using DPI.Interfaces;

namespace DPI.Components
{
	[Serializable]  
	public class WQSpinner
	{
		/*        Data        */
		const int lapse = 5;  // sleep time in minutes
		const int update = 10; // frequency in minutes
		static Thread thread;
		static WQSpinner spinner;
		
		static ISvcFactory svcFactory;
		public static void RegSvcFactory(ISvcFactory factory)
		{
			svcFactory = factory;
		}

		/*		Constructors		*/
		WQSpinner() 
		{
		//	OperationMessenger.RefreshData += new EventHandler(Restart);
			OperationMessenger.Restart += new EventHandler(Restart);
			OperationMessenger.Stop += new EventHandler(StopThread);
		}
		
		/*		Methods				*/

		public static void Load()
		{
			if (spinner == null)
				spinner = new WQSpinner();
		}
		/*		Implementation		*/
		void Restart(object sender, EventArgs ea)
		{
			spinner.StopThread(sender, ea);
			spinner.StartThred(sender, ea);
		}
		void StopThread(object sender, EventArgs ea)
		{
			if (thread == null)
				return;

			try {thread.Abort();} 
			catch {}
		}
		void StartThred(object sender, EventArgs ea)
		{
			try
			{	
				if (thread != null)
					if (thread.IsAlive)
						return;
			
				thread = new Thread( new ThreadStart(SpinIt));
				thread.Name = "PendWebQue " + DateTime.Now.ToString();
				thread.Start();
			}
			catch (Exception ex)
			{
				ErrLogging.LogError("WQSpinner.StartThred", "Auto", ex.Message);
			}
		}
		void SpinIt()
		{
			try
			{
				while (true)
				{
					ProcessQueues();
					Thread.Sleep(lapse * 60000); // converts minutes to miliseconds
				}
			}
			catch(Exception ex)
			{
				ErrLogging.LogError("WQSpinner. SpinIt", "Thread:" + Thread.CurrentThread.Name, ex.Message);
			}
		}
		void ProcessQueues()
		{
			ProcessWebSvcQue();
			//ProcessGQue();
		}	
		void ProcessGQue()
		{
			UOW uow = null;

			try
			{
				uow = new UOW();

				GQueue[] queue = GQueue.getActive(uow);
				
				ErrLogging.LogError("WQSpinner.SpinIt", uow.Cn.WorkstationId + ", " + Thread.CurrentThread.Name,
					"Begin at " + DateTime.Now.ToString() +  ", active queue count: " + queue.Length.ToString() );
				
				for (int i = 0; i < queue.Length; i++)			
					ProcessEntry(queue[i]);

				ErrLogging.LogError("WQSpinner. SpinIt", "Auto", "Ended at " + DateTime.Now.ToString());
			}	
			catch(Exception ex)
			{
				ErrLogging.LogError("WQSpinner.ProcessWebQueue", uow.Cn.WorkstationId + ", " + Thread.CurrentThread.Name, ex.Message);
			}
			finally
			{
				uow.close();
			}
		}

		void ProcessWebSvcQue()
		{
			UOW uow = null;

			try
			{
				uow = new UOW();

				IWebSvcQueue[] queue = WebSvcQueue.GetPending(uow);
				
				DPI_Err_Log.AddLogEntry(ErrLogSubSystems.WQSpinner.ToString(), uow.Cn.WorkstationId + ", " + Thread.CurrentThread.Name,
					"Begin at " + DateTime.Now.ToString() +  ", active queue count: " + queue.Length.ToString() );
				
				for (int i = 0; i < queue.Length; i++)			
					ProcessEntry(queue[i]);

				queue = WebSvcQueue.GetPending(uow);
				
				DPI_Err_Log.AddLogEntry(ErrLogSubSystems.WQSpinner.ToString(), "Auto",
					"Ended at " + DateTime.Now.ToString() +  ", active queue count: " + queue.Length.ToString() );
			}	
			catch(Exception ex)
			{
				ErrLogging.LogError("WQSpinner.ProcessWebQueue", uow.Cn.WorkstationId + ", " + Thread.CurrentThread.Name, ex.Message);
			}
			finally
			{
				uow.close();
			}
		}
		void ProcessEntry(GQueue entry)
		{
			if (Skip(entry))
				return;

			UOW uow = null;

			try
			{	
				uow = new UOW();
				entry.Status = entry.Status; // sets row state 

				if (IsKeeper(uow, entry))
					entry.FollowUp(svcFactory);

				uow.commit();

			}
			catch(Exception ex)
			{
				uow.Rollback();
				ErrLogging.LogError("WQSpinner.ProcessGQueue", uow.Cn.WorkstationId + ", " + Thread.CurrentThread.Name, ex.Message);
			}
			finally
			{
				uow.close();	
				entry.LastDateTime = DateTime.Now;
				entry.AccessCnt++;

			//	Update(entry);
			}
		}
		void ProcessEntry(IWebSvcQueue entry)
		{
			if (Skip(entry))
				return;

			UOW uow = null;

			try
			{	
				uow = new UOW();
				entry.Status = entry.Status; // sets row state 

				if (IsKeeper(uow, entry))
					entry.FollowUp();

				uow.commit();
			}
			catch(Exception ex)
			{
				uow.Rollback();
				ErrLogging.LogError("WQSpinner.ProcessWebSvcQueue", uow.Cn.WorkstationId + ", " + Thread.CurrentThread.Name, ex.Message);
			}
			finally
			{
				uow.close();	
				entry.LastAccessDate = DateTime.Now;
				Update(entry);
			}
		}
		bool IsKeeper(UOW uow, IWebSvcQueue entry)
		{
			if (WebQueType.find(uow, entry.QueType).IsEvergreen)
				return true;
				
			if (entry.Attemps < Const.MaxAttemps)
				return true;

			entry.Status = "Escalate";
			SendMail(entry);
			return false;
		}
		bool Skip(GQueue entry)
		{
			try
			{
				if (entry.LastDateTime.AddMinutes(update) < DateTime.Now) 
					return false;

				return true;
			}
			catch {}
			return false; // after exception
		}
		bool IsKeeper(UOW uow, GQueue entry)
		{
			if (GQueueType.find(uow, entry.GQueType).IsEvergreen)
				return true;
				
			if (entry.MaxCnt > entry.AccessCnt)
				return true;

			entry.Status = "Escalate";
			SendMail(entry);

			return false;
		}

		bool Skip(IWebSvcQueue entry)
		{
			try
			{
				if (entry.Attemps == 1)
					return false;

				if (entry.LastAccessDate.AddMinutes(update * entry.Attemps) < DateTime.Now) 
					return false;

				return true;
			}
			catch {}
			return false; // after exception
		}
		void SendMail(GQueue entry)
		{
			if (entry == null)
			{
				DPI_Err_Log.AddLogEntry("WQSpinner.SendMail()", "N/A", "GQueue entry is required");
				return;
			}

			try
			{
				MailMessage msg = new MailMessage();

				msg.AddEmailTo(Const.TECH_SUPPORT_EMAIL);	
				msg.EmailFrom        = "omar.azad@dpiteleconnect.com";
				msg.EmailSubject     = "Queue method failure";

				msg.EmailMessageType = MessageType.Text;
				msg.EmailMessage     = EscalationMessage(entry);
				
				msg.SendMail();
			}
			catch (Exception ex)
			{
				DPI_Err_Log.AddLogEntry("WQSpinner.SendMail()", "N/A",
					"Send Mail Error: web queue entry id " + entry.Id.ToString()
					+ ", Exception: " + ex.Message + ", Stack trace: "+  ex.StackTrace);
			}
		}
		void SendMail(IWebSvcQueue entry)
		{
			if (entry == null)
			{
				DPI_Err_Log.AddLogEntry("WQSpinner.SendMail()", "N/A", "Web queue entry is required");
				return;
			}

			try
			{
				MailMessage msg = new MailMessage();

				msg.AddEmailTo(Const.TECH_SUPPORT_EMAIL);	
				msg.EmailFrom        = "omar.azad@dpiteleconnect.com";
				msg.EmailSubject     = "Queue method failure";

				msg.EmailMessageType = MessageType.Text;
				msg.EmailMessage     = EscalationMessage(entry);
				
				msg.SendMail();
			}
			catch (Exception ex)
			{
				DPI_Err_Log.AddLogEntry("WQSpinner.SendMail()", "N/A",
					"Send Mail Error: web queue entry id " + entry.Id.ToString()
					 + ", Exception: " + ex.Message + ", Stack trace: "+  ex.StackTrace);
			}
		}
		string EscalationMessage(GQueue entry)
		{
			return "Unable to process GQueue entry " + entry.Id.ToString() 
				+ " after " + entry.AccessCnt.ToString() + "." + "\n" 
				+ "\n\t" + "Service provider: " + entry.SvcProvider
				+ "\n\t" + "Initial method: "   + entry.Method;
		}
		string EscalationMessage(IWebSvcQueue entry)
		{
			return "Unable to process Web queue entry " + entry.Id.ToString() 
				+ " after " + entry.Attemps.ToString() + "."
				+ "\n" 
				+ "\n\t" + "Web method provider: "   + entry.WSProvider
				+ "\n\t" + "Initial method: "        + entry.WebMethod
				+ "\n\t" + "Reversal method: "       + entry.ReversalMethod
				+ "\n\t" + "Initial response code: " + entry.InitReasonCode
				+ "\n\t" + "Last response code: "    + entry.LastReasonCode;
		}

		void Update(IWebSvcQueue entry)
		{
			UOW uow  = null;
			try
			{
				uow = new UOW();

				entry.Uow = uow;
				uow.Imap.add(entry);
				
				uow.commit();
			}
			catch {} 
			finally { uow.close();}
		}
	}
}