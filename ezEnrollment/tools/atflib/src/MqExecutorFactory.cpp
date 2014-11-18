/*
 *  $RCSfile: MqExecutorFactory.cpp,v $
 *
 *  $Revision: 1.3 $
 *
 *  last change: $Date: 2003/09/09 07:44:32 $
 */
// MqExecutorFactory.cpp: implementation of the CMqExecutorFactory class.
//
//////////////////////////////////////////////////////////////////////

#include <AFXWIN.H>
#include <WTYPES.H>
#include <OBJIDL.H>
#include <mq.h>

#include <atf/AssertException.h>
#include <atf/Util.h> 
#include <atf/MqException.h>
#include <atf/MqExecutorFactory.h>
#include <atf/MqExecutor.h>
#include <atf/Const.h>

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////
static const char * FORMAT_NAME_CFG = "format-name";
static const char * PATH_NAME_CFG = "path-name";
static const char * FORSE_CFG = "forse-create";

CMqExecutorFactory::CMqExecutorFactory() : m_queue(NULL) {
};

CMqExecutorFactory::~CMqExecutorFactory() {
    if ( NULL != m_queue ) {
        MQCloseQueue(m_queue);
        m_queue = NULL;
    }
};

void CMqExecutorFactory::Configure(ICfg &cfg) {
    m_formatName  = cfg.GetParam(FORMAT_NAME_CFG);
    m_pathName    = cfg.GetParam(PATH_NAME_CFG);
    m_forseCreate = cfg.GetParamAsBool(FORSE_CFG);
    if ( cfg.HasParam(ATF_DONE_DIR_CFG) ) {
        m_doneDir = cfg.GetParam(ATF_DONE_DIR_CFG);
    }
    if ( cfg.HasParam(ATF_ERROR_DIR_CFG) ) {
        m_errorDir = cfg.GetParam(ATF_ERROR_DIR_CFG);
    }
};

void CMqExecutorFactory::Initialize() {
    size_t i=0;

    DWORD len = m_formatName.GetLength();
    wchar_t * format = new wchar_t[len+1];
    CWCharHolder formatHolder(format);
    CharToWchar(m_formatName, format);
    
    len = m_pathName.GetLength();
    wchar_t * path = new wchar_t[len+1];
    CWCharHolder pathHolder(path);
    CharToWchar(m_pathName, path);

    MQQUEUEPROPS props;
    PROPVARIANT aVariant[1];
    QUEUEPROPID aPropId[1];

    aPropId[0] = PROPID_Q_PATHNAME;    // Property identifier
    aVariant[0].vt = VT_LPWSTR;        // Type indicator
    aVariant[0].pwszVal = path;
    
    props.cProp = 1;           // Number of properties
    props.aPropID = aPropId;             // Ids of properties
    props.aPropVar = aVariant;           // Values of properties
    props.aStatus = NULL;                // No error reports

    if ( m_forseCreate ) {
        HRESULT result = MQCreateQueue(NULL, &props, format, &len );
        if ( MQ_INFORMATION_FORMATNAME_BUFFER_TOO_SMALL != result ) {
            if ( MQ_OK != result && MQ_ERROR_QUEUE_EXISTS != result ) {
                char s[20];
                sprintf(s, "%ld", result);
                CString err("Can't create MSMQ with error [");
                err += s;
                err += "]";
                LOG_WARN(GetLogger(), ATF_WARNING,err);
            }
        }
    }
    HRESULT result =  MQOpenQueue(format, MQ_SEND_ACCESS, MQ_DENY_NONE, &m_queue);
    if ( result != MQ_OK ) {
        THROW_MQ_EXCEPTION(result, "Can't open MsQueue");
    }

};

IExecutor * CMqExecutorFactory::Create() {
    CMqExecutor *exec = new CMqExecutor();
    exec->EnableLogging(m_logger);
    exec->Initialize(m_doneDir, m_errorDir, m_queue);

    LOG_INFO(GetLogger(), ATF_INFO, "CMqExecutorFactory: CMqExecutor instance created.");
    return exec;
};
