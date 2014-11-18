/*
 *  $RCSfile: AgentException.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __ADVPCS_AGENT_EXCEPTION_H__
#define __ADVPCS_AGENT_EXCEPTION_H__

/* -------------------------- header place ---------------------------------- */
#include <atf/Exception.h>
/* -------------------------- implementation place -------------------------- */

extern const ATF_ERROR ADVPCS_BASE;
extern const ATF_ERROR ADVPCS_LOGIN_OK;
extern const ATF_ERROR ADVPCS_LOGIN_AUTH_ERR;
extern const ATF_ERROR ADVPCS_LOGIN_PERMISSION_ERR;
extern const ATF_ERROR ADVPCS_LOGIN_PSWD_EXPIRED_ERR;
extern const ATF_ERROR ADVPCS_LOGIN_INITAL_SIGNON_ERR;
extern const ATF_ERROR ADVPCS_LOGIN_EXCEEDMAX_ERR;
extern const ATF_ERROR ADVPCS_LOGIN_INACIVE_ERR;
extern const ATF_ERROR ADVPCS_LOGIN_EXPIRED_ERR;
extern const ATF_ERROR ADVPCS_LOGIN_DISABLED_ERR;

extern const ATF_ERROR ADVPCS_PSWD_OK;
extern const ATF_ERROR ADVPCS_PSWD_INVALID_ERR;
extern const ATF_ERROR ADVPCS_PSWD_DUPLICATE_ERR;

extern const ATF_ERROR ADVPCS_POST_FILE_RECEIVED_OK;
extern const ATF_ERROR ADVPCS_POST_AUTH_ERR;
extern const ATF_ERROR ADVPCS_POST_PERMISSION_ERR;
extern const ATF_ERROR ADVPCS_POST_FILE_NAME_ERR;
extern const ATF_ERROR ADVPCS_POST_FORMAT_ERR;
extern const ATF_ERROR ADVPCS_POST_DUPLICATE_ERR;
extern const ATF_ERROR ADVPCS_POST_TRANSMISSION_ERR;

extern const ATF_ERROR ADVPCS_STATUS_OK;
// extern const ATF_ERROR ADVPCS_STATUS_AUTH_ERR;
// extern const ATF_ERROR ADVPCS_STATUS_PERMISSION_ERR;
extern const ATF_ERROR ADVPCS_STATUS_NOT_AVAILABLE_ERR;

extern const ATF_ERROR ADVPCS_REPLY_FORMAT_ERR;

class AgentException : public CAtfException {
public:
    AgentException(const char *fileName, int lineNo, ATF_ERROR code);

protected:
    wxString GetAgentMessage(ATF_ERROR code);
};


#define THROW_AGENT_EXCEPTION(code) \
    throw AgentException(__FILE__, __LINE__, code);

#endif /* __ADVPCS_AGENT_EXCEPTION_H__ */