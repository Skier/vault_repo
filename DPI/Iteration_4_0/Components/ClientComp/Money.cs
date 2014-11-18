using System;
using System.Text;
using System.Collections;

using DPI.Interfaces;
using DPI.Components;

namespace DPI.ClientComp
{
	public class Money
	{
		public static decimal Add(string amount, decimal addAmt)
		{
			return Truncate(amount) + Money2.Round(addAmt);
		}
		public static decimal Round(decimal amt)
		{
			return Money2.Round(amt);
		}
		public static decimal[] Allocate(decimal amount, int[] ratio)
		{
			return Money2.Allocate(amount, ratio);
		}
		public static string ToPennies(decimal amt)
		{
			return Money2.ToPennies(amt);
		}
		public static string ToPennies(decimal amt, int zeros)
		{
			return Money2.ToPennies(amt, zeros);
		}
		public static decimal[] Allocate(decimal amount, long[] ratio)
		{
			return Money2.Allocate(amount, ratio);
		}
		public static decimal Truncate (string amount, int decimalPlaces)
		{
			return Money2.Truncate(amount, decimalPlaces);
		}
		public static decimal Truncate (string amount)
		{
			return Money2.Truncate(amount);
		}
		static void Validate(long[] ratio)
		{
			Money2.Validate(ratio);
		}
	}
}