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
using System.Text;
using System.Windows.Forms;
using Intuit.Common.Util;
using Intuit.Common.WinUtil.Properties;

namespace Intuit.Common.WinUtil
{
	/// <summary>
	/// Utility class to execute a NotifyingWorker task, measuring its execution time and preserving the notifications for displaying them at the end.
	/// </summary>
	public class TimedWorkWithResultsDisplayed
	{
		/// <summary>
		/// Whether or not to show diagnostic-level notifications
		/// </summary>
		protected readonly bool DiagnosticMode;

		private readonly NotifyingWorker m_NotifyingWorker;
		private TimeSpan m_Duration;
		private StringBuilder m_Summary;
		private StringBuilder m_SummaryErrors;
		private StringBuilder m_SummaryErrorsFixAndRetryAdvised;
		private StringBuilder m_SummaryWarnings;

		/// <summary>
		/// The UI that displays the result of the work can use this to display a "sub status" underneath the main status, if the work succeeded.
		/// </summary>
		public string WorkCompletedSuccessfully { get; set; }

		/// <summary>
		/// /// The UI that displays the result of the work can use this to display a "sub status" underneath the main status, if the work ended with a fatal error.
		/// </summary>
		public string WorkFailedWithFatalError { get; set; }

		/// <summary>
		/// /// The UI that displays the result of the work can use this to display a "sub status" underneath the main status, if the work ended with a recoverable (non-fatal) error.
		/// </summary>
		public string WorkFailedWithRecoverableErrors { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="diagnosticMode"></param>
		/// <param name="notifyingWorker"></param>
		/// <param name="workNoun">e.g. "Transmission" or "Upload" or "Synchronization"</param>
		/// <param name="exceptionWrapper"></param>
		public TimedWorkWithResultsDisplayed(bool diagnosticMode, NotifyingWorker notifyingWorker, string workNoun, IExceptionWrapper exceptionWrapper)
		{
			DiagnosticMode = diagnosticMode;
			m_NotifyingWorker = notifyingWorker;
			WorkNoun = workNoun;
			ExceptionWrapper = exceptionWrapper;
			Success = true;
		}

		/// <summary>
		/// Whether or not the work was successful. By default it is true. Will be set to false automatically if the work notifies of an error or an exception is thrown, depending on <see cref="UnsuccessfulOnlyOnExceptions"/>.
		/// </summary>
		public bool Success { get; set; }

		/// <summary>
		/// If false (default), work notifications indicating an error or an exception will set <see cref="Success"/> to false. Otherwise only an exception will do that.
		/// </summary>
		public bool UnsuccessfulOnlyOnExceptions { get; set; }

		/// <summary>
		/// A short noun describing the work being done, like "Transmission".
		/// </summary>
		public string WorkNoun { get; set; }

		/// <summary>
		/// Optional ExceptionWrapper to be used in the exception handling.
		/// </summary>
		public IExceptionWrapper ExceptionWrapper { get; set; }

		/// <summary>
		/// Run the NotifyingWorker while timing it
		/// </summary>
		/// <param name="onWorkNotification"></param>
		/// <returns></returns>
		protected object TimedWork(WorkNotification onWorkNotification)
		{
			m_Summary = new StringBuilder();
			m_SummaryWarnings = new StringBuilder();
			m_SummaryErrors = new StringBuilder();
			m_SummaryErrorsFixAndRetryAdvised = new StringBuilder();
			DateTime start = DateTime.Now;
			object ret = WorkNotificationHelper.CallNotifyingWorkerAndWrapCertainExceptions(onWorkNotification, m_NotifyingWorker, ExceptionWrapper);
			DateTime stop = DateTime.Now;
			m_Duration = stop.Subtract(start);
			return ret;
		}

		/// <summary>
		/// Callback for when the NotifyingWorker notifies us.
		/// </summary>
		protected object OnWorkProgress(NotificationLevel notificationLevel, string message, bool offerCancel, object[] param)
		{
			if (!DiagnosticMode && notificationLevel == NotificationLevel.DiagnosticLog)
			{
				return false;
			}
			switch (notificationLevel)
			{
				case NotificationLevel.DiagnosticLog:
					m_Summary.Append(Resources.TimedWorkWithResultsDisplayed_OnWorkProgress_Diagnostic_info__);
					m_Summary.AppendLine(message);
					break;
				case NotificationLevel.UserFacingInfo:
					m_Summary.AppendLine(message);
					break;
				case NotificationLevel.UserFacingWarning:
					m_SummaryWarnings.AppendLine(message);
					break;
				case NotificationLevel.UserFacingErrorFixAndRetryAdvised:
					m_SummaryErrorsFixAndRetryAdvised.AppendLine(message);
					if (!UnsuccessfulOnlyOnExceptions)
					{
						Success = false;
					}
					break;
				case NotificationLevel.UserFacingErrorFatal:
					m_SummaryErrors.AppendLine(message);
					if (!UnsuccessfulOnlyOnExceptions)
					{
						Success = false;
					}
					break;
				case NotificationLevel.ProgressIndication:
					//ignore
					break;
			}
			return false;
		}

		/// <summary>
		/// Builds a simple HTML summary of the notifications reported by the worker.
		/// </summary>
		/// <returns></returns>
		public string GetResultHtml()
		{
			StringBuilder msg = new StringBuilder();
			msg.AppendLine("<head>\n<title></title>\n<style type=\"text/css\">\nbody,pre{font-family:Microsoft Sans Serif,Tahoma,Verdana,Arial;font-size:8.25pt;}\n.failed{ color:red;font-weight:bold;font-size:11pt;}\n.succeeded{font-weight:bold;font-size:11pt;}\n.error{color:red;font-weight:bold;}\n.warning{color:orange;font-weight:bold;}\n</style>\n</head>\n<body>");
			GetWorkNounAndSuccessOrFail(msg, true);
			AppendErrorOrWarningPostfix(msg, true);
			msg.AppendLine(".</P>");
			if (m_SummaryErrors.Length > 0)
			{
				msg.AppendLine(@"<p class=""error"">" + WebHelper.HtmlEncode(Resources.TimedWorkWithResultsDisplayed_GetResultHtml_ERRORS_) + @"</p><pre>");
				msg.AppendLine(WebHelper.HtmlEncode(m_SummaryErrors.ToString()));
				msg.AppendLine("</pre>");
			}
			if (m_SummaryErrorsFixAndRetryAdvised.Length > 0)
			{
				msg.AppendLine(@"<p class=""error"">" + WebHelper.HtmlEncode(Resources.TimedWorkWithResultsDisplayed_GetResultHtml_ERRORS__please_fix_the_problem_and_try_again__) + @"</p><pre>");
				msg.AppendLine(WebHelper.HtmlEncode(m_SummaryErrorsFixAndRetryAdvised.ToString()));
				msg.AppendLine("</pre>");
			}
			if (m_SummaryWarnings.Length > 0)
			{
				msg.AppendLine(@"<p class=""warning"">" + WebHelper.HtmlEncode(Resources.TimedWorkWithResultsDisplayed_GetResultHtml_WARNINGS_) + @"</p><pre>");
				msg.AppendLine(WebHelper.HtmlEncode(m_SummaryWarnings.ToString()));
				msg.AppendLine("</pre>");
			}
			msg.Append("<p>");
			msg.Append(WebHelper.HtmlEncode(string.Format(Resources.TimedWorkWithResultsDisplayed_GetResultHtml__0__summary_, WorkNoun)));
			msg.AppendLine("</p><pre>");
			msg.AppendLine(WebHelper.HtmlEncode(m_Summary.ToString()));
			msg.AppendLine("</pre>");
			msg.Append("<p>");
			msg.Append(WebHelper.HtmlEncode(GetElapsedTime()));
			msg.AppendLine("</p></body></html>");
			return msg.ToString();
		}

		private string GetElapsedTime()
		{
			string seconds = ((int)m_Duration.TotalSeconds).ToString();
			return string.Format(Resources.TimedWorkWithResultsDisplayed_GetElapsedTime__0__time___1__seconds, WorkNoun, seconds);
		}

		/// <summary>
		/// A summary of all the notifications reported by the worker.
		/// </summary>
		/// <returns></returns>
		public string GetResultMessage()
		{
			StringBuilder msg = new StringBuilder();
			GetWorkNounAndSuccessOrFail(msg, false);
			AppendErrorOrWarningPostfix(msg, false);
			msg.AppendLine(".");
			msg.AppendLine();
			if (m_SummaryErrors.Length > 0)
			{
				msg.AppendLine(Resources.TimedWorkWithResultsDisplayed_GetResultHtml_ERRORS_);
				msg.AppendLine(m_SummaryErrors.ToString());
				msg.AppendLine();
			}
			if (m_SummaryErrorsFixAndRetryAdvised.Length > 0)
			{
				msg.AppendLine(Resources.TimedWorkWithResultsDisplayed_GetResultHtml_ERRORS__please_fix_the_problem_and_try_again__);
				msg.AppendLine(m_SummaryErrorsFixAndRetryAdvised.ToString());
				msg.AppendLine();
			}
			if (m_SummaryWarnings.Length > 0)
			{
				msg.AppendLine(Resources.TimedWorkWithResultsDisplayed_GetResultHtml_WARNINGS_);
				msg.AppendLine(m_SummaryWarnings.ToString());
				msg.AppendLine();
			}
			msg.AppendLine(string.Format(Resources.TimedWorkWithResultsDisplayed_GetResultHtml__0__summary_, WorkNoun));
			msg.AppendLine(m_Summary.ToString());
			msg.AppendLine();
			msg.AppendLine(GetElapsedTime());
			return msg.ToString();
		}

		private void GetWorkNounAndSuccessOrFail(StringBuilder msg, bool includeHtmlFormatting)
		{
			if (Success)
			{
				AppendStatus(includeHtmlFormatting ? "succeeded" : null, msg, Resources.TimedWorkWithResultsDisplayed_GetWorkNounAndSuccessOrFail_succeeded);
			}
			else
			{
				AppendStatus(includeHtmlFormatting ? "failed" : null, msg, HasFatalError() ? Resources.TimedWorkWithResultsDisplayed_GetWorkNounAndSuccessOrFail_failed : Resources.TimedWorkWithResultsDisplayed_GetWorkNounAndSuccessOrFail_unsuccessful);
			}
		}

		private void AppendStatus(string cssClass, StringBuilder msg, string verb)
		{
			if (cssClass != null)
			{
				msg.Append(@"<p class=""");
				msg.Append(cssClass);
				msg.Append('"');
				msg.Append(">");
			}
			string status = String.Format(Resources.TimedWorkWithResultsDisplayed_AppendStatus__0___1_, WorkNoun, verb);
			msg.Append(cssClass == null ? status : WebHelper.HtmlEncode(status));
		}

		/// <summary>
		/// Summarizes all the notifications of level "error".
		/// </summary>
		/// <returns></returns>
		public string GetErrorMessage()
		{
			if (m_SummaryErrors.Length > 0)
			{
				return m_SummaryErrors.ToString();
			}

			if (m_SummaryErrorsFixAndRetryAdvised.Length > 0)
			{
				return m_SummaryErrorsFixAndRetryAdvised.ToString();
			}

			return null;
		}

		/// <summary>
		/// Whether or not any errors where reported by the worker.
		/// </summary>
		/// <returns></returns>
		public bool IsError()
		{
			return HasRecoverableError() || HasFatalError();
		}

		/// <summary>
		/// Whether or not any recoverable errors where reported by the worker.
		/// </summary>
		/// <returns></returns>
		public bool HasRecoverableError()
		{
			return m_SummaryErrorsFixAndRetryAdvised.Length > 0;
		}

		/// <summary>
		/// Whether or not any fatal errors where reported by the worker.
		/// </summary>
		/// <returns></returns>
		public bool HasFatalError()
		{
			return m_SummaryErrors.Length > 0;
		}

		/// <summary>
		/// Builds a "status" message based on whether the work was successful or not, and the error message, if any.
		/// </summary>
		/// <returns></returns>
		public string SuggestedMessageText()
		{
			if (Success)
			{
				return SuggestedMessageCaption(true);
			}
			return GetErrorMessage();
		}

		/// <summary>
		/// Suggests a "title" or "caption" for a status message concerning the outcome of the work.
		/// </summary>
		/// <param name="longVersion"></param>
		/// <returns></returns>
		public string SuggestedMessageCaption(bool longVersion)
		{
			StringBuilder msg = new StringBuilder();
			GetWorkNounAndSuccessOrFail(msg, false);
			if (longVersion)
			{
				AppendErrorOrWarningPostfix(msg, false);
			}
			return msg.ToString();
		}

		private void AppendErrorOrWarningPostfix(StringBuilder amsg, bool html)
		{
			String msg = string.Empty;
			if (m_SummaryErrors.Length > 0)
			{
				msg = string.Format(Resources.TimedWorkWithResultsDisplayed_AppendErrorOrWarningPostfix__with_unrecoverable_errors_0_, (m_SummaryWarnings.Length > 0 ? Resources.TimedWorkWithResultsDisplayed_AppendErrorOrWarningPostfix__and_warnings : String.Empty));
			}
			else if (m_SummaryErrorsFixAndRetryAdvised.Length > 0)
			{
				msg = string.Format(Resources.TimedWorkWithResultsDisplayed_AppendErrorOrWarningPostfix__with_recoverable_errors_0_, (m_SummaryWarnings.Length > 0 ? Resources.TimedWorkWithResultsDisplayed_AppendErrorOrWarningPostfix__and_warnings : String.Empty));
			}
			else if (m_SummaryWarnings.Length > 0)
			{
				msg = Resources.TimedWorkWithResultsDisplayed_AppendErrorOrWarningPostfix__with_warnings;
			}
			amsg.Append(html ? WebHelper.HtmlEncode(msg) : msg);
		}

		/// <summary>
		/// Runs the work and shows the result in a message box. Handles errors by showing message boxes. Catches exceptions and shows them as errors if they implement IExceptionWithDisplayText, otherwise re-throws them.
		/// </summary>
		/// <param name="resultCaption">Caption for the message box to display results</param>
		/// <returns>the return value of the worker</returns>
		public object WorkAndDisplayTimeResultsAsAlert(string resultCaption)
		{
			object ret = null;
			try
			{
				ret = TimedWork(OnWorkProgress);
			}
			catch (Exception exception)
			{
				Success = false;
				if (!WorkNotificationHelper.HandleDisplayableExceptionByCallingNotifier(exception, OnWorkProgress))
				{
					throw;
				}
			}
			MessageBox.Show(GetResultMessage(), resultCaption);
			return ret;
		}

		/// <summary>
		/// Convenience function to set the sub-status messages for this work.
		/// </summary>
		/// <param name="workCompletedSuccessfully"></param>
		/// <param name="workFailedWithFatalError"></param>
		/// <param name="workFailedWithRecoverableErrors"></param>
		public void SetWorkCompletedOrFailedStrings(string workCompletedSuccessfully, string workFailedWithFatalError, string workFailedWithRecoverableErrors)
		{
			WorkCompletedSuccessfully = workCompletedSuccessfully;
			WorkFailedWithFatalError = workFailedWithFatalError;
			WorkFailedWithRecoverableErrors = workFailedWithRecoverableErrors;
		}
	}
}