package com.affilia.components
{
	import mx.controls.ComboBox;

	public class ExtendedComboBox extends ComboBox
	{
		
		public function ExtendedComboBox()
		{
			super();
		}
		
		private var _autoComplete:Boolean;
		public function get autoComplete():Boolean {return _autoComplete;}
		public function set autoComplete(value:Boolean):void 
		{
			
		}
		
	}
}