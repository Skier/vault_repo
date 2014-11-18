/*
 *  $RCSfile: NoMoreDataException.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// NoMoreDataException.h: interface for the CNoMoreDataException class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_NOMOREDATAEXCEPTION_H__2314A037_5795_4FBA_B38C_E91D9A13BB5C__INCLUDED_)
#define AFX_NOMOREDATAEXCEPTION_H__2314A037_5795_4FBA_B38C_E91D9A13BB5C__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/Exception.h>

class CNoMoreDataException : public CAtfException {
public:
	CNoMoreDataException(const char *fileName, int lineNo, size_t exists, size_t requested, const char* message);
    virtual ~CNoMoreDataException(){};
    size_t GetExists() const {return m_exists;};
    size_t GetRequested() const {return m_requested;};
private:
    size_t m_exists;
    size_t m_requested;
};

#define THROW_NO_MORE_DATA_EXCEPTION(message, exists, requested) \
{\
    throw CNoMoreDataException(__FILE__, __LINE__, exists, requested, message);\
}

#endif // !defined(AFX_NOMOREDATAEXCEPTION_H__2314A037_5795_4FBA_B38C_E91D9A13BB5C__INCLUDED_)
