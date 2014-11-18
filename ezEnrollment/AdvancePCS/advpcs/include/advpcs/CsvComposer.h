/*
 *  $RCSfile: CsvComposer.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __ADVPCS_CSV_COMPOSER_H__
#define __ADVPCS_CSV_COMPOSER_H__

#pragma warning (disable : 4786)
/* -------------------------- header place ---------------------------------- */
#include <map>
#include <advpcs/Composer.h>
/* -------------------------- implementation place -------------------------- */


typedef std::map<size_t,size_t> PosToColumnMap;

class CsvComposer : public Composer {
public:
    CsvComposer(ProcessIndicator& indicator) 
        : Composer(indicator), 
           m_fieldSep("\t"), m_lineSep("\n")
    {};

    void SetFieldSeparator(const wxString& value) { m_fieldSep = value; };
    wxString GetFieldSeparator() const { return m_fieldSep; };

    void SetLineSeparator(const wxString& value) { m_lineSep = value; };
    wxString GetLineSeparator() const { return m_lineSep; };

    virtual bool Write(const Document& doc, const wxString& filename);
    virtual void Read(Document& doc);

private:
    void FillHeader(Document& doc, const wxString& titles, const wxString& values);
    void CheckBodyHeader(Document& doc, const wxString& bodyHeader, PosToColumnMap& map);

private:

    wxString m_fieldSep;
    wxString m_lineSep;
};

#endif /* __ADVPCS_CSV_COMPOSER_H__ */

