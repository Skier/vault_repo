package com.titus.catalog.model
{
	import flash.display.Bitmap;
	
	public class PdfImage
	{
		public var url:String;
		public var bitmap:Bitmap;
		
		public function PdfImage(url:String =  null, bitmap:Bitmap = null)
		{
			this.url = url;
			this.bitmap = bitmap;
		}

	}
}