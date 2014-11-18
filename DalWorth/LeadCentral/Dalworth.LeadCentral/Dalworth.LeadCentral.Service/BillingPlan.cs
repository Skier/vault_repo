namespace Dalworth.LeadCentral.Service
{
    public class BillingPlan
    {
        public const string Base = "Base";
        public const string Silver = "Silver";
        public const string Gold = "Gold";
        public const string Platinum = "Platinum";

        public static decimal GetPriceByName(string planName)
        {
            switch (planName)
            {
                case Base :
                    return 20;
                case Silver:
                    return 40;
                case Gold:
                    return 60;
                case Platinum:
                    return 100;
                default :
                    return 0;
            }
        }
    }
}
