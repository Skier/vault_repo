package truetract.domain
{
import mx.collections.ArrayCollection;

[Bindable]
[RemoteClass(alias="TractInc.TrueTract.Entity.ProjectInfo")]
public class Project
{
    public var ProjectId:int;
    public var Name:String;
    public var ShortName:String;
    public var ClientId:int;

    public var Attachments:Array;

    private var _tabsList:ArrayCollection = new ArrayCollection();
    public function get TabsList():ArrayCollection { return _tabsList; }

    private var _tabs:Array;
    public function get Tabs():Array { return _tabs; }
    public function set Tabs(value:Array):void 
    {
        TabsList.source = _tabs = value;
        
        if (value && value.length > 0)
        {
            for each (var tab:ProjectTab in value)
            {
                tab.TabProject = this;
            }
        }
    }

    public function get children():ArrayCollection
    {
        return TabsList;
    }

    public function addTab(tab:ProjectTab):ProjectTab
    {
        tab.ProjectId = ProjectId;
        tab.TabProject = this;

        TabsList.addItem(tab);

        return tab;
    }

}
}