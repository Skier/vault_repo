package UI.Management.TestDrive
{
	import UI.Management.ManagementModel;
	import UI.Management.ServiceBrowser.ServiceMethodArg;
	import UI.Management.CodeGen.CodeFormat;
	
	public class TestDriveModel
	{
		[Bindable]
		public var Parent:ManagementModel;
		
		public var LastCallArg:Array;
		
		public function TestDriveModel(managementModel: ManagementModel)
		{
			Parent = managementModel;
		} 
		
		[Bindable]
		public var InvocationInfo:String;
		
		[Bindable]
		public var CurrentMethodArgs:Array;
		
		[Bindable]		
		public var InvocationDuration:int;
		
		public function ClearResults():void	
		{
			Result = null;
			CurrentResultArray = new Array();
		}
		
		[Bindable]	
		public var Result:ObjectBrowser;
		[Bindable]
		public var CurrentResultArray:Array;
		
		//-------------------
		[Bindable]	
		public var TreeGridAdapter:TreeGridDataProviderAdapter;
		
		public var CurrentCodeFormat:CodeFormat;
	}
}