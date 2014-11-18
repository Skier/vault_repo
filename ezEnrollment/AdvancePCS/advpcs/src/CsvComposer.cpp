/*
 *  $RCSfile: CsvComposer.cpp,v $
 *
 *  $Revision: 1.3 $
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

#pragma warning (disable : 4786)
/* -------------------------- header place ---------------------------------- */
#include <wx/file.h>
#include <wx/textfile.h>
#include <wx/tokenzr.h>
#include <atf/SystemException.h>
#include <advpcs/Document.h>
#include <advpcs/Descriptor.h>
#include <advpcs/CsvComposer.h>
#include <advpcs/ProcessIndicator.h>
#include <advpcs/ComposerException.h>
#include <advpcs/Resources.h>
/* -------------------------- implementation place -------------------------- */

bool CsvComposer::Write(const Document& doc, const wxString& fileName) {

    size_t i = 0;
    GetIndicator().StartProcess(doc.GetNumberRows());
    AutoFinnisher finnisher(GetIndicator());

    wxFile out(fileName, wxFile::write);

    if ( !out.IsOpened() ) {
        THROW_SYSTEM_EXCEPTION(fileName);
    }

        // write header titles
    for ( i = 0; i < doc.GetFieldCount(); i++ ) {
        out.Write(doc.GetField(i).GetDescriptor().GetName());
        out.Write(GetFieldSeparator());
    }
    out.Write(GetLineSeparator());

        // write header body
    for ( i = 0; i < doc.GetFieldCount(); i++ ) {
        out.Write(doc.GetField(i).GetValue());
        out.Write(GetFieldSeparator());
    }
    out.Write(GetLineSeparator());

    const Descriptor& descriptor = doc.GetColumnsDescriptor();

    for ( i = 0; i < descriptor.GetSize(); i++ ) {
        out.Write(descriptor.GetFieldDescriptor(i).GetName());
        out.Write(GetFieldSeparator());
    }
    out.Write(GetLineSeparator());

    size_t rowCount = (size_t)doc.GetNumberRows();
    for ( i = 0; i<rowCount; i++ ) {

        GetIndicator().SetState(i, wxString::Format("Row %ld", i));

        wxArrayString row;
        size_t j = 0;

            // prepare values
        for ( j = 0; j < descriptor.GetSize(); j++ ) {
            const FieldDescriptor& colDesc = descriptor.GetFieldDescriptor(j);
            if ( colDesc.IsEnabled() ) {
                row.Add(doc.GetValue(i, j));
            } else {
                row.Add(colDesc.GetDefaultValue());
            }
        }

        // transformation for not enabled dependence
        for ( j = 0; j < descriptor.GetSize(); j++) {
            const FieldDescriptor& colDesc = descriptor.GetFieldDescriptor(j);
            const FieldDependence* dep = colDesc.GetDependence();
            if ( NULL != dep ) {
                
                int idx;
                for ( int i=0; i<colDesc.GetDependences().size(); i++ ) {
                    idx = doc.GetColumnIdx( colDesc.GetDependences()[i].field );
                    if ( idx >= 0 
                      && idx < descriptor.GetSize() 
                      && !descriptor.GetFieldDescriptor(idx).IsEnabled() ) 
                    {
                         row[idx] = colDesc.GetDependenceValue(row[j], i);
                    }
                }
            }
        }

		// writing row to file
        for ( j = 0; j < descriptor.GetSize(); j++ ) {
            out.Write(row[j]);
            out.Write(GetFieldSeparator());
        }
        out.Write(GetLineSeparator());
    }
    out.Close();
    return true;
};


void CsvComposer::Read(Document& doc) {
    wxTextFile file(doc.GetFileName());
    if ( !file.Open() ) {
        THROW_SYSTEM_EXCEPTION(doc.GetFileName());
    }

    if ( 3 > file.GetLineCount() ) {
        THROW_COMPOSER_EXCEPTION(ADVPCS_COMPOSER_FORMAT_ERR_MSG);
    }

    GetIndicator().StartProcess(file.GetLineCount());
    AutoFinnisher finnisher(GetIndicator());

    wxString headerTitles = file.GetLine(0);
    wxString headerValues = file.GetLine(1);

    FillHeader(doc, headerTitles, headerValues);

    wxString bodyHeader =  file.GetLine(2);
    PosToColumnMap map;

    CheckBodyHeader(doc, bodyHeader, map);

    for (size_t i=3; i<file.GetLineCount(); i++) {
        GetIndicator().SetState(i, wxString::Format("Row %ld", i));
        doc.AppendRows(1);

        size_t col = 0;
        wxString str = file.GetLine(i);

        wxStringTokenizer s(str, GetFieldSeparator(),wxTOKEN_RET_EMPTY);
        while ( s.HasMoreTokens() ) {
            wxString value = s.GetNextToken();
            doc.SetValue(i-3, map[col++], value);
        }
    }
    file.Close();

};


void CsvComposer::FillHeader(Document& doc, const wxString& titles, const wxString& values) {
    wxStringTokenizer t(titles, GetFieldSeparator(),wxTOKEN_RET_EMPTY);
    wxStringTokenizer v(values, GetFieldSeparator(),wxTOKEN_RET_EMPTY);
    while ( t.HasMoreTokens() && v.HasMoreTokens() ) {
        wxString title = t.GetNextToken();
        wxString value = v.GetNextToken();

        Field& f = doc.GetFieldByName(title);
        f.SetValue(value);
    }

	//FIXME: for now this check is wrong 
#if 0 
    if ( t.HasMoreTokens() || v.HasMoreTokens() ) {
        THROW_COMPOSER_EXCEPTION(ADVPCS_COMPOSER_FORMAT_ERR_MSG);
    }
#endif

}

void CsvComposer::CheckBodyHeader(Document& doc, const wxString& bodyHeader, PosToColumnMap& map) {
    wxStringTokenizer h(bodyHeader, GetFieldSeparator(),wxTOKEN_RET_EMPTY);
    size_t pos = 0;
    while ( h.HasMoreTokens() ) {
        wxString title = h.GetNextToken();

        for( size_t i = 0; i < doc.GetColCount(); i++ ) {
            if ( 0 == title.CmpNoCase(doc.GetColumnDescriptor(i).GetName()) ) {
                map[pos] = i;
                goto found;
            }
        }
        THROW_COMPOSER_EXCEPTION(ADVPCS_COMPOSER_FORMAT_ERR_MSG);
    found:
        pos++;
    };
};
