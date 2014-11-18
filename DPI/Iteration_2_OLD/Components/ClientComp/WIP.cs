using System;
using System.IO;
using System.Text;
using System.Configuration;
using DPI.Interfaces;
using DPI.Services;
 
namespace DPI.ClientComp
{
	[Serializable]
	public abstract class WIP : IWIP, IMapObj, IComparable
	{
	#region Data

		[NonSerialized]
		protected WipStep step;
		string curUrl;
		public readonly int WipId;
		protected int busObjId; // Cust. number; order id; etc;
		
		protected string busObjType; // Acct, Order, etc
		DateTime time;
		string storeCode;
		string clerkId;
		string user;
		int prod;
		int wlProd;
		string prodGroup;

	#endregion
		
	#region Properties
		protected WipStep CurrStep 
		{
			get 
			{ 
				if (step == null)
					this.Sync();

				return step;
			} 
			set 
			{ 
				step = value; 
				curUrl = step.url;
			}
		}
		public string StoreCode 
		{ 
			get { return storeCode; } 
			set { storeCode = value; }
		}
		public string ClerkId 
		{ 
			get { return clerkId; }
			set { clerkId = value; }
		}
		public int BusObjId
		{
			get { return busObjId; }
			set { busObjId = value; }
		}
		public string BusObjType
		{
			get { return busObjType; }
			set { busObjType = value; }
		} 
		
		public IUOW Uow
		{
			get { return null;  }
			set 
			{
				throw new ApplicationException("WIP can't store UOW");
			}
		}		
		public object this[string attr]
		{
			get { return find(attr); }
			set { load(attr, value); }
		}	
		public string User          { get { return user; }}
		public int    Priority      { get { return 0; }}
		public static IDomKey IKeyS { get { return IDomKeyFactory.getKey("WIP", "wip"); }}
		public IDomKey IKey         { get { return IKeyS; }}
		public RowState RowState	{ get { return RowState.Clean; }}
		protected string Sql        { get { return null; }}
		public string Title         { get { return CurrStep.title; }}
		public string Url           { get { return CurrStep.url; }}
		public bool HasNext	        { get { return CurrStep.HasNext; }}
		public bool HasPrev         { get { return CurrStep.HasPrev; }}
		public bool HasRework       { get { return CurrStep.HasRework; }}
		public bool HasSkip	        { get { return CurrStep.HasSkip; }}
		public IWorkflow Workflow   { get { return CurrStep.Workflow; }}

		public virtual int StepNumber { get { return CurrStep.Workflow.CurrStep(CurrStep); }} 
		public virtual int StepCount  { get { return CurrStep.Workflow.Count; }}
		public virtual IWipStep FirstStep { get { return CurrStep.Workflow.FirstStep; }}
		public int Prod  
		{ 
			get { return prod; }
			set { prod = value; } 
		}
		public int WLProd 
		{ 
			get { return wlProd; }
			set { wlProd = value; } 
		}
		public string ProdGroup 
		{ 
			get { return prodGroup; }
			set { prodGroup = value; } 
		}

	#endregion

	#region	Constructors
		public WIP(string user, string clerkId, string storeCode)
		{
			this.clerkId = clerkId;
			this.storeCode = storeCode;
			this.user = user;

			time = DateTime.Now;
			WipId = new Random().Next(1, 1000000);
		}
	#endregion

	#region Methods
		public    void       add() {}
		public    void       save()	{}
		public    void       delete() {}
		public    void       deleteIt() {}
		public    void       checkExists() {}	
		public    void       removeFromIMap(IUOW uow)
		{
			uow.Imap.remove(IKeyS);
		}
		protected ISqlGateway loadSql() { return null; }
		public int CompareTo(object obj){return 0; }
		protected void Sync()//string url)
		{
			string urlSave = curUrl;

			CurrStep = (WipStep)FirstStep;
			while(CurrStep.url != urlSave)
				_Next();
		}	
		protected virtual object find(string attr)	{ return null; }
		protected virtual void   load(string attr, object obj) { }
		protected void _Next()
		{
			if (!CurrStep.HasNext)
				throw new ArgumentException("Step is unavailable for the current step '" + CurrStep.url + "'");

			CurrStep.Next(this);
		}
		public virtual string Next()
		{
			if (!CurrStep.HasNext)
				throw new ArgumentException("Step is unavailable for the current step '" + CurrStep.url + "'");

			string from = Url;
			CurrStep.Next(this);
			Log(from, Url);
			return Url;
		}
		public virtual string Prev()
		{
			if (!CurrStep.HasPrev)
				throw new ArgumentException("Previuos is unavailable for the current step '" + CurrStep.url + "'");

			string from = Url;
			CurrStep.Prev(this);
			Log(from, Url);
			return Url;
		}
		public virtual string Rework()
		{
			if (!CurrStep.HasRework)
				throw new ArgumentException("Rework is unavailable for the current step '" + CurrStep.url + "'");
			
			string from = Url;
			CurrStep.Rework(this);
			Log(from, Url);
			return Url;
		}
		public virtual string Current()
		{
			return CurrStep.url;
		}
		public virtual string Skip()
		{
			if (!CurrStep.HasSkip)
				throw new ArgumentException("Skip is unavailable for the current step '" + CurrStep.url + "'");
			
			string from = Url;
			CurrStep.Skip(this);
			Log(from, Url);
			return Url;				
		}
		public void LogBusObj(int busObj, string busObjType)
		{
			this.busObjId = busObj;
			this.busObjType = busObjType;
			Log(Url, Url);
		}
	#endregion

	#region Implementation
		void Log(string from, string to)
		{	
			IStepInfo step = new StepInfo();

			step.User        = user;
			step.BusObj      = busObjId.ToString();
			step.BusObjType  = busObjType;
			step.WipId       = WipId;
			step.IsCompleted = CurrStep.IsCompleted;
				
			step.StepName     = from;
			step.NextStepName = to;
			step.StepStart    = time;
			step.StepEnd      = DateTime.Now;

			step.Workflow = ToString().Substring(
				ToString().LastIndexOf(".") + 1, 
				ToString().Length - ToString().LastIndexOf(".") - 4);

			WorkflowSvc.LogStepInfo(step);	
			time = DateTime.Now;
		}
	#endregion

		[Serializable]
			public class WipStep : IWipStep
		{
		#region	Data

			IWorkflow workflow;
			public readonly string title;
			public readonly string url;
			protected WipStep nextStep;
			
			protected WipStep prevStep; 
			protected WipStep reworkStep; 
			protected WipStep skipStep; 
		#endregion

		#region Properties
			public IWipStep NextStep   { set { nextStep   = (WipStep)value; }} 
			public IWipStep PrevStep   { set { prevStep   = (WipStep)value; }} 
			public IWipStep ReworkStep { set { reworkStep = (WipStep)value; }} 
			public IWipStep SkipStep   { set { skipStep   = (WipStep)value; }} 

			public bool HasNext        { get { return nextStep   != null; }} 
			public bool HasPrev        { get { return prevStep   != null; }} 
			public bool HasRework      { get { return reworkStep != null; }} 
			public bool HasSkip        { get { return skipStep   != null; }} 
			public bool IsCompleted    { get { return nextStep   == null; }}
			
			public IWorkflow Workflow  { get { return workflow; }}
			public string Title        { get { return title; }}
		#endregion

		#region Constructors
			public WipStep(IWorkflow workflow, string title, string url)	
			{
				this.workflow = workflow;
				this.title = title;
				this.url = url;
			}
		#endregion

		#region Methods
			public virtual void Next(WIP wip)
			{
				if (!HasNext)
					throw new ApplicationException("No next step available");
				wip.CurrStep = wip.CurrStep.nextStep;
			}
			public virtual void Prev(WIP wip)
			{
				if (!HasPrev)
					throw new ApplicationException("No previous step available");
				wip.CurrStep = wip.CurrStep.prevStep;
			}
			public virtual void Rework(WIP wip)
			{
				if (!HasRework)
					throw new ApplicationException("No rework step available");
				wip.CurrStep = wip.CurrStep.reworkStep;
			}
			public virtual void Skip(WIP wip)
			{
				if (!HasSkip)
					throw new ApplicationException("No skip step available");
				wip.CurrStep = wip.CurrStep.skipStep;
			}
			public void SetNext(WipStep step)
			{
				nextStep = step;
			}
			public void SetPrev(WipStep step)
			{
				prevStep = step;
			}
			public void SetRework(WipStep step)
			{
				// clean wip
				reworkStep = step;
			}
			public void SetSkip(WipStep step)
			{
				skipStep = step;
			}
		#endregion

		#region Implementation
			protected virtual void SetCurrent(WIP wip, WipStep step)
			{
				wip.CurrStep = step;
			}
		#endregion
		}
	}
}