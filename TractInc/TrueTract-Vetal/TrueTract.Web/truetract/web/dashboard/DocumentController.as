package truetract.web.dashboard
{
import mx.controls.Alert;
import mx.core.Application;
import mx.rpc.AsyncToken;
import mx.rpc.events.ResultEvent;

import truetract.domain.Document;
import truetract.domain.DocumentAttachment;
import truetract.web.dashboard.documentPanel.attachmentEditor.AttachmentEditorView;
import truetract.web.services.TrueTractService;
import truetract.web.util.TokenResponder;
import mx.events.PropertyChangeEvent;
import mx.events.PropertyChangeEventKind;
import mx.events.DynamicEvent;
import mx.utils.ObjectUtil;
import truetract.web.AppModel;

public class DocumentController
{
    public var document:Document;

    private var ttService:TrueTractService = TrueTractService.getInstance();
    
    private var app:Application = Application(Application.application);

    public function addAttachment():void
    {
        var attachment:DocumentAttachment = new DocumentAttachment();
        attachment.DocumentId = document.DocID;

        var popup:AttachmentEditorView = AttachmentEditorView.open(app, true);
        popup.attachment = attachment;
        popup.addEventListener("commit", function ():void 
        {
            if (attachment.IsPdfCopy() && document.PdfCopy)
            {
                Alert.show("The PDF Copy of the Document is already specified");
                return;
            }

            var token:AsyncToken = ttService.service.AddDocumentAttachment(attachment, popup.uploadID);
            token.addResponder(new TokenResponder(
                function (event:ResultEvent):void 
                {
                    var a:DocumentAttachment = DocumentAttachment(event.result);

                    document.AttachmentsList.addItem(a);
                    document.dispatchEvent(new PropertyChangeEvent(
                        PropertyChangeEvent.PROPERTY_CHANGE, false, false, 
                        PropertyChangeEventKind.UPDATE, "PdfCopy"));

                    popup.close();
                },
                "Unable to Add Attachment"
                ));
        });
    }
    
    public function editAttachment(attachment:DocumentAttachment):void
    {
        //The main goal - do not change attachment properties until we don't save them to DB

        var popup:AttachmentEditorView = AttachmentEditorView.open(app, true);
        popup.attachment = DocumentAttachment(ObjectUtil.copy(attachment));

        popup.addEventListener("commit", function ():void
        {
            if (popup.attachment.IsPdfCopy() && document.PdfCopy && document.PdfCopy != attachment)
            {
                Alert.show("The PDF Copy of the Document is already specified");
                return;
            }

            var token:AsyncToken = ttService.service.UpdateDocumentAttachment(popup.attachment);
            token.addResponder(new TokenResponder(
                function (event:ResultEvent):void 
                {
                    attachment.DocumentAttachmentTypeId = popup.attachment.DocumentAttachmentTypeId;
                    attachment.Description = popup.attachment.Description;

                    document.dispatchEvent(new PropertyChangeEvent(
                        PropertyChangeEvent.PROPERTY_CHANGE, false, false, 
                        PropertyChangeEventKind.UPDATE, "PdfCopy"));

                    popup.close();
                },
                "Unable to Update Attachment"
                ));
        });
    }
    
    public function deleteAttachment(attachment:DocumentAttachment):void
    {
        var token:AsyncToken = ttService.service.DeleteDocumentAttachment(
            attachment, AppModel.getInstance().user.UserId);

        token.addResponder(new TokenResponder(
            function (event:ResultEvent):void 
            {
                var itemIndex:int = document.AttachmentsList.getItemIndex(attachment);
                document.AttachmentsList.removeItemAt(itemIndex);

                document.dispatchEvent(new PropertyChangeEvent(
                    PropertyChangeEvent.PROPERTY_CHANGE, false, false, 
                    PropertyChangeEventKind.UPDATE, "PdfCopy"));
            },
            "Unable to Delete Attachment"
            )
        );
    }
}

}