using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servman.Intuit.Weborb
{
    public class Test
    {
        public string Ping(string tst)
        {
            if (tst == "ping")
                return "pong";
            else
                return "unknown string";
        }

        public string Ping()
        {
            return "unknown string";
        }
    }
}