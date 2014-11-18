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
using System.Diagnostics;
using System.Reflection;
using Intuit.Common.Util.Properties;

namespace Intuit.Common.Util
{
	/// <summary>
	/// A helper class that parses a string that contains an assembly and type. The string should be specified in
	/// the following format: AssemblyNameWithNoDllEnding, FullyQualifiedTypeName.
	/// </summary>
	public class AssembyTypeParser
	{
		private readonly string m_AssemblyName;
		private readonly string m_TypeName;

		/// <summary>
		/// Parse the assembly type string.
		/// </summary>
		/// <param name="assemblyTypeString">the assembly / type string to parse</param>
		public AssembyTypeParser(string assemblyTypeString)
		{
			StringHelper.EnforceParameterNotNull("assemblyTypeString", assemblyTypeString);
			string[] assemblyTypeStringArray = assemblyTypeString.Split(new[] {','});
			if (assemblyTypeStringArray.Length != 2)
			{
				throw new ArgumentException(Resources.The_assembly_type_specified_must_be_in_the_following_format, "assemblyTypeString");
			}
			m_AssemblyName = assemblyTypeStringArray[0].Trim();
			m_TypeName = assemblyTypeStringArray[1].Trim();
		}

		/// <summary>
		/// The parsed assembly name.
		/// </summary>
		public string AssemblyName
		{
			get
			{
				return m_AssemblyName;
			}
		}

		/// <summary>
		/// The parsed type name.
		/// </summary>
		public string TypeName
		{
			get
			{
				return m_TypeName;
			}
		}

		/// <summary>
		/// Callback delegate for FindAssemblyByModuleName
		/// </summary>
		/// <seealso cref="FindAssemblyByModuleName"/>
		public delegate void ModuleWithMatchingNameFound(AssemblyName assemblyName);

		/// <summary>
		/// Combs through all assemblies currently loaded in the current AppDomain. If any of their manifest modules (i.e. the main DLL) matches the provided name, the provided delegate gets called with an AssemblyName object describing that assembly.
		/// </summary>
		/// <param name="moduleName">a module name such as "myassembly.dll"</param>
		/// <param name="moduleWithMatchingNameFound">delegate will be called for each matching module</param>
		public static void FindAssemblyByModuleName(string moduleName, ModuleWithMatchingNameFound moduleWithMatchingNameFound)
		{
			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				if (assembly.ManifestModule.Name.Equals(moduleName, StringComparison.InvariantCultureIgnoreCase))
				{
					string fullName = assembly.FullName;
					if (fullName != null)
					{
						AssemblyName assemblyName = new AssemblyName(fullName);
						moduleWithMatchingNameFound(assemblyName);
					}
				}
			}
		}

		/// <summary>
		/// Extracts the version of the given assembly, or returns null if not found.
		/// </summary>
		/// <param name="assembly"></param>
		/// <returns></returns>
		public static Version GetAssemblyVersion(Assembly assembly)
		{
			return assembly.FullName != null ? new AssemblyName(assembly.FullName).Version : null;
		}

		/// <summary>
		/// Returns the name of the function that called the caller of this function.
		/// </summary>
		/// <returns>Name of the caller function, or null if it couldn't be determined</returns>
		public static string GetCallingFunctionCallersName()
		{
			return GetCallersName(3);
		}

		/// <summary>
		/// Returns the name of the function that called the caller of the caller of this function.
		/// </summary>
		/// <returns>Name of the caller function, or null if it couldn't be determined</returns>
		public static string GetCallingFunctionsName()
		{
			return GetCallersName(2);
		}

		/// <summary>
		/// Returns the name of the function in the given <paramref name="framePosition"/> of the stacktrace while this function executes.
		/// </summary>
		/// <param name="framePosition">0 will return you this (<see cref="GetCallersName"/>) function's name. 1 the name of the function calling this. 2 the name of the caller of the function calling this, and so on.</param>
		/// <returns>Name of the caller (or caller's caller) function, or null if it couldn't be determined</returns>
		public static string GetCallersName(int framePosition)
		{
			string callingFunctionName = null;
			try
			{
				// get calling method name
				var stackTrace = new StackTrace();
				var frames = stackTrace.GetFrames();
				if (frames != null && frames.Length >= framePosition)
				{
					var frame = frames[framePosition];
					if (frame != null)
					{
						string className = null;
						string methodName = null;
						var methodBase = frame.GetMethod();
						if (methodBase != null)
						{
							methodName = methodBase.Name;
							var type = methodBase.DeclaringType;
							if (type != null)
							{
								className = type.FullName;
							}
						}
						if (methodName != null)
						{
							callingFunctionName = className != null ? String.Format("{0}.{1}", className, methodName) : methodName;
						}
					}
				}
			}
				// ReSharper disable EmptyGeneralCatchClause
			catch
				// ReSharper restore EmptyGeneralCatchClause
			{
				// ignore
			}
			return callingFunctionName;
		}
	}
}