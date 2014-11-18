namespace Dalworth.LeadCentral.SDK
{
    public class StringUtil
    {

        ///<summary>
        /// Extract all numbers from source string.
        ///</summary>
        public static string ExtractDigits(string source)
        {
            return string.Join(null, System.Text.RegularExpressions.Regex.Split(source, "[^\\d]"));
        }

        ///<summary>
        /// Extract last seven digits from source string.
        ///</summary>
        public static string ExtractLastSevenDigits(string value)
        {
            var numbers = ExtractDigits(value);
            return numbers.Length > 7 ? numbers.Substring(numbers.Length - 7) : numbers;
        }

    }
}
