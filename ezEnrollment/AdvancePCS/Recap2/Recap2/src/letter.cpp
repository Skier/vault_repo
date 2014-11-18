#include "client/letter.h"

wxString& DefaultLetter::GetFromAddress()
{
    return *IGetFromAddress();
};

wxString& DefaultLetter::GetToAddress()
{
    return *IGetToAddress();
};

wxString& DefaultLetter::GetSubject()
{
    return *IGetSubject();
};

wxString& DefaultLetter::GetBody()
{
    return *IGetBody();
};

bool DefaultLetter::HasAttachment()
{
    return (NULL != IGetAttachment());
};

wxString& DefaultLetter::GetAttachment()
{
    return *IGetAttachment();
};

wxString& DefaultLetter::GetAttachmentBody()
{
    return *m_attachmentBody;
};

void DefaultLetter::SetFromAddress(wxString& fromAddress)
{
    if ( !IGetFromAddress() ) {
        m_fromAddress = new wxString(fromAddress);
    } else {
        IGetFromAddress()->Clear();
	*IGetFromAddress() << fromAddress;
    }
};

void DefaultLetter::SetToAddress(wxString& toAddress)
{
    if ( !IGetToAddress() ) {
        m_toAddress = new wxString(toAddress);
    } else {
        IGetToAddress()->Clear();
	*IGetToAddress() << toAddress;
    }
};

void DefaultLetter::SetSubject(wxString& subject)
{
    if ( !IGetSubject() ) {
        m_subject = new wxString(subject);
    } else {
        IGetSubject()->Clear();
	*IGetSubject() << subject;
    }
};

void DefaultLetter::SetBody(wxString& body)
{
    if ( !IGetBody() ) {
        m_body = new wxString(body);
    } else {
        IGetBody()->Clear();
	*IGetBody() << body;
    }
};

void DefaultLetter::Attach(wxString& filename, wxString& body)
{
    if ( !IGetAttachment() ) {
        m_attachment = new wxString(filename);
    } else {
        IGetAttachment()->Clear();
	*IGetAttachment() << filename;
    }
    *m_attachmentBody << body;
};

    
