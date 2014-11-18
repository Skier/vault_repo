/**
 * Copyright(c) 2006 "Midnight Coders, LLC". All Rights Reserved.
 */

package UI.ProcessManager
{
	import mx.formatters.NumberFormatter;
	
 	[Bindable]
	[RemoteClass(alias="Weborb.Samples.Ftp.Entities.ProcessStatus")]
	public class ProcessStatus
	{
		public static const INIT:String = "INIT";
		public static const DOWNLOADING:String = "DOWNLOADING";
		public static const DOWNLOAD_COMPLETED:String = "DOWNLOAD_COMPLETED";
		public static const UPLOADING:String = "UPLOADING";
		public static const UPLOAD_COMPLETED:String = "UPLOAD_COMPLETED";
		public static const LOADING:String = "LOADING";
		public static const LOADING_ERROR:String = "LOADING_ERROR";
		public static const LOAD_COMPLETED:String = "LOAD_COMPLETED";
        public static const CHECKING_DIRECTORY_SIZE:String = "CHECKING_DIRECTORY_SIZE";
        public static const COMPRESSING:String = "COMPRESSING";
		public static const URL_RETRIEVED:String = "Ready to load";
        public static const TERMINATED:String = "Terminated";
		public static const ERROR:String = "Error";
		
		public var ProcessId:String;
		public var Description:String = "";
        public var TotalBytes:int;
        public var ProcessedBytes:int;
        public var ExceptionMessage:String;
        
        private var numberFormatter:NumberFormatter = new NumberFormatter;
        
		private var state:String = INIT;
		
		public function set State(state:String):void 
		{
			this.state = state;
			RebuildDescription();
		}
		
		public function get State():String 
		{
			return state;
		}

        public function RebuildDescription():void 
        {
        	
        	numberFormatter.precision = 2;
        	numberFormatter.useThousandsSeparator = true;
        	
            switch (State){
                
                case INIT:
                    Description = "Waiting...";
                    break;

                case DOWNLOADING:
                    Description = "Downloaded " + numberFormatter.format(ProcessedBytes/1024) + "Kb of " + numberFormatter.format(TotalBytes/1024) + "Kb";
                    break;
                    
                case DOWNLOAD_COMPLETED:
                	Description = "Download completed";
                	break;
                	
                case UPLOADING:
                    Description = "Uploaded " + numberFormatter.format(ProcessedBytes/1024) + "Kb of " + numberFormatter.format(TotalBytes/1024) + "Kb";
                    break;
                    
                case UPLOAD_COMPLETED:
                	Description = "Upload completed";
                	break;
                
                case LOADING:
                    Description = "Loaded " + numberFormatter.format(ProcessedBytes/1024) + "Kb of " + numberFormatter.format(TotalBytes/1024) + "Kb";
                    break;
                    
                case LOAD_COMPLETED:
                	Description = "Load completed";
                	break;
                
                case LOADING_ERROR:
                	Description = "Loading error";
                	break;
                	
                case CHECKING_DIRECTORY_SIZE:
                	Description = "Checking directory size...";
                	break;
                	
                case COMPRESSING:
                	Description = "Compressing...";
                	break;
                	
                case URL_RETRIEVED:
                	Description = "Ready to load";
                	break;

                case ERROR:
                    Description = ExceptionMessage;
                    break;
                
                default:
                    Description = State;
            }
        }
	}
}