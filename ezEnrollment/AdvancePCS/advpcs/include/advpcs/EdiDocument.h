/*
 *  $RCSfile: EdiDocument.h,v $
 *
 *  $Revision: 1.5 $
 *
 *  last change: $Date: 2003/10/17 16:12:48 $
 */

#ifndef __ADVPCS_EDI_DOCUMENT_H__
#define __ADVPCS_EDI_DOCUMENT_H__

/* -------------------------- header place ---------------------------------- */
#include <advpcs/Document.h>
#include <advpcs/Descriptor.h>
#include <advpcs/EdiField.h>
#include <vector>
/* -------------------------- implementation place -------------------------- */
class Descriptor;

typedef std::vector<wxString> StringVector;
typedef std::vector<StringVector> StringMatrix;

class EdiDocument : public Document {
    friend class EdiField;
    friend class DocManager;
public:
    EdiDocument(const Descriptor& headerDesc, const Descriptor& detailDesc);
    bool IsValid(bool doLog = false) const;

        // header place
    bool IsHeaderValid( bool doLog = false) const;
    bool IsFieldValid(size_t index, bool doLog = false) const;

    const Descriptor& GetDescriptor() const { return m_headerDesc; };
    size_t GetFieldCount() const { return m_headerDesc.GetSize(); };
    Field& GetField(size_t index);
    Field& GetFieldByName(const wxString& name);
    const Field& GetField(size_t index) const;

    void Clear();

        // body place
    const Descriptor& GetColumnsDescriptor() const { return m_detailDesc; };
    const FieldDescriptor& GetColumnDescriptor(size_t col) const { return m_detailDesc.GetFieldDescriptor(col); };
    size_t GetColCount() const { return m_detailDesc.GetSize(); };
    const FieldDescriptor& GetColumnDescriptor(const wxString& name) const;
    const int GetColumnIdx(const wxString& name) const;

    bool IsValidValue(size_t row, size_t col, bool doLog = false) const;
    void SetValue(size_t row, size_t col, const wxString& value);
    wxString GetValue(size_t row, size_t col) const;

    bool InsertRows(size_t pos = 0, size_t numRows = 1);
    bool AppendRows(size_t numRows);
    bool DeleteRows(size_t pos, size_t numRows);
    bool SwapRows(size_t row1, size_t row2);
//	void QuickSort(size_t from, size_t to, size_t column, bool ascend = true);

    size_t GetNumberRows() const;
    wxString GetColHint(size_t col) const;

    bool IsChanged() const { return m_changed; };

    virtual void SetChanged(bool changed) { m_changed = changed; };

    wxString GetFileName() const { return m_fileName; };

    void SetFileName(const wxString& value) {m_fileName = value;};

private:
    void CreateRow(StringVector& v) const ;

private:
    const Descriptor& m_headerDesc;
    const Descriptor& m_detailDesc;

    wxString m_fileName;
    bool     m_changed;

    StringMatrix m_data;
    StringVector m_header;
    std::vector<EdiField> m_fields;
};

#endif /* __ADVPCS_EDI_DOCUMENT_H__ */
