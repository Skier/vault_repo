package com.dalworth.servman.domain
{
    import Intuit.Sb.Cdm.vo.Customer;
    
    import com.dalworth.servman.domain.codegen.*;

    [Bindable]
    [RemoteClass(alias="Servman.Domain.Job")]
    public class Job extends _Job
    {
    	public var RelatedIdsJob:Intuit.Sb.Cdm.vo.Job;
    	public var RelatedIdsCustomer:Customer;
    	
    	public var MatchLevel:int;
    	public var IsMatched:Boolean;
    	
        public function Job()
        {
        }
    }
}
      