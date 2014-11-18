using System;

using DPI.Interfaces;

namespace DPI.Components
{
	[Serializable]
	public class KeyVal : IKeyVal
	{
		string key;
		string val;

		public string Key
		{
			get { return key;  }
			set { key = value; }
		}
		public string Val
		{
			get { return val; }
			set { val = value; }
		}
		public KeyVal() {}
		public KeyVal(string key, string val)
		{
			this.key = key;
			this.val = val;
		}
	}
}