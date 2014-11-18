/*
 *  $RCSfile: IMessage.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// IMessage.h: interface for the IMessage class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_IMESSAGE_H__D8E26181_FAC3_4200_A2AA_1CC97E10818D__INCLUDED_)
#define AFX_IMESSAGE_H__D8E26181_FAC3_4200_A2AA_1CC97E10818D__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <AFXWIN.H>
#include <atf/Types.h>

class IMessage {
public:
    IMessage(){};
    virtual ~IMessage(){};
public:
    virtual bool    IsEmpty() const = 0;
    virtual const BYTE* GetBody() = 0;
    virtual size_t  GetLength() = 0;
    virtual size_t  GetID() const = 0;
    virtual CString GetTransactionID() const = 0;
    virtual CString GetType() const = 0;
    virtual CString GetFileName() const = 0;
};

#endif // !defined(AFX_IMESSAGE_H__D8E26181_FAC3_4200_A2AA_1CC97E10818D__INCLUDED_)
