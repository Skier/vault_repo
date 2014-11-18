using System;
using System.Collections;

namespace DPI.Interfaces
{
	/// <summary>
	/// The error class interface which is included inside DTO objects
	/// </summary>
	public interface IErrorInfo
	{
		int Number {get; set;}
		string Message {get;  set;}
	//	bool IsDebugOnly {get; set;}

	}
	public interface IErrorDto
	{
		bool IsError {get;}
		IErrorInfo[] ErrorInfo {get; }
		void AddError(int errNum, string msg);//, bool debugOnly);
		void AddError(string msg);
	}
}