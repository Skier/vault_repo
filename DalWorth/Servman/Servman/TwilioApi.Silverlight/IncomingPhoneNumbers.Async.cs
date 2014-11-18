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
		/// <param name="callback">Method to call upon successful completion</param>
		public void GetIncomingPhoneNumber(string incomingPhoneNumberSid, Action<IncomingPhoneNumber> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers/{IncomingPhoneNumberSid}";
			request.RootElement = "IncomingPhoneNumber";

			request.AddParameter("IncomingPhoneNumberSid", incomingPhoneNumberSid, ParameterType.UrlSegment);

			ExecuteAsync<IncomingPhoneNumber>(request, (response) => callback(response));
		}

		/// <summary>
		/// List all incoming phone numbers on current account
		/// </summary>
		/// <param name="callback">Method to call upon successful completion</param>
		public void GetIncomingPhoneNumbers(Action<IncomingPhoneNumberResult> callback)
		{
			GetIncomingPhoneNumbers(null, null, null, null, callback);
		}

		/// <summary>
		/// List incoming phone numbers on current account with filters
		/// </summary>
		/// <param name="phoneNumber">Optional phone number to match</param>
		/// <param name="friendlyName">Optional friendly name to match</param>
		/// <param name="pageNumber">Page number to start retrieving results from</param>
		/// <param name="count">How many results to return</param>
		/// <param name="callback">Method to call upon successful completion</param>
		public void GetIncomingPhoneNumbers(string phoneNumber, string friendlyName, int? pageNumber, int? count, Action<IncomingPhoneNumberResult> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers";

			if (phoneNumber.HasValue()) request.AddParameter("PhoneNumber", phoneNumber);
			if (friendlyName.HasValue()) request.AddParameter("FriendlyName", friendlyName);

			if (pageNumber.HasValue) request.AddParameter("page", pageNumber.Value);
			if (count.HasValue) request.AddParameter("num", count.Value);

			ExecuteAsync<IncomingPhoneNumberResult>(request, (response) => callback(response));
		}

		/// <summary>
		/// List all local phone numbers on current account
		/// </summary>
		/// <param name="callback">Method to call upon successful completion</param>
		public void GetLocalIncomingPhoneNumbers(Action<IncomingPhoneNumberResult> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers/Local";
			request.RootElement = "IncomingPhoneNumbers";

			ExecuteAsync<IncomingPhoneNumberResult>(request, (response) => callback(response));
		}

		/// <summary>
		/// Purchase/provision a local phone number
		/// </summary>
		/// <param name="options">Optional parameters to use when purchasing number</param>
		/// <param name="callback">Method to call upon successful completion</param>
		public void AddLocalPhoneNumber(PhoneNumberOptions options, Action<IncomingPhoneNumber> callback)
		{
			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers";
			request.RootElement = "IncomingPhoneNumber";

			AddPhoneNumberOptionsToRequest(request, options);
			AddSmsOptionsToRequest(request, options);

			ExecuteAsync<IncomingPhoneNumber>(request, (response) => callback(response));
		}

		/// <summary>
		/// List all toll-free numbers on current account
		/// </summary>
		/// <param name="callback">Method to call upon successful completion</param>
		public void GetTollFreeIncomingPhoneNumbers(Action<IncomingPhoneNumberResult> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers/TollFree";
			request.RootElement = "IncomingPhoneNumbers";

			ExecuteAsync<IncomingPhoneNumberResult>(request, (response) => callback(response));
		}

		/// <summary>
		/// Purchase/provision a toll-free number
		/// </summary>
		/// <param name="options">Optional parameters to include when purchasing number</param>
		/// <param name="callback">Method to call upon successful completion</param>
		public void AddTollFreePhoneNumber(PhoneNumberOptions options, Action<IncomingPhoneNumber> callback)
		{
			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers";
			request.RootElement = "IncomingPhoneNumber";

			AddPhoneNumberOptionsToRequest(request, options);
            AddSmsOptionsToRequest(request, options);

			ExecuteAsync<IncomingPhoneNumber>(request, (response) => callback(response));
		}

		/// <summary>
		/// Update the settings of an incoming phone number
		/// </summary>
		/// <param name="incomingPhoneNumberSid">The Sid of the phone number to update</param>
		/// <param name="options">Which settings to update. Only properties with values set will be updated.</param>
		/// <param name="callback">Method to call upon successful completion</param>
		public void UpdateIncomingPhoneNumber(string incomingPhoneNumberSid, PhoneNumberOptions options, Action<IncomingPhoneNumber> callback)
		{
			Require.Argument("IncomingPhoneNumberSid", incomingPhoneNumberSid);

			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers/{IncomingPhoneNumberSid}";
			request.RootElement = "IncomingPhoneNumber";

			request.AddParameter("IncomingPhoneNumberSid", incomingPhoneNumberSid, ParameterType.UrlSegment);
			AddPhoneNumberOptionsToRequest(request, options);
            AddSmsOptionsToRequest(request, options);

			ExecuteAsync<IncomingPhoneNumber>(request, (response) => callback(response));
		}

		/// <summary>
		/// Remove (deprovision) a phone number from the current account
		/// </summary>
		/// <param name="incomingPhoneNumberSid">The Sid of the number to remove</param>
		/// <param name="callback">Method to call upon successful completion</param>
		public void DeleteIncomingPhoneNumber(string incomingPhoneNumberSid, Action<RestResponse> callback)
		{
			Require.Argument("IncomingPhoneNumberSid", incomingPhoneNumberSid);
			var request = new RestRequest(Method.DELETE);
			request.Resource = "Accounts/{AccountSid}/IncomingPhoneNumbers/{IncomingPhoneNumberSid}";

			request.AddParameter("IncomingPhoneNumberSid", incomingPhoneNumberSid, ParameterType.UrlSegment);

			ExecuteAsync(request, (response) => callback(response));
		}
	}
}