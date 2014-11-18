#ifndef __ADVPCS_STRING_FIELD_EDITOR_H__ 
#define __ADVPCS_STRING_FIELD_EDITOR_H__ 

#include <wx/textctrl.h>
#include <advpcs/FieldEditor.h>

class StringFieldEditor : public FieldEditor {
public:
    StringFieldEditor(Field& fld, wxWindow* parent, wxWindowID id, const wxPoint& pos = wxDefaultPosition, const wxSize& size = wxDefaultSize, long style = 0, const wxValidator& validator = wxDefaultValidator, const wxString& name = wxTextCtrlNameStr)
        : wxTextCtrl(parent, id, "", pos, size, style, const wxValidator& validator = wxDefaultValidator, const wxString& name = wxTextCtrlNameStr);
    wxString GetBufferValue() { return GetValue() };
    void SetBufferValue(const wxString& value) { SetValue(value); };
    wxWindow* GetEditWindow() { return this; };

private:
    wxTextCtrl* m_control;
};

#endif /* __ADVPCS_STRING_FIELD_EDITOR_H__ */