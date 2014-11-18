using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Weborb.Samples.Email.Pop3
{
    public class MimeParser
    {
        /// <summary>
        /// indicates the reason how a MIME entity processing has terminated
        /// </summary>
        private enum MimeEntityReturnCode
        {
            undefined = 0, //meaning like null
            bodyComplete, //end of message line found
            parentBoundaryStartFound,
            parentBoundaryEndFound,
            problem //received message doesn't follow MIME specification
        }
        
        //character array 'constants' used for analysing POP3 / MIME
        //----------------------------------------------------------
        private static char[] BracketChars = {'(', ')'};
        private static char[] CommaChars = {','};
        private static char[] WhiteSpaceChars = {' ', '\t'};

        private static StringReader _contentReader;
        
        private static IFormatProvider _culture = new CultureInfo("en-US", true);
        
        //buffer used by every ProcessMimeEntity() to store  MIME entity
        private static StringBuilder _mimeEntitySB;
        
        public static RxMailMessage Parse(string rawContent, bool headerOnly) {
            _contentReader = new StringReader(rawContent);
            _mimeEntitySB = new StringBuilder(100000);
            
            RxMailMessage message = new RxMailMessage();
            
            message.ContentTransferEncoding = TransferEncoding.SevenBit;
            message.TransferType = "7bit";

            if (headerOnly) {
                ProcessMimeHeaderEntity(message);
            } else {
                ProcessMimeEntity(message, "");
            }
            
            return message;
        }
        
        /// <summary>
        /// Process a MIME entity. Optimized for header only processing
        /// </summary>
        private static void ProcessMimeHeaderEntity(RxMailMessage message) {

            string completeHeaderField = null; //consists of one start line and possibly several continuation lines
            string headerLine;

            // read header lines until empty line is found (end of header)
            while (true) {
                headerLine = _contentReader.ReadLine();
                
                if (headerLine.Length < 1) {
                    //empty line found => end of header
                    if (completeHeaderField != null) {
                        ProcessHeaderField(message, completeHeaderField);
                    }
                    
                    return;
                }

                //read header field
                //one header field can extend over one start line and multiple continuation lines
                //a continuation line starts with at least 1 blank (' ') or tab
                if (headerLine[0] == ' ' || headerLine[0] == '\t') {
                    
                    //continuation line found.
                    if (completeHeaderField == null) {
                        
                        return;
                        
                    } else {
                        // append space, if needed, and continuation line
                        if (completeHeaderField[completeHeaderField.Length - 1] != ' ') {
                            //previous line did not end with a whitespace
                            //need to replace CRLF with a ' '
                            completeHeaderField += ' ' + headerLine.TrimStart(WhiteSpaceChars);
                        } else {
                            //previous line did end with a whitespace
                            completeHeaderField += headerLine.TrimStart(WhiteSpaceChars);
                        }
                    }
                } else {
                    //a new header field line found
                    if (completeHeaderField == null) {
                        //very first field, just copy it and then check for continuation lines
                        completeHeaderField = headerLine;
                    } else {
                        //new header line found
                        ProcessHeaderField(message, completeHeaderField);

                        //save the beginning of the next line
                        completeHeaderField = headerLine;
                    }
                }
            } //end while read header lines
        }
        
        /// <summary>
        /// Process a MIME entity
        /// 
        /// A MIME entity consists of header and body.
        /// Separator lines in the body might mark children MIME entities
        /// </summary>
        
        private static MimeEntityReturnCode ProcessMimeEntity(RxMailMessage message, string parentBoundaryStart) {
            
            bool hasParentBoundary = parentBoundaryStart.Length > 0;
            string parentBoundaryEnd = parentBoundaryStart + "--";
            MimeEntityReturnCode boundaryMimeReturnCode;

            //some format fields are inherited from parent, only the default for
            //ContentType needs to be set here, otherwise the boundary parameter would be
            //inherited too !
            message.SetContentTypeFields("text/plain; charset=us-ascii");

            string completeHeaderField = null; //consists of one start line and possibly several continuation lines
            string headerLine;

            // read header lines until empty line is found (end of header)
            while (true) {
                headerLine = _contentReader.ReadLine();
                
                if (headerLine.Length < 1) {
                    
                    //empty line found => end of header
                    if (completeHeaderField != null) {
                        ProcessHeaderField(message, completeHeaderField );
                    }
                    
                    break;
                }

                //check if there is a parent boundary in the header (wrong format!)
                if (hasParentBoundary && IsParentBoundaryFound(headerLine, parentBoundaryStart, parentBoundaryEnd, out boundaryMimeReturnCode)) {
                    return boundaryMimeReturnCode;
                }
                
                //read header field
                //one header field can extend over one start line and multiple continuation lines
                //a continuation line starts with at least 1 blank (' ') or tab
                if (headerLine[0] == ' ' || headerLine[0] == '\t') {
                    
                    //continuation line found.
                    if (completeHeaderField == null) {
                        
                        return MimeEntityReturnCode.problem;
                        
                    } else {
                        
                        // append space, if needed, and continuation line
                        if (completeHeaderField[completeHeaderField.Length - 1] != ' ') {
                            //previous line did not end with a whitespace
                            //need to replace CRLF with a ' '
                            completeHeaderField += ' ' + headerLine.TrimStart(WhiteSpaceChars);
                        } else {
                            //previous line did end with a whitespace
                            completeHeaderField += headerLine.TrimStart(WhiteSpaceChars);
                        }
                        
                    }
                    
                } else {
                    
                    //a new header field line found
                    if (completeHeaderField == null) {
                        
                        //very first field, just copy it and then check for continuation lines
                        completeHeaderField = headerLine;
                        
                    } else {
                        
                        //new header line found
                        ProcessHeaderField(message, completeHeaderField );

                        //save the beginning of the next line
                        completeHeaderField = headerLine;
                        
                    }
                    
                }
            } //end while read header lines

            //process body
            //------------

            //empty StringBuilder. For speed reasons, reuse StringBuilder defined as member of class
            _mimeEntitySB.Length = 0;
            
            string BoundaryDelimiterLineStart = null;
            bool isBoundaryDefined = false;
            
            if (message.ContentType.Boundary != null) {
                isBoundaryDefined = true;
                BoundaryDelimiterLineStart = "--" + message.ContentType.Boundary;
            }
            
            //prepare return code for the case there is no boundary in the body
            boundaryMimeReturnCode = MimeEntityReturnCode.bodyComplete;

            //read body lines
            while ((headerLine = _contentReader.ReadLine()) != null) {
                
                //check if there is a boundary line from this entity itself in the body
                if (isBoundaryDefined && headerLine.TrimEnd() == BoundaryDelimiterLineStart) {
                    
                    //boundary line found. stop the processing here and start a delimited body processing
                    return ProcessDelimitedBody(message, BoundaryDelimiterLineStart, parentBoundaryStart, parentBoundaryEnd);
                    
                }

                //check if there is a parent boundary in the body
                if (hasParentBoundary && IsParentBoundaryFound(headerLine, parentBoundaryStart, parentBoundaryEnd, out boundaryMimeReturnCode)) {
                    
                    //a parent boundary is found. Decode the content of the body received so far, then end this MIME entity
                    //note that boundaryMimeReturnCode is set here, but used in the return statement
                    break;
                    
                }

                //process next line
                _mimeEntitySB.Append(headerLine + Environment.NewLine);
            }

            //a complete MIME body read
            //convert received US ASCII characters to .NET string (Unicode)
            string TransferEncodedMessage = _mimeEntitySB.ToString();
            bool isAttachmentSaved = false;
            
            switch (message.ContentTransferEncoding) {
                    
                case TransferEncoding.SevenBit:
                    //nothing to do
                    saveMessageBody(message, TransferEncodedMessage);
                    break;

                case TransferEncoding.Base64:
                    //convert base 64 -> byte[]
                    byte[] bodyBytes = Convert.FromBase64String(TransferEncodedMessage);
                    message.ContentStream = new MemoryStream(bodyBytes);

                    if (message.MediaMainType == "text") {
                        //convert byte[] -> string
                        message.Body = DecodeByteArryToString(bodyBytes, message.BodyEncoding);
                    } else if (message.MediaMainType == "image" || message.MediaMainType == "application") {
                        SaveAttachment(message);
                        isAttachmentSaved = true;
                    }
                    
                    break;

                case TransferEncoding.QuotedPrintable:
                    saveMessageBody(message, QuotedPrintable.Decode(TransferEncodedMessage));
                    break;

                default:
                    saveMessageBody(message, TransferEncodedMessage);
                    //no need to raise a warning here, the warning was done when analising the header
                    break;
            }

            if (message.ContentDisposition != null &&
                message.ContentDisposition.DispositionType.ToLowerInvariant() == "attachment" && !isAttachmentSaved) {
                SaveAttachment(message);
            }
            
            return boundaryMimeReturnCode;
        }


        /// <summary>
        /// Check if the response line received is a parent boundary 
        /// </summary>
        private static bool IsParentBoundaryFound(string response, string parentBoundaryStart, string parentBoundaryEnd,
                                         out MimeEntityReturnCode boundaryMimeReturnCode) {
            boundaryMimeReturnCode = MimeEntityReturnCode.undefined;
            if (response == null || response.Length < 2 || response[0] != '-' || response[1] != '-') {
                //quick test: reponse doesn't start with "--", so cannot be a separator line
                return false;
            }
            
            if (response == parentBoundaryStart) {
                
                boundaryMimeReturnCode = MimeEntityReturnCode.parentBoundaryStartFound;
                return true;
                
            } else if (response == parentBoundaryEnd) {
                
                boundaryMimeReturnCode = MimeEntityReturnCode.parentBoundaryEndFound;
                return true;
                
            }
            
            return false;
        }


        /// <summary>
        /// Convert one MIME header field and update message accordingly
        /// </summary>
        private static void ProcessHeaderField(RxMailMessage message, string headerLine) {
            // lets ignore it asap.

            int separatorPosition = headerLine.IndexOf(':');

            // if header field type not found, do nothing            
            if (separatorPosition < 1) {
                return;
            } 
                
            //process header field type
            string headerLineType = headerLine.Substring(0, separatorPosition).ToLowerInvariant();
            string headerLineContent = headerLine.Substring(separatorPosition + 1).Trim(WhiteSpaceChars);

            //1 of the 2 parts missing, do nothing            
            if (headerLineType == "" || headerLineContent == "") {
                return;
            }

            //interpret if possible
            switch (headerLineType.ToLower()) {
                case "bcc":
                    AddMailAddresses(headerLineContent, message.Bcc);
                    break;
                    
                case "cc":
                    AddMailAddresses(headerLineContent, message.CC);
                    break;
                    
                case "content-description":
                    message.ContentDescription = headerLineContent;
                    break;
                    
                case "content-disposition":
                    message.ContentDisposition = new ContentDisposition(headerLineContent);
                    break;
                    
                case "content-id":
                    message.ContentId = headerLineContent;
                    break;
                    
                case "content-transfer-encoding":
                    message.TransferType = headerLineContent;
                    message.ContentTransferEncoding = ConvertToTransferEncoding(headerLineContent);
                    break;
                    
                case "content-type":
                    message.SetContentTypeFields(headerLineContent);
                    break;
                    
                case "date":
                    message.DeliveryDate = ConvertToDateTime(headerLineContent);
                    break;
                    
                case "delivered-to":
                    message.DeliveredTo = ConvertToMailAddress(headerLineContent);
                    break;
                    
                case "from":
                    MailAddress address = ConvertToMailAddress(headerLineContent);
                    if (address != null) {
                        message.From = address;
                    }
                    
                    break;
                    
                case "message-id":
                    message.MessageId = headerLineContent;
                    break;
                    
                case "mime-version":
                    message.MimeVersion = headerLineContent;
                    break;
                    
                case "sender":
                    message.Sender = ConvertToMailAddress(headerLineContent);
                    break;
                    
                case "subject":
                    message.Subject = headerLineContent;
                    break;

                case "references":
                    if (null == message.InReplyTo || message.InReplyTo.Length == 0)
                        message.InReplyTo = headerLineContent;
                    break;
                    
                case "in-reply-to":
                    message.InReplyTo = headerLineContent;
                    break;

                case "to":
                    AddMailAddresses(headerLineContent, message.To);
                    break;
                    
                default:
                    break;
            }
           
        }


        /// <summary>
        /// find individual addresses in the string and add it to address collection
        /// </summary>
        /// <param name="addresses">string with possibly several email addresses</param>
        /// <param name="addressCollection">parsed addresses</param>
        private static void AddMailAddresses(string addresses, MailAddressCollection addressCollection) {
            MailAddress item;
            
            string[] addressSplit = addresses.Split(',');
            
            foreach (string adrString in addressSplit) {
                
                item = ConvertToMailAddress(adrString);
                
                if (item != null) {
                    addressCollection.Add(item);
                }
            }
        }


        /// <summary>
        /// Tries to convert a string into an email address
        /// </summary>
        private static MailAddress ConvertToMailAddress(string address) {
            MailAddress result = null;
            
            try {
                result = new MailAddress(address.Trim());
            } catch {}
            
            return result;
        }


        /// <summary>
        /// Tries to convert string to date, following POP3 rules
        /// If there is a run time error, the smallest possible date is returned
        /// <example>Wed, 04 Jan 2006 07:58:08 -0800</example>
        /// </summary>
        public static DateTime ConvertToDateTime(string date) {
            DateTime result = DateTime.MinValue;
            
            //sample; 'Wed, 04 Jan 2006 07:58:08 -0800 (PST)'
            //remove day of the week before ','
            //remove date zone in '()', -800 indicates the zone already

            //remove day of week
            string cleanDateTime = date;
            
            string[] DateSplit = cleanDateTime.Split(CommaChars, 2);
            
            if (DateSplit.Length > 1) {
                cleanDateTime = DateSplit[1];
            }

            //remove time zone (PST)
            DateSplit = cleanDateTime.Split(BracketChars);
            
            if (DateSplit.Length > 1) {
                cleanDateTime = DateSplit[0];
            }

            DateTimeStyles styles = DateTimeStyles.AdjustToUniversal | DateTimeStyles.AllowWhiteSpaces;
            
            //convert to DateTime
            if (!DateTime.TryParse( cleanDateTime, _culture, styles, out result)) {
                
                //try just to convert the date
                int DateLength = cleanDateTime.IndexOf(':') - 3;
                cleanDateTime = cleanDateTime.Substring(0, DateLength);

                DateTime.TryParse(cleanDateTime, _culture, styles, out result);
            }
            
            return result;
        }

        /// <summary>
        /// converts TransferEncoding as defined in the RFC into a .NET TransferEncoding
        /// 
        /// .NET doesn't know the type "bit8". It is translated here into "bit7", which
        /// requires the same kind of processing (none).
        /// </summary>
        /// <param name="TransferEncodingString"></param>
        /// <returns></returns>
        private static TransferEncoding ConvertToTransferEncoding(string TransferEncodingString) {
            // here, "bit8" is marked as "bit7" (i.e. no transfer encoding needed)
            // "binary" is illegal in SMTP
            // something like "7bit" / "8bit" / "binary" / "quoted-printable" / "base64"
            
            switch (TransferEncodingString.Trim().ToLowerInvariant()) {
                case "8bit":
                case "binary":
                case "7bit":                    
                    return TransferEncoding.SevenBit;
                case "quoted-printable":
                    return TransferEncoding.QuotedPrintable;
                case "base64":
                    return TransferEncoding.Base64;
                default:
                    return TransferEncoding.Unknown;
            }
        }


        /// <summary>
        /// Copies the content found for the MIME entity to the RxMailMessage body and creates
        /// a stream which can be used to create attachements, alternative views, ...
        /// </summary>
        private static void saveMessageBody(RxMailMessage message, string contentString) {
            message.Body = contentString;
            MemoryStream bodyStream = new MemoryStream();
            StreamWriter bodyStreamWriter = new StreamWriter(bodyStream);
            bodyStreamWriter.Write(contentString);
//            int l = contentString.Length;
            bodyStreamWriter.Flush();
            message.ContentStream = bodyStream;
        }


        /// <summary>
        /// each attachement is stored in its own MIME entity and read into this entity's
        /// ContentStream. SaveAttachment creates an attachment out of the ContentStream
        /// and attaches it to the parent MIME entity.
        /// </summary>
        private static void SaveAttachment(RxMailMessage message) {
            if (message.Parent == null) {
                Debugger.Break(); //didn't have a sample email to test this
            } else {
                Attachment thisAttachment = new Attachment(message.ContentStream, message.ContentType);
                thisAttachment.ContentId = ".";
                //no idea why ContentDisposition is read only. on the other hand, it is anyway redundant
                if (message.ContentDisposition != null) {
                    ContentDisposition messageContentDisposition = message.ContentDisposition;
                    ContentDisposition AttachmentContentDisposition = thisAttachment.ContentDisposition;
                    if (messageContentDisposition.CreationDate > DateTime.MinValue) {
                        AttachmentContentDisposition.CreationDate = messageContentDisposition.CreationDate;
                    }
                    AttachmentContentDisposition.DispositionType = messageContentDisposition.DispositionType;
                    AttachmentContentDisposition.FileName = messageContentDisposition.FileName;
                    AttachmentContentDisposition.Inline = messageContentDisposition.Inline;
                    if (messageContentDisposition.ModificationDate > DateTime.MinValue) {
                        AttachmentContentDisposition.ModificationDate = messageContentDisposition.ModificationDate;
                    }
                    AttachmentContentDisposition.Parameters.Clear();
                    if (messageContentDisposition.ReadDate > DateTime.MinValue) {
                        AttachmentContentDisposition.ReadDate = messageContentDisposition.ReadDate;
                    }
                    if (messageContentDisposition.Size > 0) {
                        AttachmentContentDisposition.Size = messageContentDisposition.Size;
                    }
                    foreach (string key in messageContentDisposition.Parameters.Keys) {
                        AttachmentContentDisposition.Parameters.Add(key, messageContentDisposition.Parameters[key]);
                    }
                }

                //get ContentId
                string contentIdString = message.ContentId;
                if (contentIdString != null) {
                    thisAttachment.ContentId = RemoveBrackets(contentIdString);
                }
                thisAttachment.TransferEncoding = message.ContentTransferEncoding;
                message.Parent.Attachments.Add(thisAttachment);
            }
        }


        /// <summary>
        /// removes leading '&lt;' and trailing '&gt;' if both exist
        /// </summary>
        private static string RemoveBrackets(string parameterString) {
            if (parameterString == null) {
                return null;
            }
            if (parameterString.Length < 1 ||
                parameterString[0] != '<' ||
                parameterString[parameterString.Length - 1] != '>') {
                Debugger.Break(); //didn't have a sample email to test this
                return parameterString;
            } else {
                return parameterString.Substring(1, parameterString.Length - 2);
            }
        }


        private static MimeEntityReturnCode ProcessDelimitedBody(RxMailMessage message, string BoundaryStart, string parentBoundaryStart, string parentBoundaryEnd) {
            
            if (BoundaryStart.Trim() == parentBoundaryStart.Trim()) {
                
                //Wrong format - mime entity boundaries have to be unique. Emtpy this message
                _contentReader.ReadToEnd();
                
                return MimeEntityReturnCode.problem;
            }

            MimeEntityReturnCode ReturnCode;
            do {
                //empty StringBuilder
                _mimeEntitySB.Length = 0;
                
                RxMailMessage ChildPart = message.CreateChildEntity();

                //recursively call MIME part processing
                ReturnCode = ProcessMimeEntity(ChildPart, BoundaryStart);

                if (ReturnCode == MimeEntityReturnCode.problem) {
                    //it seems the received email doesn't follow the MIME specification. Stop here
                    return MimeEntityReturnCode.problem;
                }

                //add the newly found child MIME part to the parent
                AddChildPartsToParent(ChildPart, message);
            } while (ReturnCode != MimeEntityReturnCode.parentBoundaryEndFound);

            //disregard all future lines until parent boundary is found or end of complete message
            MimeEntityReturnCode boundaryMimeReturnCode;
            bool hasParentBoundary = parentBoundaryStart.Length > 0;
            
            string contentLine;
            
            while ((contentLine = _contentReader.ReadLine()) != null) {
                
                if (hasParentBoundary &&
                    IsParentBoundaryFound(contentLine, parentBoundaryStart, parentBoundaryEnd, out boundaryMimeReturnCode)) {
                    return boundaryMimeReturnCode;
                }
                
            }
            
            return MimeEntityReturnCode.bodyComplete;
        }


        /// <summary>
        /// Add all attachments and alternative views from child to the parent
        /// </summary>
        private static void AddChildPartsToParent(RxMailMessage child, RxMailMessage parent) {
            //add the child itself to the parent
            parent.Entities.Add(child);

            //add the alternative views of the child to the parent
            if (child.AlternateViews != null) {
                foreach (AlternateView childView in child.AlternateViews) {
                    parent.AlternateViews.Add(childView);
                }
            }

            //add the body of the child as alternative view to parent
            //this should be the last view attached here, because the POP 3 MIME client
            //is supposed to display the last alternative view
            if (child.MediaMainType == "text" && child.ContentStream != null &&
                child.Parent.ContentType != null &&
                child.Parent.ContentType.MediaType.ToLowerInvariant() == "multipart/alternative") {
                AlternateView thisAlternateView = new AlternateView(child.ContentStream);
                thisAlternateView.ContentId = RemoveBrackets(child.ContentId);
                thisAlternateView.ContentType = child.ContentType;
                thisAlternateView.TransferEncoding = child.ContentTransferEncoding;
                parent.AlternateViews.Add(thisAlternateView);
            }

            //add the attachments of the child to the parent
            if (child.Attachments != null) {
                foreach (Attachment childAttachment in child.Attachments) {
                    parent.Attachments.Add(childAttachment);
                }
            }
        }


        /// <summary>
        /// Converts byte array to string, using decoding as requested
        /// </summary>
        private static string DecodeByteArryToString(byte[] ByteArry, Encoding ByteEncoding) {
            
            if (ByteArry == null) {
                return null;
            }
            
            Decoder byteArryDecoder;
            
            if (ByteEncoding == null) {
                //no encoding indicated. Let's try UTF7
                byteArryDecoder = Encoding.UTF7.GetDecoder();
            } else {
                byteArryDecoder = ByteEncoding.GetDecoder();
            }
            
            int charCount = byteArryDecoder.GetCharCount(ByteArry, 0, ByteArry.Length);
            char[] bodyChars = new Char[charCount];
            
            byteArryDecoder.GetChars(ByteArry, 0, ByteArry.Length, bodyChars, 0);
            
            return new string(bodyChars);
        }
    }
}