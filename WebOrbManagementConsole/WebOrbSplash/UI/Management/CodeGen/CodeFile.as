package UI.Management.CodeGen
{
	[RemoteClass(alias="Weborb.Management.CodeGen.CodeFile")]
	public class CodeFile extends CodeItem
	{
		[Bindable]
		public var Content:String = "";
		
		public static var Empty:CodeFile = new CodeFile();
	}
}