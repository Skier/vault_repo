/*
 *  $RCSfile: EdiDocument.cpp,v $
 *
 *  $Revision: 1.7 $
 *
 *  last change: $Date: 2003/11/14 07:27:14 $
 */

#include "wx/wxprec.h"

#ifdef __BORLANDC__
    #pragma hdrstop
#endif

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif

/* -------------------------- header place ---------------------------------- */
#include <wx/datetime.h>
#include <atf/Logger.h>
#include <advpcs/EdiDocument.h>
#include <advpcs/Resources.h>
#include <advpcs/ComposerException.h>
#include <advpcs/App.h>
/* -------------------------- implementation place -------------------------- */

EdiDocument::EdiDocument(const Descriptor& headerDesc, const Descriptor& detailDesc) 
   : m_headerDesc(headerDesc), m_detailDesc(detailDesc), 
     m_fileName(wxEmptyString), m_changed(false), m_data()
{
    for ( size_t i = 0; i < m_headerDesc.GetSize(); i++) {
        m_fields.push_back(EdiField(i, this));
        m_header.push_back(m_headerDesc.GetFieldDescriptor(i).GetDefaultValue());
    }

};

bool EdiDocument::IsValid(bool doLog) const {
    bool result = IsHeaderValid(doLog); 
    if (result || doLog) {
        for ( size_t row = 0; row < GetNumberRows(); row++ ) {
            for ( size_t col = 0; col < GetColCount(); col++ ) {
                if ( !IsValidValue(row, col, doLog) ) {
                    if ( !doLog )  {
                      return false;
                    } else {
                      result = false;
                    }
                } 
            }
        }
    }
    return result;
};

bool EdiDocument::IsHeaderValid(bool doLog) const {
    bool result = true;
    for ( size_t index = 0; index < GetFieldCount(); index++ ) {
        if ( !IsFieldValid(index, doLog) ) {
            if ( !doLog ) {
                return false;
            } else {
                result = false;
            }
        }
    }
    return result;
};

bool EdiDocument::IsFieldValid(size_t index, bool doLog) const {
    wxString result = GetField(index).GetDescriptor().IsValid(GetField(index).GetValue());
    if ( 0 != result.Cmp(wxEmptyString) && doLog ) {
            LOG_ERROR(wxGetApp().GetLogger(), 0, wxString::Format(" Value for field '%s' invalid. '%s'", GetField(index).GetDescriptor().GetName(), result));
    }
    return 0 == result.Cmp(wxEmptyString);
};

bool EdiDocument::IsValidValue(size_t row, size_t col, bool doLog ) const {
        
    wxString result = GetColumnDescriptor(col).IsValid(GetValue(row, col));

    if ( 0 == result.Cmp(wxEmptyString) 
        && GetColumnDescriptor(col).GetType().Cmp("choice") == 0
        && GetColumnDescriptor(col).GetCrossFieldName().Cmp(wxEmptyString) != 0) 
    {
        const FieldDescriptor& desc = GetColumnDescriptor(col);
        int crossIdx = GetColumnIdx(desc.GetCrossFieldName());
        const FieldDescriptor& crossDesc = GetColumnDescriptor(crossIdx);
        long crossId = 0;
        wxString crossValue = GetValue(row, GetColumnIdx(desc.GetCrossFieldName()));
        for ( int i=0; i<crossDesc.GetChoicesDesk().size(); i++ ) {
            if ( crossValue == crossDesc.PrepareValue(crossDesc.GetChoicesDesk()[i].choice) ) {
                crossId = crossDesc.GetChoicesDesk()[i].id;
                break;
            }
        }

        result = "Must be one of: ";
        for ( i=0; i<desc.GetChoicesDesk().size(); i++ ) {
             const IdList& ids = desc.GetChoicesDesk()[i].ids;
             for ( int j=0; j<ids.size(); j++ ) {
                 if ( crossId == ids[j] ) {
                     result += "'"+desc.GetChoicesDesk()[i].choice+"' ";
                     if ( GetValue(row, col) == desc.PrepareValue(desc.GetChoicesDesk()[i].choice) ) {
                         result = wxEmptyString;
                         goto found;
                     }
                 }
             }
        }

found:;
    }

	// later than and early than
	if ( result.IsEmpty() && (!GetColumnDescriptor(col).GetLaterThan().IsEmpty() || !GetColumnDescriptor(col).GetEarlyThan().IsEmpty()) ) {
		wxDateTime this_date;
		wxDateTime l_date;
		if ( l_date.ParseFormat(GetValue(row, GetColumnIdx(GetColumnDescriptor(col).GetLaterThan())), "%m/%d/%Y") ) {
			if ( this_date.ParseFormat(GetValue(row, col), "%m/%d/%Y") && !GetColumnDescriptor(col).GetLaterThan().IsEmpty() ) {
				if ( !this_date.IsLaterThan(l_date) ) {
					result = "Date is not later than " + GetColumnDescriptor(col).GetLaterThan() + " value [" + l_date.Format("%m/%d/%Y") + "].";
				}
			}
		}
		wxDateTime e_date;
		if ( e_date.ParseFormat(GetValue(row, GetColumnIdx(GetColumnDescriptor(col).GetEarlyThan())), "%m/%d/%Y") ) {
			if ( this_date.ParseFormat(GetValue(row, col), "%m/%d/%Y") && !GetColumnDescriptor(col).GetEarlyThan().IsEmpty() ) {
				if ( !this_date.IsEarlierThan(e_date) ) {
					result = "Date is not earlier than " + GetColumnDescriptor(col).GetEarlyThan() + " value [" + e_date.Format("%m/%d/%Y") + "].";
				}
			}
		}
	}


    // check/then/else
    if ( result.IsEmpty() && !GetColumnDescriptor(col).GetCheckEmptyName().IsEmpty() ) {
        const FieldDescriptor& colDesc = GetColumnDescriptor(col);
        wxString checkValue = GetValue(row, GetColumnIdx(
            colDesc.GetCheckEmptyName())).Trim(false).Trim(true);

		bool isPatternExists = true;

		if ( checkValue.IsEmpty() && !colDesc.HasEmptyThenPattern() ) {
			isPatternExists = false;
		} else if ( !checkValue.IsEmpty() && !colDesc.HasEmptyElsePattern() ) {
			isPatternExists = false;
		}
        
		wxString pattern = checkValue.IsEmpty() ? colDesc.GetEmptyThenPattern() : colDesc.GetEmptyElsePattern();
        
		if ( isPatternExists && !colDesc.MatchPattern(GetValue(row, col), pattern) ) {
            if ( pattern.IsEmpty() ) {
                result = "Value has to be empty";
            } else {
                if ( '!' != pattern[0] ) {
                    result =  "Value has to be " + pattern;
                } else {
                    result = "Value cannot be " + pattern.Mid(1);
                }
            }
        }
    }

    if ( 0 != result.Cmp(wxEmptyString) && doLog ) {
        unsigned short ncol = col;
        unsigned short nrow = row;
        ATF_ERROR code = row;
        code <<= 16;
        code |= col;
        LOG_ERROR(wxGetApp().GetLogger(), code, wxString::Format(".Value for row %ld, column %s is invalid. '%s'", row+1, GetColumnDescriptor(col).GetName(), result));
    }
    return 0 == result.Cmp(wxEmptyString);
};


Field& EdiDocument::GetField(size_t index) {
    wxASSERT(0 <= index);
    wxASSERT(m_fields.size() > index);
    return m_fields[index];
};

const Field& EdiDocument::GetField(size_t index) const {
    EdiDocument* d = (EdiDocument*)this;
    return d->GetField(index);
};

Field& EdiDocument::GetFieldByName(const wxString& name) {
    for( size_t i=0; i < GetFieldCount(); i++ ) {
        Field& f = GetField(i);
        if ( 0 == name.CmpNoCase(f.GetDescriptor().GetName()) ) {
            return f;
        }
    }
    THROW_COMPOSER_EXCEPTION(wxString::Format(ADVPCS_UNKNOWN_FIELD_ERR_MSG, name));
};


void EdiDocument::Clear() {
    // FIXME: clear header
    m_data.clear();
};

const FieldDescriptor& EdiDocument::GetColumnDescriptor(const wxString& name) const {
    return GetColumnDescriptor(GetColumnIdx(name));
};

const int EdiDocument::GetColumnIdx(const wxString& name) const {
    for (int i = 0; i<GetColCount(); i++) {
        if ( 0 == GetColumnDescriptor(i).GetName().CmpNoCase(name) ) {
            return i;
        }
    }
    THROW_COMPOSER_EXCEPTION(wxString::Format(ADVPCS_UNKNOWN_FIELD_NAME_ERR_MSG, name));
};

void EdiDocument::SetValue(size_t row, size_t col, const wxString& value) {
    wxASSERT(0 <= row);
    wxASSERT(0 <= col);
    wxASSERT(row < m_data.size());
    wxASSERT(col < m_data[row].size());

    if (0 == GetColumnDescriptor(col).GetType().CmpNoCase("text")) {
        m_data[row][col] = GetColumnDescriptor(col).PrepareValue(value.Upper().Trim());
    } else {
        m_data[row][col] = GetColumnDescriptor(col).PrepareValue(value);
    }

//dependences  ---------        
    const FieldDependence* dep = GetColumnDescriptor(col).GetDependence();
    if ( NULL != dep ) {
        int idx;
        for ( int i=0; i<GetColumnDescriptor(col).GetDependences().size(); i++ ) {
            idx = GetColumnIdx( GetColumnDescriptor(col).GetDependences()[i].field );
            if ( idx >= 0 && idx < m_data[row].size() ) {
                m_data[row][idx] = GetColumnDescriptor(col).GetDependenceValue(m_data[row][col], i);
            }
        }
    }
    m_changed = true;
};

wxString EdiDocument::GetValue(size_t row, size_t col) const {
    wxASSERT(0 <= row);
    wxASSERT(0 <= col);
    wxASSERT(row < m_data.size());
    wxASSERT(col < m_data[row].size());

    return m_data[row][col];
};

bool EdiDocument::InsertRows(size_t pos, size_t numRows) {
    if ( pos >=0 && pos < GetNumberRows() ) {
        StringMatrix::iterator i = m_data.begin();
        i += pos;
        StringVector row;
        CreateRow(row);

        m_data.insert(i, row);
        m_changed = true;

        return true;
    } else {
        return AppendRows(1);
    }
};

bool EdiDocument::AppendRows(size_t numRows) {
    for ( size_t i = 0; i < numRows; i++ ) {
        StringVector row;
        CreateRow(row);
        m_data.push_back(row);
        m_changed = true;
        return true; 
    }
    return false;
};

bool EdiDocument::DeleteRows(size_t pos, size_t numRows) {
    wxASSERT(0 <= pos);
    wxASSERT(pos < m_data.size());
    wxASSERT(0 < numRows);
    wxASSERT(pos + numRows <= m_data.size());

    if ( pos >=0 && pos < GetNumberRows() ) {
        StringMatrix::iterator first = m_data.begin();
        first += pos;
        StringMatrix::iterator last = first;
        last += numRows;

        m_data.erase(first, last);
        m_changed = true;
 
        return true;
    } else {
        return false;
    }
};

bool EdiDocument::SwapRows(size_t row1, size_t row2) {
    if ( (row1 != row2) && (row1 < m_data.size()) && (row2 < m_data.size()) ) {
        m_data[row1].swap(m_data[row2]);
        return true;
    } else {
        return false;
    }
};

size_t EdiDocument::GetNumberRows() const {
    return m_data.size();
};


wxString EdiDocument::GetColHint(size_t col) const {
    const FieldDescriptor& ed = GetColumnDescriptor(col);
    wxString result = wxEmptyString;

    result << ed.GetName();
    result << wxT(" | type:");
    result << ed.GetType();

    if ( 0 == (ed.GetType().Cmp("date")) 
        || (0 == ed.GetType().Cmp("date0")) 
        || (0 == ed.GetType().Cmp("date9")) 
        || (0 == ed.GetType().Cmp("date09")) 
        || (0 == ed.GetType().Cmp("longdate")) 
        || (0 == ed.GetType().Cmp("longdate0")) 
        || (0 == ed.GetType().Cmp("longdate9")) 
        || (0 == ed.GetType().Cmp("longdate09")) 
       ) 
    {
        result << wxT(" | format: mm/dd/yyyy");
    } else {
        result << " | max length:";
        result << ed.GetMaxSize();
        result << " | min length:";
        result << ed.GetMinSize();
    }
    if ( ed.IsRequired() ) {
        result << " | required ";
    }
    return result;
};

void EdiDocument::CreateRow(StringVector& v) const {
    for (size_t i = 0; i < GetColCount(); i++) {
        v.push_back(GetColumnDescriptor(i).GetDefaultValue());
    }
};
