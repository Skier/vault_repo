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
using System.Windows.Forms;
using Intuit.Common.Util;
using Intuit.Common.WinUtil.Properties;

namespace Intuit.Common.WinUtil
{
	/// <summary>
	/// Helps with the execution of NotifyingWorker tasks.
	/// </summary>
	public class WorkNotificationUIHelper
	{
		private readonly bool m_DiagnosticMode;
		private readonly string m_FeatureName;

		/// <summary>
		/// New WorkNotificationUIHelper with your feature name (used in various displays) and whether or not we're in diagnostic mode (controls visibility of diagnostic level notifications).
		/// </summary>
		public WorkNotificationUIHelper(string featureName, bool diagnosticMode)
		{
			m_FeatureName = featureName;
			m_DiagnosticMode = diagnosticMode;
		}

		/// <summary>
		/// Show message box for each notification. Show OK/Cancel message box for cancellation offers.
		/// </summary>
		/// <param name="notficationLevel">the level of customer visiblity and impact, determines if/how the message is shown</param>
		/// <param name="message"></param>
		/// <param name="offerCancel">if true, will offer user OK and Cancel button, otherwise just OK button</param>
		/// <param name="param"></param>
		/// <returns>true if user canceled. Will be false if user clicked Cancel button or we didn't offer cancel.</returns>
		public object ShowMessageBox(NotificationLevel notficationLevel, string message, bool offerCancel, object[] param)
		{
			if (!m_DiagnosticMode && NotificationLevel.DiagnosticLog == notficationLevel)
			{
				return false;
			}
			string caption;
			switch (notficationLevel)
			{
				case NotificationLevel.DiagnosticLog:
					caption = Resources.WorkNotificationUIHelper_ShowMessageBox_DIAGNOSTIC_LOG_MESSAGE;
					break;
				case NotificationLevel.UserFacingInfo:
					caption = m_FeatureName;
					break;
				case NotificationLevel.UserFacingWarning:
					caption = string.Format(Resources.WorkNotificationUIHelper_ShowMessageBox_Caption, m_FeatureName, Resources.WorkNotificationUIHelper_ShowMessageBox_Warning);
					break;
				case NotificationLevel.UserFacingErrorFixAndRetryAdvised:
					caption = string.Format(Resources.WorkNotificationUIHelper_ShowMessageBox_Caption, m_FeatureName, Resources.WorkNotificationUIHelper_ShowMessageBox_Error);
					break;
				case NotificationLevel.UserFacingErrorFatal:
					caption = string.Format(Resources.WorkNotificationUIHelper_ShowMessageBox_Caption, m_FeatureName, Resources.WorkNotificationUIHelper_ShowMessageBox_FatalError);
					break;
				case NotificationLevel.ProgressIndication:
					//ignore
					return false;
				default:
					throw new Exception(Resources.WorkNotificationUIHelper_ShowMessageBox_Unexpected_NotificationLevel);
			}
			if (offerCancel)
			{
				return DialogResult.Cancel == MessageBox.Show(message, caption, MessageBoxButtons.OKCancel);
			}
			MessageBox.Show(message, caption);
			return false;
		}

		/// <summary>
		/// Runs notifyingWorker using <see cref="CallWorkerWhileBlockingForm"/>. Handles exceptions using <see cref="HandleDisplayableExceptionByShowingAlert"/>.
		/// </summary>
		public static object CallWorkerWhileBlockingFormAndAlertOnDisplayableException(Control owner, WorkNotification notify, NotifyingWorker notifyingWorker, IExceptionWrapper exceptionWrapper)
		{
			return CallWorkerWhileBlockingForm(owner, notify,
			                                   delegate
			                                   {
			                                   	return CallWorkerWrapExceptionsAndAlertOnDisplayableException(notify, notifyingWorker, exceptionWrapper);
			                                   });
		}

		/// <summary>
		/// Disables the owner, sets the wait cursor, and runs the notifyingWorker. Restores form and cursor afterwards.
		/// </summary>
		public static object CallWorkerWhileBlockingForm(Control owner, WorkNotification notify, NotifyingWorker notifyingWorker)
		{
			owner.Enabled = false;
			Cursor current = owner.Cursor;
			owner.Cursor = Cursors.WaitCursor;
			try
			{
				return notifyingWorker(notify);
			}
			finally
			{
				owner.Cursor = current;
				owner.Enabled = true;
			}
		}

		private static object CallWorkerWrapExceptionsAndAlertOnDisplayableException(WorkNotification notify, NotifyingWorker notifyingWorker, IExceptionWrapper exceptionWrapper)
		{
			try
			{
				return WorkNotificationHelper.CallNotifyingWorkerAndWrapCertainExceptions(notify, notifyingWorker, exceptionWrapper);
			}
			catch (Exception e)
			{
				if (HandleDisplayableExceptionByShowingAlert(e))
				{
					return null;
				}
				throw;
			}
		}

		/// <summary>
		/// Suggests a message box icon depending on notification level.
		/// </summary>
		public static MessageBoxIcon GetErrorIcon(NotificationLevel level)
		{
			switch (level)
			{
				case NotificationLevel.UserFacingErrorFatal:
				case NotificationLevel.UserFacingErrorFixAndRetryAdvised:
					return MessageBoxIcon.Error;
				case NotificationLevel.UserFacingInfo:
					return MessageBoxIcon.Information;
				case NotificationLevel.UserFacingWarning:
					return MessageBoxIcon.Warning;
				default:
					return MessageBoxIcon.None;
			}
		}

		/// <summary>
		/// If the exception implements <see cref="IExceptionWithDisplayText"/>, show it using a message box.
		/// </summary>
		public static bool HandleDisplayableExceptionByShowingAlert(Exception e)
		{
			IExceptionWithDisplayText qbce = e as IExceptionWithDisplayText;
			if (qbce != null)
			{
				MessageBox.Show(qbce.MessageText, qbce.MessageCaption);
				return true;
			}
			return false;
		}
	}
}