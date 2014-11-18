package UI.Management.CodeGen
{
	[Bindable]
	public class CodeFormat
	{
		public var Name:String;
		public var Type:int;
		
		public function CodeFormat(name:String, type:int)
		{
			Name = name;
			Type = type;
		}
	}
}