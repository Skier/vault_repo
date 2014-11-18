/*
 *  $RCSfile: XmlDescriptor.h,v $
 *
 *  $Revision: 1.5 $
 *
 *  last change: $Date: 2004/04/19 14:47:13 $
 */

#ifndef __ADVPCS_XML_DESCRIPTOR_H__
#define __ADVPCS_XML_DESCRIPTOR_H__

/* -------------------------- header place ---------------------------------- */
#include <wx/wx.h>
#include <atf/XmlNode.h>
#include <atf/XmlDocument.h>
#include <advpcs/Descriptor.h>
/* -------------------------- implementation place -------------------------- */

#pragma warning( disable : 4290 )

class XmlFieldDependence : public FieldDependence {
public:
    XmlFieldDependence(size_t field, size_t beginPos, size_t endPos)
        : m_field(field), m_beginPos(beginPos), m_endPos(endPos) {};

    virtual size_t GetFieldIndex() const { return m_field; };
    virtual size_t GetBeginPosition() const { return m_beginPos; };
    virtual size_t GetEndPosition() const { return m_endPos; };
    virtual wxString GetDependenceValue(const wxString& candidate) const;

private:
    size_t m_field;
    size_t m_beginPos;
    size_t m_endPos;
};

class XmlFieldDescriptor : public FieldDescriptor {
public:
    XmlFieldDescriptor(const CXmlNode& field);
    ~XmlFieldDescriptor();

    virtual wxString GetType() const;
    virtual wxString GetName() const;
    virtual wxString GetShortName() const;
    virtual bool GetAlign() const;

    virtual size_t GetLenght() const;

    virtual size_t GetMaxSize() const;
    virtual size_t GetMinSize() const;

    virtual bool IsRequired() const;
    virtual bool IsEnabled() const;
    virtual wxString IsValid(const wxString& value) const ;

    virtual wxString GetDefaultValue() const;
    virtual wxString GetSeparator() const;
    virtual const wxArrayString& GetChoices() const;
    
    virtual const wxString& GetCrossFieldName() const;
    virtual const ChoiceList& GetChoicesDesk() const;
    
    virtual const FieldDependence* GetDependence() const;
    virtual const DependenceList& GetDependences() const {
        return m_dependenceList;
    };
    virtual wxString GetDependenceValue(const wxString& candidate, int index) const;

	virtual wxString GetRegularExpression() const;

	virtual wxString GetStartDate() const;
	virtual wxString GetEndDate() const;
	virtual wxString GetLaterThan() const;
	virtual wxString GetEarlyThan() const;

	virtual wxString GetTranName() const;
	virtual wxArrayString GetTranRequired() const;

    virtual const wxString& GetCheckEmptyName() const;
    virtual const wxString& GetEmptyThenPattern() const;
    virtual const wxString& GetEmptyElsePattern() const;
    
	virtual bool MatchPattern(const wxString& candidate, const wxString& pattern) const;

    virtual const wxString& GetCheckValueField() const;
    virtual const wxArrayString& GetCheckValueValues() const;
    virtual const wxString& GetCheckValueThenPattern() const;


    wxString CutValue(const wxString& candidate) const;

    wxString PrepareValue(const wxString& candidate) const;

    wxString DependenceValue(const wxString& candidate) const;

private:
    void parseNode();
    void parseChoices();
    void parseDependence();
    void parseDependences();
    long parseLongValue(CString& value);
    bool parseBoolValue(CString& value);
    
    const CXmlNode& GetNode() { return m_data; }

private: 
    const CXmlNode& m_data;
    long      m_lenght;
    long      m_maxSize;
    long      m_minSize;
    bool      m_align;
    bool      m_required;
    bool      m_enabled;
    wxString  m_type;
    wxString  m_name;
    wxString  m_shortName;
    wxString  m_defaultValue;
    wxString  m_separator;
    wxArrayString m_choices;
    ChoiceList m_choiceList;
    DependenceList m_dependenceList;
    wxString   m_crossField;
    XmlFieldDependence* m_dependence;
    wxString  m_check_empty;
    wxString  m_empty_then;
    wxString  m_empty_else;

    wxString  m_check_value_field;
    wxArrayString  m_check_value_values;
    wxString  m_check_value_then;
	
	wxString m_regularExpression;
	wxString m_startDate;
	wxString m_endDate;
	wxString m_later_than;
	wxString m_early_than;
	wxString m_tran_name;
	wxArrayString m_tran_required;
};

class XmlDescriptor : public Descriptor {
public:
    XmlDescriptor(CXmlNode& node);
    ~XmlDescriptor();

    virtual size_t GetSize() const;
    virtual const FieldDescriptor& GetFieldDescriptor(size_t index) const;

    // check is descriptor is valid, throw Exception if not
    void CheckValid();

private:

    void parseDocument();

    wxList* GetChildren() const { return m_children; }

    FieldDescriptor* IGetFieldDescriptor(size_t index) const {
        return (FieldDescriptor*)GetChildren()->Item(index)->GetData();
    }

    CXmlNode* GetRoot() { return m_root; }

private:
    wxList*   m_children;
    CXmlNode* m_root;
};

#endif // __ADVPCS_XML_DESCRIPTOR_H__