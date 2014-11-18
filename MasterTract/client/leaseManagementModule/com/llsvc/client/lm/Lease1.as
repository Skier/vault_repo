package com.llsvc.client.lm
{
import flash.utils.ByteArray;
import mx.collections.ArrayCollection;

[Bindable]
public dynamic class Lease1
{
private var _uri:String = null;


  protected var _leaseId: int;

  protected var _lCN: String;

  protected var _documentNumber: String;

  protected var _volume: String;

  protected var _pAGE: String;

  protected var _leaseeName: String;

  protected var _assigneeName: String;

  protected var _leassorName: String;

  protected var _assignorName: String;

  protected var _stateFips: String;

  protected var _countyFips: String;

  protected var _unitDepth: Number = 0;

  protected var _fromDepth: Number = 0;

  protected var _fromFrom: Number = 0;

  protected var _toDepth: Number = 0;

  protected var _toFrom: Number = 0;

  protected var _workInt: String;

  protected var _orrInt: String;

  protected var _netAcres: Number = 0;

  protected var _grossAcres: Number = 0;

  protected var _nriAssign: String;

  protected var _rcdDate: Date;

  protected var _term: Number = 0;

  protected var _hBR: Boolean;

  protected var _encumbrances: Boolean;

  protected var _effDate: Date;

  protected var _pughClause: Boolean;

  protected var _depthLimitation: Boolean;

  protected var _shutInClau: Boolean;

  protected var _poolingClau: Boolean;

  protected var _minimumPmt: Number = 0;

  protected var _author: int;

  protected var _status: String;

  // parent tables
//  internal var _parentTermUnit: TermUnit;

      public function get  LeaseId(): int
      {
        return _leaseId;
      }

      public function set  LeaseId(value:int):void
      {
      
        _uri = null;
      
      _leaseId = value;
      }
    
      public function get  LCN(): String
      {
        return _lCN;
      }

      public function set  LCN(value:String):void
      {
      
      _lCN = value;
      }
    
      public function get  DocumentNumber(): String
      {
        return _documentNumber;
      }

      public function set  DocumentNumber(value:String):void
      {
      
      _documentNumber = value;
      }
    
      public function get  Volume(): String
      {
        return _volume;
      }

      public function set  Volume(value:String):void
      {
      
      _volume = value;
      }
    
      public function get  PAGE(): String
      {
        return _pAGE;
      }

      public function set  PAGE(value:String):void
      {
      
      _pAGE = value;
      }
    
      public function get  LeaseeName(): String
      {
        return _leaseeName;
      }

      public function set  LeaseeName(value:String):void
      {
      
      _leaseeName = value;
      }
    
      public function get  AssigneeName(): String
      {
        return _assigneeName;
      }

      public function set  AssigneeName(value:String):void
      {
      
      _assigneeName = value;
      }
    
      public function get  LeassorName(): String
      {
        return _leassorName;
      }

      public function set  LeassorName(value:String):void
      {
      
      _leassorName = value;
      }
    
      public function get  AssignorName(): String
      {
        return _assignorName;
      }

      public function set  AssignorName(value:String):void
      {
      
      _assignorName = value;
      }
    
      public function get  StateFips(): String
      {
        return _stateFips;
      }

      public function set  StateFips(value:String):void
      {
      
      _stateFips = value;
      }
    
      public function get  CountyFips(): String
      {
        return _countyFips;
      }

      public function set  CountyFips(value:String):void
      {
      
      _countyFips = value;
      }
    
      public function get  UnitDepth(): Number
      {
        return _unitDepth;
      }

      public function set  UnitDepth(value:Number):void
      {
      
      _unitDepth = value;
      }
    
      public function get  FromDepth(): Number
      {
        return _fromDepth;
      }

      public function set  FromDepth(value:Number):void
      {
      
      _fromDepth = value;
      }
    
      public function get  FromFrom(): Number
      {
        return _fromFrom;
      }

      public function set  FromFrom(value:Number):void
      {
      
      _fromFrom = value;
      }
    
      public function get  ToDepth(): Number
      {
        return _toDepth;
      }

      public function set  ToDepth(value:Number):void
      {
      
      _toDepth = value;
      }
    
      public function get  ToFrom(): Number
      {
        return _toFrom;
      }

      public function set  ToFrom(value:Number):void
      {
      
      _toFrom = value;
      }
    
      public function get  WorkInt(): String
      {
        return _workInt;
      }

      public function set  WorkInt(value:String):void
      {
      
      _workInt = value;
      }
    
      public function get  OrrInt(): String
      {
        return _orrInt;
      }

      public function set  OrrInt(value:String):void
      {
      
      _orrInt = value;
      }
    
      public function get  NetAcres(): Number
      {
        return _netAcres;
      }

      public function set  NetAcres(value:Number):void
      {
      
      _netAcres = value;
      }
    
      public function get  GrossAcres(): Number
      {
        return _grossAcres;
      }

      public function set  GrossAcres(value:Number):void
      {
      
      _grossAcres = value;
      }
    
      public function get  NriAssign(): String
      {
        return _nriAssign;
      }

      public function set  NriAssign(value:String):void
      {
      
      _nriAssign = value;
      }
    
      public function get  RcdDate(): Date
      {
        return _rcdDate;
      }

      public function set  RcdDate(value:Date):void
      {
      
      _rcdDate = value;
      }
    
      public function get  Term(): Number
      {
        return _term;
      }

      public function set  Term(value:Number):void
      {
      
      _term = value;
      }

/*    
      public function get  TermUnitId(): int
      {
      

            if(_parentTermUnit != null)
            return _parentTermUnit.TermUnitId;

          
      
      
      return undefined;
      }
      public function set TermUnitId(value:int):void
      {

      

            if(_parentTermUnit == null)
                _parentTermUnit = new TermUnit();

            _parentTermUnit.TermUnitId = value;
          
      
      }
*/    
      public function get  HBR(): Boolean
      {
        return _hBR;
      }

      public function set  HBR(value:Boolean):void
      {
      
      _hBR = value;
      }
    
      public function get  Encumbrances(): Boolean
      {
        return _encumbrances;
      }

      public function set  Encumbrances(value:Boolean):void
      {
      
      _encumbrances = value;
      }
    
      public function get  EffDate(): Date
      {
        return _effDate;
      }

      public function set  EffDate(value:Date):void
      {
      
      _effDate = value;
      }
    
      public function get  PughClause(): Boolean
      {
        return _pughClause;
      }

      public function set  PughClause(value:Boolean):void
      {
      
      _pughClause = value;
      }
    
      public function get  DepthLimitation(): Boolean
      {
        return _depthLimitation;
      }

      public function set  DepthLimitation(value:Boolean):void
      {
      
      _depthLimitation = value;
      }
    
      public function get  ShutInClau(): Boolean
      {
        return _shutInClau;
      }

      public function set  ShutInClau(value:Boolean):void
      {
      
      _shutInClau = value;
      }
    
      public function get  PoolingClau(): Boolean
      {
        return _poolingClau;
      }

      public function set  PoolingClau(value:Boolean):void
      {
      
      _poolingClau = value;
      }
    
      public function get  MinimumPmt(): Number
      {
        return _minimumPmt;
      }

      public function set  MinimumPmt(value:Number):void
      {
      
      _minimumPmt = value;
      }
    
      public function get  Author(): int
      {
        return _author;
      }

      public function set  Author(value:int):void
      {
      
      _author = value;
      }
    
      public function get  Status(): String
      {
        return _status;
      }

      public function set  Status(value:String):void
      {
      
      _status = value;
      }
    
/*  
  public function get ParentTermUnit():TermUnit
  {
  if(IsLoaded  && 
  _parentTermUnit  && 
  
  !(_parentTermUnit.IsLoaded || _parentTermUnit.IsLoading))
  {
    _parentTermUnit = DataMapperRegistry.Instance.TermUnit.load(_parentTermUnit);
    
    onParentChanged(_parentTermUnit);
  }

  return _parentTermUnit;
  }
  public function set ParentTermUnit(value:TermUnit):void
  {
    _parentTermUnit = TermUnit(IdentityMap.register( value ));

    onParentChanged(_parentTermUnit);
  }
*/

      // one to many relation
/*      
      protected var _leaseEditHistorys:ActiveCollection;
      
      public function get LeaseEditHistorys():ActiveCollection
      {
        _leaseEditHistorys = onChildRelationRequest("leaseEditHistorys",_leaseEditHistorys);
        
        return _leaseEditHistorys;
      }
*/      
  
/*  
  protected function onDirtyChanged():void
  {
    
      
      if(ParentTermUnit != null)
        ParentTermUnit.onChildChanged(this);
    
  }
*/


  
  public function extractRelevant(cascade:Boolean = false):Object
  {
    var object:Lease = new Lease();
    
    
      object.LeaseId = this.LeaseId;
    
      object.LCN = this.LCN;
    
      object.DocumentNumber = this.DocumentNumber;
    
      object.Volume = this.Volume;
    
      object.PAGE = this.PAGE;
    
      object.LeaseeName = this.LeaseeName;
    
      object.AssigneeName = this.AssigneeName;
    
      object.LeassorName = this.LeassorName;
    
      object.AssignorName = this.AssignorName;
    
      object.StateFips = this.StateFips;
    
      object.CountyFips = this.CountyFips;
    
      object.UnitDepth = this.UnitDepth;
    
      object.FromDepth = this.FromDepth;
    
      object.FromFrom = this.FromFrom;
    
      object.ToDepth = this.ToDepth;
    
      object.ToFrom = this.ToFrom;
    
      object.WorkInt = this.WorkInt;
    
      object.OrrInt = this.OrrInt;
    
      object.NetAcres = this.NetAcres;
    
      object.GrossAcres = this.GrossAcres;
    
      object.NriAssign = this.NriAssign;
    
      object.RcdDate = this.RcdDate;
    
      object.Term = this.Term;
    
      object.TermUnitId = this.TermUnitId;
    
      object.HBR = this.HBR;
    
      object.Encumbrances = this.Encumbrances;
    
      object.EffDate = this.EffDate;
    
      object.PughClause = this.PughClause;
    
      object.DepthLimitation = this.DepthLimitation;
    
      object.ShutInClau = this.ShutInClau;
    
      object.PoolingClau = this.PoolingClau;
    
      object.MinimumPmt = this.MinimumPmt;
    
      object.Author = this.Author;
    
      object.Status = this.Status;
/*    
      if(cascade)
      {
        
              
                for each(var leaseEditHistory :LeaseEditHistory in _leaseEditHistorys)
                {
                  if(leaseEditHistory.IsDirty)
                  {
                     var leaseEditHistoryExtract:Object = leaseEditHistory.extractRelevant(true);
                         leaseEditHistoryExtract.ParentLease = object;

                  object.LeaseEditHistorys.addItem(leaseEditHistoryExtract);
                  }
              }
            
      }
*/    
object.ActiveRecordId = this.ActiveRecordId;

return object;
}


    public function extractChilds():Array
    {
    var childs:Array = new Array();

/*    
          if(this["leaseEditHistorys"])
          {
            for each(var leaseEditHistory :ActiveRecord in this["leaseEditHistorys"] as Array)
              childs.push(leaseEditHistory);
          }
*/        

    return childs;
    }
  
  
  public function applyFields(object:Object):void
  {
  LeaseId = object["LeaseId"];
  LCN = object["LCN"];
  DocumentNumber = object["DocumentNumber"];
  Volume = object["Volume"];
  PAGE = object["PAGE"];
  LeaseeName = object["LeaseeName"];
  AssigneeName = object["AssigneeName"];
  LeassorName = object["LeassorName"];
  AssignorName = object["AssignorName"];
  StateFips = object["StateFips"];
  CountyFips = object["CountyFips"];
  UnitDepth = object["UnitDepth"];
  FromDepth = object["FromDepth"];
  FromFrom = object["FromFrom"];
  ToDepth = object["ToDepth"];
  ToFrom = object["ToFrom"];
  WorkInt = object["WorkInt"];
  OrrInt = object["OrrInt"];
  NetAcres = object["NetAcres"];
  GrossAcres = object["GrossAcres"];
  NriAssign = object["NriAssign"];
  RcdDate = object["RcdDate"];
  Term = object["Term"];
//  TermUnitId = object["TermUnitId"];
  HBR = object["HBR"];
  Encumbrances = object["Encumbrances"];
  EffDate = object["EffDate"];
  PughClause = object["PughClause"];
  DepthLimitation = object["DepthLimitation"];
  ShutInClau = object["ShutInClau"];
  PoolingClau = object["PoolingClau"];
  MinimumPmt = object["MinimumPmt"];
  Author = object["Author"];
  Status = object["Status"];
  
  _uri = null;
//  IsDirty = false;
  }

/*
  protected function get dataMapper():DataMapper
  {
    return DataMapperRegistry.Instance.Lease;
  }
*/  
 
  public function getURI():String
  {

    if(_uri == null)
    {
     _uri = "TractInc.Lease." 
      
        + LeaseId.toString()
      ;
    }
     
    return _uri;
  }
}

}
    