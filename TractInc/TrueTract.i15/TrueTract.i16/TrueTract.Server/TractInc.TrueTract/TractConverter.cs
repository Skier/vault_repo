using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using TractInc.TrueTract.Entity;
using Color=iTextSharp.text.Color;
using PdfDocument=iTextSharp.text.Document;
using Font=iTextSharp.text.Font;
using Image=iTextSharp.text.Image;
using Rectangle=iTextSharp.text.Rectangle;

namespace TractInc.TrueTract
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
        
        public string ConvertToPdf(TractInfo tractInfo, byte[] tractImageSource, byte[] scaleBarImageSource)
        {

            string directory = PDFExportDirectory;
            string fileName = String.Format("tract_{0}.pdf", DateTime.Now.ToString("yyyyMMdd_HH_mm"));
            
            if (!directory.EndsWith(new string(Path.DirectorySeparatorChar, 1)))
                directory += Path.DirectorySeparatorChar;

            PdfDocument document = new PdfDocument();

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(directory + fileName, FileMode.Create));

            document.SetPageSize(new Rectangle(PAGE_WIDH, PAGE_HEIGHT));
            document.SetMargins(HALF_INCH_SIZE, HALF_INCH_SIZE, HALF_INCH_SIZE, HALF_INCH_SIZE);

            document.AddProducer();
            document.AddCreator("True Tract LLC");
            document.AddAuthor("True Tract");

            Chunk footer—hunk = new Chunk("© Provided by scopemapping.com", FontFactory.GetFont(FontFactory.HELVETICA, 6, Font.NORMAL));
            HeaderFooter footer = new HeaderFooter(new Phrase(footer—hunk), false);
            footer.Alignment = Element.ALIGN_RIGHT;
            footer.BorderWidth = 0;
            document.Footer = footer;

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

            if (null != tractInfo.ParentDocument) {
                c2.Append(string.Format("{0}: {1}\n", tractInfo.ParentDocument.BuyerRoleName, tractInfo.ParentDocument.Buyer.AsNamed));
                c2.Append(string.Format("{0}: {1}\n", tractInfo.ParentDocument.SellerRoleName, tractInfo.ParentDocument.Seller.AsNamed));
                c2.Append(string.Format("State: {0}\n", tractInfo.ParentDocument.StateName));
                c2.Append(string.Format("County: {0}\n", tractInfo.ParentDocument.CountyName));
                c2.Append(string.Format("Doc Type: {0}\n", tractInfo.ParentDocument.DocumentTypeName));
                c2.Append(string.Format("Doc No: {0}\n", tractInfo.ParentDocument.DocumentNo));
                c2.Append(string.Format("Vol: {0}\n", tractInfo.ParentDocument.Volume));
                c2.Append(string.Format("Page: {0}\n", tractInfo.ParentDocument.Page));
            }

            //Middle Text
            Chunk c = new Chunk("", FontFactory.GetFont(FontFactory.HELVETICA, 24, Font.NORMAL));
            c.Append(string.Format("{0}\n\n", tractInfo.RefName));
            
            if (null != tractInfo.ParentDocument) {
                c.Append(string.Format("{0}\n\n", tractInfo.ParentDocument.Buyer.AsNamed));
            }
            
            c.Append(string.Format("Called {0} {1}\n\n\n", tractInfo.CalledAC, tractInfo.UnitName));

            Chunk c3 = new Chunk("", FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.NORMAL));

            if (null != tractInfo.ParentDocument) {

                String signedDate = (tractInfo.ParentDocument.Signed != DateTime.MinValue)
                                        ? tractInfo.ParentDocument.Signed.ToShortDateString()
                                        : "";

                String filedDate = (tractInfo.ParentDocument.Filed != DateTime.MinValue)
                                        ? tractInfo.ParentDocument.Filed.ToShortDateString()
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
                new Bitmap((int)borderWidth, (int)(borderHeight + 5)), Color.WHITE);

            drawTractBorder(writer, borderWidth, borderHeight);

            document.Add(emtpySpace);
            document.Add(tractImage);
            document.Add(tbl);

            document.Close();

            return PDFExportUrl + fileName;
        }

        private void drawTractBorder(PdfWriter writer, float borderWidth, float borderHeight) {
            PdfContentByte cb = writer.DirectContent;

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
