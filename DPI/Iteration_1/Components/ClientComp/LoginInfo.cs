using System;
using System.Text;
using System.Collections;
using DPI.Interfaces;

namespace DPI.ClientComp
{
//	public class LoginCol
//	{ 
//		static ArrayList ar;
// 
//		static LoginCol()
//		{
//			if (ar == null)
//				ar = new ArrayList(100);
//		}
//		public static void Add(LoginInfo info)
//		{
//			if (Find(info.key) == null)
//				ar.Add(info);
//				
//			//throw new ArgumentException("Duplicate Key :" + info.key.ToString());
//		}
//		public static void Remove(int key)
//		{
//			for (int i = 0; i < ar.Count; i++)
//				if (((LoginInfo)ar[i]).key == key)
//					ar.RemoveAt(i);
//		}
//		public static LoginInfo Find(int key)
//		{
//			for (int i = 0; i < ar.Count; i++)
//				if (((LoginInfo)ar[i]).key == key)
//					return (LoginInfo)ar[i];
//			
//			return null;
//		}
//	}
//	public class LoginInfo 
//	{
//		public readonly int key;
//		public readonly string acct;
//		public readonly object pw;
//
//		public LoginInfo(string acct, object pw)
//		{
//			Random random = new Random();
//			key = random.Next(10, 100000000);
//			this.acct = acct;
//			this.pw = pw;
//		}
//	}
}