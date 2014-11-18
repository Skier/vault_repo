package UI.Management.CodeGen
{
	import UI.Management.ManagementController;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.RemoteObject;
	import flash.net.FileReference;
	import flash.net.URLRequest;
	import mx.controls.Tree;
	import UI.Management.ServiceBrowser.*;
	import flash.net.navigateToURL;
	import UI.Management.IManagementControllerListener;
	import mx.managers.PopUpManager;
	import flash.display.DisplayObject;
	import UI.AppSettings;

		
	public class CodegeneratorController implements IManagementControllerListener
	{
		[Bindable]
		public var Parent:ManagementController;
		[Bindable]
		public var Model:CodegeneratorModel;
		[Bindable]
		public var View:CodegeneratorView;
				
		private var m_remoteObject:RemoteObject;
				
		public function CodegeneratorController(parentController:ManagementController, view:CodegeneratorView)
		{
			Model = new CodegeneratorModel(parentController.Model);
			View = view;
			Parent = parentController;
			Parent.AddListener(this);
			m_remoteObject = new RemoteObject("WeborbManagement");
			m_remoteObject.addEventListener("fault", Parent.OnFault);
			m_remoteObject.generateCode.addEventListener("result", OnClientCodeRecieved);
		}
		
		public function OnClientCodeRecieved(event:ResultEvent):void	
		{
			var codegenResult:CodegeneratorResult = CodegeneratorResult(event.result);
			
			Model.LastResult = codegenResult;
			
			if(codegenResult.Result.IsFile())
			{
				Model.CurrentCodeItem = codegenResult.Result as CodeItem;
				View.m_tree.selectedItem = Model.CurrentCodeItem;
			}
			else
			{
				View.m_tree.validateNow();
				
				ExpandNode(Model.RootCodeItem as CodeDirectory);
				
				Model.CurrentCodeItem = CodeDirectory.findFirstFile(codegenResult.Result as CodeDirectory);
				View.m_tree.selectedItem = Model.CurrentCodeItem;
			}	
			
			if(AppSettings.Instance.ShowCodegenInstructions)
			{
				var codegeneratorInstructionsView:CodegeneratorInstructionsView = CodegeneratorInstructionsView( 
					PopUpManager.createPopUp(DisplayObject(View.parentApplication) ,CodegeneratorInstructionsView,true) );
				codegeneratorInstructionsView.Model = Model;
			}
			

		
			View.m_code.verticalScrollPosition = 0;
		}
		
		public function ExpandNode(codeDirectory:CodeDirectory):void
		{	
			View.m_tree.expandItem(codeDirectory,true,true);
			
			for each(var codeItem:CodeItem in codeDirectory.Items)
				if(codeItem.IsDirectory())
					ExpandNode(codeItem as CodeDirectory);
		}
		
        public function OnCodeTypeChanged(codeType:int):void
        {
        	Model.CurrentCodeType = codeType;
			GenerateCode();
        }
        
        public function OnDownloadCodeClick():void	
        {
        	//var fileReference:FileReference = new FileReference();
        	var url:String = "codegen.aspx?service=" + Model.Parent.CurrentNode.getFullName() + "&type=" + Model.CurrentCodeType.toString();
 			navigateToURL(new URLRequest( url ),"_parent");
        	//fileReference.download(new URLRequest( url ), "weborb.code.zip");
        }
        
        public function GenerateCode():void
        {
        	m_remoteObject.generateCode(Model.Parent.CurrentNode.getFullName(), null,  Model.CurrentCodeType, false);
        }
        
		public function OnCodeNodeChanged(event:Event):void	
		{
			Model.CurrentCodeItem = CodeItem(Tree(event.target).selectedItem);
		}
		
		public function OnCurrentNodeChanged(serviceNode:ServiceNode):void
		{
			Reset();
			
			if(serviceNode != null && serviceNode.IsService())
				GenerateCode();
		}
		
		private function Reset():void
		{
			Model.CurrentCodeItem = null;
			Model.LastResult = null;

		}
	}
}