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

namespace Intuit.Common.Util
{
	/// <summary>
	/// Helper functions for dealing with streams.
	/// </summary>
	public class StreamHelper
	{
		/// <summary>
		/// Reads the given stream byte by byte until it ends and then converts the content to a byte array. Doesn't close the stream.
		/// </summary>
		public static byte[] StreamToByteArray(Stream stream)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				int b;
				while ((b = stream.ReadByte()) != -1)
				{
					ms.WriteByte(Convert.ToByte(b));
				}
				return ms.ToArray();
			}
		}
	}
}