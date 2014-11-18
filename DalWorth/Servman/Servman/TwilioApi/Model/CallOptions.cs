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

namespace Twilio.Model
{
	public class CallOptions
	{
		/// <summary>
		/// The phone number to use as the caller id. Format with a '+' and country code e.g., +16175551212 (E.164 format). Must be a Twilio number or a valid outgoing caller id for your account.
		/// </summary>
		public string From { get; set; }
		/// <summary>
		/// The number to call formatted with a '+' and country code e.g., +16175551212 (E.164 format). Twilio will also accept unformatted US numbers e.g., (415) 555-1212, 415-555-1212.
		/// </summary>
		public string To { get; set; }
		/// <summary>
		/// The fully qualified URL that should be consulted when the call connects. Just like when you set a URL for your inbound calls.
		/// </summary>
		public string Url { get; set; }
		/// <summary>
		/// A URL that Twilio will request when the call ends to notify your app.
		/// </summary>
		public string StatusCallback { get; set; }
		/// <summary>
		/// The HTTP method Twilio should use when requesting the above URL. Defaults to POST.
		/// </summary>
		public HttpMethod? StatusCallbackMethod { get; set; }
		/// <summary>
		/// The HTTP method Twilio should use when requesting the required Url parameter's value above. Defaults to POST.
		/// </summary>
		public HttpMethod? Method { get; set; }
		/// <summary>
		/// A string of keys to dial after connecting to the number. Valid digits in the string include: any digit (0-9), '#' and '*'. For example, if you connected to a company phone number, and wanted to dial extension 1234 and then the pound key, use SendDigits=1234#. Remember to URL-encode this string, since the '#' character has special meaning in a URL.
		/// </summary>
		public string SendDigits { get; set; }
		/// <summary>
		/// Tell Twilio to try and determine if a machine (like voicemail) or a human has answered the call. Possible values are Continue and Hangup.
		/// </summary>
		public IfMachine? IfMachine { get; set; }
		/// <summary>
		/// The integer number of seconds that Twilio should allow the phone to ring before assuming there is no answer. Default is 60 seconds, the maximum is 999 seconds. Note, you could set this to a low value, such as 15, to hangup before reaching an answering machine or voicemail.
		/// </summary>
		public int? Timeout { get; set; }
		/// <summary>
		/// A URL that Twilio will request if an error occurs requesting or executing the TwiML at Url.
		/// </summary>
		public string FallbackUrl { get; set; }
		/// <summary>
		/// The HTTP method that Twilio should use to request the FallbackUrl. Must be either GET or POST. Defaults to POST.
		/// </summary>
		public HttpMethod? FallbackMethod { get; set; }
	}
}
