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
		/// Retrieve the details for an incoming phone number
		/// </summary>
		/// <param name="incomingPhoneNumberSid">The Sid of the number to retrieve</param>
		public IncomingPhoneNumber GetIncomingPhoneNumber(string incomingPhoneNumberSid)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers/{IncomingPhoneNumberSid}";
			request.RootElement = "IncomingPhoneNumber";

			request.AddParameter("IncomingPhoneNumberSid", incomingPhoneNumberSid, ParameterType.UrlSegment);

			return Execute<IncomingPhoneNumber>(request);
		}

		/// <summary>
		/// List all incoming phone numbers on current account
		/// </summary>
		public IncomingPhoneNumberResult GetIncomingPhoneNumbers()
		{
			return GetIncomingPhoneNumbers(null, null, null, null);
		}

		/// <summary>
		/// List incoming phone numbers on current account with filters
		/// </summary>
		/// <param name="phoneNumber">Optional phone number to match</param>
		/// <param name="friendlyName">Optional friendly name to match</param>
		/// <param name="pageNumber">Page number to start retrieving results from</param>
		/// <param name="count">How many results to return</param>
		public IncomingPhoneNumberResult GetIncomingPhoneNumbers(string phoneNumber, string friendlyName, int? pageNumber, int? count)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers";

			if (phoneNumber.HasValue()) request.AddParameter("PhoneNumber", phoneNumber);
			if (friendlyName.HasValue()) request.AddParameter("FriendlyName", friendlyName);

			if (pageNumber.HasValue) request.AddParameter("page", pageNumber.Value);
			if (count.HasValue) request.AddParameter("num", count.Value);

			return Execute<IncomingPhoneNumberResult>(request);
		}

		/// <summary>
		/// List all local phone numbers on current account
		/// </summary>
		public IncomingPhoneNumberResult GetLocalIncomingPhoneNumbers()
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers/Local";
			request.RootElement = "IncomingPhoneNumbers";

			return Execute<IncomingPhoneNumberResult>(request);
		}

		/// <summary>
		/// Purchase/provision a local phone number
		/// </summary>
		/// <param name="options">Optional parameters to use when purchasing number</param>
		public IncomingPhoneNumber AddLocalPhoneNumber(PhoneNumberOptions options)
		{
			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers";
			request.RootElement = "IncomingPhoneNumber";

			AddPhoneNumberOptionsToRequest(request, options);
			AddSmsOptionsToRequest(request, options);

			return Execute<IncomingPhoneNumber>(request);
		}

		/// <summary>
		/// List all toll-free numbers on current account
		/// </summary>
		public IncomingPhoneNumberResult GetTollFreeIncomingPhoneNumbers()
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers/TollFree";
			request.RootElement = "IncomingPhoneNumbers";

			return Execute<IncomingPhoneNumberResult>(request);
		}

		/// <summary>
		/// Purchase/provision a toll-free number
		/// </summary>
		/// <param name="options">Optional parameters to include when purchasing number</param>
		public IncomingPhoneNumber AddTollFreePhoneNumber(PhoneNumberOptions options)
		{
			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers";
			request.RootElement = "IncomingPhoneNumber";

			AddPhoneNumberOptionsToRequest(request, options);
            AddSmsOptionsToRequest(request, options);

			return Execute<IncomingPhoneNumber>(request);
		}

		/// <summary>
		/// Update the settings of an incoming phone number
		/// </summary>
		/// <param name="incomingPhoneNumberSid">The Sid of the phone number to update</param>
		/// <param name="options">Which settings to update. Only properties with values set will be updated.</param>
		public IncomingPhoneNumber UpdateIncomingPhoneNumber(string incomingPhoneNumberSid, PhoneNumberOptions options)
		{
			Require.Argument("IncomingPhoneNumberSid", incomingPhoneNumberSid);

			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers/{IncomingPhoneNumberSid}";
			request.RootElement = "IncomingPhoneNumber";

			request.AddParameter("IncomingPhoneNumberSid", incomingPhoneNumberSid, ParameterType.UrlSegment);
			AddPhoneNumberOptionsToRequest(request, options);
            AddSmsOptionsToRequest(request, options);

			return Execute<IncomingPhoneNumber>(request);
		}

		/// <summary>
		/// Remove (deprovision) a phone number from the current account
		/// </summary>
		/// <param name="incomingPhoneNumberSid">The Sid of the number to remove</param>
		public RestResponse DeleteIncomingPhoneNumber(string incomingPhoneNumberSid)
		{
			Require.Argument("IncomingPhoneNumberSid", incomingPhoneNumberSid);
			var request = new RestRequest(Method.DELETE);
			request.Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers/{IncomingPhoneNumberSid}";

			request.AddParameter("IncomingPhoneNumberSid", incomingPhoneNumberSid, ParameterType.UrlSegment);

			return Execute(request);
		}

        private void AddPhoneNumberOptionsToRequest(RestRequest request, PhoneNumberOptions options)
        {
            if (options.PhoneNumber.HasValue()) request.AddParameter("PhoneNumber", options.PhoneNumber);
            if (options.AreaCode.HasValue()) request.AddParameter("AreaCode", options.AreaCode);
            if (options.FriendlyName.HasValue())
            {
                Validate.IsValidLength(options.FriendlyName, 64);
                request.AddParameter("FriendlyName", options.FriendlyName);
            }
            if (options.VoiceUrl != null) request.AddParameter("VoiceUrl", options.VoiceUrl);
            if (options.VoiceMethod != null) request.AddParameter("VoiceMethod", options.VoiceMethod.ToString());
            if (options.VoiceFallbackUrl != null) request.AddParameter("VoiceFallbackUrl", options.VoiceFallbackUrl);
            if (options.VoiceFallbackMethod != null) request.AddParameter("VoiceFallbackMethod", options.VoiceFallbackMethod.ToString());
            if (options.VoiceCallerIdLookup != null) request.AddParameter("VoiceCallerIdLookup", (options.VoiceCallerIdLookup.Value ? "true" : ""));
            if (options.StatusCallback != null) request.AddParameter("StatusCallback", options.StatusCallback);
            if (options.StatusCallbackMethod != null) request.AddParameter("StatusCallbackMethod", options.StatusCallbackMethod.ToString());
        }

        private void AddSmsOptionsToRequest(RestRequest request, PhoneNumberOptions options)
        {
            if (options.SmsUrl != null) request.AddParameter("SmsUrl", options.SmsUrl);
            if (options.SmsMethod != null) request.AddParameter("SmsMethod", options.SmsMethod.ToString());
            if (options.SmsFallbackUrl != null) request.AddParameter("SmsFallbackUrl", options.SmsFallbackUrl);
            if (options.SmsFallbackMethod != null) request.AddParameter("SmsFallbackMethod", options.SmsFallbackMethod.ToString());
        }
    }
}