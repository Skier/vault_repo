package UI
{
	import flash.net.SharedObject;
	import mx.controls.Alert;
	
	[Bindable]
	public class AppSettings
	{	
		private static var s_instance:AppSettings;
		private var m_sharedObject:SharedObject;
		
		private var m_showCodegenInstructions:Boolean = true;
		private var m_autoexpandTestDriveArgs:Boolean = true;
		
		
		
		public function AppSettings()
		{
			m_sharedObject = SharedObject.getLocal("WeborbSplash");
			
			if(m_sharedObject.data.ShowCodegenInstructions != null)
				m_showCodegenInstructions = new Boolean(m_sharedObject.data.ShowCodegenInstructions);
			
			if(m_sharedObject.data.AutoexpandTestDriveArgs != null)
				m_autoexpandTestDriveArgs = new Boolean(m_sharedObject.data.AutoexpandTestDriveArgs);
		}
		
		public static function get Instance():AppSettings
		{
			if(	s_instance == null )
				s_instance = new AppSettings();
				
			return s_instance;
		}
		
		public function get ShowCodegenInstructions():Boolean
		{
			return m_showCodegenInstructions;
		}
		
		public function set ShowCodegenInstructions(value:Boolean):void
		{
			m_showCodegenInstructions = value;
			m_sharedObject.data.ShowCodegenInstructions = value;
			m_sharedObject.flush();
		}
		
		public function get AutoexpandTestDriveArgs():Boolean
		{
			return m_autoexpandTestDriveArgs;
		}
		
		public function set AutoexpandTestDriveArgs(value:Boolean):void
		{
			m_autoexpandTestDriveArgs = value;
			m_sharedObject.data.AutoexpandTestDriveArgs = value;
			m_sharedObject.flush();
		}
	}
}