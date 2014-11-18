/*
 *  $RCSfile: EdiStatusStream.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __ADVPCS_EDISTATUS_STREAM_H__
#define __ADVPCS_EDISTATUS_STREAM_H__

class EdiStatus;

class EdiStatusStream {
public:
    virtual EdiStatus* GetNext() = 0;
    virtual void Reset() = 0;
};

#endif /* __ADVPCS_EDISTATUS_STREAM_H__ */
