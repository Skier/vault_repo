/*
 *  $RCSfile: XmlStringWriter.cpp,v $
 *
 *  $Revision: 1.3 $
 *
 *  last change: $Date: 2004/04/22 16:45:49 $
 */
// XmlStringWriter.cpp: implementation of the CXmlStringWriter class.
//
//////////////////////////////////////////////////////////////////////
#include <AFXWIN.H>
#include <atf/XmlStringWriter.h>
#include <atf/AssertException.h>
#include <atf/XmlDocument.h>

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CString CXmlStringWriter::Write(const CXmlDocument &doc) {
    CString str = "";
    ATF_ASSERT(NULL != doc.GetRoot());
    Write(str, *(doc.GetRoot()));
    return str;
};


static void MakeIdent(CString &dest, int level) {
    int i = 0;
    for (i=0; i<level; i++) {
        dest += "  ";
    }
};

void CXmlStringWriter::Write(CString& dest, const CXmlNode &node, int level) {
    if ( m_isIdent ) {
        MakeIdent(dest, level);
    }
    dest += "<";
    dest += node.GetName();
    const CXmlProperty * prop = node.GetProperties();
    while ( NULL != prop ) {
        dest += " ";
        dest += prop->GetName();
        dest += "='";
        CString value = prop->GetValue();
        value.Replace("&", "&amp;");
        value.Replace("\'", "&apos;");
        value.Replace(">", "&gt;");
        value.Replace("<", "&lt;");
        value.Replace("\"", "&quot;");
        dest += value;
        dest += "'";
        prop = prop->GetNext();
    }
    dest += ">";

    if ( m_isIdent && !node.GetContent().IsEmpty()) {
        dest += "\n";
    }
 
    CString content = node.GetContent();
    content.Replace("&", "&amp;");
    content.Replace("\'", "&apos;");
    content.Replace(">", "&gt;");
    content.Replace("<", "&lt;");
    content.Replace("\"", "&quot;");
    dest += content;

    const CXmlNode * child = node.GetChildren();
    while ( NULL != child ) {
        if ( m_isIdent ) {
            dest += "\n";
            Write(dest, *child, level+1);
        } else {
            Write(dest, *child);
        }
        child = child->GetNext();
    }

    if ( m_isIdent ) {
        dest += "\n";
        MakeIdent(dest, level);
    }
    dest += "</";
    dest += node.GetName();
    dest += ">";
};
