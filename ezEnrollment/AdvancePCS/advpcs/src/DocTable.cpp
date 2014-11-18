/*
 *  $RCSfile: DocTable.cpp,v $
 *
 *  $Revision: 1.7 $
 *
 *  last change: $Date: 2003/10/17 16:12:48 $
 */

#include "wx/wxprec.h"

#ifdef __BORLANDC__
    #pragma hdrstop
#endif

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif

#pragma warning (disable : 4786)

/* -------------------------- header place ---------------------------------- */
#include <advpcs/DocTable.h>
#include <advpcs/DateEditor.h>
#include <advpcs/NumericEditor.h>
#include <advpcs/Descriptor.h>
#include <advpcs/Resources.h>
#include <advpcs/Choice.h>
#include <advpcs/CellRenderer.h>
#include <advpcs/CrossFiller.h>
#include <advpcs/cmd/ChangeValue.h>
#include <advpcs/cmd/SortTable.h>
/* -------------------------- implementation place -------------------------- */
#pragma warning (disable : 4786)

void DocTable::SetView(wxGrid *grid) {

    wxGridTableBase::SetView(grid);
    RefreshView();

};


void DocTable::SetDocument(Document* doc) {
    wxASSERT(NULL != doc);

    if ( NULL != m_doc && NULL != GetView()) {
        wxGridTableMessage msg( this, wxGRIDTABLE_NOTIFY_ROWS_DELETED, 0, m_doc->GetNumberRows() );
        GetView()->ProcessTableMessage( msg );
    } 
    m_doc = doc;
    m_colCount = 0;
    for (int i=0; i < GetDocument().GetColCount(); i++) {
        if ( GetDocument().GetColumnDescriptor(i).IsEnabled() ) {
            m_colCount++;
        }
    }


    m_columnMap.clear();

    RefreshView();   
};

void DocTable::RefreshView() {
    int col = 0;
    if ( NULL == GetView() ) {
        return;
    }

    GetView()->BeginBatch();

    for (int i=0; i < GetDocument().GetColCount(); i++) {
        const FieldDescriptor& ed = GetDocument().GetColumnDescriptor(i);

        if ( !ed.IsEnabled() ) {
            continue;
        }

        m_columnMap[col] = i;

        wxGridCellAttr* cellAttr = new wxGridCellAttr();
        cellAttr->SetRenderer(new CellRenderer());
        wxGridCellEditor* editor = NULL;
        wxString param;

        if (  (0 == ed.GetType().CmpNoCase("date"))
            ||(0 == ed.GetType().CmpNoCase("date0")) 
            ||(0 == ed.GetType().CmpNoCase("date9")) 
            ||(0 == ed.GetType().CmpNoCase("date09")) 
            ||(0 == ed.GetType().CmpNoCase("longdate")) 
            ||(0 == ed.GetType().CmpNoCase("longdate0")) 
            ||(0 == ed.GetType().CmpNoCase("longdate9")) 
            ||(0 == ed.GetType().CmpNoCase("longdate09")) 
           ) 
        {
            //wxGridCellTextEditor* text = new wxGridCellTextEditor();
            editor = new GridDateCellEditor();
            param << "10";
        } else if (ed.GetType().CmpNoCase(ADVPCS_EDIT_TYPE_MONEY)==0) {
            GridNumericCellEditor* numericEditor = new GridNumericCellEditor();
            editor = numericEditor;
            param << ed.GetMaxSize();
            numericEditor->SetMaxLength(ed.GetMaxSize());
            cellAttr->SetAlignment(wxALIGN_RIGHT, wxALIGN_CENTER);
        } else if (ed.GetType().CmpNoCase(ADVPCS_EDIT_TYPE_CHOICE)==0) {
            size_t count = ed.GetChoices().GetCount();
            GridChoice* choice = NULL;
            if ( ed.GetCrossFieldName().IsEmpty() ) {
                choice = new GridChoice(new DummyFiller());
            } else {
                choice = new GridChoice(new CrossFiller(*this));
            }
            for (size_t j = 0; j < ed.GetChoices().GetCount(); j++) {
                param << (ed.GetChoices().Item(j));
                param << ",";
            }
            editor = choice;
        } else {
            wxGridCellTextEditor* text = new wxGridCellTextEditor();
            editor = text;
            param << ed.GetMaxSize();
        }
        editor->SetParameters(param);
        cellAttr->SetEditor(editor);

        if ( ed.IsRequired() ) {
            cellAttr->SetBackgroundColour(REQUIRED_COL);
        }

        GetView()->SetColAttr(col, cellAttr);
        wxString label = ed.GetName();
        GetView()->SetColSize(col, ((ed.GetMaxSize() * 7 + 20)>(label.Len() * 6 + 30)) ? (ed.GetMaxSize() * 7 + 20) : (label.Len() * 6 + 30));

    col++;
    }

    GetView()->EndBatch();

    wxGridTableMessage msg( this, wxGRIDTABLE_NOTIFY_ROWS_APPENDED, m_doc->GetNumberRows() );
    GetView()->ProcessTableMessage( msg );

    GetView()->ForceRefresh();
};

size_t DocTable::MapToGrid(size_t docColumn) {
    for ( std::map<size_t, size_t>::iterator i = m_columnMap.begin(); i != m_columnMap.end(); ++i ) {
        if ( (*i).second == docColumn ) {
            return (*i).first;
        }
    }
    wxFAIL;
    return -1;
};

void DocTable::SetValue( int row, int col, const wxString& value ) {
    CmdChangeValue* cmd = new CmdChangeValue(*this, row, col, value);
    m_proc.Submit(cmd);
};

bool DocTable::Sort(size_t from, size_t to, size_t column, bool ascend) {
    CmdSortTable* cmd = new CmdSortTable(*this, from, to, column, ascend);
    m_proc.Submit(cmd);
    return TRUE;
};

