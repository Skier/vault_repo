/*
 *  $RCSfile: XmlLoadException.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// XmlLoadException.h: interface for the CXmlLoadException class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_XMLLOADEXCEPTION_H__57BA5EDF_C3D0_432C_ABE0_95305D89A0CB__INCLUDED_)
#define AFX_XMLLOADEXCEPTION_H__57BA5EDF_C3D0_432C_ABE0_95305D89A0CB__INCLUDED_

#include <AFXWIN.H>

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CXmlLoadException {
public:
    CXmlLoadException():m_errorCode(0), m_lineNumber(0){};
    CXmlLoadException(const char * errorString, long errorCode, long lineNumber)
        :m_errorString(errorString),m_errorCode(errorCode),m_lineNumber(lineNumber){};

    const CString& GetErrorString() const { return m_errorString; };
    long GetErrorCode() const { return m_errorCode; };
    long GetLineNumber() const { return m_lineNumber; };

    void SetErrorString(const char * errorString) {m_errorString = errorString;};
    void SetErrorCode(long errorCode) {m_errorCode = errorCode;};
    void SetLineNumber(long lineNumber) {m_lineNumber=lineNumber;};
private:
    CString    m_errorString;
    long       m_errorCode;
    long       m_lineNumber;
};

#endif // !defined(AFX_XMLLOADEXCEPTION_H__57BA5EDF_C3D0_432C_ABE0_95305D89A0CB__INCLUDED_)
