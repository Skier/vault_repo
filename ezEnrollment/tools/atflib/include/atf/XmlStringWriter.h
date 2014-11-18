/*
 *  $RCSfile: XmlStringWriter.h,v $
 *
 *  $Revision: 1.3 $
 *
 *  last change: $Date: 2004/04/22 16:45:49 $
 */
// XmlStringWriter.h: interface for the CXmlStringWriter class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_XMLSTRINGWRITER_H__8B751827_F41C_40B2_B734_C2A0DD370B31__INCLUDED_)
#define AFX_XMLSTRINGWRITER_H__8B751827_F41C_40B2_B734_C2A0DD370B31__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CXmlNode;
class CXmlDocument;

class CXmlStringWriter {
public:
    CXmlStringWriter(bool ident = false): m_isIdent(ident) {};
public:
    CString Write(const CXmlDocument &doc);
    void Write(CString& dest, const CXmlNode &node, int level = 0);
private:
    bool m_isIdent;
};

#endif // !defined(AFX_XMLSTRINGWRITER_H__8B751827_F41C_40B2_B734_C2A0DD370B31__INCLUDED_)
