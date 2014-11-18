
      package Domain.Codegen
      {
      import weborb.data.*;
      import Domain.*;
      import mx.collections.ArrayCollection;

      [Bindable]
      public dynamic class _Document extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _docID: int;
      
        protected var _isPublic: Boolean;
      
        protected var _docTypeId: int;
      
        protected var _vol: String;
      
        protected var _pg: String;
      
        protected var _documentNo: String;
      
        protected var _county: String;
      
        protected var _state: String;
      
        protected var _dateFiled: Date;
      
        protected var _dateSigned: Date;
      
        protected var _researchNote: String;
      
        protected var _imageLink: String;
      
            public function get  DocID(): int
            {
            return _docID;
            }

            public function set  DocID(value:int):void
            {
            
            _docID = value;
            }
          
            public function get  IsPublic(): Boolean
            {
            return _isPublic;
            }

            public function set  IsPublic(value:Boolean):void
            {
            
            _isPublic = value;
            }
          
            public function get  DocTypeId(): int
            {
            return _docTypeId;
            }

            public function set  DocTypeId(value:int):void
            {
            
            _docTypeId = value;
            }
          
            public function get  Vol(): String
            {
            return _vol;
            }

            public function set  Vol(value:String):void
            {
            
            _vol = value;
            }
          
            public function get  Pg(): String
            {
            return _pg;
            }

            public function set  Pg(value:String):void
            {
            
            _pg = value;
            }
          
            public function get  DocumentNo(): String
            {
            return _documentNo;
            }

            public function set  DocumentNo(value:String):void
            {
            
            _documentNo = value;
            }
          
            public function get  County(): String
            {
            return _county;
            }

            public function set  County(value:String):void
            {
            
            _county = value;
            }
          
            public function get  State(): String
            {
            return _state;
            }

            public function set  State(value:String):void
            {
            
            _state = value;
            }
          
            public function get  DateFiled(): Date
            {
            return _dateFiled;
            }

            public function set  DateFiled(value:Date):void
            {
            
            _dateFiled = value;
            }
          
            public function get  DateSigned(): Date
            {
            return _dateSigned;
            }

            public function set  DateSigned(value:Date):void
            {
            
            _dateSigned = value;
            }
          
            public function get  ResearchNote(): String
            {
            return _researchNote;
            }

            public function set  ResearchNote(value:String):void
            {
            
            _researchNote = value;
            }
          
            public function get  ImageLink(): String
            {
            return _imageLink;
            }

            public function set  ImageLink(value:String):void
            {
            
            _imageLink = value;
            }
          
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:Document = new Document();
          
          
            object.DocID = this.DocID;
          
            object.IsPublic = this.IsPublic;
          
            object.DocTypeId = this.DocTypeId;
          
            object.Vol = this.Vol;
          
            object.Pg = this.Pg;
          
            object.DocumentNo = this.DocumentNo;
          
            object.County = this.County;
          
            object.State = this.State;
          
            object.DateFiled = this.DateFiled;
          
            object.DateSigned = this.DateSigned;
          
            object.ResearchNote = this.ResearchNote;
          
            object.ImageLink = this.ImageLink;
          

          return object;
        }
        
        
        
        public override function applyFields(object:Object):void
        {
        DocID = object["DocID"];
        IsPublic = object["IsPublic"];
        DocTypeId = object["DocTypeId"];
        Vol = object["Vol"];
        Pg = object["Pg"];
        DocumentNo = object["DocumentNo"];
        County = object["County"];
        State = object["State"];
        DateFiled = object["DateFiled"];
        DateSigned = object["DateSigned"];
        ResearchNote = object["ResearchNote"];
        ImageLink = object["ImageLink"];
        
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.Document;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "doc-capture.Document." 
            
              + DocID.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    