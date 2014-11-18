/*
 *  $RCSfile: Message.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// Message.h: interface for the CMessage class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_MESSAGE_H__7CDA8136_EBB0_4E0B_B93D_F7AA844FA027__INCLUDED_)
#define AFX_MESSAGE_H__7CDA8136_EBB0_4E0B_B93D_F7AA844FA027__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/IMessage.h>

class CMessage : public IMessage {
public:
    CMessage(CString fileName);
    CMessage(const BYTE* body, size_t length);
    virtual ~CMessage();
public:
    bool    IsEmpty() const {return m_fileName.IsEmpty() && m_body == NULL; };
    const BYTE* GetBody() {
        if ( !m_isLoaded ) { 
            Load();
        }
        return m_body;
    };
    size_t  GetLength() {
        if ( !m_isLoaded ) { 
            Load();
        }
        return m_length;
    };
    size_t  GetID() const {return m_id;};
    CString GetTransactionID() const {return m_transactionId;};
    CString GetType() const {return m_type;};
    CString GetFileName() const {return m_fileName;};

public:
    void SetType(CString value) {m_type = value;};
    void SetID(size_t id) {m_id = id;};
    void SetTransactionID(CString transationId) {
        m_transactionId = transationId;
    };

protected:
    void Load();

private:
    CString m_transactionId;
    CString m_type;
    CString m_fileName;
    size_t  m_length;
    size_t  m_id;
    BYTE*   m_body;

    bool    m_isLoaded;
    size_t  m_size;
private:
    static HANDLE  m_mutex;
    static size_t m_currentId;
    static size_t GetNextId();
};

#endif // !defined(AFX_MESSAGE_H__7CDA8136_EBB0_4E0B_B93D_F7AA844FA027__INCLUDED_)
