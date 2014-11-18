package sjd.controls{
	
	import flash.events.MouseEvent;
	import flash.events.Event;
	import flash.events.FocusEvent;
	import mx.controls.TextInput;
	import mx.controls.Button;
	import mx.events.FlexEvent;
	import sjd.utils.Constant;
	import sjd.utils.ColorUtils;

	/**
	 * @class ClearTextInput
	 * @brief A TextInput with button to clear the text.
	 * @author Jove
	 * @version 1.3
	 */
	public class ClearTextInput extends TextInput{
				
		public var defultTextColor:String = "#FF0000";

		private var clearButton:Button;

		private var _defultText:String = "Please Input Text..."
		
		public function set defultText(value:String):void{
			if(value == null || value == ""){
				defultTextColor = new String(this.getStyle("color"));
			}
			_defultText = value;
		}
		public function get defultText():String{
			return _defultText;
		}
		
		
		
		public function ClearTextInput(){
			super();
			this.addEventListener(FlexEvent.CREATION_COMPLETE, addButton);
			//this.addEventListener("textChanged", showClearButton);
			this.addEventListener(FocusEvent.FOCUS_IN, this_focusInHandler)
			this.addEventListener(FocusEvent.FOCUS_OUT, this_focusOutHandler)
		}
		
		private function addButton(event:FlexEvent):void{
			if(clearButton == null){
				clearButton = new Button();
				clearButton.width=10;
				clearButton.height=10;
				clearButton.y = (this.height - 10) / 2;
				clearButton.x = this.width - 10 - (this.height - 10) / 2;
				clearButton.focusEnabled=false;
				clearButton.setStyle("upSkin", Constant.WINDOW_CLOSE_BUTTON_1);
				clearButton.setStyle("overSkin", Constant.WINDOW_CLOSE_BUTTON_2);
				clearButton.setStyle("downSkin", Constant.WINDOW_CLOSE_BUTTON_2);
				clearButton.addEventListener(MouseEvent.CLICK, clearButton_clickHandler);
				clearButton.visible = false;
				clearButton.buttonMode = true;
				clearButton.useHandCursor = true;
				clearButton.mouseChildren = false;
				this.addChild(clearButton);
				this.addEventListener("textChanged", showClearButton);
				showClearButton(null);
				if(text == ""){
					setDefaultText();
				}
			}
		}
		
		private function clearButton_clickHandler(event:MouseEvent):void{
			this.text = "";
			clearButton.visible = false;
		}
		
		private function this_focusInHandler(event:FocusEvent):void{
			if(text == ""){
				this.text = "";
				this.textField.text = "";
				this.textField.textColor = this.getStyle("color");
				clearButton.visible = false;
			}
		}
		
		private function this_focusOutHandler(event:FocusEvent):void{
			if(text == ""){
				setDefaultText();
			}
		}
		
		private function setDefaultText():void{
			this.text = "";
			this.textField.text = _defultText;
			this.textField.textColor = ColorUtils.convertColor(defultTextColor);
			clearButton.visible = false;
		}
		
		
				
		private function showClearButton(event:Event):void{
			if(clearButton){
				if(text != ""){
					clearButton.visible = true;
				}else{
					clearButton.visible = false;
				}
				this.textField.textColor = this.getStyle("color");
			}
		}
		
		override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void{
			super.updateDisplayList(unscaledWidth, unscaledHeight);
			if(this.textField.text == this._defultText && this.text == ""){
				this.textField.textColor = ColorUtils.convertColor(defultTextColor);
			}
			if(clearButton != null){
				clearButton.x = this.width - 10 - (this.height - 10) / 2;
				clearButton.y = (this.height - 10) / 2;
			}
		}
	}
}