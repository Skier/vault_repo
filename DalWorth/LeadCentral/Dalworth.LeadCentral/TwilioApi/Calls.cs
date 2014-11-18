﻿#region License
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

using RestSharp;
using RestSharp.Extensions;
using RestSharp.Validation;
using Twilio.Model;

namespace Twilio
{
	public partial class TwilioApi
	{
		/// <summary>
		/// Returns a paged list of phone calls made to and from the account.
		/// Sorted by DateUpdated with most-recent calls first.
		/// </summary>
		public CallResult GetCalls()
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Calls";

			return Execute<CallResult>(request);
		}

		/// <summary>
		/// Returns a paged list of phone calls made to and from the account.
		/// Sorted by DateUpdated with most-recent calls first.
		/// </summary>
		/// <param name="options">List filter options. If an property is set the list will be filtered by that value.</param>
		public CallResult GetCalls(CallListRequest options)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Calls";

			AddCallListOptions(options, request);

			return Execute<CallResult>(request);
		}

		/// <summary>
		/// Returns the single Call resource identified by {CallSid}
		/// </summary>
		/// <param name="callSid">The Sid of the Call resource to retrieve</param>
		public Call GetCall(string callSid)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Calls/{CallSid}";
			request.RootElement = "Call";

			request.AddParameter("CallSid", callSid, ParameterType.UrlSegment);

			return Execute<Call>(request);
		}

		/// <summary>
		/// Initiates a new phone call.
		/// </summary>
		/// <param name="from">The phone number to use as the caller id. Format with a '+' and country code e.g., +16175551212 (E.164 format). Must be a Twilio number or a valid outgoing caller id for your account.</param>
		/// <param name="to">The number to call formatted with a '+' and country code e.g., +16175551212 (E.164 format). Twilio will also accept unformatted US numbers e.g., (415) 555-1212, 415-555-1212.</param>
		/// <param name="url">The fully qualified URL that should be consulted when the call connects. Just like when you set a URL for your inbound calls. URL should return TwiML.</param>
		public Call InitiateOutboundCall(string from, string to, string url)
		{
			return InitiateOutboundCall(from, to, url, string.Empty);
		}

		/// <summary>
		/// Initiates a new phone call.
		/// </summary>
		/// <param name="from">The phone number to use as the caller id. Format with a '+' and country code e.g., +16175551212 (E.164 format). Must be a Twilio number or a valid outgoing caller id for your account.</param>
		/// <param name="to">The number to call formatted with a '+' and country code e.g., +16175551212 (E.164 format). Twilio will also accept unformatted US numbers e.g., (415) 555-1212, 415-555-1212.</param>
		/// <param name="url">The fully qualified URL that should be consulted when the call connects. Just like when you set a URL for your inbound calls. URL should return TwiML.</param>
		/// <param name="statusCallback">A URL that Twilio will request when the call ends to notify your app.</param>
		public Call InitiateOutboundCall(string from, string to, string url, string statusCallback)
		{
			return InitiateOutboundCall(new CallOptions
			{
				From = from,
				To = to,
				Url = url,
				StatusCallback = statusCallback
			});
		}

		/// <summary>
		/// Initiates a new phone call.
		/// </summary>
		/// <param name="options">Call settings. Only properties with values set will be used.</param>
		public Call InitiateOutboundCall(CallOptions options)
		{
			Require.Argument("From", options.From);
			Require.Argument("To", options.To);
			Require.Argument("Url", options.Url);

			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/Calls";
			request.RootElement = "Calls";

			AddCallOptions(options, request);

			return Execute<Call>(request);
		}

		/// <summary>
		/// Hangs up a call in progress.
		/// </summary>
		/// <param name="callSid">The Sid of the call to hang up.</param>
		/// <param name="style">'Canceled' will attempt to hangup calls that are queued or ringing but not affect calls already in progress. 'Completed' will attempt to hang up a call even if it's already in progress.</param>
		public Call HangupCall(string callSid, HangupStyle style)
		{
			Require.Argument("CallSid", callSid);

			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/Calls/{CallSid}";
			request.RootElement = "Call";

			request.AddUrlSegment("CallSid", callSid);
			request.AddParameter("Status", style.ToString().ToLower());

			return Execute<Call>(request);
		}

		/// <summary>
		/// Redirect a call in progress to a new TwiML URL
		/// </summary>
		/// <param name="callSid">The Sid of the call to redirect</param>
		/// <param name="redirectUrl">The URL to redirect the call to.</param>
		/// <param name="redirectMethod">The HTTP method to use when requesting the redirectUrl</param>
		public Call RedirectCall(string callSid, string redirectUrl, Method? redirectMethod)
		{
			Require.Argument("CallSid", callSid);
			Require.Argument("CurrentUrl", redirectUrl);

			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/Calls/{CallSid}";
			request.RootElement = "Call";

			request.AddParameter("CallSid", callSid, ParameterType.UrlSegment);
			request.AddParameter("CurrentUrl", redirectUrl);
			if (redirectMethod.HasValue) request.AddParameter("CurrentMethod", redirectMethod.Value);

			return Execute<Call>(request);
		}
	}
}