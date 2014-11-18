using System.Web;

namespace Titus.ECatalog.Service
{

    class HttpUrlHelper
    {

        public static string AbsoluteRoot
        {
            get
            {
                string absoluteRoot = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host;

                if (HttpContext.Current.Request.Url.Port != 80)
                    absoluteRoot += ":" + HttpContext.Current.Request.Url.Port.ToString();

                if (HttpContext.Current.Request.ApplicationPath.Length > 0)
                    absoluteRoot += HttpContext.Current.Request.ApplicationPath;

                return absoluteRoot;
            }
        }

    }

}
