/*
 *  $RCSfile: Logger.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
#include <atf/Logger.h>
#include <atf/Environment.h>
#include <atf/StreamLogger.h>
#include <atf/EventLogger.h>
#include <atf/CfgException.h>
#include <atf/FileLogger.h>
#include <atf/AssertException.h>
#include <atf/Cfg.h>
#include <fstream>


const char* ATF_LOG             = "logger";
const char* ATF_LOG_TYPE        = "type";
const char* ATF_LOG_LEVEL       = "level";
const char* ATF_LOG_LEVEL_NONE  = "none";
const char* ATF_LOG_LEVEL_FATAL = "fatal";
const char* ATF_LOG_LEVEL_ERROR = "error";
const char* ATF_LOG_LEVEL_WARN  = "warn";
const char* ATF_LOG_LEVEL_INFO  = "info";
const char* ATF_LOG_LEVEL_DEBUG = "debug";
const char* ATF_LOG_LEVEL_DUMP  = "dump";


const char *  NONE_PREFIX = "NONE :";
const char * FATAL_PREFIX = "FATAL:";
const char * ERROR_PREFIX = "ERROR:";
const char *  WARN_PREFIX = "WARN :";
const char *  INFO_PREFIX = "INFO :";
const char * DEBUG_PREFIX = "DEBUG:";
const char *  DUMP_PREFIX = "DUMP :";

const char* ATF_LOG_TYPE_CERR   = "stderr";
const char* ATF_LOG_TYPE_COUT   = "stdout";
const char* ATF_LOG_TYPE_FILE   = "file";
const char* ATF_LOG_FILE_NAME   = "name";
const char* ATF_LOG_TYPE_EVENT   = "event";
const char* ATF_LOG_EVENT_NAME   = "name";


ILogger * ILogger::CreateLogger(ICfg &cfg) {
    CString type = cfg.GetParam(ATF_LOG_TYPE, ATF_LOG_TYPE_CERR);
    CString level = cfg.GetParam(ATF_LOG_LEVEL, ATF_LOG_LEVEL_DEBUG);

    LOG_LEVEL l = LOG_DEBUG;
    if ( 0 == level.CompareNoCase(ATF_LOG_LEVEL_NONE) ) {
        l = LOG_NONE;
    } else if ( 0 == level.CompareNoCase(ATF_LOG_LEVEL_FATAL) ) {
        l = LOG_FATAL;
    } else if ( 0 == level.CompareNoCase(ATF_LOG_LEVEL_ERROR) ) {
        l = LOG_ERROR;
    } else if ( 0 == level.CompareNoCase(ATF_LOG_LEVEL_WARN) ) {
        l = LOG_WARN;
    } else if ( 0 == level.CompareNoCase(ATF_LOG_LEVEL_INFO) ) {
        l = LOG_INFO;
    } else if ( 0 == level.CompareNoCase(ATF_LOG_LEVEL_DEBUG) ) {
        l = LOG_DEBUG;
    } else if ( 0 == level.CompareNoCase(ATF_LOG_LEVEL_DUMP) ) { 
        l = LOG_DUMP;
    } else {
        CString msg = "Unknown log level:\"";
        msg += level;
        msg += "\"";
        THROW_CFG_EXCEPTION(msg);
    }
 
    ILogger *logger = NULL;

    if ( 0 == type.CompareNoCase(ATF_LOG_TYPE_CERR) ) {
        logger = new CStreamLogger(cerr);
    } else if ( 0 == type.CompareNoCase(ATF_LOG_TYPE_COUT) ) {
        logger = new CStreamLogger(cout);
    } else if ( 0 == type.CompareNoCase(ATF_LOG_TYPE_FILE) ) {
        CString filename = cfg.GetParam(ATF_LOG_FILE_NAME);
        ostream * os = new ofstream(filename);
        logger = new CFileLogger(os);
    } else if ( 0 == type.CompareNoCase(ATF_LOG_TYPE_EVENT) ) {
        CString eventname = cfg.GetParam(ATF_LOG_EVENT_NAME);
        if ( eventname.IsEmpty() ) {
            THROW_CFG_EXCEPTION("Event name not defined.");
        }
        logger = new CEventLogger(eventname);
    } else {
        CString msg = "Unknown log type:\"";
        msg += type;
        msg += "\"";
        THROW_CFG_EXCEPTION(msg);
    }
    logger->SetLevel(l);
    return logger; 

};

const char * ILogger::LevelToPrefix(LOG_LEVEL level) {
    switch ( level ) {
        case LOG_NONE:
            return NONE_PREFIX;
            break;
        case LOG_FATAL:
            return FATAL_PREFIX;
            break;
        case LOG_ERROR:
            return ERROR_PREFIX;
            break;
        case LOG_WARN:
            return WARN_PREFIX;
            break;
        case LOG_INFO:
            return INFO_PREFIX;
            break;
        case LOG_DEBUG:
            return DEBUG_PREFIX;
            break;
        case LOG_DUMP:
            return DUMP_PREFIX;
            break;
        default:
            ATF_ASSERT_MSG(false, "Unknow log level");
            return NULL;
    }
};                      
