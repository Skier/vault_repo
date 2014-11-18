
      package Domain
      {
        import Domain.Codegen.*;
        
        [Bindable]
        [RemoteClass(alias="TractInc.DocCapture.Domain.Tract")]
        public dynamic class Tract extends _Tract
        {
        
            public var isNew:Boolean;
            
            public function Tract():void {
                TractID = 0;
                DocID = 0;
                RefName = "";
                CalledAC = 0;
                ScopePlotUrl = "";
            }
        
        }
      }
    