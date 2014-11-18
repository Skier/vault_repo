
package com.dalworth.servman.domain
{
    import com.dalworth.servman.domain.codegen.*;

    [Bindable]
    [RemoteClass(alias="Servman.Domain.CustomerServiceRep")]
    public class CustomerServiceRep extends _CustomerServiceRep
    {
    	public var RelatedUser:User;
    	
        public function CustomerServiceRep()
        {
        }
    }
}
      