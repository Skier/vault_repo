/*
 *  $RCSfile: XmlDocument.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// XmlDocument.cpp: implementation of the CXmlDocument class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/XmlDocument.h>

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CXmlDocument::CXmlDocument(const CXmlDocument& doc) {
    DoCopy(doc);
}



CXmlDocument& CXmlDocument::operator=(const CXmlDocument& doc) {
    delete m_root;
    DoCopy(doc);
    return *this;
}



void CXmlDocument::DoCopy(const CXmlDocument& doc) {
    m_version = doc.m_version;
    m_encoding = doc.m_encoding;
    m_root = new CXmlNode(*doc.m_root);
}

