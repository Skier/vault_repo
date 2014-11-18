/*
 *  $RCSfile: ByteStream.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// ByteStream.h: interface for the CByteStream class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_BYTESTREAM_H__CD041A6E_E931_4B93_92B1_9A8841FE7A51__INCLUDED_)
#define AFX_BYTESTREAM_H__CD041A6E_E931_4B93_92B1_9A8841FE7A51__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/Types.h>

class IByteIterator;

class CByteStream {
public:
	CByteStream();
	virtual ~CByteStream();
public:
    CByteStream& operator<<(BYTE value);
    CByteStream& Put(const BYTE *data, size_t length);

    const BYTE* GetBuffer() const { return m_buffer; };
    size_t      GetLength() const { return m_length; };
    IByteIterator* CreateIterator();
private:
    void IncBuffer();
private:
    BYTE * m_buffer;
    size_t m_length;
    size_t m_size;
};

class CByteStreamHolder {
public:
    CByteStreamHolder(CByteStream* stream):m_stream(stream){};
    ~CByteStreamHolder(){ delete m_stream; };
private:
    CByteStream* m_stream;
};

#endif // !defined(AFX_BYTESTREAM_H__CD041A6E_E931_4B93_92B1_9A8841FE7A51__INCLUDED_)
