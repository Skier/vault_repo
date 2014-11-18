/*
 *  $RCSfile: Exception.h,v $
 *
 *  $Revision: 1.3 $
 *
 *  last change: $Date: 2004/03/31 12:20:44 $
 */
// TagException.h: interface for the CTagException class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_TAGEXCEPTION_H__6617A381_B0C8_4AF7_A094_CB44CFD5526B__INCLUDED_)
#define AFX_TAGEXCEPTION_H__6617A381_B0C8_4AF7_A094_CB44CFD5526B__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <AFXWIN.H>
#include <atf/ErrorConst.h>

class CAtfException {
public:
    CAtfException(const char *fileName, int lineNo, ATF_ERROR code, 
                  const char *message):
                      m_fileName(fileName),m_line(lineNo),m_message(message),
                      m_code(code){};
public:
	void operator=(const CAtfException& x) {
		m_fileName = x.m_fileName;
		m_line = x.m_line;
		m_message = x.m_message;
		m_code = x.m_code;
	}
    const CString& GetFileName() const { return m_fileName; };
    int        GetLine() const { return m_line; }; 
    const CString& GetText() const { return m_message; };
    ATF_ERROR GetCode() const {return m_code;};
protected:
    CString m_fileName;
    int        m_line;
    CString m_message;
    ATF_ERROR  m_code;
};

#define THROW_ATF_EXCEPTION(code, message) \
     throw CAtfException(__FILE__, __LINE__, code, message)

     

unsigned long SysErrorCode();
CString SysErrorMsg(unsigned long err_code);
     
#endif // !defined(AFX_TAGEXCEPTION_H__6617A381_B0C8_4AF7_A094_CB44CFD5526B__INCLUDED_)
