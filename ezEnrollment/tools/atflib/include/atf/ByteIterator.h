/*
 *  $RCSfile: ByteIterator.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// ByteIterator.h: interface for the CByteIterator class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_BYTEITERATOR_H__0474E20C_4708_4307_9A7A_A6387185A769__INCLUDED_)
#define AFX_BYTEITERATOR_H__0474E20C_4708_4307_9A7A_A6387185A769__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/Types.h>
#include <atf/ByteStream.h> 
#include <atf/IByteIterator.h>

class CNoMoreDataException;

#pragma warning( disable : 4290 ) 

class CByteIterator : public IByteIterator {
public:
    CByteIterator(CByteStream &stream):m_stream(stream), m_pos(0){};
    virtual ~CByteIterator(){};
public:
    BYTE   GetNext() throw (CNoMoreDataException);
    void   GetNextN(BYTE* buffer, size_t length) throw (CNoMoreDataException);
    size_t Position() const { return m_pos; };
public:
    size_t Rest();
    void   Reset();
public:
    CByteStream& m_stream;
    size_t       m_pos;
};

#endif // !defined(AFX_BYTEITERATOR_H__0474E20C_4708_4307_9A7A_A6387185A769__INCLUDED_)
