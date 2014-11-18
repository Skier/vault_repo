/*
 *  $RCSfile: FileExecutor.h,v $
 *
 *  $Revision: 1.3 $
 *
 *  last change: $Date: 2003/09/09 07:44:32 $
 */
// FileExecutor.h: interface for the CFileExecutor class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_FILEEXECUTOR_H__292FEFA1_E8DA_427B_AA8B_61C02C9F749F__INCLUDED_)
#define AFX_FILEEXECUTOR_H__292FEFA1_E8DA_427B_AA8B_61C02C9F749F__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/IDoneExecutor.h>

class CFileExecutor : public IDoneExecutor {
public:
    CFileExecutor() : IDoneExecutor() {};
    virtual ~CFileExecutor(){};

    void Initialize(const CString& doneDir, const CString errorDir, const CString& workDir) {
        IDoneExecutor::Initialize(doneDir, errorDir);
        m_workDir = workDir; 
    };
    virtual void Execute(IMessage& msg, ILogger& logger);

protected:
    CString m_workDir;

};

#endif // !defined(AFX_FILEEXECUTOR_H__292FEFA1_E8DA_427B_AA8B_61C02C9F749F__INCLUDED_)
