/*
 *  $RCSfile: MqExecutorFactory.h,v $
 *
 *  $Revision: 1.3 $
 *
 *  last change: $Date: 2003/09/09 07:44:32 $
 */
// MqExecutorFactory.h: interface for the CMqExecutorFactory class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_MQEXECUTORFACTORY_H__FC0F2B3E_E642_47FF_BBB4_B7D36A6A0746__INCLUDED_)
#define AFX_MQEXECUTORFACTORY_H__FC0F2B3E_E642_47FF_BBB4_B7D36A6A0746__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/IExecutorFactory.h>
#include <atf/Cfg.h>

class CMqExecutorFactory : public IExecutorFactory {
public:
	CMqExecutorFactory();
	virtual ~CMqExecutorFactory();
public:
    void Configure(ICfg &cfg) throw (CConfigurationException);
    void Initialize();

    const char* GetName() const { return "CMqExecutorFactory"; };
public:
    IExecutor * Create();
private:
    HANDLE      m_queue;
    CString     m_formatName;
    CString     m_pathName;
    bool        m_forseCreate;
    CString     m_doneDir;
    CString     m_errorDir;
};

#endif // !defined(AFX_MQEXECUTORFACTORY_H__FC0F2B3E_E642_47FF_BBB4_B7D36A6A0746__INCLUDED_)
