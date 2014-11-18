using System;
 
namespace DPI.Interfaces
{	
	public interface  IUserAccountExtension
	{
		int		Id					{ get; }
		int		AcctId				{ get; }
		string	UserName			{ get; }
		string	Password			{ get; }
		string	EntityName			{ get; }
		string	ApplicationName		{ get; }
		string	Url					{ get; }
	}
}