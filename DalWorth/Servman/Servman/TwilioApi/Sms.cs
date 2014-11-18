#region License
//   Copyright 2010 John Sheehan
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License. 
#endregion

using System;
using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;
using Twilio.Model;

namespace Twilio
{
	public partial class TwilioApi
	{
		/// <summary>
		/// Retrieve the details for a specific SMS message instance
		/// </summary>
		/// <param name="smsMessageSid">The Sid of the message to retrieve</param>
		public SmsMessage GetSmsMessage(string smsMessageSid)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/SMS/Messages/{SMSMessageSid}";
			request.RootElement = "SMSMessage";
			request.AddParameter("SMSMessageSid", smsMessageSid);

			return Execute<SmsMessage>(request);
		}

		/// <summary>
		/// Returns a list of SMS messages. The list includes paging information and is sorted by DateSent, with most recent messages first.
		/// </summary>
		public SmsMessageResult GetSmsMessages()
		{
			return GetSmsMessages(null, null, null, null, null);
		}

		/// <summary>
		/// Returns a filtered list of SMS messages. The list includes paging information and is sorted by DateSent, with most recent messages first.
		/// </summary>
		/// <param name="to">(Optional) The phone number of the message recipient</param>
		/// <param name="from">(Optional) The phone number of the message sender</param>
		/// <param name="dateSent">(Optional) The date the message was sent (GMT)</param>
		/// <param name="pageNumber">(Optional) The page to start retrieving results from</param>
		/// <param name="count">(Optional) The number of results to retrieve</param>
		public SmsMessageResult GetSmsMessages(string to, string from, DateTime? dateSent, int? pageNumber, int? count)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/SMS/Messages";
			//request.RootElement = "SMSMessages";

			if (to.HasValue()) request.AddParameter("To", to);
			if (from.HasValue()) request.AddParameter("From", from);
			if (dateSent.HasValue) request.AddParameter("DateSent", dateSent.Value.ToString("yyyy-MM-dd"));
			if (pageNumber.HasValue) request.AddParameter("page", pageNumber.Value);
			if (count.HasValue) request.AddParameter("num", count.Value);

			return Execute<SmsMessageResult>(request);
		}

		/// <summary>
		/// Send a new SMS message to the specified recipients
		/// </summary>
		/// <param name="from">The phone number to send the message from. Must be a Twilio-provided or ported local (not toll-free) number. Validated outgoing caller IDs cannot be used.</param>
		/// <param name="to">The phone number to send the message to. If using the Sandbox, this number must be a validated outgoing caller ID</param>
		/// <param name="body">The message to send. Must be 160 characters or less.</param>
		public SmsMessage SendSmsMessage(string from, string to, string body)
		{
			return SendSmsMessage(from, to, body, string.Empty);
		}

		/// <summary>
		/// Send a new SMS message to the specified recipients
		/// </summary>
		/// <param name="from">The phone number to send the message from. Must be a Twilio-provided or ported local (not toll-free) number. Validated outgoing caller IDs cannot be used.</param>
		/// <param name="to">The phone number to send the message to. If using the Sandbox, this number must be a validated outgoing caller ID</param>
		/// <param name="body">The message to send. Must be 160 characters or less.</param>
		/// <param name="statusCallback">A URL that Twilio will POST to when your message is processed. Twilio will POST the SmsSid as well as SmsStatus=sent or SmsStatus=failed</param>
		public SmsMessage SendSmsMessage(string from, string to, string body, string statusCallback)
		{
			Validate.IsValidLength(body, 160);
			Require.Argument("from", from);
			Require.Argument("to", to);
			Require.Argument("body", body);

			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/SMS/Messages";
			request.AddParameter("From", from);
			request.AddParameter("To", to);
			request.AddParameter("Body", body);
			if (statusCallback.HasValue()) request.AddParameter("StatusCallback", statusCallback);

			return Execute<SmsMessage>(request);
		}
	}
}