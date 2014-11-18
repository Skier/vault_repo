
package com.dalworth.servman.domain
{
    import com.dalworth.servman.domain.codegen.*;

    [Bindable]
    [RemoteClass(alias="Servman.Domain.LeadChangeHistory")]
    public class LeadChangeHistory extends _LeadChangeHistory
    {
        public function LeadChangeHistory()
        {
        }
        
        public function get changed():Number 
        {
        	return DateChanged.time;
        }
    }
}
      