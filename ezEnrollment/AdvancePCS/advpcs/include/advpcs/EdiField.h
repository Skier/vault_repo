/*
 *  $RCSfile: EdiField.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __ADVPCS_EDI_FIELD_H__
#define __ADVPCS_EDI_FIELD_H__


/* -------------------------- header place ---------------------------------- */
#include <advpcs/Document.h>
/* -------------------------- implementation place -------------------------- */
class EdiDocument;

class EdiField : public Field {
public:
    EdiField(size_t index, EdiDocument* doc);
    EdiField(const EdiField& other);
    const EdiField& operator=(const EdiField& other);

    const FieldDescriptor& GetDescriptor() const;
    wxString GetValue() const;
    void SetValue(const wxString& value);

private:
    size_t    m_index;
    EdiDocument* m_doc;
};


#endif /* __ADVPCS_EDI_FIELD_H__ */