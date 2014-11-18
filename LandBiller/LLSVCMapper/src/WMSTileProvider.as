/**
 * -----------------------------------------------------------------------
 * WMS Custom Tile Provider for UMap
 * -----------------------------------------------------------------------
 */
package com.netthreads.map
{
	import com.afcomponents.umap.abstract.Provider;
	
	import flash.net.URLRequest;
	
	/**
	 * WMSTileProvider custom class.
	 * 
	 * The secret sauce is overriding the parse functions, otherwise you get errors even if you provide
	 * valid urls for settings, language and copyright. 
	 * 
	 */
	public class WMSTileProvider extends Provider
	{ 
      
		public function WMSTileProvider(defaultData:Boolean = false, settings:URLRequest = null, language:URLRequest = null, copyright:URLRequest = null)
		{
			super(defaultData, settings, language, copyright);
		}
	
		override public function getDefaultLanguage():String
		{
			return "EN";
		}
		
		
		
		override protected function parseSettings(data:String):Boolean
		{
		    // trace(data);	
		    
		    return true;
		}
		
		override protected function parseLanguage(data:String):Boolean
		{
            // trace(data);
            
            return true;
		}
		
		override protected function parseCopyright(data:String):void
		{
            // trace(data);    
		}
	
	}
}