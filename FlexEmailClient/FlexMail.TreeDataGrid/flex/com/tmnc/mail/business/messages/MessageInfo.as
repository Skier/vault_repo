/**
 * $id:

 * Copyright(c) 2006 The Midnight Coders Coders Company. All Rights Reserved.
 */

package com.tmnc.mail.business.messages
{
    import com.tmnc.mail.view.Attachment.AttachmentList;
  
    [Bindable]
    [RemoteClass(alias="Weborb.Samples.Email.Entities.MessageInfo")]
    public class MessageInfo {
        public var AccountId:int;
        public var Sent:Date;
        public var From:String = "";
        public var Subject:String = "";
        public var MessageNumber:int = 0;
        public var To:String = "";
        public var CC:String = "";
        public var BCC:String = "";        
        public var Size:int = 0;
        public var Uid:String = ""; //Message Identifier. The value of a Message-ID field.
        public var InReplyTo:String = ""; //The value of an In-Reply-To field. References on threaded message.
        public var AttachmentDir:String = "";
        public var BodyPlainText:String = "";
        public var BodyPartList:Array = new Array();
        
        public function get htmlBodyPart():BodyPartInfo {
            for each(var part:BodyPartInfo in BodyPartList){
                if (part.FileName == "Message.html"){
                    return part;
                }
            }
            
            return null;
        }
    }
}