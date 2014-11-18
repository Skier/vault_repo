using System.Collections.Generic;
using System.Web;

namespace Dalworth.LeadCentral.Web.Models
{
    public class Flash
    {
        private const string FlashSessionKey = "MessagesModels";

        private List<string> Infos { get; set; }
        private List<string> Warnings { get; set; }
        private List<string> Errors { get; set; }

        private Flash()
        {
            Infos = new List<string>();
            Warnings = new List<string>();
            Errors = new List<string>();
        }

        public static Flash GetCurrent()
        {
            var session = HttpContext.Current.Session;
            if (session[FlashSessionKey] == null)
                session[FlashSessionKey] = new Flash();

            return (Flash)session[FlashSessionKey];
        }

        public void PushInfo(string message)
        {
            Infos.Add(message);
        }

        public void PushWarning(string message)
        {
            Warnings.Add(message);
        }

        public void PushError(string message)
        {
            Errors.Add(message);
        }

        public List<string> PopInfos()
        {
            var result = new List<string>();
            
            result.AddRange(Infos);
            Infos.Clear();

            return result;
        }

        public List<string> PopWarnings()
        {
            var result = new List<string>();

            result.AddRange(Warnings);
            Warnings.Clear();

            return result;
        }

        public List<string> PopErrors()
        {
            var result = new List<string>();

            result.AddRange(Errors);
            Errors.Clear();

            return result;
        }

    }
}