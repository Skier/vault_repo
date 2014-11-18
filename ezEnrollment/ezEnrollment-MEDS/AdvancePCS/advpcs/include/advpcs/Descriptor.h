/*
 *  $RCSfile: Descriptor.h,v $
 *
 *  $Revision: 1.5 $
 *
 *  last change: $Date: 2004/04/19 14:47:13 $
 */

#ifndef __ADVPCS_DESCRIPTOR_H__
#define __ADVPCS_DESCRIPTOR_H__

#include <vector>

#define RIGHT_ALIGN true
#define LEFT_ALIGN false

class FieldDependence {
public:
    virtual size_t GetFieldIndex() const = 0;
    virtual size_t GetBeginPosition() const = 0;
    virtual size_t GetEndPosition() const = 0;
    virtual wxString GetDependenceValue(const wxString& candidate) const = 0;
};

struct FieldDependences {
    wxString field;
    long     beginPos;
    long     endPos;
    FieldDependences() {};
    FieldDependences(const FieldDependences& other) {
        field = other.field;
        beginPos = other.beginPos;
        endPos = other.endPos;
    };
    void operator=(const FieldDependences& other) {
        field = other.field;
        beginPos = other.beginPos;
        endPos = other.endPos;
    }
};

typedef std::vector<FieldDependences> DependenceList;

typedef std::vector<long> IdList;  

struct ChoiceDesc {
    wxString choice;
    long     id;
    IdList   ids;
    void operator=(const ChoiceDesc& other) {
        choice = other.choice;
        id = other.id;
        ids = other.ids;
    }
};

typedef std::vector<ChoiceDesc> ChoiceList;

class FieldDescriptor {
public:
    virtual wxString GetName() const = 0;
    virtual wxString GetShortName() const = 0;
    virtual wxString GetType() const = 0;

    virtual bool GetAlign() const = 0;

    virtual size_t GetLenght() const = 0;

    virtual size_t GetMaxSize() const = 0;
    virtual size_t GetMinSize() const = 0;

    virtual bool IsRequired() const = 0;
    virtual bool IsEnabled() const = 0;
    virtual wxString IsValid(const wxString& value ) const = 0;

    virtual wxString GetDefaultValue() const = 0;
    virtual wxString GetSeparator() const = 0;

    virtual const wxArrayString& GetChoices() const = 0;
    
    // Empty string if no cross field 
    virtual const wxString& GetCrossFieldName() const = 0;
    virtual const ChoiceList& GetChoicesDesk() const = 0;

    virtual const DependenceList& GetDependences() const = 0;
    virtual wxString GetDependenceValue(const wxString& candidate, int index) const = 0;
    
    virtual wxString PrepareValue(const wxString& candidate) const = 0;

    // may be NULL if not dependence
    virtual const FieldDependence* GetDependence() const = 0;

    // Those attributes intended, for check value dependent from value in another field
    // CheckEmpty   FieldName of field that should be checked for empty (if wxEmptyString then not used for this field)
    // EmptyThen    Correct value/pattern if CheckEmpty is empty
    // EmptyElse    Correct value/pattern if CheckField is not empty
    virtual const wxString& GetCheckEmptyName() const = 0;
    virtual const wxString& GetEmptyThenPattern() const = 0;
    virtual const wxString& GetEmptyElsePattern() const = 0;

    virtual bool MatchPattern(const wxString& candidate, const wxString& pattern) const = 0;

    virtual const wxString& GetCheckValueField() const = 0;
    virtual const wxArrayString& GetCheckValueValues() const = 0;
    virtual const wxString& GetCheckValueThenPattern() const = 0;

	virtual wxString GetRegularExpression() const = 0;

	virtual wxString GetStartDate() const = 0;
	virtual wxString GetEndDate() const   = 0;
	virtual wxString GetLaterThan() const = 0;
	virtual wxString GetEarlyThan() const = 0;

	virtual wxString GetTranName() const = 0;
	virtual wxArrayString GetTranRequired() const = 0;
};


class Descriptor {
public:
    virtual size_t GetSize() const = 0;

    virtual const FieldDescriptor& GetFieldDescriptor(size_t index) const = 0;
};

#endif /* __ADVPCS_DESCRIPTOR_H__ */
