using System;
using DPI.Interfaces;

namespace DPI.ClientComp
{
	[Serializable]  
	public class AcctNotes: IAcctNotes
	{
		/*		Data		*/
		string notes;
		string user;
		DateTime date;

		/*		Properties		*/
		public string Text     
		{
			get { return notes; }
			set { notes = value; }
		} 
		public string User  
		{
			get { return user; }
			set { user = value; }
		}
		public DateTime Date     
		{
			get { return date; }
			set { date = value; }
		}
		/*		Constructors		*/
		public AcctNotes(string notes, string user, DateTime date)
		{
			this.notes = notes;
			this.user = user;
			this.date = date;
		}
		public AcctNotes(string notes, string user) : this(notes, user, DateTime.Now) {}
	}
}