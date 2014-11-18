using System;
using System.Text;
using System.Collections;
using DPI.Interfaces;

namespace DPI.Components
{
	public class Money2
	{
		public const int decimalPoints = 2;

		public static string ToPennies(decimal amt)
		{
			return decimal.ToInt32(decimal.Round(amt, 2) * 100).ToString();
		}
		public static string ToPennies(decimal amt, int zeros)
		{
			string mask = null;
			StringBuilder sb = new StringBuilder(zeros);
			
			for (int i = 0; i < zeros; i++)
				sb.Append("0");

			mask = sb.ToString();

			return decimal.ToInt32(decimal.Round(amt, 2) * 100).ToString(mask);
		}
		public static decimal Round(decimal amt)
		{
			return Decimal.Round(amt, decimalPoints);
		}
		public static decimal[] Allocate(decimal amount, int[] ratio)
		{
			long[] lratio = new long[ratio.Length];
			for (int i = 0; i < ratio.Length; i++)
				lratio[i] = ratio[i];
			
			return Allocate(amount, lratio);
		}
		public static decimal[] Allocate(decimal amount, long[] ratio)
		{	
			if (amount == 0)
				throw new ArgumentException("Ammount to allocate can't be zero");
			
			long total = Validate(ratio);
			
			decimal reminder = Round(amount);
			decimal[] results = new decimal[ratio.Length];

			for(int i = 0; i < results.Length; i++)
			{
				results[i] = Round(amount * ratio[i] / total); 
				reminder -= results[i];
			}
			decimal delta = -0.01m;  
			if (reminder > 0)
				delta = 0.01m;  // subtract a penny until done
	
			for (int i = 0; Math.Abs(reminder) > 0 && i < results.Length; i++)
			{
				results[i] += delta;
				reminder   -= delta;
			}
			return results;
		}
		public static decimal Truncate (string amount, int decimalPlaces)
		{
			if (amount.Trim().Length == 0)
				return Decimal.Zero;

			if (amount == null)
				return Decimal.Zero;
			
			if (decimalPlaces < 0)
				throw new ArgumentException("Decimal places must be a positive interger");

			if (amount.IndexOf("$") > -1)
				amount = amount.Substring(amount.IndexOf("$") + 1, amount.Length - 1);
		
			int power = 1;
			for (int i = 0; i < decimalPlaces; i++)
				power *= 10;

			return Decimal.Truncate(Decimal.Parse(amount) * power) / power;  
		}
		public static decimal Truncate (string amount)
		{
			return Truncate(amount, 2);
		}
		public static long Validate(long[] ratio)
		{
			long total = 0;

			for (int i = 0; i < ratio.Length; i++)
				if (ratio[i] < 0)
					throw new ArgumentException("Allocation ratio(s) must be positive numbers");
				else
					total += ratio[i];

			if (total == 0)
				throw new ArgumentException("At least 1 allocation ratio must be more than zero");

			return total;
		}
	}
}