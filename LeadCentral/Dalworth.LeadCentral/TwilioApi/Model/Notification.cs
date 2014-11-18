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

namespace Twilio.Model
{
	public class Notification : TwilioBase
	{
		/// <summary>
		/// 
		/// </summary>
		public string Sid { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime DateCreated { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime DateUpdated { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string AccountSid { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CallSid { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ApiVersion { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int Log { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ErrorCode { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string MoreInfo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string MessageText { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime MessageDate { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string RequestUrl { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string RequestMethod { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string RequestVariables { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ResponseHeaders { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ResponseBody { get; set; }
	}
}