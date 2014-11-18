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

namespace Intuit.Common.Util
{
	/// <summary>
	/// Tags a class as being able to handle exceptions
	/// </summary>
	public interface IExceptionHandler
	{
		/// <summary>
		/// Accepts exception e and returns true if it was handled, otherwise false.
		/// </summary>
		bool HandleException(Exception e);
	}
}