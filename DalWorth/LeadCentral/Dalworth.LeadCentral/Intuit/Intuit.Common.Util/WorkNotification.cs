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

namespace Intuit.Common.Util
{
	/// <summary>
	/// The delegate that the worker task to be run has to implement.
	/// </summary>
	/// <param name="notify"></param>
	/// <returns></returns>
	public delegate object NotifyingWorker(WorkNotification notify);

	/// <summary>
	/// The level of notification when the worker calls back into the caller.
	/// </summary>
	public enum NotificationLevel
	{
		/// <summary>
		/// Can be used to update a progress bar running in the foreground.
		/// </summary>
		ProgressIndication,
		/// <summary>
		/// Information useful for debugging or other diagnostic purposes. Even though in most cases this won't be seen by end-users, it's advisable to keep language clean and the text clear.
		/// </summary>
		DiagnosticLog,
		/// <summary>
		/// An informational message to be shown to the user.
		/// </summary>
		UserFacingInfo,
		/// <summary>
		/// A warning that a problem occurred that can't be easily fixed or overcome by simply retrying, and the problem was not severe enough to stop the work.
		/// </summary>
		UserFacingWarning,
		/// <summary>
		/// A notification that a problem occurred that stopped the work, but the user might be able to overcome by fixing something under their control or maybe just retrying the operation.
		/// </summary>
		UserFacingErrorFixAndRetryAdvised,
		/// <summary>
		/// A problem occurred that stopped the work.
		/// </summary>
		UserFacingErrorFatal
	}

	/// <summary>
	/// Many functions allow an optional parameter of this delegate type. If not null, they will notify you/the user of events by calling into this delegate.
	/// You can also use this as a generic "callback" from your business logic into the UI.
	/// </summary>
	/// <param name="notficationLevel">the level of notification (in case you want to filter or process differently)</param>
	/// <param name="message">a description of what just happened</param>
	/// <param name="offerCancel">whether or not to offer the user to cancel. if this is true, you can cancel the action by returning true or have it continue by returning false</param>
	/// <param name="param">Extra parameters that you want to pass to IWorkNotification implementation if you happen to know what it is.</param>
	/// <returns>If offerCancel is true, and the IWorkNotification implementation supports cancellation, returns a Boolean indicating whether the client's activity should be canceled or not (true=cancel, false=continue). In case of custom handling in your own IWorkNotification implementation, returns whatever your implementatoin decides.</returns>
	public delegate object WorkNotification(NotificationLevel notficationLevel, string message, bool offerCancel, object[] param);

	/// <summary>
	/// An interface that describes a class that implements a function that can be used as a WorkNotification.
	/// You don't have to implement IWorkNotification... any function with the same signature as WorkNotification will work.
	/// </summary>
	public interface IWorkNotification
	{
		/// <summary>
		/// The implementation of the WorkNotification delegate to call into to notify the calling thread.
		/// </summary>
		object NotifyOfWork(NotificationLevel notficationLevel, string message, bool offerCancel, object[] param);
	}
}