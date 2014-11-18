/*
 *  $RCSfile: FileListener.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// CdFileListener.cpp: implementation of the CCdFileListener class.
//
//////////////////////////////////////////////////////////////////////
#include <atf/Cfg.h>
#include <atf/SystemException.h>
#include <atf/ExceptionLogger.h>
#include <atf/ThreadPool.h>
#include <atf/PoolableThread.h>
#include <atf/Util.h>
#include <atf/FileListener.h>
#include <atf/Message.h>
#include <atf/IExecutor.h>
#include <atf/ExecutorPool.h>
#include <atf/ThreadBodyRunnable.h>

static const char* ATF_WORK_DIR_CFG = "work-dir";
static const char* ATF_SRC_DIR_CFG  = "src-dir";
static const char* ATF_RETRY_UNCOMPLETED_CFG = "retry-uncompleted";

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

static const int ATF_WAIT_HANDLE = 0;
static const int ATF_STOP_HANDLE = 1;

#pragma warning (disable : 4800)

void CFileListener::Configure(ICfg & cfg) {
    m_workDir = cfg.GetParam(ATF_WORK_DIR_CFG);
    if ( !FileCheckDirExists(m_workDir) ) {
        THROW_CFG_EXCEPTION("Directory [" + m_workDir + "] not exists");
    }

    m_srcDir  = cfg.GetParam(ATF_SRC_DIR_CFG);
    if ( !FileCheckDirExists(m_srcDir) ) {
        THROW_CFG_EXCEPTION("Directory [" + m_srcDir + "] not exists");
    }
    m_retryUncompleted = cfg.GetParamAsBool(ATF_RETRY_UNCOMPLETED_CFG, false);
};

void CFileListener::RetryUncompleted() {
    WIN32_FIND_DATA info;
    HANDLE          handle;
    bool            found = true;
    CString         scanFileName;
    CString         fromFileName;
    CString         toFileName;

    scanFileName = m_workDir + "\\*";

    for( handle=FindFirstFile(scanFileName,&info);
             handle!=INVALID_HANDLE_VALUE && found; 
             found = FindNextFile(handle, &info)) {

        if(info.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY) {
            continue;
        }
        fromFileName = m_workDir + "\\" + info.cFileName;
        toFileName = m_srcDir + "\\" + info.cFileName;
        if(!MoveFile(fromFileName, toFileName)) {
            LOG_ERROR(GetLogger(), ATF_SYSTEM_ERR, "["+fromFileName+"]["+toFileName+"] "+ SysErrorMsg(0));
        } else {
            LOG_DEBUG(GetLogger(), ATF_DEBUG, "File ["+fromFileName + "] try retry" );
        }

    }

};


void CFileListener::Initialize() {
    m_handles[ATF_WAIT_HANDLE] = 
        FindFirstChangeNotification(
            m_srcDir, 
            false, 
            FILE_NOTIFY_CHANGE_FILE_NAME);

    m_handles[ATF_STOP_HANDLE] = CreateEvent(NULL, true, false, NULL);

    LOG_INFO(GetLogger(), ATF_INFO, 
              "File listener initialized for ["+m_srcDir+"] directory");

    if ( m_retryUncompleted ) {
        RetryUncompleted();
    }
};

void CFileListener::Stop() {
    SetEvent(m_handles[ATF_STOP_HANDLE]);
    m_stopped = true;
};

int CFileListener::Run(CThread * thisThread) {
    CString loop = "File Listener loop";
    bool found = true;
    while (!m_stopped) {
        try {
            if ( !found ) {
                LOG_DEBUG(GetLogger(), ATF_DEBUG, "File listener wait for dir changes");
                DWORD result = WaitForMultipleObjects(2,m_handles,false,INFINITE);
                if ( 1 == result - WAIT_OBJECT_0 ) {
                    break;
                }
                if ( 0 != result - WAIT_OBJECT_0 ) {
                    THROW_SYSTEM_EXCEPTION("Problem during wait changes");
                }

                FindNextChangeNotification(m_handles[ATF_WAIT_HANDLE]);
            }
            CString file = GetNextFile();
            if ( file.IsEmpty() ) {
                found = false;
                continue;
            }
            found = true;
            
            LOG_DEBUG(GetLogger(), ATF_DEBUG, "File found ["+file+"]");

            CMessage * msg = new CMessage(file);
            int pos_point = file.ReverseFind('.');
            int pos_slash = file.ReverseFind('\\');
            if ( pos_point < pos_slash || pos_point == -1 ) {
                msg->SetType("$$$");
            } else {
                CString ext = ((const char*)file)[pos_point+1];
                msg->SetType(ext);
            }
            
            CString tranId;
            if ( pos_point < pos_slash || pos_slash == -1) {
                tranId.Format("%0ld", msg->GetID());
                msg->SetTransactionID(tranId);
            } else {
                for ( int i = pos_slash + 1; i < pos_point; i++ ) {
                    tranId += file[i];
                }
                msg->SetTransactionID(tranId);
            }


            IExecutor * executor = m_executors.Get();
            CThread * proc = m_threads.Get();
            executor->SetTask(msg);
            IRunnable& body = proc->GetBody();
            ((CThreadBodyRunnable&)body).SetTask(executor, &m_executors);
            ((CPoolableThread*)proc)->Run();

        } catch (CSystemException &se) {
            CExceptionLogger::Log(GetLogger(), se);
        } catch (CAtfException &te) {
            CExceptionLogger::Log(GetLogger(), te);
        } catch (...) {
            LOG_ERROR(GetLogger(), ATF_ASSERT_ERR, "Unknown Exception was thrown");
        }
        LOG_DEBUG(GetLogger(), ATF_DEBUG, loop);
    }
    return 0;
};

CString CFileListener::GetNextFile() {
    WIN32_FIND_DATA info;
    HANDLE          findHandle;
    bool            findOk = true;
    bool            isFound = false;
    CString         fileName;
    CString         scanFileName;
    int             tryCount = 0;

    scanFileName = m_srcDir;
    scanFileName += "\\*.*";

    for(findHandle=FindFirstFile(scanFileName,&info); 
            findHandle!=INVALID_HANDLE_VALUE && findOk && !isFound; 
               findOk = FindNextFile(findHandle, &info)) 
    {
        if(!(info.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY)) {

                /* rename file for check it free */
            fileName = m_srcDir;
            fileName += "\\";
            fileName += info.cFileName;
            scanFileName = m_workDir + "\\" + info.cFileName;

            LOG_DEBUG(GetLogger(), LOG_DEBUG, 
                      "File Listener found ["+fileName+"] move to ["+
                      scanFileName+"]");
            while( true ) {
                if(MoveFile(fileName, scanFileName)) {
                    LOG_DEBUG(GetLogger(), LOG_DEBUG, "File moved to ["+scanFileName+"]");
                    isFound = true;
                    break;
                }
                if(tryCount++>1000) {
                    THROW_SYSTEM_EXCEPTION(fileName+"->"+scanFileName);
                    break;
                }
                Sleep(100);
            }
        }
    };
    if(findHandle !=INVALID_HANDLE_VALUE) {
        FindClose(findHandle);
    }
    if(!isFound){
        scanFileName.Empty();
    }
    LOG_DEBUG(GetLogger(), ATF_DEBUG, "Found ["+scanFileName+"]");
    return scanFileName;
};
