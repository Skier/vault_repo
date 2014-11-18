package Domain
{
    import mx.utils.StringUtil;
    
[RemoteClass(alias="Weborb.Samples.Email.Entities.EmailAddressInfo")]
[Bindable]
public class MailAddress {

    public var Name:String;
    public var Address:String;
    public var DisplayValue:String;
 
    public static function createFromString(s:String):MailAddress {
        var result:MailAddress = new MailAddress();
        
        var emailStartPosition:int = s.lastIndexOf( " " );
        
        if (emailStartPosition == -1)
            emailStartPosition = s.lastIndexOf( "<" );
            
        if (emailStartPosition != -1) 
        {
          result.Name = StringUtil.trim( s.substr( 0, emailStartPosition ) );
          result.Address = s.substr( emailStartPosition + 1 );
        } 
        else 
        {
          result.Name = "";
          result.Address = s;
        }
        
        result.Address = StringUtil.trim( result.Address );
        result.Address = result.Address.replace( /[<>]/g, "" );
        result.Name = result.Name.replace( /[\/"]/g, "" );
        return result;
    }
    
    public static function AddressListToString(addressList:Array):String 
    {
        var result:String = "";
        
        for (var i:int = 0; i < addressList.length; i++)
        {
            result += addressList[i].DisplayValue;
            
            if (i != addressList.length - 1)
            {
                result += ", ";
            }
        }
        
        return result;
    }
    
}
}