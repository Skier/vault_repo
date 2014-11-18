package UI.Management.TestDrive
{
	import mx.utils.ObjectUtil;
	
	public class ObjectBrowser
	{
		public var Name:String;
		public var Value:Object;
		public var Items:Array = new Array();
		
		public function ObjectBrowser(name:String, value:Object)
		{
			Value = value;
			Name = name;
			
			if(value == null)
			{
				Value = "Null";
				return;
			}
			
			if(!ObjectUtil.isSimple(value))
			{
				var objClassInfo:Object = ObjectUtil.getClassInfo(value);
	
				for each (var prop:QName in objClassInfo.properties)
					Items.push(new ObjectBrowser(prop.localName, value[prop.localName]));
			}
			else if(value is Array)
			{
				var i:int = 0;
				for each(var o:Object in value)
					Items.push(new ObjectBrowser("Array Item " + (++i).toString() ,o));
			}
		}

		public function IsComplex():Boolean
		{
			return  Items.length > 0 && !IsArray();
		}
		
		public function IsArray():Boolean
		{
			return Value is Array;
		}
	}
}