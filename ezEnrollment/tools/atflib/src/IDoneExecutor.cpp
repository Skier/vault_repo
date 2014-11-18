/*
 *  $RCSfile: IDoneExecutor.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2004/04/22 16:45:49 $
 */

#include <atf/IDoneExecutor.h>
#include <atf/SystemException.h>
#include <atf/Thread.h>
#include <fstream>

using namespace std;
int IDoneExecutor::Run(CThread* thisThread) {
    IMessage& msg = GetTask();
    try {
        Execute(GetTask(), thisThread->GetLogger());
        if ( !m_doneDir.IsEmpty() ) {
            WriteFile(GetMessageFileName(msg, m_doneDir), msg);
        }
    } catch (...) {
        if ( !m_errorDir.IsEmpty() ) {
            WriteFile(GetMessageFileName(msg, m_errorDir), msg);
        }
        if ( !msg.GetFileName().IsEmpty() ) { // File based message 
            if ( !DeleteFile(msg.GetFileName()) ) {
                CString m;
                m.Format("Can't delete file '%s'", msg.GetFileName());
                THROW_SYSTEM_EXCEPTION(m);
            }
        }
        throw;
    }

    if ( !msg.GetFileName().IsEmpty() ) { // File based message 
        if ( !DeleteFile(msg.GetFileName()) ) {
            CString m;
            m.Format("Can't delete file '%s'", msg.GetFileName());
            THROW_SYSTEM_EXCEPTION(m);
        }
    }
    return 0;
};
    
void IDoneExecutor::WriteFile(CString fileName, IMessage& msg) 
{
    ofstream os(fileName, ios::out|ios::binary);
    if ( !os.is_open() ) {
        THROW_SYSTEM_EXCEPTION(fileName);
    }
    os.write((const char*)msg.GetBody(), msg.GetLength());
    os.flush();
    os.close();
};

CString IDoneExecutor::GetMessageFileName(const IMessage& msg, const CString& dir)const {
    CString shortFileName;

    if ( msg.GetFileName().IsEmpty() ) { // No file based message
        shortFileName = msg.GetTransactionID() + "." + msg.GetType();
    } else {
        int pos = msg.GetFileName().ReverseFind('\\');
        int size = msg.GetFileName().GetLength();
        shortFileName = ((const char*)msg.GetFileName())+pos+1;
    }

    SYSTEMTIME systemTime;
    GetSystemTime(&systemTime);
    CString fileName;
    fileName.Format("%s\\%04d%02d%02d%02d%02d%02d-%s",
                    dir,
                    systemTime.wYear,
                    systemTime.wMonth,
                    systemTime.wDay,
                    systemTime.wHour,
                    systemTime.wMinute,
                    systemTime.wSecond, 
                    shortFileName);

    return fileName;
};
