using System;
  
namespace Dalworth.Server.Domain
{
    public enum CreditCardCVV2TypeEnum
    {
        NotUsed = 0,
        Used = 1,
        Illegible = 2,
        NoCVV2Imprinted = 9
    }

    public partial class CreditCardCVV2Type
    {
        public CreditCardCVV2Type(){}
    }
}
      