/*
 *  $RCSfile: Asn1Node.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
#include <atf/Asn1Node.h>
#include <memory.h>

static const int TAG_SIZE = 1;

size_t CAsn1Node::GetEncodedLengthSize(size_t len) {
    size_t size = 1;
    if(len<128){
        return 1;
    }
    while(len!=0) {
        size++;
        len >>= 8;
    }
    return size;
};


size_t CAsn1Node::GetLength() {
    size_t l = 0; 
    l += GetBodyLength();
    l += GetEncodedLengthSize(l);
    l += TAG_SIZE;
    return l;
};

size_t CAsn1Node::GetBodyLength() {
    size_t l = 0;
    CAsn1Node * i = NULL;

    l += GetDataLength();
    i = GetChildren();
    while(i != NULL) {
        l += i->GetLength();
        i = i->GetNext();
    }
    return l;
};

CAsn1Node::~CAsn1Node() {
    if ( NULL != m_data ) {
        delete [] m_data;
    }
    if ( NULL != m_next ) {
        delete m_next;
    };
    if ( NULL != m_children ) {
        delete m_children;
    }
};

void CAsn1Node::SetData(const BYTE* data, size_t length) {
    if ( NULL != m_data ) {
        delete [] m_data;
    }
    m_data = new BYTE[length];
    memcpy(m_data, data, length);
    m_length = length;
};
