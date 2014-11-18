#include <rxclaim/HeaderPanel.h>
#include <rxclaim/Messages.h>
#include <rxclaim/xmldescriptor.h>

#define DATE "date"
#define CHOICE "choice"
#define DATE_MAX_SIZE 10

//
// HeaderPanel
//
HeaderPanel::HeaderPanel(wxWindow* parent, Header* header)
    : wxScrolledWindow(parent, -1, wxDefaultPosition, wxDefaultSize, wxSUNKEN_BORDER | wxTAB_TRAVERSAL), 
      m_header(header)
{
    m_controls = new wxList();
    m_fields = new wxList();
    CreateFromHeader();
};
    
void HeaderPanel::ApplyData()
{
    for (size_t i=0; i<GetFields()->GetCount(); i++) {
        HeaderField* field = (HeaderField*)GetFields()->Item(i)->GetData();
        wxControl* control = (wxControl*)GetControls()->Item(i)->GetData();
        XmlElementDescriptor* d = (XmlElementDescriptor*)field->GetDescriptor();
        field->SetValue(d->CutValue(control->GetLabel()));
    }
};
    
void HeaderPanel::RefreshData()
{
    for (size_t i=0; i<GetFields()->GetCount(); i++) {
        HeaderField* field = (HeaderField*)GetFields()->Item(i)->GetData();
        wxControl* control = (wxControl*)GetControls()->Item(i)->GetData();
        ElementDescriptor* d = field->GetDescriptor();
        wxString& value = field->GetValue();
        if ( d->GetType().Cmp(CHOICE) == 0 ) {
            wxChoice* choice = (wxChoice*)control;
//            choice->SetStringSelection(value);
//            control->SetLabel(value);
        } else {
            control->SetLabel(value);
        }
    }
};
    
void HeaderPanel::CreateFromHeader()
{
    (void)new wxStaticText(this, -1, HEADER_PANEL_PROPOSE);
    wxColour REQUIRED_NAVY = wxColour(35, 35, 142);
    int textOffset = 20;
    int ctrlOffset = 105;
    int rowOffset = 20;
    int topOffset = 30;
    for (int i=0; i<GetHeader()->GetSize(); i++) {
        HeaderField* field = GetHeader()->GetField(i);
        ElementDescriptor* d = field->GetDescriptor();
        if ( d->IsEnabled() ) {
            wxStaticText* text = 
                new wxStaticText(this, -1, d->GetName(), wxPoint(textOffset, topOffset));
            if ( d->IsRequired() ) {
                text->SetForegroundColour(REQUIRED_NAVY);
            }
            int ctrlWidth = (d->GetMaxSize()>10) ? 150 : ((d->GetType().Cmp(CHOICE) == 0 ) ? 150 : (d->GetMaxSize() * 15));
            wxControl* control = NULL;
            if ( d->GetType().Cmp(CHOICE) == 0 ) {
                wxChoice* choice = new wxChoice(this, -1,
                    wxPoint(ctrlOffset, topOffset), wxSize(ctrlWidth, -1));
                for (size_t i=0; i<d->GetChoices().GetCount(); i++) {
                    choice->Append(d->GetChoices().Item(i));
                }
//                choice->SetStringSelection(field->GetValue());
                control = choice;
            } else {
                wxTextCtrl* tc = new wxTextCtrl(this, -1, field->GetValue(),
                    wxPoint(ctrlOffset, topOffset), wxSize(ctrlWidth, -1));
                if ( d->GetType().Cmp(DATE) == 0 ) {
                    tc->SetMaxLength(DATE_MAX_SIZE);
                } else {
                    tc->SetMaxLength(d->GetMaxSize());
                }
                control = tc;
            }

            m_fields->Append((wxObject*)field);
            m_controls->Append(control);
            topOffset += rowOffset;
        }
    }
    SetScrollbars(-1,5,-1,topOffset/5+1);    
};

