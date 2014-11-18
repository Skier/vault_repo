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
using Intuit.Platform.Client.Core.Properties;

namespace Intuit.Platform.Client.Core
{
	/// <summary>
	/// Version of an instance's schema
	/// </summary>
	public class SchemaVersion : IComparable
	{
		/// <summary>
		/// New SchemaVersion based on version component values
		/// </summary>
		public SchemaVersion(int majorVersion, int minorVersion)
		{
			MajorVersion = majorVersion;
			MinorVersion = minorVersion;
		}

		/// <summary>
		/// Major version
		/// </summary>
		public int MajorVersion { get; set; }

		/// <summary>
		/// Minor version
		/// </summary>
		public int MinorVersion { get; set; }

		/// <summary>
		/// Returns "Major.Minor"
		/// </summary>
		public override string ToString()
		{
			return MajorVersion + "." + MinorVersion;
		}

		#region IComparable Members

		/// <summary>
		/// <see cref="IComparable"/> implementation.
		/// </summary>
		/// <param name="obj">another SchemaVersion object</param>
		/// <returns>0 if schema versions are the same, a negative number if obj is higher than this, a positive number if obj is lower than this</returns>
		public int CompareTo(object obj)
		{
			var otherSchemaVersion = obj as SchemaVersion;
			if (otherSchemaVersion != null)
			{
				int c = MajorVersion.CompareTo(otherSchemaVersion.MajorVersion);

				if (c != 0)
				{
					return c;
				}

				return MinorVersion.CompareTo(otherSchemaVersion.MinorVersion);
			}
			throw new ArgumentException(Resources.PlatformClientException_ObjectMustBeSchemaversion_object_must_be_SchemaVersion, "obj");
		}

		#endregion

		/// <summary>
		/// Returns true if <paramref name="otherSchemaVersion"/> is higher (newer) than this instance.
		/// </summary>
		public bool IsLowerVersionThan(SchemaVersion otherSchemaVersion)
		{
			return CompareTo(otherSchemaVersion) < 0;
		}
	}
}