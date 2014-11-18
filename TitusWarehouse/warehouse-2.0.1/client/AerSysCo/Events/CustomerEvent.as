package AerSysCo.Events
{
	import flash.events.Event;
	import AerSysCo.UI.Models.CustomerUI;

	public class CustomerEvent extends Event
	{
	    public static const CURRENT_CUSTOMER_CHANGE_REQUEST:String = "currentCustomerChangeRequest";
	
		private var customer:CustomerUI;
	
	    public function CustomerEvent(type:String, customer:CustomerUI, 
	        bubbles:Boolean=false, cancelable:Boolean=false)
	    {
	        super(type, bubbles, cancelable);
	
	        this.customer = customer;
	    }
	    
	    override public function clone():Event
	    {
	        return new CustomerEvent(type, customer, bubbles, cancelable);
	    }
	    
	}
}