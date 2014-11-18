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
using System.Collections;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Intuit.Common.WinUtil.Properties;

namespace Intuit.Common.WinUtil
{
	/// <summary>
	/// The delegate the background process should call into to report progress. Don't forget to check for null!
	/// </summary>
	public delegate void OnProgress(string status);

	/// <summary>
	/// A background work task that takes input and returns output.
	/// </summary>
	public delegate object BackgrWorkInAndOut(OnProgress onProgress, object input);

	/// <summary>
	/// A background work task that takes input but doesn't return output.
	/// </summary>
	public delegate void BackgrWorkIn(OnProgress onProgress, object input);

	/// <summary>
	/// A background work task that doesn't take input but returns output.
	/// </summary>
	public delegate object BackgrWorkOut(OnProgress onProgress);

	/// <summary>
	/// A background work task that doesn't take input nor returns output.
	/// </summary>
	public delegate void BackgrWork(OnProgress onProgress);

	/// <summary>
	/// An exception wrapper for an exception thrown in the background process.
	/// </summary>
	public class ProgressWorkerException : ApplicationException
	{
		internal ProgressWorkerException(Exception backgroundException)
			: base(Resources.ProgressWorkerException_ProgressWorkerException_exception_thrown_in_background_thread, backgroundException)
		{
		}

		/// <summary>
		/// Use this to "rethrow" a ProgressWorkerException when you caught it but couldn't handle it. This will preserve the stack-trace better than just doing a "throw;".
		/// </summary>
		/// <exception cref="ProgressWorkerException"></exception>
		public static void Rethrow(ProgressWorkerException pwe)
		{
			throw new ProgressWorkerException(pwe.InnerException);
		}
	}

	/// <summary>
	/// TODO: This was written initially using .NET 1.1. This should be updated to utilize new .NET facilities such as BackgroundWorker.
	/// </summary>
	public class ProgressWorker
	{
		private const int SecondsTimeoutForBackgroundThreadToStop = 10;
		// ReSharper disable ConvertToConstant.Local
		// ReSharper disable RedundantDefaultFieldInitializer
		/// <summary>
		/// This is a debugging tool. If you're trying to debug a problem and the multi-threading
		/// is making things hard, you can turn that stuff off by:
		/// 1) setting a breakpoint in the ProgressWorker constructor
		/// 2) setting s_NoProgressBar = true at that breakpoint
		/// </summary>
		private static bool s_NoProgressBar = false;

		// ReSharper restore RedundantDefaultFieldInitializer
		// ReSharper restore ConvertToConstant.Local

		/// <summary>
		/// This needs to be called at the beginning of your application.
		/// Example:
		///	<c>ProgressWorker.Initialize(myFeatureName, myFeatureName, "Processing", Thread.CurrentThread);</c>
		/// </summary>
		/// <param name="title">The default caption for the progress bar window</param>
		/// <param name="group">The default task group description</param>
		/// <param name="detail">The default task description</param>
		/// <param name="uiThread">The default UI thread</param>
		public static void Initialize(string title, string group, string detail, Thread uiThread)
		{
			ProgressIndicatorForm.DefaultDetail = detail;
			ProgressIndicatorForm.DefaultGroup = group;
			ProgressIndicatorForm.DefaultTitle = title;
			sm_UIThread = uiThread;
			FailOnAttemptsToUse = false;
		}

		// ReSharper disable InconsistentNaming
		private static readonly ArrayList sm_NestedProgress = new ArrayList();
		private static ProgressIndicatorForm sm_ProgressIndicatorFormForm;
		private static Thread sm_UIThread;
		private static bool sm_FailOnAttemptsToUse = true;
		private static bool sm_BackgroundWorkDone;
		private static ManualResetEvent sm_SignalBackgroundwordDoneOrForegroundCallMade;
		// ReSharper restore InconsistentNaming

		private int m_CallsToShowForm;
		private readonly string m_Group;
		private string m_LastDetailShown;

		private readonly BackgrWorkInAndOut m_BackgrWorkerIO;
		private readonly BackgrWorkIn m_BackgrWorkerI;
		private readonly BackgrWorkOut m_BackgrWorkerO;
		private readonly BackgrWork m_BackgrWorker;

		private object m_WorkInput;
		private object m_WorkOutput;

		private Exception m_BackgroundError;

		private int m_ShowProgressDelay = 2000;

		/// <summary>
		/// Get a new ProgressWorker for a BackgrWork task.
		/// </summary>
		public ProgressWorker(string group, BackgrWork backgrWorker)
			: this(group)
		{
			if (backgrWorker == null)
			{
				throw new ArgumentNullException("backgrWorker", Resources.ProgressWorker_ProgressWorker_need_to_initialize_this_with_a_background_worker);
			}
			m_BackgrWorker = backgrWorker;
		}

		/// <summary>
		/// Get a new ProgressWorker for a BackgrWorkOut task.
		/// </summary>
		public ProgressWorker(string group, BackgrWorkOut backgrWorker)
			: this(group)
		{
			if (backgrWorker == null)
			{
				throw new ArgumentNullException("backgrWorker", Resources.ProgressWorker_ProgressWorker_need_to_initialize_this_with_a_background_worker);
			}
			m_BackgrWorkerO = backgrWorker;
		}

		/// <summary>
		/// Get a new ProgressWorker for a BackgrWorkIn task.
		/// </summary>
		public ProgressWorker(string group, BackgrWorkIn backgrWorker)
			: this(group)
		{
			if (backgrWorker == null)
			{
				throw new ArgumentNullException("backgrWorker", Resources.ProgressWorker_ProgressWorker_need_to_initialize_this_with_a_background_worker);
			}
			m_BackgrWorkerI = backgrWorker;
		}

		/// <summary>
		/// Get a new ProgressWorker for a BackgrWorkInAndOut task.
		/// </summary>
		public ProgressWorker(string group, BackgrWorkInAndOut backgrWorker)
			: this(group)
		{
			if (backgrWorker == null)
			{
				throw new ArgumentNullException("backgrWorker", Resources.ProgressWorker_ProgressWorker_need_to_initialize_this_with_a_background_worker);
			}
			m_BackgrWorkerIO = backgrWorker;
		}

		private ProgressWorker(string group)
		{
			if (String.IsNullOrEmpty(group))
			{
				throw new ArgumentException(Resources.ProgressWorker_ProgressWorker_group_must_be_set_for_this_class_to_work_, "group");
			}
			m_Group = group;
			if (sm_FailOnAttemptsToUse)
			{
				throw new Exception(Resources.ProgressWorker_ProgressWorker_Unit_tests_should_not_cause_the_progress_bar_to_show__unless_you_re_testing_actual_UI_classes__in_which_case_you_may_set_FailOnAttemptsToUse_to_false__);
			}
		}

		/// <summary>
		/// Milliseconds to delay before showing progress bar
		/// </summary>
		public int ShowProgressDelay
		{
			get
			{
				return m_ShowProgressDelay;
			}
			set
			{
				m_ShowProgressDelay = value;
			}
		}


		private bool RunInBackgroundifFirst(string initialProgress)
		{
			lock (typeof (ProgressWorker))
			{
				if (sm_ProgressIndicatorFormForm == null)
				{
					sm_ProgressIndicatorFormForm = new ProgressIndicatorForm();
					if (sm_UIThread == null)
					{
						sm_UIThread = Thread.CurrentThread;
					}
					else if (sm_UIThread != Thread.CurrentThread)
					{
						throw new Exception(Resources.ProgressWorker_RunInBackgroundifFirst_Internal_error_in_ProgressWorker__subsequent_call_to_RunInBackgroundifFirst_from_different_thread);
					}
				}
				else
				{
					return false;
				}
			}
			Cursor prevCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			UpdateDetailText(initialProgress);
			Thread backgroundThread = new Thread(DoWorkInBackground) {Name = "ProgressWorkerBackground"};
			sm_BackgroundWorkDone = false;
			sm_SignalBackgroundwordDoneOrForegroundCallMade = new ManualResetEvent(false);
			Form activeForm = Form.ActiveForm;
			bool activeFormEnabledState = true;
			if (activeForm != null)
			{
				activeFormEnabledState = activeForm.Enabled;
				activeForm.Enabled = false;
			}
			backgroundThread.Start();
			// Prevent ProgressBar flickering by not opening it unless the tasks takes more than 2 seconds,
			// just like QuickBooks progress bars.
			sm_SignalBackgroundwordDoneOrForegroundCallMade.WaitOne(TimeSpan.FromMilliseconds(ShowProgressDelay), false);
			bool skipProgressBarShow = false;
			lock (typeof (ProgressWorker))
			{
				if (sm_BackgroundWorkDone)
				{
					skipProgressBarShow = true;
				}
			}
			using (sm_ProgressIndicatorFormForm)
			{
				if (!skipProgressBarShow)
				{
					// this call blocks until DoWorkInBackground sets sm_BackgroundWorkDone to true
					// which in turn will make sm_ProgressIndicatorFormForm close itself.
					sm_ProgressIndicatorFormForm.StartPosition = activeForm != null ? FormStartPosition.CenterParent : FormStartPosition.WindowsDefaultLocation;
					sm_ProgressIndicatorFormForm.ShowDialog(activeForm);
				}
			}
			if (activeForm != null)
			{
				activeForm.Enabled = activeFormEnabledState;
			}
			if (m_BackgroundError != null)
			{
				Cleanup(prevCursor);
				WorkEnded();
				throw new ProgressWorkerException(m_BackgroundError);
			}
			lock (typeof (ProgressWorker))
			{
				if (!sm_BackgroundWorkDone)
				{
					throw new Exception(Resources.ProgressWorker_RunInBackgroundifFirst_Internal_error_in_ProgressWorker__Background_work_never_reported_being_done_);
				}
				sm_BackgroundWorkDone = false;
			}
			if (backgroundThread.IsAlive)
			{
				backgroundThread.Join(TimeSpan.FromSeconds(SecondsTimeoutForBackgroundThreadToStop));
				Debug.Assert(!backgroundThread.IsAlive, string.Format(Resources.ProgressWorker_RunInBackgroundifFirst_After__0__seconds__background_thread_is_still_alive_, SecondsTimeoutForBackgroundThreadToStop));
			}
			Cleanup(prevCursor);
			return true;
		}

		private static void Cleanup(Cursor prevCursor)
		{
			lock (typeof (ProgressWorker))
			{
				sm_ProgressIndicatorFormForm = null;
				sm_SignalBackgroundwordDoneOrForegroundCallMade = null;
			}
			Cursor.Current = prevCursor;
		}

		private void WorkEnded()
		{
			if (m_CallsToShowForm != 0)
			{
				sm_NestedProgress.Remove(this);
			}
			else
			{
				// we never called ShowProgress on this group
				if (sm_NestedProgress.Contains(this))
				{
					throw new Exception(Resources.ProgressWorker_WorkEnded_progress_bar_internal_nesting_logic_in_WorkEnded);
				}
			}

			if (sm_NestedProgress.Count > 0)
			{
				if (sm_ProgressIndicatorFormForm == null)
				{
					throw new Exception(Resources.ProgressWorker_WorkEnded_progress_bar_internal_logic_error_in_WorkEnded__form_is_absent);
				}
				ProgressWorker topmost = (ProgressWorker)sm_NestedProgress[sm_NestedProgress.Count - 1];
				SetProgressDialogText(topmost.m_Group, topmost.m_LastDetailShown ?? String.Empty);
			}
		}

		/// <summary>
		/// Executes the BackgrWorkOut task in the background while showing a progress bar.
		/// </summary>
		/// <param name="initialProgress"></param>
		public object DoWorkOut(string initialProgress)
		{
			if (s_NoProgressBar)
			{
				return m_BackgrWorkerO(DebugWrite);
			}

			if (!RunInBackgroundifFirst(initialProgress))
			{
				UpdateDetailText(initialProgress);
				m_WorkOutput = m_BackgrWorkerO(UpdateDetailText);
			}
			WorkEnded();
			return m_WorkOutput;
		}

		/// <summary>
		/// Executes the BackgrWorkIn task in the background while showing a progress bar.
		/// </summary>
		public void DoWorkIn(string initialProgress, object input)
		{
			if (s_NoProgressBar)
			{
				m_BackgrWorkerI(DebugWrite, input);
				return;
			}

			m_WorkInput = input;
			if (!RunInBackgroundifFirst(initialProgress))
			{
				UpdateDetailText(initialProgress);
				m_BackgrWorkerI(UpdateDetailText, input);
			}
			WorkEnded();
		}

		/// <summary>
		/// Executes the BackgrWorkInAndOut task in the background while showing a progress bar.
		/// </summary>
		public object DoWorkInAndOut(string initialProgress, object input)
		{
			if (s_NoProgressBar)
			{
				return m_BackgrWorkerIO(DebugWrite, input);
			}

			m_WorkInput = input;
			if (!RunInBackgroundifFirst(initialProgress))
			{
				UpdateDetailText(initialProgress);
				m_WorkOutput = m_BackgrWorkerIO(UpdateDetailText, input);
			}
			WorkEnded();
			return m_WorkOutput;
		}

		/// <summary>
		/// Executes the BackgrWork task in the background while showing a progress bar.
		/// </summary>
		public void DoWork(string initialProgress)
		{
			if (s_NoProgressBar)
			{
				m_BackgrWorker(DebugWrite);
				return;
			}

			if (!RunInBackgroundifFirst(initialProgress))
			{
				UpdateDetailText(initialProgress);
				m_BackgrWorker(UpdateDetailText);
			}
			WorkEnded();
		}

		private void DoWorkInBackground()
		{
			try
			{
				if (m_BackgrWorkerIO != null)
				{
					m_WorkOutput = m_BackgrWorkerIO(UpdateDetailText, m_WorkInput);
				}
				else if (m_BackgrWorkerI != null)
				{
					m_BackgrWorkerI(UpdateDetailText, m_WorkInput);
				}
				else if (m_BackgrWorkerO != null)
				{
					m_WorkOutput = m_BackgrWorkerO(UpdateDetailText);
				}
				else if (m_BackgrWorker != null)
				{
					m_BackgrWorker(UpdateDetailText);
				}
			}
			catch (Exception e)
			{
				m_BackgroundError = e;
			}
			finally
			{
				lock (typeof (ProgressWorker))
				{
					sm_BackgroundWorkDone = true;
					sm_SignalBackgroundwordDoneOrForegroundCallMade.Set();
					Thread.CurrentThread.Abort();
				}
			}
		}

		private void UpdateDetailText(string detail)
		{
			// only update the operations group once!
			if (m_CallsToShowForm == 0)
			{
				sm_NestedProgress.Add(this);
				SetProgressDialogText(m_Group, detail);
			}
			else
			{
				SetProgressDialogText(null, detail);
			}
			m_CallsToShowForm++;
			m_LastDetailShown = detail;
		}

		/// <summary>
		/// Gives you the current running instance of the progress bar window if there's one up.
		/// </summary>
		/// <returns>true if a progress bar window is running, false if not</returns>
		public static bool GetCurrentRunningProgressIndicatorForm(out Form form)
		{
			form = null;
			bool formPresent = false;
			lock (typeof (ProgressWorker))
			{
				if (sm_ProgressIndicatorFormForm != null)
				{
					form = sm_ProgressIndicatorFormForm;
					formPresent = true;
				}
			}
			return formPresent;
		}

		/// <summary>
		/// Useful delegate if you just want to execute a "void foo()" function in the foreground using <see cref="RunInForeground"/>.
		/// </summary>
		public delegate void VoidVoidDelegate();

		/// <summary>
		/// Utility function. Allows to call a method while making sure it's executed on the same thread as the UI.
		/// This is recommended for any UI-related code that executes while the progress bar is showing.
		/// </summary>
		public static object RunInForeground(Delegate method, object[] args)
		{
			Form piForm;
			bool formPresent = GetCurrentRunningProgressIndicatorForm(out piForm);
			if (formPresent && (sm_SignalBackgroundwordDoneOrForegroundCallMade != null || piForm.InvokeRequired))
			{
				if (sm_SignalBackgroundwordDoneOrForegroundCallMade != null)
				{
					sm_SignalBackgroundwordDoneOrForegroundCallMade.Set();
					while (piForm.InvokeRequired == false)
					{
						Thread.Sleep(SecondsTimeoutForBackgroundThreadToStop);
					}
				}
				return piForm.Invoke(method, args);
			}
			if (sm_UIThread != null && Thread.CurrentThread != sm_UIThread)
			{
				throw new Exception(Resources.ProgressWorker_RunInForeground_progress_bar_internal_logic_error_in_RunInForeground);
			}
			return method.DynamicInvoke(args);
		}

		private delegate void DelSetFormMsg(string group, string detail);

		private static void SetProgressDialogText(string group, string detail)
		{
			RunInForeground(new DelSetFormMsg(sm_ProgressIndicatorFormForm.UpdateDialogText), new object[] {group, detail});
		}

		/// <summary>
		/// By setting this property to true, you can prevent the progress bar from working.
		/// Instead it will throw an exception. That will help detect execution of UI code in UnitTests.
		/// Set this to false for a unit test that's allowed to show a progress bar (set to true after the test since this is a static).
		/// </summary>
		public static bool FailOnAttemptsToUse
		{
			set
			{
				sm_FailOnAttemptsToUse = value;
			}
		}

		internal static bool KeepShowingProgress
		{
			get
			{
				lock (typeof (ProgressWorker))
				{
					if (sm_BackgroundWorkDone)
					{
						return false;
					}

					return true;
				}
			}
		}

		/// <summary>
		/// The recommended maximum length for a progress bar message, so it doesn't wrap.
		/// </summary>
		public static int RecommendedDetailMaxLength
		{
			get
			{
				return 70;
			}
		}

		private static void DebugWrite(string status)
		{
			Debug.WriteLine(status);
		}

		/// <summary>
		/// Utility function if you need a quick-and-dirty way to find an owner form to show another form (<c>myForm.ShowDialog(ProgressOwner.GetOwnerForm()</c>).
		/// Returns either the output of <see cref="GetCurrentRunningProgressIndicatorForm"/> or <see cref="Form.ActiveForm"/>.
		/// </summary>
		/// <returns></returns>
		public static Form GetOwnerForm()
		{
			Form owner;
			if (!GetCurrentRunningProgressIndicatorForm(out owner))
			{
				owner = Form.ActiveForm;
			}
			return owner;
		}
	}
}