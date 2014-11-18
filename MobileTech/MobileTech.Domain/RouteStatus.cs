using System;

namespace MobileTech.Domain
{

    public enum RouteStatusEnum
    {
        IDL = 1,
        IDL_TCOM_DONE = 2,
        SOP_DONE = 3,
        LOAD_DONE = 4,
        UNLOAD_DONE = 5,
        EOP_TCOM_DONE = 6,
        EOP_DONE = 7
    }

    public partial class RouteStatus
    {

        public RouteStatus()
        {

        }
    }
}
