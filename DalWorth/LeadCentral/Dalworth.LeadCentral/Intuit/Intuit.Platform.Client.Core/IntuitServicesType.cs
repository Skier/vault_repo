using System;
using System.Collections.Generic;
using System.Text;

namespace Intuit.Platform.Client.Core
{
    /// <summary>
    /// This enum specifies which Intuit service to connect to Either QBO or QBD
    /// </summary>
    public enum IntuitServicesType
    {
        /// <summary>
        /// QuickBooks Desktop Data through IDS
        /// </summary>
        QBD,
        /// <summary>
        /// QuickBooks Online Data through IDS
        /// </summary>
        QBO
    }
}
