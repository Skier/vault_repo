using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DPI.Interfaces;

namespace DPI.Components
{
	public class ProdDupFilter
	{
		/*		Static methods		*/
		public static ProdPrice[] getTopProduts(ProdPrice[] JobProducts)
		{
            if (JobProducts == null)
				throw new ArgumentException("Available products are required");

			ArrayList arSelected = new ArrayList();
			ArrayList arAvail = new ArrayList();

			// split into selected and availble / unavaivalable
			for (int i = 0; i < JobProducts.Length; i++)
				if (JobProducts[i].ProdSelState == ProdSelectionState.Selected)
					arSelected.Add(JobProducts[i]);
				else 
					arAvail.Add(JobProducts[i]);

			// mark duplicates
			foreach(ProdPrice unsel in arAvail)
				if (unsel.ProdSelState == ProdSelectionState.Available)
					foreach (ProdPrice sel in arSelected)
						if (unsel.ProdId == sel.ProdId)
						{
							unsel.ProdSelState =  ProdSelectionState.Unavailable;
							break;
						}
			// combine
			ProdPrice[] filtered = new ProdPrice[arAvail.Count + arSelected.Count];
			arSelected.CopyTo(filtered, 0);
			arAvail.CopyTo(filtered, arSelected.Count);

			return filtered;
		}
	}
}