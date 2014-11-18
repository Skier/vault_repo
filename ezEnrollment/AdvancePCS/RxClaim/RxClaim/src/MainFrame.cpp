#include <wx/file.h>
#include <wx/progdlg.h>
#include <rxclaim/Messages.h>
#include <rxclaim/MainFrame.h>
#include <rxclaim/Wizard.h>
#include <rxclaim/Messenger.h>

BEGIN_EVENT_TABLE(MyFrame, wxFrame)
    EVT_CLOSE(MyFrame::OnClose)
    EVT_MENU(Detail_New,   MyFrame::OnNew)
    EVT_MENU(Detail_Open,  MyFrame::OnOpen)
    EVT_MENU(Detail_Save,  MyFrame::OnSave)
    EVT_MENU(Detail_SaveAs,  MyFrame::OnSaveAs)
    EVT_MENU(Detail_Quit,  MyFrame::OnQuit)
    EVT_MENU(Detail_Copy,  MyFrame::OnCopy)
    EVT_MENU(Detail_Paste, MyFrame::OnPaste)
    EVT_MENU(Detail_Search, MyFrame::Search)
    EVT_MENU(Detail_Undo, MyFrame::OnUndo)
    EVT_MENU(Detail_SelectAll, MyFrame::OnSelectAll)
    EVT_MENU(Detail_AddRow, MyFrame::AddRow)
    EVT_MENU(Detail_InsertRow, MyFrame::InsertRow)
    EVT_MENU(Detail_RemoveRow, MyFrame::RemoveRow)
    EVT_MENU(Wizard_About, MyFrame::OnAbout)
    EVT_MENU(Wizard_Run,   MyFrame::OnRunWizard)
    EVT_MENU(Header_View,   MyFrame::OnHeaderView)

    EVT_MENU(Detail_Validate, MyFrame::OnValidate)
    EVT_MENU(Detail_Compose, MyFrame::OnCompose)
    EVT_MENU(Detail_Send, MyFrame::OnSend)

    EVT_MENU(-1, MyFrame::OnToolLeftClick)
    
    EVT_GRID_LABEL_LEFT_CLICK(MyFrame::GoToSelect) 
    EVT_GRID_SELECT_CELL( MyFrame::OnSelectCell )
    EVT_GRID_RANGE_SELECT( MyFrame::OnRangeSelected )
    EVT_GRID_CELL_CHANGE( MyFrame::OnCellValueChanged )
    EVT_GRID_EDITOR_SHOWN( MyFrame::OnCellEditorShown )
   
    EVT_TEXT_ENTER(TOOL_ID_SEARCH_STRING, MyFrame::Search)
    EVT_TOOL_ENTER(ID_TOOLBAR, MyFrame::OnToolEnter)

    EVT_WIZARD_CANCEL(-1, MyFrame::OnWizardCancel)
END_EVENT_TABLE()

BEGIN_EVENT_TABLE(Grid, wxGrid)
    EVT_KEY_DOWN( Grid::OnKeyDown )
END_EVENT_TABLE()


MyFrame::MyFrame(const wxString& title,
                 Descriptor* headerDescriptor,
                 Descriptor* detailDescriptor)
       : wxFrame((wxFrame *)NULL, -1, title,
                  wxDefaultPosition, wxSize(-1, -1))  // small frame
{
// Creating header & detail
    try {
        m_header = new DefaultHeader(headerDescriptor);
        m_detail = new DefaultDetail(detailDescriptor);
//        m_detail_undo = new DefaultDetail(detailDescriptor);
        m_composer = new InternalComposer(0, m_header, m_detail, false);
    } catch (wxString ex) {
        wxMessageBox(ex, LOAD_XML_ERROR_TITLE,
                        wxOK | wxCENTRE | wxICON_ERROR );
        exit(1);
    } 

// Init selection
    m_TopSelected = -1;
    m_LeftSelected = -1;
    m_BottomSelected = -1;
    m_RightSelected = -1;

// Init isSaved flag
    isSaved = true;

// Init data file name
    m_DataFileName = new wxString();
    SetDataFileName(wxString());
    m_CurrentCell = new wxString();

// Creating menu
    wxMenu *menuFile = new wxMenu;
    menuFile->Append(Detail_New, "&New\tCtrl-N");
    menuFile->Append(Detail_Open, "&Open\tCtrl-O");
    menuFile->Append(Detail_Save, "&Save\tCtrl-S");
    menuFile->Append(Detail_SaveAs, "&Save As");
    menuFile->AppendSeparator();
    menuFile->Append(Header_View, "Edit Header...");
//    menuFile->Append(Wizard_Run, "&Header wizard...\tCtrl-H");
    menuFile->AppendSeparator();
    menuFile->Append(Detail_Quit, "E&xit", "Quit this program");

    wxMenu *menuEdit = new wxMenu;
    menuEdit->Append(Detail_Copy, "&Copy\tCtrl-C");
//    menuEdit->Enable(Detail_Copy, FALSE);
    menuEdit->Append(Detail_Paste, "&Paste\tCtrl-V");
//    menuEdit->Append(Detail_Undo, "&Undo\tCtrl-Z");
    menuEdit->AppendSeparator();
    menuEdit->Append(Detail_Search, "&Search\tCtrl-F");
    menuEdit->AppendSeparator();
    menuEdit->Append(Detail_SelectAll, "Select &all\tCtrl-A");

    wxMenu *menuGrid = new wxMenu;
    menuGrid->Append(Detail_AddRow, "A&dd Row");
    menuGrid->Append(Detail_InsertRow, "&Insert Row\tCtrl-I");
    menuGrid->Append(Detail_RemoveRow, "&Remove Row\tCtrl-D");

    wxMenu *menuProcess = new wxMenu;
    menuProcess->Append(Detail_Validate, "Validate");
    menuProcess->Append(Detail_Compose, "Compose EDI file");
    menuProcess->Append(Detail_Send, "Send data via email");

    wxMenu *helpMenu = new wxMenu;
    helpMenu->Append(Wizard_About, "&About...\tF1", "Show about dialog");

    wxMenuBar *menuBar = new wxMenuBar();
    menuBar->Append(menuFile, "&File");
    menuBar->Append(menuEdit, "&Edit");
    menuBar->Append(menuGrid, "&Grid");
    menuBar->Append(menuProcess, "&Process");
    menuBar->Append(helpMenu, "&Help");

    SetMenuBar(menuBar);

    CreateStatusBar();

    SetIcon(wxICON(mondrian));

    RecreateToolbar();

    grid = new Grid( this,
                     -1,
                     wxPoint( -1, -1 ),
                     wxSize( 600, 350 ) );
    grid->SetRowLabelSize(50);
    grid->SetColLabelSize( 25);
    grid->AutoSizeRows();
    grid->DisableDragColSize();
    grid->DisableDragRowSize();

    grid->SetTable(GetDetail()->GetTable());
    
    //BF
//  CopyDetail( GetDetail(), GetDetailUndo() );
    
    wxBoxSizer *topSizer = new wxBoxSizer( wxVERTICAL );
        
    wxBoxSizer *gridSizer = new wxBoxSizer( wxVERTICAL );

    gridSizer->Add( grid,
                  1,
                  wxEXPAND | wxALL, 3 );

    topSizer->Add( gridSizer,
                   1,
                   wxEXPAND );

    SetAutoLayout( TRUE );
    SetSizer( topSizer );

    topSizer->Fit( this );
    topSizer->SetSizeHints( this );
    
    CentreOnScreen();
}

void Grid::OnKeyDown(wxKeyEvent& event)
{
    size_t keycode = event.GetKeyCode();
    if ( WXK_TAB == keycode ) {
        if (event.ShiftDown()) {
            if ( (GetGridCursorCol() == 0) && (GetGridCursorRow() > 0) ) {
                SetGridCursor(GetGridCursorRow() - 1, GetNumberCols() - 1);
                MakeCellVisible(GetGridCursorRow() - 1, GetNumberCols() - 1);
            } else {
                wxGrid::OnKeyDown( event );
            }
        } else {
            if ( GetGridCursorCol() == GetNumberCols() - 1 ) {
                if (GetGridCursorRow() == GetNumberRows() - 1) {
                    AppendRows();
                    SetGridCursor(GetNumberRows() - 1, 0);
                    MakeCellVisible(GetNumberRows() - 1, 0);
                } else {
                    SetGridCursor(GetGridCursorRow() + 1, 0);
                    MakeCellVisible(GetGridCursorRow() + 1, 0);
                }
            } else {
                wxGrid::OnKeyDown( event );
            }
        }
    } else {
        wxGrid::OnKeyDown( event );
    }
}

void MyFrame::GoToSelect(wxGridEvent& m_event)
{
    if ( grid->GetNumberRows() > 0 ) {
        if ((m_event.GetRow() == -1) && (m_event.GetCol() != -1)) {
            grid->SetGridCursor(m_event.GetRow() + 1, m_event.GetCol());
        }
        if ((m_event.GetRow() != -1) && (m_event.GetCol() == -1)) {
            grid->SetGridCursor(m_event.GetRow(), m_event.GetCol() + 1);
        }
    }
    m_event.Skip();
}

void MyFrame::AddRow(wxCommandEvent& WXUNUSED(event))
{
    //BF
//  CopyDetail( GetDetail(), GetDetailUndo() );

    if ( grid->GetNumberRows() > 0 ) {
        grid->SetGridCursor( 0, 0);
    }

    grid->AppendRows();
    grid->ClearSelection();
    isSaved = false;
}

void MyFrame::InsertRow(wxCommandEvent& WXUNUSED(event))
{
    //BF
//  CopyDetail( GetDetail(), GetDetailUndo() );

    if ( grid->GetNumberRows() > 0 ) {
        grid->SetGridCursor( 0, 0);
    }

    grid->InsertRows(grid->GetGridCursorRow());
    isSaved = false;
}

void MyFrame::RemoveRow()
{
    // BF
//  CopyDetail( GetDetail(), GetDetailUndo() );

    if ( grid->IsSelection() ) {
        if ( ( m_LeftSelected == 0 )
                && ( m_RightSelected == ( grid->GetNumberCols() - 1 ) )
                && ( m_TopSelected < m_BottomSelected ) ) {
            grid->SetGridCursor(m_TopSelected, m_LeftSelected);
            int number = m_BottomSelected - m_TopSelected + 1;
            for (int i=0; i<number; i++) {
                grid->DeleteRows(m_TopSelected);
            }
        } else {
            grid->DeleteRows(grid->GetGridCursorRow());
        }
    } else {
        grid->DeleteRows(grid->GetGridCursorRow());
    }
    isSaved = false;
}

void MyFrame::OnValidate(wxCommandEvent& event)
{
    if ( ! GetHeader()->IsValid() ) {
        wxMessageBox(VALIDATING_HEADER_INVALID, 
                        VALIDATING_DETAIL_PROCESS_TITLE,
                        wxOK | wxCENTRE | wxICON_ERROR );
        OnHeaderView(event);
        return;
    }

    if ( grid->GetNumberRows() == 0 ) {
        wxMessageBox(VALIDATING_DETAIL_EMPTY, 
                        VALIDATING_DETAIL_PROCESS_TITLE,
                        wxOK | wxCENTRE | wxICON_EXCLAMATION );
        return;
    }

    if ( grid->GetNumberRows() > 0 ) {
        grid->SetGridCursor( 0, 0);
    }

    if (GetDetail()->IsValid()) {
        wxMessageBox(VALIDATING_ALL_OK);
    }
    else {
        wxMessageBox(VALIDATING_DETAIL_INVALID, 
                        VALIDATING_DETAIL_PROCESS_TITLE,
                        wxOK | wxCENTRE | wxICON_ERROR );
    }
}

void MyFrame::OnCompose(wxCommandEvent& event)
{
    if ( grid->GetNumberRows() == 0 ) {
        wxMessageBox(COMPOSE_DETAIL_EMPTY, 
                        VALIDATING_DETAIL_PROCESS_TITLE,
                        wxOK | wxCENTRE | wxICON_EXCLAMATION );
        return;
    }

    if ( ! GetHeader()->IsValid() ) {
        wxMessageBox(COMPOSE_HEADER_INVALID, 
                        VALIDATING_DETAIL_PROCESS_TITLE,
                        wxOK | wxCENTRE | wxICON_ERROR );
        OnHeaderView(event);
        return;
    }

    if ( grid->GetNumberRows() > 0 ) {
    grid->SetGridCursor( 0, 0);
    }

    if (! GetDetail()->IsValid()) {
        wxMessageBox(COMPOSE_DETAIL_INVALID, 
                        VALIDATING_DETAIL_PROCESS_TITLE,
                        wxOK | wxCENTRE | wxICON_ERROR );
        return;
    }
    
    if ( !isSaved ) {
        OnSave(event);
    }
    
    GetMessenger()->SetEdiPath(GetDataFileName()->BeforeLast('\\'));
    GetMessenger()->SetComposer(m_composer);
    GetMessenger()->ProcessMessage(false, true);
}

void MyFrame::OnSend(wxCommandEvent& event)
{
    if ( grid->GetNumberRows() == 0 ) {
        wxMessageBox(MAILING_DETAIL_EMPTY, 
                        VALIDATING_DETAIL_PROCESS_TITLE,
                        wxOK | wxCENTRE | wxICON_EXCLAMATION );
        return;
    }

    if ( ! GetHeader()->IsValid() ) {
        wxMessageBox(MAILING_HEADER_INVALID, 
                        VALIDATING_DETAIL_PROCESS_TITLE,
                        wxOK | wxCENTRE | wxICON_ERROR );
        OnHeaderView(event);
        return;
    }

    if ( grid->GetNumberRows() > 0 ) {
        grid->SetGridCursor( 0, 0);
    }

    if (! GetDetail()->IsValid()) {
        wxMessageBox(MAILING_DETAIL_INVALID, 
                        VALIDATING_DETAIL_PROCESS_TITLE,
                        wxOK | wxCENTRE | wxICON_ERROR );
        return;
    }
    
    if ( !isSaved ) {
        OnSave(event);
    }

    GetMessenger()->SetEdiPath(GetDataFileName()->BeforeLast('\\'));
    GetMessenger()->SetComposer(m_composer);
    GetMessenger()->ProcessMessage(false, false);
}

void MyFrame::OnQuit(wxCommandEvent& WXUNUSED(event))
{
    Close(false);
}

void MyFrame::OnSelectCell( wxGridEvent& ev )
{
    if ( ev.Selecting() ) {
        
        SetStatusText(GetDetail()->GetColHint(ev.GetCol()));
        *m_CurrentCell = GetDetail()->GetValue(ev.GetRow(), ev.GetCol());
    }
    ev.Skip();
}

void MyFrame::OnRangeSelected( wxGridRangeSelectEvent& ev )
{
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

void MyFrame::OnNew(wxCommandEvent& WXUNUSED(event))
{
    // BF
//  CopyDetail( GetDetail(), GetDetailUndo() );
    
    for (int i=0; i<grid->GetNumberRows(); )
    {
        grid->DeleteRows(i);
    };
    GetHeader()->Clear();
    SetDataFileName(wxString(""));
    isSaved = true;
    wxString title = wxString(FRAME_TITLE);
    if ( GetDataFileName()->IsEmpty() ) {
        title << " {" << "new" << "}";
    } else {
        title << " {" << GetDataFileName()->AfterLast('\\') << "}";
    }
    SetTitle(title);
}

void MyFrame::OnHeaderView(wxCommandEvent& WXUNUSED(event))
{
    HeaderFrame* head = new HeaderFrame(this, GetHeader());
    head->Show();
}

void MyFrame::OnOpen(wxCommandEvent& WXUNUSED(event))
{
    wxFileDialog dialog
                 (
                    this,
                    _T(GRID_OPEN_TITLE),
                    _T(""),
                    _T(""),
                    _T("Records files (*.csv)|*.csv")
                 );

    if (dialog.ShowModal() == wxID_OK)
    {
        Open(wxString(dialog.GetPath().c_str()));
    }
    
}

void MyFrame::Open(wxString& filename)
{
    wxCommandEvent *ev = new wxCommandEvent;
    OnNew(*ev);
    
    ::wxSetCursor(*wxHOURGLASS_CURSOR);
    
    try {
        GetDetail()->Load(filename); 
        GetHeader()->Load(filename); 
        SetDataFileName(filename);
        isSaved = true;
        wxString title = wxString(FRAME_TITLE);
        if ( GetDataFileName()->IsEmpty() ) {
            title << " {" << "new" << "}";
        } else {
            title << " {" << GetDataFileName()->AfterLast('\\') << "}";
        }
        SetTitle(title);
        // BF
   //   CopyDetail( GetDetail(), GetDetailUndo() );
    } catch (wxString ex) {
        wxMessageBox(ex, GRID_OPEN_ERROR_TITLE,
                        wxOK | wxCENTRE | wxICON_ERROR );
        return;
    }
    ::wxSetCursor(*wxSTANDARD_CURSOR);
 
}

void MyFrame::OnSaveAs(wxCommandEvent& event)
{
    SetDataFileName(wxString(""));
    OnSave(event);
}

void MyFrame::OnSave(wxCommandEvent& WXUNUSED(event))
{
    if ( GetDataFileName()->Len() == 0 ) {
        wxFileDialog dialog(this, GRID_SAVE_TITLE, "", *GetDataFileName(),
            "Records files (*.csv)|*.csv",
            wxSAVE | wxOVERWRITE_PROMPT );
        if (dialog.ShowModal() == wxID_OK) {
            SetDataFileName(wxString(dialog.GetPath().c_str()));
        } else {
            return;
        }
    }
    
    ::wxSetCursor(*wxHOURGLASS_CURSOR);

    wxFile file(GetDataFileName()->c_str(), wxFile::write);
    wxString names, values;

    if ( grid->GetNumberRows() > 0 ) {
        grid->SetGridCursor( 0, 0);
    }

    for (int m=0; m<GetHeader()->GetSize(); m++) {
        HeaderField* field = GetHeader()->GetField(m);
        names += field->GetDescriptor()->GetName();
        values += field->GetValue();
        names += DEFAULT_COLUMN_SEPARATOR;
        values += DEFAULT_COLUMN_SEPARATOR;
    }
    file.Write(names + DEFAULT_LINE_SEPARATOR);
    file.Write(values + DEFAULT_LINE_SEPARATOR);

    Descriptor* descriptor = GetDetail()->GetDescriptor();
    int k;
    names="";

    for (size_t n=0; n<descriptor->GetSize(); n++) {
                names += descriptor->GetElementDescriptor(n)->GetName();
                names += DEFAULT_COLUMN_SEPARATOR;
    }
    
    file.Write(names + DEFAULT_LINE_SEPARATOR);

    long numberRows = grid->GetNumberRows();

    wxProgressDialog dialog(GRID_SAVE_PROGRESS_TITLE,
                            GRID_SAVE_PROGRESS_MESSAGE,
                            numberRows, 
                            this,
                            wxPD_AUTO_HIDE |
                            wxPD_APP_MODAL );
    for (int i=0; i<numberRows; i++) {
        wxArrayString row;
        k = 0;
        // prepare values
        for (size_t j=0; j<descriptor->GetSize(); j++) {
            XmlElementDescriptor* colDesc = (XmlElementDescriptor*)descriptor->GetElementDescriptor(j);
            if ( colDesc->IsEnabled() ) {
                row.Add(GetDetail()->GetValue(i, k++));
            } else {
                row.Add(colDesc->GetDefaultValue());
            }
        }
        // transformation for not enabled dependence
        for (size_t l=0; l<descriptor->GetSize(); l++) {
            XmlElementDescriptor* colDesc = (XmlElementDescriptor*)descriptor->GetElementDescriptor(l);
            ElementDependence* dep = colDesc->GetDependence();
            if ( NULL != dep ) {
                size_t index = dep->GetFieldIndex();
                if ( index >= 0 
                     && index < descriptor->GetSize()
                     && !descriptor->GetElementDescriptor(index)->IsEnabled() ) {
                    row[index] = colDesc->DependenceValue(row[l]);
                }
            }
        }
        // writing row to file
        wxString rowStr;
        for (size_t m=0; m<descriptor->GetSize(); m++) {
            rowStr << (row[m] + DEFAULT_COLUMN_SEPARATOR);
        }
        file.Write(rowStr + DEFAULT_LINE_SEPARATOR);
        // update progress bar
    if ( (i % (numberRows/20 + 1)) == 0 ){
        dialog.Update(i);
    }
    }
    file.Close();
    dialog.Update(numberRows);
    ::wxSetCursor(*wxSTANDARD_CURSOR);
    isSaved = true;
    wxString title = wxString(FRAME_TITLE);
    if ( GetDataFileName()->IsEmpty() ) {
        title << " {" << "new" << "}";
    } else {
        title << " {" << GetDataFileName()->AfterLast('\\') << "}";
    }
    SetTitle(title);
}

void MyFrame::OnCopy(wxCommandEvent& WXUNUSED(event))
{
    wxString *m_text = new wxString("");

    if (grid->IsSelection())
    {
        if ( m_TopSelected < 0 ) {
            m_TopSelected = 0;
        }
        if ( m_BottomSelected > grid->GetNumberRows()-1 ) {
            m_BottomSelected = grid->GetNumberRows()-1;
        }
        if ( m_LeftSelected < 0 ) {
            m_LeftSelected = 0;
        }
        if ( m_RightSelected > grid->GetNumberCols()-1 ) {
            m_RightSelected = grid->GetNumberCols()-1;
        }
        for (int i=m_TopSelected; i<=m_BottomSelected; i++){
            for (int j=m_LeftSelected; j<=m_RightSelected; j++) {
                *m_text << grid->GetCellValue(i, j);
                *m_text << "\t";
            }
//            *m_text << grid->GetCellValue(i, m_RightSelected);
            *m_text << "\n";
        }
        wxTextDataObject *m_text_object = new wxTextDataObject(*m_text);
        if (wxTheClipboard->Open()) {
            wxTheClipboard->SetData( m_text_object );
            wxTheClipboard->Close();
        }
    } else if ((grid->GetGridCursorRow() != -1) && (grid->GetGridCursorCol() != -1)) {
        *m_text << grid->GetCellValue(grid->GetGridCursorRow(), grid->GetGridCursorCol());
        *m_text << "\t\n";
        wxTextDataObject *m_text_object = new wxTextDataObject(*m_text);
        if (wxTheClipboard->Open()) {
            wxTheClipboard->SetData( m_text_object );
            wxTheClipboard->Close();
        }
    }
}

void MyFrame::OnPaste(wxCommandEvent& event)
{

    // BF
//  CopyDetail( GetDetail(), GetDetailUndo() );
    try {
        ClipboardMatrix m_matrix;
        if ( grid->GetNumberRows() < 1 ) {
            AddRow(event);
            grid->SetGridCursor( 0, 0);
        }
//----- one-to-many pasting
        if ( grid->IsSelection() ) {
            int vertSize = (m_BottomSelected - m_TopSelected + 1);
            int horSize = (m_RightSelected - m_LeftSelected + 1);
            if ( m_matrix.GetSize() == vertSize  && m_matrix.GetVector(0).GetSize() == horSize ) {
                grid->SetGridCursor(m_TopSelected, m_LeftSelected);
                PasteMatrix(m_matrix);
            } else if ( (m_matrix.GetSize() == 1) && (m_matrix.GetVector(0).GetSize() == 1) ) {
                for (int i=m_TopSelected; i<=m_BottomSelected; i++){
                    for (int j=m_LeftSelected; j<=m_RightSelected; j++) {
                        grid->SetCellValue(i, j, m_matrix.GetVector(0).GetDataAt(0));
                    }
                }
            } else {
                throw wxString(GRID_PASTE_ERROR_MESSAGE);
            }
        } else {
            PasteMatrix(m_matrix);
        }
        PasteMatrix(m_matrix);
//-------------------------
        isSaved = false;
    } catch (wxString ex) {
        wxMessageBox(ex, GRID_PASTE_ERROR_TITLE,
                        wxOK | wxCENTRE | wxICON_ERROR );
    }
}

void MyFrame::PasteVector(long offset, Vector &v)
{
    for ( size_t i=0; i<v.GetSize(); i++ ) {
        if ( (grid->GetGridCursorCol()+i) < grid->GetNumberCols() ) {
            if ( v.GetDataAt(i).Last() == '\r' ) {
                grid->SetCellValue(grid->GetGridCursorRow()+offset, grid->GetGridCursorCol()+i, v.GetDataAt(i).RemoveLast());
            } else {
                grid->SetCellValue(grid->GetGridCursorRow()+offset, grid->GetGridCursorCol()+i, v.GetDataAt(i));
            }
        }
    }
}

void MyFrame::PasteMatrix(Matrix &m)
{
    for ( size_t i=0; i<m.GetSize(); i++ ) {
        if ( (grid->GetGridCursorRow()+i) == grid->GetNumberRows() ) {
            grid->AppendRows();
        }
        PasteVector(i, m.GetVector(i));
    }
}

void MyFrame::OnCellEditorShown( wxGridEvent& ev )
{
    ev.Skip();
}

void MyFrame::OnCellValueChanged ( wxGridEvent& ev )
{
    // BF
//  CopyDetail( GetDetail(), GetDetailUndo() );
   // GetDetailUndo()->SetValue( ev.GetRow(), ev.GetCol(), *m_CurrentCell );
    wxColour REQUIRED_COL = wxColour(230, 250, 255);
    wxColour VALID_COL = wxColour(255, 255, 255);
    wxColour NOT_VALID_COL = wxColour(255, 255, 135);
    
    int row = ev.GetRow();
    int col = ev.GetCol();
    ElementDescriptor* ed = GetDetail()->GetColumnDescriptor(col);

    if (IsCellValid(row, col, ed)) {
        if ( grid->GetCellBackgroundColour(row, col) == NOT_VALID_COL ) {
            if ( ed->IsRequired() ) {
                grid->SetCellBackgroundColour(row, col, REQUIRED_COL);
            } else {
                grid->SetCellBackgroundColour(row, col, VALID_COL);
            }
        }
    } else {
        grid->SetCellBackgroundColour(row, col, NOT_VALID_COL);
    }
    isSaved = false;
    ev.Skip();
}


bool MyFrame::IsCellValid(int i, int j, ElementDescriptor* ed)
{
    if ( ed->GetType().Cmp("text") == 0 ) {
        GetDetail()->SetValue(i, j, GetDetail()->GetValue(i, j).Upper());
    }
    
    if ( ( ed->GetType().Cmp("date")==0 || ed->GetType().Cmp("longdate")==0 ) && GetDetail()->GetValue(i, j).Cmp("  /  /    ")==0 ) {
        grid->SetCellValue(i, j, "");
    }
    return ed->IsValid(GetDetail()->GetValue(i, j));

}


void MyFrame::OnUndo(wxCommandEvent& WXUNUSED(event))
{
    if ( grid->GetNumberRows() > 0 ) {
        grid->SetGridCursor( 0, 0);
    }
    isSaved = false;
    // BF
//  CopyDetail( GetDetailUndo(), GetDetail() );
}

/* void MyFrame::CopyDetail(Detail* from, Detail* to)
{
    for ( int i=0; i<to->GetNumberRows(); ) {
        to->DeleteRows( i, 1 );
    }
    if ( ! (from->GetNumberRows() == 0 ) ) {
        for ( i=0; i<from->GetNumberRows(); i++ ) {
            if ( to->AppendRows(1) ) {
                for ( int j=0; j<from->GetNumberCols(); j++) {
                    to->SetValue( i, j, from->GetValue( i, j ) );
                }
            }
        }
    }
}*/

void MyFrame::Search(wxCommandEvent& WXUNUSED(event))
{
    int curr_col = grid->GetGridCursorCol();
    int curr_row = grid->GetGridCursorRow();
    bool first_time = true;
    wxTextCtrl* s_txt = (wxTextCtrl *)GetToolBar()->FindControl(TOOL_ID_SEARCH_STRING);
    if ( s_txt->GetLabel().IsEmpty() ) {
        grid->SetFocus();
        return;
    }
    s_txt->SetLabel(s_txt->GetLabel().Upper());
    s_txt->Enable(false);
    for (int i=0; i<grid->GetNumberRows(); i++) {
        for (int j=0; j<grid->GetNumberCols(); j++) {
            if ( first_time ) {
                i=curr_row;
                j=curr_col;
                first_time = false;
            } else if ( grid->GetCellValue(i, j).First(s_txt->GetLabel()) != wxNOT_FOUND ) {
                grid->SetGridCursor( i, j);
                grid->MakeCellVisible( i, j);
                s_txt->Enable(true);
                grid->SetFocus();
                return;
            }
        }
    }
    wxMessageBox(GRID_SEARCH_END_MESSAGE, GRID_SEARCH_END_TITLE, wxOK | wxICON_INFORMATION );
    s_txt->Enable(true);
    grid->SetFocus();
}

void MyFrame::OnSelectAll(wxCommandEvent& WXUNUSED(event))
{
    grid->SelectAll();
    grid->SetGridCursor( 0, 0);
}

void MyFrame::OnAbout(wxCommandEvent& WXUNUSED(event))
{
    wxMessageBox(GRID_HELP_ABOUT_MESSAGE,
                 GRID_HELP_ABOUT_TITLE, wxOK | wxICON_INFORMATION, this);
}

void MyFrame::OnRunWizard(wxCommandEvent& WXUNUSED(event))
{

    wxWizard *wizard = new wxWizard(this, -1,
                    WIZARD_TITLE,
                    wxBITMAP(wiztest));
    
    RadioboxPage* page2 = new RadioboxPage(wizard, this);
    HeaderRecordPage* page3 = new HeaderRecordPage(wizard, GetHeader());
    FinishPage* page4 = new FinishPage(wizard);

    wxWizardPageSimple::Chain(page2, page3);
    wxWizardPageSimple::Chain(page3, page4);
    
    wizard->SetPageSize(wxSize(300,250));

    wizard->RunWizard(page2);
    wizard->Destroy();
//      isSaved = false;
}

void MyFrame::OnWizardCancel(wxWizardEvent& event)
{
    if ( wxMessageBox(WIZARD_CANCEL_MESSAGE, WIZARD_CANCEL_TITLE,
                          wxICON_QUESTION | wxYES_NO, this) != wxYES )
    {
        event.Veto();
    }

}

void MyFrame::RecreateToolbar()
{
    wxToolBarBase *toolBar = GetToolBar();
    delete toolBar;

    SetToolBar(NULL);

    long style = wxNO_BORDER | wxTB_FLAT | wxTB_DOCKABLE;

    toolBar = CreateToolBar(style, ID_TOOLBAR);
    toolBar->SetMargins( 4, 4 );

    wxBitmap toolBarBitmaps[17];

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
    toolBarBitmaps[15] = wxBITMAP(undo);
    toolBarBitmaps[16] = wxBITMAP(search);

    int width = 24;
    int currentX = 5;

    toolBar->AddTool(TOOL_ID_NEW, toolBarBitmaps[0], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Clear grid");
    currentX += width + 5;

    toolBar->AddTool(TOOL_ID_OPEN, toolBarBitmaps[1], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Open file");
    currentX += width + 5;
    
    toolBar->AddTool(TOOL_ID_SAVE, toolBarBitmaps[2], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Save file");
    currentX += width + 5;
    
    toolBar->AddSeparator();
    
    toolBar->AddTool(TOOL_ID_COPY, toolBarBitmaps[3], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Copy to clipboard");
//    toolBar->EnableTool(TOOL_ID_COPY, FALSE);
    currentX += width + 5;
    
    toolBar->AddTool(TOOL_ID_PASTE, toolBarBitmaps[5], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Paste from clipboard");
    currentX += width + 5;
    
    toolBar->AddSeparator();
    
//    toolBar->AddTool(TOOL_ID_UNDO, toolBarBitmaps[15], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Undo");
//    currentX += width + 5;
    
//    toolBar->AddSeparator();
    
    toolBar->AddTool(TOOL_ID_HEADER, toolBarBitmaps[14], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Edit Header record");
    currentX += width + 5;
    
    toolBar->AddSeparator();
    
    toolBar->AddTool(TOOL_ID_ADD, toolBarBitmaps[8], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Append row into grid");
    currentX += width + 5;
    
    toolBar->AddTool(TOOL_ID_REMOVE, toolBarBitmaps[9], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Remove current row");
    currentX += width + 5;
    
    toolBar->AddTool(TOOL_ID_INSERT, toolBarBitmaps[10], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Insert row");
    currentX += width + 5;
    
    toolBar->AddSeparator();
    
    toolBar->AddTool(TOOL_ID_VALIDATE, toolBarBitmaps[11], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Validate grid data");
    currentX += width + 5;
    
    toolBar->AddTool(TOOL_ID_COMPOSE, toolBarBitmaps[12], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Make EDI file for sending");
    currentX += width + 5;
    
    toolBar->AddTool(TOOL_ID_SEND, toolBarBitmaps[13], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Send file via email");
    currentX += width + 5;
    
    toolBar->AddSeparator();
    wxTextCtrl* s_str = new wxTextCtrl(toolBar, TOOL_ID_SEARCH_STRING);
    toolBar->AddControl((wxControl *)s_str);
    toolBar->AddTool(TOOL_ID_SEARCH, toolBarBitmaps[16], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Search");
    currentX += width + 5;
    
    toolBar->AddSeparator();

    toolBar->AddTool(TOOL_ID_HELP, toolBarBitmaps[7], wxNullBitmap, FALSE, currentX, -1, (wxObject *) NULL, "Help button");

    toolBar->Realize();
}

void MyFrame::OnToolEnter(wxCommandEvent& event)
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
        if (event.GetSelection() == TOOL_ID_COMPOSE) { 
            str.Printf(_T(" tool - Compose EDI file"));
        }
        if (event.GetSelection() == TOOL_ID_SEND) { 
            str.Printf(_T(" tool - Send EDI file via email"));
        }
        if (event.GetSelection() == TOOL_ID_SEARCH) { 
            str.Printf(_T(" tool - Search"));
        }
//        if (event.GetSelection() == TOOL_ID_UNDO) { 
//            str.Printf(_T(" tool - Undo"));
//        }
        if (event.GetSelection() == TOOL_ID_HEADER) { 
            str.Printf(_T(" tool - Edit Header record"));
        }
        SetStatusText(str);
    }
    else
        SetStatusText("");
}

void MyFrame::OnToolLeftClick(wxCommandEvent& event)
{
    if (event.GetId() == TOOL_ID_HELP)     { OnAbout(event);      }
    if (event.GetId() == TOOL_ID_COPY)     { OnCopy(event);       }
    if (event.GetId() == TOOL_ID_PASTE)    { OnPaste(event);      }
    if (event.GetId() == TOOL_ID_NEW)      { OnNew(event);        }
    if (event.GetId() == TOOL_ID_OPEN)     { OnOpen(event);       }
    if (event.GetId() == TOOL_ID_SAVE)     { OnSave(event);       }
    if (event.GetId() == TOOL_ID_ADD)      { AddRow(event);       }
    if (event.GetId() == TOOL_ID_REMOVE)   { RemoveRow();         }
    if (event.GetId() == TOOL_ID_INSERT)   { InsertRow(event);    }
    if (event.GetId() == TOOL_ID_VALIDATE) { OnValidate(event);   }
    if (event.GetId() == TOOL_ID_COMPOSE)  { OnCompose(event);    }
    if (event.GetId() == TOOL_ID_SEND)     { OnSend(event);       }
    if (event.GetId() == TOOL_ID_SEARCH)   { Search(event);       }
//    if (event.GetId() == TOOL_ID_UNDO)     { OnUndo(event);         }
    if (event.GetId() == TOOL_ID_HEADER)   { OnHeaderView(event); }
}
 
void MyFrame::OnClose(wxCloseEvent& event)
{
    if ( event.CanVeto() )
    {
        if ( isSaved ) {
            wxMessageDialog dialog( this, _T (GRID_CANCEL_MESSAGE),
                        _T(GRID_CANCEL_TITLE),
                        wxYES_NO | wxNO_DEFAULT | wxCENTRE | wxICON_QUESTION);
            if ( dialog.ShowModal() == wxID_YES ) {
                event.Skip();
                return;
            } else {
                event.Veto();
                return;
            }
        }
        wxMessageDialog dialog( this, _T (GRID_CANCEL_WITHOUT_SAVE_MESSAGE),
                     _T(GRID_CANCEL_WITHOUT_SAVE_TITLE),
                     wxYES_NO | wxYES_DEFAULT | wxCANCEL | wxCENTRE | wxICON_QUESTION );
        int result = dialog.ShowModal();
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
