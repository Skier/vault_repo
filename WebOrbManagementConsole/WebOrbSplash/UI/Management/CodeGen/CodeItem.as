package UI.Management.CodeGen
{
	[RemoteClass(alias="Weborb.Management.CodeGen.CodeItem")]
	public class CodeItem
	{
		public var Name:String = "";
		
		public var Directory:CodeDirectory;
		
		public function IsDirectory():Boolean
		{
			return this is CodeDirectory;
		}
		
		public function IsFile():Boolean
		{
			return this is CodeFile;
		}
		
	}
}