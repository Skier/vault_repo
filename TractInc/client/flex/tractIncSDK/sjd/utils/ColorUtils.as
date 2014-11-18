package sjd.utils
{
	public class ColorUtils{
		
		public static function convertColor(color:String):Number{
			if(color.length > 1 && color.substring(0,1) == "#"){
				return Number("0x" + color.substring(1));
			}else{
				return Number(color);
			}
		}
	}
}