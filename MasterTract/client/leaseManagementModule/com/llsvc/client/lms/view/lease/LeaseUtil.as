package com.llsvc.client.lms.view.lease
{
    import com.llsvc.domain.Lease;
    import com.llsvc.domain.LeaseTract;
    
    import mx.collections.ArrayCollection;
    
    public class LeaseUtil
    {
        public function LeaseUtil()
        {
        }
        
        public static function exportLeaseListToExcel(leases:ArrayCollection):String 
        {
            var result:String = "";
            
            if (leases == null || leases.length == 0)
                return result;
            
            result += getDocHeader();
            
            for each (var lease:Lease in leases) 
            {
                result += getLeaseExcelString(lease);
            }
            
            result += getDocFooter(leases);
            
            return result.replace("&", "&amp;");
        }
        
        private static function getLeaseExcelString(lease:Lease):String 
        {
            var result:String = "";
            
            if (lease != null) 
            {
                result += getLeaseHeader(lease);
                if (lease.tracts.length > 0) 
                {
                    result += getFirstTract(lease);
                    if (lease.tracts.length > 1) 
                    {
                        result += getTracts(lease);
                    }
                } else 
                {
                    result += getEmptyTract();
                }
                
                result += getLeaseFooter();
            }
            
            return result.replace("&", "&amp;");
        }
        
        private static function getDocHeader():String 
        {
            return ("<?xml version=\"1.0\"?>\n" + 
                    "<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\n" + 
                    " xmlns:o=\"urn:schemas-microsoft-com:office:office\"\n" + 
                    " xmlns:x=\"urn:schemas-microsoft-com:office:excel\"\n" + 
                    " xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"\n" + 
                    " xmlns:html=\"http://www.w3.org/TR/REC-html40\">\n" + 
                    " <DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">\n" + 
                    "  <Author>Lease Management System</Author>\n" + 
                    "  <LastAuthor>Lease Management System</LastAuthor>\n" + 
                    "  <Created>2008-06-19T16:47:11Z</Created>\n" + 
                    "  <LastSaved>2008-06-19T17:10:52Z</LastSaved>\n" + 
                    "  <Company>Love Land Inc</Company>\n" + 
                    "  <Version>10.2625</Version>\n" + 
                    " </DocumentProperties>\n" + 
                    " <OfficeDocumentSettings xmlns=\"urn:schemas-microsoft-com:office:office\">\n" + 
                    "  <DownloadComponents/>\n" + 
                    "  <LocationOfComponents/>\n" + 
                    " </OfficeDocumentSettings>\n" + 
                    " <ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">\n" + 
                    "  <WindowHeight>11595</WindowHeight>\n" + 
                    "  <WindowWidth>19065</WindowWidth>\n" + 
                    "  <WindowTopX>360</WindowTopX>\n" + 
                    "  <WindowTopY>45</WindowTopY>\n" + 
                    "  <TabRatio>442</TabRatio>\n" + 
                    "  <ProtectStructure>False</ProtectStructure>\n" + 
                    "  <ProtectWindows>False</ProtectWindows>\n" + 
                    " </ExcelWorkbook>\n" + 
                    " <Styles>\n" + 
                    "  <Style ss:ID=\"Default\" ss:Name=\"Normal\">\n" + 
                    "   <Alignment ss:Vertical=\"Bottom\"/>\n" + 
                    "   <Borders/>\n" + 
                    "   <Font ss:FontName=\"Arial Cyr\" x:CharSet=\"204\"/>\n" + 
                    "   <Interior/>\n" + 
                    "   <NumberFormat/>\n" + 
                    "   <Protection/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24870604\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Top\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24870614\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Top\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24870452\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Top\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24870462\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Top\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24870472\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Top\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24870482\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Top\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24885752\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:Family=\"Swiss\" ss:Bold=\"1\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24885762\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:Family=\"Swiss\" ss:Bold=\"1\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24885772\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:Family=\"Swiss\" ss:Bold=\"1\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24885782\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Top\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24885792\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Top\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24885802\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Top\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24885812\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Top\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24885822\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Top\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24885832\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Top\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24885600\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:Family=\"Swiss\" ss:Bold=\"1\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24885610\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:Family=\"Swiss\" ss:Bold=\"1\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24885620\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:Family=\"Swiss\" ss:Bold=\"1\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24885630\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:Family=\"Swiss\" ss:Bold=\"1\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24885448\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:Family=\"Swiss\" ss:Bold=\"1\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24885458\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:Family=\"Swiss\" ss:Bold=\"1\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24885468\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:Family=\"Swiss\" ss:Bold=\"1\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24885478\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:Family=\"Swiss\" ss:Bold=\"1\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24885296\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:Family=\"Swiss\" ss:Bold=\"1\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24885306\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:Family=\"Swiss\" ss:Bold=\"1\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24885316\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:Family=\"Swiss\" ss:Bold=\"1\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"m24885326\">\n" + 
                    "   <Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"2\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:Family=\"Swiss\" ss:Bold=\"1\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s21\">\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s41\">\n" + 
                    "   <Alignment ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s42\">\n" + 
                    "   <Alignment ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s43\">\n" + 
                    "   <Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "   <NumberFormat/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s44\">\n" + 
                    "   <Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "   <NumberFormat/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s45\">\n" + 
                    "   <Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "   <NumberFormat ss:Format=\"0%\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s46\">\n" + 
                    "   <Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "   <NumberFormat ss:Format=\"0%\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s47\">\n" + 
                    "   <Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "   <NumberFormat/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s48\">\n" + 
                    "   <Alignment ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s49\">\n" + 
                    "   <Alignment ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders/>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s50\">\n" + 
                    "   <Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "   <NumberFormat/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s51\">\n" + 
                    "   <Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders/>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "   <NumberFormat/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s52\">\n" + 
                    "   <Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "   <NumberFormat ss:Format=\"0%\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s53\">\n" + 
                    "   <Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders/>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "   <NumberFormat ss:Format=\"0%\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s54\">\n" + 
                    "   <Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "   <NumberFormat/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s55\">\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s56\">\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s57\">\n" + 
                    "   <Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "   <NumberFormat/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s58\">\n" + 
                    "   <Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "   <NumberFormat/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s59\">\n" + 
                    "   <Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "   <NumberFormat/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s60\">\n" + 
                    "   <Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "   <NumberFormat/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s61\">\n" + 
                    "   <Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\" ss:WrapText=\"1\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Font x:CharSet=\"204\" x:Family=\"Swiss\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "   <NumberFormat/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s62\">\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s63\">\n" + 
                    "   <Borders/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s64\">\n" + 
                    "   <Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "   <NumberFormat/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s65\">\n" + 
                    "   <Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\"/>\n" + 
                    "   <Borders/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "   <NumberFormat/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s66\">\n" + 
                    "   <Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\"/>\n" + 
                    "   <Borders>\n" + 
                    "    <Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n" + 
                    "   </Borders>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "   <NumberFormat/>\n" + 
                    "  </Style>\n" + 
                    "  <Style ss:ID=\"s67\">\n" + 
                    "   <Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Bottom\"/>\n" + 
                    "   <Font ss:FontName=\"Arial Cyr\" x:CharSet=\"204\" ss:Bold=\"1\"/>\n" + 
                    "   <Interior ss:Color=\"#FFFFFF\" ss:Pattern=\"Solid\"/>\n" + 
                    "  </Style>\n" + 
                    " </Styles>\n" + 
                    " <Worksheet ss:Name=\"Leases\">\n" + 
                    "  <Table ss:ExpandedColumnCount=\"18\" x:FullColumns=\"1\"\n" + 
                    "   x:FullRows=\"1\" ss:StyleID=\"s21\">\n" + 
                    "   <Column ss:StyleID=\"s21\" ss:Width=\"49.5\"/>\n" + 
                    "   <Column ss:StyleID=\"s21\" ss:AutoFitWidth=\"0\" ss:Width=\"88.5\" ss:Span=\"1\"/>\n" + 
                    "   <Column ss:Index=\"4\" ss:StyleID=\"s21\" ss:AutoFitWidth=\"0\" ss:Width=\"70.5\"/>\n" + 
                    "   <Column ss:StyleID=\"s21\" ss:AutoFitWidth=\"0\" ss:Width=\"59.25\" ss:Span=\"1\"/>\n" + 
                    "   <Column ss:Index=\"7\" ss:StyleID=\"s21\" ss:AutoFitWidth=\"0\" ss:Width=\"90\"/>\n" + 
                    "   <Column ss:StyleID=\"s21\" ss:AutoFitWidth=\"0\" ss:Width=\"88.5\"/>\n" + 
                    "   <Column ss:StyleID=\"s21\" ss:AutoFitWidth=\"0\" ss:Width=\"294\"/>\n" + 
                    "   <Column ss:StyleID=\"s21\" ss:AutoFitWidth=\"0\" ss:Width=\"65\" ss:Span=\"8\"/>\n" + 
                    "   <Row ss:AutoFitHeight=\"0\" ss:Height=\"13.5\"/>\n" + 
                    "   <Row>\n" + 
                    "    <Cell ss:MergeDown=\"1\" ss:StyleID=\"m24885296\"><Data ss:Type=\"String\">Lease No</Data></Cell>\n" + 
                    "    <Cell ss:MergeDown=\"1\" ss:StyleID=\"m24885306\"><Data ss:Type=\"String\">Lessor</Data></Cell>\n" + 
                    "    <Cell ss:MergeDown=\"1\" ss:StyleID=\"m24885316\"><Data ss:Type=\"String\">Lessee </Data></Cell>\n" + 
                    "    <Cell ss:MergeDown=\"1\" ss:StyleID=\"m24885326\"><Data ss:Type=\"String\">Recording Book/Page</Data></Cell>\n" + 
                    "    <Cell ss:MergeDown=\"1\" ss:StyleID=\"m24885448\"><Data ss:Type=\"String\">Lease Date</Data></Cell>\n" + 
                    "    <Cell ss:MergeDown=\"1\" ss:StyleID=\"m24885458\"><Data ss:Type=\"String\">Primary Exp Date</Data></Cell>\n" + 
                    "    <Cell ss:MergeDown=\"1\" ss:StyleID=\"m24885468\"><Data ss:Type=\"String\">Township Range</Data></Cell>\n" + 
                    "    <Cell ss:MergeDown=\"1\" ss:StyleID=\"m24885478\"><Data ss:Type=\"String\">Sections</Data></Cell>\n" + 
                    "    <Cell ss:MergeDown=\"1\" ss:StyleID=\"m24885600\"><Data ss:Type=\"String\">Leased Interests</Data></Cell>\n" + 
                    "    <Cell ss:MergeDown=\"1\" ss:StyleID=\"m24885610\"><Data ss:Type=\"String\">Gross Acres</Data></Cell>\n" + 
                    "    <Cell ss:MergeDown=\"1\" ss:StyleID=\"m24885620\"><Data ss:Type=\"String\">Net Acres</Data></Cell>\n" + 
                    "    <Cell ss:MergeDown=\"1\" ss:StyleID=\"m24885630\"><Data ss:Type=\"String\">Lease Interest</Data></Cell>\n" + 
                    "    <Cell ss:MergeDown=\"1\" ss:StyleID=\"m24885630\"><Data ss:Type=\"String\">Lease Burden</Data></Cell>\n" + 
                    "    <Cell ss:MergeDown=\"1\" ss:StyleID=\"m24885630\"><Data ss:Type=\"String\">Lease NRI</Data></Cell>\n" + 
                    "    <Cell ss:MergeDown=\"1\" ss:StyleID=\"m24885752\"><Data ss:Type=\"String\">FCR WI</Data></Cell>\n" + 
                    "    <Cell ss:MergeDown=\"1\" ss:StyleID=\"m24885762\"><Data ss:Type=\"String\">Burden</Data></Cell>\n" + 
                    "    <Cell ss:MergeDown=\"1\" ss:StyleID=\"m24885762\"><Data ss:Type=\"String\">FCR NRI</Data></Cell>\n" + 
                    "    <Cell ss:MergeDown=\"1\" ss:StyleID=\"m24885772\"><Data ss:Type=\"String\">FCR Net Acres</Data></Cell>\n" + 
                    "   </Row>\n" + 
                    "   <Row ss:AutoFitHeight=\"0\" ss:Height=\"13.5\"/>\n" + 
                    "   <Row ss:AutoFitHeight=\"0\" ss:Height=\"3.75\"/>");
                    
        }
        
        private static function getLeaseHeader(lease:Lease):String 
        {
            return ("   <Row ss:AutoFitHeight=\"0\">\n" + 
                    "    <Cell ss:MergeDown=\"1\" ss:StyleID=\"m24885782\"><Data ss:Type=\"String\">" + lease.leaseNum.toString() + "</Data></Cell>\n" + 
                    "    <Cell ss:MergeDown=\"1\" ss:StyleID=\"m24885792\"><Data ss:Type=\"String\">" + lease.leasorStr.replace("\n", " ") + "</Data></Cell>\n" + 
                    "    <Cell ss:MergeDown=\"1\" ss:StyleID=\"m24885802\"><Data ss:Type=\"String\">" + lease.leaseeStr.replace("\n", " ") + " </Data></Cell>\n" + 
                    "    <Cell ss:MergeDown=\"1\" ss:StyleID=\"m24885812\"><Data ss:Type=\"String\">" + lease.recordsStr.replace("\n", " ") + "</Data></Cell>\n" + 
                    "    <Cell ss:MergeDown=\"1\" ss:StyleID=\"m24885822\"><Data ss:Type=\"String\">" + lease.leaseDateExcelStr + "</Data></Cell>\n" + 
                    "    <Cell ss:MergeDown=\"1\" ss:StyleID=\"m24885832\"><Data ss:Type=\"String\">" + lease.expirationDateExcelStr + "</Data></Cell>");
        }
        
        private static function getFirstTract(lease:Lease):String 
        {
            return ("\n" + 
                    "    <Cell ss:StyleID=\"s41\"><Data ss:Type=\"String\">" + LeaseTract(lease.tracts[0]).townshipRangeStr + "</Data></Cell>\n" + 
                    "    <Cell ss:StyleID=\"s42\"><Data ss:Type=\"String\">Sec." + LeaseTract(lease.tracts[0]).section + "</Data></Cell>\n" + 
                    "    <Cell ss:StyleID=\"s42\"><Data ss:Type=\"String\">" + LeaseTract(lease.tracts[0]).tract + "</Data></Cell>\n" + 
                    "    <Cell ss:StyleID=\"s43\"><Data ss:Type=\"Number\">" + LeaseTract(lease.tracts[0]).grossAcres.toString() + "</Data></Cell>\n" + 
                    "    <Cell ss:StyleID=\"s44\"><Data ss:Type=\"Number\">" + LeaseTract(lease.tracts[0]).netAcres.toString() + "</Data></Cell>\n" + 
                    "    <Cell ss:StyleID=\"s45\"><Data ss:Type=\"Number\">" + (LeaseTract(lease.tracts[0]).leaseInterest).toString() + "</Data></Cell>\n" + 
                    "    <Cell ss:StyleID=\"s45\"><Data ss:Type=\"Number\">" + (LeaseTract(lease.tracts[0]).leaseBurden).toString() + "</Data></Cell>\n" + 
                    "    <Cell ss:StyleID=\"s45\"><Data ss:Type=\"Number\">" + (LeaseTract(lease.tracts[0]).nri).toString() + "</Data></Cell>\n" + 
                    "    <Cell ss:StyleID=\"s45\"><Data ss:Type=\"Number\">" + (LeaseTract(lease.tracts[0]).cwi).toString() + "</Data></Cell>\n" + 
                    "    <Cell ss:StyleID=\"s45\"><Data ss:Type=\"Number\">" + (LeaseTract(lease.tracts[0]).burden).toString() + "</Data></Cell>\n" + 
                    "    <Cell ss:StyleID=\"s45\"><Data ss:Type=\"Number\">" + (LeaseTract(lease.tracts[0]).cnri).toString() + "</Data></Cell>\n" + 
                    "    <Cell ss:StyleID=\"s47\"><Data ss:Type=\"Number\">" + (LeaseTract(lease.tracts[0]).cNetAcres).toString() + "</Data></Cell>\n" + 
                    "  </Row> \n" + 
                    "");
        }
        
        private static function getEmptyTract():String 
        {
            return ("\n" + 
                    "    <Cell ss:StyleID=\"s41\"><Data ss:Type=\"String\"></Data></Cell>\n" + 
                    "    <Cell ss:StyleID=\"s42\"><Data ss:Type=\"String\"></Data></Cell>\n" + 
                    "    <Cell ss:StyleID=\"s42\"><Data ss:Type=\"String\"></Data></Cell>\n" + 
                    "    <Cell ss:StyleID=\"s43\"><Data ss:Type=\"Number\"></Data></Cell>\n" + 
                    "    <Cell ss:StyleID=\"s44\"><Data ss:Type=\"Number\"></Data></Cell>\n" + 
                    "    <Cell ss:StyleID=\"s45\"><Data ss:Type=\"Number\"></Data></Cell>\n" + 
                    "    <Cell ss:StyleID=\"s45\"><Data ss:Type=\"Number\"></Data></Cell>\n" + 
                    "    <Cell ss:StyleID=\"s45\"><Data ss:Type=\"Number\"></Data></Cell>\n" + 
                    "    <Cell ss:StyleID=\"s45\"><Data ss:Type=\"Number\"></Data></Cell>\n" + 
                    "    <Cell ss:StyleID=\"s45\"><Data ss:Type=\"Number\"></Data></Cell>\n" + 
                    "    <Cell ss:StyleID=\"s45\"><Data ss:Type=\"Number\"></Data></Cell>\n" + 
                    "    <Cell ss:StyleID=\"s47\"><Data ss:Type=\"Number\"></Data></Cell>\n" + 
                    "   </Row>\n" + 
                    "");
        }
        
        private static function getTracts(lease:Lease):String 
        {
            var result:String = "";
            
            for (var i:int = 1; i < lease.tracts.length; i++) 
            {
                result += ("\n" + 
                        "   <Row>\n" + 
                        "    <Cell ss:Index=\"7\" ss:StyleID=\"s48\"><Data ss:Type=\"String\">" + LeaseTract(lease.tracts[i]).townshipRangeStr + "</Data></Cell>\n" + 
                        "    <Cell ss:StyleID=\"s49\"><Data ss:Type=\"String\">Sec." + LeaseTract(lease.tracts[i]).section + "</Data></Cell>\n" + 
                        "    <Cell ss:StyleID=\"s49\"><Data ss:Type=\"String\">" + LeaseTract(lease.tracts[i]).tract + "</Data></Cell>\n" + 
                        "    <Cell ss:StyleID=\"s50\"><Data ss:Type=\"Number\">" + LeaseTract(lease.tracts[i]).grossAcres.toString() + "</Data></Cell>\n" + 
                        "    <Cell ss:StyleID=\"s51\"><Data ss:Type=\"Number\">" + LeaseTract(lease.tracts[i]).netAcres.toString() + "</Data></Cell>\n" + 
                        "    <Cell ss:StyleID=\"s52\"><Data ss:Type=\"Number\">" + (LeaseTract(lease.tracts[i]).leaseInterest).toString() + "</Data></Cell>\n" + 
                        "    <Cell ss:StyleID=\"s52\"><Data ss:Type=\"Number\">" + (LeaseTract(lease.tracts[i]).leaseBurden).toString() + "</Data></Cell>\n" + 
                        "    <Cell ss:StyleID=\"s52\"><Data ss:Type=\"Number\">" + (LeaseTract(lease.tracts[i]).nri).toString() + "</Data></Cell>\n" + 
                        "    <Cell ss:StyleID=\"s53\"><Data ss:Type=\"Number\">" + (LeaseTract(lease.tracts[i]).cwi).toString() + "</Data></Cell>\n" + 
                        "    <Cell ss:StyleID=\"s52\"><Data ss:Type=\"Number\">" + (LeaseTract(lease.tracts[i]).burden).toString() + "</Data></Cell>\n" + 
                        "    <Cell ss:StyleID=\"s52\"><Data ss:Type=\"Number\">" + (LeaseTract(lease.tracts[i]).cnri).toString() + "</Data></Cell>\n" + 
                        "    <Cell ss:StyleID=\"s54\"><Data ss:Type=\"Number\">" + (LeaseTract(lease.tracts[i]).cNetAcres).toString() + "</Data></Cell>\n" + 
                        "   </Row>\n" + 
                        "");
            }
            
            return result;
        }
        
        private static function getLeaseFooter():String
        {
            return ("\n" + 
                    "   <Row>\n" + 
                    "    <Cell ss:Index=\"7\" ss:StyleID=\"s55\"/>\n" + 
                    "    <Cell ss:StyleID=\"s56\"/>\n" + 
                    "    <Cell ss:StyleID=\"s56\"/>\n" + 
                    "    <Cell ss:StyleID=\"s57\"/>\n" + 
                    "    <Cell ss:StyleID=\"s58\"/>\n" + 
                    "    <Cell ss:StyleID=\"s57\"/>\n" + 
                    "    <Cell ss:StyleID=\"s58\"/>\n" + 
                    "    <Cell ss:StyleID=\"s57\"/>\n" + 
                    "    <Cell ss:StyleID=\"s58\"/>\n" + 
                    "    <Cell ss:StyleID=\"s57\"/>\n" + 
                    "    <Cell ss:StyleID=\"s57\"/>\n" + 
                    "    <Cell ss:StyleID=\"s59\"/>\n" + 
                    "   </Row>\n" + 
                    "");
        }
        
        private static function getDocFooter(leases:ArrayCollection):String 
        {
            var result:String = "";
            
            var totalGross:Number = 0;
            var totalNet:Number = 0;
            
            for each (var lease:Lease in leases) 
            {
                for each (var tract:LeaseTract in lease.tracts) 
                {
                    totalGross += tract.grossAcres;
                    totalNet += tract.netAcres;
                }
            }
            
            result += ("\n" + 
                    "   <Row ss:AutoFitHeight=\"0\" ss:Height=\"3.75\"/>\n" + 
                    "   <Row>\n" + 
                    "    <Cell ss:Index=\"9\" ss:StyleID=\"s67\"><Data ss:Type=\"String\">Total Acres</Data></Cell>\n" + 
                    "    <Cell ss:StyleID=\"s67\"><Data ss:Type=\"Number\">" + totalGross.toString() + "</Data></Cell>\n" + 
                    "    <Cell ss:StyleID=\"s67\"><Data ss:Type=\"Number\">" + totalNet.toString() + "</Data></Cell>\n" + 
                    "   </Row>\n" + 
                    "  </Table>\n" + 
                    "  <WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">\n" + 
                    "   <PageSetup>\n" + 
                    "    <PageMargins x:Bottom=\"0.984251969\" x:Left=\"0.78740157499999996\"\n" + 
                    "     x:Right=\"0.78740157499999996\" x:Top=\"0.984251969\"/>\n" + 
                    "   </PageSetup>\n" + 
                    "   <Print>\n" + 
                    "    <ValidPrinterInfo/>\n" + 
                    "    <PaperSizeIndex>9</PaperSizeIndex>\n" + 
                    "    <HorizontalResolution>600</HorizontalResolution>\n" + 
                    "    <VerticalResolution>0</VerticalResolution>\n" + 
                    "   </Print>\n" + 
                    "   <Selected/>\n" + 
                    "   <Panes>\n" + 
                    "    <Pane>\n" + 
                    "     <Number>3</Number>\n" + 
                    "     <ActiveRow>10</ActiveRow>\n" + 
                    "     <ActiveCol>1</ActiveCol>\n" + 
                    "    </Pane>\n" + 
                    "   </Panes>\n" + 
                    "   <ProtectObjects>False</ProtectObjects>\n" + 
                    "   <ProtectScenarios>False</ProtectScenarios>\n" + 
                    "  </WorksheetOptions>\n" + 
                    " </Worksheet>\n" + 
                    "</Workbook>\n" + 
                    "");
            return result;
        }
        
    }
}