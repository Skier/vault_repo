/*
 *  $RCSfile: StringIOHandler.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// StringIOHandler.h: interface for the CStringIOHandler class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_STRINGIOHANDLER_H__7B3BA139_B357_4E3D_A804_8646DE269D12__INCLUDED_)
#define AFX_STRINGIOHANDLER_H__7B3BA139_B357_4E3D_A804_8646DE269D12__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/XmlIOHandler.h>
#include <AFXWIN.H>

class CStringIOHandler : public IXmlIOHandler {
public:
    CStringIOHandler(const char * body): m_body(body), m_pos(0){};
    virtual ~CStringIOHandler(){};
    size_t LoadBuffer(char * buffer, size_t bufferSize);    
private:
    CString m_body;
    size_t  m_pos;
};

#endif // !defined(AFX_STRINGIOHANDLER_H__7B3BA139_B357_4E3D_A804_8646DE269D12__INCLUDED_)
