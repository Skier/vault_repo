using System;
using System.Collections.Generic;
using System.Text;
using Weborb;

namespace Servman.Weborb
{
	/// <summary>
	/// WebORB-enabled Web Service
	/// </summary>
	public class SampleService
	{
		public SampleService()
		{
		}

		public string echo(string text)
		{
			return "Service echo: " + text;
		}
	}
}
