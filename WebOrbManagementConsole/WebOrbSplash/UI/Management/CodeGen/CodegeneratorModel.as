package UI.Management.CodeGen
{
	import UI.Management.ManagementModel;
	
	[Bindable]
	public class CodegeneratorModel
	{
		public var Parent:ManagementModel;
		
		public var m_lastResult:CodegeneratorResult;

		public function CodegeneratorModel(managementModel: ManagementModel)
		{
			Parent = managementModel;
		} 
		
		public var CurrentCodeType:int;
		
		public var Code:String;
		
		public var Info:String;
		
		private var m_currentCodeItem:CodeItem;
		public function set CurrentCodeItem(value:CodeItem):void
		{
			m_currentCodeItem = value;
			
	    	if(CurrentCodeItem == null)
	    		Code =  "";
	    	else if(CurrentCodeItem.IsDirectory())
	    	{
	    		var firstFile:CodeFile = CodeDirectory.findFirstFile(CodeDirectory(CurrentCodeItem));
	    		
	    		if(firstFile == null)
	    			Code = "";
	    		else
	    			Code = firstFile.Content;
	    	}
	    	else
	    		Code = CodeFile(CurrentCodeItem).Content;
	    		
		}
		
		public function get CurrentCodeItem():CodeItem
		{
			return m_currentCodeItem;
		}
		
		public var RootCodeItem:CodeItem;
		
		public function set LastResult(value:CodegeneratorResult):void
		{
			m_lastResult = value;
			
			if(value == null)
			{
				RootCodeItem = null;
				Info = "";
			}
			else 
			{
				RootCodeItem = m_lastResult.Result;
				Info = m_lastResult.Info;
			}
		}
		
		/*[Bindable("propertyChange")]
	    public function get Code():String
	    {
	    	if(CurrentCodeItem == null)
	    		return "";
	    	else if(CurrentCodeItem.IsDirectory())
	    	{
	    		var firstFile:CodeFile = CodeDirectory.findFirstFile(CodeDirectory(CurrentCodeItem));
	    		
	    		if(firstFile == null)
	    			return "";
	    		else
	    			return firstFile.Content;
	    	}
	    	
	    	return CodeFile(CurrentCodeItem).Content;
		}
		
		[Bindable(event="propertyChange")]	
		public function get RootCodeItem():CodeItem
		{
			if(LastResult != null)
				return LastResult.Result;
				
			return null;
		}*/
	}
}