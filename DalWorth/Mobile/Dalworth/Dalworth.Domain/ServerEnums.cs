using System;
using System.Collections.Generic;
using System.Text;

namespace Dalworth.Domain
{    
    public enum CreditCardTypeEnum
    {
        Visa = 1,
        MasterCard = 2,
        Dinner = 3,
        Discover = 4,
    }

    public enum WorkTransactionPaymentTypeEnum
    {
        CreditCard = 1,
        BankCheck = 2,
        Cash = 3,
    }

    public enum CreditCardCVV2TypeEnum
    {
        NotUsed = 0,
        Used = 1,
        Illegible = 2,
        NoCVV2Imprinted = 9
    }

    public enum BankCheckAccountTypeEnum
    {
        Personal = 1,
        Company = 2
    }
}
