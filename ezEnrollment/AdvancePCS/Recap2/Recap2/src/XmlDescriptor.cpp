#include <wx/tokenzr.h>
#include <wx/datetime.h>
#include <client/xmldescriptor.h>
#include <client/Messages.h>

#define NAME_ATTR       "name"
#define SHORT_NAME_ATTR "short_name"
#define TYPE_ATTR       "type"
#define MAX_SIZE_ATTR   "max_size"
#define MIN_SIZE_ATTR   "min_size"
#define REQUIRED_ATTR   "required"
#define DEFAULT_ATTR    "default"

#define SEPARATOR_ATTR  "separator"
#define VALUE_ATTR      "value"

#define DEPENDENCE      "dependence"
#define FIELD_ATTR      "field"
#define BPOS_ATTR       "bpos"
#define EPOS_ATTR       "epos"


// type of variant expr
#define CHOICE        "choice"
#define DEFAULT_SEPARATOR ""

XmlDescriptor::XmlDescriptor(CXmlNode& node)
{
    m_root = &node;
    m_children = new wxList();
    parseDocument();
};

XmlDescriptor::~XmlDescriptor() {
    for (unsigned int i=0; i<GetChildren()->GetCount(); i++) {
        ElementDescriptor* d = IGetElementDescriptor(i);
        delete d;
    }
    delete m_children;
};
    
long XmlDescriptor::GetSize()
{
    return GetChildren()->GetCount();
}

ElementDescriptor* XmlDescriptor::GetElementDescriptor(long index)
{
    if ( index < GetSize() ) {
        return IGetElementDescriptor(index);
    } else {
        throw wxString(DESCRIPTOR_INVALID_INDEX);
    }
}

long XmlDescriptor::GetElementDescriptorIndex(wxString& name) 
{
    if ( m_nameToIndex.find(name) != m_nameToIndex.end() ) {
        return (*m_nameToIndex.find(name)).second;
    } else {
        throw wxString(DESCRIPTOR_INVALID_INDEX) + name;
    }
}

void XmlDescriptor::parseDocument()
{
    CXmlNode* current = GetRoot()->GetChildren();
    long index = 0;
    while ( NULL != current ) {
        XmlElementDescriptor* elem = new XmlElementDescriptor(*current);
        GetChildren()->Append((wxObject*)elem);
        m_nameToIndex[elem->GetName()] = index++;
        current = current->GetNext();
    }
}

void XmlDescriptor::CheckValid()
{
    for (int i=0; i<GetSize(); i++) {
        ElementDescriptor* ed = GetElementDescriptor(i);
        ElementDependence* dep = ed->GetDependence();
        if ( NULL != dep ) {
            if ( dep->GetFieldIndex() == i ) {
                throw wxString(DESCRIPTOR_DEPENDENCE_ONESELF);
            }
            ElementDescriptor* current = GetElementDescriptor(dep->GetFieldIndex());
            while ( true ) {
                ElementDependence* curDep = current->GetDependence();
                if ( NULL == curDep ) {
                    break;
                }
                if ( curDep->GetFieldIndex() == i ) {
                    throw wxString(DESCRIPTOR_DEPENDENCE_RECURSIVE + 
                        ed->GetName() + " from " + current->GetName());
                }
                current = GetElementDescriptor(curDep->GetFieldIndex());
            }
        }
    }
}

//
// XmlElementDescriptor
//
XmlElementDescriptor::XmlElementDescriptor(CXmlNode& field)
{
    m_choices = new wxArrayString();
    m_dependence = NULL;
    m_data = &field;
    parseNode();
};

XmlElementDescriptor::~XmlElementDescriptor() 
{
    delete m_type;
    delete m_name;
    delete m_shortName;
    delete m_defaultValue;
    delete m_separator;
    delete m_choices;
    if ( m_dependence != NULL ) {
        delete m_dependence;
    }
};
    
wxString& XmlElementDescriptor::GetType()
{
    return *m_type;
};

wxString& XmlElementDescriptor::GetName()
{
    return *m_name;
};

wxString& XmlElementDescriptor::GetShortName()
{
    return *m_shortName;
};

long XmlElementDescriptor::GetMaxSize()
{
    return m_maxSize;
};

long XmlElementDescriptor::GetMinSize()
{
    return m_minSize;
};

bool XmlElementDescriptor::IsRequired()
{
    return m_required;
};

bool XmlElementDescriptor::IsValid(wxString& value)
{
    if ( value.IsEmpty() && IsRequired()) {
        return false;
    } else if ( GetType().Cmp("date")==0 ) {
        wxDateTime date;
        bool date_is_correct = date.ParseFormat(value, "%Y/%m/%d");
        if ( date_is_correct ) {
            if ( date.GetYear() < 1000 ) {
                return false;
            } else {
                return true;
            }
        } else if ( value.Cmp("9999/99/99") == 0  
                    || value.Cmp("0000/00/00") == 0 
                    || value.IsEmpty() ) {
            return true;
        } else {
            return false;
        }
    } else if ( GetType().Cmp("choice") == 0 ) {
        if ( value.IsEmpty() && !IsRequired() ) {
            return true;	
        }
        for (int k=0; k < GetChoices().GetCount(); k++ ) {
            if ( CutValue(GetChoices().Item(k)).Cmp(value.c_str()) == 0 ) {
        	return true;
            }
        }
        return false;
    } else if ( value.Len() > GetMaxSize() ) {
        return false;
    } else if ( (value.Len() < GetMinSize()) 
                && (!value.IsEmpty()) ) {
        return false;
    } else if ((GetType().Cmp("numeric")==0) 
               && (!value.IsNumber())) {
        return false;
    } else {
        return true;
    }
};

wxString& XmlElementDescriptor::GetDefaultValue()
{
    return *m_defaultValue;
};

wxString& XmlElementDescriptor::GetSeparator()
{
    return *m_separator;
};

wxArrayString& XmlElementDescriptor::GetChoices()
{
    return *m_choices;
};

ElementDependence* XmlElementDescriptor::GetDependence()
{
    return m_dependence;
};

wxString XmlElementDescriptor::CutValue(wxString& candidate)
{
    if ( GetSeparator().IsEmpty() ) {
        return candidate;
    } else {
        wxStringTokenizer st(candidate, GetSeparator());
        return st.GetNextToken();
    }
};

wxString XmlElementDescriptor::DependenceValue(wxString& candidate)
{
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

// private 
void XmlElementDescriptor::parseNode()
{
    if ( !GetNode()->HasProp(NAME_ATTR) 
         || !GetNode()->HasProp(TYPE_ATTR) 
         || !GetNode()->HasProp(MAX_SIZE_ATTR) ) {
        throw wxString::Format(DESCRIPTOR_WRONG_XML + wxString("  %s"), wxString(GetNode()->GetName()));
    }
    m_name = new wxString(GetNode()->GetPropVal(NAME_ATTR, ""));
    m_shortName = new wxString(GetNode()->GetPropVal(SHORT_NAME_ATTR, m_name->c_str()));
    m_type = new wxString(GetNode()->GetPropVal(TYPE_ATTR, ""));
    m_maxSize = parseLongValue(GetNode()->GetPropVal(MAX_SIZE_ATTR, "0"));
    m_minSize = parseLongValue(GetNode()->GetPropVal(MIN_SIZE_ATTR, "0"));
    m_required = parseBoolValue(GetNode()->GetPropVal(REQUIRED_ATTR, "no"));
    if ( GetNode()->HasProp(DEFAULT_ATTR) ) {
        m_defaultValue = new wxString(GetNode()->GetPropVal(DEFAULT_ATTR, ""));
    } else {
		if ( wxString(GetNode()->GetPropVal(NAME_ATTR, "")).Cmp("Process Date") == 0 ) {
			wxDateTime now = wxDateTime::Now();
			m_defaultValue = new wxString( now.Format("%Y/%m/%d") );
		} else if ( wxString(GetNode()->GetPropVal(NAME_ATTR, "")).Cmp("Process Time") == 0 ) {
			wxDateTime now = wxDateTime::Now();
			m_defaultValue = new wxString( now.Format("%H%M%S") );
		} else {
        	m_defaultValue = new wxString();
		}
    }
    if ( GetNode()->HasProp(SEPARATOR_ATTR) ) {
        m_separator = new wxString(GetNode()->GetPropVal(SEPARATOR_ATTR, ""));
    } else {
        m_separator = new wxString(DEFAULT_SEPARATOR);
    }

    if ( m_type->Cmp(CHOICE) == 0 ) {
        parseChoices();
    }
    parseDependence();
};
    
long XmlElementDescriptor::parseLongValue(CString& value)
{
    return atol(value);
};

bool XmlElementDescriptor::parseBoolValue(CString& value)
{
    if ( 0 == value.CompareNoCase("no") 
         || 0 == value.CompareNoCase("n") ) {
        return false;
    } else if ( 0 == value.CompareNoCase("yes") 
                || 0 == value.CompareNoCase("y") ) {
        return true;
    } else {
        throw wxString(DESCRIPTOR_WRONG_BOOLEAN_NODE);
    }
};

void XmlElementDescriptor::parseChoices()
{
    CXmlNode* current = GetNode()->GetChildren();
    while ( NULL != current ) {
        if ( wxString(current->GetName()).Cmp(CHOICE) == 0 ) {
            if ( !current->HasProp(VALUE_ATTR) ) {
                throw wxString(DESCRIPTOR_WRONG_CHOICE_NODE);
            }
            wxString choice(current->GetPropVal(VALUE_ATTR, ""));
            m_choices->Add(choice);
        }
        current = current->GetNext();
    }
}

void XmlElementDescriptor::parseDependence()
{
    CXmlNode* current = GetNode()->GetChild(DEPENDENCE);
    if ( NULL != current ) {
        if ( !current->HasProp(FIELD_ATTR) ) {
            throw wxString(DESCRIPTOR_WRONG_DEPENDENCE_NODE);
        }
        size_t field = parseLongValue(current->GetPropVal(FIELD_ATTR, ""));
        size_t bpos = parseLongValue(current->GetPropVal(BPOS_ATTR, "0"));
        size_t epos = parseLongValue(current->GetPropVal(EPOS_ATTR, "0"));
        m_dependence = new XmlElementDependence(field, bpos, epos);
    }
}