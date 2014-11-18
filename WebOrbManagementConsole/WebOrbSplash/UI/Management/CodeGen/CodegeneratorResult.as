package UI.Management.CodeGen
{
	[Bindable]
	[RemoteClass(alias="Weborb.Management.CodeGen.CodegeneratorResult")]
	public class CodegeneratorResult
	{
		public var Result:CodeItem;
		
		public var DownloadUri:String;
		
		public var SavedOnServer:Boolean;
		
		public var Info:String;
	}
}