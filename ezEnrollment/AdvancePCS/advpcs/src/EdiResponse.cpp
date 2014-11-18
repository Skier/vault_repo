/*
 *  $RCSfile: EdiResponse.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#include "wx/wxprec.h"

#ifdef __BORLANDC__
    #pragma hdrstop
#endif

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif

/* -------------------------- header place ---------------------------------- */
#include <wx/datetime.h>
#include <atf/XmlDocument.h>
#include <atf/XmlNode.h>
#include <advpcs/EdiResponse.h>
#include <advpcs/EdiStatus.h>
#include <advpcs/AgentException.h>
#include <advpcs/Resources.h>
/* -------------------------- implementation place -------------------------- */
EdiResponse::EdiResponse(CXmlDocument* doc) 
    : m_doc(doc), m_code(0), m_current(NULL)
{

    wxASSERT(NULL != m_doc);

    CXmlNode* root = m_doc->GetRoot();
    if ( NULL == root ) {
        THROW_AGENT_EXCEPTION(ADVPCS_REPLY_FORMAT_ERR);
    }
    if ( 0 != root->GetName().CompareNoCase(ADVPCS_RESP_TAG) ) {
        THROW_AGENT_EXCEPTION(ADVPCS_REPLY_FORMAT_ERR);
    }
    CXmlNode* status = root->GetChild(ADVPCS_RESP_STATUS_TAG.c_str());
    if ( NULL == status ) {
        THROW_AGENT_EXCEPTION(ADVPCS_REPLY_FORMAT_ERR);
    }
    CXmlNode * code = status->GetChild(ADVPCS_RESP_CODE_TAG.c_str());
    if ( NULL == code ) {
        THROW_AGENT_EXCEPTION(ADVPCS_REPLY_FORMAT_ERR);
    }

    wxString num = code->GetContent();
    if ( ! num.IsNumber() ) {
        THROW_AGENT_EXCEPTION(ADVPCS_REPLY_FORMAT_ERR);
    }

    CXmlNode * msg = status->GetChild(ADVPCS_RESP_MESSAGE_TAG.c_str());
    if ( NULL == msg ) {
        THROW_AGENT_EXCEPTION(ADVPCS_REPLY_FORMAT_ERR);
    }
    m_message = msg->GetContent();

    if ( !num.ToLong(&m_code) ) {
        THROW_AGENT_EXCEPTION(ADVPCS_REPLY_FORMAT_ERR);
    }

};

EdiResponse::~EdiResponse() {
    if ( NULL != m_doc ) {
        wxDELETE(m_doc);
    }
};

EdiStatus* EdiResponse::GetNext() {
    wxASSERT( NULL != m_doc );

    if ( NULL == m_current ) {
        m_current = m_doc->GetRoot()->GetChild(ADVPCS_RESP_STATUS_HISTORY_TAG.c_str());
    } else {
        m_current = m_current->GetNext();
    };

    EdiStatus* result = NULL;
    if ( NULL != m_current ) {

        wxString userid   = GetNodeBody(m_current, ADVPCS_RESP_STATUS_USERID_TAG);
        wxString fileName = GetNodeBody(m_current, ADVPCS_RESP_STATUS_FILENAME_TAG);
        wxString fileSize = GetNodeBody(m_current, ADVPCS_RESP_STATUS_FILESIZE_TAG);
        wxString datetime = GetNodeBody(m_current, ADVPCS_RESP_STATUS_DATETIME_TAG);
        wxString tracking = GetNodeBody(m_current, ADVPCS_RESP_STATUS_TRACKINGREF_TAG);
        wxString recCount = GetNodeBody(m_current, ADVPCS_RESP_STATUS_RECORDCOUNT_TAG);
        wxString status   = GetNodeBody(m_current, ADVPCS_RESP_STATUS_STATUS_TAG);

        result = new EdiStatus(userid, fileName, fileSize, datetime, tracking, recCount, status);
    };

    return result;
};

void EdiResponse::Reset() {
    m_current = NULL;
};


wxString EdiResponse::GetNodeBody(CXmlNode* owner, wxString tag) {
    CXmlNode* node = owner->GetChild(tag.c_str());
    if ( NULL == node ) {
        THROW_AGENT_EXCEPTION(ADVPCS_REPLY_FORMAT_ERR);
    }
    return wxString(node->GetContent());
};  
