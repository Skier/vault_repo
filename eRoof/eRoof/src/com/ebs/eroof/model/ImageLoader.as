package com.ebs.eroof.model
{
	import flash.display.Loader;

	public class ImageLoader extends Loader
	{
		public var isLoading:Boolean = false;
		public var imageFileName:String = "";
		public var imageUrl:String = "";
		
		public function ImageLoader()
		{
			super();
		}
		
	}
}