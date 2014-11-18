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
		/// Returns a set of Transcriptions that includes paging information, sorted by 'DateUpdated', with most recent transcripts first.
		/// </summary>
		public TranscriptionResult GetTranscriptions()
		{
			return GetTranscriptions(null, null);
		}

		/// <summary>
		/// Returns a paged set of Transcriptions that includes paging information, sorted by 'DateUpdated', with most recent transcripts first.
		/// </summary>
		/// <param name="pageNumber">The page to start retrieving results from</param>
		/// <param name="count">The number of results to retrieve</param>
		public TranscriptionResult GetTranscriptions(int? pageNumber, int? count)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Transcriptions";
			if (pageNumber.HasValue) request.AddParameter("page", pageNumber.Value);
			if (count.HasValue) request.AddParameter("num", count.Value);

			return Execute<TranscriptionResult>(request);
		}

		/// <summary>
		/// Returns a set of Transcriptions for a specific recording that includes paging information, sorted by 'DateUpdated', with most recent transcripts first.
		/// </summary>
		/// <param name="recordingSid"></param>
		/// <param name="pageNumber"></param>
		/// <param name="count"></param>
		public TranscriptionResult GetTranscriptions(string recordingSid, int? pageNumber, int? count)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Recordings/{RecordingSid}/Transcriptions";
			request.AddUrlSegment("RecordingSid", recordingSid);

			if (pageNumber.HasValue) request.AddParameter("page", pageNumber.Value);
			if (count.HasValue) request.AddParameter("num", count.Value);

			return Execute<TranscriptionResult>(request);
		}

		/// <summary>
		/// Retrieve the details of a single transcription
		/// </summary>
		/// <param name="transcriptionSid">The Sid of the transcription to retrieve</param>
		public Transcription GetTranscription(string transcriptionSid)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Transcriptions/{TranscriptionSid}";
			request.RootElement = "Transcription";
			request.AddParameter("TranscriptionSid", transcriptionSid, ParameterType.UrlSegment);

			return Execute<Transcription>(request);
		}

		/// <summary>
		/// Retrieve the text of a single transcription
		/// </summary>
		/// <param name="transcriptionSid">The Sid of the transcription to retrieve</param>
		public string GetTranscriptionText(string transcriptionSid)
		{
			var request = new RestRequest();
			request.Resource = "Accounts/{AccountSid}/Transcriptions/{TranscriptionSid}.txt";
			request.AddParameter("TranscriptionSid", transcriptionSid, ParameterType.UrlSegment);

			var response = Execute(request);
			return response.Content;
		}
	}
}