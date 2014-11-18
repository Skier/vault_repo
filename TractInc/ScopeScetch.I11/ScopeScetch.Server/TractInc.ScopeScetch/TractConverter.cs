using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using TractInc.ScopeScetch.Entity;
using Color=iTextSharp.text.Color;
using Document=iTextSharp.text.Document;
using Font=iTextSharp.text.Font;
using Image=iTextSharp.text.Image;
using Rectangle=iTextSharp.text.Rectangle;

namespace TractInc.ScopeScetch
{
    public class TractConverter
    {
        private const float INCH_SIZE = 96;
        private const float HALF_INCH_SIZE = INCH_SIZE / 2;
        
        //"Letter" page format
        private const float PAGE_WIDH = ((float)8.5 * INCH_SIZE);
        private const float PAGE_HEIGHT = ((float)11.69 * INCH_SIZE);

        private const string PDFEXPORTDIRECTORY = "PDFExportDirectory";
        private const string PDFEXPORTURL = "PDFExportUrl";
        
        private static string PDFExportDirectory {
            get {
                string result = ConfigurationManager.AppSettings[PDFEXPORTDIRECTORY];
                if (null == result || result.Length == 0) {
                    throw new ConfigurationErrorsException("PDFExportDirectory not found");
                }

                return result;
            }
        }

        private static string PDFExportUrl {
            get {
                string result = ConfigurationManager.AppSettings[PDFEXPORTURL];
                if (null == result || result.Length == 0) {
                    throw new ConfigurationErrorsException("PDFExportUrl not found");
                }

                return result;
            }
        }
        
        public string ConvertToPdf(Tract tract, byte[] tractImageSource, byte[] scaleBarImageSource)
        {

            string directory = PDFExportDirectory;
            string fileName = String.Format("tract_{0}.pdf", DateTime.Now.ToString("yyyyMMdd_HH_mm"));
            
            if (!directory.EndsWith(new string(Path.DirectorySeparatorChar, 1)))
                directory += Path.DirectorySeparatorChar;

            Document document = new Document();

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(directory + fileName, FileMode.Create));

            document.SetPageSize(new Rectangle(PAGE_WIDH, PAGE_HEIGHT));
            document.SetMargins(HALF_INCH_SIZE, HALF_INCH_SIZE, HALF_INCH_SIZE, HALF_INCH_SIZE);

            document.Open();
            
            float borderWidth = PAGE_WIDH - INCH_SIZE;
            float borderHeight = (float) ((PAGE_HEIGHT * (3.0 / 4.0)));
            
            Point borderCenter = new Point(
                (int)PAGE_WIDH / 2,
                (int)((PAGE_HEIGHT - borderHeight - HALF_INCH_SIZE) + borderHeight/2));

            Image tractImage = Image.GetInstance(tractImageSource);
            Image scaleBarImage = Image.GetInstance(scaleBarImageSource);

            tractImage.SetAbsolutePosition(
                borderCenter.X - tractImage.Width/2, 
                borderCenter.Y - tractImage.Height/2);
            
            //----- Right Text
            Chunk c2 = new Chunk("", FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.NORMAL));

            if (null != tract.ParentDocument) {
                c2.Append(string.Format("{0}: {1}\n", tract.ParentDocument.BuyerRoleName, tract.ParentDocument.Buyer.AsNamed));
                c2.Append(string.Format("{0}: {1}\n", tract.ParentDocument.SellerRoleName, tract.ParentDocument.Seller.AsNamed));
                c2.Append(string.Format("State: {0}\n", tract.ParentDocument.StateName));
                c2.Append(string.Format("County: {0}\n", tract.ParentDocument.CountyName));
                c2.Append(string.Format("Doc Type: {0}\n", tract.ParentDocument.DocumentTypeName));
                c2.Append(string.Format("Doc No: {0}\n", tract.ParentDocument.DocumentNo));
                c2.Append(string.Format("Vol: {0}\n", tract.ParentDocument.Volume));
                c2.Append(string.Format("Page: {0}\n", tract.ParentDocument.Page));
            }

            //Middle Text
            Chunk c = new Chunk("", FontFactory.GetFont(FontFactory.HELVETICA, 24, Font.NORMAL));
            c.Append(string.Format("{0}\n\n", tract.Description));
            
            if (null != tract.ParentDocument) {
                c.Append(string.Format("{0}\n\n", tract.ParentDocument.Buyer.FullName));
            }
            
            c.Append(string.Format("Called {0} {1}\n\n\n", tract.CalledAC, tract.UnitName));

            Chunk c3 = new Chunk("", FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.NORMAL));

            if (null != tract.ParentDocument) {

                String signedDate = (tract.ParentDocument.DateSigned != DateTime.MinValue)
                                        ? tract.ParentDocument.DateSigned.ToShortDateString()
                                        : "";

                String filedDate = (tract.ParentDocument.DateFilled != DateTime.MinValue)
                                        ? tract.ParentDocument.DateFilled.ToShortDateString()
                                        : "";
                
                c3.Append(string.Format("Date Signed: {0}\n", signedDate));
                c3.Append(string.Format("Date Filed: {0}\n", filedDate));
            }

            Cell documentTableCell = new Cell(c2);
            documentTableCell.Width = "30%";
            documentTableCell.Border = Rectangle.NO_BORDER;

            Cell tractCell = new Cell(c);
            tractCell.Width = "100%";
            tractCell.Border = Rectangle.NO_BORDER;
            tractCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tractCell.Add(scaleBarImage);
            
            Cell document2TableCell = new Cell(c3);
            document2TableCell.Border = Rectangle.NO_BORDER;
            document2TableCell.HorizontalAlignment = Element.ALIGN_RIGHT;

            float tableWidth = (PAGE_WIDH - INCH_SIZE);
            
            Table tbl = new Table(3, 1);
            tbl.Padding = 3;
            tbl.Border = Rectangle.NO_BORDER;
            tbl.Spacing = 1;
            tbl.Widths = new float[] {tableWidth * 0.3f, tableWidth * 0.45f, tableWidth * 0.25f};
            tbl.WidthPercentage = 100;
            tbl.AutoFillEmptyCells = true;
            tbl.AddCell(documentTableCell, 0, 0);
            tbl.AddCell(tractCell, 0, 1);
            tbl.AddCell(document2TableCell, 0, 2);

            Image emtpySpace = Image.GetInstance(
                new Bitmap((int)borderWidth, (int)(borderHeight + HALF_INCH_SIZE / 2)), Color.WHITE);

            drawTractBorder(writer, borderWidth, borderHeight);

            document.Add(emtpySpace);
            document.Add(tractImage);
            document.Add(tbl);
            document.Close();

            return PDFExportUrl + fileName;
        }

        private void drawTractBorder(PdfWriter writer, float borderWidth, float borderHeight) {
            PdfContentByte cb = writer.DirectContent;

//            float borderWidth = PAGE_WIDH - INCH_SIZE;
//            float borderHeight = (float) ((PAGE_HEIGHT * (3.0 / 4.0)) - HALF_INCH_SIZE);

            cb.SetColorStroke(Color.BLACK);
            cb.SetLineWidth(6);
            cb.Rectangle(HALF_INCH_SIZE, PAGE_HEIGHT - borderHeight - HALF_INCH_SIZE, 
                         borderWidth, borderHeight);
            cb.Stroke();

            cb.SetColorStroke(Color.WHITE);
            cb.SetLineWidth(3);
            cb.SetLineDash(10, 15, 0);
            cb.Rectangle(HALF_INCH_SIZE, PAGE_HEIGHT - borderHeight - HALF_INCH_SIZE, 
                         borderWidth, borderHeight);
            cb.Stroke();
        }

    }
}
