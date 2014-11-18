#ifndef __RXCLAIM_DETAIL_H__
#define __RXCLAIM_DETAIL_H__

#include <wx/grid.h>
#include <wx/datetime.h>
#include <rxclaim/XmlDescriptor.h>


class Detail {
public:
    virtual Descriptor* GetDescriptor() = 0;
    virtual ElementDescriptor* GetColumnDescriptor(int col) = 0;
    virtual wxGridTableBase* GetTable() = 0;
    virtual bool IsValid() = 0;

    virtual void SetValue(int row, int col, const wxString& value) = 0;
    virtual wxString GetValue(int row, int col) = 0;
    virtual bool AppendRows(size_t numRows) = 0;
    virtual bool DeleteRows(size_t pos, size_t numRows) = 0;
    virtual int GetNumberRows() = 0;
    virtual int GetNumberCols() = 0;

    virtual void Load(wxString& filename) = 0;
    virtual void Store(wxString& filename) = 0;
    virtual wxString& GetColHint(int col) = 0;

    virtual long GetCount() = 0;
};

//
// ListArray
// 
WX_DEFINE_ARRAY(wxList*, ListArray);

//
// Defaults
//
class DefaultDetail : public Detail, public wxGridTableBase {
public:
    DefaultDetail(Descriptor* descriptor);
    ~DefaultDetail();

    Descriptor* GetDescriptor()
    {
        return m_descriptor;
    }

    ElementDescriptor* GetColumnDescriptor(int col)
    {
        return (ElementDescriptor*)m_columns->Item(col)->GetData();
    };

    wxGridTableBase* GetTable()
    {
        return this;
    }

    bool IsValid();

    void Load(wxString& filename);
    void Store(wxString& filename);

    wxString& GetColHint(int col)
    {
        ElementDescriptor* ed = GetColumnDescriptor(col);
        wxString* m_string = new wxString("");
        *m_string << ed->GetName();
        *m_string << " | type:";
        *m_string << ed->GetType();
        if ((ed->GetType().Cmp("date") == 0) || (ed->GetType().Cmp("longdate") == 0)) {
                *m_string << " | format: mm/dd/yyyy";
        } else {
                *m_string << " | max length:";
                *m_string << ed->GetMaxSize();
                *m_string << " | min length:";
                *m_string << ed->GetMinSize();
        };
        if(ed->IsRequired()) {
                *m_string << " | required ";
        }
        return *m_string;
    };

    // inheritence from wxGridTable
    int GetNumberRows()
    {
        return GetData()->GetCount();
    };

    int GetNumberCols()
    {
        return GetColumns()->GetCount();
    };

    wxString GetColLabelValue(int col)
    {
        return GetColumnDescriptor(col)->GetShortName();
    };

    wxString GetValue(int row, int col)
    {
        wxList* rowd = GetRowData(row);
        wxString* value = (wxString*)rowd->Item(col)->GetData();
        get_count++;
        return *value;
    };

    void SetValue(int row, int col, const wxString& value)
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
                 && GetDescriptor()->GetElementDescriptor(depIndex)->IsEnabled() 
                 && GetValue(row, MapIndex(depIndex)).IsEmpty()) {
                SetValue(row, MapIndex(depIndex), colDesc->DependenceValue(wxString(value)));
            }
        }
    };

    bool IsEmptyCell(int row, int col)
    {
        return false;
    };

    void Clean()
    {
        for (int i=0; i<GetNumberRows(); i++) {
            wxList* row = GetRowData(i);
            GetData()->RemoveAt(i);
            DeleteRow(row);
        }
    };

    void SetView(wxGrid* grid);

    long GetCount()
    {
        return get_count;
    }
    
    bool InsertRows(size_t pos = 0, size_t numRows = 1);
    bool AppendRows(size_t numRows = 1);
    bool DeleteRows(size_t pos = 0, size_t numRows = 1);
        
private:    
    ListArray* m_data;
    wxList* m_columns;
    wxGridCellAttr *m_cellAttr;
    wxGridCellEditor *m_sizedEditor;
    Descriptor* m_descriptor;

    long get_count;
    
    ListArray* GetData()
    {
        return m_data;
    };

    wxList* GetColumns()
    {
        return m_columns;
    };

    wxList* GetRowData(int row) 
    {
        return (wxList*)GetData()->Item(row);
    };

    wxList* CreateRow()
    {
        wxList* row = new wxList();
        for (int i=0; i<GetNumberCols(); i++) {
            ElementDescriptor* ed = GetColumnDescriptor(i);    
            wxString* value = new wxString(ed->GetDefaultValue());
            row->Append((wxObject*)value);
        }
        return row;
    };

    void DeleteRow(wxList* row)
    {
        for (size_t i=0; i<row->GetCount(); i++) {
            wxString* data = (wxString*)row->Item(i)->GetData();
            delete data;
        }
        delete row;
    };

    void Prepare()
    {
        for (size_t i=0; i<GetDescriptor()->GetSize(); i++) {
            ElementDescriptor* ed = GetDescriptor()->GetElementDescriptor(i);
            if ( ed->IsEnabled() ) {
                m_columns->Append((wxObject*)ed);
            }
        }
    };
    
    int MapIndex(size_t absoluteIndex)
    {
        for (size_t i=0, k=0; i<GetDescriptor()->GetSize(); i++) {
            ElementDescriptor* ed = GetDescriptor()->GetElementDescriptor(i);
            if ( ed->IsEnabled() ) {
                if ( i == absoluteIndex ) {
                    return k;
                } else {
                    k++;
                }
            }
        }
        throw wxString("Invalid absolute index.");
    };
    
};

#endif // __RXCLAIM_DETAIL_H__
