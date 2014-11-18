using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Dalworth.Server.Web.Partner.Paging
{
	public class Pager
	{
		private ViewContext m_viewContext;
		private readonly int m_pageSize;
		private readonly int m_currentPage;
		private readonly int m_totalItemCount;
		private readonly RouteValueDictionary m_linkWithoutPageValuesDictionary;

        //Page number start from 1
		public Pager(ViewContext viewContext, int pageSize, int currentPage, int totalItemCount, RouteValueDictionary valuesDictionary)
		{            
			this.m_viewContext = viewContext;
			this.m_pageSize = pageSize;
			this.m_currentPage = currentPage;
            if (m_currentPage < 1)
                m_currentPage = 1;
			this.m_totalItemCount = totalItemCount;
			this.m_linkWithoutPageValuesDictionary = valuesDictionary;            
		}

		public string RenderHtml()
		{
			int totalPagesCount = (int)Math.Ceiling(this.m_totalItemCount / (double)this.m_pageSize);

			var result = new StringBuilder();
		    result.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" id=\"paging-table\"><tr><td>");
            if (m_currentPage <= 1)
            {
                result.Append("<a class=\"page-far-left\"></a>");
                result.Append("<a class=\"page-left\"></a>");
            }
            else
            {
                result.Append(string.Format("<a href=\"{0}\" class=\"page-far-left\"></a>", GeneratePageLink(1)));
                result.Append(string.Format("<a href=\"{0}\" class=\"page-left\"></a>", 
                    GeneratePageLink(m_currentPage - 1)));
            }

            result.Append(string.Format("<div id=\"page-info\">Page <strong>{0}</strong> / {1}</div>",
                totalPagesCount == 0 ? 0 : m_currentPage, totalPagesCount));

            if (m_currentPage >= totalPagesCount)
            {
                result.Append("<a class=\"page-right\"></a>");
                result.Append("<a class=\"page-far-right\"></a>");                
            }
            else
            {
                result.Append(string.Format("<a href=\"{0}\" class=\"page-right\"></a>", 
                    GeneratePageLink(m_currentPage + 1)));
                result.Append(string.Format("<a href=\"{0}\" class=\"page-far-right\"></a>", 
                    GeneratePageLink(totalPagesCount)));
            }
		    result.Append("</td><td>");
		    string selectId = Guid.NewGuid().ToString();
            result.Append(string.Format("<select id=\"{0}\" class=\"styledselect_pages\" onchange=\"document.location.href = document.getElementById('{1}').value;\"><option value=\"\">Go to page</option>",
                selectId, selectId));

            for (int i = 1; i <= totalPagesCount; i++)
            {
                if (i == m_currentPage)
                    continue;
                result.Append(string.Format("<option value=\"{0}\">{1}</option>", GeneratePageLink(i), i));
            }
            result.Append("</select></td></tr></table>");

			return result.ToString();
		}

		private string GeneratePageLink(int pageNumber)
		{
			var pageLinkValueDictionary = new RouteValueDictionary(this.m_linkWithoutPageValuesDictionary);
            foreach (string key in m_viewContext.RequestContext.HttpContext.Request.QueryString.AllKeys)
            {
                if (key == "page")
                    continue;
                pageLinkValueDictionary.Add(key, m_viewContext.RequestContext.HttpContext.Request.QueryString.Get(key));
            }
                
            pageLinkValueDictionary.Add("page", pageNumber);


			//var virtualPathData = this.viewContext.RouteData.Route.GetVirtualPath(this.viewContext, pageLinkValueDictionary);
			var virtualPathData = RouteTable.Routes.GetVirtualPath(this.m_viewContext.RequestContext, pageLinkValueDictionary);
            //this.viewContext.RequestContext.HttpContext.Request.QueryString

			if (virtualPathData != null)
			    return virtualPathData.VirtualPath;
			return null;
		}
	}
}