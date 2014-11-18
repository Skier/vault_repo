/*
 *  $RCSfile: ErrorConst.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
#ifndef __ATF_ERROR_CONST_H__
#define __ATF_ERROR_CONST_H__

typedef unsigned long ATF_ERROR;

extern const ATF_ERROR ATF_DEBUG;
extern const ATF_ERROR ATF_WARNING;
extern const ATF_ERROR ATF_INFO;

extern const ATF_ERROR ATF_UNKNOWN_ERR;
extern const ATF_ERROR ATF_ASSERT_ERR;
extern const ATF_ERROR ATF_INVALID_CFG_ERR;
extern const ATF_ERROR ATF_SYSTEM_ERR;
extern const ATF_ERROR ATF_MQ_ERR;
extern const ATF_ERROR ATF_NOTFOUND_ERR;
extern const ATF_ERROR ATF_NOMEMORY_ERR;
extern const ATF_ERROR ATF_SOCKET_ERR;
extern const ATF_ERROR ATF_NO_MORE_DATA_ERR;
extern const ATF_ERROR ATF_WRONG_TRANSID_ERR;

extern const ATF_ERROR ATF_SERVICE_ERR;
extern const ATF_ERROR ATF_NO_IMPLEMENT_ERR;

extern const ATF_ERROR ATF_USER_ERR;

#endif /*__ATF_ERROR_CONST_H__*/
