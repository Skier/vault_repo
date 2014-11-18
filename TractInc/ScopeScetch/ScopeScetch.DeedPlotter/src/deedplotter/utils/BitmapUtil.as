package src.deedplotter.utils
{
    import flash.display.BitmapData;
    import flash.display.DisplayObject;
    import flash.geom.Matrix;
    
    public class BitmapUtil
    {
        
        public static function getUIComponentBitmapData( target:DisplayObject, matrix:Matrix ):BitmapData
    	{
    	    var bd:BitmapData = new BitmapData(target.width, target.height, true, 0);

    	    bd.draw(target, matrix);

    	    return bd;
    	}
	        
    }
}