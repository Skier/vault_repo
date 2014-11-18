using System.ComponentModel.DataAnnotations;

namespace Dalworth.LeadCentral.Web.Models.Validators
{
    public class PhoneAttribute :RegularExpressionAttribute
    {
        public PhoneAttribute() : base("^\\(?(?<AreaCode>[2-9]\\d{2})(\\)?)(-|.|\\s)?(?<Prefix>[1-9]\\d{2})(-|.|\\s)?(?<Suffix>\\d{4})$")
        {
        }
    }
}
