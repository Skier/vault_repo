package sjd.validators
{
	import mx.validators.Validator;
	import mx.core.UIComponent;
	import mx.validators.ValidationResult;
	
	/**
	 * @class MultipTipsValidator
	 * @brief Support Multip-Field Validator with Different Error String
	 * @author Jove
	 * @version 1.2
	 */
	public class MultipTipsValidator extends Validator{
		
		//The fields that want to show the error string.
		public var allListeners:Array = [];
		
	
		override protected function get actualListeners():Array{
			super.doValidation(new Object());
			var result:Array = [];
			if (listener){
				if(listener is Array){
					var listenerArray:Array = listener as Array;
					for(var i:Number = 0; i < listenerArray.length; i++){
						result.push(listenerArray[i]);
					}
				}else{
					result.push(listener);
				}
			}else if (source){
				result.push(source);
			}	
			return result;
		}
	
		protected function setListener(results:Array):void{
			var actualListeners:Array = [];
			if(allListeners != null && allListeners.length > 0){
				for(var i:Number = 0; i < allListeners.length; i++){
					//Reset components' errorString
					(allListeners[i] as UIComponent).errorString = "";
					for(var j:Number = 0; j < results.length; j++){
						if(results[j] is MultipTipsValidationResult){
							var result:MultipTipsValidationResult = results[j] as MultipTipsValidationResult;
							if(allListeners[i].id == result.errorFieldId){
								actualListeners.push(allListeners[i]);
								//Update different Error String
								(allListeners[i] as UIComponent).callLater(setErrorStr, [(allListeners[i] as UIComponent), result.errorMessage]);
							}
						}
						
					}
				}
			}
			if(actualListeners.length > 0){
				this.listener = actualListeners;
			}
		}
		
		protected function setErrorStr(comp:UIComponent, str:String):void{
			comp.errorString = str;
		}
	}
}