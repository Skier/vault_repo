package UI.Management.ServiceBrowser
{
	import flash.events.Event;

	public class ServiceBrowserEvent extends Event
	{
		public var SelectedNode:ServiceNode;
		
		public function ServiceBrowserEvent(serviceNode:ServiceNode)
		{
			super("nodeChanged");
			
			SelectedNode = serviceNode;
		}
		
		
	}
}