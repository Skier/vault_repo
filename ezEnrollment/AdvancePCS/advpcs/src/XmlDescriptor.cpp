/*
 *  $RCSfile: XmlDescriptor.cpp,v $
 *
 *  $Revision: 1.6 $
 *
 *  last change: $Date: 2004/04/19 14:47:13 $
 */

#include "wx/wxprec.h"

#ifdef __BORLANDC__
    #pragma hdrstop
#endif

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif

/* -------------------------- header place ---------------------------------- */
#include <wx/tokenzr.h>
#include <wx/datetime.h>
#include <wx/regex.h>
#include <atf/CfgException.h>
#include <advpcs/XmlDescriptor.h>
#include <advpcs/Resources.h>
#include <advpcs/App.h>

/* -------------------------- implementation place -------------------------- */

static wxString NAME_ATTR(wxT(       "name"       ));
static wxString SHORT_NAME_ATTR(wxT( "short_name" ));
static wxString TYPE_ATTR(wxT(       "type"       ));
static wxString LENGHT_ATTR(wxT(     "lenght"   ));
static wxString MAX_SIZE_ATTR(wxT(   "max_size"   ));
static wxString MIN_SIZE_ATTR(wxT(   "min_size"   ));
static wxString REQUIRED_ATTR(wxT(   "required"   ));
static wxString ENABLED_ATTR(wxT(    "enabled"    ));
static wxString DEFAULT_ATTR(wxT(    "default"    ));
static wxString ALIGN_ATTR(wxT(      "align"      ));

static wxString CHECK_EMPTY_ATTR(wxT("check_empty"));
static wxString EMPTY_THEN_ATTR(wxT("empty_then"));
static wxString EMPTY_ELSE_ATTR(wxT("empty_else"));
static wxString ZERO_DATE_PATTERN(wxT("00/00/0000"));

static wxString SEPARATOR_ATTR(wxT(  "separator"  ));
static wxString VALUE_ATTR(wxT(      "value"      ));

static wxString PATTERN_ATTR(wxT(    "pattern"    ));

static wxString START_DATE_ATTR(wxT( "start_date" ));
static wxString END_DATE_ATTR(wxT(   "end_date"   ));

static wxString DEPENDENCE(wxT(      "dependence" ));
static wxString FIELD_ATTR(wxT(      "field"      ));
static wxString BPOS_ATTR(wxT(       "bpos"       ));
static wxString EPOS_ATTR(wxT(       "epos"       ));
static wxString CROSS_FIELD(wxT(     "cross-field"));

static wxString LATER_THAN_ATTR (wxT("later_than" ));
static wxString EARLY_THAN_ATTR (wxT("early_than" ));

// type of variant expr
static wxString CHOICE(wxT(            "choice"     ));
static wxString CHOICE_ID(wxT(          "id"        ));
static wxString CROSS_IDS(wxT(       "cross-ids"    ));
static wxString DEFAULT_SEPARATOR(wxT( ""           ));


wxString XmlFieldDependence::GetDependenceValue(const wxString& candidate) const {
    if ( GetBeginPosition() >= candidate.Length() ) {
        return wxEmptyString;
    } 
    if ( GetEndPosition() >= candidate.Length() ) {
        return candidate.Mid(GetBeginPosition());
    } 
    if ( GetBeginPosition() > GetEndPosition() ) {
        return candidate.Mid(GetEndPosition(),
                             GetBeginPosition() - GetEndPosition() + 1);
    } else {
        return candidate.Mid(GetBeginPosition(),
                             GetEndPosition() - GetBeginPosition() + 1);
    }

};


XmlDescriptor::XmlDescriptor(CXmlNode& node)
{
    m_root = &node;
    m_children = new wxList();
    parseDocument();
};

XmlDescriptor::~XmlDescriptor() {
    for (unsigned int i=0; i<GetChildren()->GetCount(); i++) {
        FieldDescriptor* d = IGetFieldDescriptor(i);
        delete d;
    }
    delete m_children;
};
    
size_t XmlDescriptor::GetSize() const {
    return GetChildren()->GetCount();
}

const FieldDescriptor& XmlDescriptor::GetFieldDescriptor(size_t index) const {
    if ( index < GetSize() ) {
        return *(IGetFieldDescriptor(index));
    } else {
        THROW_CFG_EXCEPTION(ADVPCS_DESC_INVALID_INDEX);
    }
}

void XmlDescriptor::parseDocument()
{
    CXmlNode* current = GetRoot()->GetChildren();
    while ( NULL != current ) {
        GetChildren()->Append((wxObject*)new XmlFieldDescriptor(*current));
        current = current->GetNext();
    }
}

void XmlDescriptor::CheckValid()
{
    size_t m_rowSize = 0;

	for (size_t i=0; i<GetSize(); i++) {
        const FieldDescriptor& ed = GetFieldDescriptor(i);
        m_rowSize += ed.GetMaxSize();
		if ( !ed.IsEnabled() 
            && 0 != ed.IsValid(ed.GetDefaultValue()).Cmp(wxEmptyString) 
            && !(ed.GetType().Cmp("carrier") == 0)) 
        {
            THROW_CFG_EXCEPTION(ADVPCS_DESC_WRONG_FIELD_NODE + ed.GetName() + "]");
        }
        bool fieldFound = false;
        if ( !ed.GetCrossFieldName().Cmp(wxEmptyString) == 0 ) {
            for ( size_t indx=0; indx<GetSize(); indx++ ) {
                if ( GetFieldDescriptor(indx).GetName().Cmp(ed.GetCrossFieldName()) == 0 ) {
                    fieldFound = true;
                }
            }
            if ( !fieldFound ) {
                THROW_CFG_EXCEPTION(wxString::Format(ADVPCS_UNKNOWN_FIELD_NAME_ERR_MSG, ed.GetCrossFieldName()));
            }
        }
        const FieldDependence* dep = ed.GetDependence();
        if ( NULL != dep ) {
            if ( dep->GetFieldIndex() == i ) {
                THROW_CFG_EXCEPTION(ADVPCS_DESC_DEPENDENCE_ONESELF);
            }
            const FieldDependence* curDep = dep;
            while ( true ) {
                const FieldDescriptor& current = GetFieldDescriptor(curDep->GetFieldIndex());
                curDep = current.GetDependence();
                if ( NULL == curDep ) {
                    break;
                }
                if ( curDep->GetFieldIndex() == i ) {
                    THROW_CFG_EXCEPTION(ADVPCS_DESC_DEPENDENCE_RECURSIVE + 
                        ed.GetName() + " from " + current.GetName());
                }
            }
        }
    }
	if ( m_rowSize > (wxGetApp().GetEdiRowLength()) ) {
        THROW_CFG_EXCEPTION("Invalid row_size parameter");
	}
}

//
// XmlElementDescriptor
//
XmlFieldDescriptor::XmlFieldDescriptor(const CXmlNode& field)
    : m_choices(), m_data(field)
{
    m_dependence = NULL;
    parseNode();
};

XmlFieldDescriptor::~XmlFieldDescriptor() 
{
    if ( m_dependence != NULL ) {
        delete m_dependence;
    }
};
    
wxString XmlFieldDescriptor::GetType() const {
    return m_type;
};

wxString XmlFieldDescriptor::GetName() const {
    return m_name;
};

wxString XmlFieldDescriptor::GetShortName() const {
    return m_shortName;
};

bool XmlFieldDescriptor::GetAlign() const {
    return m_align;
};

size_t XmlFieldDescriptor::GetLenght() const {
    return m_lenght;
};

size_t XmlFieldDescriptor::GetMaxSize() const {
    return m_maxSize;
};

size_t XmlFieldDescriptor::GetMinSize() const {
    return m_minSize;
};

bool XmlFieldDescriptor::IsRequired() const {
    return m_required;
};

bool XmlFieldDescriptor::IsEnabled() const {
    return m_enabled;
};

wxString XmlFieldDescriptor::GetStartDate() const {
    return m_startDate;
};

wxString XmlFieldDescriptor::GetEndDate() const {
    return m_endDate;
};

wxString XmlFieldDescriptor::GetRegularExpression() const {
    return m_regularExpression;
};

wxString XmlFieldDescriptor::GetLaterThan() const {
    return m_later_than;
};

wxString XmlFieldDescriptor::GetEarlyThan() const {
    return m_early_than;
};


wxString XmlFieldDescriptor::IsValid(const wxString& value) const {

    if ( 0 == GetType().Cmp("carrier") ) {
        return wxEmptyString;
    }
    
	if ( value.IsEmpty() && IsRequired()) {
        return "Can't be empty";
    } else if ((GetType().SubString(0,3).Cmp("date")==0) || (GetType().SubString(4,7).Cmp("date")==0)) {
        wxDateTime date;
        if ( date.ParseFormat(value, "%m/%d/%Y") ) {
            if ( date.GetYear() < 1000 ) {
                return "Invalid date format";
            } else if ( !GetStartDate().IsEmpty() || !GetEndDate().IsEmpty() ) {
				wxDateTime s_date;
                if ( s_date.ParseFormat(GetStartDate(), "%m/%d/%Y") ) {
					if ( date.IsEarlierThan(s_date) && !(date.Format("%m/%d/%Y") == s_date.Format("%m/%d/%Y")) ) {
						return "Date is earlier than start_date [" + s_date.Format("%m/%d/%Y") + "].";
//						return "Date is earlier than start_date.  date: " + date.Format("%m/%d/%Y") + " s_date: " + s_date.Format("%m/%d/%Y");
					}
				}
				wxDateTime e_date;
				if ( e_date.ParseFormat(GetEndDate(), "%m/%d/%Y") ) {
					if ( date.IsLaterThan(e_date) ) {
						return "Date is later than end_date [" + e_date.Format("%m/%d/%Y") + "].";
					}
				}
			}
        } else if ( 
			       value.IsEmpty()
				|| ( (GetType().Cmp("date0")==0 || GetType().Cmp("longdate0")==0) && (value.Cmp("00/00/0000") == 0) )
				|| ( (GetType().Cmp("date9")==0 || GetType().Cmp("longdate9")==0) && (value.Cmp("99/99/9999") == 0) )
				|| ( (GetType().Cmp("date09")==0 || GetType().Cmp("longdate09")==0) && ((value.Cmp("99/99/9999") == 0) || (value.Cmp("00/00/0000") == 0)))
				) {
            return wxEmptyString;
        } else {
            return "Invalid date format";
        }
    } else if ( GetType().Cmp("choice") == 0 ) {
        if ( value.IsEmpty() ) {
            if ( IsRequired() ) {
                return "Can't be empty";    
            } else {
                return wxEmptyString;
            }
        }
        for (size_t k=0; k < GetChoices().GetCount(); k++ ) {
            if ( CutValue(GetChoices().Item(k)).Cmp(CutValue(value)) == 0 ) {
                return wxEmptyString;
            }
        }
        return "Value not in list";
    } else if ( GetType().Cmp("money") == 0 ) {
        if ( value.Len() > GetMaxSize()+1 ) {
            return "Too long";
        }
    } else if ( value.Len() > GetMaxSize() ) {
        return "Length too big";
    } else if ( (value.Len() < GetMinSize()) && (!value.IsEmpty()) ) {
        return "Length too small";
    } else if ((GetType().Cmp("numeric")==0) && (!value.IsNumber())) {
        return "Must be numeric";
    } else if ( GetType().Cmp("regexp")==0 ) {
        wxRegEx reAlphaNum = GetRegularExpression();
        if ( !reAlphaNum.Matches(wxString(value)) ) {
            return "Do not match regular expression";
        }
    } else if ( GetType().Cmp("alphanumeric")==0 ) {
        wxRegEx reAlphaNum = "([^A-Za-z0-9])";
        if ( reAlphaNum.Matches(wxString(value)) ) {
            return "Must be alpha-numeric";
        }
    } 
    return wxEmptyString;
};

wxString XmlFieldDescriptor::GetDefaultValue() const {
    return m_defaultValue;
};

wxString XmlFieldDescriptor::GetSeparator() const {
    return m_separator;
};

const wxArrayString& XmlFieldDescriptor::GetChoices() const {
    return m_choices;
};

const wxString& XmlFieldDescriptor::GetCrossFieldName() const {
    return m_crossField;
};

const ChoiceList& XmlFieldDescriptor::GetChoicesDesk() const {
    return m_choiceList;
};

#if 0
const DependenceList& XmlFieldDescriptor::GetDependences() const {
    return m_dependenceList;
};
#endif

const FieldDependence* XmlFieldDescriptor::GetDependence() const {
    return m_dependence;
};

const wxString& XmlFieldDescriptor::GetCheckEmptyName() const {
    return m_check_empty;
};

const wxString& XmlFieldDescriptor::GetEmptyThenPattern() const {
    return m_empty_then;
};

const bool XmlFieldDescriptor::HasEmptyThenPattern() const {
    return m_has_empty_then;
};

const wxString& XmlFieldDescriptor::GetEmptyElsePattern() const {
    return m_empty_else;
};

const bool XmlFieldDescriptor::HasEmptyElsePattern() const {
    return m_has_empty_else;
};

bool XmlFieldDescriptor::MatchPattern(const wxString& candidate, const wxString& pattern) const {
    wxString value = candidate;
    value = value.Trim(false).Trim(true);
    if ( pattern.IsEmpty() ) {
        return value.IsEmpty();
    } else {
        wxChar designator = pattern[0];
        if ( '!' == designator ) {
            return ( !value.IsEmpty() && 0 != pattern.Mid(1).Cmp(value) );
        } else {
            // to do: || is added specially for 00/00/0000 pattern
            return (0 == pattern.Cmp(value) 
                || (0 == ZERO_DATE_PATTERN.Cmp(pattern) && value.IsEmpty()));
        }
    }
};

wxString XmlFieldDescriptor::CutValue(const wxString& candidate) const {
    if ( GetSeparator().IsEmpty() ) {
        return candidate;
    } else {
        wxStringTokenizer st(candidate, GetSeparator());
        return st.GetNextToken();
    }
};

wxString XmlFieldDescriptor::PrepareValue(const wxString& candidate) const {
    wxString result = candidate;

//------ Cur First tag 
    if ( ! GetSeparator().IsEmpty() ) {
        wxStringTokenizer st(result, GetSeparator());
        result = st.GetNextToken();
    }

// ----- convert to Upper Case
    if ( GetType().Cmp("text") == 0 || GetType().Cmp("alphanumeric") == 0 || GetType().Cmp("regexp") == 0 ) {
        result = result.Upper();
    }

//------ remove last <CR> or <LF>
    if ( result.Last() == '\r' || result.Last() == '\n') {
        result = result.RemoveLast();
        if ( result.Last() == '\r' || result.Last() == '\n') {
            result = result.RemoveLast();
        }
    }
//------ parsing dates
    if ( 0 == GetType().Cmp("date") 
        || 0 == GetType().Cmp("date0") 
        || 0 == GetType().Cmp("date9") 
        || 0 == GetType().Cmp("date09") 
        || 0 == GetType().Cmp("longdate") 
        || 0 == GetType().Cmp("longdate0") 
        || 0 == GetType().Cmp("longdate9") 
        || 0 == GetType().Cmp("longdate09") 
        ) 
    {
        wxDateTime date;
        if ( 0 == result.Cmp("  /  /    ") ) {
            result = wxEmptyString;
        } else if ( date.ParseFormat(result, "%m/%d/%Y") ) {
            result = date.Format("%m/%d/%Y");
        }
    } else if ( 0 == GetType().Cmp("money") ) 
    {
        // do not truncate it
        ;
    } else {
//------ Cut value to max_size 
        if ( result.Len() > GetMaxSize() ) {
            result = wxString(result.Truncate(GetMaxSize()));
        }
    }
    return result;
};

wxString XmlFieldDescriptor::DependenceValue(const wxString& candidate) const {
    if ( GetDependence() == NULL ) {
        return wxString();
    } else {
        if ( GetDependence()->GetBeginPosition() >= candidate.Length() ) {
            return wxString();
        } 
        if ( GetDependence()->GetEndPosition() >= candidate.Length() ) {
            return candidate.Mid(GetDependence()->GetBeginPosition());
        } 
        if ( GetDependence()->GetBeginPosition() > GetDependence()->GetEndPosition() ) {
            return candidate.Mid(GetDependence()->GetEndPosition(),
                                 GetDependence()->GetBeginPosition() - GetDependence()->GetEndPosition() + 1);
        } else {
            return candidate.Mid(GetDependence()->GetBeginPosition(),
                                 GetDependence()->GetEndPosition() - GetDependence()->GetBeginPosition() + 1);
        }
    }
};

wxString XmlFieldDescriptor::GetDependenceValue(const wxString& candidate, int index) const {
    if ( GetDependence() == NULL ) {
        return wxString();
    } else {
        if ( GetDependences()[index].beginPos >= candidate.Length() ) {
            return wxString();
        } 
        if ( GetDependences()[index].endPos >= candidate.Length() ) {
            return candidate.Mid(GetDependences()[index].beginPos);
        } 
        if ( GetDependences()[index].beginPos > GetDependences()[index].endPos ) {
            return candidate.Mid(GetDependences()[index].endPos,
                                 GetDependences()[index].beginPos - GetDependences()[index].endPos + 1);
        } else {
            return candidate.Mid(GetDependences()[index].beginPos,
                                 GetDependences()[index].endPos - GetDependences()[index].beginPos + 1);
        }
    }
};

// private 
void XmlFieldDescriptor::parseNode()
{
    if ( !GetNode().HasProp(NAME_ATTR.c_str()) 
         || !GetNode().HasProp(TYPE_ATTR.c_str()) 
         || ( !GetNode().HasProp(LENGHT_ATTR.c_str()) && !GetNode().HasProp(MAX_SIZE_ATTR.c_str()) )
         || !GetNode().HasProp(ENABLED_ATTR.c_str()) 
         || !GetNode().HasProp(REQUIRED_ATTR.c_str()) 
		 ) 
    {
        THROW_CFG_EXCEPTION(ADVPCS_DESC_WRONG_XML);
    }

    m_name = GetNode().GetPropVal(NAME_ATTR.c_str(), "");
    m_shortName = GetNode().GetPropVal(SHORT_NAME_ATTR.c_str(), m_name.c_str());
    m_type = GetNode().GetPropVal(TYPE_ATTR.c_str(), "");
//    m_lenght = parseLongValue(GetNode().GetPropVal(LENGHT_ATTR.c_str(), ""));
    if ( !GetNode().HasProp(LENGHT_ATTR.c_str()) ) {
        m_lenght = parseLongValue(GetNode().GetPropVal(MAX_SIZE_ATTR.c_str(), ""));
    } else {
        m_lenght = parseLongValue(GetNode().GetPropVal(LENGHT_ATTR.c_str(), ""));
    };
    if ( !GetNode().HasProp(MAX_SIZE_ATTR.c_str()) ) {
        m_maxSize = parseLongValue(GetNode().GetPropVal(LENGHT_ATTR.c_str(), ""));
    } else {
        m_maxSize = parseLongValue(GetNode().GetPropVal(MAX_SIZE_ATTR.c_str(), ""));
    };

// start - end dates
	wxDateTime date;
    m_startDate = GetNode().GetPropVal(START_DATE_ATTR.c_str(), "");
	if ( 0 == m_startDate.CmpNoCase("now") ) {
		m_startDate = wxDateTime::Now().Format("%m/%d/%Y");
	} else if ( !(date.ParseFormat(m_startDate, "%m/%d/%Y") || m_startDate.IsEmpty()) ) {
        THROW_CFG_EXCEPTION(GetName() + ": [start_date] " + ADVPCS_DESC_WRONG_XML);
	}
    m_endDate = GetNode().GetPropVal(END_DATE_ATTR.c_str(), "");
	if ( 0 == m_endDate.CmpNoCase("now") ) {
		m_endDate = wxDateTime::Now().Format("%m/%d/%Y");
	} else if ( !(date.ParseFormat(m_endDate, "%m/%d/%Y") || m_endDate.IsEmpty()) ) {
        THROW_CFG_EXCEPTION(GetName() + ": [end_date] " + ADVPCS_DESC_WRONG_XML);
	}
//-------

// later_than, early_than
    if ( GetNode().HasProp(LATER_THAN_ATTR.c_str()) ) {
        m_later_than = GetNode().GetPropVal(LATER_THAN_ATTR.c_str(), "");
	} else {
		m_later_than = wxEmptyString;
	}
    if ( GetNode().HasProp(EARLY_THAN_ATTR.c_str()) ) {
        m_early_than = GetNode().GetPropVal(EARLY_THAN_ATTR.c_str(), "");
	} else {
		m_early_than = wxEmptyString;
	}
//--------

    m_minSize = parseLongValue(GetNode().GetPropVal(MIN_SIZE_ATTR.c_str(), "0"));
    m_required = parseBoolValue(GetNode().GetPropVal(REQUIRED_ATTR.c_str(), ""));
    m_enabled = parseBoolValue(GetNode().GetPropVal(ENABLED_ATTR.c_str(), ""));
    
    if ( m_lenght < m_maxSize
         || m_maxSize < m_minSize 
         ) 
    {
        THROW_CFG_EXCEPTION(GetName() + ": " + ADVPCS_DESC_WRONG_XML);
    }

    if ( GetNode().HasProp(DEFAULT_ATTR.c_str()) ) {
        m_defaultValue = GetNode().GetPropVal(DEFAULT_ATTR.c_str(), "");
        if ( 0 == m_type.Cmp("date") && 0 == m_defaultValue.CmpNoCase("now")) { 
            m_defaultValue = wxDateTime::Now().Format("%m/%d/%Y");
        }
    } else {
        m_defaultValue = wxEmptyString;
    }
    if ( GetNode().HasProp(ALIGN_ATTR.c_str()) ) {
        m_align = parseBoolValue(GetNode().GetPropVal(ALIGN_ATTR.c_str(), "left"));
    } else {
        m_align = LEFT_ALIGN;
    }
    if ( GetNode().HasProp(SEPARATOR_ATTR.c_str()) ) {
        m_separator = GetNode().GetPropVal(SEPARATOR_ATTR.c_str(), "");
    } else {
        m_separator = DEFAULT_SEPARATOR.c_str();
    }

    if ( GetNode().HasProp(CROSS_FIELD.c_str()) ) {
        m_crossField = GetNode().GetPropVal(CROSS_FIELD.c_str(), "");
    }
    
    if ( m_type.Cmp(CHOICE) == 0 ) {
        parseChoices();
    }

    // check/then/else
    if ( GetNode().HasProp(CHECK_EMPTY_ATTR.c_str()) ) {
        m_check_empty = GetNode().GetPropVal(CHECK_EMPTY_ATTR.c_str(), "");

        if ( GetNode().HasProp(EMPTY_THEN_ATTR.c_str()) ) {
            m_empty_then = GetNode().GetPropVal(EMPTY_THEN_ATTR.c_str(), "");
			m_has_empty_then = true;
        } else {
			m_has_empty_then = false;
//            THROW_CFG_EXCEPTION(GetName() + ": " + ADVPCS_DESC_WRONG_XML);
        }

        if ( GetNode().HasProp(EMPTY_ELSE_ATTR.c_str()) ) {
            m_empty_else = GetNode().GetPropVal(EMPTY_ELSE_ATTR.c_str(), "");
			m_has_empty_else = true;
        } else {
			m_has_empty_else = false;
//            THROW_CFG_EXCEPTION(GetName() + ": " + ADVPCS_DESC_WRONG_XML);
        }
    } else {
        m_check_empty = wxEmptyString;
    }

	if ( m_type.Cmp("regexp") == 0 ) {
		if ( !GetNode().HasProp(PATTERN_ATTR.c_str()) ) {
			THROW_CFG_EXCEPTION(GetName() + ": " + 	ADVPCS_DESC_WRONG_REGEXP_NODE);
		} else {
			m_regularExpression = GetNode().GetPropVal(PATTERN_ATTR.c_str(), "");
			wxRegEx reRegExp = m_regularExpression;
			if ( reRegExp.IsValid() ) {
				m_regularExpression = GetNode().GetPropVal(PATTERN_ATTR.c_str(), "");
			} else {
				THROW_CFG_EXCEPTION(GetName() + ": " + 	ADVPCS_DESC_WRONG_REGEXP_NODE);
			}

		}
	} 

    // dependences
    parseDependence();
    parseDependences();
};
    
long XmlFieldDescriptor::parseLongValue(CString& value)
{
    return atol(value);
};

bool XmlFieldDescriptor::parseBoolValue(CString& value)
{
    if ( 0 == value.CompareNoCase("no") 
         || 0 == value.CompareNoCase("n")
         || 0 == value.CompareNoCase("left") ) 
    {
        return false;
    } else if ( 0 == value.CompareNoCase("yes") 
                || 0 == value.CompareNoCase("y")
                || 0 == value.CompareNoCase("right") ) 
    {
        return true;
    } else {
        THROW_CFG_EXCEPTION(ADVPCS_DESC_WRONG_BOOLEAN_NODE);
    }
};

void XmlFieldDescriptor::parseChoices()
{
    CXmlNode* current = GetNode().GetChildren();
    while ( NULL != current ) {
        ChoiceDesc desk;
        
        if ( wxString(current->GetName()).Cmp(CHOICE) == 0 ) {
            if ( !current->HasProp(VALUE_ATTR.c_str()) ) {
                THROW_CFG_EXCEPTION(ADVPCS_DESC_WRONG_CHOICE_NODE);
            }
            desk.choice = current->GetPropVal(VALUE_ATTR.c_str(), "");
            m_choices.Add(desk.choice);
        
            if ( current->HasProp(CHOICE_ID.c_str()) ) {
                wxString idStr = current->GetPropVal(CHOICE_ID.c_str(), "");
                if ( !idStr.ToLong(&(desk.id)) ) {
                    THROW_CFG_EXCEPTION(ADVPCS_DESC_WRONG_CHOICE_ID);
                }
            }
            
            if ( current->HasProp(CROSS_IDS.c_str()) ) {
                wxString idsStr = current->GetPropVal(CROSS_IDS.c_str(), "");
                for (wxStringTokenizer tk(idsStr, ","); tk.HasMoreTokens(); ) {
                    wxString token = tk.GetNextToken();
                    long crossId = 0; 
                    if ( !token.ToLong(&crossId) ) {
                        THROW_CFG_EXCEPTION(ADVPCS_WRONG_CROSS_ID);
                    }
                    desk.ids.push_back(crossId);
                }
            }
            m_choiceList.push_back(desk);
        }
        current = current->GetNext();
    }
}

void XmlFieldDescriptor::parseDependence()
{
    CXmlNode* current = GetNode().GetChild(DEPENDENCE.c_str());
    if ( NULL != current ) {
        if ( !current->HasProp(FIELD_ATTR.c_str()) ) {
            THROW_CFG_EXCEPTION(ADVPCS_DESC_WRONG_DEPENDENCE_NODE);
        }
        size_t field = parseLongValue(current->GetPropVal(FIELD_ATTR.c_str(), ""));
        size_t bpos  = parseLongValue(current->GetPropVal(BPOS_ATTR.c_str(), "0"));
        size_t epos  = parseLongValue(current->GetPropVal(EPOS_ATTR.c_str(), "0"));
        m_dependence = new XmlFieldDependence(field, bpos, epos);
    }
}

void XmlFieldDescriptor::parseDependences()
{
    CXmlNode* current = GetNode().GetChildren();
    while ( NULL != current ) {
        FieldDependences dep;
        
        if ( wxString(current->GetName()).Cmp(DEPENDENCE) == 0 ) {
            if ( !current->HasProp(FIELD_ATTR.c_str()) ) {
                THROW_CFG_EXCEPTION(ADVPCS_DESC_WRONG_DEPENDENCE_NODE);
            }
            dep.field = current->GetPropVal(FIELD_ATTR.c_str(), "");
        
            if ( current->HasProp(BPOS_ATTR.c_str()) ) {
                wxString bposStr = current->GetPropVal(BPOS_ATTR.c_str(), "");
                if ( !bposStr.ToLong(&(dep.beginPos)) ) {
                    THROW_CFG_EXCEPTION(ADVPCS_DESC_WRONG_DEPENDENCE_NODE);
                }
            }
            
            if ( current->HasProp(EPOS_ATTR.c_str()) ) {
                wxString eposStr = current->GetPropVal(EPOS_ATTR.c_str(), "");
                if ( !eposStr.ToLong(&(dep.endPos)) ) {
                    THROW_CFG_EXCEPTION(ADVPCS_DESC_WRONG_DEPENDENCE_NODE);
                }
            }
            m_dependenceList.push_back(dep);
        }
        current = current->GetNext();
    }
}

            