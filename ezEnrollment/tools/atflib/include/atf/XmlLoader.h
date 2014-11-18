/*
 *  $RCSfile: XmlLoader.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// XmlLoader.h: interface for the CXmlLoader class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_XMLLOADER_H__EC78051F_56F2_438E_BCE8_70D4ED2E0AD2__INCLUDED_)
#define AFX_XMLLOADER_H__EC78051F_56F2_438E_BCE8_70D4ED2E0AD2__INCLUDED_

#include <atf/XmlDocument.h>
#include <atf/XmlLoadException.h>
#include <atf/XmlIOHandler.h>
#include <expat.h>

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#pragma warning( disable : 4290 ) 

const size_t BUFSIZE = 1024;

struct XmlParsingContext {
    CXmlNode *root;
    CXmlNode *node;
    CXmlNode *lastAsText;
    CString encoding;
    CString version;
};

class CXmlLoader {
public:
    CXmlLoader(IXmlIOHandler & handler);
    ~CXmlLoader();
    CXmlDocument& Load(CXmlDocument & doc) throw (CXmlLoadException);
    IXmlIOHandler & GetIOHandler() const { return m_handler; };
private:
    IXmlIOHandler &   m_handler;
    char              m_buf[BUFSIZE];
    XmlParsingContext m_ctx;
    XML_Parser        m_parser;
};

#endif // !defined(AFX_XMLLOADER_H__EC78051F_56F2_438E_BCE8_70D4ED2E0AD2__INCLUDED_)
