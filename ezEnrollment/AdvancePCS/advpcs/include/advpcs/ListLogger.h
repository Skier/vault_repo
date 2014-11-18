/*
 *  $RCSfile: ListLogger.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __ADVPCS_LIST_LOGGER_H__
#define __ADVPCS_LIST_LOGGER_H__


/* -------------------------- header place ---------------------------------- */
#include <wx/listctrl.h>
#include <atf/Logger.h>
/* -------------------------- implementation place -------------------------- */

class ListLogger : public ILogger {
public:
    ListLogger(wxListCtrl* logList) : ILogger() {
        wxASSERT(NULL != logList);
        m_logList = logList;
    };    

    virtual void Fatal(const char* file, long line, ATF_ERROR code, 
                       const char* msg, size_t length = 0, 
                       unsigned char* dump = NULL);

    virtual void Error(const char* file, long line, ATF_ERROR code, 
                       const char* msg, size_t length = 0, 
                       const void* dump = NULL);
    virtual void Warn(const char* file, long line, ATF_ERROR code, 
                      const char* msg, size_t length = 0, 
                      const void* dump = NULL);
    virtual void Info(const char* file, long line, ATF_ERROR code, 
                      const char* msg, size_t length = 0, 
                      const void* dump = NULL);
    virtual void Debug(const char* file, long line, ATF_ERROR code, 
                       const char* msg, size_t length = 0, 
                       const void* dump = NULL);
    virtual void Dump(const char* file, long line, ATF_ERROR code, 
                      const char* msg, size_t length = 0, 
                      const void* dump = NULL);
    virtual void DumpString(const char* file, long line, ATF_ERROR code, 
                      const char* msg, CString dump);
    virtual void DumpXML(const char* file, long line, ATF_ERROR code, 
                      const char* msg, const CXmlNode& dump);

protected:
    wxListCtrl* GetList() { 
        wxASSERT(NULL != m_logList);
        return m_logList; 
    };

private:
    void LogDump(unsigned char *dump, size_t len);
    void Log(LOG_LEVEL level,const char* file, long line, ATF_ERROR code, 
                      const char* msg, size_t length = 0, 
                      const void* dump = NULL);
private:
    wxListCtrl* m_logList;
};

#endif /* __ADVPCS_LIST_LOGGER_H__ */