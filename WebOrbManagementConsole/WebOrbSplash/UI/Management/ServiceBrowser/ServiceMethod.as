package UI.Management.ServiceBrowser
{
	import mx.rpc.xml.DataType;
	
	[RemoteClass(alias="Weborb.Management.ServiceBrowser.ServiceMethod")]
	public class ServiceMethod extends ServiceNode
	{
		public function getInfo():String
		{
			var info:String = this.Name + "(";
			var first:Boolean = true;
			
			for each(var arg:ServiceMethodArg in Items)
			{
				if(!first)
					info = info + ",";
				else
					first = false;
					
				info = info + arg.Name + ":" + arg.DataType.Name;
			}
			
			info = info + "):" + ReturnDataType.Name;
			
			return info;
		}
		
		public var ReturnDataType:ServiceDataType;

		/*
		public function getArgs(values:Array):Array
		{
			var args:Array = new Array();
			
        	for each(var arg:ServiceMethodArg in Items)
        	{
        		var uri:String = arg.Name;
        		
        		if(arg.DataType.IsComplexType())		
        			args.push(getComplexValue(arg.DataType,values,uri));
        		else
        			args.push(values[uri]);
        	}
        	
        	return args;
		}
		
		private function getComplexValue(dataType:ServiceDataType, values:Array, uri:String):Array
		{
			var fields:Array = new Array();
			
			for each(var field:ServiceDataTypeField in dataType.Items)
			{
				var valueUri: String = uri + "." + field.Name;
				
				if(field.DataType.IsComplexType())
        			fields[field.Name] = getComplexValue(field.DataType,values,valueUri);
        		else
        			fields[field.Name] = values[valueUri];
   			}
   			
   			return fields;
		}
		*/
	}
}