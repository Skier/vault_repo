package UI.Management
{
	import UI.Management.ServiceBrowser.ServiceNode;
	
	public interface IManagementControllerListener
	{
		function OnCurrentNodeChanged(serviceNode:ServiceNode):void;
	}
}