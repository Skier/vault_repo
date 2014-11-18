package com.titus.catalog.controller
{
	import com.titus.catalog.model.PdfImage;
	
	import mx.collections.ArrayCollection;
	
	public class PdfImageCash
	{
		private static const MAX_IMAGES:int = 20;
		
		private var images:ArrayCollection = new ArrayCollection();
		
        private static var instance:PdfImageCash;
        
        public static function getInstance():PdfImageCash 
        {
            if (instance == null)
                instance = new PdfImageCash();
            
            return instance;
        }
        
        public function PdfImageCash()
        {
            if (instance != null)
                throw new Error("Singleton!");
        }
        
        public function getImage(url:String):PdfImage 
        {
        	return getImageByUrl(url);
        }
        
        public function storeImage(image:PdfImage):void 
        {
        	var localImage:PdfImage = getImageByUrl(image.url);

        	if (localImage != null) {
        		localImage.bitmap = image.bitmap;
        	} else {
        		images.addItem(image);
        	}
        	
        	while (images.length > MAX_IMAGES) {
        		images.removeItemAt(0);
        	}
        }
        
        private function getImageByUrl(url:String):PdfImage 
        {
        	for each (var image:PdfImage in images) 
        	{
        		if (image.url == url)
        			return image;
        	}
        	
        	return null;
        }

	}
}