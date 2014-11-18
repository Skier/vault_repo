/*
 *  $RCSfile: FieldEditor.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __ADVPCS_FIELD_EDITOR_H__
#define __ADVPCS_FIELD_EDITOR_H__

/* -------------------------- header place ---------------------------------- */
#include <advpcs/Document.h>
/* -------------------------- implementation place -------------------------- */

class FieldEditor {
public:
    FieldEditor(Field& field, wxWindow* parent, wxWindowID id, const wxPoint& pos = wxDefaultPosition, const wxSize& size = wxDefaultSize);
    virtual ~FieldEditor();


    virtual void PostValue();
    virtual void RefreshView();

    virtual wxString GetBufferValue();
    virtual void SetBufferValue(const wxString& value);

protected:
    Field& GetField() { return m_field; };
    wxControl& GetControl() {
        wxASSERT(NULL != m_control);
        return *m_control;
    };

private:
    Field& m_field;
    wxControl* m_control;
};

#endif /* __ADVPCS_FIELD_EDITOR_H__ */
