using System;
 
        namespace Dalworth.Server.Domain
        {
            public enum QbItemTypeEnum
            {
                Service = 1, 
                Tax = 2,
                Discount = 3,
                OtherCharge = 4
            }

            public partial class QbItemType
            {
                public QbItemType()
                {

                }
            }
        }
      