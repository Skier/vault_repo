package com.tmnc.mail.view.helpers
{
    import com.tmnc.mail.model.MailBoxModel;
    import com.tmnc.mail.view.*;
    import com.tmnc.mail.vo.*;
    
    import flash.text.StyleSheet;
    import flash.text.TextField;
    import flash.text.TextFormat;
    
    import mx.controls.Label;
    import mx.core.Application;
    
    public class ViewHelper
    {
        private static const mainView:MainView = Application.application.mainView;
        private static const messageCreationView:MessageCreationView = Application.application.messageCreationView;
        private static const settingsView:AccountSettingsView = Application.application.settingsView;
        
        public static function get SelectedMessageTreeItem():* {
            return mainView.dg.selectedItem;
        }
        
        public static function set SelectedMessageTreeItem(value:*):void {
            mainView.dg.selectedItem = value;
        }
        
        public static function UpdateMailBoxStatus():void {
            var statusLine:StatusLine = mainView.mailBoxStatus;
            
            var model:MailBoxModel = MailBoxModel.getInstance();

            var boxStatus:MailBoxStatus = model.mailBoxStatus;
            
            statusLine.visible = true;            
            if (boxStatus.MessagesOnServer == 0) {
                statusLine.statusLabel.text = "You have no messages.";
                statusLine.currentState = "checkNewMessageState";
                return;
            }
            
            var retrievedMessagesCount:int = model.messageListDescriptor.messagesCount;
            if (retrievedMessagesCount == boxStatus.MessagesOnServer) {
                statusLine.statusLabel.text = retrievedMessagesCount + " messages.";
                statusLine.currentState = "checkNewMessageState";
                return;
            }

            if (retrievedMessagesCount < boxStatus.MessagesOnServer){
                statusLine.statusLabel.text = "Loaded " + retrievedMessagesCount + " of " + boxStatus.MessagesOnServer;
                statusLine.currentState = "getMoreMessageState";
                return;
            }
            
        }
        
        public static function CleanComposeView():void {
            
            messageCreationView.ToEmail.text    = "";
            messageCreationView.CcEmail.text    = "";
            messageCreationView.BccEmail.text   = "";
            messageCreationView.subjectTxt.text = "";
            messageCreationView.body.text       = "";
            messageCreationView.inReplyTo       = "";
            
            messageCreationView.attachmentPanel.attachmentList.removeAllItems();
        }
        
        public static function PrepareMessageReply(message:MessageInfo):void {
            CleanComposeView();
            
            messageCreationView.ToEmail.text = message.From.DisplayValue;
            messageCreationView.CcEmail.text    = MessageInfo.getEmailAddressesText(message.Cc);
            messageCreationView.BccEmail.text   = MessageInfo.getEmailAddressesText(message.Bcc);
            messageCreationView.subjectTxt.text = "Re: " + message.Subject;
            
            if (message.MessageId) {
                messageCreationView.inReplyTo       = message.MessageId;                
            }
            
            messageCreationView.attachmentPanel.attachmentList.removeAllItems();
            
            //insert symbols ">>" before each line
            var replyText:String = message.Body.ReplyText.replace(/^|\r\n|\r|\n/g, "\n>>");
            replyText = StringToProperHtml(replyText, 0x663300);
                        
            var header:String = "Hello, " + message.From.Name + "\n\n";
            header += message.Sent.toDateString() + " you wrote :\n\n";
            header = StringToProperHtml(header);
            
            messageCreationView.body.htmlText = header + replyText;
            messageCreationView.body.setStyle("fontFamily", "Verdana");
        }

        public static function PrepareMessageForward(message:MessageInfo):void {
            CleanComposeView();

            messageCreationView.subjectTxt.text = "Fwd: " + message.Subject;
            
            var header:String = "\n========This is a forwarded message======.\n";
            header += "From    : " + message.From.DisplayValue + "\n";
            header += "To      : " + message.From.DisplayValue + "\n";            
            header += "Sent    : " + message.Sent.toDateString() + "\n";
            header += "Subject : " + message.Subject + "\n";
            header += "=======================================\n\n";
            header = StringToProperHtml(header, 0x663300);
            
            var body:String = StringToProperHtml(message.Body.ReplyText);
            messageCreationView.body.htmlText = header + body;
        }
        
        public static function PrepareRegistrationView():void {
            settingsView.cleanFields();
            settingsView.isAccountExists = false;
            settingsView.panel.title = "New User Registration";
        }
        
        public static function PrepareAccountSettingsView():void {
            var model:MailBoxModel = MailBoxModel.getInstance();
            
            settingsView.panel.title = "Account Settings";
            
            settingsView.userEmail.text = model.accountInfo.Email;
            settingsView.smtpServer.text = model.smtpSettings.Host;
            settingsView.smtpPort.text = model.smtpSettings.Port.toString();

            if (model.smtpSettings.ConnectionType == ServerSettingsInfo.CONNECTION_TYPE_SECURE_TLS)
                settingsView.smtpConnectionType.selectedIndex = 1;
                
            settingsView.smtpUserName.text = model.smtpSettings.UserName;
            settingsView.smtpPassword.text = model.smtpSettings.UserPassword;

            settingsView.pop3Server.text = model.pop3Settings.Host;
            settingsView.pop3Port.text = model.pop3Settings.Port.toString();
            
            if (model.pop3Settings.ConnectionType == ServerSettingsInfo.CONNECTION_TYPE_SECURE_TLS)
                settingsView.pop3ConnectionType.selectedIndex = 1;
            
            settingsView.pop3UserName.text = model.pop3Settings.UserName;
            settingsView.pop3Password.text = model.pop3Settings.UserPassword;
            settingsView.isAccountExists = true;
        }
        
        private static function StringToProperHtml(s:String, fontColor:uint=0x000000, fontFace:String="Verdana"):String {
            var field:TextField = new TextField();
            var tf:TextFormat = new TextFormat();
            tf.color = fontColor;
            tf.font = fontFace;
            field.text = s;
            field.setTextFormat(tf);
            return field.htmlText;
        }
        
    }
}