/*
 *  $RCSfile: MqExecutor.h,v $
 *
 *  $Revision: 1.3 $
 *
 *  last change: $Date: 2003/09/09 07:44:32 $
 */
// MqExecutor.h: interface for the CMqExecutor class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_MQEXECUTOR_H__412C46BA_2C74_4DD7_8049_7D4DC09CADB1__INCLUDED_)
#define AFX_MQEXECUTOR_H__412C46BA_2C74_4DD7_8049_7D4DC09CADB1__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/IDoneExecutor.h>

class CThread;

class CMqExecutor : public IDoneExecutor {
public:
    CMqExecutor();
	virtual ~CMqExecutor();
public:
	void Initialize(const CString& doneDir, const CString errorDir, HANDLE queue);
    virtual void Execute(IMessage& msg, ILogger& logger);

private:
    HANDLE      m_queue;
};

#endif // !defined(AFX_MQEXECUTOR_H__412C46BA_2C74_4DD7_8049_7D4DC09CADB1__INCLUDED_)
