package UI.Management.ServiceBrowser
{
	[RemoteClass(alias="Weborb.Management.ServiceBrowser.ServiceDataType")]
	public class ServiceDataType extends ServiceNode
	{
		public function IsComplexType():Boolean
		{
			return Items.length > 0;
		}
		
		public function IsDate():Boolean
		{
			return Name == "Date";
		}
		
		public function IsArray():Boolean
		{
			return Name == "Array";
		}
		
		public function IsBoolean():Boolean
		{
			return Name == "Boolean";
		}
		
		public function IsString():Boolean
		{
			return Name == "String";
		}
		
		public function IsNumeric():Boolean
		{
			return Name == "int" || Name == "Number";
		}
		
		public var ElementType:ServiceDataType;
		
		public var IsHashTable:Boolean;
	}
}