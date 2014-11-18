using System;
  
namespace Dalworth.Server.Servman.Domain
{
    public partial class m_alt_ad
    {
        public m_alt_ad(){}

        #region ZipParsed

        public int? ZipParsed
        {
            get
            {
                if (zip == null || zip.Trim() == string.Empty)
                    return null;

                try
                {
                    return int.Parse(zip.Trim());
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        #endregion
    }
}
      