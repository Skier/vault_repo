/*
 *  $RCSfile: Logger.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// Logger.h: interface for the CLogger class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_LOGGER_H__5067B1B8_5419_4B41_9295_84B176F87C14__INCLUDED_)
#define AFX_LOGGER_H__5067B1B8_5419_4B41_9295_84B176F87C14__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/Exception.h>
#include <atf/CfgException.h>
#include <atf/ErrorConst.h>
#include <stdio.h>

#pragma warning( disable : 4290 ) 

class ICfg;
class CXmlNode;

extern const char* ATF_LOG;
extern const char* ATF_LOG_TYPE;
extern const char* ATF_LOG_LEVEL;
extern const char* ATF_LOG_LEVEL_NONE;
extern const char* ATF_LOG_LEVEL_FATAL;
extern const char* ATF_LOG_LEVEL_ERROR;
extern const char* ATF_LOG_LEVEL_WARN;
extern const char* ATF_LOG_LEVEL_INFO;
extern const char* ATF_LOG_LEVEL_DEBUG;
extern const char* ATF_LOG_LEVEL_DUMP;


extern const char* ATF_LOG_TYPE_CERR;
extern const char* ATF_LOG_TYPE_COUT;
extern const char* ATF_LOG_TYPE_FILE;
extern const char* ATF_LOG_FILE_NAME;
extern const char* ATF_LOG_TYPE_EVENT;
extern const char* ATF_LOG_EVENT_NAME;


enum LOG_LEVEL { LOG_NONE, LOG_FATAL, LOG_ERROR, LOG_WARN, LOG_INFO, LOG_DEBUG, 
                 LOG_DUMP };

class ILogger {
public:
    ILogger():m_level(LOG_DUMP){};
    virtual ~ILogger(){};

    LOG_LEVEL GetLevel() const {return m_level;};
    void SetLevel(LOG_LEVEL level) { m_level = level;};

    bool IsDumpEnabled()   const {return GetLevel() >= LOG_DUMP; };
    bool IsDebugEnabled()  const {return GetLevel() >= LOG_DEBUG;};
    bool IsInfoEnabled()   const {return GetLevel() >= LOG_INFO; };
    bool IsWarnEnabled()   const {return GetLevel() >= LOG_WARN; };
    bool IsErrorEnabled()  const {return GetLevel() >= LOG_ERROR;};
    bool IsFatalEnabled()  const {return GetLevel() >= LOG_FATAL;};

    virtual void Fatal(const char* file, long line, ATF_ERROR code, 
                       const char* msg, size_t length = 0, 
                       unsigned char* dump = NULL) = 0;

    virtual void Error(const char* file, long line, ATF_ERROR code, 
                       const char* msg, size_t length = 0, 
                       const void* dump = NULL) = 0;
    virtual void Warn(const char* file, long line, ATF_ERROR code, 
                      const char* msg, size_t length = 0, 
                      const void* dump = NULL) = 0;
    virtual void Info(const char* file, long line, ATF_ERROR code, 
                      const char* msg, size_t length = 0, 
                      const void* dump = NULL) = 0;
    virtual void Debug(const char* file, long line, ATF_ERROR code, 
                       const char* msg, size_t length = 0, 
                       const void* dump = NULL) = 0;
    virtual void Dump(const char* file, long line, ATF_ERROR code, 
                      const char* msg, size_t length = 0, 
                      const void* dump = NULL) = 0;
    virtual void DumpString(const char* file, long line, ATF_ERROR code, 
                      const char* msg, CString dump) = 0;
    virtual void DumpXML(const char* file, long line, ATF_ERROR code, 
                      const char* msg, const CXmlNode& dump) = 0;

public:
    static ILogger * CreateLogger(ICfg &cfg) throw (CCfgException);
protected:
    const char * LevelToPrefix(LOG_LEVEL level);                      
private:
    LOG_LEVEL m_level;
};



#define LOG_DUMP_XML(logger, message, xml) \
{\
    if( logger.IsDumpEnabled() ) { \
        logger.DumpXML(__FILE__, __LINE__, ATF_DEBUG, message, xml); \
    }\
}

#define LOG_DUMP_STRING(logger, message, str) \
{\
    if( logger.IsDumpEnabled() ) { \
        logger.DumpString(__FILE__, __LINE__, ATF_DEBUG, message, str); \
    }\
}

#define LOG_DUMP(logger, message, len, buffer) \
{\
    if( logger.IsDumpEnabled() ) { \
        logger.Dump(__FILE__, __LINE__, ATF_DEBUG, message, len, buffer); \
    }\
}

#define LOG_DEBUG(logger, code, message) \
{\
    if( logger.IsDebugEnabled() ) { \
        logger.Debug(__FILE__, __LINE__, code, message); \
    }\
}

#define LOG_INFO(logger, code, message) \
{\
    if( logger.IsInfoEnabled() ) { \
        logger.Info(__FILE__, __LINE__, code, message); \
    }\
}

#define LOG_WARN(logger, code, message) \
{ \
    if( logger.IsWarnEnabled() ) { \
        logger.Warn(__FILE__, __LINE__, code, message); \
    }\
}

#define LOG_ERROR(logger, code, message) \
{\
    if( logger.IsErrorEnabled() ) { \
        logger.Error(__FILE__, __LINE__, code, message); \
    }\
}

#define LOG_ERROR_DUMP(logger, code, message, length, body) \
{\
    if( logger.IsErrorEnabled() ) { \
        logger.Error(__FILE__, __LINE__, code, message, length, body); \
    }\
}

#define LOG_FATAL(logger, code, message) \
{\
    if( logger.IsFatalEnabled() ) { \
        logger.Fatal(__FILE__, __LINE__, code, message); \
    }\
}



#endif // !defined(AFX_LOGGER_H__5067B1B8_5419_4B41_9295_84B176F87C14__INCLUDED_)

