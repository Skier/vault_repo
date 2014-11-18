using System;
using DPI.Interfaces;
 
namespace DPI.ClientComp
{
	public abstract class APageCriteria : IPageCriteria
	{
		protected string type;
		protected object val;
	
		public virtual string CritType
		{
			get { return type; }
			set { type = value; }
		}
		public abstract object Criter { get ; set; }
	}
}