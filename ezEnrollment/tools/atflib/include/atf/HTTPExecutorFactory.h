/*
 *  $RCSfile: HTTPExecutorFactory.h,v $
 *
 *  $Revision: 1.3 $
 *
 *  last change: $Date: 2003/09/09 07:44:32 $
 */
// CdHTTPExecutorFactory.h: interface for the CCdHTTPExecutorFactory class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_CDHTTPEXECUTORFACTORY_H__CBDE1FD8_79E7_4FF7_AD11_18AF95BBEA4D__INCLUDED_)
#define AFX_CDHTTPEXECUTORFACTORY_H__CBDE1FD8_79E7_4FF7_AD11_18AF95BBEA4D__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/IExecutorFactory.h>

class CHTTPExecutorFactory : public IExecutorFactory {
public:
    CHTTPExecutorFactory();
    virtual ~CHTTPExecutorFactory() {
    };
public:

    void Configure(ICfg &cfg) throw (CConfigurationException);
    void Initialize(){};

    const char* GetName() const { return "CHTTPExecutorFactory"; };
public:
    IExecutor * Create();
private:
    CString m_serverName;
    unsigned short m_port;
    CString m_userName;
    CString m_password;
    CString m_objectName;
    CString m_url;
    CString m_varName;
    CString m_workDir;
    CString m_doneDir;
    CString m_errorDir;

};

#endif // !defined(AFX_CDHTTPEXECUTORFACTORY_H__CBDE1FD8_79E7_4FF7_AD11_18AF95BBEA4D__INCLUDED_)
