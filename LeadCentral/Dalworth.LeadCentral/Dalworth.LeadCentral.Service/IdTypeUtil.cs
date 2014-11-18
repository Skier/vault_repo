using System;
using Intuit.Sb.Cdm;

namespace Dalworth.LeadCentral.Service
{
    public class IdTypeUtil
    {
        private const string IdSplitter = ":";
        
        public static string GetQbIdString(IdType qbId)
        {
            return Enum.GetName(typeof(idDomainEnum), qbId.idDomain) + IdSplitter + qbId.Value;
        }

        public static IdType ParseQbIdString(string qbIdStr)
        {
            var result = new IdType();
            var arr = qbIdStr.Split(IdSplitter.ToCharArray());
            if (arr.Length > 1)
            {
                result.idDomain = (idDomainEnum)Enum.Parse(typeof(idDomainEnum), arr[0], true);
                result.Value = arr[1];
                return result;
            }
            return null;
        }

    }
}
