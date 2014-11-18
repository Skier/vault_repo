/*
 *  $RCSfile: FileExecutor.cpp,v $
 *
 *  $Revision: 1.3 $
 *
 *  last change: $Date: 2003/09/09 07:44:32 $
 */
// FileExecutor.cpp: implementation of the CFileExecutor class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/FileExecutor.h>
#include <atf/SystemException.h>


void CFileExecutor::Execute(IMessage& msg, ILogger& logger) {
    CString fileName;
    if ( msg.GetFileName().IsEmpty() ) { // Not file based message 
        fileName = m_workDir + "\\" + msg.GetTransactionID() + "." + msg.GetType();
    } else {
        CString toName = m_workDir + "\\";
        int pos = msg.GetFileName().ReverseFind('\\');
        int size = msg.GetFileName().GetLength();
        CString shortFileName = ((const char*)msg.GetFileName())+pos+1;
        fileName.Format("%s\\%s", m_workDir, shortFileName);
    }
    WriteFile(fileName, msg);
};


