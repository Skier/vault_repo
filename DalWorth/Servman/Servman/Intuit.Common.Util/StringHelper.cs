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
using System.Security;
using System.Text;
using System.Xml;
using Intuit.Common.Util.Properties;

namespace Intuit.Common.Util
{
	/// <summary>
	/// Helper functions to deal with strings, text and XML.
	/// </summary>
	public class StringHelper
	{
		private const string TwoSpaces = "  ";
		private const string OneSpace = " ";

		/// <summary>
		/// Mask some or all digits of a confidential number. Leaves punctuation and letters untouched.
		/// E.g., to mask all but the last four digits of a social security number, pass in the SSN as the confidential number, 4 as the maxUnmaskedDigits and '*' as the maskChar.
		/// </summary>
		/// <param name="confidentialNumber">the confidential number, e.g. "123-45-6789"</param>
		/// <param name="maxUnmaskedDigits">the number of digits that may remain unmasked (if this is equal or higher than the total number of digits, nothing gets masked)</param>
		/// <param name="maskChar">the char to replace the masked digits with, e.g. '*'</param>
		/// <returns></returns>
		public static string MaskConfidentialNumberString(string confidentialNumber, int maxUnmaskedDigits, char maskChar)
		{
			if (confidentialNumber == null)
			{
				return null;
			}
			StringBuilder newStr = new StringBuilder();
			int unmaskedDigits = 0;
			for (int i = confidentialNumber.Length - 1; i >= 0; i--)
			{
				char digit = confidentialNumber[i];
				if (Char.IsDigit(digit))
				{
					unmaskedDigits++;
					if (unmaskedDigits > maxUnmaskedDigits)
					{
						newStr.Insert(0, maskChar);
						continue;
					}
				}
				newStr.Insert(0, digit);
			}
			return newStr.ToString();
		}

		/// <summary>
		/// Return null if the string is empty or just contains spaces, otherwise the string itself.
		/// </summary>
		public static string EmptyToNull(string stringToFix)
		{
			return (stringToFix != null && stringToFix.Trim().Length == 0) ? null : stringToFix;
		}

		/// <summary>
		/// Returns empty string if stringToFix is null or empty or only contains spaces, otherwise the string itself.
		/// </summary>
		public static string NullOrSpacesToEmpty(string stringToFix)
		{
			return IsEmpty(stringToFix) ? String.Empty : stringToFix;
		}

		/// <summary>
		/// Throw an <code>ArgumentNullException</code> if the value is null. This is
		/// a nice method to use to ensure parameters are not null.
		/// </summary>
		/// <param name="paramName">the name of the variable</param>
		/// <param name="value">the value of the variable</param>
		/// <exception cref="ArgumentNullException">thrown if the value is null</exception>
		public static void EnforceParameterNotNull(string paramName, object value)
		{
			if (value == null)
			{
				string callingMethodName = AssembyTypeParser.GetCallingFunctionCallersName();
				if (callingMethodName == null)
				{
					throw new ArgumentNullException(paramName); // this has to be paramName, not "paramName", in case you're tempted to "fix it"
				}
				throw new ArgumentNullException(paramName, String.Format("Method \"{0}\" expects parameter \"{1}\" to not be null.", callingMethodName, paramName));
			}
		}

		/// <summary>
		/// Condense any and all occurrences of consecutive spaces in a string to a single space.
		/// </summary>
		/// <param name="s">The original string</param>
		/// <returns>The original string with any occurrence of consecutive spaces reduced to a single space.</returns>
		public static string StripMultipleSpaces(string s)
		{
			for (string newResult = s.Replace(TwoSpaces, OneSpace); newResult.Length != s.Length; )
			{
				s = newResult;
				newResult = s.Replace(TwoSpaces, OneSpace);
			}

			return s;
		}

		/// <summary>
		/// Returns true when the string is either null or empty (with/without spaces)
		/// </summary>
		/// <param name="val"></param>
		/// <returns></returns>
		public static bool IsEmpty(string val)
		{
			//to determine if the data is empty
			if (val == null)
			{
				return true;
			}
			return val.Trim().Length == 0;
		}

		/// <summary>
		/// Makes sure that a given string doesn't exceed a certain length.
		/// If the length of longString exceeds maxLengthIncludingEllipsis, it's cut off and an ellipsis (three dots) are added to the end so that the total length of cut-off string and ellipsis add up to the desired maximum length.
		/// </summary>
		/// <param name="longString"></param>
		/// <param name="maxLengthIncludingEllipsis"></param>
		/// <returns></returns>
		public static string EllipsisString(string longString, int maxLengthIncludingEllipsis)
		{
			string ellipsis = Resources.StringHelper_EllipsisString;
			return longString.Length > maxLengthIncludingEllipsis ? longString.Substring(0, maxLengthIncludingEllipsis - ellipsis.Length) + ellipsis : longString;
		}

		/// <summary>
		/// Given a first, middle and last name, builds a "full name", with the ordering determined by lastNameAtBeginning.
		/// If lastNameAtBeginning is true, returns "Last, First Middle", otherwise "First Middle Last".
		/// </summary>
		public static string BuildThreePartName(string first, string middle, string last, bool lastNameAtBeginning)
		{
			if (lastNameAtBeginning)
			{
				string lfm = TrimAndConcatWithSpace(TrimAndConcatWithSpace(Trim(last) + ",", first), middle);
				return lfm == "," ? String.Empty : lfm;
			}
			return TrimAndConcatWithSpace(TrimAndConcatWithSpace(first, middle), last);
		}


		/// <summary>
		/// Converts string from null to empty or trims if not empty.
		/// </summary>
		/// <param name="str">the string to be trimmed</param>
		/// <returns>if str was null, returns String.Empty, otherwise the result of str.Trim()</returns>
		public static string Trim(string str)
		{
			return str == null ? String.Empty : str.Trim();
		}

		/// <summary>
		/// Converts both strings from null to empty, trims both, concatenates them, insert a space inbetween if both are not empty.
		/// </summary>
		public static string TrimAndConcatWithSpace(string firstPart, string secondPart)
		{
			firstPart = Trim(firstPart);
			secondPart = Trim(secondPart);
			return firstPart + ((firstPart.Length > 0 && secondPart.Length > 0) ? " " : String.Empty) + secondPart;
		}

		/// <summary>
		/// Appends a single space to the string if it's not an empty string. Useful when concatenating strings to make sure they're separated by spaces.
		/// </summary>
		public static string AddSpaceIfNotEmpty(string str)
		{
			return String.IsNullOrEmpty(str) ? String.Empty : (str + OneSpace);
		}

		/// <summary>
		/// Format a U.S. Federal Employer Identification Number (EIN). Returns null if not a valid EIN.
		/// </summary>
		/// <param name="ein"></param>
		/// <param name="removeFormatting">if true, the EIN will be formatted with a dash at the correct location. if false, the dash will be stripped.</param>
		public static string FormatEin(string ein, bool removeFormatting)
		{
			if (ein == null)
			{
				return null;
			}
			string formattedEin = String.Empty;
			int numDigits = 0;

			if (!(ein.Contains("-") ? (ein.Length == 10) : (ein.Length == 9)))
			{
				return null;
			}
			for (int i = 0; i < ein.Length; i++)
			{
				char currentDigit = ein[i];

				if (Char.IsNumber(currentDigit) || currentDigit == '-')
				{
					numDigits++;
					if (numDigits == 3)
					{
						formattedEin += removeFormatting ? String.Empty : "-";
						formattedEin += (currentDigit == '-') ? String.Empty : currentDigit.ToString();
					}
					else if (currentDigit == '-')
					{
						return null;
					}
					else
					{
						formattedEin += currentDigit;
					}
				}
				else
				{
					return null;
				}
			}
			return formattedEin;
		}

		/// <summary>
		/// Compares to EINs regardless of their formatting.
		/// </summary>
		public static bool AreEinsTheSame(string ein1, string ein2)
		{
			string formattedEin1 = FormatEin(ein1, true);
			string formattedEin2 = FormatEin(ein2, true);

			if (formattedEin1 == null || formattedEin2 == null)
			{
				return false;
			}
			return formattedEin1.Equals(formattedEin2);
		}

		/// <summary>
		/// Utility function to load a XmlDocument. Can read from a string or from a file, you must specify either xmlString or xmlFilename but not both.
		/// Catches all common exceptions and will return the exception message is an exception was thrown.
		/// Will throw ArgumentException if neither or both of xmlString/xmlFilename are specified.
		/// </summary>
		/// <param name="xmlString">a XML document as a string</param>
		/// <param name="xmlFilename">the name or URL of a file containing XML</param>
		/// <param name="document">the document contained in the string or at the URL/file location</param>
		/// <returns>null if XML document was successfully loaded. Else, returns an error string you can present to the user.</returns>
		public static string GetXmlDocument(string xmlString, string xmlFilename, out XmlDocument document)
		{
			document = null;
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				if (!String.IsNullOrEmpty(xmlString))
				{
					if (!String.IsNullOrEmpty(xmlFilename))
					{
						throw new ArgumentException(Resources.StringHelper_EitherXmlstringOrXmlfilenameHaveToBeSpecifiedButNotBoth_Either_xmlString_or_xmlFilename_have_to_be_specified__but_not_both_);
					}
					xmlDocument.LoadXml(xmlString);
				}
				else if (String.IsNullOrEmpty(xmlFilename))
				{
					throw new ArgumentException(Resources.StringHelper_NeitherXmlstringOrXmlfilenameWereSpecified_Neither_xmlString_or_xmlFilename_were_specified_);
				}
				else
				{
					try
					{
						xmlDocument.Load(xmlFilename);
					}
					catch (FileNotFoundException e)
					{
						return e.Message;
					}
					catch (PathTooLongException e)
					{
						return e.Message;
					}
					catch (DirectoryNotFoundException e)
					{
						return e.Message;
					}
					catch (IOException e)
					{
						return e.Message;
					}
					catch (UnauthorizedAccessException e)
					{
						return e.Message;
					}
					catch (NotSupportedException e)
					{
						return e.Message;
					}
					catch (SecurityException e)
					{
						return e.Message;
					}
					catch (ArgumentException e)
					{
						return e.Message;
					}
				}
			}
			catch (XmlException e)
			{
				return e.Message;
			}
			document = xmlDocument;
			return null;
		}

		/// <summary>
		/// Reads the InnerText of a top-level node from the xmlDocument with the given nodeName, returns null if the node didn't exist.
		/// </summary>
		public static string ReadNode(XmlNode xmlDocument, string nodeName)
		{
			XmlNode node = xmlDocument.SelectSingleNode("//" + nodeName);
			return (node == null) ? null : node.InnerText;
		}

		/// <summary>
		/// Adds a new text node with the given nodeName to the root element of xmlDocument and inserts text as its content.
		/// </summary>
		public static void AddTextNode(XmlDocument xmlDocument, XmlNode root, string nodeName, string text)
		{
			if (text != null)
			{
				XmlElement xmlElement = xmlDocument.CreateElement(nodeName);
				xmlElement.AppendChild(xmlDocument.CreateTextNode(text));
				root.AppendChild(xmlElement);
			}
		}

		/// <summary>
		/// Creates an XmlDocument with the given root element name.
		/// </summary>
		public static XmlDocument MakeXmlDoc(out XmlElement root, string roolElementName)
		{
			var idXml = new XmlDocument();
			root = idXml.CreateElement(roolElementName);
			idXml.AppendChild(root);
			return idXml;
		}

		///<summary>
		/// Returns empty string if the value is null, otherwise just returns the value.
		///</summary>
		public static string NullToEmpty(string value)
		{
			return value ?? String.Empty;
		}

		///<summary>
		/// Given long values of Total and Partial, scales Total to fit into an int and adjusts Partial so that the ratio of partial to total is preserved (Partial64/Total64=Partial32/Total32). 
		///</summary>
		public static void ScaleTotalAndPartial(long total64, long part64, out int total32, out int part32)
		{
			ScaleTotalAndPartial(Decimal.ToInt32,  total64, part64, Int32.MaxValue, out total32, out part32, true);
		}

		///<summary>
		/// If Total is larger than max, scales Total and Partial down so their ratio is preserved (Total will be max, Partial will be Partial/Total*max). Set avoidZero to true if you want scaledPartial only to be 0 if partial was 0.
		///</summary>
		public static void ScaleTotalAndPartial<T>(Converter<Decimal, T> converter, decimal total, decimal partial, decimal max, out T scaledTotal, out T scaledPartial, bool avoidZero)
		{
			if (total < partial)
			{
				throw new ArgumentException("partial must not be larger than total");
			}
			if (total <= 0 || partial < 0)
			{
				throw new ArgumentException("total must be greater than 0 and partial must not be negative");
			}

			if (total > max)
			{
				scaledTotal = converter(max);
				decimal scaledPartialDec = Math.Round((partial / total) * max);
				if (avoidZero && scaledPartialDec == 0 && partial != 0)
				{
					scaledPartialDec = 1; // 0 is a special value for partial, so avoid it unless it was 0 in the first place
				}
				scaledPartial = converter(scaledPartialDec);
			}
			else
			{
				scaledTotal = converter(total);
				scaledPartial = converter(partial);
			}
		}
	}
}