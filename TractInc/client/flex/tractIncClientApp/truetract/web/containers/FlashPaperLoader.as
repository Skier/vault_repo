/*
 * Copyright (c) 2006 Darron Schall <darron@darronschall.com>
 *
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following
 * conditions:
 * 
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 */
package truetract.web.containers
{

import flash.display.Loader;
import flash.external.ExternalInterface;
import flash.geom.Rectangle;

import mx.controls.SWFLoader;
import mx.core.mx_internal;

// Use the mx_internal namespace because we need to access "contentHolder"
use namespace mx_internal;

/**
 * UIComponent designed specifically to load FlashPaper documents
 * and size them correctly in the available area.
 */
public class FlashPaperLoader extends SWFLoader
{

	/** 
	 * The id of the FlashPaperLoader.swf proxy that will be used
	 * for communication pyurposes.
	 */
	public var fpProxyId:int;
	
	/**
	 * The name of the application that is loading in the FlashPaperLoader.swf
	 * file.  This name must correspond to the "id" attribute in the object tag
	 * and the "name" attribute in the embed tag when the Flex client is embedded
	 * into the html page.
	 */
	public var swfDomId:String;

	/** 
	 * Constructor
	 */
	public function FlashPaperLoader()
	{
		// Configure settings for FlashPaper documents
		scaleContent = false;
		maintainAspectRatio = false;
	}
	
	override protected function updateDisplayList( unscaledWidth:Number,
												   unscaledHeight:Number ):void
	{
		if ( contentHolder )
		{
			// Adjust values so the FlashPaper document is displayed correctly
			contentHolder.scaleX = 1.0;
			contentHolder.scaleY = 1.0;
			contentHolder.x = 0;
			contentHolder.y = 0;
	
			contentHolder.scrollRect = new Rectangle( 0, 0, unscaledWidth, unscaledHeight );
	
			// When the content has loaded, call the setSize method so that the
			// FlashPaper document sizes right in the available area
			if ( Loader( contentHolder ).content )
			{
				setSize( unscaledWidth, unscaledHeight );
			}
			
		}
	}
	
	// =================================================================
	//  Expose methods that are proxied from FlashPaperLoader.swf - Call
	//  JavaScript methods that the FlashPaperLoader.swf file picks up
	//  and passes to the loaded FlashPaper document.
	// =================================================================
	
	public function setSize( width:Number, height:Number ):Boolean
	{
		return ExternalInterface.call( swfDomId + ".setSize" + fpProxyId, width, height );
	}
	
	public function printTheDocument():Boolean
	{
		return ExternalInterface.call( swfDomId + ".printTheDocument" + fpProxyId );
	}
	
	// TODO: You can add more methods here.. see FlashPaper.IFlashPAper for a 
	// list of all available methods.
	
} // end class
} // end package