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

namespace Twilio.Model
{
	public class PhoneNumberOptions
	{
        public string PhoneNumber { get; set; }
        public string AreaCode { get; set; }
		public string FriendlyName { get; set; }
		public string VoiceUrl { get; set; }
		public HttpMethod? VoiceMethod { get; set; }
		public string VoiceFallbackUrl { get; set; }
		public HttpMethod? VoiceFallbackMethod { get; set; }
		public string SmsUrl { get; set; }
		public HttpMethod? SmsMethod { get; set; }
		public string SmsFallbackUrl { get; set; }
		public HttpMethod? SmsFallbackMethod { get; set; }
		public bool? VoiceCallerIdLookup { get; set; }
		public string StatusCallback { get; set; }
		public HttpMethod? StatusCallbackMethod { get; set; }
	}
}