using System.Data;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.Domain
{
    public partial class DetailAccountType
    {
        
        public static DetailAccountType AP                                    = new DetailAccountType(0,"AP");
        public static DetailAccountType AR                                    = new DetailAccountType(1,"AR");
        public static DetailAccountType AccumulatedAdjustment                 = new DetailAccountType(2,"AccumulatedAdjustment");
        public static DetailAccountType AccumulatedAmortization               = new DetailAccountType(3,"AccumulatedAmortization");
        public static DetailAccountType AccumulatedAmortizationOfOtherAssets  = new DetailAccountType(4,"AccumulatedAmortizationOfOtherAssets");
        public static DetailAccountType AccumulatedDepletion                  = new DetailAccountType(5,"AccumulatedDepletion");
        public static DetailAccountType AccumulatedDepreciation               = new DetailAccountType(6,"AccumulatedDepreciation");
        public static DetailAccountType AdvertisingOrPromotional              = new DetailAccountType(7,"AdvertisingOrPromotional");
        public static DetailAccountType AllowanceForBadDebts                  = new DetailAccountType(8,"AllowanceForBadDebts");
        public static DetailAccountType Amortization                          = new DetailAccountType(9,"Amortization");
        public static DetailAccountType Auto                                  = new DetailAccountType(10,"Auto");
        public static DetailAccountType BadDebts                              = new DetailAccountType(11,"BadDebts");
        public static DetailAccountType BankCharges                           = new DetailAccountType(12,"BankCharges");
        public static DetailAccountType Buildings                             = new DetailAccountType(13,"Buildings");
        public static DetailAccountType CashOnHand                            = new DetailAccountType(14,"CashOnHand");
        public static DetailAccountType CharitableContributions               = new DetailAccountType(15,"CharitableContributions");
        public static DetailAccountType Checking                              = new DetailAccountType(16,"Checking");
        public static DetailAccountType CommonStock                           = new DetailAccountType(17,"CommonStock");
        public static DetailAccountType CostOfLabor                           = new DetailAccountType(18,"CostOfLabor");
        public static DetailAccountType CostOfLaborCOS                        = new DetailAccountType(19,"CostOfLaborCOS");
        public static DetailAccountType CreditCard                            = new DetailAccountType(20,"CreditCard");
        public static DetailAccountType DepletableAssets                      = new DetailAccountType(21,"DepletableAssets");
        public static DetailAccountType Depreciation                          = new DetailAccountType(22,"Depreciation");
        public static DetailAccountType DevelopmentCosts                      = new DetailAccountType(23,"DevelopmentCosts");
        public static DetailAccountType DiscountsOrRefundsGiven               = new DetailAccountType(24,"DiscountsOrRefundsGiven");
        public static DetailAccountType DividendIncome                        = new DetailAccountType(25,"DividendIncome");
        public static DetailAccountType DuesAndSubscriptions                  = new DetailAccountType(26,"DuesAndSubscriptions");
        public static DetailAccountType EmployeeCashAdvances                  = new DetailAccountType(27,"EmployeeCashAdvances");
        public static DetailAccountType Entertainment                         = new DetailAccountType(28,"Entertainment");
        public static DetailAccountType EntertainmentMeals                    = new DetailAccountType(29,"EntertainmentMeals");
        public static DetailAccountType EquipmentRental                       = new DetailAccountType(30,"EquipmentRental");
        public static DetailAccountType EquipmentRentalCOS                    = new DetailAccountType(31,"EquipmentRentalCOS");
        public static DetailAccountType FederalIncomeTaxPayable               = new DetailAccountType(32,"FederalIncomeTaxPayable");
        public static DetailAccountType FurnitureAndFixtures                  = new DetailAccountType(33,"FurnitureAndFixtures");
        public static DetailAccountType Goodwill                              = new DetailAccountType(34,"Goodwill");
        public static DetailAccountType Insurance                             = new DetailAccountType(35,"Insurance");
        public static DetailAccountType InsurancePayable                      = new DetailAccountType(36,"InsurancePayable");
        public static DetailAccountType IntangibleAssets                      = new DetailAccountType(37,"IntangibleAssets");
        public static DetailAccountType InterestEarned                        = new DetailAccountType(38,"InterestEarned");
        public static DetailAccountType InterestPaid                          = new DetailAccountType(39,"InterestPaid");
        public static DetailAccountType Inventory                             = new DetailAccountType(40,"Inventory");
        public static DetailAccountType InvestmentMortgageOrRealEstateLoans   = new DetailAccountType(41,"InvestmentMortgageOrRealEstateLoans");
        public static DetailAccountType InvestmentOther                       = new DetailAccountType(42,"InvestmentOther");
        public static DetailAccountType InvestmentTaxExemptSecurities         = new DetailAccountType(43,"InvestmentTaxExemptSecurities");
        public static DetailAccountType InvestmentUSGovObligations            = new DetailAccountType(44,"InvestmentUSGovObligations");
        public static DetailAccountType Land                                  = new DetailAccountType(45,"Land");
        public static DetailAccountType LeaseBuyout                           = new DetailAccountType(46,"LeaseBuyout");
        public static DetailAccountType LeaseholdImprovements                 = new DetailAccountType(47,"LeaseholdImprovements");
        public static DetailAccountType LegalAndProfessionalFees              = new DetailAccountType(48,"LegalAndProfessionalFees");
        public static DetailAccountType Licenses                              = new DetailAccountType(49,"Licenses");
        public static DetailAccountType LineOfCredit                          = new DetailAccountType(50,"LineOfCredit");
        public static DetailAccountType LoanPayable                           = new DetailAccountType(51,"LoanPayable");
        public static DetailAccountType LoansToOfficers                       = new DetailAccountType(52,"LoansToOfficers");
        public static DetailAccountType LoansToOthers                         = new DetailAccountType(53,"LoansToOthers");
        public static DetailAccountType LoansToStockholders                   = new DetailAccountType(54,"LoansToStockholders");
        public static DetailAccountType MachineryAndEquipment                 = new DetailAccountType(55,"MachineryAndEquipment");
        public static DetailAccountType MoneyMarket                           = new DetailAccountType(56,"MoneyMarket");
        public static DetailAccountType NonProfitIncome                       = new DetailAccountType(57,"NonProfitIncome");
        public static DetailAccountType NotesPayable                          = new DetailAccountType(58,"NotesPayable");
        public static DetailAccountType OfficeOrGeneralAdministrativeExpenses = new DetailAccountType(59,"OfficeOrGeneralAdministrativeExpenses"); 
        public static DetailAccountType OpeningBalanceEquity                  = new DetailAccountType(60,"OpeningBalanceEquity");
        public static DetailAccountType OrganizationalCosts                   = new DetailAccountType(61,"OrganizationalCosts");
        public static DetailAccountType OtherCostsOfServiceCOS                = new DetailAccountType(62,"OtherCostsOfServiceCOS");
        public static DetailAccountType OtherCurrentAssets                    = new DetailAccountType(63,"OtherCurrentAssets");
        public static DetailAccountType OtherCurrentLiab                      = new DetailAccountType(64,"OtherCurrentLiab");
        public static DetailAccountType OtherFixedAssets                      = new DetailAccountType(65,"OtherFixedAssets");
        public static DetailAccountType OtherInvestmentIncome                 = new DetailAccountType(66,"OtherInvestmentIncome");
        public static DetailAccountType OtherLongTermAssets                   = new DetailAccountType(67,"OtherLongTermAssets");
        public static DetailAccountType OtherLongTermLiab                     = new DetailAccountType(68,"OtherLongTermLiab");
        public static DetailAccountType OtherMiscExpense                      = new DetailAccountType(69,"OtherMiscExpense");
        public static DetailAccountType OtherMiscIncome                       = new DetailAccountType(70,"OtherMiscIncome");
        public static DetailAccountType OtherMiscServiceCost                  = new DetailAccountType(71,"OtherMiscServiceCost");
        public static DetailAccountType OtherPrimaryIncome                    = new DetailAccountType(72,"OtherPrimaryIncome");
        public static DetailAccountType OwnersEquity                          = new DetailAccountType(73,"OwnersEquity");
        public static DetailAccountType PaidInCapitalOrSurplus                = new DetailAccountType(74,"PaidInCapitalOrSurplus");
        public static DetailAccountType PartnerContributions                  = new DetailAccountType(75,"PartnerContributions");
        public static DetailAccountType PartnerDistributions                  = new DetailAccountType(76,"PartnerDistributions");
        public static DetailAccountType PartnersEquity                        = new DetailAccountType(77,"PartnersEquity");
        public static DetailAccountType PayrollClearing                       = new DetailAccountType(78,"PayrollClearing");
        public static DetailAccountType PayrollExpenses                       = new DetailAccountType(79,"PayrollExpenses");
        public static DetailAccountType PayrollTaxPayable                     = new DetailAccountType(80,"PayrollTaxPayable");
        public static DetailAccountType PenaltiesAndSettlements               = new DetailAccountType(81,"PenaltiesAndSettlements");
        public static DetailAccountType PreferredStock                        = new DetailAccountType(82,"PreferredStock");
        public static DetailAccountType PrepaidExpenses                       = new DetailAccountType(83,"PrepaidExpenses");
        public static DetailAccountType PrepaidExpensesPayable                = new DetailAccountType(84,"PrepaidExpensesPayable");
        public static DetailAccountType PromotionalMeals                      = new DetailAccountType(85,"PromotionalMeals");
        public static DetailAccountType RentOrLeaseOfBuildings                = new DetailAccountType(86,"RentOrLeaseOfBuildings");
        public static DetailAccountType RentsHeldInTrust                      = new DetailAccountType(87,"RentsHeldInTrust");
        public static DetailAccountType RentsInTrustLiab                      = new DetailAccountType(88,"RentsInTrustLiab");
        public static DetailAccountType RepairAndMaintenance                  = new DetailAccountType(89,"RepairAndMaintenance");
        public static DetailAccountType Retainage                             = new DetailAccountType(90,"Retainage");
        public static DetailAccountType RetainedEarnings                      = new DetailAccountType(91,"RetainedEarnings");
        public static DetailAccountType SalesOfProductIncome                  = new DetailAccountType(92,"SalesOfProductIncome");
        public static DetailAccountType SalesTaxPayable                       = new DetailAccountType(93,"SalesTaxPayable");
        public static DetailAccountType Savings                               = new DetailAccountType(94,"Savings");
        public static DetailAccountType SecurityDeposits                      = new DetailAccountType(95,"SecurityDeposits");
        public static DetailAccountType ServiceOrFeeIncome                    = new DetailAccountType(96,"ServiceOrFeeIncome");
        public static DetailAccountType ShareholderNotesPayable               = new DetailAccountType(97,"ShareholderNotesPayable");
        public static DetailAccountType ShippingFreightAndDelivery            = new DetailAccountType(98,"ShippingFreightAndDelivery");
        public static DetailAccountType ShippingFreightAndDeliveryCOS         = new DetailAccountType(99,"ShippingFreightAndDeliveryCOS");
        public static DetailAccountType StateOrLocalIncomeTaxPayable          = new DetailAccountType(100,"StateOrLocalIncomeTaxPayable");
        public static DetailAccountType SuppliesAndMaterials                  = new DetailAccountType(101,"SuppliesAndMaterials");
        public static DetailAccountType SuppliesAndMaterialsCOGS              = new DetailAccountType(102,"SuppliesAndMaterialsCOGS");
        public static DetailAccountType TaxExemptInterest                     = new DetailAccountType(103,"TaxExemptInterest");
        public static DetailAccountType TaxesPaid                             = new DetailAccountType(104,"TaxesPaid");
        public static DetailAccountType Travel                                = new DetailAccountType(105,"Travel");
        public static DetailAccountType TravelMeals                           = new DetailAccountType(106,"TravelMeals");
        public static DetailAccountType TreasuryStock                         = new DetailAccountType(107,"TreasuryStock");
        public static DetailAccountType TrustAccounts                         = new DetailAccountType(108,"TrustAccounts");
        public static DetailAccountType TrustAccountsLiab                     = new DetailAccountType(109,"TrustAccountsLiab");
        public static DetailAccountType UndepositedFunds                      = new DetailAccountType(110,"UndepositedFunds");
        public static DetailAccountType Utilities                             = new DetailAccountType(111,"Utilities");
        public static DetailAccountType Vehicles                              = new DetailAccountType(112,"Vehicles");
        
        public DetailAccountType() {}

        public static bool operator ==(DetailAccountType arg1, DetailAccountType arg2)
        {
            return arg1.Equals(arg2);
        }
        
        public static bool operator !=(DetailAccountType arg1, DetailAccountType arg2)
        {
            return !arg1.Equals(arg2);
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is DetailAccountType)
                return (obj as DetailAccountType).DetailAccountTypeId == DetailAccountTypeId;

            return false;
        }

        public override int GetHashCode()
        {
            return DetailAccountTypeId.GetHashCode();
        }

        #region FindBy Description

        private const string SqlFindDescription =
            @"SELECT DetailAccountTypeId, DetailAccountTypeDescription 
            FROM DetailAccountType
                WHERE DetailAccountTypeDescription = @DetailAccountTypeDescription";

        public static DetailAccountType FindBy(string description)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindDescription))
            {
                Database.PutParameter(dbCommand, "@DetailAccountTypeDescription", description);
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);

                    throw new DataNotFoundException("DetailAccountType " + description + " not found");
                }
            }
        }

        #endregion                
        
    }
}
      