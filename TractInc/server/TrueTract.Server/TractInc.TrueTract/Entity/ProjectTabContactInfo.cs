namespace TractInc.TrueTract.Entity
{
public class ProjectTabContactInfo
{
    public int ProjectTabContactId;
    public int ProjectTabId;

    public string ContactType;

    public string ContactName;
    public string FirstName;
    public string MiddleName;
    public string LastName;
    public string EntityRelationship;
    public string PhoneNumber;
    public string Email;
    public bool IsActive;
    public bool IsEntity;

    public int PhysicalAddressId;
    public int MailingAddressId;

    public AddressInfo PhysicalAddress;
    public AddressInfo MailingAddress;

    private const string XML_TEMPLATE = @"<contact id=""{0}"" contactName=""{1}"" firstName=""{2}"" middleName=""{3}"" lastName=""{4}"" relationship=""{5}"" phone=""{6}"" email=""{7}"" isEntity=""{8}"">
<physicalAddress>{9}</physicalAddress>
<mailingAddress>{10}</mailingAddress>
</contact>";

    public string toXml()
    {
        string physicalAddress = string.Empty;
        if (PhysicalAddress != null)
        {
            physicalAddress += PhysicalAddress.toXml();
        }

        string mailingAddress = string.Empty;
        if (MailingAddress != null)
        {
            mailingAddress += MailingAddress.toXml();
        }

        return string.Format(XML_TEMPLATE,
                             ProjectTabContactId,
                             XmlString.validate(ContactName),
                             XmlString.validate(FirstName),
                             XmlString.validate(MiddleName),
                             XmlString.validate(LastName),
                             XmlString.validate(EntityRelationship),
                             XmlString.validate(PhoneNumber),
                             XmlString.validate(Email),
                             IsEntity ? "true" : "false",
                             physicalAddress,
                             mailingAddress);
    }

    public string toSearchString()
    {
        string physicalAddress = string.Empty;
        if (PhysicalAddress != null)
        {
            physicalAddress += PhysicalAddress.toSearchString();
        }

        string mailingAddress = string.Empty;
        if (MailingAddress != null)
        {
            mailingAddress += MailingAddress.toSearchString();
        }

        return ContactName + " " 
               + FirstName + " " 
               + MiddleName + " " 
               + LastName + " " 
               + EntityRelationship + " " 
               + PhoneNumber + " " 
               + Email + " " 
               + physicalAddress + " " 
               + mailingAddress;
    }
}
}
