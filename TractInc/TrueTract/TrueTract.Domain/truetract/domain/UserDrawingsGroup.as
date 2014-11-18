package truetract.domain
{
public class UserDrawingsGroup extends UserGroup
{

    public static function createSystemGroup(groupName:String):UserDrawingsGroup
    {
        var result:UserDrawingsGroup = new UserDrawingsGroup();

        result.groupName = groupName;
        result.systemGroup = true;
        result.groupItems = [];

        return result;
    }

    public function UserDrawingsGroup()
    {
        filter = new DrawingsFilter();
    }
}
}