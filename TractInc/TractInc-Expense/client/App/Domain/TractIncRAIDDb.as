
      package App.Domain
      {
        import App.Domain.Codegen._TractIncRAIDDb;
        
        public final class TractIncRAIDDb extends _TractIncRAIDDb
        {
          private static var _instance:TractIncRAIDDb;
          
          public static function get Instance():TractIncRAIDDb
          {
            if( _instance == null )
              _instance = new TractIncRAIDDb();
              
            return   _instance;
          }
          
          
          
        }
    }
  