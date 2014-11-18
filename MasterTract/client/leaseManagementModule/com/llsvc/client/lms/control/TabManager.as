package com.llsvc.client.lms.control
{
    import com.llsvc.client.lms.events.LeaseEvent;
    import com.llsvc.client.lms.view.assignment.AssignmentPanel;
    import com.llsvc.client.lms.view.assignment.AssignmentsPanel;
    import com.llsvc.client.lms.view.lease.LeasesPanel;
    import com.llsvc.client.lms.view.lease.detail.LeaseDetailView;
    import com.llsvc.client.lms.view.lease.search.LeaseSearchTabPanel;
    import com.llsvc.client.lms.view.project.ProjectsTabPanel;
    import com.llsvc.domain.Lease;
    import com.llsvc.domain.LeaseAssignment;
    
    import flash.events.Event;
    
    import mx.collections.ArrayCollection;
    import mx.containers.TabNavigator;
    import mx.core.Container;
    import mx.events.ChildExistenceChangedEvent;
    import mx.events.DynamicEvent;
    
    public class TabManager
    {
        private static var tabNav:TabNavigator;
        
        public static var uniqueTabs:Object = new Object();

        public static function set tabNavigator(tabNavigator:TabNavigator):void
        {
            tabNav = tabNavigator;
            tabNav.addEventListener(ChildExistenceChangedEvent.CHILD_REMOVE,
                function (event:ChildExistenceChangedEvent):void
                {
                    for (var uniqueId:String in uniqueTabs)
                    {
                        if (uniqueTabs[uniqueId] == event.relatedObject)
                        {
                            uniqueTabs[uniqueId] = null;
                            return;
                        }
                    }
                });
        }
        
        public static function openTab(tabClass:Class, uniqueId:String=null):Container
        {
            if (uniqueId && uniqueTabs[uniqueId])
            {
                tabNav.selectedChild = uniqueTabs[uniqueId];
                return uniqueTabs[uniqueId];
            }

            var tab:Container = new tabClass();

            if (uniqueId)
            {
                uniqueTabs[uniqueId] = tab;
            }
            
            tabNav.addChild(tab);
            
            tabNav.selectedChild = tab;

            return tab;
        }
        
        public static function setUniqueTab(uniqueId:String, tab:Container):void
        {
            uniqueTabs[uniqueId] = tab;     
        }

        public static function openLease(lease:Lease):LeaseDetailView
        {
            var tab:LeaseDetailView; 
            
            if (lease == null) {
            	lease = new Lease(); 
            	tab = TabManager.openTab(LeaseDetailView) as LeaseDetailView;
            } else {
            	tab = TabManager.openTab(LeaseDetailView, "LEASE DETAIL:" + lease.document.id.toString()) as LeaseDetailView;
            }

            tab.lease = new Lease();
            tab.lease.populate(lease);
            tab.addEventListener("closeTabRequest", closeTabRequestHandler);
            tab.addEventListener(LeaseEvent.CREATE_LEASE, createLeaseHandler);
            tab.addEventListener(LeaseEvent.REMOVE_LEASE, removeLeaseHandler);
            tab.addEventListener(LeaseEvent.UPDATE_LEASE, updateLeaseHandler);

            return tab;
        }
        
        public static function openAssignment(assignment:LeaseAssignment):AssignmentPanel
        {
            var tab:AssignmentPanel;
            
            if (assignment == null) {
            	assignment = new LeaseAssignment();
            	tab = TabManager.openTab(AssignmentPanel) as AssignmentPanel;
            } else {
            	tab = TabManager.openTab(AssignmentPanel, "ASSIGNMENT:" + assignment.document.id.toString()) as AssignmentPanel;
            } 
            
            tab.assignment = assignment;
            tab.addEventListener("closeTabRequest", closeTabRequestHandler);
            return tab;
        }

        public static function openLeaseList():LeasesPanel
        {
            var tab:LeasesPanel = TabManager.openTab(LeasesPanel, "LEASE_LIST") as LeasesPanel;

           	tab.init();

            tab.addEventListener("closeTabRequest", closeTabRequestHandler);
            tab.addEventListener("openLeaseRequest", openLeaseRequestHandler);
            return tab;
        }
        
        public static function openLeaseSearch():LeaseSearchTabPanel
        {
            var tab:LeaseSearchTabPanel = TabManager.openTab(LeaseSearchTabPanel, "LEASE_SEARCH") as LeaseSearchTabPanel;

           	tab.init();

            tab.addEventListener("closeTabRequest", closeTabRequestHandler);
            tab.addEventListener("openLeaseRequest", openLeaseRequestHandler);
            return tab;
        }
        
        public static function openProjectList():ProjectsTabPanel
        {
            var tab:ProjectsTabPanel = TabManager.openTab(ProjectsTabPanel, "PROJECT_LIST") as ProjectsTabPanel;

           	tab.init();

            tab.addEventListener("closeTabRequest", closeTabRequestHandler);
            tab.addEventListener("openLeaseRequest", openLeaseRequestHandler);
            return tab;
        }
        
        public static function openAssignmentList(collection:ArrayCollection = null):AssignmentsPanel
        {
            var tab:AssignmentsPanel = TabManager.openTab(AssignmentsPanel, "ASSIGNMENT_LIST") as AssignmentsPanel;

            if (collection != null) {
	            tab.assignments = collection;  
            } else {
	            tab.reset();
            }  

            tab.addEventListener("closeTabRequest", closeTabRequestHandler);
            return tab;
        }
        
        public static function removeTab(tab:Container):void
        {   
            tabNav.removeChild(tab);
        }
        
        public static function getUniqueTab(uniqueId:String):Container
        {
            return uniqueTabs[uniqueId];
        }
        
        private static function closeTabRequestHandler(event:Event):void 
        {
        	var tab:Container = event.currentTarget as Container;
        	removeTab(tab);
        }
        
        private static function createLeaseHandler(event:LeaseEvent):void 
        {
        	var tab:LeasesPanel = getUniqueTab("LEASE_LIST") as LeasesPanel;
        	if (tab != null)
        		tab.createLease(event.lease);
        }
        
        private static function updateLeaseHandler(event:LeaseEvent):void 
        {
        	var tab:LeasesPanel = getUniqueTab("LEASE_LIST") as LeasesPanel;
        	if (tab != null)
        		tab.updateLease(event.lease);
        }
        
        private static function removeLeaseHandler(event:LeaseEvent):void 
        {
        	var tab:LeasesPanel = getUniqueTab("LEASE_LIST") as LeasesPanel;
        	if (tab != null)
        		tab.removeLease(event.lease);
        }
        
        private static function updateLeaseListRequestHandler(event:Event):void 
        {
        	var tab:LeasesPanel = getUniqueTab("LEASE_LIST") as LeasesPanel;
        	if (tab != null)
        		tab.init();
        }
        
        private static function openLeaseRequestHandler(event:DynamicEvent):void 
        {
        	var lease:Lease = event.lease as Lease;
        	TabManager.openLease(lease);
        }

    }
}