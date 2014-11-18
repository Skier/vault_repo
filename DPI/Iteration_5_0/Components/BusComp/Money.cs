using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
 
namespace DPI.Components
{
	public class Money 
	{
		public const int decimalPoints = 2;

		public static decimal round(decimal amt)
		{
			return Decimal.Round(amt, decimalPoints);
		}
		public static decimal[] allocate(decimal amount, int[] ratio)
		{
			long[] lratio = new long[ratio.Length];
			for (int i = 0; i < ratio.Length; i++)
				lratio[i] = ratio[i];
			
			return allocate(amount, lratio);
		}
		public static decimal[] allocate(decimal amount, long[] ratio)
		{
			validate(ratio);

			long total = 0;
			for (int i = 0; i < ratio.Length; i++)
				total += ratio[i];

			decimal reminder = round(amount);
			decimal[] results = new decimal[ratio.Length];
			
			for(int i = 0; i < results.Length; i++)
			{
				results[i] = round(amount * ratio[i] / total); 
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
		static void validate(long[] ratio)
		{
			for (int i = 0; i < ratio.Length; i++)
				if (ratio[i] < 0)
					throw new ArgumentException("Allocation ratio must be a positive number");
		}
	}
}			