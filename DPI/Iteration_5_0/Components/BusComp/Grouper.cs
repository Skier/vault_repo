//Grouper
using System;
using System.Collections;

using DPI.Interfaces;

namespace DPI.Components
{
	public class Grouper 
	{
		public static ISummable[] Collapse (ISummable[] sum)
		{
			Hashtable htable = new Hashtable(sum.Length);
			
			for (int i = 0; i < sum.Length; i++)
				if (htable.ContainsKey(sum[i].SumType))
					((ISummable)htable[sum[i].SumType]).Amount += sum[i].Amount;
				else
					htable.Add(sum[i].SumType, sum[i]);
			
			return Convert(htable);
		}
		public static ISummable[] ConvertTo(object[] objs)
		{
			ISummable[] ism = new ISummable[objs.Length];
			for (int i = 0; i < ism.Length; i++)
			{
				if (!(objs[i] is ISummable))
					throw new ArgumentException("Grouper.ConverTo() objects must be of ISummable type");
				ism[i] = (ISummable)objs[i];
			}
			return ism;
		}
		public static ISummable[] Collapse (ISummable[] sum, ISummable[] items)
		{
			ISummable[] comb = Combine(sum, items);
			Hashtable htable = new Hashtable(500);
			
			for (int i = 0; i < comb.Length; i++)
				if (htable.ContainsKey(comb[i].SumType))
						((ISummable)htable[comb[i].SumType]).Amount += comb[i].Amount;
				else
					htable.Add(comb[i].SumType, comb[i]);
			
			return Convert(htable);
		}
		static ISummable[] Convert(Hashtable htable)
		{
			DictionaryEntry[] de = new DictionaryEntry[htable.Count];
			htable.CopyTo(de, 0);

			ISummable[] ret = new ISummable[htable.Count];			
			for (int i = 0; i < de.Length; i++)
				ret[i] = (ISummable)de[i].Value;
 
			return ret;
		}
		static ISummable[] Combine (ISummable[] sum, ISummable[] items)
		{
			if (sum == null)
				sum = new ISummable[0];

			if (items == null)
				items = new ISummable[0];

			ISummable[] comb = new ISummable[sum.Length + items.Length];
 
			sum.CopyTo(comb, 0);
			items.CopyTo(comb, sum.Length);
			return comb;
		}

		public static ISummable[] Group (ref ISummable[] sum, ISummable[] items)
		{
			if (sum == null)
				sum = new ISummable[0];
			
			Hashtable htable = new Hashtable();
			for (int i = 0; i < sum.Length; i++)
				htable.Add(sum[i].SumType , sum[i]);
			
			for (int i = 0; i < items.Length; i++)
				if (htable.ContainsKey(items[i].SumType))
					((ISummable)htable[items[i].SumType]).Amount += items[i].Amount;
				else
					htable.Add(items[i].SumType, items[i]);

			
			DictionaryEntry[] de = new DictionaryEntry[htable.Count];
			htable.CopyTo(de, 0);

			ISummable[] ret = new ISummable[htable.Count];			
			for (int i = 0; i < de.Length; i++)
				ret[i] = (ISummable)de[i].Value;
 
			return ret;
		}
	}
}