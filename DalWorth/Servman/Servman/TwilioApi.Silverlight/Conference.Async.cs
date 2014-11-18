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
using RestSharp.Extensions;
using Twilio.Model;

namespace Twilio
{
	public partial class TwilioApi
	{
		/// <summary>
		/// Returns a list of conferences within an account. The list includes paging information and is sorted by DateUpdated, with most recent conferences first.
		/// </summary>
		/// <param name="callback">Method to call upon successful completion</param>
		public void GetConferences(Action<ConferenceResult> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Conferences";

			ExecuteAsync<ConferenceResult>(request, (response) => callback(response));
		}

		/// <summary>
		/// Returns a list of conferences within an account. The list includes paging information and is sorted by DateUpdated, with most recent conferences first.
		/// </summary>
		/// <param name="options">List filter options. Only properties with values are included in request.</param>
		/// <param name="callback">Method to call upon successful completion</param>
		public void GetConferences(ConferenceListRequest options, Action<ConferenceResult> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Conferences";

			AddConferenceListOptions(options, request);

			ExecuteAsync<ConferenceResult>(request, (response) => callback(response));
		}

		private void AddConferenceListOptions(ConferenceListRequest options, RestRequest request)
		{
			if (options.Status.HasValue) request.AddParameter("Status", options.Status);
			if (options.FriendlyName.HasValue()) request.AddParameter("FriendlyName", options.FriendlyName);

			var dateCreatedParameterName = GetParameterNameWithEquality(options.DateCreatedComparison, "DateCreated");
			var dateUpdatedParameterName = GetParameterNameWithEquality(options.DateUpdatedComparison, "DateUpdated");

			if (options.DateCreated.HasValue) request.AddParameter(dateCreatedParameterName, options.DateCreated.Value.ToString("yyyy-MM-dd"));
			if (options.DateUpdated.HasValue) request.AddParameter(dateUpdatedParameterName, options.DateUpdated.Value.ToString("yyyy-MM-dd"));

			if (options.Count.HasValue) request.AddParameter("num", options.Count.Value);
			if (options.PageNumber.HasValue) request.AddParameter("page", options.PageNumber.Value);
		}

		/// <summary>
		/// Retrieve details for specific conference
		/// </summary>
		/// <param name="conferenceSid">The Sid of the conference to retrieve</param>
		/// <param name="callback">Method to call upon successful completion</param>
		public void GetConference(string conferenceSid, Action<Conference> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Conferences/{ConferenceSid}";
			request.RootElement = "Conference";

			request.AddParameter("ConferenceSid", conferenceSid);

			ExecuteAsync<Conference>(request, (response) => callback(response));
		}

		/// <summary>
		/// Retrieve a list of conference participants
		/// </summary>
		/// <param name="conferenceSid">The Sid of the conference</param>
		/// <param name="muted">Set to null to retrieve all, true to retrieve muted, false to retrieve unmuted</param>
		/// <param name="callback">Method to call upon successful completion</param>
		public void GetConferenceParticipants(string conferenceSid, bool? muted, Action<ParticipantResult> callback)
		{
			GetConferenceParticipants(conferenceSid, muted, null, null, callback);
		}

		/// <summary>
		/// Retrieve a list of conference participants
		/// </summary>
		/// <param name="conferenceSid">The Sid of the conference</param>
		/// <param name="muted">Set to null to retrieve all, true to retrieve muted, false to retrieve unmuted</param>
		/// <param name="pageNumber">Which page number to start retrieving from</param>
		/// <param name="count">How many participants to retrieve</param>
		/// <param name="callback">Method to call upon successful completion</param>
		public void GetConferenceParticipants(string conferenceSid, bool? muted, int? pageNumber, int? count, Action<ParticipantResult> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Conferences/{ConferenceSid}/Participants";

			request.AddParameter("ConferenceSid", conferenceSid);

			if (muted.HasValue) request.AddParameter("Muted", muted.Value);
			if (pageNumber.HasValue) request.AddParameter("page", pageNumber.Value);
			if (count.HasValue) request.AddParameter("num", count.Value);

			ExecuteAsync<ParticipantResult>(request, (response) => callback(response));
		}

		/// <summary>
		/// Retrieve a single conference participant by their CallSid
		/// </summary>
		/// <param name="conferenceSid">The Sid of the conference</param>
		/// <param name="callSid">The Sid of the call instance</param>
		/// <param name="callback">Method to call upon successful completion</param>
		public void GetConferenceParticipant(string conferenceSid, string callSid, Action<Participant> callback)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Conferences/{ConferenceSid}/Participants/{CallSid}";
			request.RootElement = "Participant";

			request.AddParameter("ConferenceSid", conferenceSid);
			request.AddParameter("CallSid", callSid);

			ExecuteAsync<Participant>(request, (response) => callback(response));
		}

		/// <summary>
		/// Change a participant of a conference to be muted
		/// </summary>
		/// <param name="conferenceSid">The Sid of the conference</param>
		/// <param name="callSid">The Sid of the call to mute</param>
		/// <param name="callback">Method to call upon successful completion</param>
		public void MuteConferenceParticipant(string conferenceSid, string callSid, Action<Participant> callback)
		{
			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/Conferences/{ConferenceSid}/Participants/{CallSid}";
			request.RootElement = "Participant";

			request.AddParameter("ConferenceSid", conferenceSid);
			request.AddParameter("CallSid", callSid);
			request.AddParameter("Muted", true);

			ExecuteAsync<Participant>(request, (response) => callback(response));
		}

		/// <summary>
		/// Change a participant of a conference to be unmuted
		/// </summary>
		/// <param name="conferenceSid">The Sid of the conference</param>
		/// <param name="callSid">The Sid of the call to unmute</param>
		/// <param name="callback">Method to call upon successful completion</param>
		public void UnmuteConferenceParticipant(string conferenceSid, string callSid, Action<Participant> callback)
		{
			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/Conferences/{ConferenceSid}/Participants/{CallSid}";
			request.RootElement = "Participant";

			request.AddParameter("ConferenceSid", conferenceSid);
			request.AddParameter("CallSid", callSid);
			request.AddParameter("Muted", false);

			ExecuteAsync<Participant>(request, (response) => callback(response));
		}

		/// <summary>
		/// Remove a caller from a conference
		/// </summary>
		/// <param name="conferenceSid">The Sid of the conference</param>
		/// <param name="callSid">The Sid of the call to remove</param>
		/// <param name="callback">Method to call upon successful completion</param>
		public void KickParticipantFromConference(string conferenceSid, string callSid, Action<bool> callback)
		{
			var request = new RestRequest(Method.POST);
			request.Resource = "Accounts/{AccountSid}/Conferences/{ConferenceSid}/Participants/{CallSid}";
			request.AddParameter("ConferenceSid", conferenceSid);
			request.AddParameter("CallSid", callSid);

			ExecuteAsync(request, (response) => callback(response.StatusCode == System.Net.HttpStatusCode.NoContent));
		}
	}
}