using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

using DPI.Interfaces;

namespace DPI.Components
{	
	public enum MessageType { Text,	HTML }

	public class SmtpException : ApplicationException
	{
	#region Constractors
		public SmtpException(string message) : base(message) {}
	#endregion
	}

	public class MailMessage
	{
	#region Data
		string emailFrom;
		string emailSubject;
		ArrayList emailTo;
		string emailMessage;
		MessageType emailMessageType;
	#endregion

	#region Properties
		public string EmailFrom
		{
			get { return emailFrom; }
			set { emailFrom = value; }
		}
		public string EmailSubject
		{
			get { return emailSubject; }
			set { emailSubject = value; }
		}
		public ArrayList EmailTo
		{
			get { return emailTo; }
		}
		public void AddEmailTo(string email)
		{
			if(emailTo == null)
				emailTo = new ArrayList();
			emailTo.Add(email);
		}
		public string EmailMessage
		{
			get { return emailMessage; }
			set { emailMessage = value; }
		}
		public MessageType EmailMessageType
		{
			get { return emailMessageType; }
			set { emailMessageType = value; }
		}
	#endregion
	
	#region Methods
		public void SendMail()
		{
			try
			{
				Smtp.SendEmail(this);
			}
			catch (Exception)
			{
				//TextUtil.WriteText("Send Mail Error. Exception: " + ex.Message, logpath);
			}

		}	
	#endregion
	}
}