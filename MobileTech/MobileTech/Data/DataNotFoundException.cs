using System;
using System.Collections.Generic;
using System.Text;

namespace MobileTech.Data
{
    public class DataNotFoundException: MobileTechException
    {
        public DataNotFoundException(string message):base(message)
        {
        }
    }
}
