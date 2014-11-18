/*
 *  $RCSfile: MqException.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// MqException.h: interface for the CMqException class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_MQEXCEPTION_H__505D4228_5B8B_43F9_A1B5_D751C47B4C20__INCLUDED_)
#define AFX_MQEXCEPTION_H__505D4228_5B8B_43F9_A1B5_D751C47B4C20__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/Exception.h>
#include <objidl.h>
#include <wtypes.h>
#include <mq.h>

extern const ATF_ERROR TAGERR_MQ;

class CMqException : public CAtfException {
public:
    CMqException(const char *fileName, int lineNo, HRESULT result, const char *message);
    HRESULT GetResult() const {return m_result;};
private:
    HRESULT m_result;
};


#define THROW_MQ_EXCEPTION(result, message) \
    throw CMqException(__FILE__, __LINE__, result, message)

#endif // !defined(AFX_MQEXCEPTION_H__505D4228_5B8B_43F9_A1B5_D751C47B4C20__INCLUDED_)
