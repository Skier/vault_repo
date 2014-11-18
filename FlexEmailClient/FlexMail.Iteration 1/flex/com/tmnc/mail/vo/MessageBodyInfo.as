/**
 * $id:

 * Copyright(c) 2006 The Midnight Coders Coders Company. All Rights Reserved.
 */

package com.tmnc.mail.vo
{
    import com.tmnc.mail.view.Attachment.AttachmentList;
    
    import mx.controls.Text;
  
    [Bindable]
    [RemoteClass(alias="Weborb.Samples.Email.Entities.MessageBodyInfo")]
    public class MessageBodyInfo {

        public var ReplyText:String = "";
        public var Views:Array = [];
        public var Attachments:Array = [];
        public var Uid:String = "";
        
    }
}