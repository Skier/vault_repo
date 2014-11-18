
package com.dalworth.leadCentral.domain
{
    import com.dalworth.leadCentral.domain.codegen.*;

    [Bindable]
    [RemoteClass(alias="Dalworth.LeadCentral.Domain.Transaction")]
    public class Transaction extends _Transaction
    {
        public function Transaction()
        {
        }

        public function get timeStamp():Number
        {
        	if (TransactionDate != null)
        		return TransactionDate.getTime();
        	else 
        		return 0;
        }
    }
}
      