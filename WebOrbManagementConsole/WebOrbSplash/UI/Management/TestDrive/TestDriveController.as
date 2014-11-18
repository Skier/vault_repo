package UI.Management.TestDrive
{
	import UI.Management.*;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.RemoteObject;
	import mx.rpc.AbstractOperation;
	import flash.events.Event;
	import flash.utils.getTimer;
	import mx.controls.Alert;
	import mx.utils.ObjectUtil;
	import mx.controls.Tree;
	import UI.Management.CodeGen.CodegeneratorResult;
	import flash.external.ExternalInterface;
	import mx.rpc.xml.DataType;
	import flash.net.FileReference;
	import flash.net.URLRequest;
	import flash.net.URLVariables;
	import flash.net.URLRequestMethod;
	import flash.net.navigateToURL;
	import UI.Management.ServiceBrowser.*;
	import mx.utils.ObjectProxy;
	import flash.net.getClassByAlias;
	import mx.core.ClassFactory;
	import UI.AppSettings;

		
	public class TestDriveController implements IManagementControllerListener
	{
		[Bindable]
		public var Parent:ManagementController;
		[Bindable]
		public var Model:TestDriveModel;
		
		private var m_invocationStart:int;
		
		private var m_remoteObject:RemoteObject;
		
		private var View:TestDriveView;
		
		public function TestDriveController(parentController:ManagementController, view:TestDriveView)
		{
			Model = new TestDriveModel(parentController.Model);
			Model.CurrentCodeFormat = Model.Parent.CodeFormats[0];
			View = view;
			Parent = parentController;
			Parent.AddListener(this);
			m_remoteObject = new RemoteObject("WeborbManagement");
			m_remoteObject.addEventListener("fault", Parent.OnFault);
			m_remoteObject.generateCode.addEventListener("result", OnGenerateCodeReturn);
		}
		
		public function OnGenerateCodeReturn(event:ResultEvent):void	
		{
			var codegenResult:CodegeneratorResult = CodegeneratorResult(event.result);
			
			var url:String = "codegen.aspx?service=" 
			+ Model.Parent.CurrentNode.getFullName() + "&uri=" + codegenResult.DownloadUri;

			navigateToURL(new URLRequest( url ), "_parent");		
		}
		
        public function IsArgEditable(node:ServiceNode):Boolean
        {
        	if(node == null)
        		return false;
        		
        	if(node.IsMethodArg() && ServiceMethodArg(node).DataType.IsComplexType())
        		return false;
        
        	return node.IsDataTypeField() || node.IsMethodArg();
        }
        
        public function Invoke():void
        {
        	var serviceMethod:ServiceMethod = ServiceMethod(Model.Parent.CurrentNode);
        	var remoteObject:RemoteObject = new RemoteObject("GenericDestination");
        	remoteObject.source = serviceMethod.Parent.getFullName();
        	remoteObject.addEventListener("result",OnInvokeResult);
        	remoteObject.addEventListener("fault",OnInvokeFault);  
        	
        	var remoteMethod:AbstractOperation = remoteObject.getOperation(serviceMethod.Name);
        	
        	var args:Array = getArrayValue(Model.TreeGridAdapter.Root);
        	
        	if(args.length > 0)
        		remoteMethod.arguments = args;
        		
 			Model.InvocationDuration = 0;
 			Model.ClearResults();
 			m_invocationStart = getTimer();
        	remoteMethod.send();
        }
        
		
		private function getComplexValue(node:TreeGridNode):Object
		{
			
			var dataType:ServiceDataType = ServiceDataTypeContainer(node.Value).DataType;
			
			var fields:Object;
			
			if(!dataType.IsHashTable)
			{
				flash.net.registerClassAlias( dataType.getFullName() , 	Object );
	        		
				var remoteClass:Class = flash.net.getClassByAlias(dataType.getFullName());
				
				fields = new ClassFactory(remoteClass).newInstance();
			}
			else
				fields = new Object();
			
			for each(var childNode:TreeGridNode in node.Items)
			{
				var serviceDataTypeContainer:ServiceDataTypeContainer = ServiceDataTypeContainer(childNode.Value);
							
				if(serviceDataTypeContainer.DataType.IsComplexType() 
					|| serviceDataTypeContainer.DataType.IsHashTable)
					fields[serviceDataTypeContainer.Name] = getComplexValue(childNode);
				else if(serviceDataTypeContainer.DataType.IsArray())
					fields[serviceDataTypeContainer.Name] = getArrayValue(childNode);
				else
					fields[serviceDataTypeContainer.Name] = childNode.Data;
			}
			
   			return fields;
		}
		
		private function getArrayValue(node:TreeGridNode):Array
		{
			var fields:Array = new Array();
			
			for each(var childNode:TreeGridNode in node.Items)
			{
				var serviceDataTypeContainer:ServiceDataTypeContainer = ServiceDataTypeContainer(childNode.Value);
							
				if(serviceDataTypeContainer.DataType.IsComplexType() 
					|| serviceDataTypeContainer.DataType.IsHashTable)
					fields.push( getComplexValue(childNode) );
				else if(serviceDataTypeContainer.DataType.IsArray())
					fields.push( getArrayValue(childNode) );
				else
					fields.push( childNode.Data );
			}
			
   			return fields;
		}
		
		public function OnInvokeFault (event:FaultEvent):void 
        {
            Alert.show(event.fault.faultString, 'Error');
        }
        
		public function OnInvokeResult (event:ResultEvent):void 
        {
        	Model.InvocationDuration = getTimer() - m_invocationStart;
        	
        	//flash.net.registerClassAlias( ServiceMethod(Model.Parent.CurrentNode).ReturnDataType.getFullName() , 
        	//	Object )
        	
        	
        	Model.Result = new ObjectBrowser("Result", event.result );
        	
			View.m_result.validateNow();
			
			//ExpandResult(Model.LastResult);		
			View.m_result.expandItem(Model.Result,true,false);
        }
        
        
	    public function OnResultNodeChanged(event:Event):void	
	    {
	    	var objectBrowser:ObjectBrowser = ObjectBrowser(Tree(event.target).selectedItem);
	    	
	    	if(objectBrowser.IsArray())
	    	{
	    		try
	    		{
	    			Model.CurrentResultArray = objectBrowser.Items;
	    			
	    			return;
	    		}
	    		catch(o:Object) {}
	    	}
	    	
	    	Model.CurrentResultArray = new Array();
	    		
	    }
	    
	    
	    public function OnDownloadClick():void
	    {
	    	if(Model.CurrentCodeFormat.Type > 2 && Model.CurrentCodeFormat.Type < 6)
	    	{
	    		Alert.show("This format yet not supported");
	    		return;
	    	}
	    	
	    	var serviceMethod:ServiceMethod = ServiceMethod(Model.Parent.CurrentNode);

			m_remoteObject.generateCode(serviceMethod.Parent.getFullName() + "#" + serviceMethod.Name , 
				getArrayValue(Model.TreeGridAdapter.Root),  
				Model.CurrentCodeFormat.Type, 
				true);	    	
	    }
	    
	    public function OnCurrentNodeChanged(currentNode:ServiceNode):void
	    {
	    	Reset();
	    	
	    	if(currentNode == null || !currentNode.IsMethod())
	    		return;
	    	
	    	var serviceMethod:ServiceMethod = ServiceMethod(currentNode);
	    	var testDriveArgTreeDataDescriptor:TestDriveArgTreeDataDescriptor = new TestDriveArgTreeDataDescriptor();
	    	Model.InvocationInfo = serviceMethod.getInfo();
	    	Model.TreeGridAdapter = new TreeGridDataProviderAdapter(serviceMethod.Items, 
	    										testDriveArgTreeDataDescriptor);
	    										
	    	if(AppSettings.Instance.AutoexpandTestDriveArgs)
	    	{
	    		View.m_grdArgs.validateNow();
	    	
		    	var branchesForExpand:Array = new Array();
		    	
		   		for each(var treeGridNode:TreeGridNode in Model.TreeGridAdapter)
		   			branchesForExpand.push(treeGridNode);
		   			
		   		for each(var node:TreeGridNode in branchesForExpand)
		   			if(testDriveArgTreeDataDescriptor.isBranch(node.Value))
		   				Model.TreeGridAdapter.loadBranch(node);
	   		}
	    }
	    
		
		public function ExpandResult(objectBrowser:ObjectBrowser):void
		{	
			View.m_result.expandItem(objectBrowser,true,true);
			
			for each(var childNode:ObjectBrowser in objectBrowser.Items)
				if(childNode.Items.length > 0)
					ExpandResult(childNode);
		}
		
	    private function Reset():void
	    {
	    	Model.InvocationInfo = "";
	    	Model.CurrentMethodArgs = new Array();
	    	Model.InvocationDuration = 0;
			
			Model.ClearResults();
	    }
	}
}