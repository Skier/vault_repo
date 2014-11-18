/*
 *  $RCSfile: Asn1Node.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// Asn1Node.h: interface for the CAsn1Node class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_ASN1NODE_H__B72882E3_42CE_4EAD_8B10_27B90A7E1681__INCLUDED_)
#define AFX_ASN1NODE_H__B72882E3_42CE_4EAD_8B10_27B90A7E1681__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/Types.h>
#include <stdlib.h>

class CAsn1Node {
public:
    CAsn1Node() : 
        m_tag(0), m_length(0), m_data(NULL), m_next(NULL), 
        m_children(NULL){};

    virtual ~CAsn1Node();

public:
    BYTE        GetTag() const { return m_tag; };
    void        SetTag(BYTE tag) { m_tag = tag; };

    size_t      GetDataLength() const { return m_length; }; 

    const BYTE* GetData() const { return m_data; };
    void        SetData(const BYTE* data, size_t length);

    CAsn1Node*  GetNext() { return m_next; };
    void        SetNext(CAsn1Node* next) { m_next = next; };

    CAsn1Node*  GetChildren() { return m_children; };
    void        SetChildren(CAsn1Node* children) { m_children = children; };
public:
    size_t GetLength();
    size_t GetBodyLength();

    static size_t GetEncodedLengthSize(size_t len);
private:
    BYTE   m_tag;
    size_t m_length;
    BYTE*  m_data;

    CAsn1Node* m_next;
    CAsn1Node* m_children;
};

#endif // !defined(AFX_ASN1NODE_H__B72882E3_42CE_4EAD_8B10_27B90A7E1681__INCLUDED_)
