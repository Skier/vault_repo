/*
 *  $RCSfile: Disabler.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __ADVPCS_DISABLER_H__
#define __ADVPCS_DISABLER_H__


class Disabler {
public:
    Disabler(wxWindow* w) : m_w(NULL) {
        wxASSERT(NULL != w);
        m_w = w;
        m_w->Enable(false);
    };
    ~Disabler() {
        m_w->Enable(true);
    };
private:
    wxWindow* m_w;
};

#endif /* __ADVPCS_DISABLER_H__ */

