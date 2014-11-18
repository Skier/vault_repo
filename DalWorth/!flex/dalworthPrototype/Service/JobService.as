package Service
{
	import Domain.*;
	
	public class JobService
	{
		
		public static function createJob(job:Job):Job{
			
			return Database.Instance.createJob(job);
		}
		
		private function generateJobNumber():int{
			return 0;
		}
	}
}