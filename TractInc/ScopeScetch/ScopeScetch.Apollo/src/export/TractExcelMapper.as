package src.export
{
	import src.deedplotter.domain.Tract;
	import src.deedplotter.domain.TractCall;import src.deedplotter.domain.TractTextObject;
	
	public class TractExcelMapper
	{
		
		private static const EXCEL_FILE_HEADER:String = 
							'<?xml version="1.0"?>' +
							'\n<?mso-application progid="Excel.Sheet"?>' +
							'\n<Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet"' +
							'\n xmlns:o="urn:schemas-microsoft-com:office:office"' +
							'\n xmlns:x="urn:schemas-microsoft-com:office:excel"' +
							'\n xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"' +
							'\n xmlns:html="http://www.w3.org/TR/REC-html40">' +
							'\n <DocumentProperties xmlns="urn:schemas-microsoft-com:office:office">' +
							'\n  <Author>ScopeScetch</Author>' +
							'\n  <LastAuthor>ScopeScetch</LastAuthor>' +
							'\n  <Company>TractInc</Company>' +
							'\n  <Version>pre 1.00</Version>' +
							'\n </DocumentProperties>' +
							'\n <ExcelWorkbook xmlns="urn:schemas-microsoft-com:office:excel">' +
							'\n  <ProtectStructure>False</ProtectStructure>' +
							'\n  <ProtectWindows>False</ProtectWindows>' +
							'\n </ExcelWorkbook>';
							
		private static const TRACT_SHEET_HEADER:String = 
							'\n <Worksheet ss:Name="MAIN INFO">' +
							'\n  <Table>' +
							'\n   <Row>' +
							'\n    <Cell><Data ss:Type="String">DESCRIPTION</Data></Cell>' +
							'\n    <Cell><Data ss:Type="String">EASTING</Data></Cell>' +
							'\n    <Cell><Data ss:Type="String">NORTHING</Data></Cell>' +
							'\n    <Cell><Data ss:Type="String">CREATED BY</Data></Cell>' +
							'\n   </Row>';

		private static const CALLS_SHEET_HEADER:String = 
							'\n <Worksheet ss:Name="CALLS">' +
							'\n  <Table>' +
							'\n   <Row>' +
							'\n    <Cell><Data ss:Type="String">CALL DB VALUE</Data></Cell>' +
							'\n    <Cell><Data ss:Type="String">TYPE</Data></Cell>' +
							'\n    <Cell><Data ss:Type="String">ORDER</Data></Cell>' +
							'\n    <Cell><Data ss:Type="String">CREATED BY MOUSE</Data></Cell>' +
							'\n   </Row>';
		
		private static const TEXT_OBJECTS_SHEET_HEADER:String = 
							'\n <Worksheet ss:Name="TEXT OBJECTS">' +
							'\n  <Table>' +
							'\n   <Row>' +
							'\n    <Cell><Data ss:Type="String">TEXT</Data></Cell>' +
							'\n    <Cell><Data ss:Type="String">EASTING</Data></Cell>' +
							'\n    <Cell><Data ss:Type="String">NORTHING</Data></Cell>' +
							'\n    <Cell><Data ss:Type="String">ROTATION</Data></Cell>' +
							'\n   </Row>';

		private static const SHEET_FOOTER:String = 
							'\n   </Table>' +
							'\n  <WorksheetOptions xmlns="urn:schemas-microsoft-com:office:excel">' +
							'\n   <ProtectObjects>False</ProtectObjects>' +
							'\n   <ProtectScenarios>False</ProtectScenarios>' +
							'\n  </WorksheetOptions>' +
							'\n </Worksheet>';

		private static const BOOK_FOOTER:String = '\n</Workbook>';

		public static function getExcelXml(tract:Tract):String {
			
			var result:String = "";
			
			result += EXCEL_FILE_HEADER;
			result += TRACT_SHEET_HEADER;
			result += tractSection(tract);
			result += SHEET_FOOTER;
			
			if (tract.Calls.length > 0) {
				result += CALLS_SHEET_HEADER;
				for each (var call:TractCall in tract.Calls) {
					result += TractCallExcelMapper.getExcelXml(call);
				}
				result += SHEET_FOOTER;
			}
			
			if (tract.TextObjects.length > 0) {
				result += TEXT_OBJECTS_SHEET_HEADER;
				for each (var textObject:TractTextObject in tract.TextObjects) {
					result += TractTOExcelMapper.getExcelXml(textObject);
				}
				result += SHEET_FOOTER;
			}
			
			result += BOOK_FOOTER;

			return result;
		
		}
		
		private static function tractSection(tract:Tract):String {
			
			var result:String = "";
			
			result += '\n   <Row>';
			result += '\n    <Cell><Data ss:Type="String">';
			result += tract.Description;
			result += '</Data></Cell>';
			result += '\n    <Cell><Data ss:Type="Number">';
			result += tract.Easting.toString();
			result += '</Data></Cell>';
			result += '\n    <Cell><Data ss:Type="Number">';
			result += tract.Northing.toString();
			result += '</Data></Cell>';
			result += '\n    <Cell><Data ss:Type="Number">';
			result += tract.CreatedBy.toString();
			result += '</Data></Cell>';
			result += '\n   </Row>';
			
			return result;

		}
		
	}
}