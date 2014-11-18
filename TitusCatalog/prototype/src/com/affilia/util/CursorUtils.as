package com.affilia.util
{
	import mx.managers.CursorManager;
	import mx.managers.CursorManagerPriority;
	
	public class CursorUtils{

		private static var currentType:Class = null;
				
		public static function changeCursor(type:Class, xOffset:Number = 0, yOffset:Number = 0):void{
			if(currentType != type){
				currentType = type;
				CursorManager.removeCursor(CursorManager.currentCursorID);
				if(type != null){
					CursorManager.setCursor(type, CursorManagerPriority.MEDIUM, xOffset, yOffset);
				}
			}
		}
	}
}