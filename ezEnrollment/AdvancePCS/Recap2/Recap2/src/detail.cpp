#include <wx/file.h>
#include <wx/progdlg.h>
#include <client/detail.h>
#include <client/filematrix.h>
#include <client/DateEditor.h>
#include <client/Messages.h>
#include <client/Choice.h>

#define MAX_ERRORS 200
//
// DefaultDetail
//
DefaultDetail::DefaultDetail(Descriptor* descriptor)
    : m_descriptor(descriptor) 
{
    m_data = new ListArray();
    m_columns = new wxList();
    Prepare();
};

DefaultDetail::~DefaultDetail()
{
    Clean();
    delete m_data;
    delete m_columns;
};

void DefaultDetail::SetValue(int row, int col, const wxString& value)
{
    XmlElementDescriptor* colDesc = (XmlElementDescriptor*)GetColumnDescriptor(col);
    wxString val(value);
    val = colDesc->CutValue(val);
    wxList* rowd = GetRowData(row);
    wxString* v = (wxString*)rowd->Item(col)->GetData();
    v->Clear();
    *v << val;

    // Dependiences 
    ElementDependence* dep = colDesc->GetDependence();
    if ( NULL != dep ) {
        size_t depIndex = dep->GetFieldIndex();
        if ( depIndex < GetDescriptor()->GetSize() 
             && depIndex >= 0 
             && GetValue(row, depIndex).IsEmpty()) {
            SetValue(row, depIndex, colDesc->DependenceValue(wxString(value)));
        }
    }
};


void DefaultDetail::SetView(wxGrid* grid)
{
    wxColour REQUIRED_COL = wxColour(230, 250, 255);
    wxGridTableBase::SetView(grid);
    for (int i=0; i<GetNumberCols(); i++) {
        m_cellAttr = new wxGridCellAttr();
        ElementDescriptor* ed = GetColumnDescriptor(i);
        wxGridCellEditor* m_editor = NULL;
        wxString *param = new wxString();
        if ((ed->GetType().Cmp("date")==0)||(ed->GetType().Cmp("longdate")==0)) {
            //wxGridCellTextEditor* text = new wxGridCellTextEditor();
            m_editor = new GridDateCellEditor();
            *param << "10";
        } else if (ed->GetType().Cmp("choice")==0) {
            int count = ed->GetChoices().GetCount();
            GridChoice* choice = new GridChoice(0, NULL, FALSE);
   			for (int i=0; i<ed->GetChoices().GetCount(); i++) {
        		*param << (ed->GetChoices().Item(i));
        		*param << ",";
    		}
    		m_editor = choice;
    	} else {
            wxGridCellTextEditor* text = new wxGridCellTextEditor();
            m_editor = text;
            *param << ed->GetMaxSize();
        }
        m_editor->SetParameters(*param);
        m_cellAttr->SetEditor(m_editor);
        if ( ed->IsRequired() ) {
            m_cellAttr->SetBackgroundColour(REQUIRED_COL);
        }
        grid->SetColAttr(i, m_cellAttr);
        wxString label = ed->GetName();
        grid->SetColSize(i, ((ed->GetMaxSize() * 7 + 20)>(label.Len() * 6 + 30)) ? (ed->GetMaxSize() * 7 + 20) : (label.Len() * 6 + 30));
    }
};

bool DefaultDetail::IsValid()
{
#if 1
    wxColour REQUIRED_COL = wxColour(230, 250, 255);
    wxColour VALID_COL = wxColour(255, 255, 255);
    wxColour NOT_VALID_COL = wxColour(255, 255, 135);

    wxGrid* grid = GetView();

    int maxrows = GetNumberRows();
    int maxcols = GetNumberCols();
    bool m_valid = true;
    int error_count = 0;

    int first_error_row = -1;
    int first_error_col = -1;

   	wxProgressDialog dialog(VALIDATING_DETAIL_PROCESS_TITLE,
                       		VALIDATING_DETAIL_PROCESS_MESSAGES,
                       		maxrows, 
                       		GetView(),
                       		wxPD_AUTO_HIDE |
                       		wxPD_APP_MODAL );
    for (int i=0; i<maxrows; i++) {

//------ dialog update
    	if ( i % (maxrows/20 + 1) == 0 ) {
    		dialog.Update(i);
    	}

        for (int j=0; j<maxcols; j++) {
        	ElementDescriptor* ed = GetColumnDescriptor(j);
			wxString value = GetValue(i, j);                

// ----- convert to Upper Case
       		if ( ed->GetType().Cmp("text") == 0 ) {
       			SetValue(i, j, value.Upper());
       		}

//------ remove last <CR> or <LF>
       		if ( GetValue(i, j).Length() > 0 
                     && ((GetValue(i, j).Last() == '\r') || (GetValue(i, j).Last() == '\n'))
                        ) {
       			SetValue(i, j, GetValue(i, j).RemoveLast());
       		}

//------ parsing dates
            if ( ed->GetType().Cmp("date")==0 ) {
            	wxDateTime date;
            	if ( value.Cmp("    /  /  ")==0 ) {
            		grid->SetCellValue(i, j, "");
            	} else if ( date.ParseFormat(value, "%Y/%m/%d") ) {
            		grid->SetCellValue(i, j, date.Format("%Y/%m/%d"));
            	};
            }

//------ Verification
            if ( !ed->IsValid( GetValue(i, j) ) ) {
        		grid->SetCellBackgroundColour(i, j, NOT_VALID_COL);
                m_valid = false;
                error_count++;
                if ( error_count > MAX_ERRORS ) {
                	break;
                }
            } else {
                if ( grid->GetCellBackgroundColour(i, j) == NOT_VALID_COL ) {
                    if ( ed->IsRequired() ) {
                        grid->SetCellBackgroundColour(i, j, REQUIRED_COL);
                    } else {
                        grid->SetCellBackgroundColour(i, j, VALID_COL);
                    }
                }
            }

// Termination Date and Action Code dependences
#if 0
            long TDcol = GetDescriptor()->GetElementDescriptorIndex(wxString("Termination Date"));
            long ACcol = GetDescriptor()->GetElementDescriptorIndex(wxString("Action Code"));

            if ( j==TDcol ) {
            	// todo: reaction on input Action Code or Termination Date
            	wxString val = wxString( GetValue( i, ACcol ) ); 
            	if ( val.Cmp(" 1")==0 || val.Cmp(" 7")==0 ) {
            		if ( GetValue(i, TDcol).IsEmpty() ) {
            			grid->SetCellBackgroundColour(i, TDcol, NOT_VALID_COL);
                        m_valid = false;
                        error_count++;
            		} else {
            			grid->SetCellBackgroundColour(i, TDcol, REQUIRED_COL);
            		}
            	} else {
            		if ( !GetValue(i, TDcol).IsEmpty() ) {
            			grid->SetCellBackgroundColour(i, TDcol, NOT_VALID_COL);
                        m_valid = false;
                        error_count++;
            		} else {
            			grid->SetCellBackgroundColour(i, TDcol, VALID_COL);
            		}
            	}
            }
#else
            long TDcol = GetDescriptor()->GetElementDescriptorIndex(wxString("Termination Date"));
            long ACcol = GetDescriptor()->GetElementDescriptorIndex(wxString("Action Code"));

            if ( j==TDcol ) {
            	// todo: reaction on input Action Code or Termination Date
            	wxString val = wxString( GetValue( i, ACcol ) ); 
            	if ( val.Cmp(" 1")==0 || val.Cmp(" 7")==0 ) {
            		if ( GetValue(i, TDcol).IsEmpty() ) {
            			grid->SetCellBackgroundColour(i, TDcol, NOT_VALID_COL);
                        m_valid = false;
                        error_count++;
            		} else {
            			grid->SetCellBackgroundColour(i, TDcol, REQUIRED_COL);
            		}
            	} else {
           			grid->SetCellBackgroundColour(i, TDcol, VALID_COL);
            	}
            }

#endif

//------ goto first error
            if ( (error_count == 1) && (first_error_row == -1) && (first_error_col == -1)) {
        		first_error_row = i;
        		first_error_row = j;
        		grid->SetGridCursor(i, j);
        		grid->MakeCellVisible(i, j);
            }
        }
        if ( error_count > MAX_ERRORS ) {
        	break;
        }
    }
   	dialog.Update(maxrows);
    grid->ForceRefresh();
    if ( error_count > MAX_ERRORS ) {
    	wxMessageBox(VALIDATING_DETAIL_TOO_MUCH_ERRORS, 
    					VALIDATING_DETAIL_TOO_MUCH_ERRORS_TITLE, 
    					wxOK | wxCENTRE | wxICON_EXCLAMATION );
    }
    return m_valid;
#else
    return true;
#endif
};


bool DefaultDetail::InsertRows(size_t pos, size_t numRows)
{
    if ( pos >=0 && pos < GetNumberRows() ) {
        wxList* row = CreateRow();
        GetData()->Insert(row, pos);
        if ( GetView() ) {
            wxGridTableMessage msg(this,
                                   wxGRIDTABLE_NOTIFY_ROWS_INSERTED,
                                   pos,
                                   1);

            GetView()->ProcessTableMessage(msg);
        }
        return true;
    } else {
        return AppendRows(1);
    }
};

bool DefaultDetail::AppendRows(size_t numRows)
{
    wxList* row = CreateRow();
    GetData()->Add(row);
    if ( GetView() ) {
        wxGridTableMessage msg(this,
                               wxGRIDTABLE_NOTIFY_ROWS_APPENDED,
                               1);

        GetView()->ProcessTableMessage(msg);
    }
    return true;
};

bool DefaultDetail::DeleteRows(size_t pos, size_t numRows)
{
    if ( pos >=0 && pos < GetNumberRows() ) {
        wxList* row = GetRowData(pos);
        GetData()->RemoveAt(pos);
        DeleteRow(row);

        if ( GetView() ) {
            wxGridTableMessage msg(this,
                                   wxGRIDTABLE_NOTIFY_ROWS_DELETED,
                                   pos,
                                   1);

            GetView()->ProcessTableMessage(msg);
        }
        return true;
    } else {
        return false;
    }
};

void DefaultDetail::Load(wxString& filename)
{
    FileMatrix fm(filename);
//    FileMatrix fm(filename, -1, DEFAULT_LINE_SEPARATOR, COMMA_COLUMN_SEPARATOR );
    if ( fm.GetSize() < 3 ) {
        throw wxString("Invalid data file [" + filename + "] detail section. Must have minimum three lines.");
    }
    Vector& names = fm.GetVector(2);
 	int maxrows = fm.GetSize();
   	int maxcols = names.GetSize();
	if ( maxrows > 3 ) {
    	Vector& values = fm.GetVector(3);
        if ( maxcols != GetDescriptor()->GetSize() ) {
            throw wxString("Invalid data file [" + filename + "] detail section. Data & config mismatch.");
        }
    	if ( maxcols != values.GetSize() ) {
	    throw wxString("Invalid data file [" + filename + "] detail section. Names & values mismatch.");
    	}
    	wxProgressDialog dialog(GRID_OPEN_PROGRESS_TITLE,
                            	GRID_OPEN_PROGRESS_MESSAGE,
                            	maxrows, 
                            	GetView(),
                            	wxPD_AUTO_HIDE |
                            	wxPD_APP_MODAL );
		::wxSetCursor(*wxHOURGLASS_CURSOR);
        for (int i=3; i<maxrows; i++) {
            Vector& values = fm.GetVector(i);
       		wxList* row = new wxList();
            for (int j=0; j<maxcols; j++) {
		wxString* val = new wxString();
		*val = values.GetDataAt(j);
		row->Append((wxObject*)val);
       	    }
       	    GetData()->Add(row);
       	    if ( (i % 10) == 0 ){
       	 	dialog.Update(i);
       	    }
        }
        dialog.Update(maxrows);
	   	::wxSetCursor(*wxHOURGLASS_CURSOR);
        if ( GetView() ) {
        	wxGridTableMessage msg(this,
                               wxGRIDTABLE_NOTIFY_ROWS_APPENDED,
                               maxrows-3);

        	GetView()->ProcessTableMessage(msg);
        }
		::wxSetCursor(*wxSTANDARD_CURSOR);
    }
}

void DefaultDetail::Store(wxString& filename)
{
    wxFile file(filename, wxFile::write);
    wxString names, values;
    Descriptor* descriptor = GetDescriptor();
    int k;

    for (int n=0; n<descriptor->GetSize(); n++) {
                names += descriptor->GetElementDescriptor(n)->GetName();
                names += DEFAULT_COLUMN_SEPARATOR;
    }
    file.Write(names + DEFAULT_LINE_SEPARATOR);
    
    for (int i=0; i<GetNumberRows(); i++) {
        k = 0;
        values = "";
        for (int j=0; j<descriptor->GetSize(); j++) {
            ElementDescriptor* ed = descriptor->GetElementDescriptor(j);
            values += GetValue(i, k++);
            values += DEFAULT_COLUMN_SEPARATOR;
        }
        file.Write(values + DEFAULT_LINE_SEPARATOR);
    }
    file.Close();
}

