
      package Domain
      {
        import Domain.Codegen._DataMapperRegistry;

        public class DataMapperRegistry extends _DataMapperRegistry
        {
          		private static var s_instance:DataMapperRegistry;
		          public static function get Instance():DataMapperRegistry
		          {
			          if(s_instance == null)
				          s_instance = new DataMapperRegistry();
          				
			          return s_instance;
		          } 
        }
      }
    