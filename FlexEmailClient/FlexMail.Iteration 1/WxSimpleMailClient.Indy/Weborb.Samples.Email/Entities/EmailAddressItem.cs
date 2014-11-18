using IndyEmailAddressItem = Indy.Sockets.EMailAddressItem;

namespace Weborb.Samples.Email.Entities
{
public class EmailAddressItem {

    private string _name;
    private string _address;
    private string _text;

    public EmailAddressItem() {
    }

    public EmailAddressItem(string name, string address, string text) {
        _name = name;
        _address = address;
        _text = text;
    }

    public EmailAddressItem(string text) {
        _text = text;
    }
    
    public EmailAddressItem(IndyEmailAddressItem indyAddress) {
        _name = indyAddress.Name;
        _address = indyAddress.Address;
        _text = indyAddress.Text;
    }
    
    public string Name {
        get { return _name; }
        set { _name = value; }
    }

    public string Address {
        get { return _address; }
        set { _address = value; }
    }

    public string Text {
        get { return _text; }
        set { _text = value; }
    }

    public IndyEmailAddressItem ToIndyEmailAddressItem() {
        IndyEmailAddressItem result =  new IndyEmailAddressItem();
        result.Name = _name;
        result.Text = _text;
        result.Address = _address;
        
        return result;
    }
    
    public static implicit operator EmailAddressItem(IndyEmailAddressItem indyAddressItem) {
        return new EmailAddressItem(indyAddressItem);
    }

    public static implicit operator IndyEmailAddressItem(EmailAddressItem indyAddressItem) {
        return indyAddressItem.ToIndyEmailAddressItem();
    }

}
}
