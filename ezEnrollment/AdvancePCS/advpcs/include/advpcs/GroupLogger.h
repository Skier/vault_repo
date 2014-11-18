/*
 *  $RCSfile: GroupLogger.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __ADVPCS_GROUP_LOGGER_H__
#define __ADVPCS_GROUP_LOGGER_H__


/* -------------------------- header place ---------------------------------- */
#include <atf/Logger.h>
#include <vector>
/* -------------------------- implementation place -------------------------- */

class GroupLogger : public ILogger {
public:
    GroupLogger() : ILogger() {};  
    ~GroupLogger();

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

    void Add(ILogger* logger) {
        m_group.push_back(logger);
    };


private:
    std::vector<ILogger*> m_group;
};

#endif /* __ADVPCS_LIST_LOGGER_H__ */