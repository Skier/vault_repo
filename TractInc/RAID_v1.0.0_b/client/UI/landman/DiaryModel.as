package UI.landman
{

    import App.Domain.*;
    import mx.collections.ArrayCollection;
    import weborb.data.ActiveCollection;
    import mx.collections.ListCollectionView;

    [Bindable]
    public class DiaryModel
    {

        public var billsHash:Array;
        public var asset:Asset;
        public var billDate:Date;
        public var bill:Bill;

        public var currentAssignments:ActiveCollection;
        public var currentAssignmentsFiltered:ListCollectionView;
        
        public var assignmentsHash:Array;

        public var billItemsByDay:Array;
        public var currentBillItems:ActiveCollection;
        
        public var eventGroups:ArrayCollection = new ArrayCollection();

    }

}
