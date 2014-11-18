/**
 * $id:

 * Copyright(c) 2006 The Midnight Coders Coders Company. All Rights Reserved.
 */

package com.tmnc.mail.business.messages
{
    import mx.formatters.NumberFormatter;
    
	[Bindable]
	[RemoteClass(alias="Weborb.Samples.Email.Entities.BodyPartInfo")]
	public class BodyPartInfo {
		public var FileName:String;
		public var FileUrl:String;
				
		public var Size:Number = 0;
        public var Text:String;
        public var ContentType:String;
        
        //need for Data Provider support
		public function get label():String{
		    
            if (Size > 1024){
		        return FileName + " (" + Math.round(Size/1024) + " Kb)";
            } else {
		        return FileName + " (" + Size + " b)";                
            }
		}
	}
}