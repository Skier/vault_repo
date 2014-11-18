using System;

namespace TractInc.TrueTract.Entity
{
public class AddressInfo
{
    private const string XML_TEMPLATE = @"<address id=""{0}"" address1=""{1}"" address2=""{2}"" city=""{3}"" state=""{4}"" zip=""{5}""/>";

    public int AddressId;
    public string Address1;
    public string Address2;
    public string City;
    public int State;
    public string StateName;
    public string Zip;

    public string toXml()
    {
        return String.Format(XML_TEMPLATE,
                             AddressId,
                             XmlString.validate(Address1),
                             XmlString.validate(Address2),
                             XmlString.validate(City),
                             XmlString.validate(StateName),
                             XmlString.validate(Zip));
    }

    public string toSearchString()
    {
        return Address1 + " " 
               + Address2 + " " 
               + City + " " 
               + StateName + " " 
               + Zip;
    }
}
}
