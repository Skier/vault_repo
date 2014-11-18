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

            Image scaleBarImage = Image.GetInstance(scaleBarImageSource);

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

            Chunk footerÑhunk = new Chunk("© Provided by scopemapping.com", FontFactory.GetFont(FontFactory.HELVETICA, 6, Font.NORMAL));
            HeaderFooter footer = new HeaderFooter(new Phrase(footerÑhunk), false);
            footer.Alignment = Element.ALIGN_RIGHT;
            footer.BorderWidth = 0;
            document.Footer = footer;

			document.Open();

            //----- Left side Text

            Phrase phLeft = new Phrase(12);

            if (null != tractInfo.ParentDocument)
            {
                int bLen = tractInfo.ParentDocument.Buyer.AsNamed.Length;
                float bFontSize = bLen > 20 ? bLen > 50 ? bLen > 100 ? bLen > 200 ? 8 : 9 : 10 : 11 : 12;
                Chunk buyerCh = new Chunk("", FontFactory.GetFont(FontFactory.HELVETICA, bFontSize, Font.NORMAL));

                buyerCh.Append(string.Format("{0}: {1}\n", tractInfo.ParentDocument.BuyerRoleName, tractInfo.ParentDocument.Buyer.AsNamed));

                int sLen = tractInfo.ParentDocument.Seller.AsNamed.Length;
                float sFontSize = sLen > 20 ? sLen > 50 ? sLen > 100 ? sLen > 200 ? 8 : 9 : 10 : 11 : 12;
                Chunk sellerCh = new Chunk("", FontFactory.GetFont(FontFactory.HELVETICA, sFontSize, Font.NORMAL));

                sellerCh.Append(string.Format("{0}: {1}\n", tractInfo.ParentDocument.SellerRoleName, tractInfo.ParentDocument.Seller.AsNamed));

                Chunk chDocInfo = new Chunk("", FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.NORMAL));

                chDocInfo.Append(string.Format("State: {0}\n", tractInfo.ParentDocument.StateName));
                chDocInfo.Append(string.Format("County: {0}\n", tractInfo.ParentDocument.CountyName));
                chDocInfo.Append(string.Format("Doc Type: {0}\n", tractInfo.ParentDocument.DocumentTypeName));
                chDocInfo.Append(string.Format("Doc No: {0}\n", tractInfo.ParentDocument.DocumentNo));
                chDocInfo.Append(string.Format("Vol: {0}\n", tractInfo.ParentDocument.Volume));
                chDocInfo.Append(string.Format("Page: {0}\n", tractInfo.ParentDocument.Page));

                phLeft.Add(buyerCh);
                phLeft.Add(sellerCh);

                phLeft.Add(chDocInfo);
            }


            //Middle Text

            Phrase phCenter = new Phrase();

            Chunk refNameCh = new Chunk("", FontFactory.GetFont(FontFactory.HELVETICA, 24, Font.NORMAL));
            refNameCh.Append(string.Format("{0}\n\n", tractInfo.RefName));
            
            phCenter.Add(refNameCh);
            
            if (null != tractInfo.ParentDocument) {
                int bLen = tractInfo.ParentDocument.Buyer.AsNamed.Length;
                float asNamedFontSize = bLen > 20 ? bLen > 50 ? bLen > 100 ? bLen > 200 ? 12 : 14 : 16 : 18 : 24;
                Chunk asNamedCh = new Chunk("", FontFactory.GetFont(FontFactory.HELVETICA, asNamedFontSize, Font.NORMAL));
                asNamedCh.Append(string.Format("{0}\n\n", tractInfo.ParentDocument.Buyer.AsNamed));
                
                phCenter.Add(asNamedCh);
            }

            Chunk calledCh = new Chunk("", FontFactory.GetFont(FontFactory.HELVETICA, 24, Font.NORMAL));
            calledCh.Append(string.Format("Called {0} {1}\n\n\n", tractInfo.CalledAC, tractInfo.UnitName));

            phCenter.Add(calledCh);
            
            // Rignt Text
            Chunk datesCh = new Chunk("", FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.NORMAL));

            if (null != tractInfo.ParentDocument) {

                String signedDate = (tractInfo.ParentDocument.DateSigned != DateTime.MinValue)
                                        ? tractInfo.ParentDocument.DateSigned.ToShortDateString()
                                        : "";

                String filedDate = (tractInfo.ParentDocument.DateFilled != DateTime.MinValue)
                                        ? tractInfo.ParentDocument.DateFilled.ToShortDateString()
                                        : "";

                datesCh.Append(string.Format("Date Signed: {0}\n", signedDate));
                datesCh.Append(string.Format("Date Filed: {0}\n", filedDate));
            }

            Cell documentTableCell = new Cell(phLeft);
            documentTableCell.Width = "30%";
            documentTableCell.Border = Rectangle.NO_BORDER;

            Cell tractCell = new Cell(phCenter);
            tractCell.Width = "100%";
            tractCell.Border = Rectangle.NO_BORDER;
            tractCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tractCell.Add(scaleBarImage);

            Cell document2TableCell = new Cell(datesCh);
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

            float borderWidth = PAGE_WIDH - INCH_SIZE;
//            float borderHeight = (float)((PAGE_HEIGHT * (3.0 / 4.0)));

            int len = tractInfo.ParentDocument.Buyer.AsNamed.Length + tractInfo.ParentDocument.Seller.AsNamed.Length;
            float tableH = len > 20 ? len > 50 ? len > 100 ? len > 200 ? 280 : 240 : 220 : 200 : 190;
            float borderHeight = PAGE_HEIGHT - INCH_SIZE - tableH;

            Point borderCenter = new Point(
                (int)PAGE_WIDH / 2,
                (int)((PAGE_HEIGHT - borderHeight - HALF_INCH_SIZE) + borderHeight / 2));

            Image tractImage = Image.GetInstance(tractImageSource);

            tractImage.SetAbsolutePosition(
                borderCenter.X - tractImage.Width / 2,
                borderCenter.Y - tractImage.Height / 2);

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
