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

EdiDocument::EdiDocument(const Descriptor& headerDesc, const Descriptor& detailDesc, const Descriptor& trailerDesc) 
   : m_headerDesc(headerDesc), m_detailDesc(detailDesc), m_trailerDesc(trailerDesc),
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

bool EdiDocument::IsColRequired(size_t row, size_t col) const {
	
	wxString tranName = GetColumnDescriptor(col).GetTranName();
	wxArrayString tranRequired = GetColumnDescriptor(col).GetTranRequired();
	wxString temp_str;

	if ( tranRequired.IsEmpty() ) {
		return GetColumnDescriptor(col).IsRequired();
	} else {
		for ( size_t i=0; i < tranRequired.Count(); i++ ) {
			if ( GetValue(row, GetColumnIdx(tranName)) == tranRequired[i] ) {
//				wxMessageBox("valu : " + GetValue(row, GetColumnIdx(tranName)) + ";   req : " + tranRequired[i]);
				return true;
			}
		}
		return false;
	}
};

bool EdiDocument::IsFieldValid(size_t index, bool doLog) const {
    wxString result = GetField(index).GetDescriptor().IsValid(GetField(index).GetValue());
    
	if ( result.IsEmpty() 
		&& GetField(index).GetValue().IsEmpty() 
		&& IsFieldRequired(index) ) {
		result = "Value can not be empty.";
	}

	EdiDocument* d = (EdiDocument*)this;

	// later than and early than
	if ( result.IsEmpty() && (!GetField(index).GetDescriptor().GetLaterThan().IsEmpty() || !GetField(index).GetDescriptor().GetEarlyThan().IsEmpty()) ) {
		wxDateTime this_date;
		if ( !GetField(index).GetDescriptor().GetLaterThan().IsEmpty() ) {
			wxDateTime l_date;
			Field& f_later = d->GetFieldByName(GetField(index).GetDescriptor().GetLaterThan());
			if ( l_date.ParseFormat(f_later.GetValue(), "%m/%d/%Y") ) {
				if ( this_date.ParseFormat(GetField(index).GetValue(), "%m/%d/%Y") && !GetField(index).GetDescriptor().GetLaterThan().IsEmpty() ) {
					if ( !this_date.IsLaterThan(l_date) ) {
						result = "Date is not later than " + GetField(index).GetDescriptor().GetLaterThan() + " value [" + l_date.Format("%m/%d/%Y") + "].";
					}
				}
			}
		}
		if ( !GetField(index).GetDescriptor().GetEarlyThan().IsEmpty() ) {
			wxDateTime e_date;
			Field& f_early = d->GetFieldByName(GetField(index).GetDescriptor().GetEarlyThan());
			if ( e_date.ParseFormat(f_early.GetValue(), "%m/%d/%Y") ) {
				if ( this_date.ParseFormat(GetField(index).GetValue(), "%m/%d/%Y") && !GetField(index).GetDescriptor().GetEarlyThan().IsEmpty() ) {
					if ( !this_date.IsEarlierThan(e_date) ) {
						result = "Date is not earlier than " + GetField(index).GetDescriptor().GetEarlyThan() + " value [" + e_date.Format("%m/%d/%Y") + "].";
					}
				}
			}
		}
	}

    // check/then/else
    if ( result.IsEmpty() && !GetField(index).GetDescriptor().GetCheckEmptyName().IsEmpty() ) {
        const FieldDescriptor& fieldDesc = GetField(index).GetDescriptor();
        wxString checkValue = d->GetFieldByName(fieldDesc.GetCheckEmptyName()).GetValue().Trim(false).Trim(true);
        wxString pattern = checkValue.IsEmpty() ? fieldDesc.GetEmptyThenPattern() : fieldDesc.GetEmptyElsePattern();
        if ( !fieldDesc.MatchPattern(GetField(index).GetValue(), pattern) ) {
            if ( pattern.IsEmpty() ) {
                d->GetField(index).SetValue(wxEmptyString);
            } else {
                if ( '!' != pattern[0] ) {
                    d->GetField(index).SetValue(pattern);
                } else {
                    result = "Value cannot be " + pattern.Mid(1);
                }
            }
        }
    }

#if 1
    // check_value/check_value_then/check_value_else
    if ( result.IsEmpty() && !GetField(index).GetDescriptor().GetCheckValueField().IsEmpty() ) {
        const FieldDescriptor& fieldDesc = GetField(index).GetDescriptor();
        wxString checkValue = d->GetFieldByName(fieldDesc.GetCheckValueField()).GetValue().Trim(false).Trim(true);
		wxString pattern = fieldDesc.GetCheckValueThenPattern();
		bool good_value = false;
		for ( int i = 0; i < fieldDesc.GetCheckValueValues().Count(); i++ ) {
			if ( 0 == checkValue.Cmp(fieldDesc.GetCheckValueValues().Item(i)) ) {
				good_value = true;
			}
		}
        if ( good_value && !fieldDesc.MatchPattern(GetField(index).GetValue(), pattern) ) {
            if ( pattern.IsEmpty() ) {
                d->GetField(index).SetValue(wxEmptyString);
            } else {
                if ( '!' != pattern[0] ) {
                    d->GetField(index).SetValue(pattern);
                } else {
                    result = "Value cannot be " + pattern.Mid(1);
                }
            }
        }
    }
#endif
	
	if ( 0 != result.Cmp(wxEmptyString) && doLog ) {
            LOG_ERROR(wxGetApp().GetLogger(), 0, wxString::Format(" Value for field '%s' invalid. '%s'", GetField(index).GetDescriptor().GetName(), result));
    }
    return 0 == result.Cmp(wxEmptyString);
};

bool EdiDocument::IsFieldRequired(size_t index) const {
	
	wxString tranName = GetField(index).GetDescriptor().GetTranName();
	wxArrayString tranRequired = GetField(index).GetDescriptor().GetTranRequired();
	wxString temp_str;

	if ( tranRequired.IsEmpty() ) {
		return GetField(index).GetDescriptor().IsRequired();
	} else {
		for ( size_t i=0; i < tranRequired.Count(); i++ ) {
			if ( GetField(index).GetValue() == tranRequired[i] ) {
//				wxMessageBox("valu : " + GetValue(row, GetColumnIdx(tranName)) + ";   req : " + tranRequired[i]);
				return true;
			}
		}
		return false;
	}
};

bool EdiDocument::IsValidValue(size_t row, size_t col, bool doLog ) const {
        
    wxString result = GetColumnDescriptor(col).IsValid(GetValue(row, col));

	if ( result.IsEmpty() 
		&& GetValue(row, col).IsEmpty() 
		&& IsColRequired(row, col) ) {
		result = "Value can not be empty.";
	}

    if ( 0 == result.Cmp(wxEmptyString ) 
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
		if ( !GetColumnDescriptor(col).GetLaterThan().IsEmpty() ) {
			wxDateTime l_date;
			size_t later_idx = GetColumnIdx(GetColumnDescriptor(col).GetLaterThan());
			if ( l_date.ParseFormat(GetValue(row, later_idx), "%m/%d/%Y") ) {
				if ( this_date.ParseFormat(GetValue(row, col), "%m/%d/%Y") && !GetColumnDescriptor(col).GetLaterThan().IsEmpty() ) {
					if ( !this_date.IsLaterThan(l_date) ) {
						result = "Date is not later than " + GetColumnDescriptor(col).GetLaterThan() + " value [" + l_date.Format("%m/%d/%Y") + "].";
					}
				}
			}
		}
		if ( !GetColumnDescriptor(col).GetEarlyThan().IsEmpty() ) {
			wxDateTime e_date;
			size_t early_idx = GetColumnIdx(GetColumnDescriptor(col).GetEarlyThan());
			if ( e_date.ParseFormat(GetValue(row, early_idx), "%m/%d/%Y") ) {
				if ( this_date.ParseFormat(GetValue(row, col), "%m/%d/%Y") && !GetColumnDescriptor(col).GetEarlyThan().IsEmpty() ) {
					if ( !this_date.IsEarlierThan(e_date) ) {
						result = "Date is not earlier than " + GetColumnDescriptor(col).GetEarlyThan() + " value [" + e_date.Format("%m/%d/%Y") + "].";
					}
				}
			}
		}
	}


    // check/then/else
    if ( result.IsEmpty() && !GetColumnDescriptor(col).GetCheckEmptyName().IsEmpty() ) {
        const FieldDescriptor& colDesc = GetColumnDescriptor(col);
        wxString checkValue = GetValue(row, GetColumnIdx(
            colDesc.GetCheckEmptyName())).Trim(false).Trim(true);
        wxString pattern = checkValue.IsEmpty() ? colDesc.GetEmptyThenPattern() : colDesc.GetEmptyElsePattern();
        if ( !colDesc.MatchPattern(GetValue(row, col), pattern) ) {
			EdiDocument* d = (EdiDocument*)this;
            if ( pattern.IsEmpty() ) {
                d->SetValue(row, col, wxEmptyString);
            } else {
                if ( '!' != pattern[0] ) {
                    d->SetValue(row, col, pattern);
                } else {
                    result = "Value cannot be " + pattern.Mid(1);
                }
            }
        }
    }

    // check_value/check_value_then/check_value_else
    if ( result.IsEmpty() && !GetColumnDescriptor(col).GetCheckValueField().IsEmpty() ) {
        const FieldDescriptor& colDesc = GetColumnDescriptor(col);
        wxString checkValue;
		EdiDocument* d = (EdiDocument*)this;
		if ( 0 == colDesc.GetCheckValueField().SubString(0,6).CmpNoCase("header:") ) {
			checkValue = d->GetFieldByName(colDesc.GetCheckValueField().Mid(7)).GetValue().Trim(false).Trim(true);
		} else {
			checkValue = GetValue(row, GetColumnIdx(colDesc.GetCheckValueField())).Trim(false).Trim(true);
		}
		wxString pattern = colDesc.GetCheckValueThenPattern();
		bool good_value = false;
		for ( int i = 0; i < colDesc.GetCheckValueValues().Count(); i++ ) {
			if ( 0 == checkValue.Cmp(colDesc.GetCheckValueValues().Item(i)) ) {
				good_value = true;
			}
		}
        if ( good_value 
					&& ('>' != pattern[0]) 
					&& ('<' != pattern[0]) ) {
			if ( !colDesc.MatchPattern(GetValue(row, col), pattern) ) {
				if ( pattern.IsEmpty() ) {
					d->SetValue(row, col, wxEmptyString);
				} else {
					if ( '!' != pattern[0] ) {
						d->SetValue(row, col, pattern);
					} else {
						result = "Value cannot be " + pattern.Mid(1);
					}
				}
			}
        } else if ( good_value 
					&& (('>' == pattern[0]) || ('<' == pattern[0]) ) 
					&& ("numeric" == colDesc.GetType()) ) {
			long val, pat;
			val = 0; pat = 0;
			if ( GetValue(row, col).IsEmpty() ) {
				d->SetValue(row, col, "0");
			}
			if ( pattern.Mid(1).ToLong(&pat) && GetValue(row, col).ToLong(&val) ) {
				if ( val <= pat && ('>' == pattern[0]) ) {
					result = "Value must be greater than " + pattern.Mid(1);
				} else if ( val >= pat && ('<' == pattern[0]) ) {
					result = "Value must be less than " + pattern.Mid(1);
				}
			} else {
					wxMessageBox(ADVPCS_DESC_WRONG_XML + " [" + colDesc.GetName() + "]" );
			        THROW_CFG_EXCEPTION(ADVPCS_DESC_WRONG_XML);
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

//trailer place

const Descriptor& EdiDocument::GetTrailerDescriptor() const { 
	return m_trailerDesc;
};

const FieldDescriptor& EdiDocument::GetTrailerFieldDescriptor(size_t index) const {
	return m_trailerDesc.GetFieldDescriptor(index);
};

wxString EdiDocument::GetTrailerValue(size_t index) const {
	wxString value;
	value = "";
	const FieldDescriptor& fd = GetTrailerFieldDescriptor(index);
	if ( 0 == fd.GetType().Cmp("row_counter") ) {
		value << GetNumberRows();
	} else if ( 0 == fd.GetName().SubString(0,6).Cmp("header:") ) {
		EdiDocument* d = (EdiDocument*)this;
		value << d->GetFieldByName(fd.GetName().Mid(7)).GetValue();
	} else if ( 0 == fd.GetType().Cmp("file_counter") ) {
		value << wxGetApp().EdiFileCounter();
	} else {
		value << fd.GetDefaultValue();
	}

	return value;
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


wxString EdiDocument::GetColHint(size_t row, size_t col) const {
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
    if ( IsColRequired(row, col) ) {
        result << " | required ";
    }
    return result;
};

void EdiDocument::CreateRow(StringVector& v) const {
    for (size_t i = 0; i < GetColCount(); i++) {
        v.push_back(GetColumnDescriptor(i).GetDefaultValue());
    }
};
