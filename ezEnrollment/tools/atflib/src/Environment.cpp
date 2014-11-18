/*
 *  $RCSfile: Environment.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// Environment.cpp: implementation of the CEnvironment class.
//
//////////////////////////////////////////////////////////////////////

#include <iostream>
#include <fstream>
#include <atf/Environment.h>
#include <atf/StreamLogger.h>

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CStreamLogger c_defaultLogger(cerr);

CEnvironment c_defaultEnvironment;

CEnvironment::CEnvironment():m_logger(NULL){};

ILogger* CEnvironment::GetLogger(){
    if ( NULL != m_logger ) {
        return m_logger;
    } else {
        return &c_defaultLogger;
    }
}

ILogger& CEnvironment::GetRefLogger() {
    if ( NULL != m_logger ) {
        return *m_logger;
    } else {
        return c_defaultLogger;
    }
};
