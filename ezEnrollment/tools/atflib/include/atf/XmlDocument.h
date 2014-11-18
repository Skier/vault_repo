/*
 *  $RCSfile: XmlDocument.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// XmlDocument.h: interface for the CXmlDocument class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_XMLDOCUMENT_H__A0991F70_7240_4930_9D1F_4E7A69C7B2DE__INCLUDED_)
#define AFX_XMLDOCUMENT_H__A0991F70_7240_4930_9D1F_4E7A69C7B2DE__INCLUDED_

#include <atf/XmlNode.h>
#include <AFXWIN.H>

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CXmlDocument {
public:
    CXmlDocument() : m_version("1.0"), m_root(NULL)  {}
    ~CXmlDocument() { delete m_root; }

    CXmlDocument(const CXmlDocument& doc);
    CXmlDocument& operator=(const CXmlDocument& doc);

    bool IsOk() const { return m_root != NULL; }

    CXmlNode *GetRoot() const { return m_root; }

    CString GetVersion() const { return m_version; }
    CString GetEncoding() const { return m_encoding; }

    void SetRoot(CXmlNode *node) { delete m_root ; m_root = node; }
    void SetVersion(const CString& version) { m_version = version; }
    void SetEncoding(const CString& encoding) { m_encoding = encoding; }

private:
    CString m_version, m_encoding;
    CXmlNode *m_root;

    void DoCopy(const CXmlDocument& doc);
};

#endif // !defined(AFX_XMLDOCUMENT_H__A0991F70_7240_4930_9D1F_4E7A69C7B2DE__INCLUDED_)
