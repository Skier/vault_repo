
package com.dalworth.servman.domain
{
    import com.dalworth.servman.domain.codegen.*;

    [Bindable]
    [RemoteClass(alias="Servman.Domain.Owner")]
    public class Owner extends _Owner
    {
    	public var RelatedUser:User;
    	
        public function Owner()
        {
        }
    }
}
      