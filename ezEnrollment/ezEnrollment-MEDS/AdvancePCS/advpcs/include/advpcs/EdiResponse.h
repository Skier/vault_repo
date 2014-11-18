/*
 *  $RCSfile: EdiResponse.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __ADVPCS_EDI_RESPONSE_H__
#define __ADVPCS_EDI_RESPONSE_H__

/* -------------------------- header place ---------------------------------- */
#include <advpcs/EdiStatusStream.h>
/* -------------------------- implementation place -------------------------- */

class CXmlDocument;
class CXmlNode;

class EdiResponse : public EdiStatusStream {
public:
    EdiResponse(CXmlDocument* doc);
    ~EdiResponse();

    virtual EdiStatus* GetNext();
    virtual void Reset();

    long GetCode() const { return m_code; };
    wxString GetMessage() const { return m_message; };

protected:
    wxString GetNodeBody(CXmlNode* owner, wxString tag);  

private:
    long          m_code;
    wxString      m_message;
    CXmlDocument* m_doc;
    CXmlNode*     m_current;
};

#endif /* __ADVPCS_EDI_RESPONSE_H__ */
