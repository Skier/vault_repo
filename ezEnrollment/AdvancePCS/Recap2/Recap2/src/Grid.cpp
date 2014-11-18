#ifndef ___NEW_GRID___
#define ___NEW_GRID___

#include <Grid.h>

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
    } else if ( WXK_TAB == keycode ) {
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

void Grid::CopyToClipboard()
{
	wxString *m_text = new wxString("");

	if (IsSelection())
	{
		for (int i=m_TopSelected; i<=m_BottomSelected; i++){
			for (int j=m_LeftSelected; j<m_RightSelected; j++) {
				*m_text << GetCellValue(i, j);
				*m_text << "\t";
			}
			*m_text << grid->GetCellValue(i, m_RightSelected);
			*m_text << "\r\n";
		}
		wxTextDataObject *m_text_object = new wxTextDataObject(*m_text);

		if (wxTheClipboard->Open())	{
			wxTheClipboard->AddData( m_text_object );
			wxTheClipboard->Close();
		}
	} else {
		if ((GetGridCursorRow() != -1) && (GetGridCursorCol() != -1)) {
			*m_text << GetCellValue(GetGridCursorRow(), (GetGridCursorCol()));
			wxTextDataObject *m_text_object = new wxTextDataObject(*m_text);
			if (wxTheClipboard->Open())	{
				wxTheClipboard->AddData( m_text_object );
				wxTheClipboard->Close();
			}
		}
	}
}

void Grid::PasteFromClipboard()
{
    try {
        ClipboardMatrix m_matrix;
        if ( GetNumberRows() < 1 ) {
            AddRow(event);
            SetGridCursor( 0, 0);
        }
//----- one-to-many pasting
        if ( IsSelection() ) {
            int vertSize = (m_BottomSelected - m_TopSelected + 1);
            int horSize = (m_RightSelected - m_LeftSelected + 1);
            if ( m_matrix.GetSize() == vertSize  && m_matrix.GetVector(0).GetSize() == horSize ) {
                SetGridCursor(m_TopSelected, m_LeftSelected);
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

void Grid::PasteVector(long offset, Vector &v)
{
    for (int i=0; i<v.GetSize(); i++) {
        if ((grid->GetGridCursorCol()+i) < grid->GetNumberCols())
        {
			if ( v.GetDataAt(i).Last() == '\r' ) {
                grid->SetCellValue(grid->GetGridCursorRow()+offset, grid->GetGridCursorCol()+i, v.GetDataAt(i).RemoveLast());
            } else {
                grid->SetCellValue(grid->GetGridCursorRow()+offset, grid->GetGridCursorCol()+i, v.GetDataAt(i));
            }
        }
    }
}

void Grid::PasteMatrix(Matrix &m)
{
    for (int i=0; i<m.GetSize(); i++) {
        if ((grid->GetGridCursorRow()+i) == grid->GetNumberRows())
        {
            grid->AppendRows();
        }
        PasteVector(i, m.GetVector(i));
    }
}


class Grid : public wxGrid {

public:
    Grid(wxWindow* parent, 
    		wxWindowID id, 
    		const wxPoint& pos = wxDefaultPosition, 
    		const wxSize& size = wxDefaultSize, 
    		long style = wxWANTS_CHARS, 
    		const wxString& name = wxPanelNameStr);
    ~Grid();

    void PasteFromClipboard();
    void CopyToClipboard();
    void CutToClipboard();

private:

};

#endif // ___NEW_GRID___

