/*
 *  $RCSfile: StreamIOHandler.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// StreamIOHandler.h: interface for the CStreamIOHandler class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_STREAMIOHANDLER_H__A3BA8AB1_96F0_497D_9382_0BA903C523C0__INCLUDED_)
#define AFX_STREAMIOHANDLER_H__A3BA8AB1_96F0_497D_9382_0BA903C523C0__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/XmlIOHandler.h>
#include <iostream>

using namespace std;

class CStreamIOHandler : public IXmlIOHandler {
public:
    CStreamIOHandler(istream& stream):m_stream(stream){};
    virtual ~CStreamIOHandler(){};
    virtual size_t LoadBuffer(char * buffer, size_t bufferSize);    
private:
    istream& m_stream;
};

#endif // !defined(AFX_STREAMIOHANDLER_H__A3BA8AB1_96F0_497D_9382_0BA903C523C0__INCLUDED_)
