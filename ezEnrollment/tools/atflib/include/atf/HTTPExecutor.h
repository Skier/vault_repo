/*
 *  $RCSfile: HTTPExecutor.h,v $
 *
 *  $Revision: 1.3 $
 *
 *  last change: $Date: 2003/09/09 07:44:32 $
 */
// HTTPExecutor.h: interface for the CHTTPExecutor class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_CDHTTPEXECUTOR_H__BAEF1D93_BC2F_4E3F_91E0_E77C7F42C466__INCLUDED_)
#define AFX_CDHTTPEXECUTOR_H__BAEF1D93_BC2F_4E3F_91E0_E77C7F42C466__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/IDoneExecutor.h>

class IMessage;

class CHTTPExecutor : public IDoneExecutor {
public:
    CHTTPExecutor() : IDoneExecutor(), m_count(0){};
    virtual ~CHTTPExecutor();
public:
    void Initialize(const CString& serverName, unsigned short port,
                    const CString& userName, const CString& password,
                    const CString& objectName, const CString& url, 
                    const CString& varName, const CString& workDir, 
                    const CString& doneDir, const CString& errorDir);

    virtual void Execute(IMessage& msg, ILogger& logger);
protected: 
    CString GetFileName(IMessage & msg);
private:
    size_t    m_count;

    CString m_serverName;
    unsigned short m_port;
    CString m_userName;
    CString m_password;
    CString m_objectName;
    CString m_url;
    CString m_varName;
    CString m_workDir;
};


#endif // !defined(AFX_CDHTTPEXECUTOR_H__BAEF1D93_BC2F_4E3F_91E0_E77C7F42C466__INCLUDED_)
