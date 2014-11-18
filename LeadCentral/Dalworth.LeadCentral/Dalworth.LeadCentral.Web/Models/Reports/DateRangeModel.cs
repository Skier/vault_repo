using System;
using System.ComponentModel.DataAnnotations;

namespace Dalworth.LeadCentral.Web.Models.Reports
{
    public class DateRangeModel
    {
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [RegularExpression(@"^(([0-2]\d|[3][0-1])\.([0]\d|[1][0-2])\.[2][0]\d{2})$",
            ErrorMessage = @"Incorrect Start Date")]
        public DateTime? DateFrom { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [RegularExpression(@"^(([0-2]\d|[3][0-1])\.([0]\d|[1][0-2])\.[2][0]\d{2})$",
            ErrorMessage = @"Incorrect End Date")]
        public DateTime? DateTo { get; set; }
    }
}