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
using System.Linq;
using RestSharp;
using Twilio.Model;

namespace Twilio
{
	public partial class TwilioApi
	{
		private const string BaseUrl = "https://api.twilio.com/2010-04-01";
		private RestClient _client;

		private readonly string _accountSid;
		private readonly string _authToken;
		public TwilioApi(string accountSid, string authToken)
		{
			_accountSid = accountSid;
			_authToken = authToken;

			_client = new RestClient();
			_client.Authenticator = new HttpBasicAuthenticator(_accountSid, _authToken);
			_client.BaseUrl = BaseUrl;
		}

#if FRAMEWORK
		public T Execute<T>(RestRequest request) where T : new()
		{
			AddAccountSidIfNotSet(request);
			var response = _client.Execute<T>(request);
			return response.Data;
		}

		public RestResponse Execute(RestRequest request)
		{
			AddAccountSidIfNotSet(request);
			return _client.Execute(request);
		}
#endif

		private string GetParameterNameWithEquality(ComparisonType? comparisonType, string parameterName)
		{
			if (comparisonType.HasValue)
			{
				switch (comparisonType)
				{
					case ComparisonType.GreaterThanOrEqualTo:
						parameterName += ">";
						break;
					case ComparisonType.LessThanOrEqualTo:
						parameterName += "<";
						break;
				}
			}
			return parameterName;
		}
    
        private void AddAccountSidIfNotSet(RestRequest request)
        {
            // Add AccountSid parameter if not already set
            // check is required to allow retrieving subaccount detail
            if (!request.Parameters.Any(p => p.Name == "AccountSid" && p.Type == ParameterType.UrlSegment))
            {
                request.AddParameter("AccountSid", _accountSid, ParameterType.UrlSegment);
            }
        }

    }
}