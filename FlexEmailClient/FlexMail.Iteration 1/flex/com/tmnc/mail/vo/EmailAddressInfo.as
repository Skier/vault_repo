package com.tmnc.mail.vo
{
    import mx.utils.StringUtil;
    
    
[RemoteClass(alias="Weborb.Samples.Email.Entities.EmailAddressInfo")]
[Bindable]
public class EmailAddressInfo {

    public var Name:String;
    public var Address:String;
    
    public function get DisplayValue():String {
        var result:String = "";
        
        if (Name && Name.length > 0 && Name != Address){
            result = Name;
        }
        
        if (Address && Address.length > 0){
            if (result.length > 0) result += " ";
            result += "<" + Address + ">";            
        }
        
        return result;
    }
 
    public static function createFromString(s:String):EmailAddressInfo {
        var result:EmailAddressInfo  = new EmailAddressInfo();
        
        var i:int = s.lastIndexOf(" ");
        
        if (i >= 0) {
            
          result.Name = StringUtil.trim(s.substr(0,i));
          result.Address = s.substr(i + 1);
          
        } else {
            
          result.Name = "";
          result.Address = s;
          
        }
        
        result.Address = StringUtil.trim(result.Address);
        result.Address = result.Address.replace("<", "");
        result.Address = result.Address.replace(">", "");
        
        result.Name = result.Name.replace("\"", "");        
        return result;
    }
}
}

