using System;
using System.Configuration;
using System.Globalization;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using XMLViewService.Entity;

namespace XMLViewService
{

    public class MainService
    {

        public CatalogDataObject GetCatalog(int id)
        {
            FileStream stream = new FileStream(ConfigurationManager.AppSettings["XMLViewDataSource"], FileMode.Open, FileAccess.Read);

            CatalogDataObject catalogInfo = new CatalogDataObject();

            using (XmlTextReader reader = new XmlTextReader(stream))
            {
                while (reader.Read())
                {
                    if (("DOCUMENT" == reader.Name.ToUpper())
                            && (XmlNodeType.Element == reader.NodeType))
                    {
                        ParseDocument(reader, catalogInfo);
                    }
                }
            }

            return catalogInfo;
        }

        private void ParseDocument(XmlTextReader reader, CatalogDataObject catalogInfo)
        {
            while (reader.Read())
            {
                if (XmlNodeType.Element != reader.NodeType)
                {
                    continue;
                }

                if ("METADATA" == reader.Name.ToUpper())
                {
                    ParseMetadata(reader, catalogInfo);
                }
                else if ("PAGE" == reader.Name.ToUpper())
                {
                    PageDataObject pageInfo = new PageDataObject();

                    ParsePage(reader, pageInfo);

                    catalogInfo.Pages.Add(pageInfo);
                }
            }
        }

        private void ParseMetadata(XmlTextReader reader, CatalogDataObject catalogInfo)
        {
            while (reader.Read())
            {
                if (XmlNodeType.Element != reader.NodeType)
                {
                    continue;
                }

                if ("PDFFILENAME" == reader.Name)
                {
                    reader.Read();

                    catalogInfo.CatalogName = reader.ReadContentAsString();

                    break;
                }
            }
        }

        private void ParsePage(XmlTextReader reader, PageDataObject pageInfo)
        {
            pageInfo.Width = Decimal.Parse(reader.GetAttribute("width").Replace(".", ","));
            pageInfo.Height = Decimal.Parse(reader.GetAttribute("height").Replace(".", ","));
            pageInfo.ID = reader.GetAttribute("id");

            while (reader.Read())
            {
                if (XmlNodeType.Element == reader.NodeType)
                {
                    if ("TEXT" == reader.Name)
                    {
                        TextDataObject textInfo = new TextDataObject();

                        ParseText(reader, textInfo);

                        pageInfo.Texts.Add(textInfo);
                    }
                }

                if ((XmlNodeType.EndElement == reader.NodeType)
                        && ("PAGE" == reader.Name))
                {
                    break;
                }
            }
        }

        private void ParseText(XmlTextReader reader, TextDataObject textInfo)
        {
            textInfo.Height = Int32.Parse(reader.GetAttribute("width"));
            textInfo.Width = Int32.Parse(reader.GetAttribute("width"));

            while (reader.Read())
            {
                if (XmlNodeType.Element == reader.NodeType)
                {
                    if ("TOKEN" == reader.Name)
                    {
                        TokenDataObject tokenInfo = new TokenDataObject();

                        ParseToken(reader, tokenInfo);

                        textInfo.Tokens.Add(tokenInfo);
                    }
                }

                if ((XmlNodeType.EndElement == reader.NodeType)
                        && ("TEXT" == reader.Name))
                {
                    break;
                }
            }
        }

        private void ParseToken(XmlTextReader reader, TokenDataObject tokenInfo)
        {
            tokenInfo.Angle = Int32.Parse(reader.GetAttribute("angle"));
            tokenInfo.FontColor = Int32.Parse(reader.GetAttribute("font-color").Substring(1), NumberStyles.HexNumber);
            tokenInfo.FontName = reader.GetAttribute("font-name");
            tokenInfo.FontSize = Int32.Parse(reader.GetAttribute("font-size"));
            tokenInfo.Height = Int32.Parse(reader.GetAttribute("height"));
            tokenInfo.ID = reader.GetAttribute("id");
            tokenInfo.IsBold = (reader.GetAttribute("bold") == "yes") ? true : false;
            tokenInfo.IsItalic = (reader.GetAttribute("italic") == "yes") ? true : false;
            tokenInfo.Left = Int32.Parse(reader.GetAttribute("x"));
            tokenInfo.Rotation = Int32.Parse(reader.GetAttribute("rotation"));
            tokenInfo.Top = Int32.Parse(reader.GetAttribute("y"));
            tokenInfo.Width = Int32.Parse(reader.GetAttribute("y"));

            while (reader.Read())
            {
                if (XmlNodeType.Text == reader.NodeType)
                {
                    tokenInfo.Text = reader.ReadString();
                }

                if ((XmlNodeType.EndElement == reader.NodeType)
                        && ("TOKEN" == reader.Name))
                {
                    break;
                }
            }
        }

    }

}
