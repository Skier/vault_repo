/*
 *  $RCSfile: IDoneExecutor.h,v $
 *
 *  $Revision: 1.1 $
 *
 *  last change: $Date: 2003/10/11 15:35:28 $
 */
#ifndef __ATFLIB_DONE_EXECUTOR_H__
#define __ATFLIB_DONE_EXECUTOR_H__

#include <atf/IExecutor.h>

class IDoneExecutor : public IExecutor {
public:
    IDoneExecutor() : IExecutor() {};

    virtual int Run(CThread * thisThread);
    virtual void Execute(IMessage& msg, ILogger& logger) = 0;

protected:
    void WriteFile(CString fileName, IMessage& msg);
    CString GetMessageFileName(const IMessage& msg, const CString& dir)const;
    void Initialize(const CString& doneDir, const CString errorDir) { 
        m_doneDir = doneDir;
        m_errorDir = errorDir;
    };

private: 
    CString m_doneDir;
    CString m_errorDir;
};

#endif /* __ATFLIB_DONE_EXECUTOR_H__ */

