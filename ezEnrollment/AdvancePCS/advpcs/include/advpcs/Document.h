/*
 *  $RCSfile: Document.h,v $
 *
 *  $Revision: 1.5 $
 *
 *  last change: $Date: 2003/10/17 16:12:48 $
 */

#ifndef __ADVPCS_DOCUMENT_H__
#define __ADVPCS_DOCUMENT_H__

class FieldDescriptor;
class Descriptor;
class wxGridTableBase; 

class Field {
public:

    virtual const FieldDescriptor& GetDescriptor() const = 0;
    virtual wxString GetValue() const = 0;
    virtual void SetValue(const wxString& valu) = 0;
};

class Document {
public:
    Document(){};
    virtual ~Document(){};
    virtual bool IsValid(bool doLog = false) const = 0;

        // header place
    virtual bool IsHeaderValid(bool doLog = false) const = 0;
    virtual bool IsFieldValid(size_t index, bool doLog = false) const = 0;
		;
    virtual const Descriptor& GetDescriptor() const = 0;
    virtual size_t GetFieldCount() const = 0;
    virtual Field& GetField(size_t index) = 0;
    virtual Field& GetFieldByName(const wxString& name) = 0;
    virtual const Field& GetField(size_t index) const = 0;
    virtual void Clear() = 0;

        // body place
    virtual const Descriptor& GetColumnsDescriptor() const = 0;
    virtual const FieldDescriptor& GetColumnDescriptor(size_t col) const = 0;
    virtual const FieldDescriptor& GetColumnDescriptor(const wxString& name) const = 0;
    virtual const int GetColumnIdx(const wxString& name) const = 0;
    virtual size_t GetColCount() const = 0;

    virtual bool IsValidValue(size_t row, size_t col, bool doLog = false) const = 0;
    virtual void SetValue(size_t row, size_t col, const wxString& value) = 0;
    virtual wxString GetValue(size_t row, size_t col) const = 0;
    virtual size_t GetNumberRows() const = 0;
    virtual wxString GetColHint(size_t col) const = 0;

    virtual bool InsertRows(size_t pos = 0, size_t numRows = 1) = 0;
    virtual bool AppendRows(size_t numRows) = 0;
    virtual bool DeleteRows(size_t pos, size_t numRows) = 0;
    virtual bool SwapRows(size_t row1, size_t row2) = 0;
//	virtual void QuickSort(size_t from, size_t to, size_t column, bool ascend = true) = 0;

    virtual bool IsChanged() const = 0;
    virtual void SetChanged(bool changed) = 0;

    virtual wxString GetFileName() const = 0;
    virtual void SetFileName(const wxString& value) = 0;
};


#endif /* __ADVPCS_DOCUMENT_H__ */

