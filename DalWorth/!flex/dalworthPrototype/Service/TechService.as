package Service
{
	import  mx.collections.ArrayCollection;
	import Domain.Technician;
	import Domain.Route;
	import Domain.Technician;
	import Domain.Dispatcher;
	
	public class TechService
	{
		
		public static function getAllTechs(): ArrayCollection{
			return Database.Instance.getAllTechs();
		}
		
		public static function getRoute(tech:Technician):Route{
			return Database.Instance.getRoute(tech);
		}
		
	}
}