using System;
using System.Collections.Generic;
using System.Text;
using RestSharp.Contrib;

namespace Twilio.TwiML
{
    public class Verb
    {
        protected string Tag;

        protected string Body;

        protected Dictionary<string, string> Attributes;

        protected List<Verb> Children;

        protected List<string> AllowedVerbs;

        public const string VSay = "Say";
        public const string VPlay = "Play";
        public const string VGather = "Gather";
        public const string VRecord = "Record";
        public const string VPause = "Pause";
        public const string VHangup = "Hangup";
        public const string VDial = "Dial";
        public const string VNumber = "Number";
        public const string VRedirect = "Redirect";
        public const string VResponse = "Response";
        public const string VConference = "Conference";
        public const string VSms = "Sms";

        public Verb(string tag, string body)
        {
            Tag = tag;
            Body = body;
            Attributes = new Dictionary<string, string>();
            Children = new List<Verb>();
        }

        public Verb Append(Verb verb)
        {
            if (AllowedVerbs != null && AllowedVerbs.Contains(verb.GetTag()))
            {
                Children.Add(verb);
                return verb;
            }
            
            throw new TwiMLException("This is not a supported verb");
        }

        public string ToXmlString()
        {
            var xml = "<" + Tag;
            foreach (var attribute in Attributes)
            {
                xml += (" " + attribute.Key + "=\"" + attribute.Value + "\"");
            }
            xml += ">";

            if (Body != null)
                xml += Body;

            foreach (var child in Children)
            {
                xml += child.ToXmlString();
            }

            xml += ("</" + Tag + ">");
            
            return xml;
        }

        public string AsUrl()
        {
            try
            {
                return HttpUtility.UrlEncode(ToXmlString(), Encoding.UTF8);
            }
            catch (Exception e)
            {
                throw new TwiMLException(e.Message);
            }
        }

        public void Set(string key, string value)
        {
            Attributes[key] = value;
        }

        public string GetBody()
        {
            return Body;
        }

        public string GetTag()
        {
            return Tag;
        }

        public List<Verb> GetChildren()
        {
            return Children;
        }

        public Dictionary<string, string> GetAttributes()
        {
            return Attributes;
        }
    }
}

