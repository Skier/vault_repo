/*
 *  $RCSfile: ByteStream.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// ByteStream.cpp: implementation of the CByteStream class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/ByteStream.h>
#include <atf/ErrorConst.h>
#include <atf/Exception.h>
#include <atf/ByteIterator.h>
#include <stdio.h>
#include <memory.h>

static const size_t BUFFER_INC_SIZE = 1024;

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CByteStream::CByteStream() : m_buffer(NULL), m_length(0), m_size(0){
}

CByteStream::~CByteStream(){
    if ( NULL != m_buffer ) {
        free(m_buffer);
    }
}

CByteStream& CByteStream::operator<<(BYTE value) {
    if ( m_length == m_size ) {
        IncBuffer();
    }
    m_buffer[m_length++] = value;
    return *this;
};

CByteStream& CByteStream::Put(const BYTE *data, size_t length) {
    for ( size_t i = 0; i < length; i++ ) {
        (*this)<<data[i];
    }
    return * this;
};

void CByteStream::IncBuffer() {
    void *new_buffer = realloc(m_buffer, m_size+BUFFER_INC_SIZE);
    if ( NULL == new_buffer ) {
        THROW_ATF_EXCEPTION(ATF_NOMEMORY_ERR, "No memory");
    }
    m_buffer = (BYTE*)new_buffer;
    m_size += BUFFER_INC_SIZE;
};

IByteIterator* CByteStream::CreateIterator() {
    return new CByteIterator(*this);
};
