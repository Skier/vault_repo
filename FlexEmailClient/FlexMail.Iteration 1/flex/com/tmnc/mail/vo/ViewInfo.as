/**
 * $id:

 * Copyright(c) 2006 The Midnight Coders Coders Company. All Rights Reserved.
 */

package com.tmnc.mail.vo
{
    
    [Bindable]
    [RemoteClass(alias="Weborb.Samples.Email.Entities.ViewInfo")]
    public class ViewInfo extends FileInfo {
        
        public static const CONTENT_TYPE_PLAIN:String = "text/plain";
        public static const CONTENT_TYPE_HTML:String = "text/html";
                
        public var ContentType:String;
        public var Text:String;
        
        override public function get label():String {
            return Name;
        }
    }
}