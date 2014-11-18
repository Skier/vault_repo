using System;
using System.Collections.Generic;
using System.Text;

namespace MobileTech.Domain
{
    public enum RouteOptionEnum
    {
        EnableDualInventory,
        AutoCalcLoadIn,
        Odometer
    }

    public partial class RouteOption
    {
        const int DEFAULT_VALUE = 0;

        public RouteOption() { }


        public static bool ValueIs(RouteOptionEnum option, int checkValue)
        {
            return GetValue(option,false) == checkValue;
        }

        public static int GetValue(RouteOptionEnum option, bool errorIfNotFound)
        {
            try
            {
                RouteOption routeOption = FindByPrimaryKey(Route.Current.LocationId,
                    Route.Current.RouteNumber, option.ToString());

                return routeOption.OptionValue;
            }
            catch(Exception ex)
            {
                if (errorIfNotFound)
                    throw ex;
            }

            return DEFAULT_VALUE;
        }
    }
}
