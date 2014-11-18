/*
 *  $RCSfile: AgentException.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#include "wx/wxprec.h"

#ifdef __BORLANDC__
    #pragma hdrstop
#endif

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif

/* -------------------------- header place ---------------------------------- */
#include <advpcs/AgentException.h>
#include <advpcs/Resources.h>
/* -------------------------- implementation place -------------------------- */

const ATF_ERROR ADVPCS_BASE                     = ATF_USER_ERR + 1000;
const ATF_ERROR ADVPCS_LOGIN_OK                 = ADVPCS_BASE  + 1000;
const ATF_ERROR ADVPCS_LOGIN_AUTH_ERR           = ADVPCS_BASE  + 1001;
const ATF_ERROR ADVPCS_LOGIN_PERMISSION_ERR     = ADVPCS_BASE  + 1002;
const ATF_ERROR ADVPCS_LOGIN_PSWD_EXPIRED_ERR   = ADVPCS_BASE  + 1003;
const ATF_ERROR ADVPCS_LOGIN_INITAL_SIGNON_ERR  = ADVPCS_BASE  + 1004;
const ATF_ERROR ADVPCS_LOGIN_EXCEEDMAX_ERR      = ADVPCS_BASE  + 1005;
const ATF_ERROR ADVPCS_LOGIN_INACIVE_ERR        = ADVPCS_BASE  + 1006;
const ATF_ERROR ADVPCS_LOGIN_EXPIRED_ERR        = ADVPCS_BASE  + 1007;
const ATF_ERROR ADVPCS_LOGIN_DISABLED_ERR       = ADVPCS_BASE  + 1008;


const ATF_ERROR ADVPCS_PSWD_OK                  = ADVPCS_BASE  + 1500;
const ATF_ERROR ADVPCS_PSWD_INVALID_ERR         = ADVPCS_BASE  + 1501;
const ATF_ERROR ADVPCS_PSWD_DUPLICATE_ERR       = ADVPCS_BASE  + 1502;


const ATF_ERROR ADVPCS_POST_FILE_RECEIVED_OK    = ADVPCS_BASE  + 2000;
const ATF_ERROR ADVPCS_POST_AUTH_ERR            = ADVPCS_BASE  + 2001;
const ATF_ERROR ADVPCS_POST_PERMISSION_ERR      = ADVPCS_BASE  + 2002;
const ATF_ERROR ADVPCS_POST_FILE_NAME_ERR       = ADVPCS_BASE  + 2003;
const ATF_ERROR ADVPCS_POST_FORMAT_ERR          = ADVPCS_BASE  + 2004;
const ATF_ERROR ADVPCS_POST_DUPLICATE_ERR       = ADVPCS_BASE  + 2005;
const ATF_ERROR ADVPCS_POST_TRANSMISSION_ERR    = ADVPCS_BASE  + 2006;

const ATF_ERROR ADVPCS_STATUS_OK                = ADVPCS_BASE  + 3000;
//const ATF_ERROR ADVPCS_STATUS_AUTH_ERR          = ADVPCS_BASE  + 3001;
//const ATF_ERROR ADVPCS_STATUS_PERMISSION_ERR    = ADVPCS_BASE  + 3002;
const ATF_ERROR ADVPCS_STATUS_NOT_AVAILABLE_ERR = ADVPCS_BASE  + 3001; 

const ATF_ERROR ADVPCS_REPLY_FORMAT_ERR  = ATF_USER_ERR + 1;

AgentException::AgentException(const char *fileName, int lineNo, ATF_ERROR code)
    : CAtfException(fileName, lineNo, code+ADVPCS_BASE, GetAgentMessage(code + ADVPCS_BASE))
{    
};


wxString AgentException::GetAgentMessage(ATF_ERROR code) {
    if ( code == ADVPCS_LOGIN_AUTH_ERR          ) return ADVPCS_LOGIN_AUTH_ERR_MSG;
    if ( code == ADVPCS_LOGIN_PSWD_EXPIRED_ERR  ) return ADVPCS_LOGIN_PSWD_EXPIRED_ERR_MSG ;
    if ( code == ADVPCS_LOGIN_INITAL_SIGNON_ERR ) return ADVPCS_LOGIN_INITAL_SIGNON_ERR_MSG;
    if ( code == ADVPCS_LOGIN_EXCEEDMAX_ERR     ) return ADVPCS_LOGIN_EXCEEDMAX_ERR_MSG;
    if ( code == ADVPCS_LOGIN_INACIVE_ERR       ) return ADVPCS_LOGIN_INACIVE_ERR_MSG;
    if ( code == ADVPCS_LOGIN_EXPIRED_ERR       ) return ADVPCS_LOGIN_EXPIRED_ERR_MSG;
    if ( code == ADVPCS_LOGIN_DISABLED_ERR      ) return ADVPCS_LOGIN_DISABLED_ERR_MSG;
    if ( code == ADVPCS_POST_AUTH_ERR           ) return ADVPCS_POST_AUTH_ERR_MSG;
    if ( code == ADVPCS_POST_PERMISSION_ERR     ) return ADVPCS_POST_PERMISSION_ERR_MSG;
    if ( code == ADVPCS_POST_FILE_NAME_ERR      ) return ADVPCS_POST_FILE_NAME_ERR_MSG;
    if ( code == ADVPCS_POST_FORMAT_ERR         ) return ADVPCS_POST_FORMAT_ERR_MSG;
    if ( code == ADVPCS_POST_DUPLICATE_ERR      ) return ADVPCS_POST_DUPLICATE_ERR_MSG;
    if ( code == ADVPCS_POST_TRANSMISSION_ERR   ) return ADVPCS_POST_TRANSMISSION_ERR_MSG;
//    if ( code == ADVPCS_STATUS_AUTH_ERR         ) return ADVPCS_STATUS_AUTH_ERR_MSG;
//    if ( code == ADVPCS_STATUS_PERMISSION_ERR   ) return ADVPCS_STATUS_PERMISSION_ERR_MSG;
    if ( code == ADVPCS_STATUS_NOT_AVAILABLE_ERR) return ADVPCS_STATUS_NOT_AVAILABLE_ERR_MSG;
    if ( code == ADVPCS_REPLY_FORMAT_ERR        ) return ADVPCS_REPLY_FORMAT_ERR_MSG; 

    return wxString::Format(ADVPCS_STATUS_UNKNOWN_CODE_MSG, code - ADVPCS_BASE);
};