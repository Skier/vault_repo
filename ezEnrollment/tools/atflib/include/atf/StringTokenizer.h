/*
 *  $RCSfile: StringTokenizer.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// StringTokenizer.h: interface for the CStringTokenizer class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_STRINGTOKENIZER_H__037FA41E_4A7D_4922_A56F_281CEC16C06F__INCLUDED_)
#define AFX_STRINGTOKENIZER_H__037FA41E_4A7D_4922_A56F_281CEC16C06F__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <AFXWIN.H>

#pragma warning( disable : 4290 ) 

class CNoMoreDataException;

class CStringTokenizer {
public:
    CStringTokenizer(const CString& arg, const CString& delim): 
      m_arg(arg), m_delim(delim), m_pos(0) {};
    virtual ~CStringTokenizer(){};
public:

    CString nextToken() throw (CNoMoreDataException);
    bool hasNext() { return m_pos < m_arg.GetLength(); };
    void Reset() { m_pos = 0; };
private:
    CString m_arg;
    CString m_delim;
    int m_pos;
};

#endif // !defined(AFX_STRINGTOKENIZER_H__037FA41E_4A7D_4922_A56F_281CEC16C06F__INCLUDED_)
