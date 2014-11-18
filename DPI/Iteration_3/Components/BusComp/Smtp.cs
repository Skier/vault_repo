using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Configuration;

using DPI.Interfaces;

namespace DPI.Components
{	
	public class Smtp
	{
		#region Methods
		public static void SendEmail(MailMessage msg)
		{
			int code; 

			if(msg.EmailFrom == "" || msg.EmailSubject == "" || msg.EmailTo == null)
				throw new SmtpException("Invalid Smtp or email parameters.");

			using(TcpClient smtpSocket = new TcpClient(ConfigurationSettings.AppSettings["DpiMailServer"], int.Parse(ConfigurationSettings.AppSettings["DpiMailServerPort"]))) // open a connection to the Smtp server

			using(NetworkStream ns = smtpSocket.GetStream())
			{
				code = GetSmtpResponse(ReadBuffer(ns));// get response from Smtp server

				if(code != 220)
					throw new SmtpException("Error connecting to Smtp server. (" + code.ToString() + ")");
			
				WriteBuffer(ns, "ehlo\r\n");// EHLO
				string buffer = ReadBuffer(ns);// get response from Smtp server

				code = GetSmtpResponse(buffer);
				if(code != 250)
					throw new SmtpException("Error initiating communication with Smtp server. (" + code.ToString() + ")");

				if(buffer.IndexOf("AUTH=LOGIN") >= 0)  //check for AUTH=LOGIN
				{
					WriteBuffer(ns, "auth login\r\n");
					code = GetSmtpResponse(ReadBuffer(ns));// get response from Smtp server

					WriteBuffer(ns, System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes("")) + "\r\n");	// username:
					code = GetSmtpResponse(ReadBuffer(ns));	// get response from Smtp server

					WriteBuffer(ns, System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes("")) + "\r\n");	// password:
					code = GetSmtpResponse(ReadBuffer(ns));			// get response from Smtp server
				}

				WriteBuffer(ns, "mail from: <" + msg.EmailFrom + ">\r\n");	// MAIL FROM:
				code = GetSmtpResponse(ReadBuffer(ns));			            // get response from Smtp server

				if(code != 250)
					throw new SmtpException("Error setting sender email address. (" + code.ToString() + ")");

	
				foreach(string sEmailTo in msg.EmailTo)			// RCPT TO:
				{
					WriteBuffer(ns, "rcpt to:<" + sEmailTo + ">\r\n");
					code = GetSmtpResponse(ReadBuffer(ns));

					if(code != 250 && code != 251)
						throw new SmtpException("Error setting receipient email address. (" + code.ToString() + ")");
				}

				WriteBuffer(ns, "data\r\n");
				code = GetSmtpResponse(ReadBuffer(ns));

				if(code != 354)
					throw new SmtpException("Error starting email body. (" + code.ToString() + ")");

				// Repeat the from and to addresses in the data section
				WriteBuffer(ns, "from:<" + msg.EmailFrom + ">\r\n");

				foreach(string sEmailTo in msg.EmailTo)
					WriteBuffer(ns, "to:<" + sEmailTo + ">\r\n");
				
				WriteBuffer(ns, "Subject:" + msg.EmailSubject + "\r\n");
				switch(msg.EmailMessageType)
				{
					case MessageType.Text:	// send text message
					{
						WriteBuffer(ns, "\r\n" + msg.EmailMessage + "\r\n.\r\n");
						break; 
					}
					case MessageType.HTML:
					{
						WriteBuffer(ns, "MIME-Version: 1.0\r\n");	// send HTML message
						WriteBuffer(ns, "Content-type: text/html\r\n");
						WriteBuffer(ns, "\r\n" + msg.EmailMessage + "\r\n.\r\n");
						break;
					}
				}

				code = GetSmtpResponse(ReadBuffer(ns));
				if(code != 250)
					throw new SmtpException("Error setting email body. (" + code.ToString() + ")");

				WriteBuffer(ns, "quit\r\n"); 				// QUIT
			}
		}

		#endregion

		#region Implementation
	
		static int GetSmtpResponse(string sResponse)
		{
			int response = 0;
			int iSpace = sResponse.IndexOf(" ");
			int iDash = sResponse.IndexOf("-");

			if(iDash > 0 && iDash < iSpace)
				iSpace = sResponse.IndexOf("-");

			try
			{
				if (iSpace > 0) 
					response = int.Parse(sResponse.Substring(0, iSpace));
			}
			catch	{}

			return response;
		}
		static void WriteBuffer(NetworkStream ns, string sBuffer)
		{
			try
			{
				byte[] buffer = Encoding.ASCII.GetBytes(sBuffer);
				ns.Write(buffer, 0, buffer.Length);
			}
			catch(System.IO.IOException)
			{
				throw new SmtpException("Error sending data to Smtp server.");
			}
		}
		static string ReadBuffer(NetworkStream ns)
		{
			byte[] buffer = new byte[1024];
			int i = 0;
			int b;
			int timeout = System.Environment.TickCount;

			try
			{
				while (!ns.DataAvailable && ((System.Environment.TickCount - timeout) < 20000))
					System.Threading.Thread.Sleep(100);

				if (!ns.DataAvailable)
					throw new SmtpException("No response received from Smtp server.");

				while (i < buffer.Length && ns.DataAvailable)		// read while there's data on the stream
				{
					b = ns.ReadByte();
					buffer[i++] = (byte)b;
				}
			}
			catch(System.IO.IOException)
			{
				throw new SmtpException("Error receiving data from Smtp server.");
			}

			return Encoding.ASCII.GetString(buffer);
		}
		#endregion
	}
}