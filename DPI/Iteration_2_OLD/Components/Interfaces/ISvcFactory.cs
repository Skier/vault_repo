namespace DPI.Interfaces
{	
	public interface ISvcFactory
	{
		ISvcProvider GetProvider(string provider);
	}
}