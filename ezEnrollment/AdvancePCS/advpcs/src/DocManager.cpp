/*
 *  $RCSfile: DocManager.cpp,v $
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
#include <wx/filename.h>
#include <atf/XmlNode.h>
#include <atf/CfgException.h>
#include <advpcs/XmlDescriptor.h>
#include <advpcs/Document.h>
#include <advpcs/Composer.h>
#include <advpcs/DocManager.h>
#include <advpcs/EdiDocument.h>
#include <advpcs/CsvComposer.h>
#include <advpcs/EdiComposer.h>
#include <advpcs/Resources.h>
/* -------------------------- implementation place -------------------------- */


DocManager::DocManager(const CXmlNode& cfg, ProcessIndicator& indicator) 
    : m_headerDesc(NULL), m_detailDesc(NULL) 
{
    BuildDescriptors(cfg);
    BuildComposers(indicator);

    wxASSERT(NULL != m_headerDesc);
    wxASSERT(NULL != m_detailDesc);
    wxASSERT(0 < m_composers.size());
};

DocManager::~DocManager() {
    if ( NULL != m_headerDesc ) {
        wxDELETE(m_headerDesc);
    }
    if ( NULL != m_detailDesc ) {
        wxDELETE(m_detailDesc);
    }
};


Document* DocManager::Create() const {
    wxASSERT(NULL != m_headerDesc);
    wxASSERT(NULL != m_detailDesc);

    return new EdiDocument(*m_headerDesc, *m_detailDesc);
};


Composer& DocManager::FindComposerForFile(const wxString& fileName) const {
    wxFileName fn(fileName);
    wxString ext = "."+fn.GetExt();
    
    wxASSERT(!(m_composers.end() == m_composers.find(ext)));

    DocManager* m = (DocManager*)this;
    Composer* c = m->m_composers[ext.Lower()];
	wxASSERT(NULL != c );
    return *c;
};

void DocManager::SaveAs(const Document& doc, const wxString& fileName) const {
    Composer& cmpsr = FindComposerForFile(fileName);
    if ( cmpsr.Write(doc, fileName) ) {
        ((EdiDocument&)doc).SetFileName(fileName);
        ((EdiDocument&)doc).m_changed = false;
    }
}; 

void DocManager::Save(const Document& doc) const {
    Composer& cmpsr = FindComposerForFile(doc.GetFileName());
    if ( cmpsr.Write(doc, doc.GetFileName()) ) {
        ((EdiDocument&)doc).m_changed = false;
    }
};

Document* DocManager::Open(const wxString& fileName) const {
	Document* doc = Create();
	doc->SetFileName(fileName);
	try {
        Composer& cmpsr = FindComposerForFile(fileName);
        cmpsr.Read(*doc);
		((EdiDocument*)doc)->m_changed = false;
	} catch ( ... ) {
		wxDELETE(doc);
		throw;
	}
	return doc;
};

void DocManager::BuildDescriptors(const CXmlNode& cfg) {

    CXmlNode* headerNode = cfg.GetChild(ADVPCS_HEADER_CFG.c_str());
    if ( NULL == headerNode ) {
        THROW_CFG_EXCEPTION(ADVPCS_CFG_HEADER_NOT_FOUND);
    }
    m_headerDesc = new XmlDescriptor(*headerNode);
	m_headerDesc->CheckValid();

    CXmlNode* detailNode = cfg.GetChild(ADVPCS_DETAIL_CFG.c_str());
    if ( NULL == detailNode ) {
        THROW_CFG_EXCEPTION(ADVPCS_CFG_DETAIL_NOT_FOUND);
    }
    m_detailDesc = new XmlDescriptor(*detailNode);
    m_detailDesc->CheckValid();
    
    wxASSERT(NULL != m_headerDesc);
    wxASSERT(NULL != m_detailDesc);
};

void DocManager::BuildComposers(ProcessIndicator& indicator) {
    m_composers[ADVPCS_CSV_EXT] = new CsvComposer(indicator); 
    m_composers[ADVPCS_EDI_EXT] = new EdiComposer(indicator);
};

