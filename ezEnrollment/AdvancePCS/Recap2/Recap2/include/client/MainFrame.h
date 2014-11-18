#ifndef ___CLIENT_MAIN_FRAME___
#define ___CLIENT_MAIN_FRAME___

#include <wx/wxprec.h>
#include <wx/wizard.h>
#include <wx/valtext.h>
#include <wx/grid.h>
#include <wx/generic/gridctrl.h>

#include <wx/toolbar.h>
#include <wx/image.h>

#include <client/header.h>
#include <client/detail.h>
#include <client/composer.h>
#include <client/ClipbrdMatrix.h>
#include <client/descriptor.h>
#include <client/ComposerDescriptor.h>
#include <client/HeaderFrame.h>

enum
{
    Wizard_Quit = 100,
    Wizard_Run = 200,
    Header_View,
    Wizard_About = 1000,
    
    Detail_New,
    Detail_Open,
    Detail_Save,
    Detail_SaveAs,
    Detail_Quit,
    
    Detail_Copy,
    Detail_Paste,
    
    Detail_Search,

    Detail_Undo,
    
    Detail_SelectAll,

    Detail_AddRow,
    Detail_InsertRow,
    Detail_RemoveRow,

    Detail_Validate,
    Detail_Compose,
    Detail_Send, 

    ID_TOOLBAR,
    TOOL_ID_HELP,
    TOOL_ID_COPY,
    TOOL_ID_PASTE,

    TOOL_ID_SEARCH_STRING,
    TOOL_ID_SEARCH,

    TOOL_ID_UNDO,
    TOOL_ID_NEW,
    TOOL_ID_OPEN,
    TOOL_ID_SAVE,
    TOOL_ID_ADD,
    TOOL_ID_REMOVE,
    TOOL_ID_INSERT,

    TOOL_ID_HEADER,

    TOOL_ID_VALIDATE,
    TOOL_ID_COMPOSE,
    TOOL_ID_SEND
};

// ----------------------------------------------------------------------------
// private classes
// ----------------------------------------------------------------------------

class MyApp : public wxApp
{
public:
    virtual bool OnInit();
};

class WXDLLEXPORT Grid : public wxGrid 
{
public:
    Grid( wxWindow* parent, 
        wxWindowID id, 
        const wxPoint& pos = wxDefaultPosition, 
        const wxSize& size = wxDefaultSize)
    : wxGrid( parent, id, pos, size){};
    
    void OnKeyDown( wxKeyEvent& event );
private:
    DECLARE_EVENT_TABLE()
};

class MyFrame : public wxFrame
{
public:
    MyFrame(const wxString& title, 
            Descriptor* headerDescriptor,
            Descriptor* detailDescriptor,
            ComposerDescriptor* composerDescriptor);

    void SetEmail(bool isEnabled)
    {
        if ( !isEnabled ) {
            GetToolBar()->EnableTool( TOOL_ID_SEND, FALSE );
            GetMenuBar()->GetMenu(3)->Enable(Detail_Send, FALSE);
        } else {
            GetToolBar()->EnableTool( TOOL_ID_SEND, TRUE );
            GetMenuBar()->GetMenu(3)->Enable(Detail_Send, TRUE);
        }
    }

    wxString* GetDataFileName() {
        return m_DataFileName;
    }
    
    void SetDataFileName(wxString& filename) {
        *m_DataFileName = filename;
    }

    void OnClose(wxCloseEvent& event);
    void OnNew(wxCommandEvent& event);
    void OnOpen(wxCommandEvent& event);
    void Open(wxString& filename);
    
    void OnSave(wxCommandEvent& event);
    void OnSaveAs(wxCommandEvent& event);
    void OnRunWizard(wxCommandEvent& event);
    void OnHeaderView(wxCommandEvent& event);
    void OnQuit(wxCommandEvent& event);

    void OnCopy(wxCommandEvent& event);
    void OnPaste(wxCommandEvent& event);
    void OnSelectAll(wxCommandEvent& event);
    
    void Search(wxCommandEvent& event);

    void OnUndo(wxCommandEvent& event);

    void GoToSelect(wxGridEvent& event);

    void AddRow(wxCommandEvent& event);
    void InsertRow(wxCommandEvent& event);
    void RemoveRow();

    void OnValidate(wxCommandEvent& event);
    void OnCompose(wxCommandEvent& event);
    void OnSend(wxCommandEvent& event);

    void OnAbout(wxCommandEvent& event);
    void OnWizardCancel(wxWizardEvent& event);
    
    void OnTestXmlDescriptor(wxCommandEvent& event);
    
    void OnSelectCell(wxGridEvent& event);
    void OnCellValueChanged(wxGridEvent& event);
    void OnCellEditorShown( wxGridEvent& ev ); 
    void OnRangeSelected(wxGridRangeSelectEvent& event);

    void RecreateToolbar();
    void OnToolEnter(wxCommandEvent& event);
    void OnToolLeftClick(wxCommandEvent& event);

    bool isSaved;

private:
    DECLARE_EVENT_TABLE()

    Header* m_header;
    Detail* m_detail;
    Detail* m_detail_undo;
    Composer* m_composer;

    Grid            *grid;
    wxToolBar       *panel;
    wxStaticText    *m_label;

    int m_TopSelected;
    int m_LeftSelected;
    int m_BottomSelected;
    int m_RightSelected;

    wxString    *m_DataFileName;
    wxString    *m_CurrentCell;

    bool nomail;

    Header* GetHeader()
    {
        return m_header;
    };

    Detail* GetDetail()
    {
        return m_detail;
    };

    // BF
   /* Detail* GetDetailUndo()
    {
        return m_detail_undo;
    }; */

   // void CopyDetail(Detail* from, Detail* to);

    void PasteVector(long offset, Vector &v);
    void PasteMatrix(Matrix &m);
    
    bool IsCellValid(int i, int j, ElementDescriptor* ed);

    //remove first and last "" if found
    wxString& PreparePath(wxString& path)
    {
        wxString& new_path = wxString();
    
        if ( (path.First('\"') == 0) && (path.Last() == '\"') ) {
            new_path = wxString(path.AfterFirst('\"')).BeforeLast('\"');
            if ( new_path.Last == '\\' ) {
                return (wxString&)new_path.BeforeLast('\\');
            } else {
                return new_path;
            }
        } else {
            return path;
        }
    };

};

#endif ___CLIENT_MAIN_FRAME___

