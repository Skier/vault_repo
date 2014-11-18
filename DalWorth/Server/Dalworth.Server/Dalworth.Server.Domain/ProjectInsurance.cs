namespace Dalworth.Server.Domain
{
    public partial class ProjectInsurance
    {
        public ProjectInsurance()
        {
        }

        public bool IsFilled()
        {
            if (ProjectId > 0 ||
                !string.IsNullOrEmpty(Phone) || 
                !string.IsNullOrEmpty(Address1) ||
                !string.IsNullOrEmpty(Address2) ||
                !string.IsNullOrEmpty(ClaimNumber) ||
                !string.IsNullOrEmpty(Company) ||
                !string.IsNullOrEmpty(Contact) ||
                DeductibleAmount > 0)
                return true;

            return false;
        }
    }
}
      