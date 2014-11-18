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

using System.Collections.Generic;
using System.Text;

namespace Intuit.Platform.Client.Core
{
	/// <summary>
	/// Helps repair encoding of QuickBase responses.
	/// </summary>
	/// <seealso cref="FixQuickBaseEncoding"/>
	public class EncodingFixer
	{
		/// <summary>
		/// A list of characters for which QuickBase always uses Windows-1252 encoding. For use in FixQuickBaseEncoding().
		/// "LEFT DOUBLE QUOTATION MARK"
		/// "RIGHT DOUBLE QUOTATION MARK"
		/// "EN DASH"
		/// </summary>
		private static readonly Dictionary<byte, char> Replacements = new Dictionary<byte, char> {{147, '\u201C'}, {148, '\u201D'}, {150, '\u2013'}};

		/// <summary>
		/// QuickBase has a peculiar "feature" where when it is given certain characters on input it will convert
		/// them into Windows-1252 encoding and store them as such in its database.
		/// (That is to help Windows users when they use the QuickBase HTML UI)
		/// When we query that data it will remain this way... these few characters will be Windows-1252 encoded,
		/// surrounded by otherwise UTF8-encoded XML.
		/// Since we expect UTF-8 encoding for XML parsing, we first have to reencode those characters.
		/// This peculiar "feature" was recently "removed" from Workplace, but still exists in QuickBase.
		/// However, it is benign to run a Workplace (or any other platforms) stream through this since it
		/// does nothing if the input is properly encoded in UTF8.
		/// </summary>
		/// <param name="encodedBytes">a response from QuickBase that's mostly UTF8 encoded but has Windows-1252-encoded characters embedded in it</param>
		public static string FixQuickBaseEncoding(byte[] encodedBytes)
		{
			int restartPosition = 0;
			StringBuilder decodedString = new StringBuilder(encodedBytes.Length);
			Encoding enc8 = new UTF8Encoding(false, true);
			while (restartPosition < encodedBytes.Length)
			{
				try
				{
					decodedString.Append(enc8.GetString(encodedBytes, restartPosition, encodedBytes.Length - restartPosition));
					return decodedString.ToString();
				}
				catch (DecoderFallbackException e)
				{
					int badPosition = e.Index + 1 + restartPosition;

					decodedString.Append(enc8.GetString(encodedBytes, restartPosition, badPosition - restartPosition));

					byte trip = encodedBytes[badPosition];
					if (Replacements.ContainsKey(trip))
					{
						decodedString.Append(Replacements[trip]);
					}
					else
					{
						// nothing we can/should do
						decodedString.Append(trip);
					}
					restartPosition = badPosition + 1;
				}
			}
			return decodedString.ToString();
		}
	}
}