
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _AssetAssignment extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _assetAssignmentId: int;
      
        protected var _deleted: Boolean;
      
        // parent tables
        internal var _relatedAfe: Afe
            = new Afe()
        ;
      
        // parent tables
        internal var _relatedAsset: Asset
            = new Asset()
        ;
      
        // parent tables
        internal var _relatedSubAfe: SubAfe
            = new SubAfe()
        ;
      
            public function get  AssetAssignmentId(): int
            {
              return _assetAssignmentId;
            }

            public function set  AssetAssignmentId(value:int):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            _assetAssignmentId = value;
            }
          
            public function get  AFE(): String
            {
            

                  if(_relatedAfe != null)
                  return _relatedAfe.AFE;

                
            
            
            return undefined;
            }
            protected function set AFE(value:String):void
            {

            

                  if(_relatedAfe == null)
                      _relatedAfe = new Afe();

                  _relatedAfe.AFE = value;
                
            
            }
          
            public function get  SubAFE(): String
            {
            

                  if(_relatedSubAfe != null)
                  return _relatedSubAfe.SubAFE;

                
            
            
            return undefined;
            }
            protected function set SubAFE(value:String):void
            {

            

                  if(_relatedSubAfe == null)
                      _relatedSubAfe = new SubAfe();

                  _relatedSubAfe.SubAFE = value;
                
            
            }
          
            public function get  AssetId(): int
            {
            

                  if(_relatedAsset != null)
                  return _relatedAsset.AssetId;

                
            
            
            return undefined;
            }
            protected function set AssetId(value:int):void
            {

            

                  if(_relatedAsset == null)
                      _relatedAsset = new Asset();

                  _relatedAsset.AssetId = value;
                
            
            }
          
            public function get  Deleted(): Boolean
            {
              return _deleted;
            }

            public function set  Deleted(value:Boolean):void
            {
            
            _deleted = value;
            }
          
        
        public function get RelatedAfe():Afe
        {
        if(IsLoaded  && 
        
        !(_relatedAfe.IsLoaded || _relatedAfe.IsLoading))
        {
          _relatedAfe = DataMapperRegistry.Instance.Afe.load(_relatedAfe);
          
          onParentChanged(_relatedAfe);
        }

        return _relatedAfe;
        }
        public function set RelatedAfe(value:Afe):void
        {
          _relatedAfe = Afe(IdentityMap.register( value ));

          onParentChanged(_relatedAfe);
        }
      
        
        public function get RelatedAsset():Asset
        {
        if(IsLoaded  && 
        
        !(_relatedAsset.IsLoaded || _relatedAsset.IsLoading))
        {
          _relatedAsset = DataMapperRegistry.Instance.Asset.load(_relatedAsset);
          
          onParentChanged(_relatedAsset);
        }

        return _relatedAsset;
        }
        public function set RelatedAsset(value:Asset):void
        {
          _relatedAsset = Asset(IdentityMap.register( value ));

          onParentChanged(_relatedAsset);
        }
      
        
        public function get RelatedSubAfe():SubAfe
        {
        if(IsLoaded  && 
        
        !(_relatedSubAfe.IsLoaded || _relatedSubAfe.IsLoading))
        {
          _relatedSubAfe = DataMapperRegistry.Instance.SubAfe.load(_relatedSubAfe);
          
          onParentChanged(_relatedSubAfe);
        }

        return _relatedSubAfe;
        }
        public function set RelatedSubAfe(value:SubAfe):void
        {
          _relatedSubAfe = SubAfe(IdentityMap.register( value ));

          onParentChanged(_relatedSubAfe);
        }
      

            // one to many relation
            protected var _relatedBillItem:ActiveCollection;
            
            public function get RelatedBillItem():ActiveCollection
            {
              _relatedBillItem = onChildRelationRequest("relatedBillItem",_relatedBillItem);
              
              return _relatedBillItem;
            }
            
        

            // one to many relation
            protected var _relatedRateByAssignment:ActiveCollection;
            
            public function get RelatedRateByAssignment():ActiveCollection
            {
              _relatedRateByAssignment = onChildRelationRequest("relatedRateByAssignment",_relatedRateByAssignment);
              
              return _relatedRateByAssignment;
            }
            
        

            // one to many relation
            protected var _relatedInvoiceItem:ActiveCollection;
            
            public function get RelatedInvoiceItem():ActiveCollection
            {
              _relatedInvoiceItem = onChildRelationRequest("relatedInvoiceItem",_relatedInvoiceItem);
              
              return _relatedInvoiceItem;
            }
            
        
        protected override function onDirtyChanged():void
        {
          
            
            if(RelatedAfe != null)
              RelatedAfe.onChildChanged(this);
          
            
            if(RelatedAsset != null)
              RelatedAsset.onChildChanged(this);
          
            
            if(RelatedSubAfe != null)
              RelatedSubAfe.onChildChanged(this);
          
        }
      
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:AssetAssignment = new AssetAssignment();
          
          
            object.AssetAssignmentId = this.AssetAssignmentId;
          
            object.AFE = this.AFE;
          
            object.SubAFE = this.SubAFE;
          
            object.AssetId = this.AssetId;
          
            object.Deleted = this.Deleted;
          
            if(cascade)
            {
              
                    
                      for each(var billItem :BillItem in _relatedBillItem)
                      {
                        if(billItem.IsDirty)
                        {
                           var billItemExtract:Object = billItem.extractRelevant(true);
                               billItemExtract.RelatedAssetAssignment = object;

                        object.RelatedBillItem.addItem(billItemExtract);
                        }
                    }
                  
                    
                      for each(var rateByAssignment :RateByAssignment in _relatedRateByAssignment)
                      {
                        if(rateByAssignment.IsDirty)
                        {
                           var rateByAssignmentExtract:Object = rateByAssignment.extractRelevant(true);
                               rateByAssignmentExtract.RelatedAssetAssignment = object;

                        object.RelatedRateByAssignment.addItem(rateByAssignmentExtract);
                        }
                    }
                  
                    
                      for each(var invoiceItem :InvoiceItem in _relatedInvoiceItem)
                      {
                        if(invoiceItem.IsDirty)
                        {
                           var invoiceItemExtract:Object = invoiceItem.extractRelevant(true);
                               invoiceItemExtract.RelatedAssetAssignment = object;

                        object.RelatedInvoiceItem.addItem(invoiceItemExtract);
                        }
                    }
                  
            }
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
          public override function extractChilds():Array
          {
          var childs:Array = new Array();

          
                if(this["relatedBillItem"])
                {
                  for each(var billItem :ActiveRecord in this["relatedBillItem"] as Array)
                    childs.push(billItem);
                }
              
                if(this["relatedRateByAssignment"])
                {
                  for each(var rateByAssignment :ActiveRecord in this["relatedRateByAssignment"] as Array)
                    childs.push(rateByAssignment);
                }
              
                if(this["relatedInvoiceItem"])
                {
                  for each(var invoiceItem :ActiveRecord in this["relatedInvoiceItem"] as Array)
                    childs.push(invoiceItem);
                }
              

          return childs;
          }
        
        
        public override function applyFields(object:Object):void
        {
        
                if(!IsPrimaryKeyInitialized)
                  AssetAssignmentId = object["AssetAssignmentId"];
              Deleted = object["Deleted"];
              RelatedAfe =
          object.RelatedAfe;
        RelatedAsset =
          object.RelatedAsset;
        RelatedSubAfe =
          object.RelatedSubAfe;
        
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.AssetAssignment;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.AssetAssignment." 
            
              + AssetAssignmentId.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    