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
	/// Runs a NotifyingWorker in a background thread while displaying a progress bar in the UI thread.
	/// </summary>
	public class TimedWorkWithProgressBar : TimedWorkWithResultsDisplayed
	{
		private OnProgress m_OnProgress;

		/// <summary>
		/// Whether or not the user chose to cancel when a cancellation was offered.
		/// </summary>
		public bool Canceled { set; get; }

		private const string CustomFormMagicString = "CUSTOMFORM";

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="diagnosticMode">controls whether or not diagnostic level notifications will be considered</param>
		/// <param name="notifyingWorker">the task to run</param>
		/// <param name="workNoun">a short noun describing the work that's being done, like "Transmission" or "Process"</param>
		/// <param name="exceptionWrapper">a class that knows how to wrap certain exceptions it wants to catch into other types of exceptions</param>
		public TimedWorkWithProgressBar(bool diagnosticMode, NotifyingWorker notifyingWorker, string workNoun, IExceptionWrapper exceptionWrapper)
			: base(diagnosticMode, notifyingWorker, workNoun, exceptionWrapper)
		{
		}

		private object PerformWork(OnProgress onProgress)
		{
			m_OnProgress = onProgress;
			return TimedWork(OnNotificationWithProgressBarUpdate);
		}

		private object OnNotificationWithProgressBarUpdate(NotificationLevel notificationLevel, string message, bool offerCancel, object[] param)
		{
			if (notificationLevel == NotificationLevel.ProgressIndication)
			{
				if (m_OnProgress != null)
				{
					m_OnProgress(message);
				}
			}
			if (offerCancel && (DiagnosticMode || notificationLevel != NotificationLevel.DiagnosticLog))
			{
				Canceled = (bool)ProgressWorker.RunInForeground(new CheckForCancelDelegate(CheckForCancel), new object[] {message, notificationLevel, param});
				if (Canceled)
				{
					message += Resources.TimedWorkWithProgressBar_OnNotificationWithProgressBarUpdate_ACTION_CANCELED;
				}
				else
				{
					message += Resources.TimedWorkWithProgressBar_OnNotificationWithProgressBarUpdate_offer_to_cancel_declined;
				}
			}
			return (bool)OnWorkProgress(notificationLevel, message, offerCancel, null) || Canceled;
		}

		private delegate object CheckForCancelDelegate(string message, NotificationLevel notificationLevel, object[] param);

		private static object CheckForCancel(string message, NotificationLevel notificationLevel, object[] param)
		{
			Form owner;
			if (!ProgressWorker.GetCurrentRunningProgressIndicatorForm(out owner))
			{
				owner = Form.ActiveForm;
			}
			if (message == CustomFormMagicString)
			{
				if (param == null || param.Length != 1 || (!(param[0] is Form)))
				{
					throw new Exception(Resources.TimedWorkWithProgressBar_CheckForCancel_expected_custom_form_as_first_param);
				}
				Form customForm = (Form)param[0];
				DialogResult result = customForm.ShowDialog(owner);
				customForm.Close();
				return result == DialogResult.Cancel;
			}
			return DialogResult.Cancel == MessageBox.Show(owner, message, Resources.TimedWorkWithProgressBar_CheckForCancel_Please_confirm, MessageBoxButtons.OKCancel, WorkNotificationUIHelper.GetErrorIcon(notificationLevel));
		}

		/// <summary>
		/// Runs the task.
		/// </summary>
		/// <param name="progressBarCaption">the desired text for the title bar of the progress bar window</param>
		/// <param name="progressBarFirstDetail">the first "progress" text to show in the progress bar window</param>
		/// <returns>the return value of the task</returns>
		public object WorkWithProgressBar(string progressBarCaption, string progressBarFirstDetail)
		{
			try
			{
				return new ProgressWorker(progressBarCaption, new BackgrWorkOut(PerformWork)).DoWorkOut(progressBarFirstDetail);
			}
			catch (ProgressWorkerException pwe)
			{
				Exception inner = pwe.InnerException;
				IExceptionKnowsIfRetryAdvised ekra = inner as IExceptionKnowsIfRetryAdvised;
				if (ekra == null)
				{
					throw new ProgressWorkerException(inner);
				}
				OnWorkProgress(ekra.FixAndRetryAdvised ? NotificationLevel.UserFacingErrorFixAndRetryAdvised : NotificationLevel.UserFacingErrorFatal, ((ekra is IExceptionWithDisplayText) ? ((IExceptionWithDisplayText)ekra).MessageText : inner.Message), false, null);
				Success = false;
				return null;
			}
		}

		/// <summary>
		/// Runs the task and shows the result in a simple message box.
		/// </summary>
		/// <param name="progressBarCaption"></param>
		/// <param name="progressBarFirstDetail"></param>
		/// <param name="resultCaption">the text for the title bar of the message box showing the result</param>
		/// <returns>the return value of the task</returns>
		/// <seealso cref="WorkWithProgressBar"/>
		public object WorkWithProgressbarAndShowResults(string progressBarCaption, string progressBarFirstDetail, string resultCaption)
		{
			object ret = WorkWithProgressBar(progressBarCaption, progressBarFirstDetail);
			MessageBox.Show(GetResultMessage(), resultCaption);
			return ret;
		}

		/// <summary>
		/// Runs the task and shows the result in a <see cref="HtmlDisplayerWithPrintButton"/>.
		/// </summary>
		/// <param name="form">the owner window to which the result window should be modal to (may be null)</param>
		/// <param name="progressCaption"></param>
		/// <param name="progressFirstDetail"></param>
		/// <param name="resultCaption"></param>
		/// <returns>the return value of the task</returns>
		/// <seealso cref="WorkWithProgressBar"/>
		public object WorkWithProgressBarAndShowResultHtml(IWin32Window form, string progressCaption, string progressFirstDetail, string resultCaption)
		{
			object ret = WorkWithProgressBar(progressCaption, progressFirstDetail);
			using (Form ur = new HtmlDisplayerWithPrintButton(this))
			{
				ur.ShowDialog(form);
			}
			return ret;
		}
	}
}