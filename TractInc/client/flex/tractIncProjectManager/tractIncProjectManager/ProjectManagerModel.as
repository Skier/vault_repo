package tractIncProjectManager
{
    import tractInc.domain.packages.ProjectManagerPackage;
    
    [Bindable]
    public class ProjectManagerModel
    {
        public var isBusy:Boolean = false;
        
        public var projectManagerPackage:ProjectManagerPackage = null;
    }
}