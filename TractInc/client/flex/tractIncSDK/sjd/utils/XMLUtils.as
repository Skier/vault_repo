package sjd.utils{
	
	
	public class XMLUtils{
		
		public static function arrayToXML(dataArray:Array, parent:XML = null):XML{
			if(parent == null){
				parent = <node/>;
			}
			for(var i:Number = 0, n:Number = dataArray.length; i < n; i++){
				var node:XML = <node/>;
				if(dataArray[i] is Array){
					arrayToXML(dataArray[i], node);
				}else{
					for(var attr:String in dataArray[i]){
						var value:* = dataArray[i][attr];
						if(
							value is String ||
							value is Number ||
							value is int ||
							value is uint ||
							value is Date
						){
							node.@[attr] = dataArray[i][attr];
						}else if(value is Array){
							arrayToXML(value, node);
						}
						
					}
				}
				parent.appendChild(node);
			}
			return parent;
		}
		
		
		public static function objectToXML(obj:Object):XML{
			if(obj is XML){
				return (obj as XML);
			}
			var node:XML = <node/>;
			if(obj is Array){
				arrayToXML((obj as Array), node);
			}else{
				for(var attr:String in obj){
					var value:* = obj[attr];
					if(
						value is String ||
						value is Number ||
						value is int ||
						value is uint ||
						value is Date
					){
						node.@[attr] = obj[attr];
					}else if(value is Array){
						arrayToXML(value, node);
					}
				}
			}
			return node;
		}
	}
}