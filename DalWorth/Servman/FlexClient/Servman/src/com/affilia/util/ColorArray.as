package com.affilia.util
{
	public class ColorArray
	{
		public function ColorArray()
		{
		}

		private static var colors:Array = [0x9d1200,0x0055a4,0x61008c,0xffd200,0x009200,0xff7f0b,0x008c88,0xe3001f,0x27198c,0x00704a,0x9f218b,0xee2c74,0x878c00];
		

		public static function getColorAt(index:int):uint{
			return colors[index % 13] as uint;
		}
		
		public static function get length():int{
			return colors.length;
		}
		
		public static function shiftColors(index:int):void
		{
			var i:int;
			var colorToShift:uint = colors[i] as uint;
			var newColors:Array = new Array();
			for( i=0 ; i < colors.length ; i++ )
			{
				if(i != index)
				{
					newColors.push(colors[i]);
				}
			}
			newColors.push(colors[index]);
			colors = newColors;
		}

	}
}