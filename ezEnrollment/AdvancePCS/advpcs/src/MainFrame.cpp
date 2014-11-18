/*
 *  $RCSfile: MainFrame.cpp,v $
 *
 *  $Revision: 1.6 $
 *
 *  last change: $Date: 2004/04/19 14:47:13 $
 */

// For compilers that support precompilation, includes "wx.h".
#include "wx/wxprec.h"

#ifdef __BORLANDC__
    #pragma hdrstop
#endif

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif

#pragma warning (disable : 4786)
/* -------------------------- header place ---------------------------------- */
#include <memory>

#include <wx/listctrl.h>
#include <wx/notebook.h>
#include <wx/dataobj.h>
#include <wx/clipbrd.h>
#include <wx/tokenzr.h>
#include <wx/filename.h>
#include <wx/file.h>
#include <wx/cmdproc.h>

// fl headers
#include <wx/fl/controlbar.h>     // core API

// extra plugins
#include <wx/fl/barhintspl.h>    // bevel for bars with "X"s and grooves
#include <wx/fl/rowdragpl.h>     // NC-look with draggable rows
#include <wx/fl/cbcustom.h>      // customization plugin
#include <wx/fl/hintanimpl.h>

// beauty-care
#include <wx/fl/gcupdatesmgr.h>  // smooth d&d
#include <wx/fl/antiflickpl.h>   // double-buffered repaint of decorations
#include <wx/fl/dyntbar.h>       // auto-layout toolbar
#include <wx/fl/dyntbarhnd.h>    // control-bar dimension handler for it

#include <atf/CfgXml.h>
#include <atf/Exception.h>
#include <atf/XmlLoadException.h>
#include <advpcs/App.h>
#include <advpcs/ListLogger.h>
#include <advpcs/MainFrame.h>
#include <advpcs/StatusList.h>
#include <advpcs/Resources.h>
#include <advpcs/StatusBar.h>
#include <advpcs/Grid.h>
#include <advpcs/Document.h>
#include <advpcs/EdiDocument.h>
#include <advpcs/DocManager.h>
#include <advpcs/Wizard.h>
#include <advpcs/DocTable.h>
#include <advpcs/Descriptor.h>
#include <advpcs/HeaderFrame.h>
#include <advpcs/Agent.h>
#include <advpcs/AgentException.h>
#include <advpcs/EdiResponse.h>
#include <advpcs/Disabler.h>
#include <advpcs/AgentExecutor.h>
/* -------------------------- implementation place -------------------------- */

enum
{
    Wizard_Quit = 100,
    Wizard_Run = 200,
    Header_View,
    App_About = 1000,
    
    Detail_New,        Detail_Open,        Detail_Save,
    Detail_SaveAs,     Detail_Quit,        Detail_Copy,
    Detail_Paste,      Detail_Search,      Detail_Undo, 
    Detail_Redo,
    Detail_SelectAll,  Detail_AddRow,      Detail_InsertRow,
    Detail_RemoveRow,  Detail_Validate,    Detail_Compose,
    Detail_Send,       Detail_Status,      Detail_ChangePassword,

    Detail_SortA,      Detail_SortD,

    ID_TOOLBAR,        TOOL_ID_HELP,           TOOL_ID_COPY,
    TOOL_ID_PASTE,     TOOL_ID_SEARCH_STRING,  TOOL_ID_SEARCH,

    TOOL_ID_NEW,       TOOL_ID_OPEN,
    TOOL_ID_SAVE,      TOOL_ID_ADD,            TOOL_ID_REMOVE,
    TOOL_ID_INSERT,    TOOL_ID_HEADER,         

    TOOL_ID_SORT_A,    TOOL_ID_SORT_D,
    
    TOOL_ID_UNDO,      TOOL_ID_REDO,    
    
    TOOL_ID_VALIDATE,  TOOL_ID_COMPOSE,        TOOL_ID_SEND, 
    TOOL_ID_STATUS,    TOOL_ID_PASSWD,

    LOG_LIST_ID
};


BEGIN_EVENT_TABLE(MainFrame, wxFrame)
    EVT_CLOSE(MainFrame::OnClose)

    EVT_MENU(Detail_New,   MainFrame::OnNew)
    EVT_MENU(Detail_Open,  MainFrame::OnOpen)
    EVT_MENU(Detail_Save,  MainFrame::OnSave)
    EVT_MENU(Detail_SaveAs,  MainFrame::OnSaveAs)
    EVT_MENU(Wizard_Run,   MainFrame::OnRunWizard)

    EVT_MENU(Header_View,  MainFrame::OnHeaderView)

    EVT_MENU(Detail_Quit,   MainFrame::OnQuit)

    EVT_MENU(Detail_Validate, MainFrame::OnValidate)
    EVT_MENU(Detail_Compose, MainFrame::OnCompose)
    EVT_MENU(Detail_Send, MainFrame::OnUpload)
    EVT_MENU(Detail_Status, MainFrame::OnUpdateStatus)
    EVT_MENU(Detail_ChangePassword, MainFrame::OnChangePassword)
    EVT_MENU(Detail_Search, MainFrame::Search)

    EVT_MENU(App_About, MainFrame::OnAbout)

    EVT_MENU(wxID_UNDO,  MainFrame::OnUndo)
    EVT_MENU(wxID_REDO,  MainFrame::OnRedo)
    EVT_MENU(Detail_Copy,  MainFrame::OnCopy)
    EVT_MENU(Detail_Paste, MainFrame::OnPaste)

    EVT_MENU(Detail_SortA,  MainFrame::OnSortA)
    EVT_MENU(Detail_SortD,  MainFrame::OnSortD)

    EVT_MENU(Detail_AddRow, MainFrame::AddRow)
    EVT_MENU(Detail_InsertRow, MainFrame::InsertRow)
    EVT_MENU(Detail_RemoveRow, MainFrame::RemoveRow)

    EVT_GRID_LABEL_LEFT_CLICK(MainFrame::GoToSelect) 
    EVT_GRID_CELL_LEFT_CLICK(MainFrame::GoToCell)
    EVT_GRID_SELECT_CELL( MainFrame::OnSelectCell )
    EVT_GRID_RANGE_SELECT( MainFrame::OnRangeSelected )
    EVT_GRID_CELL_CHANGE( MainFrame::OnCellValueChanged )
    EVT_GRID_EDITOR_SHOWN( MainFrame::OnCellEditorShown )

    EVT_TEXT_ENTER(TOOL_ID_SEARCH_STRING, MainFrame::Search)
    EVT_TOOL_ENTER(ID_TOOLBAR, MainFrame::OnToolEnter)

    EVT_LIST_ITEM_ACTIVATED(LOG_LIST_ID, MainFrame::OnLogListEnter)

    EVT_MENU(-1, MainFrame::OnToolLeftClick)
    
END_EVENT_TABLE()

MainFrame::MainFrame(const wxString& title) 
    : wxFrame((wxFrame *)NULL, -1, title, wxDefaultPosition, wxSize(-1, -1)),
      m_logList(NULL), m_layout(NULL), m_infoNotebook(NULL), m_statusBar(NULL), m_grid(NULL),
      m_doc(NULL), m_table(NULL), m_TopSelected(-1), m_LeftSelected(-1), m_BottomSelected(-1),
      m_RightSelected(-1), m_commandProcessor()

{
    CreateMenu();    
    m_statusBar = new StatusBar(this, -1);
    SetStatusBar(m_statusBar);
        wxSizeEvent event;
        m_statusBar->OnSize(event);

    SetIcon(wxICON(mondrian));

    m_grid = new Grid(this, m_commandProcessor, -1);
    wxASSERT(NULL != m_grid);

    m_grid->SetRowLabelSize(50);
    m_grid->SetColLabelSize( 25);
    m_grid->AutoSizeRows();
    m_grid->DisableDragColSize();
    m_grid->DisableDragRowSize();

    m_layout = new wxFrameLayout( this, m_grid );

    
    m_layout->SetUpdatesManager( new cbGCUpdatesMgr() );

    
    // setup plugins for testing
    m_layout->PushDefaultPlugins();
    
    // drop in some bars
    cbDimInfo sizes0( 200,150, // when docked horizontally      
                      200,150, // when docked vertically        
                      200,150, // when floated                  
                      FALSE,  // the bar is not fixed-size
                      4,      // vertical gap (bar border)
                      4       // horizontal gap (bar border)
                    ); 
   
    cbDimInfo sizes2( 640,30, // when docked horizontally      
                      640,30, // when docked vertically        
                      640,30, // when floated                  
                      TRUE,   // the bar is not fixed-size
                      4,      // vertical gap (bar border)
                      4,      // horizontal gap (bar border)
                      new cbDynToolBarDimHandler()
                    ); 

    m_infoNotebook = new wxNotebook(this, -1, wxDefaultPosition, wxSize( 320, 240 ), wxNB_BOTTOM);


    m_logList = new wxListCtrl(m_infoNotebook, LOG_LIST_ID, wxDefaultPosition, wxSize( -1, -1 ), wxLC_REPORT | wxLC_SINGLE_SEL );
    m_logList->InsertColumn(0, ADVPCS_LOG_COL_LEVEL_TITLE, wxLIST_FORMAT_LEFT, 80);
    m_logList->InsertColumn(1, ADVPCS_LOG_COL_TIME_TITLE, wxLIST_FORMAT_LEFT, 80);
    m_logList->InsertColumn(2, ADVPCS_LOG_COL_MSG_TITLE, wxLIST_FORMAT_LEFT, 120);

    m_infoNotebook->AddPage(m_logList, ADVPCS_LOG_PAGE_TITLE, true);

    m_statusList = new StatusList(m_infoNotebook, -1, wxDefaultPosition, wxSize( 320, 240 ), wxLC_REPORT | wxLC_SINGLE_SEL );
    m_infoNotebook->AddPage(m_statusList, ADVPCS_STATUS_PAGE_TITLE, false);



    m_layout->AddBar( m_infoNotebook,  // bar window
                      sizes0, FL_ALIGN_BOTTOM,     // alignment ( 0-top,1-bottom, etc)
                      0,                        // insert into 0th row (vert. position)
                      0,                        // offset from the start of row (in pixels)
                      "InfoViewer1",            // name to refer in customization pop-ups
                      TRUE
                    );

    
    m_layout->AddBar( ReCreateToolBar(),             // bar window (can be NULL)
                      sizes2, FL_ALIGN_TOP, // alignment ( 0-top,1-bottom, etc)
                      0,                    // insert into 0th row (vert. position)
                      0,                    // offset from the start of row (in pixels)
                      "ToolBar2",           // name to refer in customization pop-ups
                      FALSE
                    );


    m_layout->EnableFloating( FALSE ); // off, thinking about wxGtk...

    CentreOnScreen();
    m_grid->SetFocus();
};

MainFrame::~MainFrame()
{
    if ( NULL != m_layout ) { 
        delete m_layout; 
    }
    if ( NULL != m_doc ) {
        wxDELETE(m_doc);
    }
}

void MainFrame::SetDocument(Document* doc) {
    wxASSERT(NULL != doc);

    Document* old = m_doc;

    m_doc = doc;

    if ( NULL == m_table  ) {
        m_table = new DocTable(m_doc, m_commandProcessor);
        m_grid->SetTable(m_table, true);
    } else {
        m_table->SetDocument(m_doc);
    }

    bool changed = m_doc->IsChanged();
    if ( 0 == m_doc->GetNumberRows() ) {
        m_grid->AppendRows(1);
    }
    m_doc->SetChanged(changed);

    if (NULL != old) {
        wxDELETE(old);
    }

    wxASSERT(NULL != m_doc);
};


void MainFrame::CreateMenu() {
    wxMenu *menuFile = new wxMenu;
    menuFile->Append(Detail_New, "&New\tCtrl-N");
    menuFile->Append(Detail_Open, "&Open\tCtrl-O");
    menuFile->Append(Detail_Save, "&Save\tCtrl-S");
    menuFile->Append(Detail_SaveAs, "&Save As");
    menuFile->AppendSeparator();
    menuFile->Append(Header_View, "Edit Header...");
    menuFile->AppendSeparator();
    menuFile->Append(Detail_Quit, "E&xit", "Quit this program");

    wxMenu *menuEdit = new wxMenu;
    menuEdit->Append(wxID_UNDO, "&Undo\tCtrl-Z");
    menuEdit->Append(wxID_REDO, "&Redo\tCtrl-Y");
    menuEdit->AppendSeparator();
    menuEdit->Append(Detail_Copy, "&Copy\tCtrl-C");
    menuEdit->Append(Detail_Paste, "&Paste\tCtrl-V");
    menuEdit->AppendSeparator();
    menuEdit->Append(Detail_Search, "&Search\tCtrl-F");
    menuEdit->AppendSeparator();
    menuEdit->Append(Detail_SelectAll, "Select &all\tCtrl-A");
    menuEdit->AppendSeparator();
    menuEdit->Append(Detail_SortA, "Ascending sort");
    menuEdit->Append(Detail_SortD, "Descending sort");

    m_commandProcessor.SetEditMenu(menuEdit);
    m_commandProcessor.SetUndoAccelerator("\tCtrl-Z");
    m_commandProcessor.SetRedoAccelerator("\tCtrl-Y");
    m_commandProcessor.Initialize();

    wxMenu *menuGrid = new wxMenu;
    menuGrid->Append(Detail_AddRow, "A&dd Row");
    menuGrid->Append(Detail_InsertRow, "&Insert Row\tCtrl-I");
    menuGrid->Append(Detail_RemoveRow, "&Remove Row\tCtrl-D");

    wxMenu *menuProcess = new wxMenu;
    menuProcess->Append(Detail_Validate, "Validate");
    menuProcess->Append(Detail_Compose, "Compose EDI file");
    menuProcess->Append(Detail_Send, "Send data to server");
    menuProcess->Append(Detail_Status, "Get sent files status");
    menuProcess->Append(Detail_ChangePassword, "Change password");

    wxMenu *helpMenu = new wxMenu;
    helpMenu->Append(App_About, "&About...\tF1", "Show about dialog");

    wxMenuBar *menuBar = new wxMenuBar();
    menuBar->Append(menuFile, "&File");
    menuBar->Append(menuEdit, "&Edit");
    menuBar->Append(menuGrid, "&Grid");
    menuBar->Append(menuProcess, "&Process");
    menuBar->Append(helpMenu, "&Help");

    SetMenuBar(menuBar);
};

wxDynamicToolBar* MainFrame::ReCreateToolBar() {

    m_toolBar = new wxDynamicToolBar();
    
    m_toolBar->Create( this, -1 );
    
    wxBitmap toolBarBitmaps[23];

    toolBarBitmaps[0] = wxBITMAP(new);
    toolBarBitmaps[1] = wxBITMAP(open);
    toolBarBitmaps[2] = wxBITMAP(save);
    toolBarBitmaps[3] = wxBITMAP(copy);
    toolBarBitmaps[4] = wxBITMAP(cut);
    toolBarBitmaps[5] = wxBITMAP(paste);
    toolBarBitmaps[6] = wxBITMAP(print);
    toolBarBitmaps[7] = wxBITMAP(help);
    toolBarBitmaps[8] = wxBITMAP(add);
    toolBarBitmaps[9] = wxBITMAP(remove);
    toolBarBitmaps[10] = wxBITMAP(insert);

    toolBarBitmaps[11] = wxBITMAP(validate);
    toolBarBitmaps[12] = wxBITMAP(compose);
    toolBarBitmaps[13] = wxBITMAP(send);
    toolBarBitmaps[14] = wxBITMAP(header);
//    toolBarBitmaps[15] = wxBITMAP(undo);
    toolBarBitmaps[16] = wxBITMAP(search);
    toolBarBitmaps[17] = wxBITMAP(status);
    toolBarBitmaps[18] = wxBITMAP(passwd);

    toolBarBitmaps[19] = wxBITMAP(sort-a);
    toolBarBitmaps[20] = wxBITMAP(sort-d);

    toolBarBitmaps[21] = wxBITMAP(undo);
    toolBarBitmaps[22] = wxBITMAP(redo);

    int width = 24;
    int currentX = 5;

    m_toolBar->AddTool(TOOL_ID_NEW, toolBarBitmaps[0], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Clear grid");
    currentX += width + 5;

    m_toolBar->AddTool(TOOL_ID_OPEN, toolBarBitmaps[1], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Open file");
    currentX += width + 5;
    
    m_toolBar->AddTool(TOOL_ID_SAVE, toolBarBitmaps[2], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Save file");
    currentX += width + 5;
    
    m_toolBar->AddSeparator();
    
    m_toolBar->AddTool(TOOL_ID_COPY, toolBarBitmaps[3], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Copy to clipboard");
    currentX += width + 5;
    
    m_toolBar->AddTool(TOOL_ID_PASTE, toolBarBitmaps[5], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Paste from clipboard");
    currentX += width + 5;
    
    m_toolBar->AddSeparator();
    
    m_toolBar->AddTool(TOOL_ID_UNDO, toolBarBitmaps[21], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Undo");
    currentX += width + 5;
    
    m_toolBar->AddTool(TOOL_ID_REDO, toolBarBitmaps[22], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Redo");
    currentX += width + 5;
    
    m_toolBar->AddSeparator();
    
    m_toolBar->AddTool(TOOL_ID_HEADER, toolBarBitmaps[14], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Edit Header record");
    currentX += width + 5;
    
    m_toolBar->AddSeparator();
    
    m_toolBar->AddTool(TOOL_ID_ADD, toolBarBitmaps[8], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Append row into grid");
    currentX += width + 5;
    
    m_toolBar->AddTool(TOOL_ID_REMOVE, toolBarBitmaps[9], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Remove current row");
    currentX += width + 5;
    
    m_toolBar->AddTool(TOOL_ID_INSERT, toolBarBitmaps[10], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Insert row");
    currentX += width + 5;
    
    m_toolBar->AddSeparator();
    
    m_toolBar->AddTool(TOOL_ID_VALIDATE, toolBarBitmaps[11], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Validate grid data");
    currentX += width + 5;
    
    m_toolBar->AddSeparator();
    
    m_toolBar->AddTool(TOOL_ID_SORT_A, toolBarBitmaps[19], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Ascending sort");
    currentX += width + 5;
    
    m_toolBar->AddTool(TOOL_ID_SORT_D, toolBarBitmaps[20], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Descending sort");
    currentX += width + 5;
    
    m_toolBar->AddSeparator();
    
    m_toolBar->AddTool(TOOL_ID_COMPOSE, toolBarBitmaps[12], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Make EDI file for sending");
    currentX += width + 5;
    
    m_toolBar->AddTool(TOOL_ID_SEND, toolBarBitmaps[13], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Send data to server");
    currentX += width + 5;
    
    m_toolBar->AddTool(TOOL_ID_STATUS, toolBarBitmaps[17], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Status request");
    currentX += width + 5;
    
    m_toolBar->AddTool(TOOL_ID_PASSWD, toolBarBitmaps[18], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Change password");
    currentX += width + 5;
    
    m_toolBar->AddSeparator();
    m_str = new wxTextCtrl(m_toolBar, TOOL_ID_SEARCH_STRING);
    m_toolBar->AddTool(17, (wxWindow *)m_str, wxSize());
    m_toolBar->AddTool(TOOL_ID_SEARCH, toolBarBitmaps[16], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Search");
    currentX += width + 5;
    
    m_toolBar->AddSeparator();

    m_toolBar->AddTool(TOOL_ID_HELP, toolBarBitmaps[7], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Help button");

    m_toolBar->Realize();

    return m_toolBar;

};

ProcessIndicator& MainFrame::GetProcessIndicator() {
    return *m_statusBar;
};


void MainFrame::OnClose(wxCloseEvent& event) {
    if ( event.CanVeto() ) {
        if ( NULL != GetDocument() ) {
            if ( !GetDocument()->IsChanged() ) {
                wxMessageDialog dialog( this, ADVPCS_GRID_CANCEL_MESSAGE,
                            ADVPCS_GRID_CANCEL_TITLE,
                            wxYES_NO | wxNO_DEFAULT | wxCENTRE | wxICON_QUESTION);
                if ( dialog.ShowModal() == wxID_YES ) {
                    event.Skip();
                    return;
                } else {
                    event.Veto();
                    return;
                }
            }
        }
        wxMessageDialog dialog( this, ADVPCS_GRID_CANCEL_WITHOUT_SAVE_MESSAGE,
                     ADVPCS_GRID_CANCEL_WITHOUT_SAVE_TITLE,
                     wxYES_NO | wxYES_DEFAULT | wxCANCEL | wxCENTRE | wxICON_QUESTION );
        int result = dialog.ShowModal();
        m_grid->SetFocus();
        if ( result == wxID_YES ) {
            OnSave(wxCommandEvent());
            event.Skip();
        } else if ( result == wxID_NO ) {
            event.Skip();
        } else {
            event.Veto();
        }
    }
}

void MainFrame::OnNew(wxCommandEvent& event) {
    if ( NULL != GetDocument() ) {
       if ( GetDocument()->IsChanged() ) {
            wxMessageDialog dialog( this, ADVPCS_GRID_CANCEL_WITHOUT_SAVE_MESSAGE,
                                   ADVPCS_GRID_CANCEL_WITHOUT_SAVE_TITLE,
                                   wxYES_NO | wxYES_DEFAULT | wxCANCEL | wxCENTRE | wxICON_QUESTION );
            int result = dialog.ShowModal();
                    m_grid->SetFocus();
            if ( result == wxID_YES ) {
                OnSave(wxCommandEvent());
            } else if ( result != wxID_NO ){
                event.Skip();
                return;
            }
        }
    }
    SetDocument(wxGetApp().GetDocManager().Create());
    m_commandProcessor.ClearCommands();
    m_commandProcessor.Initialize();
    RefreshTitle();
    UpdateToolbar();
}
void MainFrame::OnSave(wxCommandEvent& WXUNUSED(event)) {
    wxASSERT(NULL != GetDocument());

    wxString fileName = GetDocument()->GetFileName();
    try {
        if ( 0 == fileName.CmpNoCase(wxEmptyString) ) {
            wxFileDialog dialog(this, ADVPCS_GRID_SAVE_TITLE, "", "",
                               "Records files (*"+ADVPCS_CSV_EXT+")|*"+ADVPCS_CSV_EXT,
                                wxSAVE | wxOVERWRITE_PROMPT );
            if (dialog.ShowModal() == wxID_OK) {
                fileName = dialog.GetPath();
            } else {
                return;
            }
            ::wxGetApp().GetDocManager().SaveAs(*GetDocument(), fileName);
        } else {
            ::wxGetApp().GetDocManager().Save(*GetDocument());
        }

        RefreshTitle();
    } catch (CAtfException &ex) {
        LOG_ERROR(::wxGetApp().GetLogger(), ex.GetCode(), ex.GetText());
    }
    m_grid->SetFocus();
}

const DocManager& MainFrame::GetDocManager() const 
{ 
    return ::wxGetApp().GetDocManager();
};

void MainFrame::OnRunWizard(wxCommandEvent& WXUNUSED(event)) {

    wxWizard* wizard = new wxWizard(this, -1, ADVPCS_WIZARD_TITLE, wxBITMAP(wiztest));
    
    RadioboxPage* page2 = new RadioboxPage(wizard, this);
    HeaderRecordPage* page3 = new HeaderRecordPage(wizard, this);
    FinishPage* page4 = new FinishPage(wizard);

    wxWizardPageSimple::Chain(page2, page3);
    wxWizardPageSimple::Chain(page3, page4);
    
    wizard->SetPageSize(wxSize(300,250));

    wizard->RunWizard(page2);
    if ( 1 == m_wizResult ) {
        wxCommandEvent& event = wxCommandEvent();
        OnUpdateStatus(event);
    };
    wizard->Destroy();
    m_grid->SetFocus();
    UpdateToolbar();
}

void MainFrame::AddRow(wxCommandEvent& WXUNUSED(event)) {
    m_grid->AddRow();
    UpdateToolbar();
}

void MainFrame::OnSortA(wxCommandEvent& WXUNUSED(event)) {
    if ( m_table->Sort(0, m_grid->GetNumberRows()-1, m_grid->GetGridCursorCol(), true) ) {
        m_grid->ForceRefresh();
    } else {
        wxMessageBox("Sorting error !");
    };
    UpdateToolbar();
}

void MainFrame::OnSortD(wxCommandEvent& WXUNUSED(event)) {
    if ( m_table->Sort(0, m_grid->GetNumberRows()-1, m_grid->GetGridCursorCol(), false) ) {
        m_grid->ForceRefresh();
    } else {
        wxMessageBox("Sorting error !");
    };
    UpdateToolbar();
}

void MainFrame::InsertRow(wxCommandEvent& WXUNUSED(event)) {

    m_grid->InsertRow();
    UpdateToolbar();
}

void MainFrame::RemoveRow() {

    if ( m_grid->IsSelection() ) {
        if ( ( m_LeftSelected == 0 )
                && ( m_RightSelected == ( m_grid->GetNumberCols() - 1 ) )
                && ( m_TopSelected < m_BottomSelected ) ) 
        {
            m_grid->DeleteRow(m_TopSelected, m_BottomSelected);
        } else {
            m_grid->DeleteRow(m_grid->GetGridCursorRow());
        }
    } else {
        if ( 0 < m_grid->GetNumberRows() ) {
            m_grid->DeleteRow();
        }
    }
    UpdateToolbar();
}

void MainFrame::OnSelectCell( wxGridEvent& ev )
{
    if ( ev.Selecting() ) {
        SetStatusText(GetDocument()->GetColHint(m_table->Map(ev.GetCol())), 1);
    }
    ev.Skip();
}

void MainFrame::OnRangeSelected( wxGridRangeSelectEvent& ev )
{
#if 0 
    m_grid->OnRangeSelected(ev);
#endif        
    // TO BE moved into Grid         
    if ( ev.Selecting() ) {
        if ((m_TopSelected != -1) && (m_TopSelected > ev.GetTopRow())) {
            m_TopSelected = ev.GetTopRow();
        } else if ((m_LeftSelected != -1) && (m_LeftSelected > ev.GetLeftCol())) {
            m_LeftSelected = ev.GetLeftCol();
        } else if ((m_BottomSelected != -1) && (m_BottomSelected < ev.GetBottomRow())) {
            m_BottomSelected = ev.GetBottomRow();
        } else if ((m_RightSelected != -1) && (m_RightSelected < ev.GetRightCol())) {
            m_RightSelected = ev.GetRightCol();
        } else {
            m_TopSelected = ev.GetTopRow();
            m_LeftSelected = ev.GetLeftCol();
            m_BottomSelected = ev.GetBottomRow();
            m_RightSelected = ev.GetRightCol();
        };
    } else {
        m_TopSelected = -1;
        m_LeftSelected = -1;
        m_BottomSelected = -1;
        m_RightSelected = -1;
    }
    ev.Skip();
}

void MainFrame::OnSelectAll(wxCommandEvent& WXUNUSED(event)) {
    m_grid->SelectAll();
    m_grid->SetGridCursor( 0, 0);
}

void MainFrame::GoToSelect(wxGridEvent& event)
{
    m_grid->SetFocus();
    if ( m_grid->GetNumberRows() > 0 ) {
        if ((event.GetRow() == -1) && (event.GetCol() != -1)) {
            m_grid->SetGridCursor(event.GetRow() + 1, event.GetCol());
        }
        if ((event.GetRow() != -1) && (event.GetCol() == -1)) {
            m_grid->SetGridCursor(event.GetRow(), event.GetCol() + 1);
        }
    }
    event.Skip();
}


void MainFrame::OnCellValueChanged(wxGridEvent& ev) {
    
    int row = ev.GetRow();
    int col = ev.GetCol();

    if ( GetDocument()->IsValidValue(row, m_table->Map(col), false) ) {
        if ( GetDocument()->GetColumnDescriptor(m_table->Map(col)).IsRequired() ) {
            m_grid->SetCellBackgroundColour(row, col, REQUIRED_COL);
        } else {
            m_grid->SetCellBackgroundColour(row, col, VALID_COL);
        }
    } else {
        m_grid->SetCellBackgroundColour(row, col, NOT_VALID_COL);
    };
    UpdateToolbar();
    ev.Skip();
}

void MainFrame::OnCellEditorShown(wxGridEvent& WXUNUSED(ev)) {
    
    wxTextDataObject textData;

    if ( wxTheClipboard->Open() && wxTheClipboard->IsSupported(wxDF_TEXT) ) {
        wxTheClipboard->GetData(textData);
        wxTheClipboard->Close();
    }

    wxString text = textData.GetText();
    if ( 0 <= (text.Find('\t') + text.Find('\r') + text.Find('\n')) ) {
        if ( wxTheClipboard->Open() ) {
            wxTheClipboard->Clear();
            wxTheClipboard->Close();
        }
    }

}

void MainFrame::OnOpen(wxCommandEvent& WXUNUSED(event))
{

    if ( GetDocument()->IsChanged() ) {
         wxMessageDialog dialog( this, ADVPCS_GRID_ASK_SAVE_MESSAGE,
                                 ADVPCS_GRID_SAVE_TITLE,
                                 wxYES_NO | wxYES_DEFAULT | wxCENTRE | wxICON_QUESTION);
        if ( dialog.ShowModal() == wxID_YES ) {
            wxCommandEvent evt;
            OnSave(evt);
        }
    }

    wxFileDialog dialog( this, ADVPCS_GRID_OPEN_TITLE, wxEmptyString, wxEmptyString,
                    "Records files (*"+ADVPCS_CSV_EXT+")|*"+ADVPCS_CSV_EXT );

    if (dialog.ShowModal() == wxID_OK) {
        Open(dialog.GetPath());
        m_commandProcessor.ClearCommands();
        m_commandProcessor.Initialize();
    }
    UpdateToolbar();
    RefreshTitle();
    m_grid->SetFocus();
}

bool MainFrame::Open(wxString& filename) {
    try {
    Document* doc = GetDocManager().Open(filename);
        SetDocument(doc);
    } catch (CAtfException& ex) {
        wxMessageBox((const char*)ex.GetText(), ADVPCS_GRID_OPEN_ERROR_TITLE,
                     wxOK | wxCENTRE | wxICON_ERROR );
                m_grid->SetFocus();
        return false;
    }
    return true;
}

void MainFrame::OnCopy(wxCommandEvent& WXUNUSED(event))
{
    wxString text("");

    if (m_grid->IsSelection())
    {
        if ( m_TopSelected < 0 ) {
            m_TopSelected = 0;
        }
        if ( m_BottomSelected > m_grid->GetNumberRows()-1 ) {
            m_BottomSelected = m_grid->GetNumberRows()-1;
        }
        if ( m_LeftSelected < 0 ) {
            m_LeftSelected = 0;
        }
        if ( m_RightSelected > m_grid->GetNumberCols()-1 ) {
            m_RightSelected = m_grid->GetNumberCols()-1;
        }
        for (int i=m_TopSelected; i<=m_BottomSelected; i++){
            for (int j=m_LeftSelected; j<=m_RightSelected; j++) {
                text << m_grid->GetCellValue(i, j);
                text << "\t";
            }
            text << "\n";
        }
        wxTextDataObject *text_object = new wxTextDataObject(text);
        if (wxTheClipboard->Open()) {
            wxTheClipboard->SetData( text_object );
            wxTheClipboard->Close();
        }
    } else if ((m_grid->GetGridCursorRow() != -1) && (m_grid->GetGridCursorCol() != -1)) {
        text << m_grid->GetCellValue(m_grid->GetGridCursorRow(), m_grid->GetGridCursorCol());
        text << "\t\n";
        wxTextDataObject *text_object = new wxTextDataObject(text);
        if (wxTheClipboard->Open()) {
            wxTheClipboard->SetData( text_object );
            wxTheClipboard->Close();
        }
    }
}

void MainFrame::OnPaste(wxCommandEvent& event)
{

    try {
        StringTable matrix;
        ParseClipboard(matrix);

        if ( m_grid->IsSelection() ) {
            int vertSize = (m_BottomSelected - m_TopSelected + 1);
            int horSize = (m_RightSelected - m_LeftSelected + 1);
            if (  (matrix.size() == vertSize  && matrix[0].size() == horSize) 
                 ||(matrix.size() == 1) && (matrix[0].size() == 1)) 
            {
                m_grid->SetGridCursor(m_TopSelected, m_LeftSelected);
                m_commandProcessor.Submit(new CmdPaste(*m_grid, *m_table, matrix, 
                                           wxRect( wxPoint(m_LeftSelected, m_TopSelected), 
                                                   wxPoint(m_RightSelected, m_BottomSelected))));
            } else {
                THROW_ATF_EXCEPTION(0, ADVPCS_GRID_PASTE_ERROR_MESSAGE);
            }
        } else {
            m_commandProcessor.Submit(new CmdPaste(*m_grid, *m_table, matrix, 
                                       wxRect( wxPoint(m_grid->GetGridCursorCol(), m_grid->GetGridCursorRow()), 
                                               wxPoint(m_grid->GetGridCursorCol()+matrix[0].size()-1, m_grid->GetGridCursorRow()+matrix.size()-1))));
        }
        UpdateToolbar();


#if 0
        if ( m_grid->GetNumberRows() < 1 ) {
            AddRow(event);
            m_grid->SetGridCursor( 0, 0);
        }
//----- one-to-many pasting
        if ( m_grid->IsSelection() ) {
            int vertSize = (m_BottomSelected - m_TopSelected + 1);
            int horSize = (m_RightSelected - m_LeftSelected + 1);
            if ( matrix.size() == vertSize  && matrix[0].size() == horSize ) {
                m_grid->SetGridCursor(m_TopSelected, m_LeftSelected);
                PasteMatrix(matrix);
            } else if ( (matrix.size() == 1) && (matrix[0].size() == 1) ) {
                for (int i=m_TopSelected; i<=m_BottomSelected; i++){
                    for (int j=m_LeftSelected; j<=m_RightSelected; j++) {
                        m_grid->SetCellValue(i, j, matrix[0][0]);
                    }
                }
            } else {
                THROW_ATF_EXCEPTION(0, ADVPCS_GRID_PASTE_ERROR_MESSAGE);
            }
        } else {
            PasteMatrix(matrix);
        }
#endif
        //PasteMatrix(matrix);
//-------------------------
    } catch (CAtfException&  ex) {
        wxMessageBox((const char*)ex.GetText(), ADVPCS_GRID_PASTE_ERROR_TITLE,
                        wxOK | wxCENTRE | wxICON_ERROR );
            m_grid->SetFocus();
    }
}

void MainFrame::PasteVector(long offset, std::vector<wxString> &v)
{
    for ( size_t i=0; i<v.size(); i++ ) {
        if ( (m_grid->GetGridCursorCol()+i) < m_grid->GetNumberCols() ) {

            if ( v[i].Last() == '\r' ) {
                m_grid->SetCellValue(m_grid->GetGridCursorRow()+offset, m_grid->GetGridCursorCol()+i, v[i].RemoveLast());
            } else {
                m_grid->SetCellValue(m_grid->GetGridCursorRow()+offset, m_grid->GetGridCursorCol()+i, v[i]);
            }
        }
    }
}

void MainFrame::PasteMatrix(StringTable &m)
{
    long offset = 0;
    for ( StringTable::iterator i = m.begin(); i < m.end(); ++i, offset++ ) {
                
        if ( (m_grid->GetGridCursorRow()+offset) == m_grid->GetNumberRows() ) {
            m_grid->AppendRows();
        }
        PasteVector(offset, *i);
    }
}

void MainFrame::ParseClipboard(StringTable& matrix) {
    size_t i = 0;
 
    matrix.clear();

    wxTextDataObject textData;

    if ( wxTheClipboard->Open() ) {
        if ( wxTheClipboard->IsSupported(wxDF_TEXT) ) {
            wxTheClipboard->GetData(textData);
            wxTheClipboard->Close();
        } else {
            wxTheClipboard->Close();
            THROW_ATF_EXCEPTION(0, ADVPCS_GRID_PASTE_WRONG_CONTENT.c_str());
        }  
    } else {
        THROW_ATF_EXCEPTION(0, ADVPCS_GRID_WRONG_CLIPBOARD.c_str());
    }

    wxStringTokenizer st(textData.GetText(), "\n", wxTOKEN_STRTOK);
    while ( st.HasMoreTokens() ) {
        wxString str = st.GetNextToken();
        if ( str.Last() == '\r' ) {
             str = str.RemoveLast();
        }
        if ( str.Last() == '\t' ) {
              str = str.RemoveLast();
        }
        wxStringTokenizer fld(str, "\t", wxTOKEN_RET_EMPTY_ALL);
        std::vector<wxString> v;
        if ( fld.HasMoreTokens() ) {
            while ( fld.HasMoreTokens() ) {
                    v.push_back(fld.GetNextToken());
            }
        } else {
            v.push_back(str);
        }
        matrix.push_back(v);
    }

};

void MainFrame::OnHeaderView(wxCommandEvent& WXUNUSED(event))
{
    HeaderFrame head(this);
    head.Show();
}

void MainFrame::OnCompose(wxCommandEvent& event) {
    EdiDocument* d = (EdiDocument*)GetDocument();
    wxString oldFile = d->GetFileName(); 
    if ( Compose() ) {
        wxString ediFileName = d->GetFileName();
        d->SetFileName(oldFile);
//        wxFileName fn(GetDocument()->GetFileName());
//      wxString ediFileName = fn.GetPath(wxPATH_GET_SEPARATOR)+ wxGetApp().EdiFileName()+ ADVPCS_EDI_EXT;
        ::wxMessageBox(wxString::Format(ADVPCS_COMPOSED_OK, ediFileName));
    };
    m_grid->SetFocus();
}

bool MainFrame::Compose() {
    if ( !Validate() ) {
        return false;
    }

    try {
        if ( GetDocument()->IsChanged() ) {
            wxCommandEvent event;
            OnSave(event);
        }

        wxString fn = GetDocument()->GetFileName();
        wxString path = fn.Left(fn.Find('\\', TRUE));
        wxString ediFileName = wxString::Format("%s\\%s%s",path,wxGetApp().EdiFileName(),ADVPCS_EDI_EXT);
        if ( wxFile::Exists(ediFileName) ) {
            if ( wxMessageBox("File " + ediFileName + " already exists.\n Do you want to create another version?", ADVPCS_COMPOSE_OVERWRITING_TITLE,
                     wxICON_QUESTION | wxYES_NO) != wxYES ) {
                LOG_INFO(wxGetApp().GetLogger(), ATF_INFO, ADVPCS_COMPOSE_CANCEL);
                return false;
            } else if ( !GenerateEdiFileName(ediFileName) ) {
                wxMessageBox(ADVPCS_CANNOT_CREATE_MORE_FILES);
                return false;
            }
        }

        //wxMessageBox(ediFileName);
        wxGetApp().GetDocManager().SaveAs(*GetDocument(), ediFileName);

        m_grid->SetFocus();

        return true;
    } catch (CAtfException &ex) {
        wxMessageBox((const char*)ex.GetText(), ADVPCS_ERROR_TITLE,
                        wxOK | wxCENTRE | wxICON_ERROR );
        m_grid->SetFocus();
    }
    return false;
};

bool MainFrame::GenerateEdiFileName(wxString& fileName) {

#define CAPITAL_A 65    
#define CAPITAL_Z 90

    wxFileName fn(fileName);
    wxString suffix = (const char*)wxGetApp().GetCfg()->GetChild(ADVPCS_COMPOSER_CFG)->GetParam(ADVPCS_COMPOSER_SUFFIX_CFG, "");
    int code = CAPITAL_A;
    wxString f;
    if ( !suffix.IsEmpty() ) {
        code = suffix.GetChar(0);
    }
    while ( code <= CAPITAL_Z ) {
        if ( suffix.IsEmpty() ) {
            f = wxString( fn.GetPath(wxPATH_GET_SEPARATOR) + fn.GetName() + ((char)code) + "." + fn.GetExt() );
        } else {
            f = wxString( fn.GetPath(wxPATH_GET_SEPARATOR) + fn.GetName().RemoveLast() + ((char)code) + "." + fn.GetExt() );
        }
        if ( !wxFile::Exists( f ) ) {
            fileName = f;
            return true;
        } else {
            code++;
        }
    }
    return false;
};


bool MainFrame::Validate() {
    Disabler d(this);
    ClearLog();
    if ( m_grid->GetNumberRows() == 0 ) {
        m_infoNotebook->SetSelection(0);
        wxMessageBox(ADVPCS_COMPOSE_DETAIL_EMPTY, 
                        ADVPCS_VALIDATING_DETAIL_PROCESS_TITLE,
                        wxOK | wxCENTRE | wxICON_EXCLAMATION );
        m_grid->SetFocus();
        return false;
    } else {
        m_grid->SetGridCursor( 0, 0);
    }

    if ( ! GetDocument()->IsHeaderValid(true) ) {
        m_infoNotebook->SetSelection(0);
        wxMessageBox(ADVPCS_COMPOSE_HEADER_INVALID, 
                        ADVPCS_VALIDATING_DETAIL_PROCESS_TITLE,
                        wxOK | wxCENTRE | wxICON_ERROR );
        wxCommandEvent event;
        OnHeaderView(event);
        m_grid->SetFocus();
        return false;
    }

    GetProcessIndicator().StartProcess(m_table->GetNumberRows());
    
    int wrong = 0;
        for ( int row = 0; row < m_table->GetNumberRows(); row++ ) {
        for ( int col = 0; col < m_table->GetNumberCols(); col++ ) {

                GetProcessIndicator().SetState(row, wxString::Format("Row %u", row));

                        if ( GetDocument()->IsValidValue(row, m_table->Map(col), true) ) {

                                if ( m_grid->GetCellBackgroundColour(row, col) == NOT_VALID_COL ) {
                                        if ( GetDocument()->GetColumnDescriptor(m_table->Map(col)).IsRequired() ) {
                                           m_grid->SetCellBackgroundColour(row, col, REQUIRED_COL);
                                        } else {
                                           m_grid->SetCellBackgroundColour(row, col, VALID_COL);
                                        }
                                }

                        } else {
                m_grid->SetCellBackgroundColour(row, col, NOT_VALID_COL);
                if ( wrong++ > 100 ) {
                                        goto w;
                                }
                        }

                }
    }
w:
    GetProcessIndicator().FinishProcess("");

    if ( 0 != wrong ) {
        m_infoNotebook->SetSelection(0);
        m_grid->ForceRefresh();

        if ( 100 <= wrong ) {
           wxMessageBox(ADVPCS_COMPOSE_DETAIL_MORE_100_ERRORS, 
                        ADVPCS_VALIDATING_DETAIL_PROCESS_TITLE,
                        wxOK | wxCENTRE | wxICON_ERROR );
        } else {
            wxMessageBox(ADVPCS_COMPOSE_DETAIL_INVALID, 
                         ADVPCS_VALIDATING_DETAIL_PROCESS_TITLE,
                         wxOK | wxCENTRE | wxICON_ERROR );
        }

        m_logList->EnsureVisible(0);
        ToCodeCell(m_logList->GetItemData(0));

        m_grid->SetFocus();
        return false;
    }
    m_grid->ForceRefresh();
    m_infoNotebook->SetSelection(0);
    m_grid->SetFocus();
    return true;
};

void MainFrame::Search(wxCommandEvent& WXUNUSED(event))
{
    int curr_col = m_grid->GetGridCursorCol();
    int curr_row = m_grid->GetGridCursorRow();
    bool first_time = true;
    wxTextCtrl* txt = m_str;
    if ( txt->GetLabel().IsEmpty() ) {
        m_grid->SetFocus();
        return;
    }
    txt->SetLabel(txt->GetValue().Upper());
    txt->Enable(false);
    for (int i=0; i<m_grid->GetNumberRows(); i++) {
        for (int j=0; j<m_grid->GetNumberCols(); j++) {
            if ( first_time ) {
                i=curr_row;
                j=curr_col;
                first_time = false;
            } else if ( m_grid->GetCellValue(i, j).First(txt->GetValue()) != wxNOT_FOUND ) {
                m_grid->SetGridCursor( i, j);
                m_grid->MakeCellVisible( i, j);
                txt->Enable(true);
                m_grid->SetFocus();
                return;
            }
        }
    }
    wxMessageBox(ADVPCS_GRID_SEARCH_END_MESSAGE, ADVPCS_GRID_SEARCH_END_TITLE, wxOK | wxICON_INFORMATION );
    txt->Enable(true);
    m_grid->SetFocus();
}

void MainFrame::OnToolEnter(wxCommandEvent& event)
{
    if (event.GetSelection() > -1)
    {
        wxString str;
        if (event.GetSelection() == TOOL_ID_HELP) {
            str.Printf(_T(" tool - About application"));
        }
        if (event.GetSelection() == TOOL_ID_COPY) { 
            str.Printf(_T(" tool - Copy selected to clipboard"));
        }
        if (event.GetSelection() == TOOL_ID_PASTE) { 
            str.Printf(_T(" tool - Paste from clipboard"));
        }
        if (event.GetSelection() == TOOL_ID_NEW) { 
            str.Printf(_T(" tool - Clear grid"));
        }
        if (event.GetSelection() == TOOL_ID_OPEN) { 
            str.Printf(_T(" tool - Open Detail file"));
        }
        if (event.GetSelection() == TOOL_ID_SAVE) { 
            str.Printf(_T(" tool - Save Detail file"));
        }
        if (event.GetSelection() == TOOL_ID_ADD) { 
            str.Printf(_T(" tool - Add row into grid"));
        }
        if (event.GetSelection() == TOOL_ID_REMOVE) { 
            str.Printf(_T(" tool - Remove row from grid"));
        }
        if (event.GetSelection() == TOOL_ID_INSERT) { 
            str.Printf(_T(" tool - Insert row into grid"));
        }
        if (event.GetSelection() == TOOL_ID_VALIDATE) { 
            str.Printf(_T(" tool - Validate grid data"));
        }
        if (event.GetSelection() == TOOL_ID_SORT_A) { 
            str.Printf(_T(" tool - Ascending sort"));
        }
        if (event.GetSelection() == TOOL_ID_SORT_D) { 
            str.Printf(_T(" tool - Descending sort"));
        }
        if (event.GetSelection() == TOOL_ID_COMPOSE) { 
            str.Printf(_T(" tool - Compose EDI file"));
        }
        if (event.GetSelection() == TOOL_ID_SEND) { 
            str.Printf(_T(" tool - Send data to server"));
        }
        if (event.GetSelection() == TOOL_ID_STATUS) { 
            str.Printf(_T(" tool - Status request"));
        }
        if (event.GetSelection() == TOOL_ID_SEARCH) { 
            str.Printf(_T(" tool - Search"));
        }
        if (event.GetSelection() == TOOL_ID_UNDO) { 
            str.Printf(_T(" tool - Undo"));
        }
        if (event.GetSelection() == TOOL_ID_REDO) { 
            str.Printf(_T(" tool - Redo"));
        }
        if (event.GetSelection() == TOOL_ID_HEADER) { 
            str.Printf(_T(" tool - Edit Header record"));
        }
        SetStatusText(str, 1);
    }
    else
        SetStatusText("");
}

void MainFrame::OnToolLeftClick(wxCommandEvent& event)
{
    if (event.GetId() == TOOL_ID_HELP)     { OnAbout(event);      }
    if (event.GetId() == TOOL_ID_COPY)     { OnCopy(event);       }
    if (event.GetId() == TOOL_ID_PASTE)    { OnPaste(event);      }
    if (event.GetId() == TOOL_ID_UNDO)     { OnUndo(event);       }
    if (event.GetId() == TOOL_ID_REDO)     { OnRedo(event);       }
    if (event.GetId() == TOOL_ID_NEW)      { OnNew(event);        }
    if (event.GetId() == TOOL_ID_OPEN)     { OnOpen(event);       }
    if (event.GetId() == TOOL_ID_SAVE)     { OnSave(event);       }
    if (event.GetId() == TOOL_ID_ADD)      { AddRow(event);       }
    if (event.GetId() == TOOL_ID_REMOVE)   { RemoveRow();         }
    if (event.GetId() == TOOL_ID_INSERT)   { InsertRow(event);    }
    if (event.GetId() == TOOL_ID_VALIDATE) { OnValidate(event);   }
    if (event.GetId() == TOOL_ID_SORT_A)   { OnSortA(event);      }
    if (event.GetId() == TOOL_ID_SORT_D)   { OnSortD(event);      }
    if (event.GetId() == TOOL_ID_COMPOSE)  { OnCompose(event);    }
    if (event.GetId() == TOOL_ID_SEND)     { OnUpload(event);     }
    if (event.GetId() == TOOL_ID_STATUS)   { OnUpdateStatus(event);   }
    if (event.GetId() == TOOL_ID_SEARCH)   { Search(event);       }
    if (event.GetId() == TOOL_ID_HEADER)   { OnHeaderView(event); }
    if (event.GetId() == TOOL_ID_PASSWD)   { OnChangePassword(event); }
}

void MainFrame::OnUpload(wxCommandEvent& event) {
    wxASSERT(::wxGetApp().GetAgent().IsEnabled());

    if ( !GetExecutor().IsConnected() ) {
        if ( !GetExecutor().Login() ) {
            return;
        }
    }

    ClearLog();

    EdiDocument* d = (EdiDocument*)GetDocument();
    wxString oldFile = d->GetFileName(); 
    
    m_infoNotebook->SetSelection(0);

    if ( ! Compose() ) {
        return;
    };
    wxString fileName = d->GetFileName();
    d->SetFileName(oldFile);

    wxString fName(fileName);
    wxFileName fn(fName);

    if ( wxGetApp().GetCfg()->GetChild(ADVPCS_AGENT_CFG)->GetParamAsBool(ADVPCS_HTTP_AGENT_COMPRESED_CFG) ) {
        fName = fn.GetPath(wxPATH_GET_SEPARATOR) + fn.GetName() + ADVPCS_ZIP_EXT;
    } else {
        fName = fn.GetPath(wxPATH_GET_SEPARATOR) + fn.GetName() + ADVPCS_EDI_EXT;
    }

    if ( GetExecutor().UploadFile(fileName) ) {
        ::wxMessageBox(wxString::Format(ADVPCS_UPLOAD_OK, fName));
    } else {
        ::wxMessageBox(wxString::Format(ADVPCS_UPLOAD_FAIL, fileName), "Error", wxICON_ERROR|wxOK|wxCENTRE);
    }

    m_grid->SetFocus();
}

void MainFrame::OnSaveAs(wxCommandEvent& event) {
    wxString fileName;

    wxFileDialog dialog(this, ADVPCS_GRID_SAVE_TITLE, GetDocument()->GetFileName(), "",
               "Records files (*"+ADVPCS_CSV_EXT+")|*"+ADVPCS_CSV_EXT,
                wxSAVE | wxOVERWRITE_PROMPT );
    if (dialog.ShowModal() == wxID_OK) {
        fileName = dialog.GetPath();
    } else {
        return;
    }
    ::wxGetApp().GetDocManager().SaveAs(*GetDocument(), fileName);

    RefreshTitle();
    m_grid->SetFocus();
}

void MainFrame::ClearLog() {
        m_logList->DeleteAllItems();
};

void MainFrame::OnValidate(wxCommandEvent& event) {
        if ( Validate() ) {
        wxMessageBox(ADVPCS_VALIDATING_ALL_OK);
            m_grid->SetFocus();
        } else {
                m_logList->SetItemState(0, wxLIST_STATE_FOCUSED, wxLIST_STATE_FOCUSED);
        };
};

void MainFrame::OnLogListEnter(wxListEvent& event) {
        unsigned long code = event.GetData();
        ToCodeCell(code);
};

void MainFrame::ToCodeCell(unsigned long code) {
    if ( code > 0 ) {
        unsigned short row = 0;
        unsigned short col = 0;
        col = code & 0x0000FFFF;
        code >>= 16;
        row = code & 0x0000FFFF;
        if ( col >= 0 && m_table->MapToGrid(col) < m_table->GetNumberCols() 
            && row >=0 && row < m_table->GetNumberRows() ) 
        {
            m_grid->SetGridCursor( row, m_table->MapToGrid(col));
            m_grid->MakeCellVisible( row, m_table->MapToGrid(col));
        }
        m_grid->SetFocus();
    }
}

void MainFrame::OnUpdateStatus(wxCommandEvent& event) {
    wxASSERT(::wxGetApp().GetAgent().IsEnabled());

    ClearLog();
    m_infoNotebook->SetSelection(0);

    if ( GetExecutor().RequestStatus() ) {
        m_infoNotebook->SetSelection(1);
        m_statusList->SetFocus();
        ::wxMessageBox(ADVPCS_GET_STATUS_OK);
    } else {
        ::wxMessageBox(ADVPCS_GET_STATUS_FAIL, "Error", wxICON_ERROR|wxOK|wxCENTRE);
    }
};

void MainFrame::GoToCell(wxGridEvent& event) {
    m_grid->SetFocus();
    event.Skip();
};

void MainFrame::SetFocus() {
    wxFrame::SetFocus();
        m_grid->SetFocus();
};

void MainFrame::RefreshTitle() {
    wxString title = ADVPCS_FRAME_TITLE;
    wxString file = GetDocument()->GetFileName().AfterLast('\\');
    if (  0 == file.Cmp(wxEmptyString) ) {
        file = "new";
    }
    title << " {" << file << "}";
    SetTitle(title);

};

void MainFrame::SetLastListColumn() {
    m_logList->SetColumnWidth(2, m_logList->GetSize().GetWidth()-165 < 50 ? 50 : m_logList->GetSize().GetWidth()-165);
    m_statusList->SetColumnWidth(6, m_statusList->GetSize().GetWidth()-785 < 50 ? 50 : m_statusList->GetSize().GetWidth()-785);
};

void MainFrame::OnQuit(wxCommandEvent& WXUNUSED(event)) {
     Close(false);
}

void MainFrame::OnChangePassword(wxCommandEvent& event) {
    m_infoNotebook->SetSelection(0);
    ClearLog();
    if ( GetExecutor().ChangePassword() ) {
        m_infoNotebook->SetSelection(0);
    } else {
        ::wxMessageBox(ADVPCS_CHANGE_PSWD_FAIL, "Error", wxICON_ERROR|wxOK|wxCENTRE);
    }
};

void MainFrame::OnAbout(wxCommandEvent& WXUNUSED(event))
{
    wxMessageBox(ADVPCS_ABOUT_MESSAGE,
                 ADVPCS_ABOUT_TITLE, wxOK | wxICON_INFORMATION, this);
};

AgentExecutor& MainFrame::GetExecutor() {
    return wxGetApp().GetExecutor();
};

void MainFrame::OnUndo(wxCommandEvent& event) {
    m_commandProcessor.Undo();
    UpdateToolbar();
};
void MainFrame::OnRedo(wxCommandEvent& event) {
    m_commandProcessor.Redo();
    UpdateToolbar();
};
void MainFrame::UpdateToolbar() {
    if ( m_commandProcessor.CanUndo() ) {
        m_toolBar->EnableTool(TOOL_ID_UNDO, true);
    } else {
        m_toolBar->EnableTool(TOOL_ID_UNDO, false);
    }
    if ( m_commandProcessor.CanRedo() ) {
        m_toolBar->EnableTool(TOOL_ID_REDO, true);
    } else {
        m_toolBar->EnableTool(TOOL_ID_REDO, false);
    }
};

