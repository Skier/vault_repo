/*
 *  $RCSfile: EdiField.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#include "wx/wxprec.h"

#ifdef __BORLANDC__
    #pragma hdrstop
#endif

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif

/* -------------------------- header place ---------------------------------- */
#include <atf/Exception.h>
#include <advpcs/EdiField.h>
#include <advpcs/Descriptor.h>
#include <advpcs/EdiDocument.h>
/* -------------------------- implementation place -------------------------- */
EdiField::EdiField(size_t index, EdiDocument* doc) 
    : m_index(index), m_doc(doc)
{
    wxASSERT(NULL != m_doc);
    wxASSERT(0 <= m_index);
    wxASSERT(m_index < m_doc->GetFieldCount());
};

EdiField::EdiField(const EdiField& other) 
    : m_index(other.m_index), m_doc(other.m_doc)
{
};

const EdiField& EdiField::operator=(const EdiField& other) {
    m_index = other.m_index;
    m_doc = other.m_doc;
    return other;
};

const FieldDescriptor& EdiField::GetDescriptor() const {
    return m_doc->GetDescriptor().GetFieldDescriptor(m_index);
};

wxString EdiField::GetValue() const {
    return m_doc->m_header[m_index];
};

void EdiField::SetValue(const wxString& value) {
    m_doc->m_header[m_index] = GetDescriptor().PrepareValue(value);
    m_doc->m_changed = true;
};

