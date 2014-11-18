package util
{
    
    import mx.collections.ArrayCollection;
    
    public class ArrayUtil
    {
    	
        //Add range to the ArrayCollection
        public static function addRange(source:ArrayCollection, range:ArrayCollection):void 
        {
            if (!source || !range)
                return;
        
            for each (var o:Object in range) {
            	if (null == o) {
            		continue;
            	}
                source.addItem(o);
            }
        }
        
        //Add range to the ArrayCollection from Array
        public static function addRangeHash(source:ArrayCollection, range:Array):void 
        {
            if (!source || !range)
                return;
        
            for each (var o:Object in range) {
            	if (null == o) {
            		continue;
            	}
                source.addItem(o);
            }
        }
        
    }
    
}
