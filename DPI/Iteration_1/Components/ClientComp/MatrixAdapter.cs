using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;

using DPI.Components;
using DPI.Interfaces;

namespace DPI.ClientComp
{
	public class FeatureMatrixAdapter
	{
		public static string[][] GetMatrix(IProdPrice[] products)
		{
			return Filter(Load(), products);
		}
		static string[][] Filter(string[][] rows, IProdPrice[] products)
		{
			int[] matchCols = Matching(rows[0], products); 

			string[][] res = new string[rows.Length][];
			
			for (int i = 0; i < res.Length; i++)
				res[i] = EstRow(rows[i], matchCols);

			return res;
		}
		static string[] EstRow(string[] matrixRow, int[] matchCol)
		{
			string[] newRow = new string[matchCol.Length];
			
			for (int i = 0; i < newRow.Length; i++)
				newRow[i] = matrixRow[matchCol[i]];

			return newRow;
		}
		static int[] Matching(string[] prodRow, IProdPrice[] products)
		{
			ArrayList ar = new ArrayList();
			
			ar.Add(0); // we always want the first column 

			for (int i = 1; i < prodRow.Length; i++)
				for (int j = 0; j < products.Length; j++)
					if (prodRow[i] == products[j].ProdId.ToString())
					{
						ar.Add(i);
						break;
					}

			int[] matchCols = new int[ar.Count];
			ar.CopyTo(matchCols);
			return matchCols;
		}
		static string[][] Load()
		{
			string line;
			string path 
				= (string) new System.Configuration.AppSettingsReader().GetValue(
				"FEATURESFILEPATH",typeof(string));
			ArrayList ar = new ArrayList();
			TextReader tr = new StreamReader(path); 
			
			while ((line = tr.ReadLine()) != null) 
				ar.Add(line.Split((char)9)); // tab delimetted
			tr.Close();

			string[][] rows = new string[ar.Count][];
			for (int i = 0; i < rows.Length; i++)
				rows[i] = (string[])ar[i];
		
			return rows;
		}
	}
}