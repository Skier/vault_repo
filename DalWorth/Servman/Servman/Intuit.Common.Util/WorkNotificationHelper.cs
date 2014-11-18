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
using System.IO;
using System.Text;
using Intuit.Common.Util.Properties;

namespace Intuit.Common.Util
{
	/// <summary>
	/// Helper functions to execute NotifyingWorker tasks.
	/// </summary>
	public class WorkNotificationHelper
	{
		/// <summary>
		/// Calls notifyingWorker(notify) and returns its return value. If an exception occurrs uses the exceptionWrapper to wrap it.
		/// </summary>
		public static object CallNotifyingWorkerAndWrapCertainExceptions(WorkNotification notify, NotifyingWorker notifyingWorker, IExceptionWrapper exceptionWrapper)
		{
			if (exceptionWrapper == null)
			{
				return notifyingWorker(notify); // avoid exception catching overhead
			}

			try
			{
				return notifyingWorker(notify);
			}
			catch (Exception e)
			{
				Exception wrappedException = exceptionWrapper.WrapException(e);
				if (wrappedException != null)
				{
					throw wrappedException;
				}
				throw;
			}
		}

		/// <summary>
		/// A function that handles exceptions by simply reporting them as fatal errors to the notifier.
		/// </summary>
		public static bool HandleDisplayableExceptionByCallingNotifier(Exception e, WorkNotification notifier)
		{
			IExceptionWithDisplayText edt = e as IExceptionWithDisplayText;
			if (edt != null)
			{
				notifier(NotificationLevel.UserFacingErrorFatal, edt.MessageText, false, null);
				return true;
			}
			return false;
		}
	}

	/// <summary>
	///  Abstract WorkNotification implementation using notifications to log into some logging facility.
	/// </summary>
	public abstract class SimpleAbstractLogWritingWorkNotification : IWorkNotification
	{
		#region IWorkNotification Members

		/// <summary>
		/// Accepts notifications from the worker and writes them to the log. Discards progress indications and diagnostic log level notifications.
		/// </summary>
		public object NotifyOfWork(NotificationLevel notficationLevel, string message, bool offerCancel, object[] param)
		{
			lock (this)
			{
				switch (notficationLevel)
				{
					case NotificationLevel.ProgressIndication:
					case NotificationLevel.DiagnosticLog:
						break;
					case NotificationLevel.UserFacingInfo:
						WriteToLog(Resources.SimpleAbstractLogWritingWorkNotification_NotifyOfWork_INFO);
						break;
					case NotificationLevel.UserFacingWarning:
						WriteToLog(Resources.SimpleAbstractLogWritingWorkNotification_NotifyOfWork_WARNING);
						break;
					case NotificationLevel.UserFacingErrorFixAndRetryAdvised:
						WriteToLog(Resources.SimpleAbstractLogWritingWorkNotification_NotifyOfWork_ERROR);
						break;
					case NotificationLevel.UserFacingErrorFatal:
						WriteToLog(Resources.SimpleAbstractLogWritingWorkNotification_NotifyOfWork_FATALERROR);
						break;
				}
				WriteLineToLog(message);
				if (offerCancel)
				{
					return GetCancelStatus();
				}
			}
			return false;
		}

		#endregion

		/// <summary>
		/// Writes a partial line to the log, leave line open so more information can be appended to the same line.
		/// </summary>
		protected abstract void WriteToLog(string partialLine);

		/// <summary>
		/// Writes the rest of the line to the log and starts a new line.
		/// </summary>
		protected abstract void WriteLineToLog(string line);

		/// <summary>
		/// Needs to ask the user if he/she wants to cancel
		/// </summary>
		protected abstract bool GetCancelStatus();
	}

	/// <summary>
	/// A simple example implementation of a WorkNotification that logs all the notifications into a string and never offers user to cancel
	/// </summary>
	public class StringLoggingWorkNotification : SimpleAbstractLogWritingWorkNotification
	{
		private StringBuilder m_Log = new StringBuilder();

		/// <summary>
		/// Writes the partial line to the internal log buffer
		/// </summary>
		protected override void WriteToLog(string partialLine)
		{
			m_Log.Append(partialLine);
		}

		/// <summary>
		/// Writes the line and a newline to the internal log buffer
		/// </summary>
		protected override void WriteLineToLog(string line)
		{
			m_Log.AppendLine(line);
		}

		/// <summary>
		/// Always returns false
		/// </summary>
		/// <returns>false</returns>
		protected override bool GetCancelStatus()
		{
			return false;
		}

		/// <summary>
		/// Retrieves the log written so far and clears the internal buffer.
		/// </summary>
		public string GetLogAndReset()
		{
			string s;
			lock (this)
			{
				s = m_Log.ToString();
				m_Log = new StringBuilder();
			}
			return s;
		}
	}

	/// <summary>
	/// A simple example implementation of a WorkNotification that logs all the notifications to a stream (e.g. Console.Out or Console.Error) and checks with user whether to cancel by reading input from a stream (e.g. Console.In).
	/// </summary>
	public class StreamLoggingWorkNotification : SimpleAbstractLogWritingWorkNotification
	{
		private readonly StreamReader m_InStreamReader;
		private readonly StreamWriter m_OutStreamWriter;

		/// <summary>
		/// Instantiates a new logging WorkNotification, writing all notifications to outStream, and asking for Y/N from inStream in case a cancellation is offered.
		/// </summary>
		public StreamLoggingWorkNotification(Stream outStream, Stream inStream)
		{
			m_OutStreamWriter = new StreamWriter(outStream);
			m_InStreamReader = new StreamReader(inStream);
		}

		/// <summary>
		/// Writes partialLine into the output stream
		/// </summary>
		protected override void WriteToLog(string partialLine)
		{
			m_OutStreamWriter.Write(partialLine);
		}

		/// <summary>
		/// Writes the line and a newline to the output stream
		/// </summary>
		protected override void WriteLineToLog(string line)
		{
			m_OutStreamWriter.WriteLine(line);
		}

		/// <summary>
		/// Writes "CANCEL (Y/N)? " to the output stream, then queries the input stream for a character. If the input is y or Y return true, otherwise false.
		/// </summary>
		/// <returns>true if the input stream provides Y or y, other false</returns>
		protected override bool GetCancelStatus()
		{
			m_OutStreamWriter.Write(Resources.StreamLoggingWorkNotification_GetCancelStatus__CANCEL__Y_N___);
			int c = m_InStreamReader.Read();
			if (c == -1)
			{
				return false;
			}
			if (Char.ToUpper(Convert.ToChar(c)).Equals(Resources.StreamLoggingWorkNotification_GetCancelStatus_Y))
			{
				return true;
			}
			return false;
		}
	}
}