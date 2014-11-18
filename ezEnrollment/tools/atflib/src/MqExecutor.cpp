/*
 *  $RCSfile: MqExecutor.cpp,v $
 *
 *  $Revision: 1.3 $
 *
 *  last change: $Date: 2003/09/09 07:44:32 $
 */
// MqExecutor.cpp: implementation of the CMqExecutor class.
//
//////////////////////////////////////////////////////////////////////

#include <AFXWIN.H>
#include <WTYPES.H>
#include <OBJIDL.H>
#include <mq.h>

#include <atf/AssertException.h>
#include <atf/Message.h> 
#include <atf/Util.h> 
#include <atf/ExceptionLogger.h>
#include <atf/MqException.h>
#include <atf/MqExecutor.h>
#include <atf/SystemException.h>
#include <atf/Thread.h>

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////
static const int NUM_OF_SEND_PROP=5;

CMqExecutor::CMqExecutor() 
    :IDoneExecutor(), m_queue(NULL) 
{};

CMqExecutor::~CMqExecutor() {
}

void CMqExecutor::Initialize(const CString& doneDir, const CString errorDir, HANDLE queue) {
    IDoneExecutor::Initialize(doneDir, errorDir);
    m_queue = queue;
};

void CMqExecutor::Execute(IMessage& msg, ILogger& logger) {

    unsigned char correlationId[PROPID_M_CORRELATIONID_SIZE];
    memset(correlationId, 0, PROPID_M_CORRELATIONID_SIZE);

    DWORD  corrId = atol(msg.GetTransactionID());
    DWORD* corrIdBody = (DWORD *) &(correlationId[PROPID_M_CORRELATIONID_SIZE - sizeof(DWORD)]);

    *corrIdBody = corrId;

    MQMSGPROPS    props;
    MSGPROPID     propId[NUM_OF_SEND_PROP];
    MQPROPVARIANT propVar[NUM_OF_SEND_PROP];

    wchar_t * tranId = new wchar_t[msg.GetTransactionID().GetLength()+1];
    CWCharHolder tranIdHolder(tranId);
    CharToWchar(msg.GetTransactionID(), tranId);

    propId[0] = PROPID_M_LABEL;               
    propVar[0].vt = VT_LPWSTR;                
    propVar[0].pwszVal = tranId;  

    propId[1] = PROPID_M_BODY;                 
    propVar[1].vt = VT_VECTOR | VT_UI1;        
    propVar[1].caub.pElems = (unsigned char*)msg.GetBody();
    propVar[1].caub.cElems = msg.GetLength();

    propId[2] = PROPID_M_BODY_TYPE;            
    propVar[2].vt = VT_UI4;                    
    propVar[2].ulVal = VT_LPSTR;

    DWORD appData = atol(msg.GetType());

    propId[3] = PROPID_M_APPSPECIFIC;            
    propVar[3].vt = VT_UI4;                    
    propVar[3].ulVal = appData;

    propId[4] = PROPID_M_CORRELATIONID;     
    propVar[4].vt = VT_VECTOR | VT_UI1;      
    propVar[4].caub.pElems = correlationId;
    propVar[4].caub.cElems = PROPID_M_CORRELATIONID_SIZE ;


    props.cProp = NUM_OF_SEND_PROP;    
    props.aPropID = propId;
    props.aPropVar = propVar;
    props.aStatus = NULL;
  
    HRESULT result = MQSendMessage(m_queue, &props, MQ_NO_TRANSACTION);
    if (result != MQ_OK) {
        CString msg;
        msg.Format("0x%08x",result); 
        THROW_MQ_EXCEPTION(result, "Can't send message. Error="+msg);
    }
    return;
};
