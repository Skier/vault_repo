/*
 *  $RCSfile: IByteIterator.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// IByteIterator.h: interface for the IByteIterator class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_IBYTEITERATOR_H__9F74F367_1A95_42C9_B590_1A4CC734A504__INCLUDED_)
#define AFX_IBYTEITERATOR_H__9F74F367_1A95_42C9_B590_1A4CC734A504__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/Types.h>

class CNoMoreDataException;

#pragma warning( disable : 4290 ) 

class IByteIterator {
public:
    virtual BYTE   GetNext() throw (CNoMoreDataException) = 0;
    virtual void   GetNextN(BYTE* buffer, size_t length) throw (CNoMoreDataException) = 0;
    virtual size_t Position() const = 0;
};

#endif // !defined(AFX_IBYTEITERATOR_H__9F74F367_1A95_42C9_B590_1A4CC734A504__INCLUDED_)
