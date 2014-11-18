using System.ComponentModel.DataAnnotations;

namespace Dalworth.LeadCentral.Web.Models.Validators
{
    public class RedirectPhoneAttribute :RegularExpressionAttribute
    {
        public RedirectPhoneAttribute() : base("^\\d{10}$")
        {
        }
    }
}