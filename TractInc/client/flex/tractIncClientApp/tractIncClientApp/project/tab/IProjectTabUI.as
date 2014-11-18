package tractIncClientApp.project.tab
{
	import truetract.domain.ProjectTab;
	
	[Bindable]
	public interface IProjectTabUI
	{
		function get projectTab():ProjectTab;
		function set projectTab(projectTab:ProjectTab):void;
		
		function set selected(value:Boolean):void;
		function get selected():Boolean;
	}
}