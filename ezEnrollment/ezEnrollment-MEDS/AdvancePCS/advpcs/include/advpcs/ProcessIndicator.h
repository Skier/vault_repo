/*
 *  $RCSfile: ProcessIndicator.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __ADVPCS_PROCESS_INDICATOR_H__
#define __ADVPCS_PROCESS_INDICATOR_H__

class ProcessIndicator {
public:
    virtual void StartProcess(long maxState = 100, const wxString& processName = wxEmptyString) = 0;
    virtual void SetState(long state, const wxString& description = wxEmptyString) = 0;
    virtual void FinishProcess(const wxString& description = wxEmptyString) = 0;
};

class AutoFinnisher {
public:
    AutoFinnisher(ProcessIndicator& process) : m_process(process){};
    ~AutoFinnisher() {
        m_process.FinishProcess();
    };
private:
    ProcessIndicator& m_process;
};

#endif /* __ADVPCS_PROCESS_INDICATOR_H__ */