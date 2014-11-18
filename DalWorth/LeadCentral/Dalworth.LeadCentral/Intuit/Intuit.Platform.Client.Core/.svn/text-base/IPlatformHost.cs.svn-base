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
using System.Reflection;

namespace Intuit.Platform.Client.Core
{
	/// <summary>
	/// Describes a host that accepts API requests
	/// </summary>
	public interface IPlatformHost
	{
		/// <summary>
		/// The user-facing name of the platform
		/// </summary>
		[Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
		[Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
		string CustomerFacingName { get; }

		/// <summary>
		/// DNS hostname of the host
		/// </summary>
		[Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
		[Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
		string Hostname { get; }

		/// <summary>
		/// https:// or http://
		/// </summary>
		[Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
		[Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
		string ConnectionScheme { get; }

		/// <summary>
		/// Whether or not this host should recieve HTTPS instead of HTTP request.
		/// </summary>
		[Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
		[Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
		bool UseSecureConnectionScheme { get; }

		/// <summary>
		/// Constructs the URL that points to the given dbid for this host.
		/// </summary>
		/// <returns>the Uri for this dbid, or null if dbid is null or empty</returns>
		Uri MakeDbidUrl(string dbid);
	}
}