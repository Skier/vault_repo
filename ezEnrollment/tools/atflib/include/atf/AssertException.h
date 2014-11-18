/*
 *  $RCSfile: AssertException.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// AssertException.h: interface for the CAssertException class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_ASSERTEXCEPTION_H__54003AFC_EFDA_4608_BF69_0BE02ABFA8A0__INCLUDED_)
#define AFX_ASSERTEXCEPTION_H__54003AFC_EFDA_4608_BF69_0BE02ABFA8A0__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/Exception.h>

extern const ATF_ERROR ATF_ASSERT_ERR;

class CAssertException : public CAtfException {
public:
    CAssertException(const char *fileName, int lineNo, const char *message):
        CAtfException(fileName, lineNo, ATF_ASSERT_ERR, message) {};
};

#define ATF_ASSERT( condition ) \
{\
    if( !(condition) ) { \
        throw CAssertException(__FILE__, __LINE__, "Assertion failed"); \
    } \
}

#define ATF_ASSERT_MSG( condition, message) \
{ \
    if( !(condition) ) { \
        throw CAssertException(__FILE__, __LINE__, message); \
    } \
}
#endif // !defined(AFX_ASSERTEXCEPTION_H__54003AFC_EFDA_4608_BF69_0BE02ABFA8A0__INCLUDED_)
