/**
 * $id:

 * Copyright(c) 2006 The Midnight Coders Coders Company. All Rights Reserved.
 */

package com.tmnc.mail.vo
{
    import com.tmnc.mail.view.Attachment.AttachmentList;
    
    import mx.controls.Text;
  
    [Bindable]
    [RemoteClass(alias="Weborb.Samples.Email.Entities.MessageInfo")]
    public class MessageInfo {

        public var Sent:Date;
        
        public var From:EmailAddressInfo;
        
        public var Subject:String = "";

        public var To:Array = [];
       
        public var Cc:Array = [];
        
        public var Bcc:Array = [];
        
        public var Size:int = 0;
        
        public var AttachmentDir:String = "";
        
        public var Uid:String = "";
        
        public var HasAttachments:Boolean = false;
        
        //Message Identifier. The value of a Message-ID field.        
        public var MessageId:String = "";
        
        //The value of an In-Reply-To field. References on threaded message.        
        public var InReplyTo:String = "";
        
        public var Body:MessageBodyInfo = null;
        
        public static function getEmailAddressesText(addresses:Array):String {
            var result:String = "";
            
            for (var i:int = 0; i < addresses.length; i++){
                result += addresses[i].DisplayValue;
                
                if (i != addresses.length - 1){
                    result += ", ";
                }
            }
            
            return result;
        }
        
    }
}