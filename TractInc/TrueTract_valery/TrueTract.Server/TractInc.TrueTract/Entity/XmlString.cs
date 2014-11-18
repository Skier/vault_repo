namespace TractInc.TrueTract.Entity
{
    class XmlString
    {
        public static string validate(string str)
        {
            if (str == null)
                return "";
            else 
                return str.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;").Replace("\t", "&#10;");
        }
    }
}
