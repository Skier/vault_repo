
namespace DPI.Interfaces
{	
	public interface IAddressPageDTO
	{
		int AccNumber			{ get; set; }
		IAddr MailAddress		{ get; set; }
	}
}