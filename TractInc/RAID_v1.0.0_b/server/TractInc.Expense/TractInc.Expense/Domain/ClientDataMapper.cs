using System.Collections.Generic;

namespace TractInc.Expense.Domain
{
    public class ClientDataMapper :_ClientDataMapper
    {
        public ClientDataMapper(){}
        public ClientDataMapper(TractIncRAIDDb database):base(database){}
    }
}
      