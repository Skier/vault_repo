/*
 *  $RCSfile: MainFrame.h,v $
 *
 *  $Revision: 1.5 $
 *
 *  last change: $Date: 2004/04/19 14:47:13 $
 */

#ifndef __ADVPCS_MAIN_FRAME_H__ 
#define __ADVPCS_MAIN_FRAME_H__ 
/* -------------------------- header place ---------------------------------- */
#include <vector>
#include <advpcs/cmd/Paste.h>
/* -------------------------- implementation place -------------------------- */

class wxDynamicToolBar;
class StatusList;
class StatusBar;
class ProcessIndicator;
class Grid;
class Document;
class Composer;
class DocManager;
class wxFrameLayout;
class DocTable;
class wxGridEvent;
class wxListEvent;
class wxGridRangeSelectEvent;
class wxListCtrl;
class wxDynamicToolBar;
class EdiResponse;
class AgentExecutor;
class wxCommandProcessor;


//typedef std::vector<wxString> StrList;
//typedef std::vector<StrList> StringTable;

class MainFrame : public wxFrame {
public: 
    MainFrame(const wxString& title);
    ~MainFrame();

    wxListCtrl* GetLogList() {
        wxASSERT(NULL != m_logList);
        return m_logList;
    };

    ProcessIndicator& GetProcessIndicator();

    void SetLastListColumn();
public:
    void OnQuit(wxCommandEvent& event);
    void OnClose(wxCloseEvent& event);
    void OnNew(wxCommandEvent& event);
    void OnSave(wxCommandEvent& event);
    void OnSaveAs(wxCommandEvent& event);

    void OnRunWizard(wxCommandEvent& event);
    void OnOpen(wxCommandEvent& event);

    void AddRow(wxCommandEvent& event);
    void InsertRow(wxCommandEvent& event);
    void RemoveRow();

    void OnUndo(wxCommandEvent& event);
    void OnRedo(wxCommandEvent& event);
    void OnPaste(wxCommandEvent& event);
    void OnCopy(wxCommandEvent& event);
    void Search(wxCommandEvent& event);

	void OnSortA(wxCommandEvent& event);
	void OnSortD(wxCommandEvent& event);

    void OnValidate(wxCommandEvent& event);
    void OnCompose(wxCommandEvent& event);
    void OnChangePassword(wxCommandEvent& event);

    void OnSelectAll(wxCommandEvent& event);
    void GoToSelect(wxGridEvent& event);
    void GoToCell(wxGridEvent& event);
    void OnSelectCell(wxGridEvent& event);
    void OnCellValueChanged(wxGridEvent& event);
    void OnCellEditorShown( wxGridEvent& ev ); 
    void OnRangeSelected(wxGridRangeSelectEvent& event);

    void OnHeaderView(wxCommandEvent& event);

    void OnToolEnter(wxCommandEvent& event);
    void OnToolLeftClick(wxCommandEvent& event);

    void OnUpload(wxCommandEvent& event);
    void OnUpdateStatus(wxCommandEvent& event);
    void OnAbout(wxCommandEvent& WXUNUSED(event));

    void OnLogListEnter(wxListEvent& event);

    void SetDocument(Document* doc);
    Document* GetDocument() { 
        wxASSERT(NULL != m_doc);
        return m_doc; 
    };
    bool Open(wxString& filename);

    void ClearLog(); 

    void SetFocus(); 

    void RefreshTitle();

    StatusList& GetStatusList() {
        wxASSERT(NULL != m_statusList);     
        return *m_statusList;
    };

    void SetWizardResult( int result ) {
        m_wizResult = result;
    };

private:
    void CreateMenu();
    wxDynamicToolBar* ReCreateToolBar();
    const DocManager& GetDocManager() const;
    DocTable& GetTable() { return *m_table; };
    void ParseClipboard(StringTable& matrix);
    void PasteMatrix(StringTable &m);
    void PasteVector(long offset, std::vector<wxString> &v);

    void UpdateRow(int row, bool refresh);
    bool ValidateRow(int row);
    bool Validate();
    bool Compose();
    bool GenerateEdiFileName(wxString& fileName);

    void ToCodeCell(unsigned long code);

    AgentExecutor& GetExecutor();

	void UpdateToolbar();


private:
    DECLARE_EVENT_TABLE()

    int m_TopSelected;
    int m_LeftSelected;
    int m_BottomSelected;
    int m_RightSelected;

    int m_wizResult;

    wxTextCtrl*       m_str;
    wxDynamicToolBar* m_toolBar;
    wxFrameLayout*  m_layout;
    wxNotebook*     m_infoNotebook;
    wxListCtrl*     m_logList;
    StatusList*     m_statusList;
    StatusBar*      m_statusBar;
    Grid*           m_grid;
    Document*       m_doc;
    DocTable*       m_table;
    wxCommandProcessor m_commandProcessor; 

	int m_wrong;
};

#endif /* __ADVPCS_MAIN_FRAME_H__ */
