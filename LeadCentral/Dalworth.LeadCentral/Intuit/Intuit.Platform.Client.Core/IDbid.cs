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

namespace Intuit.Platform.Client.Core
{
	/// <summary>
	/// Tags a class that identifies an application or database instance or a table inside one of those, and can therefore return its <see cref="Dbid"/>.
	/// </summary>
	public interface IDbid
	{
		/// <summary>
		/// The application's or database's instance ID, or one of its tables' ID.
		/// </summary>
		string Dbid { get; }
	}
}