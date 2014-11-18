package sjd.controls{
	
	import mx.core.Application;
	import mx.controls.Button;
	import mx.controls.TextInput;
	import mx.controls.ToolTip;
	import mx.events.FlexEvent;
	import flash.display.InteractiveObject;
	import flash.events.Event;
	import flash.events.FocusEvent;
	import flash.events.KeyboardEvent;
	import flash.events.MouseEvent;
	import flash.events.TextEvent;
	import flash.geom.Point;
	import flash.ui.Keyboard;
	import sjd.utils.Constant;
	import sjd.utils.ColorUtils;
	
	
	/**
	 * @class TemplateInput
	 * @brief Enable template for TextInput and clear text button.
	 * @author Jove flex-flex.net
	 * @version 1.2
	 */
	public class TemplateInput extends TextInput implements ITemplateInputRule{
	
		private var clearButton:Button;
		
		private var errorTip:ToolTip;
		
		//public var defultText:String = "Please Input...";
		public var defultTextColor:String = "#FF0000";
		//If errorTipText is blank, the ErrorTip won't be shown.
		public var errorTipText:String = "<font size=\"12\">You couldn't input \"<font color=\"#0000FF\">@</font>\" at this position!</font>";
		
		private var _inputDefault:String = null;//__(__)___
		private var _inputTemplate:String = null;//cc(CC)NNN
		private var _blankChar:String = "_";//_
		private var _actualText:String = null;
		private var _fullText:String = null;
		//provide the default validate rule
		private var _templateInputRule:ITemplateInputRule = ITemplateInputRule(this);
		
		private var isTemplated:Boolean = false;
		
		private var oldPosition:Number = -1;
		private var beginPosition:Number = -1;
		private var endPosition:Number = -1;
		private var currentKeyCode:Number = -1;
		
		// Temp variable for display
		private var _text:String = "";
		private var oldStr:String = "";
		
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
	
	
		[Bindable]
		public function get inputTemplate():String{
			return _inputTemplate;
		}
		public function set inputTemplate(templateStr:String):void{
			
			_inputTemplate = templateStr;
			
			//Reset the text.
			text = "";
			if(_inputTemplate == "" || _inputTemplate == null){
				isTemplated = false;
			}else{
				isTemplated = true;
				
				_inputDefault = _inputTemplate;
				for(var m:Number = 0; m < _inputTemplate.length; m++){
					if(validateCharAt(m)){
						_inputDefault = _inputDefault.substring(0, m) + _blankChar + _inputDefault.substring(m + 1);
					}
				}
			}
			
			callLater(setDefaultText);
		}
		
		[Bindable]
		public function get actualText():String{
			_actualText = "";
			
			if(isTemplated){
				
				for(var i:Number = 0; i < text.length; i++){
					if(validateCharAt(i) && text.charAt(i) != _blankChar){
						_actualText += text.charAt(i);
					}
				}
				
			}else{
				_actualText = text;
			}
			return _actualText;
		}
		public function set actualText(actualTextStr:String):void{
			if(actualTextStr != null && actualTextStr != _text){
				if(isTemplated && _inputDefault != null){
					this_focusInHandler(new FocusEvent(FocusEvent.FOCUS_IN));
					_actualText = _inputDefault;
					_actualText = _actualText.substr(0, _inputTemplate.length);
					for(var m:Number = 0, n:Number = 0; m < _inputDefault.length; m++){
						if(validateCharTypeAt(m, actualTextStr.charAt(n))){
							_actualText = _actualText.substring(0, m) + actualTextStr.charAt(n) + _actualText.substring(m + 1);
							n++;
						}
					}
					_text = _actualText;
					invalidateDisplayList();
					//Set the old text.
					oldStr = _text;
				}else{
					text = actualTextStr;
				}
			}
		}
		
		[Bindable]
		public function get fullText():String{
			//full text = text
			_fullText = text;
			if(_fullText == "" && isTemplated){
				_fullText = _inputDefault;
			}
			
			return _fullText;
		}
		public function set fullText(fullTextStr:String):void{
			//Because set text is used in other methods and it's difficulty to differentiate paste and set text events,
			//fullText is used to set all the text to the TemplateInput.
			if(fullTextStr != null && fullTextStr != text){
				if(isTemplated){
					this_focusInHandler(new FocusEvent(FocusEvent.FOCUS_IN));
					_fullText = fullTextStr;
					_fullText = _fullText.substr(0, _inputTemplate.length);
					for(var i:Number = 0; i < _inputTemplate.length; i++){
						if(!validateCharTypeAt(i, _fullText.charAt(i))){
							_fullText = _fullText.substring(0, i) + _inputDefault.charAt(i) + _fullText.substring(i + 1);
						}
					}
					_text = _fullText;
					invalidateDisplayList();
					//Set the old text.
					oldStr = _text;
				}else{
					text = fullTextStr;
				}
			}
		}
		
		[Bindable]
		public function get blankChar():String{
			return _blankChar;
		}
		public function set blankChar(blankCharStr:String):void{
			
			if(blankCharStr != null && blankCharStr.length > 0){
				var tempChar:String = blankCharStr.substring(0, 1);
				if(isTemplated && _inputDefault != null){
					for(var m:Number = 0; m < _inputTemplate.length; m++){
						if(validateCharAt(m)){
							//Replace the _inputDefault.
							_inputDefault = _inputDefault.substring(0, m) + tempChar + _inputDefault.substring(m + 1);
							//Replace the current text.
							if(_text.charAt(m) == _blankChar){
								_text = _text.substring(0, m) + tempChar + _text.substring(m + 1);
							}
						}
					}
					_blankChar = tempChar;
					invalidateDisplayList();
					//Set the old text.
					oldStr = _text;
				}else{
					_blankChar = tempChar;
				}
			}
		}
		
		[Bindable]
		public function get templateInputRule():ITemplateInputRule{
			return _templateInputRule;
		}
		public function set templateInputRule(rule:ITemplateInputRule):void{
			if(rule == null){
				_templateInputRule = ITemplateInputRule(this);
			}else{
				_templateInputRule = rule;
			}
			//Reset the text.
			text = "";
			for(var m:Number = 0; m < _inputTemplate.length; m++){
				if(validateCharAt(m)){
					_inputDefault = _inputDefault.substring(0, m) + _blankChar + _inputDefault.substring(m + 1);
				}else{
					_inputDefault = _inputDefault.substring(0, m) + _inputTemplate.charAt(m) + _inputDefault.substring(m + 1);
				}
			}
			
			callLater(setDefaultText);
		}
		
		
		/**
	 	 * Constructor.
	 	 * Add envents listener
	 	 */
		public function TemplateInput(){
			super();
			addEventListener(FlexEvent.CREATION_COMPLETE, this_init);
			addEventListener("textChanged", this_textChanged);
			addEventListener(FocusEvent.FOCUS_IN, this_focusInHandler)
			addEventListener(FocusEvent.FOCUS_OUT, this_focusOutHandler)
			addEventListener(KeyboardEvent.KEY_DOWN, this_setPosition);
			addEventListener(MouseEvent.MOUSE_DOWN, this_setPosition);
			addEventListener(MouseEvent.MOUSE_MOVE, this_setPosition);
			addEventListener(MouseEvent.MOUSE_OUT, hideErrorTip);
	
		}
		
		private function this_init(event:FlexEvent):void{
			if(clearButton == null){
				//Create the button to clear text.
				clearButton = new Button();
				clearButton.width=10;
				clearButton.height=10;
				clearButton.y = (height - 10) / 2;
				clearButton.x = width - 10 - (height - 10) / 2;
				clearButton.focusEnabled=false;
				clearButton.setStyle("upSkin", Constant.WINDOW_CLOSE_BUTTON_1);
				clearButton.setStyle("overSkin", Constant.WINDOW_CLOSE_BUTTON_2);
				clearButton.setStyle("downSkin", Constant.WINDOW_CLOSE_BUTTON_2);
				clearButton.addEventListener(MouseEvent.CLICK, clearButton_clickHandler);
				clearButton.visible = false;
				clearButton.useHandCursor = true;
				clearButton.buttonMode = true;
				addChild(clearButton);
				
				//Create the ErrorTip.
				errorTip = new HtmlToolTip();
				errorTip.visible = false;
				errorTip.setStyle("styleName", "errorTip");
				errorTip.setStyle("borderStyle", "errorTipBelow");
				systemManager.addChild(errorTip);
				
				//Set the _inputDefault.
				if(isTemplated && _blankChar != null){
					_inputDefault = _inputTemplate;
					for(var m:Number = 0; m < _inputTemplate.length; m++){
						if(validateCharAt(m)){
							_inputDefault = _inputDefault.substring(0, m) + _blankChar + _inputDefault.substring(m + 1);
						}
					}
				}
				//If there's no text, set the default text.
				if(text == "" || text == _inputDefault){
					callLater(setDefaultText);
				}
			}
		}
		
		private function clearButton_clickHandler(event:MouseEvent):void{
			if(isTemplated){
				text = _inputDefault;
				_text = _inputDefault;
			}else{
				text = "";
			}
			oldStr = _inputDefault;
			clearButton.visible = false;
			dispatchEvent(new Event(Event.CHANGE));
		}
		
		private function this_focusInHandler(event:FocusEvent):void{
			
			if(text == "" || text == _inputDefault){
				
				if(isTemplated){
					_text = _inputDefault;
					text = _inputDefault;
					textField.text = _inputDefault;
				}else{
					_text = "";
					textField.text = "";
				}
				
				oldStr = textField.text;
				textField.textColor = getStyle("color");
				clearButton.visible = false;
				dispatchEvent(new Event(Event.CHANGE));
			}
		}
		
		private function this_focusOutHandler(event:FocusEvent):void{
			
			if(text == "" || text == _inputDefault){
				setDefaultText();
			}
			hideErrorTip();
		}
		
		private function this_textChanged(event:Event):void{
			//close button
			if(text == "" || text == _inputDefault){
				clearButton.visible = false;
			}else{
				clearButton.visible = true;
			}
			
			textField.textColor = getStyle("color");
			
			if(isTemplated){
				if(oldStr != text){
					//Get the old text.
					_text = oldStr;
					
					if(currentKeyCode == Keyboard.BACKSPACE && beginPosition == endPosition){
						//Process BACKSPACE.
						if(validateCharAt(selectionBeginIndex)){
							_text = _text.substring(0, selectionBeginIndex) + _blankChar + _text.substring(selectionBeginIndex + 1);
						}
					}else if(text == ""){
						//For the check condition of updateDisplayList.
						_text = _inputDefault;
					}else{
						var backStr:String = _text.substring(endPosition);
						var newStr:String = "";
						
						newStr = text.substring(beginPosition);
						
						if(newStr.length >= backStr.length){
							newStr = newStr.substr(0, newStr.length - backStr.length);
						}else{
							newStr = "";
						}
						//Get the new input text.
						newStr = newStr.substr(0, _inputTemplate.length);
						
						if(beginPosition == endPosition){
							endPosition++;
						}
						
						//End position of the new input text for check .
						var ep:Number = 0;
						//Set the end position.
						if(endPosition > beginPosition + newStr.length){
							ep = endPosition;
						}else{
							ep = beginPosition + newStr.length;
						}
						//Because we check the position first, the position is suitable for a single char.
						if(newStr.length == 1){
							if(validateCharTypeAt(beginPosition, newStr.charAt(0))){
								_text = _text.substring(0, beginPosition) + newStr.charAt(0) + _text.substring(beginPosition + 1);
							}else{
								setSelection(oldPosition, oldPosition);
								if(newStr && newStr.length > 0){
									showErrorTip(errorTipText.replace(/@/g, newStr.substr(0, 1)));
								}
							}
						//}else if(newStr.length > 1 || Keyboard.DELETE){
						}else{
							//multiple chars
							var isReplaced:Boolean = false;
							for(var m:Number = beginPosition, n:Number = 0; m < ep; m++){
								//Is template position and within newStr, replace new char.
								if(validateCharAt(m) && n < newStr.length){
									if(validateCharTypeAt(m, newStr.charAt(n))){
										_text = _text.substring(0, m) + newStr.charAt(n) + _text.substring(m + 1);
										isReplaced = true;
									}
								//Is template position but without the newStr, replace blank char.
								}else if(validateCharAt(m)){
									_text = _text.substring(0, m) + _blankChar + _text.substring(m + 1);
								//Not template position, keep char of template.
								}else if(!validateCharAt(m)){
									_text = _text.substring(0, m) + _inputTemplate.charAt(m) + _text.substring(m + 1);
								}
								n++;
							}
							if(_text == oldStr && !isReplaced){
								if(newStr && newStr.length > 0){
									showErrorTip(errorTipText.replace(/@/g, newStr.substr(0, 1)));
								}
							}
						}
					}
					
					invalidateDisplayList();
					
					//set the old text
					oldStr = _text;
						
				}
				
				//set the old position
				beginPosition = selectionBeginIndex;
				endPosition = selectionEndIndex;
				oldPosition = selectionBeginIndex;
			}
			//Make the actualText change to notify others the property has benn changed (use for data binding).
			notifyTextChange();
			
		}
		
		private function this_setPosition(event:Event):void{
			if(isTemplated){
				
				beginPosition = selectionBeginIndex;
				endPosition = selectionEndIndex;
					
				if(event is KeyboardEvent){
					currentKeyCode = KeyboardEvent(event).keyCode;
					hideErrorTip();
				}
				
				if(event.type != MouseEvent.MOUSE_MOVE && currentKeyCode != Keyboard.BACKSPACE){
					invalidateDisplayList();
					hideErrorTip();
				}
			}
		}
		
		private function checkPosition():void{
			if(isTemplated){
				
				if(currentKeyCode == Keyboard.BACKSPACE){
					
					if(!validateCharAt(selectionBeginIndex) && oldPosition >= 0 && selectionBeginIndex <= _inputTemplate.length && selectionBeginIndex >=0){
						//BackSpace: Del the jumped char.
						var t:String = _inputDefault.substr(0, selectionBeginIndex);
						var ep:Number = t.lastIndexOf(_blankChar);
						
						if(ep >= 0){
							for(var j:Number = ep; j < oldPosition; j++){
								if(validateCharAt(j)){
									_text = oldStr.substr(0, j) + _blankChar + oldStr.substr(j + 1);
								}
							}
							
							oldPosition = ep;
							setSelection(ep, ep);
							callLater(invalidateDisplayList);
						}else{
							ep = _inputDefault.indexOf(_blankChar);
							oldPosition = ep;
							setSelection(ep, ep);
						}
						
						oldStr = _text;
					}else{
						oldPosition = selectionBeginIndex;
					}
					
					
				}else if(!validateCharAt(selectionBeginIndex) && oldPosition >= 0 && selectionBeginIndex <= _inputTemplate.length && selectionBeginIndex >=0){
					
					if(selectionBeginIndex >= oldPosition){
						//To find the latest "#" position, and move to it.
						var t1:String = _inputDefault.substring(selectionBeginIndex);
						var p1:Number = t1.indexOf(_blankChar);
						if(p1 >= 0){
							oldPosition = selectionBeginIndex + p1;
							setSelection(selectionBeginIndex + p1, selectionBeginIndex + p1);
						}else{
							p1 = _inputDefault.lastIndexOf(_blankChar);
							oldPosition = p1;
							setSelection(p1, p1);
						}
					}else{
						//To find the latest "#" position, and move to it.
						var t2:String = _inputDefault.substr(0, selectionBeginIndex);
						var p2:Number = t2.lastIndexOf(_blankChar);
						
						if(p2 >= 0){
							
							oldPosition = p2;
							setSelection(p2, p2);
						}else{
							p2 = _inputDefault.indexOf(_blankChar);
							oldPosition = p2;
							setSelection(p2, p2);
						}
						
					}
				}else{
					oldPosition = selectionBeginIndex;
				}
			}
		}
		
		private function setDefaultText():void{
			if(clearButton == null){
				return;
			}
			oldStr = _inputDefault;
			_text = _inputDefault;
			
			if(defultText == null || defultText == ""){
				defultText = _inputDefault;
				defultTextColor = getStyle("color");
			}
			
			textField.text = defultText;
			textField.textColor = ColorUtils.convertColor(defultTextColor);
			
			clearButton.visible = false;
			
			//Move out the focus to avoid focus-in event.
			if(stage.focus == InteractiveObject(textField)){
				stage.focus = InteractiveObject(Application.application);
			}
			//Make the actualText change to notify others the property has benn changed (use for data binding) .
			notifyTextChange();
			
			dispatchEvent(new Event(Event.CHANGE));
			
		}
		
		private function notifyTextChange():void{
			actualText = null;
			actualText = _text;
			fullText = null;
			fullText = text;
		}
	
		protected function validateCharAt(index:Number):Boolean{
			var templateChar:String = _inputTemplate.charAt(index);
			return _templateInputRule.validateChar(templateChar);
		}
	
		protected function validateCharTypeAt(index:Number, inputChar:String):Boolean{
			var templateChar:String = _inputTemplate.charAt(index);
			return _templateInputRule.validateCharType(templateChar, inputChar);
		}
		
		private function showErrorTip(text:String):void{
			if(text != ""){
				var point:Point = new Point(0, 0);
				point = this.localToGlobal(point);
				errorTip.x = point.x;
				errorTip.y = point.y + this.height;
				errorTip.text = text;
				errorTip.visible = true;
			}
		}
		
		private function hideErrorTip(event:MouseEvent = null):void{
			errorTip.visible = false;
		}
				
		override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void{
			
			if(isTemplated && (_text == _inputDefault || text != "") ){
				text = _text;
			}
			
			super.updateDisplayList(unscaledWidth, unscaledHeight);
			
			if(this.textField.text == this._defultText && this.text == ""){
				this.textField.textColor = ColorUtils.convertColor(defultTextColor);
			}
			
			
			if(clearButton != null){
				clearButton.x = width - 10 - (height - 10) / 2;
				clearButton.y = (height - 10) / 2;
			}
			
			checkPosition();
		}
		
		/**
		 * Implement the ITemplateInputRule to validate if the template char is a wildcard. 
		 * Default: C means [A-Z]; c means [a-z]; N means a number.
		 * @param templateChar The template char
		 * @return The validate result
		 */
		public function validateChar(templateChar:String):Boolean{
			switch(templateChar){
				case "C":
					return true;
				case "c":
					return true;
				case "N":
					return true;
				default:
					return false;
			}
		}
		
		/**
		 * Implement the ITemplateInputRule to validate if the input char matchs the template char. 
		 * Default: C means [A-Z]; c means [a-z]; N means a number.
		 * @param templateChar The template char
		 * @param inputChar The input char
		 * @return The validate result
		 */
		public function validateCharType(templateChar:String, inputChar:String):Boolean{
			var pattern:RegExp = /""/;
			switch(templateChar){
				case "C":
					pattern = /[A-Z]/
					break;
				case "c":
					pattern = /[a-z]/
					break;
				case "N":
					pattern = /\d/
					break;
				default:
					pattern = /""/;
					break;
			}
			return pattern.test(inputChar);
		}
		
	}
	
}