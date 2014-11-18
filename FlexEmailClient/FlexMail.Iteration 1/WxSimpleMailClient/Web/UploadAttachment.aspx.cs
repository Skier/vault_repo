using System;
using System.Collections.Generic;
using System.Web;
using Weborb.Samples.Email;
using FileInfo=Weborb.Samples.Email.Entities.FileInfo;

public partial class UploadAttachment : System.Web.UI.Page 
{
    
    private const string MESSAGE_UID_PARAM = "uid";

    protected void Page_Load(object sender, EventArgs e) {
        ProcessRequest();
    }
    
    private void ProcessRequest() {
        if (Request.Files.Count > 0) {
            string messageUid = UidParamValue;

            EmailClient emailClient = EmailClient.SessionInstance;
            
            if (null == emailClient) {
                throw new InvalidOperationException("Invalid application state.");
            }
            
            List<FileInfo> messageFileList = (List<FileInfo>) emailClient.uploadedFilesMap[messageUid];
            
            if (null == messageFileList) {
                messageFileList = new List<FileInfo>();
            }
            
            foreach (string key in Request.Files.Keys) {
                HttpPostedFile file = Request.Files[key];

                if (file.ContentLength > 0) {
                    messageFileList.Add(new FileInfo(file));
                }
            }
            
            emailClient.uploadedFilesMap[messageUid] = messageFileList;
        }
    }

    #region Properties

    private string UidParamValue {
        get {
            string result = Request.Params[MESSAGE_UID_PARAM];
    
            if (null == result || result.Length == 0) {
                throw new ArgumentException("Parameter [" + MESSAGE_UID_PARAM + "] must contain a value.");
            }
            return result;
        }
    }

    #endregion

}

