
package com.dalworth.servman.domain
{
    import com.dalworth.servman.domain.codegen.*;

    [Bindable]
    [RemoteClass(alias="Servman.Domain.CallWorkflow")]
    public class CallWorkflow extends _CallWorkflow
    {
    	public var RelatedDetails:Array;
    	
        public function CallWorkflow()
        {
        }
    }
}
      