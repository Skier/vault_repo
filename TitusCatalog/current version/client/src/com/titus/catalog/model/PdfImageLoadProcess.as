package com.titus.catalog.model
{
	import flash.display.Loader;
	import flash.net.URLRequest;
	import flash.system.LoaderContext;

	public class PdfImageLoadProcess extends Loader
	{
		public var priority:int;
		public var url:String;
		
		public var isLoading:Boolean = false;
		
		public function PdfImageLoadProcess()
		{
			super();
		}
		
		public function loadImage():void 
		{
			super.load(new URLRequest(this.url), new LoaderContext(true));
			this.isLoading = true;
		}
		
	}
}