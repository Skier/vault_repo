package UI.Management
{
	import flash.events.Event;
	import mx.controls.Tree;
	import mx.rpc.events.ResultEvent;
	import mx.managers.CursorManager;
	import mx.rpc.events.FaultEvent;
	import mx.controls.Alert;
	import flash.external.ExternalInterface;
	import mx.rpc.xml.DataType;
	import mx.rpc.remoting.RemoteObject;
	import mx.rpc.Fault;
	import mx.rpc.AbstractOperation;
	import flash.utils.describeType;
	import mx.utils.ObjectUtil;
	import mx.utils.ObjectProxy;
	import mx.messaging.management.ObjectInstance;
	import flash.utils.getTimer;
	import mx.skins.halo.BrokenImageBorderSkin;
	import flash.system.ApplicationDomain;
	import flash.system.LoaderContext;
	import UI.Management.CodeGen.CodeItem;
	import UI.Management.CodeGen.CodeFile;
	import UI.Management.CodeGen.CodeDirectory;
	import UI.Management.CodeGen.CodegeneratorResult;
	import UI.Management.TestDrive.TestDriveController;
	import flash.net.FileReference;
	import flash.net.URLRequest;
	import UI.Management.CodeGen.CodegeneratorController;
	import flash.utils.Timer;
	import flash.events.TimerEvent;
	import mx.containers.TabNavigator;
	import UI.Management.CodeGen.CodegeneratorView;
	import UI.Management.TestDrive.TestDriveView;
	import UI.Management.Security.SecurityView;
	import UI.Management.Monitoring.MonitoringView;
	import UI.Management.Exposure.ExposureView;
	import UI.Management.ServiceBrowser.*;
	
	
	public class ManagementController
	{
		[Bindable]
		public var View:ManagementView;
		
		[Bindable]
		public var Model:ManagementModel = new ManagementModel();
		
		[Bindable]
		public var IsExposureEnabled:Boolean = false;
		
		[Bindable]
		public var IsCodegeneratorEnabled:Boolean = false;
	
		[Bindable]
		public var IsTestDriveEnabled:Boolean = false;
		
		[Bindable]
		public var IsMonitoringEnabled:Boolean = false;
		
		[Bindable]
		public var IsSecurityEnabled:Boolean = false;
		
		private var m_listeners:Array;

		public function ManagementController(view:ManagementView)
		{
			View = view;
			m_listeners = new Array();
		}
		
		public function AddListener(listener:IManagementControllerListener):void
		{
			m_listeners.push(listener);
		}
		
		public function OnCurrentNodeChanged(event:ServiceBrowserEvent):void	
		{
			setCurrentNode(event.SelectedNode);
		}
		
		public static function expandTreeNode(tree:Tree):void
		{
			if(!tree.isItemOpen(tree.selectedItem))
				tree.expandItem(tree.selectedItem, true, true);	
		}
		
		private function setCurrentNode(serviceNode:ServiceNode):void	
		{
			if(serviceNode != null)
			{
				IsCodegeneratorEnabled = serviceNode.IsService();
				IsTestDriveEnabled = serviceNode.IsMethod();
				IsSecurityEnabled = true;
				IsExposureEnabled = serviceNode.IsService();
				IsMonitoringEnabled = serviceNode.IsService();
				
				if(serviceNode.IsMethod())
				{
					if(Model.WorkflowState != Model.WORKFLOWSTATE_SECURITY)
					{
						View.m_tabs.selectedChild = View.m_testDrive;
						View.m_tabs.validateNow();
					}
				}
				else if(serviceNode.IsService())
				{
					if(Model.WorkflowState == Model.WORKFLOWSTATE_TESTDRIVE)
					{
						View.m_tabs.selectedChild = View.m_codegen;
						View.m_tabs.validateNow();
					}
				}
			}
			else
				IsCodegeneratorEnabled = IsTestDriveEnabled = IsSecurityEnabled = IsExposureEnabled = IsMonitoringEnabled = false;
			
			Model.CurrentNode = serviceNode;
			
			for each(var listener:IManagementControllerListener in m_listeners)
				listener.OnCurrentNodeChanged(Model.CurrentNode);
		}
	
        public function OnServicesRecieved(event:ResultEvent):void
        {
        	CursorManager.removeBusyCursor();
        	
        	Model.Services = event.result as Array;
        }
       
        public function OnRefresh():void
        {
            setCurrentNode(null);
            
            View.service.getServices();
        }
        
        public function OnCreated():void	
        {
        	OnRefresh();

        	View.m_tabs.selectedChild = View.m_codegen;
        }
        
        public function OnTabChanged(event:Event):void
        {
        	var tabNavigator:TabNavigator = TabNavigator(event.target);
        	var currentView:Object = tabNavigator.selectedChild;
        	
        	if(currentView is CodegeneratorView)
        		Model.WorkflowState = Model.WORKFLOWSTATE_CODEGEN;
        	else if(currentView is TestDriveView)
        		Model.WorkflowState = Model.WORKFLOWSTATE_TESTDRIVE;
        	else if(currentView is SecurityView)
        		Model.WorkflowState = Model.WORKFLOWSTATE_SECURITY;
        	else if(currentView is MonitoringView)
        		Model.WorkflowState = Model.WORKFLOWSTATE_MONITORING;        		
        	else if(currentView is ExposureView)
        		Model.WorkflowState = Model.WORKFLOWSTATE_EXPOSURE;
        		
        }
        public function OnFault(event:FaultEvent):void
        {
           CursorManager.removeBusyCursor();
            	
           Alert.show(event.fault.message);
        }
        
	}
}