/*
 * Copyright (c) 2009-2010 Intuit, Inc.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 *
 * Contributors:
 *    Intuit Partner Platform – initial contribution
 */

using System;

namespace Intuit.Platform.Client.Core
{
	/// <summary>
	/// Wraps an error returned from the platform backend by a QuickBase or Workplace XML over HTTP API call.
	/// </summary>
	public class PlatformApiXmlHttpError : PlatformClientException
	{
		#region ErrorCodes enum

		/// <summary>
		/// Error codes defined by the platform.
		/// </summary>
		public enum ErrorCodes
		{
#pragma warning disable 1591
			NoError = 0,
			UnknownError = 1,
			InvalidInput = 2,
			InsufficientPermissions = 3,
			BadTicket = 4,
			UnimplementedOperation = 5,
			SyntaxError = 6,
			ApiNotAllowedOnThisApplicationTable = 7,
			SslRequiredForThisApplicationTable = 8,
			InvalidChoice = 9,
			InvalidFieldType = 10,
			CouldNotParseXmlInput = 11,
			InvalidSourceDbId = 12,
			InvalidAccountId = 13,
			MissingDbInfoOrOfWrongType = 14,
			InvalidHostname = 15,
			UnknownUserPassword = 20,
			UnknownUser = 21,
			SigninRequired = 22,
			FeatureNotSupported = 23,
			InvalidApplicationToken = 24,
			DuplicateDeveloperKey = 25,
			MaxCount = 26,
			RegistrationRequired = 27,
			ManagedByLdap = 28,
			NoSuchRecord = 30,
			NoSuchField = 31,
			AppDoesntExistOrDeleted = 32,
			NoSuchQuery = 33,
			YouCantChangeValueOfThisField = 34,
			NoDataReturned = 35,
			CloningError = 36,
			NoSuchReport = 37,
			PeriodicReportContainsRestrictedField = 38,
			MissingRequiredField = 50,
			AttemptToAddNonUniqueValueToFieldMarkedUnique = 51,
			DuplicateField = 52,
			TheseRequiredFieldsMissingFromImportData = 53,
			CachedListOfRecordsNotFound = 54,
			IAMLinkRequired = 55,
			UpdateConflictDetected = 60,
			SchemaIsLocked = 61,
			AccountSizeLimitExceeded = 70,
			DatabaseSizeLimitExceeded = 71,
			AccountSuspended = 73,
			NotAllowedToCreateApplications = 74,
			ViewTooLarge = 75,
			TooManyCriteriaInQuery = 76, // see PlatformSessionContext.MaxQueryParameters
			ApiRequestLimitExceeded = 77,
			DataLimitExceeded = 78,
			Overflow = 80,
			ItemNotFound = 81,
			OperationTookTooLong = 82,
			AccessDenied = 83,
			DatabaseError = 84,
			SchemaUpdateError = 85,
			TechnicalDifficulties = 100, // try again later
			InvalidRole = 110,
			UserExists = 111,
			NoUserInRole = 112,
			UserAlreadyInRole = 113,
			MustBeAdminUser = 114,
			QARFileDoesNotExist = 120,
			UpgradePlan = 150,
			ExpiredPlan = 151,
			AppSuspended = 152,
#pragma warning restore 1591
		}

		#endregion

		private readonly int m_ErrorCode;

		internal PlatformApiXmlHttpError(IPlatformHost host, int errorCode)
			: base(host)
		{
			m_ErrorCode = errorCode;
			CheckIfCanAdviseRetry();
		}

		private void CheckIfCanAdviseRetry()
		{
			switch ((ErrorCodes)m_ErrorCode)
			{
				case ErrorCodes.TechnicalDifficulties:
				case ErrorCodes.UnknownUserPassword:
				case ErrorCodes.UnknownUser:
					FixAndRetryAdvised = true;
					break;
				default:
					break;
			}
		}

		internal PlatformApiXmlHttpError(IPlatformHost host, int errorCode, string msg)
			: this(host, errorCode, msg, null)
		{
		}

		internal PlatformApiXmlHttpError(IPlatformHost host, int errorCode, string msg, Exception inner)
			: base(host, msg, inner)
		{
			m_ErrorCode = errorCode;
			CheckIfCanAdviseRetry();
		}

		/// <exception cref="PlatformApiXmlHttpError"><c>PlatformApiXmlHttpError</c>.</exception>
		public PlatformApiXmlHttpError Rethrower()
		{
			return new PlatformApiXmlHttpError(Host, ErrorCode, Message, this);
		}

		/// <summary>
		/// Checks to see if the error code of this error was the one specified
		/// </summary>
		public bool IsErrorCode(ErrorCodes errorCode)
		{
			return ((int)errorCode) == ErrorCode;
		}

		/// <summary>
		/// Error code reported by the platform
		/// </summary>
		public int ErrorCode
		{
			get
			{
				return m_ErrorCode;
			}
		}
	}
}