package truetract.domain
{
[RemoteClass(alias="TractInc.TrueTract.Entity.UserGroupInfo")]
public class UserDocumentsGroup extends UserGroup
{

    public static function createSystemGroup(groupName:String):UserDocumentsGroup
    {
        var result:UserDocumentsGroup = new UserDocumentsGroup();

        result.groupName = groupName;
        result.systemGroup = true;
        result.groupItems = [];
        
        return result;
    }

    public function UserDocumentsGroup()
    {
        filter = new DocumentsFilter();
    }
    
}
}