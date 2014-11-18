package UI.landman
{

    import mx.collections.ArrayCollection;
    import mx.collections.ListCollectionView;
    import App.Entity.BillDataObject;
    import App.Entity.LandmanDataObject;
    import App.Entity.DictionariesDataObject;
    import App.Entity.AssetDataObject;

    [Bindable]
    public class DiaryModel
    {

        public var billsHash:Array;
        public var asset:AssetDataObject;
        public var billDate:Date;
        public var endBillDate:Date;
        public var bill:BillDataObject;
        
        public var bills:ArrayCollection;
        public var currentBills:ArrayCollection;
        public var processedBills:ArrayCollection;

        public var currentAssignments:ArrayCollection;
        public var currentAssignmentsFiltered:ListCollectionView;
        
        public var assignmentsHash:Array;
        public var assignmentsByIdHash:Array;

        public var billItemsByDay:Array;
        public var currentBillItems:ArrayCollection;
        
        public var eventGroups:ArrayCollection = new ArrayCollection();
        
        public var clients:ArrayCollection;
        public var types:ArrayCollection;

        public var compositions:ArrayCollection = new ArrayCollection();
        public var composition:Composition;
        
        public var isReadOnly:Boolean = true;
        
        public var dictionaries:DictionariesDataObject;
        
        public var mainModel:AppModel;
        
        public var landmanData:LandmanDataObject;

    }

}
