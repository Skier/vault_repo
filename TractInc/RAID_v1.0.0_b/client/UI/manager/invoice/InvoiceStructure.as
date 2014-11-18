package UI.manager.invoice
{
    import App.Domain.Invoice;
    import mx.collections.ArrayCollection;
    import App.Domain.BillItem;
    import App.Domain.InvoiceItem;
    import weborb.data.DynamicLoadEvent;
    import App.Domain.Afe;
    import App.Domain.Asset;
    import App.Domain.ActiveRecords;
    import App.Domain.AssetAssignment;
    import App.Domain.InvoiceItemDataMapper;
    import common.StatusesRegistry;
    import flash.events.Event;
    import mx.events.CollectionEvent;
    import mx.rpc.events.FaultEvent;
    import App.Domain.SubAfe;
    import mx.rpc.Responder;
    import App.Domain.InvoiceItemType;
    import App.Domain.Note;
    
    [Bindable]
    public class InvoiceStructure extends InvoiceItemGroup
    {
        public var invoice:Invoice;
        public var afeGroups:ArrayCollection = new ArrayCollection();
        public var additionalItems:ArrayCollection = new ArrayCollection();
        
        public var isLoaded:Boolean = false;

        private var _afes:ArrayCollection = new ArrayCollection();

        private var _subAfes:ArrayCollection = new ArrayCollection();

        private var _assets:ArrayCollection = new ArrayCollection();
        
        private var _assetAssignments:ArrayCollection = new ArrayCollection();
        
        private var _types:ArrayCollection = new ArrayCollection();

        public function init(actualInvoice:Invoice):void 
        {
            isLoaded = false;
            invoice = actualInvoice;
            afeGroups = new ArrayCollection();
//          invoice.loadNotes();
            LoadTypes();
        }
        
        public function setStatus(status:String):void 
        {
            invoice.RelatedInvoiceStatus = StatusesRegistry.getInstance().getInvoiceStatusByName(status);

            for each (var invoiceItem:InvoiceItem in invoice.RelatedInvoiceItem) {
                invoiceItem.RelatedInvoiceItemStatus = StatusesRegistry.getInstance().getInvoiceItemStatusByName(status);
            }
        }
        
        public function save(cascade:Boolean, responder:Responder):void 
        {
            if (deletedItems.length == 0) {
                invoice.save(cascade, responder);
            }
            
            for each (var item:InvoiceItem in deletedItems) {
                item.remove(new Responder(
                    function (item:InvoiceItem):void {
                        var index:int = deletedItems.getItemIndex(item);
                        if (index >= 0) {
                            deletedItems.removeItemAt(index);
                        }
                        if (deletedItems.length == 0) {
                            invoice.save(cascade, responder);
                        }
                    }, onFault));
            }
        }
        
        public override function removeItem(item:InvoiceItem):void 
        {
            var index:int;
            
            if (additionalItems.contains(item)) {
                index = additionalItems.getItemIndex(item);
                if (index >= 0) {
                    additionalItems.removeItemAt(index);
                }
            }

            if (invoice.RelatedInvoiceItem.contains(item)) {
                index = invoice.RelatedInvoiceItem.getItemIndex(item);
                if (index >= 0) {
                    invoice.RelatedInvoiceItem.removeItemAt(index);
                }
            }

            super.removeItem(item);
        }
        
        private function LoadTypes():void 
        {
            var types:ArrayCollection = new ArrayCollection();
            types = ActiveRecords.InvoiceItemType.findAll();
            types.addEventListener("loaded", 
                function(event:DynamicLoadEvent):void {
                    _types.removeAll();
                    for each (var type:InvoiceItemType in event.data as ArrayCollection) {
                        _types.addItem(type);
                    }
                    LoadAfes();
                });
            types.addEventListener(FaultEvent.FAULT, onFault);
        }
        
        private function LoadAfes():void 
        {
            var afes:ArrayCollection = new ArrayCollection();
            afes = ActiveRecords.Afe.findAll();
            afes.addEventListener("loaded", 
                function(event:DynamicLoadEvent):void {
                    _afes.removeAll();
                    for each (var afe:Afe in event.data as ArrayCollection) {
                        _afes.addItem(afe);
                    }
                    LoadSubAfes();
                });
            afes.addEventListener(FaultEvent.FAULT, onFault);
        }
        
        private function LoadSubAfes():void 
        {
            var subAfes:ArrayCollection = new ArrayCollection();
            subAfes = ActiveRecords.SubAfe.findAll();
            subAfes.addEventListener("loaded", 
                function(event:DynamicLoadEvent):void {
                    _subAfes.removeAll();
                    for each (var subAfe:SubAfe in event.data as ArrayCollection) {
                        _subAfes.addItem(subAfe);
                    }
                    LoadAssets();
                });
            subAfes.addEventListener(FaultEvent.FAULT, onFault);
        }
        
        private function LoadAssets():void 
        {
            var assets:ArrayCollection = new ArrayCollection();
            assets = ActiveRecords.Asset.findAll({loadRelations:["RelatedAssetType"]});
            assets.addEventListener("loaded", 
                function(event:DynamicLoadEvent):void {
                    _assets.removeAll();
                    for each (var asset:Asset in event.data as ArrayCollection) {
                        _assets.addItem(asset);
                    }
                    LoadAssetAssignments();
                });
            assets.addEventListener(FaultEvent.FAULT, onFault);
        }
        
        private function LoadAssetAssignments():void 
        {
            var assetAssignments:ArrayCollection = new ArrayCollection();
            assetAssignments = ActiveRecords.AssetAssignment.findAll();
            assetAssignments.addEventListener("loaded", 
                function(event:DynamicLoadEvent):void {
                    _assetAssignments.removeAll();
                    for each (var assetAssignment:AssetAssignment in event.data as ArrayCollection) {
                        assetAssignment.RelatedAfe = getAfeById(assetAssignment.AFE);
                        assetAssignment.RelatedSubAfe = getSubAfeById(assetAssignment.SubAFE);
                        assetAssignment.RelatedAsset = getAssetById(assetAssignment.AssetId);
                        _assetAssignments.addItem(assetAssignment);
                    }
                    LoadItems();
                });
            assetAssignments.addEventListener(FaultEvent.FAULT, onFault);
        }
        
        private function LoadItems():void 
        {
            if (!invoice.RelatedInvoiceItem.IsLoaded) {
                invoice.RelatedInvoiceItem.addEventListener("loaded", onItemsLoaded);
            } else {
                parseItems();
            }

        }
        
        private function onItemsLoaded(event:DynamicLoadEvent):void 
        {
            invoice.RelatedInvoiceItem.removeEventListener("loaded", onItemsLoaded);
            parseItems();
        }

        private function parseItems():void 
        {
            for each (var invoiceItem:InvoiceItem in invoice.RelatedInvoiceItem) {
                invoiceItem.RelatedAssetAssignment = getAssetAssignmentById(invoiceItem.AssetAssignmentId);
                addItem(invoiceItem);
                
                invoiceItem.loadNotes();
            }
            isLoaded = true;
            dispatchEvent(new Event("invoice_structure_loaded"));
        }

        public override function addItem(item:InvoiceItem):void 
        {
            super.addItem(item);
            
            if (item.RelatedBillItem == null ) {
                additionalItems.addItem(item);
            } else {
                var group:InvoiceItemGroupByAfe = getAfeGroupByAfe(item.RelatedAssetAssignment.AFE);
                if (group == null) {
                    group = new InvoiceItemGroupByAfe;
                    group.parentInvoice = invoice;
                    group.afe = item.RelatedAssetAssignment.RelatedAfe;
                    afeGroups.addItem(group);
                }
                group.addItem(item);
            }
            
            if (!invoice.RelatedInvoiceItem.contains(item)) {
                invoice.RelatedInvoiceItem.addItem(item);
            }
            
        }
        
        public function getItemTypes():ArrayCollection 
        {
            return _types;
        }
        
        public function getAfes():ArrayCollection 
        {
            return _afes;
        }
        
        public function getSubAfes():ArrayCollection 
        {
            return _subAfes;
        }
        
        public function getAssets():ArrayCollection 
        {
            return _assets;
        }
        
        public function getAssetAssignments():ArrayCollection 
        {
            return _assetAssignments;
        }
        
        private function getAfeGroupByAfe(id:String):InvoiceItemGroupByAfe 
        {
            for each (var item:InvoiceItemGroupByAfe in afeGroups) {
                if (item.afe.AFE == id) {
                    return item;
                }
            }
            return null;
        }

        private function getAfeById(AFE:String):Afe 
        {
            for each (var afe:Afe in _afes) {
                if (afe.AFE == AFE) {
                    return afe;
                }
            }
            throw new Error("Afe not found - " + AFE);
            return null;
        }

        private function getSubAfeById(SubAFE:String):SubAfe 
        {
            for each (var subAfe:SubAfe in _subAfes) {
                if (subAfe.SubAFE == SubAFE) {
                    return subAfe;
                }
            }
            throw new Error("Project not found - " + SubAFE);
            return null;
        }

        private function getAssetById(id:int):Asset 
        {
            for each (var asset:Asset in _assets) {
                if (asset.AssetId == id) {
                    return asset;
                }
            }
            throw new Error("Asset not found - " + id.toString());
            return null;
        }
        
        private function getAssetAssignmentById(id:int):AssetAssignment 
        {
            for each (var assetAssignment:AssetAssignment in _assetAssignments) {
                if (assetAssignment.AssetAssignmentId == id) {
                    return assetAssignment;
                }
            }
            throw new Error("AssetAssignment not found - " + id.toString());
            return null;
        }
        
        private function onFault(event:FaultEvent):void {
            dispatchEvent(event);
        }
        
    }
}