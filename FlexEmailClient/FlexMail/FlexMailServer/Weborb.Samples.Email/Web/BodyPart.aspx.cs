using System;
using System.Text;
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
    
    private const string BODY_HEADER_TEMPLATE = 
        @"<table style=""font-family: 'Courier New', Courier, monospace; background: #FFEFD5; width: 100%; border: 1px solid #FFCE84;"">
        <tr><td align=right>From :</td><td width=100%>{0}</td></tr>
        <tr><td align=right>To :</td><td width=100%>{1}</td></tr>
        <tr><td align=right>Cc :</td><td width=100%>{2}</td></tr>
        <tr><td align=right>Date :</td><td width=100%>{3}</td></tr>
        <tr><td align=right>Subject :</td width=100%><td>{4}</td></tr>
        </table>
        <hr style=""border: 1px solid #6495ED;"">";
    
    protected void Page_Load(object sender, EventArgs e) {
        
        ProcessRequest();
        
        Response.End();
    }
    
    private void ProcessRequest() {
        
        string messageUID = UidParamValue;
        string partName = NameParamValue;
        string partType = TypeParamValue;
        
        FlexMailServer flexMailServer = FlexMailServer.SessionInstance;
        
        if (null == flexMailServer) {
            throw new InvalidOperationException("Invalid application state.");
        }
        
        MessageInfo message = (MessageInfo) flexMailServer.retrievedMessages[messageUID];

        if (null == message || null == message.Body) {
            throw new InvalidOperationException(
                string.Format("Invalid application state. messageUID = {0}, partName = {1}", messageUID, partName));
        }
        
        switch(partType) {
                
            case BODY_PART_TYPE_ATTACHMENT:

                FileInfo att = message.Body.GetAttachmentByName(partName);
                
                Response.Clear();
                att.Content.WriteTo(Response.OutputStream);
                
                break;
                
            case BODY_PART_TYPE_VIEW:
                FileInfo body = message.Body.HtmlBody;
                
                string sBody = Encoding.ASCII.GetString(body.Content.ToArray());
                string sHeader = CreateBodyHeader(message);
                
                int htmlBodyPosition = sBody.IndexOf("<body>", StringComparison.OrdinalIgnoreCase);
                
                if (htmlBodyPosition != -1) {
                    sBody = sBody.Insert(htmlBodyPosition + "<body>".Length, sHeader);
                } else {
                    sBody = sBody.Insert(0, sHeader);
                }
                
                Response.Write(sBody);
                
                break;
                
            default:
                throw new Exception("Unknown body part type");
        }
    }

    private string CreateBodyHeader(MessageInfo message) {
        
        return string.Format(BODY_HEADER_TEMPLATE,
              Server.HtmlEncode(message.From.DisplayValue),
              Server.HtmlEncode(AddressListToString(message.To)),
              Server.HtmlEncode(AddressListToString(message.Cc)),
              message.Sent.ToString("r"),
              Server.HtmlEncode(message.Subject));
    }
    
    private string AddressListToString(EmailAddressInfo[] list) {
        string result = "";
        
        if (null != list && list.Length > 0) {
            for (int i = 0; i < list.Length; i++) {
                result += list[i].DisplayValue;
                
                if (i != list.Length - 1) {
                    result += ", ";
                }
                        }
        }
        
        return result;
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