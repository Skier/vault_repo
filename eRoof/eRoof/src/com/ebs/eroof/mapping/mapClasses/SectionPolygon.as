package com.ebs.eroof.mapping.mapClasses
{
	import com.afcomponents.umap.overlays.Polygon;
	import com.afcomponents.umap.styles.GeometryStyle;
	import com.afcomponents.umap.types.LatLng;
	import com.ebs.eroof.model.wrapper.Section;

	[Bindable]
	public class SectionPolygon extends EditablePolygon
	{
		private static const POINTS_DIVIDER:String = ";";
		private static const LATLNG_DIVIDER:String = " ";
		
		private var _section:Section;
		public function get section():Section { return _section; }
		public function set section(value:Section):void 
		{
			_section = value;
			
			updatePoints();
		}
		
		public function SectionPolygon(arg0:Object=null, arg1:Object=null)
		{
			super(arg0, arg1);
		}
		
		private function updatePoints():void 
		{
			// todo
		}
		
		public function toPolygon():Polygon 
		{
			var result:Polygon = new Polygon({points:points}, defaultPolygonStyle);
			result.autoInfo = false;
			result.smartPosition = false;
			return result;
		}
		
		public function toDBString():String 
		{
			var result:String = "";
			
			for each (var point:LatLng in corePoints) 
			{
				if (result != "")
					result += POINTS_DIVIDER;
				
				result += point.lat.toString();
				result += LATLNG_DIVIDER;
				result += point.lng.toString();
			}
			
			return result;
		}
		
		public static function parse(polygonStr:String):SectionPolygon 
		{
			return getPolygon(parsePoints(polygonStr));
		}
		
		private static function parsePoints(pointsStr:String):Array 
		{
			var points:Array = new Array();
			
			var pointStrArr:Array = pointsStr.split(";");
			for each (var pointStr:String in pointStrArr)
			{
				var lat:Number = Number(pointStr.split(" ")[0]);
				var lng:Number = Number(pointStr.split(" ")[1]);
				
				if (!isNaN(lat) && !isNaN(lng))
					points.push(new LatLng(lat, lng));
			}
			
			return points;
		}
		
		public static function getPolygon(points:Array):SectionPolygon
		{
			if (!points || points.length < 3)
				return null;
				
            var param:Object = new Object();
                param.points = points;

            var result:SectionPolygon = new SectionPolygon(param, defaultStyle);
	            result.autoInfo = false;
	            result.smartPosition = false;
            return result;
		}
		
        public static function get defaultStyle():GeometryStyle 
        {
            var result:GeometryStyle = new GeometryStyle();
                result.fill = GeometryStyle.RGB;
                result.fillRGB = 0x0000ff;
                result.fillAlpha = 0.3;
                result.stroke = GeometryStyle.RGB;
                result.strokeRGB = 0x000099;
                result.strokeThickness = 2;
                result.strokeAlpha = .8;
            return result;
        }
            
        private function get defaultPolygonStyle():GeometryStyle 
        {
            var result:GeometryStyle = new GeometryStyle();
                result.fill = GeometryStyle.RGB;
                result.fillRGB = 0xffffff;
                result.fillAlpha = 0.2;
                result.stroke = GeometryStyle.RGB;
                result.strokeRGB = 0x666666;
                result.strokeThickness = 2;
                result.strokeAlpha = .8;
            return result;
        }
            
	}
}