package com.ebs.eroof.control
{
	import com.ebs.eroof.model.wrapper.CorePhoto;
	import com.ebs.eroof.model.wrapper.Expenditure;
	import com.ebs.eroof.model.wrapper.Facility;
	import com.ebs.eroof.model.wrapper.Inspection;
	import com.ebs.eroof.model.wrapper.Layer;
	import com.ebs.eroof.model.wrapper.Section;
	
	import mx.formatters.CurrencyFormatter;
	import mx.formatters.DateFormatter;
	import mx.formatters.NumberFormatter;
	
	public class FacilityReportBuilder
    {
    	private static var cf:CurrencyFormatter = new CurrencyFormatter();
    	private static var sqFtF:NumberFormatter = new NumberFormatter();
    	
        public static function getRoofConditionReportContent(source:Facility):String 
        {
            var facility:Facility = source;

            var result:String = "";

            result += getHeader(facility);
            result += getTitlePage(facility);
            result += getFacilityPages(facility);
            result += getFooter(facility);

            return result;
        }
        
        private static function get currentDateStr():String 
        {
        	return getDateStr(new Date());
        }
        
        private static function getDateStr(value:Date):String 
        {
        	var df:DateFormatter = new DateFormatter();
        	df.formatString = "MMMM D, YYYY";

        	return df.format(value);
        }
        
        private static function getFoMultilineContent(value:String):String 
        {
        	var result:String = "";
        	
        	var lines:Array = value.split("\n");
        	for each (var line:String in lines)
        	{
        		result += "<fo:block>";
        		result += line;
        		result += "</fo:block>";
        	}
        	
        	return result;
        }

        private static function getHeader(facility:Facility):String 
        {
			var result:String = "";			

			result += " <fo:root xmlns:fo=\"http://www.w3.org/1999/XSL/Format\" font-size=\"12pt\" font-weight=\"bold\" font-family=\"sans-serif\"> ";
			result += " <fo:layout-master-set> ";
			result += " <fo:simple-page-master margin-right=\"9.75mm\" margin-left=\"9.75mm\" margin-bottom=\"11.2mm\" margin-top=\"11.2mm\" page-height=\"279.4mm\" page-width=\"215.5mm\" master-name=\"title\"> ";
			result += " <fo:region-body /> ";
			result += " </fo:simple-page-master> ";
			result += " <fo:simple-page-master margin-right=\"9.75mm\" margin-left=\"9.75mm\" margin-bottom=\"8.2mm\" margin-top=\"8.2mm\" page-height=\"279.4mm\" page-width=\"215.5mm\" master-name=\"main\"> ";
			result += " <fo:region-body margin-bottom=\"20mm\" margin-top=\"20mm\"/> ";
			result += " <fo:region-before extent=\"10mm\"/> ";
			result += " <fo:region-after extent=\"16mm\"/> ";
			result += " </fo:simple-page-master> ";
			result += " </fo:layout-master-set> ";
            
            return result;
        }

        private static function getFooter(facility:Facility):String 
        {
            var result:String = "";
            
            result += "</fo:root>";
            
            return result;
        }

        private static function getTitlePage(facility:Facility):String 
        {
			var result:String = "";			
			result += " <fo:page-sequence master-reference=\"title\" force-page-count=\"no-force\"> ";
			result += " <fo:flow flow-name=\"xsl-region-body\"> ";
			result += " <fo:block border=\"2pt solid #0000ff\" padding=\"2pt\"> ";
			result += " <fo:block border=\"1pt solid #0000ff\"> ";
			result += " <fo:block space-before=\"5mm\" text-align=\"center\"> ";
			result += " <fo:external-graphic height=\"28mm\" width=\"160mm\" ";
			result += " content-height=\"28mm\" ";
			result += " content-width=\"160mm\" ";
			result += " padding=\"0mm\" scaling=\"non-uniform\" ";
			if (facility.client.segment.company.consultantDTO)
				result += (" src=\"" + facility.client.segment.company.consultantDTO.ReportBanner.file + "\"/> ");
			else 
				result += (" src=\"defaultlogo.png\"/> ");
			result += " </fo:block> ";
			result += " <fo:block space-before=\"20mm\" text-align=\"center\" font-size=\"36pt\" color=\"#0000ff\"> ";
			result += " Roof Condition Report ";
			result += " </fo:block> ";
			result += " <fo:block space-before=\"15mm\" font-size=\"16pt\" margin-left=\"65mm\"> ";
			result += " <fo:block font-size=\"24pt\" color=\"#0000ff\"> ";
			result += " Prepared for: ";
			result += " </fo:block> ";
			result += " <fo:block space-before=\"8mm\"> ";
			result += facility.client.clientDTO.PrimaryContact;
			result += " </fo:block> ";
			result += " <fo:block> ";
			result += facility.facilityDTO.FacilityName;
			result += " </fo:block> ";
			result += " <fo:block font-weight=\"normal\"> ";
			result += getFoMultilineContent(facility.facilityDTO.Address);
			result += " <fo:block> ";
			result += facility.facilityDTO.City;
			result += ", ";
			result += facility.facilityDTO.Province;
			result += " </fo:block> ";
			result += " </fo:block> ";
			result += " </fo:block> ";
			result += " <fo:block space-before=\"30mm\"/> ";
			result += " <fo:table table-layout=\"fixed\" width=\"100%\"> ";
			result += " <fo:table-column column-width=\"proportional-column-width(1)\" /> ";
			result += " <fo:table-column column-width=\"10mm\" /> ";
			result += " <fo:table-column column-width=\"proportional-column-width(1)\" /> ";
			result += " <fo:table-body> ";
			result += " <fo:table-row> ";
			result += " <fo:table-cell> ";
			result += " <fo:block text-align=\"right\"> ";
			result += " <fo:external-graphic height=\"70mm\" width=\"80mm\" ";
			result += " content-height=\"70mm\" ";
			result += " content-width=\"80mm\" ";
			result += " scaling=\"non-uniform\" ";
			result += (" src=\"" + facility.facilityDTO.Photo.file + "\"/> ");
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell column-number=\"3\"> ";
			result += " <fo:block> ";
			result += " <fo:external-graphic height=\"70mm\" width=\"80mm\" ";
			result += " content-height=\"70mm\" ";
			result += " content-width=\"80mm\" ";
			result += " scaling=\"non-uniform\" ";
			result += (" src=\"" + facility.facilityDTO.Keyplan.file + "\"/> ");
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " </fo:table-row> ";
			result += " </fo:table-body> ";
			result += " </fo:table> ";
			result += " <fo:block space-before=\"5mm\" text-align=\"right\" font-size=\"14pt\" margin-right=\"8mm\"> ";
			result += " Date: ";
			result += currentDateStr;
			result += " </fo:block> ";
			result += " <fo:block space-before=\"9mm\"/> ";
			result += " </fo:block> ";
			result += " </fo:block> ";
			result += " </fo:flow> ";
			result += " </fo:page-sequence> ";
            
            return result;
        }
        
        private static function getFacilityPages(facility:Facility):String 
        {
        	sqFtF.precision = 0;
        	sqFtF.useThousandsSeparator = true;
        	
            var result:String = "";
            
			result += " <fo:page-sequence master-reference=\"main\" initial-page-number=\"1\"> ";
			result += " <fo:static-content flow-name=\"xsl-region-before\"> ";
			result += " <fo:block padding-bottom=\"1pt\" border-bottom=\"1pt solid #0000ff\"> ";
			result += " <fo:table table-layout=\"fixed\" width=\"100%\" border-bottom=\"2pt solid #0000ff\"> ";
			result += " <fo:table-column column-width=\"proportional-column-width(1)\" /> ";
			result += " <fo:table-column column-width=\"proportional-column-width(1)\" /> ";
			result += " <fo:table-column column-width=\"proportional-column-width(1)\" /> ";
			result += " <fo:table-body start-indent=\"0mm\" end-indent=\"0mm\" font-size=\"10pt\" font-weight=\"normal\"> ";
			result += " <fo:table-row> ";
			result += " <fo:table-cell> ";
			result += " <fo:block> ";
			result += " <fo:retrieve-marker retrieve-class-name=\"resultTopLeft\" ";
			result += " retrieve-boundary=\"page-sequence\" ";
			result += " retrieve-position=\"first-including-carryover\"/> ";
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell> ";
			result += " <fo:block text-align=\"center\"> ";
			result += " Facility: " + facility.name;
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell> ";
			result += " <fo:block text-align=\"right\"> ";
			result += " <fo:retrieve-marker retrieve-class-name=\"resultTopRight\" ";
			result += " retrieve-boundary=\"page-sequence\" ";
			result += " retrieve-position=\"first-including-carryover\"/> ";
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " </fo:table-row> ";
			result += " </fo:table-body> ";
			result += " </fo:table> ";
			result += " </fo:block> ";
			result += " </fo:static-content> ";
			result += " <fo:static-content flow-name=\"xsl-region-after\"> ";
			result += " <fo:block padding-top=\"1pt\" border-top=\"1pt solid #0000ff\"> ";
			result += " <fo:table table-layout=\"fixed\" width=\"100%\" border-top=\"2pt solid #0000ff\"> ";
			result += " <fo:table-column column-width=\"proportional-column-width(1)\" /> ";
			result += " <fo:table-column column-width=\"30mm\" /> ";
			result += " <fo:table-column column-width=\"proportional-column-width(1)\" /> ";
			result += " <fo:table-body start-indent=\"0mm\" end-indent=\"0mm\" font-size=\"10pt\" font-weight=\"normal\" vertical-align=\"middle\"> ";
			result += " <fo:table-row> ";
			result += " <fo:table-cell> ";
			result += " <fo:block> ";
			result += " <fo:external-graphic height=\"9mm\" width=\"50mm\" ";
			result += " content-height=\"9mm\" ";
			result += " content-width=\"50mm\" ";
			result += " padding=\"0mm\" scaling=\"non-uniform\" ";
			if (facility.client.segment.company.consultantDTO)
				result += (" src=\"" + facility.client.segment.company.consultantDTO.ReportBanner.file + "\"/> ");
			else 
				result += (" src=\"defaultlogo.png\"/> ");
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell> ";
			result += " <fo:block text-align=\"center\" vertical-align=\"middle\"> ";
			result += " Page <fo:page-number/> ";
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell> ";
			result += " <fo:block text-align=\"right\" vertical-align=\"middle\"> ";
			result += " Printed: ";
			result += currentDateStr;
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " </fo:table-row> ";
			result += " </fo:table-body> ";
			result += " </fo:table> ";
			result += " </fo:block> ";
			result += " </fo:static-content> ";
			result += " <fo:flow flow-name=\"xsl-region-body\"> ";
			result += "<!-- Main description --> ";
			result += " <!--fo:block--> ";
			result += " <fo:marker marker-class-name=\"resultTopLeft\"> ";
			result += " <fo:block font-size=\"10pt\" font-weight=\"normal\"> ";
			result += " Moisture Survey Report ";
			result += " </fo:block> ";
			result += " </fo:marker> ";
			result += " <fo:marker marker-class-name=\"resultTopRight\"> ";
			result += " <fo:block text-align=\"right\" font-size=\"10pt\" font-weight=\"normal\"> ";
			result += " Facility Summary ";
			result += " </fo:block> ";
			result += " </fo:marker> ";
			result += " <!--/fo:block--> ";
			result += " ";
			result += " <fo:table table-layout=\"fixed\" width=\"100%\"> ";
			result += " <fo:table-column column-width=\"40mm\"/> ";
			result += " <fo:table-column column-width=\"5mm\"/> ";
			result += " <fo:table-column column-width=\"75mm\"/> ";
			result += " <fo:table-column column-width=\"5mm\"/> ";
			result += " <fo:table-column column-width=\"70mm\"/> ";
			result += " <fo:table-body> ";
			result += " <fo:table-row> ";
			result += " <fo:table-cell> ";
			result += " <fo:block text-align=\"right\"> ";
			result += " Facility: ";
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell> ";
			result += " <fo:block/> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell> ";
			result += " <fo:block> ";
			result += facility.facilityDTO.FacilityName;
			result += " </fo:block> ";
			result += getFoMultilineContent(facility.facilityDTO.Address);
			result += " <fo:block> ";
			result += (facility.facilityDTO.City + ", " + facility.facilityDTO.Province);
			result += " </fo:block> ";
			result += " <fo:block space-before=\"10mm\" /> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell> ";
			result += " <fo:block/> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell number-rows-spanned=\"5\"> ";
			result += " <fo:block> ";
			result += " <fo:external-graphic height=\"52mm\" width=\"60mm\" ";
			result += " content-height=\"52mm\" ";
			result += " content-width=\"60mm\" ";
			result += (" src=\"" + facility.facilityDTO.Photo.file + "\"/> ");
			result += " </fo:block> ";
			result += " <fo:block> ";
			result += " <fo:external-graphic height=\"52mm\" width=\"60mm\" ";
			result += " content-height=\"52mm\" ";
			result += " content-width=\"60mm\" ";
			result += (" src=\"" + facility.facilityDTO.Keyplan.file + "\"/> ");
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " </fo:table-row> ";
			result += " <fo:table-row> ";
			result += " <fo:table-cell> ";
			result += " <fo:block text-align=\"right\"> ";
			result += " Contact Name: ";
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell column-number=\"3\"> ";
			result += " <fo:block> ";
			result += facility.facilityDTO.PrimaryContact;
			result += " </fo:block> ";
			result += " <fo:block space-before=\"10mm\" /> ";
			result += " </fo:table-cell> ";
			result += " </fo:table-row> ";
			result += " <fo:table-row> ";
			result += " <fo:table-cell> ";
			result += " <fo:block text-align=\"right\"> ";
			result += " Contact Telephone: ";
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell column-number=\"3\"> ";
			result += " <fo:block> ";
			result += facility.facilityDTO.Phone;
			result += " </fo:block> ";
			result += " <fo:block space-before=\"10mm\" /> ";
			result += " </fo:table-cell> ";
			result += " </fo:table-row> ";
			result += " <fo:table-row> ";
			result += " <fo:table-cell> ";
			result += " <fo:block text-align=\"right\"> ";
			result += " Type of Building: ";
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell column-number=\"3\"> ";
			result += " <fo:block> ";
			result += facility.facilityDTO.TypeOfBuilding;
			result += " </fo:block> ";
			result += " <fo:block space-before=\"10mm\" /> ";
			result += " </fo:table-cell> ";
			result += " </fo:table-row> ";
			result += " <fo:table-row> ";
			result += " <fo:table-cell> ";
			result += " <fo:block text-align=\"right\"> ";
			result += " Type of Neighborhood: ";
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell column-number=\"3\"> ";
			result += " <fo:block> ";
			result += facility.facilityDTO.Neighbourhood;
			result += " </fo:block> ";
			result += " <fo:block space-before=\"10mm\" /> ";
			result += " </fo:table-cell> ";
			result += " </fo:table-row> ";
			result += " </fo:table-body> ";
			result += " </fo:table> ";
			
			if (facility.sectionsCollection.length > 0) 
			{
				result += " <fo:table table-layout=\"fixed\" width=\"195mm\" ";
				result += " border-collapse=\"separate\"> ";
				result += " <fo:table-column column-width=\"25mm\"/> ";
				result += " <fo:table-column column-width=\"33mm\"/> ";
				result += " <fo:table-column column-width=\"20mm\"/> ";
				result += " <fo:table-column column-width=\"18mm\"/> ";
				result += " <fo:table-column column-width=\"45mm\"/> ";
				result += " <fo:table-column column-width=\"24mm\"/> ";
				result += " <fo:table-column column-width=\"30mm\"/> ";
				result += " <fo:table-header text-align=\"center\" vertical-align=\"top\" line-height=\"1.6em\"> ";
				result += " <fo:table-row background-color=\"#000080\"> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\" number-columns-spanned=\"7\"> ";
				result += " <fo:block> ";
				result += " List of Roof Sections ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " </fo:table-row> ";
				result += " <fo:table-row background-color=\"#c0c0c0\"> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block> ";
				result += " Photo ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block> ";
				result += " Section / Name / Year installed ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block> ";
				result += " Sq. Ft. ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block> ";
				result += " Height ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block> ";
				result += " System Type ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block> ";
				result += " Condition Index ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block> ";
				result += " Estimated Replacement Value ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " </fo:table-row> ";
				result += " </fo:table-header> ";
				result += " <fo:table-body text-align=\"center\" line-height=\"1.6em\"> ";

				var section:Section;	
				for each (section in facility.sectionsCollection) 
				{
					result += " <fo:table-row> ";
					result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
					result += " <fo:block margin=\"3mm\"> ";
					result += " <fo:external-graphic height=\"18mm\" width=\"18mm\" ";
					result += " content-height=\"18mm\" ";
					result += " content-width=\"18mm\" ";
					result += (" src=\"" + section.sectionDTO.Photo.file + "\"/> ");
					result += " </fo:block> ";
					result += " </fo:table-cell> ";
					result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
					result += " <fo:block> ";
					result += section.sectionDTO.Designation;
					result += " </fo:block> ";
					result += " <fo:block> ";
					result += section.sectionDTO.RoofName;
					result += " </fo:block> ";
					result += " <fo:block> ";
					result += section.sectionDTO.YearInstalled.toString();
					result += " </fo:block> ";
					result += " </fo:table-cell> ";
					result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
					result += " <fo:block text-align=\"right\"> ";
					result += sqFtF.format(section.sectionDTO.SqFt);
					result += " </fo:block> ";
					result += " </fo:table-cell> ";
					result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
					result += " <fo:block text-align=\"right\"> ";
					result += (section.sectionDTO.Height + "ft");
					result += " </fo:block> ";
					result += " </fo:table-cell> ";
					result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
					result += " <fo:block> ";
					result += section.sectionDTO.RoofSystem;
					result += " </fo:block> ";
					result += " </fo:table-cell> ";
					result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
					result += " <fo:block> ";
					result += section.sectionDTO.ConditionIndex;
					result += " </fo:block> ";
					result += " </fo:table-cell> ";
					result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
					result += " <fo:block text-align=\"right\"> ";
					result += cf.format(section.sectionDTO.EstCost);
					result += " </fo:block> ";
					result += " </fo:table-cell> ";
					result += " </fo:table-row> ";
				}
				result += " <fo:table-row background-color=\"#c0c0c0\"> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block/> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block/> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block text-align=\"right\"> ";
				result += sqFtF.format(facility.facilityDTO.TotalSqFt);
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block/> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block/> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block/> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block text-align=\"right\"> ";
				result += cf.format(facility.facilityDTO.TotalValue);
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " </fo:table-row> ";
				result += " </fo:table-body> ";
				result += " </fo:table> ";
				
	            for each (section in facility.sectionsCollection) 
	            {
	            	result += getSectionPages(section);
	            }
			}
			
			result += " </fo:flow> ";
			result += " </fo:page-sequence> ";
            
			return result;
        }

        private static function getSectionPages(section:Section):String 
        {
        	sqFtF.precision = 0;
        	sqFtF.useThousandsSeparator = true;
        	
            var result:String = "";
            
			result += " <fo:block break-before=\"page\" font-size=\"10pt\" font-weight=\"normal\"> ";
			result += " <fo:marker marker-class-name=\"resultTopLeft\"> ";
			result += " <fo:block> ";
			result += " Condition Report ";
			result += " </fo:block> ";
			result += " </fo:marker> ";
			result += " <fo:marker marker-class-name=\"resultTopRight\"> ";
			result += " <fo:block text-align=\"right\"> ";
			result += (" Roof Area " + section.sectionDTO.Designation);
			result += " </fo:block> ";
			result += " </fo:marker> ";
			result += " </fo:block> ";
			result += " <fo:table table-layout=\"fixed\" width=\"100%\"> ";
			result += " <fo:table-column column-width=\"45mm\"/> ";
			result += " <fo:table-column column-width=\"5mm\"/> ";
			result += " <fo:table-column column-width=\"65mm\"/> ";
			result += " <fo:table-column column-width=\"5mm\"/> ";
			result += " <fo:table-column column-width=\"75mm\"/> ";
			result += " <fo:table-body> ";
			result += " <fo:table-row> ";
			result += " <fo:table-cell> ";
			result += " <fo:block text-align=\"right\"> ";
			result += " Designation: ";
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell> ";
			result += " <fo:block/> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell> ";
			result += " <fo:block> ";
			result += section.sectionDTO.Designation;
			result += " </fo:block> ";
			result += " <fo:block space-before=\"5mm\" /> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell> ";
			result += " <fo:block/> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell number-rows-spanned=\"14\"> ";
			result += " <fo:block> ";
			result += " <fo:external-graphic height=\"52mm\" width=\"60mm\" ";
			result += " content-height=\"52mm\" ";
			result += " content-width=\"60mm\" ";
			result += (" src=\"" + section.sectionDTO.Photo.file + "\"/> ");
			result += " </fo:block> ";
			result += " <fo:block> ";
			result += " <fo:external-graphic height=\"52mm\" width=\"60mm\" ";
			result += " content-height=\"52mm\" ";
			result += " content-width=\"60mm\" ";
			result += (" src=\"" + "" + "\"/> ");
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " </fo:table-row> ";
			result += " <fo:table-row> ";
			result += " <fo:table-cell> ";
			result += " <fo:block text-align=\"right\"> ";
			result += " Roof Name: ";
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell column-number=\"3\"> ";
			result += " <fo:block> ";
			result += section.sectionDTO.RoofName;
			result += " </fo:block> ";
			result += " <fo:block space-before=\"5mm\" /> ";
			result += " </fo:table-cell> ";
			result += " </fo:table-row> ";
			result += " <fo:table-row> ";
			result += " <fo:table-cell> ";
			result += " <fo:block text-align=\"right\"> ";
			result += " Roof Size: ";
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell column-number=\"3\"> ";
			result += " <fo:block> ";
			result += ( sqFtF.format(section.sectionDTO.SqFt) + " sq. ft. ");
			result += " </fo:block> ";
			result += " <fo:block space-before=\"5mm\" /> ";
			result += " </fo:table-cell> ";
			result += " </fo:table-row> ";
			result += " <fo:table-row> ";
			result += " <fo:table-cell> ";
			result += " <fo:block text-align=\"right\"> ";
			result += " Est. Replacement Cost: ";
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell column-number=\"3\"> ";
			result += " <fo:block> ";
			result += (cf.format(section.sectionDTO.EstCost));
			result += " </fo:block> ";
			result += " <fo:block space-before=\"5mm\" /> ";
			result += " </fo:table-cell> ";
			result += " </fo:table-row> ";
			result += " <fo:table-row> ";
			result += " <fo:table-cell> ";
			result += " <fo:block text-align=\"right\"> ";
			result += " Existing System Type: ";
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell column-number=\"3\"> ";
			result += " <fo:block> ";
			result += section.sectionDTO.RoofSystem;
			result += " </fo:block> ";
			result += " <fo:block space-before=\"5mm\" /> ";
			result += " </fo:table-cell> ";
			result += " </fo:table-row> ";
			result += " <fo:table-row> ";
			result += " <fo:table-cell> ";
			result += " <fo:block text-align=\"right\"> ";
			result += " Year Installed: ";
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell column-number=\"3\"> ";
			result += " <fo:block> ";
			result += section.sectionDTO.YearInstalled.toString();
			result += " </fo:block> ";
			result += " <fo:block space-before=\"5mm\" /> ";
			result += " </fo:table-cell> ";
			result += " </fo:table-row> ";
			result += " <fo:table-row> ";
			result += " <fo:table-cell> ";
			result += " <fo:block text-align=\"right\"> ";
			result += " Height: ";
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell column-number=\"3\"> ";
			result += " <fo:block> ";
			result += (section.sectionDTO.Height.toString() + " feet ");
			result += " </fo:block> ";
			result += " <fo:block space-before=\"5mm\" /> ";
			result += " </fo:table-cell> ";
			result += " </fo:table-row> ";
			result += " <fo:table-row> ";
			result += " <fo:table-cell> ";
			result += " <fo:block text-align=\"right\"> ";
			result += " Slope: ";
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell column-number=\"3\"> ";
			result += " <fo:block> ";
			result += section.sectionDTO.Slope;
			result += " </fo:block> ";
			result += " <fo:block space-before=\"5mm\" /> ";
			result += " </fo:table-cell> ";
			result += " </fo:table-row> ";
			result += " <fo:table-row> ";
			result += " <fo:table-cell> ";
			result += " <fo:block text-align=\"right\"> ";
			result += " Interior Sensitivity: ";
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell column-number=\"3\"> ";
			result += " <fo:block> ";
			result += section.sectionDTO.InteriorSensitivity;
			result += " </fo:block> ";
			result += " <fo:block space-before=\"5mm\" /> ";
			result += " </fo:table-cell> ";
			result += " </fo:table-row> ";
			result += " <fo:table-row> ";
			result += " <fo:table-cell> ";
			result += " <fo:block text-align=\"right\"> ";
			result += " Condition Index: ";
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell column-number=\"3\"> ";
			result += getFoMultilineContent(section.sectionDTO.ConditionIndex);
			result += " <fo:block space-before=\"5mm\" /> ";
			result += " </fo:table-cell> ";
			result += " </fo:table-row> ";
			result += " <fo:table-row> ";
			result += " <fo:table-cell> ";
			result += " <fo:block text-align=\"right\"> ";
			result += " Drainage: ";
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell column-number=\"3\"> ";
			result += " <fo:block> ";
			result += section.sectionDTO.Drainage;
			result += " </fo:block> ";
			result += " <fo:block space-before=\"5mm\" /> ";
			result += " </fo:table-cell> ";
			result += " </fo:table-row> ";
			result += " <fo:table-row> ";
			result += " <fo:table-cell> ";
			result += " <fo:block text-align=\"right\"> ";
			result += " Currently Leaking? ";
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell column-number=\"3\"> ";
			result += getFoMultilineContent(section.sectionDTO.CurrentlyLeaking);
			result += " <fo:block space-before=\"5mm\" /> ";
			result += " </fo:table-cell> ";
			result += " </fo:table-row> ";
			result += " <fo:table-row> ";
			result += " <fo:table-cell> ";
			result += " <fo:block text-align=\"right\"> ";
			result += " History of Leaking? ";
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell column-number=\"3\"> ";
			result += getFoMultilineContent(section.sectionDTO.HistoryOfLeaking);
			result += " <fo:block space-before=\"5mm\" /> ";
			result += " </fo:table-cell> ";
			result += " </fo:table-row> ";
			result += " <fo:table-row> ";
			result += " <fo:table-cell> ";
			result += " <fo:block text-align=\"right\"> ";
			result += " Roof Condition Summary: ";
			result += " </fo:block> ";
			result += " </fo:table-cell> ";
			result += " <fo:table-cell column-number=\"3\"> ";
			result += " <fo:block number-columns-spanned=\"3\"> ";
			result += getFoMultilineContent(section.sectionDTO.ConditionDetails);
			result += " </fo:block> ";
			result += " <fo:block space-before=\"5mm\" /> ";
			result += " </fo:table-cell> ";
			result += " </fo:table-row> ";
			result += " </fo:table-body> ";
			result += " </fo:table> ";

			result += " <fo:block space-before=\"5mm\"/> ";
			result += " <fo:table table-layout=\"fixed\" width=\"100%\"> ";
			result += " <fo:table-column column-width=\"proportional-column-width(1)\" /> ";
			result += " <fo:table-column column-width=\"180mm\" /> ";
			result += " <fo:table-column column-width=\"proportional-column-width(1)\" /> ";
			result += " <fo:table-body> ";

			result += " <fo:table-row> ";
			result += " <fo:table-cell column-number=\"2\"> ";
			result += " <fo:block border=\"0.1mm solid #000080\"> ";
			result += " <fo:block text-align=\"center\" background-color=\"#000080\"> ";
			result += " <fo:block padding=\"1mm\"> ";
			result += " Overall Core Condition ";
			result += " </fo:block> ";
			result += " </fo:block> ";
			result += getFoMultilineContent(section.sectionDTO.OverallCoreCondition);
			result += " </fo:block> ";
			
			if (section.corePhotosCollection.length > 0) 
			{
				result += " <fo:block space-before=\"10mm\"/> ";
				result += " ";
				result += " <fo:table table-layout=\"fixed\" width=\"180mm\" border-collapse=\"separate\"> ";
				result += " <fo:table-column column-width=\"30mm\"/> ";
				result += " <fo:table-column column-width=\"50mm\"/> ";
				result += " <fo:table-column column-width=\"100mm\"/> ";
				result += " <fo:table-header text-align=\"center\" vertical-align=\"top\" line-height=\"1.6em\"> ";
				result += " <fo:table-row background-color=\"#000080\"> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\" number-columns-spanned=\"3\"> ";
				result += " <fo:block> ";
				result += " Core photos ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " </fo:table-row> ";
				result += " <fo:table-row background-color=\"#c0c0c0\"> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block> ";
				result += " Photo ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block> ";
				result += " Date ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block> ";
				result += " Description ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " </fo:table-row> ";
				result += " </fo:table-header> ";
				result += " <fo:table-body text-align=\"center\" vertical-align=\"middle\"> ";
				
				for each (var corePhoto:CorePhoto in section.corePhotosCollection) 
				{
					result += " <fo:table-row> ";
					result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
					result += " <fo:block margin=\"3mm\"> ";
					result += " <fo:external-graphic height=\"18mm\" width=\"18mm\" ";
					result += " content-height=\"18mm\" ";
					result += " content-width=\"18mm\" ";
					result += " padding=\"0mm\" scaling=\"non-uniform\" ";
					result += (" src=\"" + corePhoto.corePhotoDTO.Photo.file + "\"/> ");
					result += " </fo:block> ";
					result += " </fo:table-cell> ";
					result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
					result += " <fo:block> ";
					result += getDateStr(corePhoto.corePhotoDTO.PhotoDate);
					result += " </fo:block> ";
					result += " </fo:table-cell> ";
					result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
					result += " <fo:block> ";
					result += getFoMultilineContent(corePhoto.corePhotoDTO.Description);
					result += " </fo:block> ";
					result += " </fo:table-cell> ";
					result += " </fo:table-row> ";
				} 
				
				result += " </fo:table-body> ";
				result += " </fo:table> ";
			}
		
			if (section.layersCollection.length > 0) 
			{
				result += " <fo:block space-before=\"10mm\"/> ";
				result += " <fo:table table-layout=\"fixed\" width=\"180mm\" border-collapse=\"separate\"> ";
				result += " <fo:table-column column-width=\"60mm\"/> ";
				result += " <fo:table-column column-width=\"60mm\"/> ";
				result += " <fo:table-column column-width=\"60mm\"/> ";
				result += " <fo:table-header text-align=\"center\" vertical-align=\"top\" line-height=\"1.6em\"> ";
				result += " <fo:table-row background-color=\"#000080\"> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\" number-columns-spanned=\"3\"> ";
				result += " <fo:block> ";
				result += " Existing Roof System Construction ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " </fo:table-row> ";
				result += " <fo:table-row background-color=\"#c0c0c0\"> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block> ";
				result += " Layer Type ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block> ";
				result += " Description ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block> ";
				result += " Method of Attachment ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " </fo:table-row> ";
				result += " </fo:table-header> ";
				result += " <fo:table-body line-height=\"1.6em\"> ";
				
				for each (var layer:Layer in section.layersCollection) 
				{
					result += " <fo:table-row> ";
					result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
					result += " <fo:block> ";
					result += layer.layerDTO.LayerType;
					result += " </fo:block> ";
					result += " </fo:table-cell> ";
					result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
					result += " <fo:block> ";
					result += getFoMultilineContent(layer.layerDTO.Description);
					result += " </fo:block> ";
					result += " </fo:table-cell> ";
					result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
					result += " <fo:block> ";
					result += layer.layerDTO.Attachment;
					result += " </fo:block> ";
					result += " </fo:table-cell> ";
					result += " </fo:table-row> ";
				}
				result += " </fo:table-body> ";
				result += " </fo:table> ";
			}

			if (section.inspectionsCollection.length > 0) 
			{
				result += " <fo:block space-before=\"52mm\"/> ";
				result += " <fo:table table-layout=\"fixed\" width=\"180mm\" border-collapse=\"separate\"> ";
				result += " <fo:table-column column-width=\"25mm\"/> ";
				result += " <fo:table-column column-width=\"40mm\"/> ";
				result += " <fo:table-column column-width=\"60mm\"/> ";
				result += " <fo:table-column column-width=\"55mm\"/> ";
				result += " <fo:table-header text-align=\"center\" vertical-align=\"top\" line-height=\"1.6em\"> ";
				result += " <fo:table-row background-color=\"#000080\"> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\" number-columns-spanned=\"4\"> ";
				result += " <fo:block> ";
				result += " Overall Roof Inspection Assessments ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " </fo:table-row> ";
				result += " <fo:table-row background-color=\"#c0c0c0\"> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block> ";
				result += " Date ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block> ";
				result += " Inspection Type ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block> ";
				result += " Inspecting Company ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block> ";
				result += " Inspector Name ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " </fo:table-row> ";
				result += " </fo:table-header> ";
				result += " <fo:table-body> ";

				for each (var inspection:Inspection in section.inspectionsCollection) 
				{
					result += " <fo:table-row background-color=\"#c0c0c0\" line-height=\"1.6em\"> ";
					result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
					result += " <fo:block> ";
					result += getDateStr(inspection.inspectionDTO.InspectionDate);
					result += " </fo:block> ";
					result += " </fo:table-cell> ";
					result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
					result += " <fo:block> ";
					result += inspection.inspectionDTO.InspectionType;
					result += " </fo:block> ";
					result += " </fo:table-cell> ";
					result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
					result += " <fo:block> ";
					result += inspection.inspectionDTO.InspectorCompany;
					result += " </fo:block> ";
					result += " </fo:table-cell> ";
					result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
					result += " <fo:block> ";
					result += inspection.inspectionDTO.InspectorName;
					result += " </fo:block> ";
					result += " </fo:table-cell> ";
					result += " </fo:table-row> ";
					result += " <fo:table-row> ";
					result += " <fo:table-cell border=\"0.1mm solid #000080\" number-columns-spanned=\"4\"> ";
					result += " <fo:block> ";
					result += getFoMultilineContent(inspection.inspectionDTO.Assessment);
					result += " </fo:block> ";
					result += " </fo:table-cell> ";
					result += " </fo:table-row> ";
				}
				result += " </fo:table-body> ";
				result += " </fo:table> ";
			}

			if (section.expendituresCollection.length > 0) 
			{
				result += " <fo:block space-before=\"15mm\"/> ";
				result += " <fo:table table-layout=\"fixed\" width=\"180mm\" border-collapse=\"separate\"> ";
				result += " <fo:table-column column-width=\"25mm\"/> ";
				result += " <fo:table-column column-width=\"40mm\"/> ";
				result += " <fo:table-column column-width=\"25mm\"/> ";
				result += " <fo:table-column column-width=\"30mm\"/> ";
				result += " <fo:table-column column-width=\"30mm\"/> ";
				result += " <fo:table-column column-width=\"30mm\"/> ";
				result += " <fo:table-header text-align=\"center\" vertical-align=\"top\" line-height=\"1.6em\"> ";
				result += " <fo:table-row background-color=\"#000080\"> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\" number-columns-spanned=\"6\"> ";
				result += " <fo:block> ";
				result += " Recommendations - Details ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " </fo:table-row> ";
				result += " <fo:table-row background-color=\"#c0c0c0\"> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block> ";
				result += " Budget Year ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block> ";
				result += " Type of Activity ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block> ";
				result += " Action Item? ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block> ";
				result += " Allocation ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block> ";
				result += " Urgency ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block>Budget $</fo:block> ";
				result += " </fo:table-cell> ";
				result += " </fo:table-row> ";
				result += " <fo:table-row background-color=\"#c0c0c0\"> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\" number-columns-spanned=\"6\"> ";
				result += " <fo:block text-align=\"left\"> ";
				result += " Details ";
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " </fo:table-row> ";
				result += " </fo:table-header> ";
				result += " <fo:table-body line-height=\"1.6em\"> ";

				var totalAmount:Number = 0;
				for each (var expenditure:Expenditure in section.expendituresCollection) 
				{
					result += " <fo:table-row> ";
					result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
					result += " <fo:block> ";
					result += expenditure.expenditureDTO.BudgetYear;
					result += " </fo:block> ";
					result += " </fo:table-cell> ";
					result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
					result += " <fo:block text-align=\"left\"> ";
					result += expenditure.expenditureDTO.TypeOfWork;
					result += " </fo:block> ";
					result += " </fo:table-cell> ";
					result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
					result += " <fo:block> ";
					result += expenditure.expenditureDTO.ActionItem;
					result += " </fo:block> ";
					result += " </fo:table-cell> ";
					result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
					result += " <fo:block> ";
					result += expenditure.expenditureDTO.Allocation;
					result += " </fo:block> ";
					result += " </fo:table-cell> ";
					result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
					result += " <fo:block> ";
					result += expenditure.expenditureDTO.Urgency;
					result += " </fo:block> ";
					result += " </fo:table-cell> ";
					result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
					result += " <fo:block text-align=\"right\"> ";
					result += cf.format(expenditure.expenditureDTO.Amount);
					result += " </fo:block> ";
					result += " </fo:table-cell> ";
					result += " </fo:table-row> ";
					result += " <fo:table-row> ";
					result += " <fo:table-cell border=\"0.1mm solid #000080\" number-columns-spanned=\"6\"> ";
					result += " <fo:block> ";
					result += getFoMultilineContent(expenditure.expenditureDTO.Description);
					result += " </fo:block> ";
					result += " </fo:table-cell> ";
					result += " </fo:table-row> ";
					
					totalAmount += expenditure.expenditureDTO.Amount;
				}
				
				result += " <fo:table-row background-color=\"#c0c0c0\"> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block/> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block/> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block/> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block/> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block/> ";
				result += " </fo:table-cell> ";
				result += " <fo:table-cell border=\"0.1mm solid #000080\"> ";
				result += " <fo:block text-align=\"right\"> ";
				result += cf.format(totalAmount);
				result += " </fo:block> ";
				result += " </fo:table-cell> ";
				result += " </fo:table-row> ";
				result += " </fo:table-body> ";
				result += " </fo:table> ";
			}
			
			result += " </fo:table-cell> ";
			result += " </fo:table-row> ";
			result += " </fo:table-body> ";
			result += " </fo:table> ";
            
            return result;
        }
    }
}
