using System;
using Weborb.Samples.Email;
using Weborb.Samples.Email.Entities;
using FileInfo=Weborb.Samples.Email.Entities.FileInfo;

namespace Weborb.Samples.Email.Web
{
    
public class BodyPart : System.Web.UI.Page 
{
    
    private const string MESSAGE_UID_PARAM = "uid";
    private const string BODY_PART_NAME_PARAM = "name";
    private const string BODY_PART_TYPE_PARAM = "type";
    
    private const string BODY_PART_TYPE_ATTACHMENT = "a";
    private const string BODY_PART_TYPE_VIEW = "v";

    protected void Page_Load(object sender, EventArgs e) {
        
        ProcessRequest();
        
        Response.End();
    }
    
    private void ProcessRequest() {
        
        string messageUID = UidParamValue;
        string partName = NameParamValue;
        string partType = TypeParamValue;
        
        EmailClient emailClient = EmailClient.SessionInstance;
        
        if (null == emailClient) {
            throw new InvalidOperationException("Invalid application state.");
        }
        
        MessageBodyInfo body = (MessageBodyInfo) emailClient.messageBodyMap[messageUID];

        if (null == body) {
            throw new InvalidOperationException(string.Format("Invalid application state. messageUID = {0}, partName = {1}", messageUID, partName));
        }
        
        switch(partType) {
                
            case BODY_PART_TYPE_ATTACHMENT:
                Response.Clear();
                FileInfo att = body.GetAttachmentByName(partName);
                att.Content.WriteTo(Response.OutputStream);                    
                break;
                
            case BODY_PART_TYPE_VIEW:
                ViewInfo view = body.GetViewByName(partName);
                
                if (view.ContentType == "text/plain") {
                    
                    Response.Write("<HTML><BODY><FONT face=\"Courier New\" size=2>");
                    view.Content.WriteTo(Response.OutputStream);
                    Response.Write("</FONT></HTML></BODY>");
                    
                } else {
                    view.Content.WriteTo(Response.OutputStream);
                }

                break;
                
            default:
                throw new Exception("Unknown body part type");
        }
    }

    #region Properties

    private string UidParamValue {
        get {
            string result = Request.Params[MESSAGE_UID_PARAM];
    
            if (null == result || result.Length == 0) {
                throw new ArgumentException("Parameter [" + MESSAGE_UID_PARAM + "] must contain a value.");
            }
            result = result.Replace("plus", "+");
            return result;
        }
    }

    private string NameParamValue {
        get {
            string result = Request.Params[BODY_PART_NAME_PARAM];
        
            if (null == result || result.Length == 0) {
                throw new ArgumentException("Parameter [" + BODY_PART_NAME_PARAM + "] must contain a value.");
            }
            return result;
        }
    }

    private string TypeParamValue {
        get {
            string result = Request.Params[BODY_PART_TYPE_PARAM];
        
            if (null == result || result.Length == 0) {
                throw new ArgumentException("Parameter [" + BODY_PART_TYPE_PARAM + "] must contain a value.");
            }
            return result;
        }
    }    
    
    #endregion

}

}