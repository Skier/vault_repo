package truetract.web.client.project
{
	import truetract.domain.Project;
	
	[Bindable]
	public class ProjectFilter
	{
		public var isProjectActive:Boolean = false;
		public var isProjectComplete:Boolean = false;
		public var lastChangeFrom:Date;
		public var lastChangeTo:Date;
		
		public function accept(project:Project):Boolean 
		{
			var result:Boolean = false;
			
			if (lastChangeFrom != null && project.Changed != null && (lastChangeFrom > project.Changed))
				return false;
			if (lastChangeTo != null && project.Changed != null && (lastChangeTo < project.Changed))
				return false;

			if (isProjectActive && (project.Status == Project.PROJECT_STATUS_ACTIVE))
				result = true;
			if (isProjectComplete && (project.Status == Project.PROJECT_STATUS_COMPLETE))
				result = true;
			
			return result;
		}
	}
}