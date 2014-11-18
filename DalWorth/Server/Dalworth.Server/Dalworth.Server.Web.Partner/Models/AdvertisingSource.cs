using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web.Mvc;
using Dalworth.Server.Data;

namespace Dalworth.Server.Web.Partner.Models
{
    public class AdvertisingSource
    {
        #region AdvertisingSource

        public AdvertisingSource()
        {
        }

        public AdvertisingSource(int partnerId)
        {
            PartnerId = partnerId;            
        }

        #endregion

        #region Properties

        public int PartnerId { get; set; }

        [Required(ErrorMessage = "Please enter Advertising Source")]
        public string AdvertisingSourceCode { get; set; }
        
        public string TrackingUrl { get; set; }

        #endregion
    }
}
