using System.Collections;

namespace DPI.Interfaces 
{	
	public interface ICellPhoneInfo : IDomObj
	{
		int WireleesProduct		  { get; set;}
		string Pin				  { get; set;}
		string Zip				  { get; set;}
		string NewESN			  { get; set;}
		string PhoneNumber		  { get; set;}
		string ControlNumber	  { get; set;} 
		decimal ActivationCharge  { get; set;}
		DictionaryEntry[] Entries { get; }
	}
}