package UI.Management.CodeGen
{
	[RemoteClass(alias="Weborb.Management.CodeGen.CodeDirectory")]
	public class CodeDirectory extends CodeItem
	{
		public var Items:Array = new Array();
		
		public static function findFirstFile(directory:CodeDirectory):CodeFile
		{
			var arr:Array = new Array();
			
			for each(var codeItem:CodeItem in directory.Items)
			{
				if(codeItem.IsFile())
					return CodeFile(codeItem);
				else
					arr.push(codeItem);
			}
			
			for each(var codeDirectory:CodeDirectory in arr)
			{
				var codeFile:CodeFile = findFirstFile(codeDirectory);
				
				if(codeFile != CodeFile.Empty)
					return codeFile;
			}
			
			return CodeFile.Empty;
		}
		
		
		public static function findDirectories(directory:CodeDirectory):Array
		{
			var returnArr:Array = new Array();
			
			for each(var codeItem:CodeItem in directory.Items)
			{
				if(codeItem.IsDirectory())
				{
					returnArr.push(codeItem);
					
					for each(var codeDir:CodeDirectory in findDirectories(codeItem as CodeDirectory))
						returnArr.push(codeDir);
				}
			}
			
			return returnArr;
		}
	}
}