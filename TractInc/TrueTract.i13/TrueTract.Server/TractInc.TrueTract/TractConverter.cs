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
        private const float INCH_SIZE = 72;
        private const float HALF_INCH_SIZE = INCH_SIZE / 2;
        
        //"Letter" portrait page format
//        private const float LETTER_PAGE_WIDTH = ((float)8.5 * INCH_SIZE);
//        private const float LETTER_PAGE_HEIGHT = ((float)11.69 * INCH_SIZE);

        private const string PDFEXPORTDIRECTORY = "PDFExportDirectory";
        private const string PDFEXPORTURL = "PDFExportUrl";

        private float m_pageWidth;
        private float m_pageHeight;
        
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

        public string ConvertToPdf(TractInfo tractInfo, byte[] tractImageSource, byte[] scaleBarImageSource, UserInfo drawnBy, float pageWidth, float pageHeight)
        {

            m_pageWidth = pageWidth * INCH_SIZE;
            m_pageHeight = pageHeight * INCH_SIZE;

            Image scaleBarImage = Image.GetInstance(scaleBarImageSource);

            string directory = PDFExportDirectory;
            string fileName = String.Format("tract_{0}.pdf", DateTime.Now.ToString("yyyyMMdd_HH_mm"));
            
            if (!directory.EndsWith(new string(Path.DirectorySeparatorChar, 1)))
                directory += Path.DirectorySeparatorChar;

            PdfDocument document = new PdfDocument();

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(directory + fileName, FileMode.Create));

            document.SetPageSize(new Rectangle(m_pageWidth, m_pageHeight));
            document.SetMargins(HALF_INCH_SIZE, HALF_INCH_SIZE, HALF_INCH_SIZE, HALF_INCH_SIZE);

            document.AddProducer();
            document.AddCreator("True Tract LLC");
            document.AddAuthor("True Tract");

            document.Footer = getFooterChunk(drawnBy);

			document.Open();

            //----- Left side Text

            //Middle Text

            Cell documentTableCell = new Cell(getTractInfoPhrase(tractInfo));
            documentTableCell.Width = "30%";
            documentTableCell.Border = Rectangle.NO_BORDER;

            Cell tractCell = new Cell(getMainInfoPhrase(tractInfo));
            tractCell.Width = "100%";
            tractCell.Border = Rectangle.NO_BORDER;
            tractCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tractCell.Add(scaleBarImage);

            Cell document2TableCell = new Cell(getDatesChunk(tractInfo));
            document2TableCell.Border = Rectangle.NO_BORDER;
            document2TableCell.HorizontalAlignment = Element.ALIGN_RIGHT;

            float tableWidth = (m_pageWidth - INCH_SIZE);
            
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

            float borderWidth = m_pageWidth - INCH_SIZE;
//            float borderHeight = (float)((m_pageHeight * (2.0 / 3.0)));

            int len = tractInfo.ParentDocument.Buyer.AsNamed.Length + tractInfo.ParentDocument.Seller.AsNamed.Length;
            
            float tableH;
            
            if (m_pageWidth > m_pageHeight)
            {
                tableH = len > 40 ? len > 100 ? len > 180 ? len > 350 ? 220 : 200 : 190 : 180 : 170;
            } else
            {
                tableH = len > 40 ? len > 100 ? len > 180 ? len > 350 ? 240 : 220 : 200 : 190 : 180;
            }

            float borderHeight = m_pageHeight - INCH_SIZE - tableH;

            Point borderCenter = new Point(
                (int)m_pageWidth / 2,
                (int)((m_pageHeight - borderHeight - HALF_INCH_SIZE) + borderHeight / 2));

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

        private Phrase getTractInfoPhrase(TractInfo tractInfo)
        {
            Phrase phLeft = new Phrase(10);

            if (null != tractInfo.ParentDocument)
            {
                int bLen = tractInfo.ParentDocument.Buyer.AsNamed.Length;
                float bFontSize = bLen > 20 ? bLen > 50 ? bLen > 100 ? bLen > 200 ? 6 : 7 : 8 : 9 : 10;
                Chunk buyerCh = new Chunk("", FontFactory.GetFont(FontFactory.HELVETICA, bFontSize, Font.NORMAL));

                buyerCh.Append(string.Format("{0}: {1}\n", tractInfo.ParentDocument.BuyerRoleName, tractInfo.ParentDocument.Buyer.AsNamed));

                int sLen = tractInfo.ParentDocument.Seller.AsNamed.Length;
                float sFontSize = sLen > 20 ? sLen > 50 ? sLen > 100 ? sLen > 200 ? 6 : 7 : 8 : 9 : 10;
                Chunk sellerCh = new Chunk("", FontFactory.GetFont(FontFactory.HELVETICA, sFontSize, Font.NORMAL));

                sellerCh.Append(string.Format("{0}: {1}\n", tractInfo.ParentDocument.SellerRoleName, tractInfo.ParentDocument.Seller.AsNamed));

                Chunk chDocInfo = new Chunk("", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL));

                chDocInfo.Append(string.Format("State: {0}\n", tractInfo.ParentDocument.StateName));
                chDocInfo.Append(string.Format("County: {0}\n", tractInfo.ParentDocument.CountyName));
                chDocInfo.Append(string.Format("Doc Type: {0}\n", tractInfo.ParentDocument.DocumentTypeName));

                if (tractInfo.ParentDocument.DocumentNo != null)
                    chDocInfo.Append(string.Format("Doc No: {0}\n", tractInfo.ParentDocument.DocumentNo));

                if (tractInfo.ParentDocument.Volume != null)
                    chDocInfo.Append(string.Format("Vol: {0}\n", tractInfo.ParentDocument.Volume));

                if (tractInfo.ParentDocument.Page != null)
                    chDocInfo.Append(string.Format("Page: {0}\n", tractInfo.ParentDocument.Page));

                phLeft.Add(buyerCh);
                phLeft.Add(sellerCh);

                phLeft.Add(chDocInfo);
            }

            return phLeft;
        }

        private Chunk getDatesChunk(TractInfo tractInfo)
        {
            Chunk datesCh = new Chunk("", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL));

            if (null != tractInfo.ParentDocument)
            {

                String signedDate = (tractInfo.ParentDocument.DateSigned != DateTime.MinValue)
                                        ? tractInfo.ParentDocument.DateSigned.ToShortDateString()
                                        : "";

                String filedDate = (tractInfo.ParentDocument.DateFilled != DateTime.MinValue)
                                        ? tractInfo.ParentDocument.DateFilled.ToShortDateString()
                                        : "";

                datesCh.Append(string.Format("Date Signed: {0}\n", signedDate));
                datesCh.Append(string.Format("Date Filed: {0}\n", filedDate));
            }

            return datesCh;
        }

        private HeaderFooter getFooterChunk(UserInfo drawnBy)
        {
            Chunk footerÑhunk = new Chunk("", FontFactory.GetFont(FontFactory.HELVETICA, 5, Font.NORMAL));
            footerÑhunk.Append(string.Format("© Provided by scopemapping.com \nDrawn by: {0} {1}", drawnBy.FirstName, drawnBy.LastName));
            
            HeaderFooter footer = new HeaderFooter(new Phrase(footerÑhunk), false);
            footer.Alignment = Element.ALIGN_RIGHT;
            footer.BorderWidth = 0;

            return footer;
        }

        private Phrase getMainInfoPhrase(TractInfo tractInfo)
        {
            Phrase phCenter = new Phrase();

            int refLen = tractInfo.RefName.Length;
            float refFontSize = refLen > 20 ? refLen > 50 ? 12 : 14 : 18;
            Chunk refNameCh = new Chunk("", FontFactory.GetFont(FontFactory.HELVETICA, refFontSize, Font.NORMAL));
            refNameCh.Append(string.Format("{0}\n", tractInfo.RefName));

            phCenter.Add(refNameCh);

            if (null != tractInfo.ParentDocument)
            {
                int bLen = tractInfo.ParentDocument.Buyer.AsNamed.Length;
                float asNamedFontSize = bLen > 20 ? bLen > 50 ? bLen > 100 ? bLen > 200 ? 8 : 10 : 12 : 14 : 18;
                Chunk asNamedCh = new Chunk("", FontFactory.GetFont(FontFactory.HELVETICA, asNamedFontSize, Font.NORMAL));
                asNamedCh.Append(string.Format("{0}\n\n", tractInfo.ParentDocument.Buyer.AsNamed));

                phCenter.Add(asNamedCh);

                Chunk calledCh = new Chunk("", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.NORMAL));
                calledCh.Append(string.Format("Called {0} {1}\n\n\n", tractInfo.CalledAC.ToString(), tractInfo.UnitName));

                phCenter.Add(calledCh);
            }

            return phCenter;

        }

        private void drawTractBorder(PdfWriter writer, float borderWidth, float borderHeight)
        {
            PdfContentByte cb = writer.DirectContent;

            cb.SetColorStroke(Color.BLACK);
            cb.SetLineWidth(6);
            cb.Rectangle(HALF_INCH_SIZE, m_pageHeight - borderHeight - HALF_INCH_SIZE, 
                         borderWidth, borderHeight);
            cb.Stroke();

            cb.SetColorStroke(Color.WHITE);
            cb.SetLineWidth(3);
            cb.SetLineDash(10, 15, 0);
            cb.Rectangle(HALF_INCH_SIZE, m_pageHeight - borderHeight - HALF_INCH_SIZE, 
                         borderWidth, borderHeight);
            cb.Stroke();
        }

    }
}
