/*
 *  $RCSfile: DocTable.h,v $
 *
 *  $Revision: 1.4 $
 *
 *  last change: $Date: 2003/10/16 15:11:44 $
 */

#ifndef __ADVPCS_DOC_TABLE_H__
#define __ADVPCS_DOC_TABLE_H__

#pragma warning (disable : 4786)
/* -------------------------- header place ---------------------------------- */
#include <map>
#include <wx/grid.h>
#include <wx/cmdproc.h>
#include <advpcs/Document.h>
#include <advpcs/Descriptor.h>
/* -------------------------- implementation place -------------------------- */
#pragma warning (disable : 4786)

class DocTable : public wxGridTableBase {
public:
    DocTable(Document* doc, wxCommandProcessor& proc)
        :m_doc(NULL), m_colCount(0), m_proc(proc) 
    {
	SetDocument(doc);
    };

    int GetNumberRows() { return GetDocument().GetNumberRows(); };
    int GetNumberCols() { return m_colCount; };
    bool IsEmptyCell( int row, int col ) { 
        return 0 == GetDocument().GetValue(row, Map(col)).Cmp(wxEmptyString); 
    };
    wxString GetValue( int row, int col ) {
        return GetDocument().GetValue(row, Map(col));
    };
    void SetValue( int row, int col, const wxString& value );

    void SetView(wxGrid *grid);

	void SetDocument(Document* doc);

	wxString GetColLabelValue(int col) {
		return GetDocument().GetColumnDescriptor(Map(col)).GetName();

	}

    bool InsertRows( size_t pos = 0, size_t numRows = 1 ) {
       if (GetDocument().InsertRows(pos, numRows) ) {

            if ( GetView() ) {
                wxGridTableMessage msg(this, wxGRIDTABLE_NOTIFY_ROWS_INSERTED, pos, numRows);
                GetView()->ProcessTableMessage(msg);
			}
			return true;
		} else {
			return false;
		}

	};

    bool AppendRows( size_t numRows = 1 ) {
		if ( GetDocument().AppendRows(numRows) ) {
            if ( GetView() ) {
                wxGridTableMessage msg(this, wxGRIDTABLE_NOTIFY_ROWS_APPENDED, numRows);

                GetView()->ProcessTableMessage(msg);
			}

   		    return true;
		} else {
			return false;
		}

	};

    bool DeleteRows( size_t pos = 0, size_t numRows = 1 ) {
		if ( GetDocument().DeleteRows(pos, numRows ) ) {
            if ( GetView() ) {
                wxGridTableMessage msg(this, wxGRIDTABLE_NOTIFY_ROWS_DELETED, pos, numRows);

                GetView()->ProcessTableMessage(msg);
			}
			return true;
		} else {
			return false;
		}
	};

	Document& GetDocument() { return *m_doc; };

	size_t Map(size_t gridColumn) {
		return m_columnMap[gridColumn];
	}

	size_t MapToGrid(size_t docColumn);
	bool Sort(size_t from, size_t to, size_t column, bool ascend);
protected:


    void RefreshView();
private:
    wxCommandProcessor& m_proc;
    std::map<size_t, size_t> m_columnMap;
    Document* m_doc;
    size_t m_colCount;
};
#endif /*  __ADVPCS_DOC_TABLE_H__ */
