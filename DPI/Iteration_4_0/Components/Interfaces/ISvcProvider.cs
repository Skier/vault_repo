namespace DPI.Interfaces
{	
	public interface ISvcProvider
	{
		string Provider { get; }
		void FireAway(string action, string xml);
	}	
}