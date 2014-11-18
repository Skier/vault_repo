using System;
using System.Collections;
using DPI.Interfaces;

namespace DPI.Components
{
	/// <summary>
	/// An error class to be included in DTOs. 
	/// </summary>
	public class ErrorDto : IErrorDto
	{
		ArrayList errors;
		/*		Properties		*/
		public bool IsError
		{
			get 
			{
				return errors.Count > 0;
			}
		}
		public IErrorInfo[] ErrorInfo
		{
			get { return (IErrorInfo[])errors.ToArray(typeof(ErrorInfo));}
		}
		/*		Ctors		*/
		public ErrorDto()
		{
			errors = new ArrayList();
		}
		/*		Methods		*/
		/// <summary>
		///		Use this method to add an error to the collection. 
		/// </summary>
		/// <param name="ErrorNumber">Can be homemade or come from the source of the error. SQL Server, etc.</param>
		/// <param name="ErrorMessage">Can be the original message or customized</param>
		/// <param name="devOnly">If this is true, the message is only for tracking/logging and not for user display.</param>
		public void AddError(int ErrorNumber, string ErrorMessage)//, bool devOnly)
		{
			AddError(new ErrorInfo(ErrorNumber, ErrorMessage));//, devOnly));
		}
		public void AddError(string ErrorMessage)//, bool devOnly)
		{
			AddError(0, ErrorMessage);//, devOnly));
		}
		public void AddError(IErrorInfo ErrorData)
		{
			errors.Add(ErrorData);
		}
	}
	public class ErrorInfo : IErrorInfo
	{
		int number;
		string message;
	//	public bool isDebugOnly = false;
		public ErrorInfo(int ErrorNumber, string ErrorMessage)//, bool debugOnly)
		{
			Number = ErrorNumber;
			Message = ErrorMessage;
			//isDebugOnly = debugOnly;
		}

		public string Message
		{
			get {return message;}
			set {message = value;}
		}

		public int Number
		{
			get { return number; }
			set { number = value; }
		}

	/*	public bool IsDebugOnly
		{
			get {return isDebugOnly;}
			set { isDebugOnly = value;}
		}
	*/
	}
}