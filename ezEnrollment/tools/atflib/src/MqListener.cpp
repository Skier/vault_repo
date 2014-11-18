/*
 *  $RCSfile: MqListener.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// MqListener.cpp: implementation of the CMqListener class.
//
//////////////////////////////////////////////////////////////////////

#include <AFXWIN.H>
#include <WTYPES.H>
#include <OBJIDL.H>
#include <mq.h>

#include <atf/Mutex.h>
#include <atf/MqListener.h>
#include <atf/AssertException.h>
#include <atf/Message.h> 
#include <atf/Cfg.h>
#include <atf/Util.h> 
#include <atf/ExecutorPool.h>
#include <atf/ThreadPool.h> 
#include <atf/ThreadBodyRunnable.h>
#include <atf/ExceptionLogger.h>
#include <atf/PoolableThread.h>
#include <atf/MqException.h>

static const char* MSG_QUEUE_NOT_OPENED = "CMqListener.Stop: Queue not openned";
static const int NUM_OF_RECV_PROP = 6;
static const size_t MQ_MAX_LABEL_LEN = 1024;
static const size_t MQ_MAX_BODY_BUFFER = 0x400000;

static const char * FORMAT_NAME_CFG = "format-name";
static const char * PATH_NAME_CFG = "path-name";
static const char * FORSE_CFG = "forse-create";


//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////
CMqListener::CMqListener(CExecutorPool &executors, CThreadPool &threads) : 
    IListener(executors, threads), m_stopped(false), m_queue(NULL) 
{
};

CMqListener::~CMqListener() {
    if ( !m_stopped && NULL != m_queue ) {
        Stop();
    }
};

void CMqListener::Configure(ICfg & cfg) {
    m_formatName = cfg.GetParam(FORMAT_NAME_CFG);
    m_pathName = cfg.GetParam(PATH_NAME_CFG);
    m_forseCreate = cfg.GetParamAsBool(FORSE_CFG);
};

void CMqListener::Initialize() {
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
                THROW_MQ_EXCEPTION(result, err);
            }
        }
    }
    CharToWchar(m_formatName, format);
    HRESULT result =  MQOpenQueue(format, MQ_RECEIVE_ACCESS, MQ_DENY_NONE, &m_queue);
    if ( result != MQ_OK ) {
        THROW_MQ_EXCEPTION(result, "Can't open MsQueue");
    }

    LOG_INFO(GetLogger(), ATF_INFO, "MqListener initalized");
};

void CMqListener::Stop() {
    ATF_ASSERT_MSG(NULL != m_queue, MSG_QUEUE_NOT_OPENED);

    m_stopped = true;
    MQCloseQueue(m_queue);
    m_queue = NULL;
};

int CMqListener::Run(CThread * thisThread) {
    ATF_ASSERT_MSG(NULL != m_queue, MSG_QUEUE_NOT_OPENED);
    while (!m_stopped) { 
        try {
            IMessage * msg = ReceiveMessage();
            IExecutor * executor = m_executors.Get();
            CThread * proc = m_threads.Get();
            executor->SetTask(msg);
            IRunnable& body = proc->GetBody();
            ((CThreadBodyRunnable&)body).SetTask(executor, &m_executors);
            ((CPoolableThread*)proc)->Run();
        } catch (CMqException &me) {
            if ( m_stopped ) {
                break; // expected
            }
            CExceptionLogger::Log(GetLogger(), me);
        } catch (CSystemException &se) {
            CExceptionLogger::Log(GetLogger(), se);
        } catch (CAtfException &te) {
            CExceptionLogger::Log(GetLogger(), te);
        } catch (...) {
            LOG_ERROR(GetLogger(), ATF_UNKNOWN_ERR, "Unknown Exception was thrown");
        }
        LOG_DEBUG(GetLogger(), ATF_DEBUG, "MqListener loop finnished");
    }
    return 0;
};


IMessage * CMqListener::ReceiveMessage() {
    char    *bodyBuffer = new char[MQ_MAX_BODY_BUFFER];
    wchar_t *labelBuffer = new wchar_t[MQ_MAX_LABEL_LEN];

    DWORD appData = 0;

    int i = 0;

    for( i=0; i< MQ_MAX_BODY_BUFFER; i++ ) bodyBuffer[i]=0;
    for( i=0; i< MQ_MAX_LABEL_LEN; i++ ) labelBuffer[i]=0;


    MQMSGPROPS    props;
    MSGPROPID     propId[NUM_OF_RECV_PROP];
    MQPROPVARIANT propVar[NUM_OF_RECV_PROP];

    propId[0] = PROPID_M_BODY_SIZE;            
    propVar[0].vt = VT_NULL;                   

    propId[1] = PROPID_M_BODY;                 
    propVar[1].vt = VT_VECTOR | VT_UI1;        
    propVar[1].caub.pElems = (UCHAR*)bodyBuffer;
    propVar[1].caub.cElems = MQ_MAX_BODY_BUFFER;

    propId[2] = PROPID_M_LABEL_LEN;           
    propVar[2].vt =VT_UI4;                    
    propVar[2].ulVal = MQ_MAX_LABEL_LEN;  

    propId[3] = PROPID_M_LABEL;               
    propVar[3].vt = VT_LPWSTR;                
    propVar[3].pwszVal = labelBuffer;

    propId[4] = PROPID_M_APPSPECIFIC;               
    propVar[4].vt = VT_UI4;                
    propVar[4].ulVal = appData;

    propId[5] = PROPID_M_BODY_TYPE;            // Property ID
    propVar[5].vt = VT_NULL;                   // Type indicator

    props.cProp = NUM_OF_RECV_PROP;    
    props.aPropID = propId;
    props.aPropVar = propVar;
    props.aStatus = NULL;



    HRESULT result = MQReceiveMessage(m_queue, INFINITE, MQ_ACTION_RECEIVE, &props, NULL, NULL, NULL, NULL);
   
    if (result != MQ_OK) {
        THROW_MQ_EXCEPTION(result, "Can't receive message");
    }
 
    LOG_DUMP(GetLogger(), "MqListener: received", propVar[0].ulVal, bodyBuffer);

    
    CMessage * msg = NULL;
    CString body;

    switch( propVar[5].ulVal ) {
        case VT_BSTR:
        case VT_LPWSTR:
			body =(const wchar_t*)bodyBuffer;
            msg =  new CMessage((const unsigned char*)((const char*)body), body.GetLength()+1);
            break;
        default:
            msg =  new CMessage((const unsigned char*)bodyBuffer, propVar[0].ulVal);
    }

    msg->SetTransactionID(labelBuffer);

    char tmp[10];
    ltoa(propVar[4].ulVal, tmp, 10);
    msg->SetType(tmp);

    delete [] bodyBuffer;
    delete [] labelBuffer;
    return msg;
};

