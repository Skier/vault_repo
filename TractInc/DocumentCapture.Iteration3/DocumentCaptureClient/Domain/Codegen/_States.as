
      package Domain.Codegen
      {
      import weborb.data.*;
      import Domain.*;
      import mx.collections.ArrayCollection;

      [Bindable]
      public dynamic class _States extends ActiveRecord
      {
      private var _uri:String = null;

      
        protected var _oBJECTID: int;
      
        protected var _sTATE_ID: int;
      
        protected var _sTATE_NAME: String;
      
        protected var _sTATE_FIPS: String;
      
        protected var _sUB_REGION: String;
      
        protected var _sTATE_ABBR: String;
      
        protected var _pOP2000: Number;
      
        protected var _pOP2004: Number;
      
        protected var _pOP00_SQMI: Number;
      
        protected var _pOP04_SQMI: Number;
      
        protected var _wHITE: Number;
      
        protected var _bLACK: Number;
      
        protected var _aMERI_ES: Number;
      
        protected var _aSIAN: Number;
      
        protected var _hAWN_PI: Number;
      
        protected var _oTHER: Number;
      
        protected var _mULT_RACE: Number;
      
        protected var _hISPANIC: Number;
      
        protected var _mALES: Number;
      
        protected var _fEMALES: Number;
      
        protected var _aGE_UNDER5: Number;
      
        protected var _aGE_5_17: Number;
      
        protected var _aGE_18_21: Number;
      
        protected var _aGE_22_29: Number;
      
        protected var _aGE_30_39: Number;
      
        protected var _aGE_40_49: Number;
      
        protected var _aGE_50_64: Number;
      
        protected var _aGE_65_UP: Number;
      
        protected var _mED_AGE: Number;
      
        protected var _mED_AGE_M: Number;
      
        protected var _mED_AGE_F: Number;
      
        protected var _hOUSEHOLDS: Number;
      
        protected var _aVE_HH_SZ: Number;
      
        protected var _hSEHLD_1_M: Number;
      
        protected var _hSEHLD_1_F: Number;
      
        protected var _mARHH_CHD: Number;
      
        protected var _mARHH_NO_C: Number;
      
        protected var _mHH_CHILD: Number;
      
        protected var _fHH_CHILD: Number;
      
        protected var _fAMILIES: Number;
      
        protected var _aVE_FAM_SZ: Number;
      
        protected var _hSE_UNITS: Number;
      
        protected var _vACANT: Number;
      
        protected var _oWNER_OCC: Number;
      
        protected var _rENTER_OCC: Number;
      
        protected var _nO_FARMS97: Number;
      
        protected var _aVG_SIZE97: Number;
      
        protected var _cROP_ACR97: Number;
      
        protected var _aVG_SALE97: Number;
      
        protected var _sQMI: Number;
      
        protected var _shape_Leng: Number;
      
        protected var _shape_Area: Number;
      
            public function get  OBJECTID(): int
            {
            return _oBJECTID;
            }

            public function set  OBJECTID(value:int):void
            {
            
            _oBJECTID = value;
            }
          
            public function get  STATE_ID(): int
            {
            return _sTATE_ID;
            }

            public function set  STATE_ID(value:int):void
            {
            
            _sTATE_ID = value;
            }
          
            public function get  STATE_NAME(): String
            {
            return _sTATE_NAME;
            }

            public function set  STATE_NAME(value:String):void
            {
            
            _sTATE_NAME = value;
            }
          
            public function get  STATE_FIPS(): String
            {
            return _sTATE_FIPS;
            }

            public function set  STATE_FIPS(value:String):void
            {
            
            _sTATE_FIPS = value;
            }
          
            public function get  SUB_REGION(): String
            {
            return _sUB_REGION;
            }

            public function set  SUB_REGION(value:String):void
            {
            
            _sUB_REGION = value;
            }
          
            public function get  STATE_ABBR(): String
            {
            return _sTATE_ABBR;
            }

            public function set  STATE_ABBR(value:String):void
            {
            
            _sTATE_ABBR = value;
            }
          
            public function get  POP2000(): Number
            {
            return _pOP2000;
            }

            public function set  POP2000(value:Number):void
            {
            
            _pOP2000 = value;
            }
          
            public function get  POP2004(): Number
            {
            return _pOP2004;
            }

            public function set  POP2004(value:Number):void
            {
            
            _pOP2004 = value;
            }
          
            public function get  POP00_SQMI(): Number
            {
            return _pOP00_SQMI;
            }

            public function set  POP00_SQMI(value:Number):void
            {
            
            _pOP00_SQMI = value;
            }
          
            public function get  POP04_SQMI(): Number
            {
            return _pOP04_SQMI;
            }

            public function set  POP04_SQMI(value:Number):void
            {
            
            _pOP04_SQMI = value;
            }
          
            public function get  WHITE(): Number
            {
            return _wHITE;
            }

            public function set  WHITE(value:Number):void
            {
            
            _wHITE = value;
            }
          
            public function get  BLACK(): Number
            {
            return _bLACK;
            }

            public function set  BLACK(value:Number):void
            {
            
            _bLACK = value;
            }
          
            public function get  AMERI_ES(): Number
            {
            return _aMERI_ES;
            }

            public function set  AMERI_ES(value:Number):void
            {
            
            _aMERI_ES = value;
            }
          
            public function get  ASIAN(): Number
            {
            return _aSIAN;
            }

            public function set  ASIAN(value:Number):void
            {
            
            _aSIAN = value;
            }
          
            public function get  HAWN_PI(): Number
            {
            return _hAWN_PI;
            }

            public function set  HAWN_PI(value:Number):void
            {
            
            _hAWN_PI = value;
            }
          
            public function get  OTHER(): Number
            {
            return _oTHER;
            }

            public function set  OTHER(value:Number):void
            {
            
            _oTHER = value;
            }
          
            public function get  MULT_RACE(): Number
            {
            return _mULT_RACE;
            }

            public function set  MULT_RACE(value:Number):void
            {
            
            _mULT_RACE = value;
            }
          
            public function get  HISPANIC(): Number
            {
            return _hISPANIC;
            }

            public function set  HISPANIC(value:Number):void
            {
            
            _hISPANIC = value;
            }
          
            public function get  MALES(): Number
            {
            return _mALES;
            }

            public function set  MALES(value:Number):void
            {
            
            _mALES = value;
            }
          
            public function get  FEMALES(): Number
            {
            return _fEMALES;
            }

            public function set  FEMALES(value:Number):void
            {
            
            _fEMALES = value;
            }
          
            public function get  AGE_UNDER5(): Number
            {
            return _aGE_UNDER5;
            }

            public function set  AGE_UNDER5(value:Number):void
            {
            
            _aGE_UNDER5 = value;
            }
          
            public function get  AGE_5_17(): Number
            {
            return _aGE_5_17;
            }

            public function set  AGE_5_17(value:Number):void
            {
            
            _aGE_5_17 = value;
            }
          
            public function get  AGE_18_21(): Number
            {
            return _aGE_18_21;
            }

            public function set  AGE_18_21(value:Number):void
            {
            
            _aGE_18_21 = value;
            }
          
            public function get  AGE_22_29(): Number
            {
            return _aGE_22_29;
            }

            public function set  AGE_22_29(value:Number):void
            {
            
            _aGE_22_29 = value;
            }
          
            public function get  AGE_30_39(): Number
            {
            return _aGE_30_39;
            }

            public function set  AGE_30_39(value:Number):void
            {
            
            _aGE_30_39 = value;
            }
          
            public function get  AGE_40_49(): Number
            {
            return _aGE_40_49;
            }

            public function set  AGE_40_49(value:Number):void
            {
            
            _aGE_40_49 = value;
            }
          
            public function get  AGE_50_64(): Number
            {
            return _aGE_50_64;
            }

            public function set  AGE_50_64(value:Number):void
            {
            
            _aGE_50_64 = value;
            }
          
            public function get  AGE_65_UP(): Number
            {
            return _aGE_65_UP;
            }

            public function set  AGE_65_UP(value:Number):void
            {
            
            _aGE_65_UP = value;
            }
          
            public function get  MED_AGE(): Number
            {
            return _mED_AGE;
            }

            public function set  MED_AGE(value:Number):void
            {
            
            _mED_AGE = value;
            }
          
            public function get  MED_AGE_M(): Number
            {
            return _mED_AGE_M;
            }

            public function set  MED_AGE_M(value:Number):void
            {
            
            _mED_AGE_M = value;
            }
          
            public function get  MED_AGE_F(): Number
            {
            return _mED_AGE_F;
            }

            public function set  MED_AGE_F(value:Number):void
            {
            
            _mED_AGE_F = value;
            }
          
            public function get  HOUSEHOLDS(): Number
            {
            return _hOUSEHOLDS;
            }

            public function set  HOUSEHOLDS(value:Number):void
            {
            
            _hOUSEHOLDS = value;
            }
          
            public function get  AVE_HH_SZ(): Number
            {
            return _aVE_HH_SZ;
            }

            public function set  AVE_HH_SZ(value:Number):void
            {
            
            _aVE_HH_SZ = value;
            }
          
            public function get  HSEHLD_1_M(): Number
            {
            return _hSEHLD_1_M;
            }

            public function set  HSEHLD_1_M(value:Number):void
            {
            
            _hSEHLD_1_M = value;
            }
          
            public function get  HSEHLD_1_F(): Number
            {
            return _hSEHLD_1_F;
            }

            public function set  HSEHLD_1_F(value:Number):void
            {
            
            _hSEHLD_1_F = value;
            }
          
            public function get  MARHH_CHD(): Number
            {
            return _mARHH_CHD;
            }

            public function set  MARHH_CHD(value:Number):void
            {
            
            _mARHH_CHD = value;
            }
          
            public function get  MARHH_NO_C(): Number
            {
            return _mARHH_NO_C;
            }

            public function set  MARHH_NO_C(value:Number):void
            {
            
            _mARHH_NO_C = value;
            }
          
            public function get  MHH_CHILD(): Number
            {
            return _mHH_CHILD;
            }

            public function set  MHH_CHILD(value:Number):void
            {
            
            _mHH_CHILD = value;
            }
          
            public function get  FHH_CHILD(): Number
            {
            return _fHH_CHILD;
            }

            public function set  FHH_CHILD(value:Number):void
            {
            
            _fHH_CHILD = value;
            }
          
            public function get  FAMILIES(): Number
            {
            return _fAMILIES;
            }

            public function set  FAMILIES(value:Number):void
            {
            
            _fAMILIES = value;
            }
          
            public function get  AVE_FAM_SZ(): Number
            {
            return _aVE_FAM_SZ;
            }

            public function set  AVE_FAM_SZ(value:Number):void
            {
            
            _aVE_FAM_SZ = value;
            }
          
            public function get  HSE_UNITS(): Number
            {
            return _hSE_UNITS;
            }

            public function set  HSE_UNITS(value:Number):void
            {
            
            _hSE_UNITS = value;
            }
          
            public function get  VACANT(): Number
            {
            return _vACANT;
            }

            public function set  VACANT(value:Number):void
            {
            
            _vACANT = value;
            }
          
            public function get  OWNER_OCC(): Number
            {
            return _oWNER_OCC;
            }

            public function set  OWNER_OCC(value:Number):void
            {
            
            _oWNER_OCC = value;
            }
          
            public function get  RENTER_OCC(): Number
            {
            return _rENTER_OCC;
            }

            public function set  RENTER_OCC(value:Number):void
            {
            
            _rENTER_OCC = value;
            }
          
            public function get  NO_FARMS97(): Number
            {
            return _nO_FARMS97;
            }

            public function set  NO_FARMS97(value:Number):void
            {
            
            _nO_FARMS97 = value;
            }
          
            public function get  AVG_SIZE97(): Number
            {
            return _aVG_SIZE97;
            }

            public function set  AVG_SIZE97(value:Number):void
            {
            
            _aVG_SIZE97 = value;
            }
          
            public function get  CROP_ACR97(): Number
            {
            return _cROP_ACR97;
            }

            public function set  CROP_ACR97(value:Number):void
            {
            
            _cROP_ACR97 = value;
            }
          
            public function get  AVG_SALE97(): Number
            {
            return _aVG_SALE97;
            }

            public function set  AVG_SALE97(value:Number):void
            {
            
            _aVG_SALE97 = value;
            }
          
            public function get  SQMI(): Number
            {
            return _sQMI;
            }

            public function set  SQMI(value:Number):void
            {
            
            _sQMI = value;
            }
          
            public function get  Shape_Leng(): Number
            {
            return _shape_Leng;
            }

            public function set  Shape_Leng(value:Number):void
            {
            
            _shape_Leng = value;
            }
          
            public function get  Shape_Area(): Number
            {
            return _shape_Area;
            }

            public function set  Shape_Area(value:Number):void
            {
            
            _shape_Area = value;
            }
          

            // one to many relation
            protected var _childCountysList:ActiveCollection;


            public function get ChildsCountys():ActiveCollection
            {
            
            if(_childCountysList == null)
            {
              if(this["childsCountys"] == null)
                this["childsCountys"] = new Array();
                
              var hiddenArray:Array = this["childsCountys"] as Array;
              
              _childCountysList = new ActiveCollection(hiddenArray);
            }

            if(IsLoaded  && 
              _childCountysList.length == 0 && 
              !(_childCountysList.IsLoaded || _childCountysList.IsLoading))
              {

                _childCountysList = DataMapperRegistry.Instance.Countys.
                  findBySTATE_ID(
                  STATE_ID)
                ;
                _childCountysList.source = this["childsCountys"] as Array;
              }

              return _childCountysList;
            }
            

        
      
      
        
        public override function extractRelevant(cascade:Boolean = false):Object
        {
          var object:States = new States();
          
          
            object.OBJECTID = this.OBJECTID;
          
            object.STATE_ID = this.STATE_ID;
          
            object.STATE_NAME = this.STATE_NAME;
          
            object.STATE_FIPS = this.STATE_FIPS;
          
            object.SUB_REGION = this.SUB_REGION;
          
            object.STATE_ABBR = this.STATE_ABBR;
          
            object.POP2000 = this.POP2000;
          
            object.POP2004 = this.POP2004;
          
            object.POP00_SQMI = this.POP00_SQMI;
          
            object.POP04_SQMI = this.POP04_SQMI;
          
            object.WHITE = this.WHITE;
          
            object.BLACK = this.BLACK;
          
            object.AMERI_ES = this.AMERI_ES;
          
            object.ASIAN = this.ASIAN;
          
            object.HAWN_PI = this.HAWN_PI;
          
            object.OTHER = this.OTHER;
          
            object.MULT_RACE = this.MULT_RACE;
          
            object.HISPANIC = this.HISPANIC;
          
            object.MALES = this.MALES;
          
            object.FEMALES = this.FEMALES;
          
            object.AGE_UNDER5 = this.AGE_UNDER5;
          
            object.AGE_5_17 = this.AGE_5_17;
          
            object.AGE_18_21 = this.AGE_18_21;
          
            object.AGE_22_29 = this.AGE_22_29;
          
            object.AGE_30_39 = this.AGE_30_39;
          
            object.AGE_40_49 = this.AGE_40_49;
          
            object.AGE_50_64 = this.AGE_50_64;
          
            object.AGE_65_UP = this.AGE_65_UP;
          
            object.MED_AGE = this.MED_AGE;
          
            object.MED_AGE_M = this.MED_AGE_M;
          
            object.MED_AGE_F = this.MED_AGE_F;
          
            object.HOUSEHOLDS = this.HOUSEHOLDS;
          
            object.AVE_HH_SZ = this.AVE_HH_SZ;
          
            object.HSEHLD_1_M = this.HSEHLD_1_M;
          
            object.HSEHLD_1_F = this.HSEHLD_1_F;
          
            object.MARHH_CHD = this.MARHH_CHD;
          
            object.MARHH_NO_C = this.MARHH_NO_C;
          
            object.MHH_CHILD = this.MHH_CHILD;
          
            object.FHH_CHILD = this.FHH_CHILD;
          
            object.FAMILIES = this.FAMILIES;
          
            object.AVE_FAM_SZ = this.AVE_FAM_SZ;
          
            object.HSE_UNITS = this.HSE_UNITS;
          
            object.VACANT = this.VACANT;
          
            object.OWNER_OCC = this.OWNER_OCC;
          
            object.RENTER_OCC = this.RENTER_OCC;
          
            object.NO_FARMS97 = this.NO_FARMS97;
          
            object.AVG_SIZE97 = this.AVG_SIZE97;
          
            object.CROP_ACR97 = this.CROP_ACR97;
          
            object.AVG_SALE97 = this.AVG_SALE97;
          
            object.SQMI = this.SQMI;
          
            object.Shape_Leng = this.Shape_Leng;
          
            object.Shape_Area = this.Shape_Area;
          
            if(cascade)
            {
              
                    
                      for each(var countys :Countys in _childCountysList)
                      {
                        if(countys.IsDirty)
                        {
                           var countysExtract:Object = countys.extractRelevant(true);
                               countysExtract._parentStates = object;

                    object.ChildsCountys.addItem(countysExtract);
                    }
                    }
                  
            }
          

          return object;
        }
        
        
          public override function extractChilds():Array
          {
          var childs:Array = new Array();

          
                if(this["childsCountys"])
                {
                  for each(var countys :ActiveRecord in this["childsCountys"] as Array)
                    childs.push(countys);
                }
              

          return childs;
          }
        
        
        public override function applyFields(object:Object):void
        {
        OBJECTID = object["OBJECTID"];
        STATE_ID = object["STATE_ID"];
        STATE_NAME = object["STATE_NAME"];
        STATE_FIPS = object["STATE_FIPS"];
        SUB_REGION = object["SUB_REGION"];
        STATE_ABBR = object["STATE_ABBR"];
        POP2000 = object["POP2000"];
        POP2004 = object["POP2004"];
        POP00_SQMI = object["POP00_SQMI"];
        POP04_SQMI = object["POP04_SQMI"];
        WHITE = object["WHITE"];
        BLACK = object["BLACK"];
        AMERI_ES = object["AMERI_ES"];
        ASIAN = object["ASIAN"];
        HAWN_PI = object["HAWN_PI"];
        OTHER = object["OTHER"];
        MULT_RACE = object["MULT_RACE"];
        HISPANIC = object["HISPANIC"];
        MALES = object["MALES"];
        FEMALES = object["FEMALES"];
        AGE_UNDER5 = object["AGE_UNDER5"];
        AGE_5_17 = object["AGE_5_17"];
        AGE_18_21 = object["AGE_18_21"];
        AGE_22_29 = object["AGE_22_29"];
        AGE_30_39 = object["AGE_30_39"];
        AGE_40_49 = object["AGE_40_49"];
        AGE_50_64 = object["AGE_50_64"];
        AGE_65_UP = object["AGE_65_UP"];
        MED_AGE = object["MED_AGE"];
        MED_AGE_M = object["MED_AGE_M"];
        MED_AGE_F = object["MED_AGE_F"];
        HOUSEHOLDS = object["HOUSEHOLDS"];
        AVE_HH_SZ = object["AVE_HH_SZ"];
        HSEHLD_1_M = object["HSEHLD_1_M"];
        HSEHLD_1_F = object["HSEHLD_1_F"];
        MARHH_CHD = object["MARHH_CHD"];
        MARHH_NO_C = object["MARHH_NO_C"];
        MHH_CHILD = object["MHH_CHILD"];
        FHH_CHILD = object["FHH_CHILD"];
        FAMILIES = object["FAMILIES"];
        AVE_FAM_SZ = object["AVE_FAM_SZ"];
        HSE_UNITS = object["HSE_UNITS"];
        VACANT = object["VACANT"];
        OWNER_OCC = object["OWNER_OCC"];
        RENTER_OCC = object["RENTER_OCC"];
        NO_FARMS97 = object["NO_FARMS97"];
        AVG_SIZE97 = object["AVG_SIZE97"];
        CROP_ACR97 = object["CROP_ACR97"];
        AVG_SALE97 = object["AVG_SALE97"];
        SQMI = object["SQMI"];
        Shape_Leng = object["Shape_Leng"];
        Shape_Area = object["Shape_Area"];
        
        IsDirty = false;
        }

        protected override function get dataMapper():DataMapper
        {
          return DataMapperRegistry.Instance.States;
        }
        
       
        public override function getURI():String
        {

          if(_uri == null)
          {
           _uri = "doc-capture.States." 
            
              + STATE_ID.toString()
            ;
          }
           
          return _uri;
        }
      }

      }
    