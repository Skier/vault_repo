/*
 *  $RCSfile: MqListener.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// MqListener.h: interface for the CMqListener class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_MQLISTENER_H__39752ED6_210A_44DF_90EF_F8833C229AA9__INCLUDED_)
#define AFX_MQLISTENER_H__39752ED6_210A_44DF_90EF_F8833C229AA9__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/IListener.h>
#include <atf/IMessage.h>

class CMqListener : public IListener {
public:
	CMqListener(CExecutorPool &executors, CThreadPool &threads);
	virtual ~CMqListener();
public:
    void Configure(ICfg & cfg);
    void Initialize();
    void Stop();
    int Run(CThread * thisThread);
protected:
    IMessage * ReceiveMessage();
private:
    HANDLE      m_queue;
    bool        m_stopped;
    CString     m_formatName;
    CString     m_pathName;
    bool        m_forseCreate;
};

#endif // !defined(AFX_MQLISTENER_H__39752ED6_210A_44DF_90EF_F8833C229AA9__INCLUDED_)
