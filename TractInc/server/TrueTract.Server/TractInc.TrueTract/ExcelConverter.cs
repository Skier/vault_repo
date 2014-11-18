using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract
{
    internal class ExcelConverter
    {

        #region Constants

        private static string EXCEL_XML_BOOK_BEGIN = 
@"<?xml version=""1.0""?>
<Workbook xmlns=""urn:schemas-microsoft-com:office:spreadsheet""
 xmlns:o=""urn:schemas-microsoft-com:office:office""
 xmlns:x=""urn:schemas-microsoft-com:office:excel""
 xmlns:ss=""urn:schemas-microsoft-com:office:spreadsheet""
 xmlns:html=""http://www.w3.org/TR/REC-html40"">
";

        private static string EXCEL_XML_BOOK_END = @"
</Workbook>
";

        private static string EXCEL_XML_BOOK_STYLES = @"
 <Styles>
  <Style ss:ID=""Default"" ss:Name=""Normal"">
   <Alignment ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Font ss:FontName=""Tahoma""/>
  </Style>
  <Style ss:ID=""s21"">
   <Alignment ss:Horizontal=""Left"" ss:Vertical=""Top"" ss:WrapText=""1""/>
  </Style>
  <Style ss:ID=""s22"">
   <Alignment ss:Horizontal=""Left"" ss:Vertical=""Top"" ss:WrapText=""1""/>
  </Style>
  <Style ss:ID=""s24"">
   <Alignment ss:Horizontal=""Center"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Font ss:Size=""14"" ss:Bold=""1""/>
  </Style>
  <Style ss:ID=""s26"">
   <Alignment ss:Horizontal=""Center"" ss:Vertical=""Top"" ss:WrapText=""1""/>
  </Style>
  <Style ss:ID=""s28"">
   <Alignment ss:Horizontal=""Center"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Font ss:Bold=""1""/>
   <Interior ss:Color=""#FFFF00"" ss:Pattern=""Solid""/>
  </Style>
  <Style ss:ID=""s31"">
   <Alignment ss:Horizontal=""Center"" ss:Vertical=""Center"" ss:WrapText=""1""/>
  </Style>
  <Style ss:ID=""s33"">
   <Alignment ss:Horizontal=""Right"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Font ss:Italic=""1""/>
  </Style>
  <Style ss:ID=""s34"">
   <Alignment ss:Horizontal=""Center"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>
    <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Top"" ss:LineStyle=""Double"" ss:Weight=""3""/>
   </Borders>
   <Font ss:Bold=""1""/>
   <Interior ss:Color=""#FFFF99"" ss:Pattern=""Solid""/>
  </Style>
  <Style ss:ID=""s35"">
   <Alignment ss:Horizontal=""Center"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>
    <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Top"" ss:LineStyle=""Double"" ss:Weight=""3""/>
   </Borders>
   <Font ss:Bold=""1""/>
   <Interior ss:Color=""#FFFF99"" ss:Pattern=""Solid""/>
  </Style>
  <Style ss:ID=""s36"">
   <Alignment ss:Horizontal=""Center"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
   </Borders>
  </Style>
  <Style ss:ID=""s37"">
   <Alignment ss:Horizontal=""Center"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""2""/>
    <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Top"" ss:LineStyle=""Double"" ss:Weight=""3""/>
   </Borders>
   <Font ss:Bold=""1""/>
   <Interior ss:Color=""#FFFF99"" ss:Pattern=""Solid""/>
  </Style>
  <Style ss:ID=""s38"">
   <Alignment ss:Horizontal=""Center"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
   </Borders>
  </Style>
  <Style ss:ID=""s39"">
   <Alignment ss:Horizontal=""Left"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
   </Borders>
  </Style>
  <Style ss:ID=""s40"">
   <Alignment ss:Horizontal=""Center"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
   </Borders>
   <NumberFormat ss:Format=""mm/dd/YYYY""/>
  </Style>
  <Style ss:ID=""s41"">
   <Alignment ss:Horizontal=""Left"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
   </Borders>
  </Style>
  <Style ss:ID=""s42"">
   <Alignment ss:Horizontal=""Right"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Double"" ss:Weight=""3""/>
    <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
   </Borders>
  </Style>
  <Style ss:ID=""s43"">
   <Alignment ss:Horizontal=""Left"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Double"" ss:Weight=""3""/>
    <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
   </Borders>
  </Style>
  <Style ss:ID=""s44"">
   <Alignment ss:Horizontal=""Center"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Double"" ss:Weight=""3""/>
    <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
   </Borders>
  </Style>
  <Style ss:ID=""s45"">
   <Alignment ss:Horizontal=""Left"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Double"" ss:Weight=""3""/>
    <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
   </Borders>
  </Style>
  <Style ss:ID=""s47"">
   <Alignment ss:Horizontal=""Left"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
   </Borders>
   <Font ss:Size=""8""/>
  </Style>
  <Style ss:ID=""s48"">
   <Alignment ss:Horizontal=""Left"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
   </Borders>
   <Font ss:Size=""8""/>
  </Style>
  <Style ss:ID=""s49"">
   <Alignment ss:Horizontal=""Left"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Font ss:Size=""8""/>
  </Style>
  <Style ss:ID=""s50"">
   <Alignment ss:Horizontal=""Left"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
   </Borders>
   <Font ss:Size=""8""/>
  </Style>
  <Style ss:ID=""s51"">
   <Alignment ss:Horizontal=""Left"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
   </Borders>
   <Font ss:Size=""8""/>
  </Style>
  <Style ss:ID=""s52"">
   <Alignment ss:Horizontal=""Left"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Top"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
   </Borders>
   <Font ss:Size=""8""/>
  </Style>
  <Style ss:ID=""s54"">
   <Alignment ss:Horizontal=""Left"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Borders/>
   <Font ss:Size=""8""/>
  </Style>
  <Style ss:ID=""s55"">
   <Alignment ss:Horizontal=""Left"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
   </Borders>
   <Font ss:Size=""8""/>
  </Style>
  <Style ss:ID=""s57"">
   <Alignment ss:Horizontal=""Left"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Left"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
   </Borders>
   <Font ss:Size=""8""/>
  </Style>
  <Style ss:ID=""s58"">
   <Alignment ss:Horizontal=""Left"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
   </Borders>
   <Font ss:Size=""8""/>
  </Style>
  <Style ss:ID=""s59"">
   <Alignment ss:Horizontal=""Left"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
   </Borders>
   <Font ss:Size=""8""/>
  </Style>
  <Style ss:ID=""s60"">
   <Alignment ss:Horizontal=""Left"" ss:Vertical=""Top"" ss:WrapText=""1""/>
   <Borders>
    <Border ss:Position=""Bottom"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
    <Border ss:Position=""Right"" ss:LineStyle=""Continuous"" ss:Weight=""1""/>
   </Borders>
   <Font ss:Size=""8""/>
  </Style>
 </Styles>
";

        private static string EXCEL_XML_TAB_SHEET_BEGIN = @"
 <Worksheet ss:Name=""{0}"">
  <Table ss:ExpandedColumnCount=""8"" x:FullColumns=""1"" x:FullRows=""1"">
   <Column ss:Width=""50""/>
   <Column ss:AutoFitWidth=""0"" ss:Width=""120""/>
   <Column ss:Width=""60""/>
   <Column ss:Width=""60""/>
   <Column ss:AutoFitWidth=""0"" ss:Width=""140""/>
   <Column ss:AutoFitWidth=""0"" ss:Width=""140""/>
   <Column ss:AutoFitWidth=""0"" ss:Width=""200""/>
   <Column ss:AutoFitWidth=""0"" ss:Width=""200""/>
";

        private static string EXCEL_XML_TAB_SHEET_END = @"
   <Row ss:AutoFitHeight=""0"" ss:Height=""13.5"">
    <Cell ss:StyleID=""s42""/>
    <Cell ss:StyleID=""s43""/>
    <Cell ss:StyleID=""s44""/>
    <Cell ss:StyleID=""s44""/>
    <Cell ss:StyleID=""s43""/>
    <Cell ss:StyleID=""s43""/>
    <Cell ss:StyleID=""s43""/>
    <Cell ss:StyleID=""s45""/>
   </Row>
   <Row ss:AutoFitHeight=""0"" ss:Height=""13.5""/>
   <Row>
    <Cell ss:MergeAcross=""3"" ss:StyleID=""s47""><Data ss:Type=""String"">AOGL - Assignment Oil and Gas Lease</Data></Cell>
    <Cell ss:MergeAcross=""1"" ss:StyleID=""s50""><Data ss:Type=""String"">FBO - For the Benefit of</Data></Cell>
    <Cell ss:StyleID=""s51""><Data ss:Type=""String"">PN -  Promissory Note</Data></Cell>
    <Cell ss:StyleID=""s52""/>
   </Row>
   <Row>
    <Cell ss:MergeAcross=""3"" ss:StyleID=""s48""><Data ss:Type=""String"">AOGML - Assignment Oil, Gas and Mineral Lease</Data></Cell>
    <Cell ss:MergeAcross=""1"" ss:StyleID=""s49""><Data ss:Type=""String"">FS - Financing Statement</Data></Cell>
    <Cell ss:StyleID=""s54""><Data ss:Type=""String"">QC - Quit Claim</Data></Cell>
    <Cell ss:StyleID=""s55""/>
   </Row>
   <Row>
    <Cell ss:MergeAcross=""3"" ss:StyleID=""s48""><Data ss:Type=""String"">AORRI - Assignment Overriding Royalty Interest</Data></Cell>
    <Cell ss:MergeAcross=""1"" ss:StyleID=""s49""><Data ss:Type=""String"">LP - Limited Partnership</Data></Cell>
    <Cell ss:StyleID=""s54""><Data ss:Type=""String"">QCD - Quit Claim Deed</Data></Cell>
    <Cell ss:StyleID=""s55""/>
   </Row>
   <Row>
    <Cell ss:MergeAcross=""3"" ss:StyleID=""s48""><Data ss:Type=""String"">ARTI - All right, title and interest</Data></Cell>
    <Cell ss:MergeAcross=""1"" ss:StyleID=""s49""><Data ss:Type=""String"">NFR - Not found of record</Data></Cell>
    <Cell ss:StyleID=""s54""><Data ss:Type=""String"">SA - Security Agreement</Data></Cell>
    <Cell ss:StyleID=""s55""/>
   </Row>
   <Row>
    <Cell ss:MergeAcross=""3"" ss:StyleID=""s48""><Data ss:Type=""String"">Asmt - Assignment</Data></Cell>
    <Cell ss:MergeAcross=""1"" ss:StyleID=""s49""><Data ss:Type=""String"">NRI - Net Revenue Interest</Data></Cell>
    <Cell ss:StyleID=""s54""><Data ss:Type=""String"">SWD - Special Warranty Deed</Data></Cell>
    <Cell ss:StyleID=""s55""/>
   </Row>
   <Row>
    <Cell ss:MergeAcross=""3"" ss:StyleID=""s48""><Data ss:Type=""String"">BOS - Bill of Sale</Data></Cell>
    <Cell ss:MergeAcross=""1"" ss:StyleID=""s49""><Data ss:Type=""String"">OGL - Oil and gas Lease</Data></Cell>
    <Cell ss:StyleID=""s54""><Data ss:Type=""String"">WD - Warranty Deed</Data></Cell>
    <Cell ss:StyleID=""s55""/>
   </Row>
   <Row>
    <Cell ss:MergeAcross=""3"" ss:StyleID=""s48""><Data ss:Type=""String"">CC - Certified Copy</Data></Cell>
    <Cell ss:MergeAcross=""1"" ss:StyleID=""s49""><Data ss:Type=""String"">OGM - Oil, gas and mineral</Data></Cell>
    <Cell ss:StyleID=""s54""><Data ss:Type=""String"">WI - Working Interest</Data></Cell>
    <Cell ss:StyleID=""s55""/>
   </Row>
   <Row>
    <Cell ss:MergeAcross=""3"" ss:StyleID=""s48""><Data ss:Type=""String"">DR - Deed Records</Data></Cell>
    <Cell ss:MergeAcross=""1"" ss:StyleID=""s49""><Data ss:Type=""String"">ORRI - Overridging Royalty Interest</Data></Cell>
    <Cell ss:StyleID=""s54""/>
    <Cell ss:StyleID=""s55""/>
   </Row>
   <Row>
    <Cell ss:MergeAcross=""3"" ss:StyleID=""s57""><Data ss:Type=""String"">DT - Deed of Trust</Data></Cell>
    <Cell ss:MergeAcross=""1"" ss:StyleID=""s58""><Data ss:Type=""String"">PAOGL - Partial Assignment Oil, Gas and Mineral Lease</Data></Cell>
    <Cell ss:StyleID=""s59""/>
    <Cell ss:StyleID=""s60""/>
   </Row>
  </Table>
  <WorksheetOptions xmlns=""urn:schemas-microsoft-com:office:excel"">
   <PageSetup>
    <Layout x:Orientation=""Landscape"" x:CenterHorizontal=""1""/>
    <Header x:Margin=""0.5""/>
    <Footer x:Margin=""0.25""
     x:Data=""Prepared by TrueTract for TRACT, Inc.""/>
    <PageMargins x:Bottom=""0.5"" x:Left=""0.5""
     x:Right=""0.5"" x:Top=""0.5""/>
   </PageSetup>
   <ProtectObjects>True</ProtectObjects>
   <ProtectScenarios>True</ProtectScenarios>
  </WorksheetOptions>
 </Worksheet>
";

        private static string EXCEL_XML_TAB_SHEET_HEADER = @"
   <Row>
    <Cell ss:MergeAcross=""7"" ss:StyleID=""s24"">
     <Data ss:Type=""String"">{0}</Data>
    </Cell>
   </Row>
   <Row>
    <Cell ss:MergeAcross=""7"" ss:StyleID=""s26"">
     <Data ss:Type=""String"">{1}</Data>
    </Cell>
   </Row>
   <Row>
    <Cell ss:MergeAcross=""7"" ss:StyleID=""s24"">
     <Data ss:Type=""String"">{2}</Data>
    </Cell>
   </Row>
   <Row>
    <Cell ss:MergeAcross=""7"" ss:StyleID=""s26""/>
   </Row>
   <Row>
    <Cell ss:MergeAcross=""7"" ss:StyleID=""s28"">
     <Data ss:Type=""String"">{3}</Data>
    </Cell>
   </Row>
   <Row>
    <Cell ss:MergeAcross=""7"" ss:StyleID=""s31"">
     <Data ss:Type=""String"">{4}</Data>
    </Cell>
   </Row>
   <Row>
    <Cell ss:MergeAcross=""7"" ss:StyleID=""s26""/>
   </Row>
   <Row>
    <Cell ss:MergeAcross=""7"" ss:StyleID=""s33""><Data ss:Type=""String"">See last page for Abbreviations and Comments.</Data></Cell>
   </Row>

   <Row ss:AutoFitHeight=""1"">
    <Cell ss:StyleID=""s34""><Data ss:Type=""String"">NO.</Data></Cell>
    <Cell ss:StyleID=""s35""><Data ss:Type=""String"">INSTRUMENT</Data></Cell>
    <Cell ss:StyleID=""s35""><Data ss:Type=""String"">D.O.D.</Data></Cell>
    <Cell ss:StyleID=""s35""><Data ss:Type=""String"">D.O.R.</Data></Cell>
    <Cell ss:StyleID=""s35""><Data ss:Type=""String"">GRANTOR</Data></Cell>
    <Cell ss:StyleID=""s35""><Data ss:Type=""String"">GRANTEE</Data></Cell>
    <Cell ss:StyleID=""s35""><Data ss:Type=""String"">DESCRIPTION</Data></Cell>
    <Cell ss:StyleID=""s37""><Data ss:Type=""String"">REMARKS</Data></Cell>
   </Row>
";

        private static string EXCEL_XML_TAB_ENTRY_ROW = @"
   <Row ss:AutoFitHeight=""1"">
    <Cell ss:StyleID=""s38""><Data ss:Type=""String"">{0}</Data></Cell>
    <Cell ss:StyleID=""s36""><Data ss:Type=""String"">{1}</Data></Cell>
    <Cell ss:StyleID=""s40""><Data ss:Type=""DateTime"">{2}</Data></Cell>
    <Cell ss:StyleID=""s40""><Data ss:Type=""DateTime"">{3}</Data></Cell>
    <Cell ss:StyleID=""s39""><Data ss:Type=""String"">{4}</Data></Cell>
    <Cell ss:StyleID=""s39""><Data ss:Type=""String"">{5}</Data></Cell>
    <Cell ss:StyleID=""s39""><Data ss:Type=""String"">{6}</Data></Cell>
    <Cell ss:StyleID=""s41""><Data ss:Type=""String"">{7}</Data></Cell>
   </Row>
";

        #endregion        

        public static string ConvertTabToExcelXml(ProjectTabInfo tab)
        {
            string result = "";
            
            ProjectTabDocumentInfo primaryEntry = tab.getActiveTabDocument();

            result += EXCEL_XML_BOOK_BEGIN;
            result += EXCEL_XML_BOOK_STYLES;
            result += string.Format(EXCEL_XML_TAB_SHEET_BEGIN, xmlReplace(tab.Name));
            result += string.Format(
                EXCEL_XML_TAB_SHEET_HEADER,
                xmlReplace(tab.Name),
                "Surface Tiltle Report",
                xmlReplace(primaryEntry.DocumentRef.Seller.AsNamed),
                xmlReplace(primaryEntry.Description),
                xmlReplace(primaryEntry.Remarks));
            
            for(int i = 0; i < tab.Documents.Count; i++)
            {
                ProjectTabDocumentInfo entry = tab.Documents[i];
                
                result += string.Format(EXCEL_XML_TAB_ENTRY_ROW, 
                                        xmlReplace(getTractsField(entry)), 
                                        xmlReplace(getInstrumentField(entry.DocumentRef)),
                                        entry.DocumentRef.DateSignedYear + "-"
                                            + ((entry.DocumentRef.DateSignedMonth < 10) ? "0" + entry.DocumentRef.DateSignedMonth.ToString() : entry.DocumentRef.DateSignedMonth.ToString()) + "-"
                                            + ((entry.DocumentRef.DateSignedDay < 10) ? "0" + entry.DocumentRef.DateSignedDay.ToString() : entry.DocumentRef.DateSignedDay.ToString()),
                                        entry.DocumentRef.DateFiledYear + "-" 
                                            + ((entry.DocumentRef.DateFiledMonth < 10) ? "0" + entry.DocumentRef.DateFiledMonth.ToString() : entry.DocumentRef.DateFiledMonth.ToString()) + "-"
                                            + ((entry.DocumentRef.DateFiledDay < 10) ? "0" + entry.DocumentRef.DateFiledDay.ToString() : entry.DocumentRef.DateFiledDay.ToString()), 
                                        xmlReplace(entry.DocumentRef.Buyer.AsNamed),
                                        xmlReplace(entry.DocumentRef.Seller.AsNamed),
                                        xmlReplace(entry.Description),
                                        xmlReplace(entry.Remarks));
            }

            result += EXCEL_XML_TAB_SHEET_END;
            result += EXCEL_XML_BOOK_END;

            return result;
        }

        private static string getTractsField(ProjectTabDocumentInfo entry)
        {
            string result = "";
            
            foreach (ProjectTabDocumentTractInfo tabTract in entry.Tracts)
            {
                result += tabTract.TractRef.RefName;
                result += "\t";
            }

            return result;
        }

        private static string getInstrumentField(DocumentInfo doc)
        {
            string result = "";

            if (doc.DocumentNo != null && doc.DocumentNo.Length > 0)
            {
                result += doc.DocumentNo;
                result += "\t";
            } else if (doc.Volume != null && doc.Volume.Length > 0 && doc.Page != null && doc.Page.Length > 0)
            {
                result += (doc.Volume + "/" + doc.Page + "\t");
            }

            result += doc.DocTypeName;

            return result;
        }

        private static string xmlReplace(string str)
        {
            return str.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;").Replace("\t", "&#10;");
        }

    }
}
