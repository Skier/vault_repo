package UI.Management
{
	import mx.controls.treeClasses.ITreeDataDescriptor;
	import mx.collections.ICollectionView;
	import mx.collections.ArrayCollection;
	import UI.Management.CodeGen.CodeFile;
	import UI.Management.CodeGen.CodeItem;
	import UI.Management.CodeGen.CodeFormat;
	import UI.Management.CodeGen.CodegeneratorResult;
	import UI.Management.ServiceBrowser.*;
	
	public class ManagementModel
	{		
		public function ManagementModel()
		{
			CodeFormats = new Array();
			CodeFormats.push(new CodeFormat("Flex Remoting/AS3",0));
			CodeFormats.push(new CodeFormat("Flash Remoting/AS2",1));
			CodeFormats.push(new CodeFormat("Flash Remoting/AS2 inline",2));
			CodeFormats.push(new CodeFormat("ARP Framework",3));
			CodeFormats.push(new CodeFormat("Cairngorm Framework",4));
			CodeFormats.push(new CodeFormat("FlashComm/FMS2",5));
			CodeFormats.push(new CodeFormat("AJAX Client",6));			
		}
		
		[Bindable]
		public var CodeFormats:Array;
		
		[Bindable]
        public var Services:Array;
		
		[Bindable]
		public var CurrentNode:ServiceNode;	

		public var WorkflowState:int;
		
		public const WORKFLOWSTATE_EXPOSURE:int = 0;
		public const WORKFLOWSTATE_CODEGEN:int = 1;
		public const WORKFLOWSTATE_TESTDRIVE:int = 2;
		public const WORKFLOWSTATE_SECURITY:int = 3;
		public const WORKFLOWSTATE_MONITORING:int = 4;
	}	
}