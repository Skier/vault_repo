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
	/// Interface for Exception class implementations that know whether the error that caused the exception is temporary and/or easily fixable by the user.
	/// </summary>
	public interface IExceptionKnowsIfRetryAdvised
	{
		/// <summary>
		/// Returns true if the problem causing the exception can either be fixed by the user or might go away if user retries the operation later.
		/// </summary>
		bool FixAndRetryAdvised { get; set; }
	}
}