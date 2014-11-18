package UI.Management.ServiceBrowser
{
	import mx.collections.ArrayCollection;
	[RemoteClass(alias="Weborb.Management.ServiceBrowser.ServiceNode")]
	public class ServiceNode
	{
	    [Bindable]
		public var Items:Array = new Array();
		[Bindable]
		public var Name:String = "";
		
		public var Parent:ServiceNode;
		
		public function IsService():Boolean
		{
			return this is Service;
		}
		
		public function IsMethod():Boolean
		{
			return this is ServiceMethod;
		}
		
		public function IsMethodArg():Boolean
		{
			return this is ServiceMethodArg;
		}
		
		public function IsNamespace():Boolean
		{
			return this is ServiceNamespace;
		}
		
		public function IsDataType():Boolean
		{
			return this is ServiceDataType;
		}
		public function IsDataTypeField():Boolean
		{
			return this is ServiceDataTypeField;
		}
		
		public function getFullName():String
		{
			var serviceNode:ServiceNode = this.Parent;
			var fullName:String = Name;
			
			while(serviceNode != null)
			{
				fullName = serviceNode.Name + "." + fullName;
				
				serviceNode = serviceNode.Parent;
			}
			
			return fullName;
		}
		
		public function IsDataTypeContainer():Boolean
		{
			return this is ServiceDataTypeContainer;
		}
		
	}
}