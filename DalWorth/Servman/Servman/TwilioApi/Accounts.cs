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

using System;
using RestSharp;
using Twilio.Model;

namespace Twilio
{
	public partial class TwilioApi
	{
		/// <summary>
		/// Retrieve the account details for the currently authenticated account
		/// </summary>
		public Account GetAccount()
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}";
			request.RootElement = "Account";

			return Execute<Account>(request);
		}

		/// <summary>
		/// Retrieve the account details for a subaccount
		/// </summary>
		/// <param name="accountSid">The Sid of the subaccount to retrieve</param>
		public Account GetAccount(string accountSid)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}";
			request.RootElement = "Account";

			request.AddUrlSegment("AccountSid", accountSid);

			return Execute<Account>(request);
		}

		/// <summary>
		/// List all subaccounts created for the authenticated account
		/// </summary>
		public AccountResult ListSubAccounts()
		{
			var request = new RestRequest();
			request.Resource = "Accounts";

			return Execute<AccountResult>(request);
		}

		/// <summary>
		/// Creates a new subaccount under the authenticated account
		/// </summary>
		/// <param name="friendlyName">Name associated with this account for your own reference (can be empty string)</param>
		public Account CreateSubAccount(string friendlyName)
		{
			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts";
			request.RootElement = "Account";

			request.AddParameter("FriendlyName", friendlyName);

			return Execute<Account>(request);
		}

		/// <summary>
		/// Update the friendly name associated with the currently authenticated account
		/// </summary>
		/// <param name="friendlyName">Name to use when updating</param>
		public Account UpdateAccountName(string friendlyName)
		{
			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}";
			request.RootElement = "Account";
			request.AddParameter("FriendlyName", friendlyName);

			return Execute<Account>(request);
		}
	}
}