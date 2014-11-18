
      package App.Domain.Codegen
      {
      import weborb.data.*;
      import App.Domain.*;
      import mx.collections.ArrayCollection;
      import flash.utils.ByteArray;

      [Bindable]
      public dynamic class _AssetType extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var __Type: String;
      
            public function get  _Type(): String
            {
              return __Type;
            }

            public function set  _Type(value:String):void
            {
            
              _isPrimaryKeyAffected = true;
              _uri = null;

              if(IsLoaded || IsLoading)
              {
                trace("Critical error: attempt to modify primary key in initialized object " + getURI());
                return;
              }
            
            __Type = value;
            }
          

            // one to many relation
            protected var _relatedAsset:ActiveCollection;
            
            public function get RelatedAsset():ActiveCollection
            {
              _relatedAsset = onChildRelationRequest("relatedAsset",_relatedAsset);
              
              return _relatedAsset;
            }
            
        
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:AssetType = new AssetType();
          
          
            object._Type = this._Type;
          
            if(cascade)
            {
              
                    
                      for each(var asset :Asset in _relatedAsset)
                      {
                        if(asset.IsDirty)
                        {
                           var assetExtract:Object = asset.extractRelevant(true);
                               assetExtract.RelatedAssetType = object;

                        object.RelatedAsset.addItem(assetExtract);
                        }
                    }
                  
            }
          
      object.ActiveRecordId = this.ActiveRecordId;
      
      return object;
      }

      
          public override function extractChilds():Array
          {
          var childs:Array = new Array();

          
                if(this["relatedAsset"])
                {
                  for each(var asset :ActiveRecord in this["relatedAsset"] as Array)
                    childs.push(asset);
                }
              

          return childs;
          }
        
        
        public override function applyFields(object:Object):void
        {
        
                if(!IsPrimaryKeyInitialized)
                  _Type = object["_Type"];
              
      
        _uri = null;
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.AssetType;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "TractIncRAID.AssetType." 
            
              + _Type.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    