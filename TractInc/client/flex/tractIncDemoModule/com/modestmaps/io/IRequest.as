/**
 * @author darren
 * $Id: IRequest.as 292 2007-06-01 00:50:52Z migurski $
 */
package com.modestmaps.io
{
	import flash.events.IEventDispatcher;
	
	public interface IRequest extends IEventDispatcher
	{
		function send():void;
		function execute():void;
		function isBlocking():Boolean;
	}
}