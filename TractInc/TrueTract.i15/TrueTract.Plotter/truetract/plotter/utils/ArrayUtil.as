package truetract.plotter.utils
{
public class ArrayUtil
{
    //Add range to the Array
    public static function addRange(source:Array, range:Array):void 
    {
        if (!source || !range)
            return;
        
        for (var i:int = 0; i < range.length; i++)
            source.push(range[i]);
    }
}
}