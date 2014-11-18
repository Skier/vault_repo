using System;
using System.Collections.Generic;
using System.Text;

namespace AerSysCo.CustomerCenter
{
public class SalesRepNotFoundException 
    : Exception
{
    private String userName;
    public SalesRepNotFoundException(String userName) {
        this.userName = userName;
    }

    public String toString() {
        return "SalesRep with username " + userName + " is not found.";
    }
}
}
