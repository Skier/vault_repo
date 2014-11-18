/*
 *  $RCSfile: FileExecutorFactory.h,v $
 *
 *  $Revision: 1.3 $
 *
 *  last change: $Date: 2003/09/09 07:44:32 $
 */
// FileExecutorFactory.h: interface for the CFileExecutorFactory class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_FILEEXECUTORFACTORY_H__DD7005FD_40B1_47F9_BD20_791F09FAC083__INCLUDED_)
#define AFX_FILEEXECUTORFACTORY_H__DD7005FD_40B1_47F9_BD20_791F09FAC083__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/Cfg.h>
#include <atf/IExecutorFactory.h>

class CFileExecutorFactory : public IExecutorFactory {
public:
	CFileExecutorFactory();
    virtual ~CFileExecutorFactory(){};
public:
    void Configure(ICfg &cfg) throw (CConfigurationException);
    void SetWorkDir(const CString & workDir) { m_workDir = workDir; };
    void Initialize(){};

    const char* GetName() const { return "CFileExecutorFactory"; };
public:
    IExecutor * Create();
protected:
    CString  m_workDir;
    CString  m_doneDir;
    CString  m_errorDir;

};

#endif // !defined(AFX_FILEEXECUTORFACTORY_H__DD7005FD_40B1_47F9_BD20_791F09FAC083__INCLUDED_)
