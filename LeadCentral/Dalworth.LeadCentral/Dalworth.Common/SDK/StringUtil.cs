namespace Dalworth.Common.SDK
{
    public class StringUtil
    {
        ///<summary>
        /// Extract all numbers from source string.
        ///</summary>
        public static string ExtractDigits(string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            return string.Join(null, System.Text.RegularExpressions.Regex.Split(source, "[^\\d]"));
        }

        ///<summary>
        /// Extract last seven digits from source string.
        ///</summary>
        public static string ExtractLastSevenDigits(string value)
        {
            return ExtractLastDigits(value, 7);
        }

        ///<summary>
        /// Extract last seven digits from source string.
        ///</summary>
        public static string ExtractLastDigits(string value, int count)
        {
            var numbers = ExtractDigits(value);
            return numbers.Length > count ? numbers.Substring(numbers.Length - count) : numbers;
        }

    }
}
