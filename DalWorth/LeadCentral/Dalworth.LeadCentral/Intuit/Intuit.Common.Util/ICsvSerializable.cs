namespace Intuit.Common.Util
{
	/// <summary>
	/// A class that can serialize itself into a Comma-Separated-Line (CSV). Use <see cref="Intuit.Common.Util.CsvHelper"/> to generate CSV for collections of these.
	/// </summary>
	public interface ICsvSerializable
	{
		/// <summary>
		/// Builds the first line of a CSV file, the optional header row which contains column names.
		/// </summary>
		/// <returns></returns>
		string GetCsvHeader();

		/// <summary>
		/// Builds one line of a CSV file, containing all the values of this object.
		/// </summary>
		/// <returns></returns>
		string GetCsvLine();
	}
}