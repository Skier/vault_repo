using System;
using DPI.Interfaces;
 
namespace DPI.ClientComp
{
	public abstract class APage : IPage
	{
		protected int rows;  // page size 
		protected IPageCriteria crit;
		protected PageDirection pd; // direction
		protected int pageNum; // 

		public int Rows
		{
			get { return rows; }
			set { rows = value; }
		}
		public IPageCriteria Criteria
		{
			get { return crit; }
			set { crit = value; }
		}
		public PageDirection Direction
		{
			get { return pd; }
			set { pd = value; }
		}
	}
}