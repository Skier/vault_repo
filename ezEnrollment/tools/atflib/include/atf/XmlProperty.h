/*
 *  $RCSfile: XmlProperty.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// XmlProperty.h: interface for the XmlProperty class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_XMLPROPERTY_H__D964130B_6C87_47EF_A1FE_DCD5D327C341__INCLUDED_)
#define AFX_XMLPROPERTY_H__D964130B_6C87_47EF_A1FE_DCD5D327C341__INCLUDED_

#include <AFXWIN.H>
#include <stdio.h>

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CXmlProperty  
{
public:
    CXmlProperty() : m_next(NULL) {};
    CXmlProperty(const CString& name, const CString& value,
                  CXmlProperty *next)
            : m_name(name), m_value(value), m_next(next) {}

    CString GetName() const { return m_name; }
    CString GetValue() const { return m_value; }
    CXmlProperty *GetNext() const { return m_next; }

    void SetName(const CString& name) { m_name = name; }
    void SetValue(const CString& value) { m_value = value; }
    void SetNext(CXmlProperty *next) { m_next = next; }


private:
    CString m_name;
    CString m_value;
    CXmlProperty *m_next;
};

#endif // !defined(AFX_XMLPROPERTY_H__D964130B_6C87_47EF_A1FE_DCD5D327C341__INCLUDED_)
