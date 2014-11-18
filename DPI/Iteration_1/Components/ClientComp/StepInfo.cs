using System;
using System.Text;
using System.Collections;
using DPI.Interfaces;

namespace DPI.ClientComp
{
	public class StepInfo : IStepInfo
	{
		string   workflow; 
		int      wipId;
		string   stepName;
		DateTime stepStart;
		DateTime stepEnd;
		string   busObj	;
		string   busObjType;
		string   nextStepName;
		bool     isCompleted;
		string   user;

		public string   Workflow
		{ 
			get { return workflow; }
			set {workflow = value; } 
		}
		public int      WipId
		{ 
			get { return wipId;  } 
			set { wipId = value; }
		}
		public string   StepName
		{ 
			get { return stepName;  }
			set { stepName = value; }
		}
		public DateTime StepStart
		{ 
			get { return stepStart;  }
			set { stepStart = value; } 
		}
		public DateTime StepEnd	  
		{ 
			get { return stepEnd;  } 
			set { stepEnd = value; } 
		}
		public string   BusObj
		{ 
			get { return busObj;  }
			set { busObj = value; }
		}
		public string   BusObjType
		{ 
			get { return busObjType; }
			set {busObjType = value; } 
		}
		public string   NextStepName 
		{ 
			get { return nextStepName; } 
			set {nextStepName = value; }
		}
		public bool     IsCompleted
		{ 
			get { return isCompleted; }
			set { isCompleted = value; }
		}
		public string   User 
		{ 
			get { return user; } 
			set {user = value; }
		}
	}
}	