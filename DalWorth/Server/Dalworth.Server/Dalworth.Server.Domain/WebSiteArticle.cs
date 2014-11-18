using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Dalworth.Server.Data;

  
namespace Dalworth.Server.Domain
{
    public partial class WebSiteArticle
    {
        public WebSiteArticle()
        {

        }

        private const String SqlSelectByURL = "Select "


        + " ID, "

        + " WebSiteArticleCategoryId, "

        + " WebSiteArticleTypeId, "

        + " Url, "

        + " MenuId, "

        + " DatePublished "

      + " From WebSiteArticle "


        + " Where "

        + " Url = ?Url";

        public static WebSiteArticle FindByURL(string url, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByURL, connection))
            {

                Database.PutParameter(dbCommand, "?Url", url);


                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }
            throw new DataNotFoundException("WebSiteArticle not found, search by primary key");
        }

        public static BindingList<WebSiteArticlePartWrapper> GetWebSiteArticlePartWrapperWithBreadCrumTitle(int? webSiteArticleCategoryId, int? websitearticletypeid, IDbConnection connection)
        {
            string sql = @" 
           SELECT w.*, part.* FROM websitearticle w
            join websitearticlepart part on w.id = part.websitearticleid
            where 
            part.WebSiteArticlePartTypeId = 12 
            and w.datepublished is not null";

            if (websitearticletypeid.HasValue)
            {
                sql += " and websitearticletypeid = ?ArticleTypeId";
            }
           
           if (webSiteArticleCategoryId.HasValue)
           {
               sql = sql +
               @" and w.WebSiteArticleCategoryId = ?CategoryId"; 
           }
           else
           {
               sql = sql + " and w.WebSiteArticleCategoryId <> 10";
           }

            sql = sql+ 
            @" order by datepublished desc
            LIMIT 0,10
            ";

            BindingList<WebSiteArticlePartWrapper> wrappers = new BindingList<WebSiteArticlePartWrapper>();

            using (IDbCommand dbCommand = Database.PrepareCommand(sql, connection))
            {
                if (webSiteArticleCategoryId.HasValue)
                {
                    Database.PutParameter(dbCommand, "?CategoryId", webSiteArticleCategoryId.Value);
                }

                if (websitearticletypeid.HasValue)
                {
                    Database.PutParameter(dbCommand, "?ArticleTypeId", websitearticletypeid.Value);
                }

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while(dataReader.Read())
                    {
                        WebSiteArticle article = Load(dataReader);
                        WebSiteArticlePart part = WebSiteArticlePart.Load(dataReader, FieldsCount);
                        wrappers.Add(new WebSiteArticlePartWrapper(article, part));
                    }           
                }
            }

            return wrappers;
        }
    }

    public class WebSiteArticlePartWrapper
    {
        private WebSiteArticle m_article;

        public WebSiteArticle Article
        {
            get { return m_article; }
        }

        private WebSiteArticlePart m_part;
        
        public WebSiteArticlePartWrapper () {}

        public WebSiteArticlePartWrapper (WebSiteArticle article, WebSiteArticlePart part)
        {
            m_article = article;
            m_part = part;
        }

        public string Title 
        {
            get { return m_part.ContentText; }
        }

        public string Url 
        {
            get {return m_article.Url;}
            set { m_article.Url = value;}
        }

        private double m_priority;
        public double Priority
        {
            get { return m_priority;}
            set { m_priority = value; }
        }

    }
}
      