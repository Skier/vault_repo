package com.llsvc.domain.events
{
import com.llsvc.domain.LeaseClause;
import flash.events.Event;

public class LeaseClauseEvent extends Event
{
	public static const CLAUSE_CHANGE:String = "leaseClauseChange";
	public static const CLAUSE_DELETE:String = "leaseClauseDelete";
	
	public var leaseClause:LeaseClause;
	
	public function LeaseClauseEvent(type:String, leaseClause:LeaseClause, bubbles:Boolean=false, cancelable:Boolean=false)
	{
		super(type, bubbles, cancelable);
		
		this.leaseClause = leaseClause;
	}
	
}
}