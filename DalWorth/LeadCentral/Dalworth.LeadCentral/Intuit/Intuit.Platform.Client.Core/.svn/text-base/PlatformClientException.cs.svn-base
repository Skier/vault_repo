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
using System.Net;
using Intuit.Common.Util;
using Intuit.Platform.Client.Core.Properties;

namespace Intuit.Platform.Client.Core
{
	/// <summary>
	/// An exception thrown by this client / dev kit code.
	/// </summary>
	public class PlatformClientException : Exception, IExceptionWrapper, IExceptionWithDisplayText, IExceptionKnowsIfRetryAdvised
	{

		/// <summary>
		/// Level/kind of error
		/// </summary>
		public enum ExceptionKind
		{
			/// <summary>
			/// General problem, user might be able to fix or recover by trying operation again.
			/// </summary>
			GeneralFixAndRetryAdvised,
			/// <summary>
			/// General unrecoverable problem.
			/// </summary>
			GeneralFatal,
			/// <summary>
			/// Error was returned by API call, user might be able to fix or recover by trying operation again.
			/// </summary>
			PlatformErrorFixAndRetryAdvised,
			/// <summary>
			/// Error was returned by API call, problem unrecoverable (in the context of this client)
			/// </summary>
			PlatformErrorFatal,
			/// <summary>
			/// A WebException occured and this exception wraps it
			/// </summary>
			WrapsWebException,
		}

		private readonly IPlatformHost m_Host;

		/// <summary>
		/// Whether or not this is an error the user can fix or can overcome by trying again later.
		/// </summary>
		public bool FixAndRetryAdvised { get; set; }

		/// <summary>
		/// New exception, caused by communicating with <paramref name="host"/>
		/// </summary>
		public PlatformClientException(IPlatformHost host)
		{
			m_Host = host;
		}

		/// <summary>
		/// New exception with error message <paramref name="msg"/>, caused by communicating with <paramref name="host"/>
		/// </summary>
		public PlatformClientException(IPlatformHost host, string msg)
			: base(msg)
		{
			m_Host = host;
		}

		/// <summary>
		/// New exception with error message <paramref name="msg"/> wrapping <paramref name="inner"/>, caused by communicating with <paramref name="host"/>
		/// </summary>
		public PlatformClientException(IPlatformHost host, string msg, Exception inner)
			: base(msg, inner)
		{
			m_Host = host;
		}

		///<summary>
		/// The kind of exception this is.
		///</summary>
		public ExceptionKind Kind
		{
			get
			{
				if (InnerException != null && InnerException is WebException)
				{
					return ExceptionKind.WrapsWebException;
				}
				if (this is PlatformApiXmlHttpError)
				{
					if (FixAndRetryAdvised)
					{
						return ExceptionKind.PlatformErrorFixAndRetryAdvised;
					}
					return ExceptionKind.PlatformErrorFatal;
				}
				if (FixAndRetryAdvised)
				{
					return ExceptionKind.GeneralFixAndRetryAdvised;
				}
				return ExceptionKind.GeneralFatal;
			}
		}

		/// <summary>
		/// A string that can by used as a title bar for an error message displaying this error
		/// </summary>
		/// <returns></returns>
		public string SuggestMessageCaption()
		{
			switch (Kind)
			{
				case ExceptionKind.GeneralFatal:
				case ExceptionKind.GeneralFixAndRetryAdvised:
					return Host == null ? Resources.PlatformClientException_SuggestMessageCaptionError_Error : String.Format(Resources.PlatformClientException_ErrorCommunicatingWith_Error_communicating_with__0_, Host.CustomerFacingName);
				case ExceptionKind.PlatformErrorFatal:
				case ExceptionKind.PlatformErrorFixAndRetryAdvised:
					// these should always have a host so no need for null check
					return String.Format(Resources.PlatformClientException_ErrorReportedBy_Error_reported_by__0_, Host.CustomerFacingName);
				case ExceptionKind.WrapsWebException:
					if (Host == null)
					{
						return Resources.PlatformClientException_WebConnectivityProblem_Web_connectivity_problem;
					}
					return String.Format(Resources.PlatformClientException_WebConnectivityProblem_Web_connectivity_problem + Resources.PlatformClientException_WebConnectivityProblemCommunicatingWith__communicating_with__0__, Host.CustomerFacingName);
			}
			return null;
		}

		/// <summary>
		/// Whether or not this exception wraps a <see cref="WebException"/>.
		/// </summary>
		public bool WrapsWebException
		{
			get
			{
				return Kind == ExceptionKind.WrapsWebException;
			}
		}

		/// <summary>
		/// Wraps the exception <paramref name="e"/> if it's a <see cref="WebException"/>.
		/// </summary>
		public Exception WrapException(Exception e)
		{
			WebException we = e as WebException;
			if (we != null)
			{
				return WrapWebIntoClientException(we);
			}
			return null;
		}

		/// <summary>
		/// Wraps the given <see cref="WebException"/>.
		/// </summary>
		public static PlatformClientException WrapWebIntoClientException(WebException we)
		{
			PlatformClientException qbce = new PlatformClientException(null, Resources.PlatformClientException_WebExceptionErrorMessagePrefix_Connection_error__ + we.Message, we) {FixAndRetryAdvised = true};
			return qbce;
		}

		/// <summary>
		/// Suggested user facing message for this error
		/// </summary>
		public string MessageText
		{
			get
			{
				return Message;
			}
		}

		/// <summary>
		/// Suggested user facing text for a title or caption for a message box that shows <see cref="MessageText"/>.
		/// </summary>
		public string MessageCaption
		{
			get
			{
				return SuggestMessageCaption();
			}
		}

		/// <summary>
		/// If applicable, the Host that returned the error.
		/// </summary>
		public IPlatformHost Host
		{
			get
			{
				return m_Host;
			}
		}
	}
}